using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;

namespace Advantage.Data.Provider
{
    [DesignerCategory("Form")]
    [ToolboxBitmap(typeof(AdsDataAdapter), "adsdataadapter.bmp")]
    [Designer("Advantage.Data.Provider.AdsDataAdapterDesigner, Advantage.Designer")]
    public sealed class AdsDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, IDisposable
    {
        private bool mbDisposed;
        private AdsCommand m_selectCommand;
        private bool mbExternalSelect;
        private AdsCommand m_insertCommand;
        private AdsCommand m_updateCommand;
        private AdsCommand m_deleteCommand;
        private static readonly object EventRowUpdated = new object();
        private static readonly object EventRowUpdating = new object();

        public AdsDataAdapter()
        {
        }

        public AdsDataAdapter(AdsCommand selectCommand)
        {
            this.m_selectCommand = selectCommand;
            this.mbExternalSelect = true;
        }

        public AdsDataAdapter(string selectCommandText, AdsConnection selectConnection)
        {
            this.m_selectCommand = new AdsCommand(selectCommandText);
            this.m_selectCommand.Connection = selectConnection;
        }

        public AdsDataAdapter(string selectCommandText, string selectConnectionString)
        {
            this.m_selectCommand = new AdsCommand(selectCommandText);
            this.m_selectCommand.Connection = new AdsConnection(selectConnectionString);
            this.m_selectCommand.Connection.Open();
        }

        ~AdsDataAdapter() => this.Dispose(false);

        protected override void Dispose(bool bExplicitDispose)
        {
            if (this.mbDisposed)
                return;
            lock (this)
            {
                if (this.mbDisposed)
                    return;
                if (bExplicitDispose)
                {
                    if (!this.mbExternalSelect && this.m_selectCommand != null)
                        this.m_selectCommand.Dispose();
                    this.m_selectCommand = (AdsCommand)null;
                }

                base.Dispose(bExplicitDispose);
                this.mbDisposed = true;
            }
        }

        [Category("Fill")]
        [Description("Used during Fill/FillSchema.")]
        public new AdsCommand SelectCommand
        {
            get => this.m_selectCommand;
            set => this.m_selectCommand = value;
        }

        IDbCommand IDbDataAdapter.SelectCommand
        {
            get => (IDbCommand)this.m_selectCommand;
            set => this.m_selectCommand = (AdsCommand)value;
        }

        [Category("Update")]
        [Description("Used during Update for new rows in DataSet.")]
        public new AdsCommand InsertCommand
        {
            get => this.m_insertCommand;
            set => this.m_insertCommand = value;
        }

        IDbCommand IDbDataAdapter.InsertCommand
        {
            get => (IDbCommand)this.m_insertCommand;
            set => this.m_insertCommand = (AdsCommand)value;
        }

        [Category("Update")]
        [Description("Used during Update for modified rows in DataSet.")]
        public new AdsCommand UpdateCommand
        {
            get => this.m_updateCommand;
            set => this.m_updateCommand = value;
        }

        IDbCommand IDbDataAdapter.UpdateCommand
        {
            get => (IDbCommand)this.m_updateCommand;
            set => this.m_updateCommand = (AdsCommand)value;
        }

        [Description("Used during Update for deleted rows in DataSet.")]
        [Category("Update")]
        public new AdsCommand DeleteCommand
        {
            get => this.m_deleteCommand;
            set => this.m_deleteCommand = value;
        }

        IDbCommand IDbDataAdapter.DeleteCommand
        {
            get => (IDbCommand)this.m_deleteCommand;
            set => this.m_deleteCommand = (AdsCommand)value;
        }

        protected override RowUpdatedEventArgs CreateRowUpdatedEvent(
            DataRow dataRow,
            IDbCommand command,
            StatementType statementType,
            DataTableMapping tableMapping)
        {
            return (RowUpdatedEventArgs)new AdsRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
        }

        protected override RowUpdatingEventArgs CreateRowUpdatingEvent(
            DataRow dataRow,
            IDbCommand command,
            StatementType statementType,
            DataTableMapping tableMapping)
        {
            return (RowUpdatingEventArgs)new AdsRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
        }

        protected override void OnRowUpdating(RowUpdatingEventArgs value)
        {
            AdsRowUpdatingEventHandler updatingEventHandler =
                (AdsRowUpdatingEventHandler)this.Events[AdsDataAdapter.EventRowUpdating];
            if (updatingEventHandler == null || !(value is AdsRowUpdatingEventArgs))
                return;
            updatingEventHandler((object)this, (AdsRowUpdatingEventArgs)value);
        }

        protected override void OnRowUpdated(RowUpdatedEventArgs value)
        {
            AdsRowUpdatedEventHandler updatedEventHandler =
                (AdsRowUpdatedEventHandler)this.Events[AdsDataAdapter.EventRowUpdated];
            if (updatedEventHandler == null || !(value is AdsRowUpdatedEventArgs))
                return;
            updatedEventHandler((object)this, (AdsRowUpdatedEventArgs)value);
        }

        public event AdsRowUpdatingEventHandler RowUpdating
        {
            add => this.Events.AddHandler(AdsDataAdapter.EventRowUpdating, (Delegate)value);
            remove => this.Events.RemoveHandler(AdsDataAdapter.EventRowUpdating, (Delegate)value);
        }

        public event AdsRowUpdatedEventHandler RowUpdated
        {
            add => this.Events.AddHandler(AdsDataAdapter.EventRowUpdated, (Delegate)value);
            remove => this.Events.RemoveHandler(AdsDataAdapter.EventRowUpdated, (Delegate)value);
        }
    }
}