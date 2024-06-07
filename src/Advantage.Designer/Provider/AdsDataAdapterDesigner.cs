using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using VSLangProj;

namespace Advantage.Data.Provider
{
    public class AdsDataAdapterDesigner : ComponentDesigner
    {
        private DesignerVerbCollection mVerbs;
        private AdsDataAdapter mAdapter;

        public new void Dispose()
        {
            base.Dispose();
            ((IComponentChangeService)GetService(typeof(IComponentChangeService))).ComponentRemoving -=
                OnComponentRemoving;
        }

        public override void Initialize(IComponent component)
        {
            mAdapter = component as AdsDataAdapter;
            ((IComponentChangeService)mAdapter.Site.GetService(
                    typeof(IComponentChangeService))).ComponentRemoving +=
                OnComponentRemoving;
            base.Initialize(component);
        }

        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            if (e.Component != Component)
                return;
            var service = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (mAdapter.SelectCommand != null)
                service.DestroyComponent(mAdapter.SelectCommand);
            if (mAdapter.DeleteCommand != null)
                service.DestroyComponent(mAdapter.DeleteCommand);
            if (mAdapter.InsertCommand != null)
                service.DestroyComponent(mAdapter.InsertCommand);
            if (mAdapter.UpdateCommand == null)
                return;
            service.DestroyComponent(mAdapter.UpdateCommand);
        }

        public override void DoDefaultAction()
        {
            var service1 = (IEventBindingService)GetService(typeof(IEventBindingService));
            var service2 = (IDesignerHost)GetService(typeof(IDesignerHost));
            DesignerTransaction designerTransaction = null;
            EventDescriptor e = null;
            string str = null;
            try
            {
                e = TypeDescriptor.GetEvents(mAdapter)["RowUpdated"];
                var eventProperty = service1.GetEventProperty(e);
                if (service2 != null && designerTransaction == null)
                    designerTransaction = service2.CreateTransaction(e.Name);
                str = (string)eventProperty.GetValue(mAdapter);
                if (str == null)
                {
                    str = service1.CreateUniqueMethodName(mAdapter, e);
                    eventProperty.SetValue(mAdapter, str);
                }
            }
            finally
            {
                designerTransaction?.Commit();
            }

            if (e == null || str == null)
                return;
            service1.ShowCode(mAdapter, e);
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                var flag = false;
                if (mVerbs == null)
                {
                    mVerbs = new DesignerVerbCollection();
                    mVerbs.Add(new DesignerVerb("Configure Data Adapter...", OnConfigure));
                    try
                    {
                        Marshal.GetActiveObject("VisualStudio.DTE");
                        flag = true;
                    }
                    catch
                    {
                    }

                    if (flag)
                        mVerbs.Add(new DesignerVerb("Generate Dataset...", OnGenerate));
                    mVerbs.Add(new DesignerVerb("Preview Data...", OnPreview));
                }

                return mVerbs;
            }
        }

        [Obsolete]
        public override void OnSetComponentDefaults()
        {
            OnConfigure(null, null);
            base.OnSetComponentDefaults();
        }

        public override ICollection AssociatedComponents
        {
            get
            {
                var associatedComponents = new ArrayList();
                if (mAdapter != null)
                {
                    if (mAdapter.SelectCommand != null)
                        associatedComponents.Add(mAdapter.SelectCommand);
                    if (mAdapter.DeleteCommand != null)
                        associatedComponents.Add(mAdapter.DeleteCommand);
                    if (mAdapter.InsertCommand != null)
                        associatedComponents.Add(mAdapter.InsertCommand);
                    if (mAdapter.UpdateCommand != null)
                        associatedComponents.Add(mAdapter.UpdateCommand);
                }

                return associatedComponents;
            }
        }

        protected void OnGenerate(object sender, EventArgs e)
        {
            var sortedList1 = new SortedList();
            if (mAdapter == null)
            {
                var num1 = (int)MessageBox.Show("AdsDataAdapter is invalid. Cannot configure.", "Component Error");
            }
            else
            {
                var fileName = "";
                ProjectItem projectItem1 = null;
                var sortedList2 = new SortedList();
                var str = "";
                var service1 =
                    (IDesignerHost)mAdapter.Site.GetService(
                        typeof(IDesignerHost));
                var activeObject = (DTE2)Marshal.GetActiveObject("VisualStudio.DTE");
                var containingProject = activeObject.ActiveDocument.ProjectItem.ContainingProject;
                if (containingProject.CodeModel.Language != "{B5E9BD33-6D3E-4B5D-925E-8A43B79820B4}" &&
                    containingProject.CodeModel.Language != "{B5E9BD34-6D3E-4B5D-925E-8A43B79820B4}")
                {
                    var num2 = (int)MessageBox.Show("Unsupported language. Currently support VC# and VB",
                        "Component Error");
                }
                else
                {
                    var vsProject = (VSProject)containingProject.Object;
                    var genDataSetForm = new GenDataSetForm();
                    genDataSetForm.ebNew.Text =
                        vsProject.GetUniqueFilename(containingProject, "AdsDataSet", ".xsd");
                    genDataSetForm.ebNew.Text =
                        genDataSetForm.ebNew.Text.Substring(0, genDataSetForm.ebNew.Text.Length - 4);
                    foreach (ProjectItem projectItem2 in containingProject.ProjectItems)
                    {
                        if (projectItem2.Name.ToUpper().EndsWith(".XSD"))
                        {
                            sortedList2.Add(projectItem2.Name.Substring(0, projectItem2.Name.Length - 4),
                                new ExistingDataset(
                                    projectItem2.Name.Substring(0, projectItem2.Name.Length - 4), projectItem2.Name,
                                    projectItem2));
                            genDataSetForm.comboExisting.Items.Add(
                                projectItem2.Name.Substring(0, projectItem2.Name.Length - 4));
                        }
                    }

                    for (var index = 0; index < service1.Container.Components.Count; ++index)
                    {
                        if (service1.Container.Components[index].GetType() == (object)typeof(AdsDataAdapter))
                        {
                            var dataSet = new DataSet();
                            var component = (AdsDataAdapter)service1.Container.Components[index];
                            component.FillSchema(dataSet, SchemaType.Mapped);
                            if (dataSet.Tables.Count > 0)
                            {
                                var adapterTablePair =
                                    new AdapterTablePair(dataSet.Tables[0].TableName, component);
                                sortedList1.Add(adapterTablePair.Name, adapterTablePair);
                                genDataSetForm.TableList.Add(adapterTablePair.Name);
                                if (mAdapter == component)
                                    genDataSetForm.iThisAdapterIndex = genDataSetForm.TableList.Count - 1;
                            }
                        }
                    }

                    if (genDataSetForm.ShowDialog() != DialogResult.OK)
                        return;
                    var dataSet1 = new DataSet();
                    if (genDataSetForm.rbExisting.Checked)
                    {
                        var existingDataset =
                            (ExistingDataset)sortedList2[
                                genDataSetForm.comboExisting.Text];
                        if (existingDataset != null)
                        {
                            dataSet1.ReadXmlSchema(existingDataset.SchemaFile);
                            fileName = existingDataset.SchemaFile;
                            projectItem1 = existingDataset.DatasetProjectItem;
                            str = existingDataset.Name;
                        }
                        else
                        {
                            var num3 = (int)MessageBox.Show("Internal Error. Unable to locate existing dataset.",
                                "Component Error");
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (containingProject.CodeModel.Language == "{B5E9BD34-6D3E-4B5D-925E-8A43B79820B4}")
                            {
                                var projectItemTemplate =
                                    ((Solution2)activeObject.Solution).GetProjectItemTemplate("DataSet.zip", "CSharp");
                                projectItem1 = containingProject.ProjectItems.AddFromTemplate(projectItemTemplate,
                                    genDataSetForm.ebNew.Text + ".xsd");
                            }
                            else if (containingProject.CodeModel.Language == "{B5E9BD33-6D3E-4B5D-925E-8A43B79820B4}")
                            {
                                var projectItemTemplate =
                                    ((Solution2)activeObject.Solution).GetProjectItemTemplate("DataSet.zip",
                                        "VisualBasic");
                                projectItem1 = containingProject.ProjectItems.AddFromTemplate(projectItemTemplate,
                                    genDataSetForm.ebNew.Text + ".xsd");
                            }

                            if (projectItem1 == null)
                            {
                                foreach (ProjectItem projectItem3 in containingProject.ProjectItems)
                                {
                                    if (projectItem3.Name == genDataSetForm.ebNew.Text + ".xsd")
                                    {
                                        projectItem1 = projectItem3;
                                        break;
                                    }
                                }
                            }

                            fileName = projectItem1.Document.FullName;
                            str = genDataSetForm.ebNew.Text;
                            activeObject.ActiveDocument.Close((vsSaveChanges)1);
                            dataSet1.DataSetName = genDataSetForm.ebNew.Text;
                        }
                        catch (Exception ex)
                        {
                            var num4 = (int)MessageBox.Show(ex.ToString(), "Error Generating Typed Dataset");
                        }
                    }

                    foreach (var checkedItem in genDataSetForm.lbTables.CheckedItems)
                        (((AdapterTablePair)sortedList1[
                            checkedItem.ToString()])?.Adapter).FillSchema(dataSet1, SchemaType.Mapped);
                    dataSet1.WriteXmlSchema(fileName);
                    ((VSProjectItem)projectItem1.Object).RunCustomTool();
                    if (!genDataSetForm.cbAddToDesigner.Checked)
                        return;
                    var service2 =
                        (INameCreationService)mAdapter.Site.GetService(
                            typeof(INameCreationService));
                    service1.CreateComponent(Type.GetType(containingProject.Name + "." + str),
                        service2.CreateName(service1.Container,
                            Type.GetType(containingProject.Name + "." + str)));
                }
            }
        }

        protected void OnConfigure(object sender, EventArgs e)
        {
            if (mAdapter == null)
            {
                var num1 = (int)MessageBox.Show("AdsDataAdapter is invalid. Cannot configure.", "Component Error");
            }
            else
            {
                var service1 =
                    (IDesignerHost)mAdapter.Site.GetService(
                        typeof(IDesignerHost));
                var arrayList = new ArrayList();
                foreach (IComponent component in service1.Container.Components)
                {
                    if (component is AdsConnection adsConnection)
                        arrayList.Add(adsConnection);
                }

                var service2 =
                    (INameCreationService)mAdapter.Site.GetService(
                        typeof(INameCreationService));
                var name1 = "";
                var name2 = "";
                var name3 = "";
                var name4 = "";
                if (mAdapter.SelectCommand != null)
                    name1 = mAdapter.SelectCommand.Site.Name;
                if (mAdapter.InsertCommand != null)
                    name2 = mAdapter.InsertCommand.Site.Name;
                if (mAdapter.DeleteCommand != null)
                    name3 = mAdapter.DeleteCommand.Site.Name;
                if (mAdapter.UpdateCommand != null)
                    name4 = mAdapter.UpdateCommand.Site.Name;
                var num2 = 0;
                var str1 = "";
                if (mAdapter.SelectCommand != null)
                {
                    var str2 = "adsSelectCommand";
                    if (name1.IndexOf(str2) != -1)
                    {
                        str1 = name1.Remove(0, str2.Length);
                        num2 = -1;
                    }
                }

                while (num2++ < 1000)
                {
                    if (num2 > 0)
                        str1 = num2.ToString();
                    if (mAdapter.SelectCommand == null)
                    {
                        name1 = "adsSelectCommand" + str1;
                        if (!service2.IsValidName(name1))
                            continue;
                    }

                    if (mAdapter.InsertCommand == null)
                    {
                        name2 = "adsInsertCommand" + str1;
                        if (!service2.IsValidName(name2))
                            continue;
                    }

                    if (mAdapter.DeleteCommand == null)
                    {
                        name3 = "adsDeleteCommand" + str1;
                        if (!service2.IsValidName(name3))
                            continue;
                    }

                    if (mAdapter.UpdateCommand == null)
                    {
                        name4 = "adsUpdateCommand" + str1;
                        if (service2.IsValidName(name4))
                            break;
                    }
                    else
                        break;
                }

                var configWizard = new ConfigWizard();
                AdsConnection adsConnection1 = null;
                if (mAdapter.SelectCommand != null && mAdapter.SelectCommand.Connection != null)
                {
                    configWizard.ConnectionString =
                        mAdapter.SelectCommand.Connection.ConnectionString;
                    adsConnection1 = mAdapter.SelectCommand.Connection;
                }

                if (mAdapter.SelectCommand != null)
                {
                    configWizard.SelectCommand.CommandText =
                        mAdapter.SelectCommand.CommandText;
                    configWizard.SelectCommand.CommandType =
                        mAdapter.SelectCommand.CommandType;
                    CopyParams(mAdapter.SelectCommand.Parameters, configWizard.SelectCommand.Parameters);
                }

                if (mAdapter.InsertCommand != null)
                {
                    configWizard.InsertCommand.CommandText =
                        mAdapter.InsertCommand.CommandText;
                    configWizard.InsertCommand.CommandType =
                        mAdapter.InsertCommand.CommandType;
                    CopyParams(mAdapter.InsertCommand.Parameters, configWizard.InsertCommand.Parameters);
                }

                if (mAdapter.DeleteCommand != null)
                {
                    configWizard.DeleteCommand.CommandText =
                        mAdapter.DeleteCommand.CommandText;
                    configWizard.DeleteCommand.CommandType =
                        mAdapter.DeleteCommand.CommandType;
                    CopyParams(mAdapter.DeleteCommand.Parameters, configWizard.DeleteCommand.Parameters);
                }

                if (mAdapter.UpdateCommand != null)
                {
                    configWizard.UpdateCommand.CommandText =
                        mAdapter.UpdateCommand.CommandText;
                    configWizard.UpdateCommand.CommandType =
                        mAdapter.UpdateCommand.CommandType;
                    CopyParams(mAdapter.UpdateCommand.Parameters, configWizard.UpdateCommand.Parameters);
                }

                configWizard.AdapterName = Component.Site.Name;
                var dialogResult = DialogResult.OK;
                try
                {
                    while (dialogResult == DialogResult.OK &&
                           configWizard.mNextDlg != ConfigWizard.WizardDialogs.AllDone)
                    {
                        switch (configWizard.mNextDlg)
                        {
                            case ConfigWizard.WizardDialogs.Welcome:
                                dialogResult = configWizard.mWelcomeDlg.ShowDialog();
                                continue;
                            case ConfigWizard.WizardDialogs.Connection:
                                dialogResult = configWizard.mConnectionDlg.ShowDialog();
                                continue;
                            case ConfigWizard.WizardDialogs.QueryType:
                                dialogResult = configWizard.mQueryTypeDlg.ShowDialog();
                                continue;
                            case ConfigWizard.WizardDialogs.QueryBuild:
                                dialogResult = configWizard.mQueryBuildDlg.ShowDialog();
                                continue;
                            case ConfigWizard.WizardDialogs.StoredProc:
                                dialogResult = configWizard.mStoredProcDlg.ShowDialog();
                                continue;
                            case ConfigWizard.WizardDialogs.TableDirect:
                                dialogResult = configWizard.mTableDirectDlg.ShowDialog();
                                continue;
                            case ConfigWizard.WizardDialogs.Results:
                                configWizard.mResultsDlg.Hidden = false;
                                dialogResult = configWizard.mResultsDlg.ShowDialog();
                                continue;
                            case ConfigWizard.WizardDialogs.Finish:
                                configWizard.mResultsDlg.Hidden = true;
                                dialogResult = configWizard.mResultsDlg.ShowDialog();
                                continue;
                            default:
                                continue;
                        }
                    }

                    if (dialogResult != DialogResult.OK)
                        return;
                    if (mAdapter.SelectCommand != null)
                    {
                        service1.DestroyComponent(mAdapter.SelectCommand);
                        mAdapter.SelectCommand = null;
                    }

                    if (mAdapter.DeleteCommand != null)
                    {
                        service1.DestroyComponent(mAdapter.DeleteCommand);
                        mAdapter.DeleteCommand = null;
                    }

                    if (mAdapter.InsertCommand != null)
                    {
                        service1.DestroyComponent(mAdapter.InsertCommand);
                        mAdapter.InsertCommand = null;
                    }

                    if (mAdapter.UpdateCommand != null)
                    {
                        service1.DestroyComponent(mAdapter.UpdateCommand);
                        mAdapter.UpdateCommand = null;
                    }

                    if (adsConnection1 == null ||
                        adsConnection1.ConnectionString != configWizard.ConnectionString)
                    {
                        adsConnection1 = null;
                        foreach (var obj in arrayList)
                        {
                            if (((DbConnection)obj).ConnectionString == configWizard.ConnectionString)
                            {
                                adsConnection1 = (AdsConnection)obj;
                                break;
                            }
                        }

                        if (adsConnection1 == null)
                        {
                            var name5 = service2.CreateName(service1.Container, typeof(AdsConnection));
                            adsConnection1 = (AdsConnection)service1.CreateComponent(typeof(AdsConnection), name5);
                            adsConnection1.ConnectionString = configWizard.ConnectionString;
                        }
                    }

                    if (configWizard.SelectCommand != null)
                    {
                        IComponent component = new AdsCommand();
                        ((DbCommand)component).DesignTimeVisible = false;
                        service1.Container.Add(component, name1);
                        mAdapter.SelectCommand = component as AdsCommand;
                        mAdapter.SelectCommand.CommandText =
                            configWizard.SelectCommand.CommandText;
                        mAdapter.SelectCommand.CommandType =
                            configWizard.SelectCommand.CommandType;
                        mAdapter.SelectCommand.Connection = adsConnection1;
                        CopyParams(configWizard.SelectCommand.Parameters, mAdapter.SelectCommand.Parameters);
                    }

                    if (configWizard.InsertCommand != null)
                    {
                        IComponent component = new AdsCommand();
                        ((DbCommand)component).DesignTimeVisible = false;
                        service1.Container.Add(component, name2);
                        mAdapter.InsertCommand = component as AdsCommand;
                        mAdapter.InsertCommand.CommandText =
                            configWizard.InsertCommand.CommandText;
                        mAdapter.InsertCommand.CommandType =
                            configWizard.InsertCommand.CommandType;
                        mAdapter.InsertCommand.Connection = adsConnection1;
                        CopyParams(configWizard.InsertCommand.Parameters, mAdapter.InsertCommand.Parameters);
                    }

                    if (configWizard.DeleteCommand != null)
                    {
                        IComponent component = new AdsCommand();
                        ((DbCommand)component).DesignTimeVisible = false;
                        service1.Container.Add(component, name3);
                        mAdapter.DeleteCommand = component as AdsCommand;
                        mAdapter.DeleteCommand.CommandText =
                            configWizard.DeleteCommand.CommandText;
                        mAdapter.DeleteCommand.CommandType =
                            configWizard.DeleteCommand.CommandType;
                        mAdapter.DeleteCommand.Connection = adsConnection1;
                        CopyParams(configWizard.DeleteCommand.Parameters, mAdapter.DeleteCommand.Parameters);
                    }

                    if (configWizard.UpdateCommand != null)
                    {
                        IComponent component = new AdsCommand();
                        ((DbCommand)component).DesignTimeVisible = false;
                        service1.Container.Add(component, name4);
                        mAdapter.UpdateCommand = component as AdsCommand;
                        mAdapter.UpdateCommand.CommandText =
                            configWizard.UpdateCommand.CommandText;
                        mAdapter.UpdateCommand.CommandType =
                            configWizard.UpdateCommand.CommandType;
                        mAdapter.UpdateCommand.Connection = adsConnection1;
                        CopyParams(configWizard.UpdateCommand.Parameters, mAdapter.UpdateCommand.Parameters);
                    }

                    mAdapter.TableMappings.Clear();
                    mAdapter.TableMappings.AddRange(new DataTableMapping[1]
                    {
                        configWizard.TableMapping
                    });
                }
                catch (Exception ex)
                {
                    var num3 = (int)MessageBox.Show("An unexpected error has occured.\n\n" + ex,
                        "Data Adapter Configuration Wizard");
                }
            }
        }

        private void CopyParams(AdsParameterCollection src, AdsParameterCollection dst)
        {
            dst.Clear();
            foreach (AdsParameter adsParameter in (DbParameterCollection)src)
            {
                if (adsParameter.Direction != ParameterDirection.Output)
                    dst.Add(new AdsParameter(adsParameter.ParameterName,
                        adsParameter.DbType, adsParameter.Size,
                        adsParameter.Direction, adsParameter.IsNull, adsParameter.Precision,
                        adsParameter.Scale, adsParameter.SourceColumn,
                        adsParameter.SourceVersion, adsParameter.Value));
            }
        }

        private void OnPreview(object sender, EventArgs e)
        {
            var num = (int)new PreviewDlg(mAdapter, Component.Site.Name).ShowDialog();
        }

        public class AdapterTablePair
        {
            public string Name;
            private string mstrTableName;
            public AdsDataAdapter Adapter;

            public AdapterTablePair(string strTableName, AdsDataAdapter oAdapter)
            {
                mstrTableName = strTableName;
                Adapter = oAdapter;
                Name = strTableName + " (" + oAdapter.Site.Name + ")";
            }
        }

        private class ExistingDataset
        {
            public string Name;
            public string SchemaFile;
            public ProjectItem DatasetProjectItem;

            public ExistingDataset(string strName, string strSchemaFile, ProjectItem oProjectItem)
            {
                Name = strName;
                SchemaFile = strSchemaFile;
                DatasetProjectItem = oProjectItem;
            }
        }
    }
}