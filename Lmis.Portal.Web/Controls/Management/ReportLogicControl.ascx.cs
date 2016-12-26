using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Entites;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
    public partial class ReportLogicControl : BaseExtendedControl<ReportLogicModel>
    {
        public List<BindingInfoModel> Bindings
        {
            get
            {
                var list = ViewState["Bindings"] as List<BindingInfoModel>;
                list = (list ?? new List<BindingInfoModel>());

                return list;
            }
            set
            {
                ViewState["Bindings"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyViewMode();
            FillComboBoxes();
            FillBindingsGrids();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            FillBindingsGrids();
        }

        protected void cbxTable_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            OnDataChanged();
        }

        protected void cbxLogic_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            OnDataChanged();
        }

        protected void btnEditChartBinding_OnCommand(object sender, CommandEventArgs e)
        {
            OnDataChanged();
        }

        protected void btnDeleteChartBinding_OnCommand(object sender, CommandEventArgs e)
        {
            if (Bindings == null)
            {
                OnDataChanged();
                return;
            }

            var entity = (from n in GetChartBindings()
                          where n.Key == Convert.ToString(e.CommandArgument)
                          select n).FirstOrDefault();

            if (entity != null)
            {
                if (entity.XBinding != null)
                    Bindings.Remove(entity.XBinding);

                if (entity.YBinding != null)
                    Bindings.Remove(entity.YBinding);
            }

            OnDataChanged();
        }

        protected void btnEditGridBinding_OnCommand(object sender, CommandEventArgs e)
        {
            OnDataChanged();
        }

        protected void btnDeleteGridBinding_OnCommand(object sender, CommandEventArgs e)
        {
            if (Bindings == null)
            {
                OnDataChanged();
                return;
            }

            var entity = (from n in GetGridBindings()
                          where n.Key == Convert.ToString(e.CommandArgument)
                          select n).FirstOrDefault();

            if (entity != null)
                Bindings.Remove(entity.Binding);

            OnDataChanged();
        }

        protected void btnSaveBinding_OnClick(object sender, EventArgs e)
        {
            Bindings = (Bindings ?? new List<BindingInfoModel>());

            var type = Convert.ToString(hdType.Value);
            if (type == "Grid")
            {
                var binding = new BindingInfoModel
                {
                    ID = Guid.NewGuid(),
                    Caption = tbxGridCaption.Text,
                    Source = cbxGridSource.TryGetStringValue(),
                };

                Bindings.Add(binding);
            }
            else
            {
                var xBinding = new BindingInfoModel
                {
                    ID = Guid.NewGuid(),
                    Caption = cbxChartCaption.TryGetStringValue(),
                    Source = cbxChartXValue.TryGetStringValue(),
                    Target = "XValue"
                };

                var yBinding = new BindingInfoModel
                {
                    ID = Guid.NewGuid(),
                    Caption = cbxChartCaption.TryGetStringValue(),
                    Source = cbxChartYValue.TryGetStringValue(),
                    Target = "YValue"
                };

                Bindings = (Bindings ?? new List<BindingInfoModel>());

                Bindings.Add(xBinding);
                Bindings.Add(yBinding);
            }

            cbxChartCaption.SelectedItem = null;
            cbxChartXValue.SelectedItem = null;
            cbxChartYValue.SelectedItem = null;

            OnDataChanged();
        }

        protected override void OnGetModel(object model, Type type)
        {
            var reportLogicModel = model as ReportLogicModel;
            if (reportLogicModel == null)
                return;

            if (reportLogicModel.Type != "Grid")
                reportLogicModel.Type = cbxType.TryGetStringValue();

            var logicID = cbxLogic.TryGetGuidValue();
            if (logicID != null)
            {
                var logicEntity = DataContext.LP_Logics.Single(n => n.ID == logicID);

                var converter = new LogicEntityModelConverter(DataContext);
                var logicModel = converter.Convert(logicEntity);

                reportLogicModel.Logic = logicModel;
            }

            var bindingsModel = reportLogicModel.Bindings;
            if (bindingsModel == null)
            {
                bindingsModel = new BindingInfosModel();
                reportLogicModel.Bindings = bindingsModel;
            }

            bindingsModel.List = Bindings;
        }

        protected override void OnSetModel(object model, Type type)
        {
            var reportLogicModel = model as ReportLogicModel;
            if (reportLogicModel == null)
                return;

            cbxLogic.SelectedItem = null;
            cbxTable.SelectedItem = null;

            if (reportLogicModel.Type != "Grid")
                cbxType.TrySetSelectedValue(reportLogicModel.Type);

            if (reportLogicModel.Bindings == null)
                return;

            if (reportLogicModel.Logic != null)
            {
                var logicModel = reportLogicModel.Logic;

                cbxTable.TrySetSelectedValue(logicModel.SourceID);
                FillComboBoxes();

                cbxLogic.TrySetSelectedValue(logicModel.ID);
                FillComboBoxes();
            }

            Bindings = reportLogicModel.Bindings.List;
        }

        protected void ApplyViewMode()
        {
            var type = Convert.ToString(hdType.Value);
            if (type == "Grid")
            {
                pnlChartBinding.Visible = false;
                pnlChartBindings.Visible = false;

                trChartType.Visible = false;

                pnlGridBinding.Visible = true;
                pnlGridBindings.Visible = true;
            }
            else
            {
                pnlChartBinding.Visible = true;
                pnlChartBindings.Visible = true;

                trChartType.Visible = true;

                pnlGridBinding.Visible = false;
                pnlGridBindings.Visible = false;
            }
        }

        protected void FillComboBoxes()
        {
            var tables = (from n in DataContext.LP_Tables
                          where n.DateDeleted == null
                          orderby n.Name
                          select n);

            cbxTable.DataSource = tables;
            cbxTable.DataBind();

            var selTableID = cbxTable.TryGetGuidValue();
            if (selTableID != null)
            {
                var logics = (from n in DataContext.LP_Logics
                              where n.DateDeleted == null && n.TableID == selTableID
                              orderby n.Name
                              select n);

                cbxLogic.DataSource = logics;
                cbxLogic.DataBind();
            }

            var selLogicID = cbxLogic.TryGetGuidValue();
            if (selLogicID != null)
            {
                var columns = GetLogicOutputColumns(selLogicID.Value);

                cbxChartXValue.DataSource = columns;
                cbxChartXValue.DataBind();

                cbxChartYValue.DataSource = columns;
                cbxChartYValue.DataBind();

                var groupersList = columns.ToList();
                groupersList.Insert(0, "");

                cbxChartCaption.DataSource = groupersList;
                cbxChartCaption.DataBind();

                cbxGridSource.DataSource = columns;
                cbxGridSource.DataBind();
            }
        }

        protected IList<String> GetLogicOutputColumns(Guid logicID)
        {
            var logic = DataContext.LP_Logics.Single(n => n.ID == logicID);

            var converter = new LogicEntityModelConverter(DataContext);
            var model = converter.Convert(logic);

            if (model.Type == "Query")
                return GetQueryOutputColumns(model.Query);

            var queryGen = new QueryGenerator(DataContext, model);
            var columns = queryGen.OutputColumns.ToList();
            return columns;
        }

        protected IList<String> GetQueryOutputColumns(String query)
        {
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader())
                    {
                        var list = new List<String>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var fieldName = reader.GetName(i);
                            list.Add(fieldName);
                        }

                        return list;
                    }
                }
            }
        }

        protected void FillBindingsGrids()
        {
            if (Bindings == null)
                return;

            var type = Convert.ToString(hdType.Value);

            if (type == "Grid")
                FillGridBindings();
            else
                FillChartBindings();
        }

        protected void FillGridBindings()
        {
            var bindings = GetGridBindings();

            gvGridBindings.DataSource = bindings;
            gvGridBindings.DataBind();
        }

        protected void FillChartBindings()
        {
            var bindings = GetChartBindings();

            gvChartBindings.DataSource = bindings;
            gvChartBindings.DataBind();
        }

        protected IEnumerable<GridBindingEntity> GetGridBindings()
        {
            var query = (from n in Bindings
                         select new GridBindingEntity
                         {
                             Key = GetDataKey(GetBindingID(n)),
                             Caption = n.Caption,
                             Target = n.Target,
                             Source = n.Source,
                             Binding = n,
                         });

            return query;
        }

        protected IEnumerable<ChartBindingEntity> GetChartBindings()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;

            var bindingsLp = Bindings.ToLookup(n => n.Caption, comparer);

            var query = (from n in bindingsLp
                         let lp = n.ToLookup(m => m.Target)

                         let xInfo = lp["XValue"].FirstOrDefault()
                         let yInfo = lp["YValue"].FirstOrDefault()

                         select new ChartBindingEntity
                         {
                             Key = GetDataKey(GetBindingID(xInfo), GetBindingID(yInfo)),
                             Caption = n.Key,
                             XValue = GetBindingSource(xInfo),
                             YValue = GetBindingSource(yInfo),
                             XBinding = xInfo,
                             YBinding = yInfo,
                         });

            return query;
        }

        protected String GetDataKey(params Object[] items)
        {
            var text = String.Join(",", items);
            return text.ComputeMd5();
        }

        protected Guid? GetBindingID(BindingInfoModel item)
        {
            if (item != null)
                return item.ID;

            return null;
        }

        protected String GetBindingSource(BindingInfoModel item)
        {
            if (item != null)
                return item.Source;

            return null;
        }

        protected String GetConnectionString()
        {
            var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];
            return connString.ConnectionString;
        }
    }
}