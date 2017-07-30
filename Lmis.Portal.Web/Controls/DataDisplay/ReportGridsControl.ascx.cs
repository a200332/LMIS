using System;
using System.Data;
using System.Linq;
using AjaxControlToolkit;
using CITI.EVO.Tools.Web.UI.Controls;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class ReportGridsControl : System.Web.UI.UserControl
    {
        private Object _dataSet;

        public Object DataSource { get; set; }

        public override void DataBind()
        {
            if (_dataSet == DataSource)
                return;

            tbGrids.Tabs.Clear();

            var dataSet = (DataSet)DataSource;

            foreach (DataTable dataTable in dataSet.Tables)
            {
                var tabPanel = new TabPanel();
                tabPanel.HeaderText = dataTable.TableName;

                var dataGrid = new ASPxGridView();

                var keyFields = dataTable.Columns.Cast<DataColumn>().Select(n => n.ColumnName);
                var keyColumns = String.Join(";", keyFields);

                dataGrid.AutoGenerateColumns = (dataGrid.Columns.Count == 0);
                dataGrid.KeyFieldName = keyColumns;
                dataGrid.DataSource = dataTable;
                dataGrid.DataBind();

                tabPanel.Controls.Add(dataGrid);

                tbGrids.Tabs.Add(tabPanel);
                tbGrids.ActiveTab = (tbGrids.ActiveTab ?? tabPanel);
            }

            _dataSet = DataSource;

            base.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}