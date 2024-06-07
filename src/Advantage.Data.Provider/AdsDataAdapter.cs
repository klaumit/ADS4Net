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
            m_selectCommand = selectCommand;
            mbExternalSelect = true;
        }

        public AdsDataAdapter(string selectCommandText, AdsConnection selectConnection)
        {
            m_selectCommand = new AdsCommand(selectCommandText);
            m_selectCommand.Connection = selectConnection;
        }

        public AdsDataAdapter(string selectCommandText, string selectConnectionString)
        {
            m_selectCommand = new AdsCommand(selectCommandText);
            m_selectCommand.Connection = new AdsConnection(selectConnectionString);
            m_selectCommand.Connection.Open();
        }

        ~AdsDataAdapter() => Dispose(false);

        protected override void Dispose(bool bExplicitDispose)
        {
            if (mbDisposed)
                return;
            lock (this)
            {
                if (mbDisposed)
                    return;
                if (bExplicitDispose)
                {
                    if (!mbExternalSelect && m_selectCommand != null)
                        m_selectCommand.Dispose();
                    m_selectCommand = null;
                }

                base.Dispose(bExplicitDispose);
                mbDisposed = true;
            }
        }

        [Category("Fill")]
        [Description("Used during Fill/FillSchema.")]
        public new AdsCommand SelectCommand
        {
            get => m_selectCommand;
            set => m_selectCommand = value;
        }

        IDbCommand IDbDataAdapter.SelectCommand
        {
            get => m_selectCommand;
            set => m_selectCommand = (AdsCommand)value;
        }

        [Category("Update")]
        [Description("Used during Update for new rows in DataSet.")]
        public new AdsCommand InsertCommand
        {
            get => m_insertCommand;
            set => m_insertCommand = value;
        }

        IDbCommand IDbDataAdapter.InsertCommand
        {
            get => m_insertCommand;
            set => m_insertCommand = (AdsCommand)value;
        }

        [Category("Update")]
        [Description("Used during Update for modified rows in DataSet.")]
        public new AdsCommand UpdateCommand
        {
            get => m_updateCommand;
            set => m_updateCommand = value;
        }

        IDbCommand IDbDataAdapter.UpdateCommand
        {
            get => m_updateCommand;
            set => m_updateCommand = (AdsCommand)value;
        }

        [Description("Used during Update for deleted rows in DataSet.")]
        [Category("Update")]
        public new AdsCommand DeleteCommand
        {
            get => m_deleteCommand;
            set => m_deleteCommand = value;
        }

        IDbCommand IDbDataAdapter.DeleteCommand
        {
            get => m_deleteCommand;
            set => m_deleteCommand = (AdsCommand)value;
        }

        protected override RowUpdatedEventArgs CreateRowUpdatedEvent(
            DataRow dataRow,
            IDbCommand command,
            StatementType statementType,
            DataTableMapping tableMapping)
        {
            return new AdsRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
        }

        protected override RowUpdatingEventArgs CreateRowUpdatingEvent(
            DataRow dataRow,
            IDbCommand command,
            StatementType statementType,
            DataTableMapping tableMapping)
        {
            return new AdsRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
        }

        protected override void OnRowUpdating(RowUpdatingEventArgs value)
        {
            var updatingEventHandler =
                (AdsRowUpdatingEventHandler)Events[EventRowUpdating];
            if (updatingEventHandler == null || !(value is AdsRowUpdatingEventArgs))
                return;
            updatingEventHandler(this, (AdsRowUpdatingEventArgs)value);
        }

        protected override void OnRowUpdated(RowUpdatedEventArgs value)
        {
            var updatedEventHandler =
                (AdsRowUpdatedEventHandler)Events[EventRowUpdated];
            if (updatedEventHandler == null || !(value is AdsRowUpdatedEventArgs))
                return;
            updatedEventHandler(this, (AdsRowUpdatedEventArgs)value);
        }

        public event AdsRowUpdatingEventHandler RowUpdating
        {
            add => Events.AddHandler(EventRowUpdating, value);
            remove => Events.RemoveHandler(EventRowUpdating, value);
        }

        public event AdsRowUpdatedEventHandler RowUpdated
        {
            add => Events.AddHandler(EventRowUpdated, value);
            remove => Events.RemoveHandler(EventRowUpdated, value);
        }
    }
}