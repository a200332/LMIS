using System;
using System.Linq;
using CITI.EVO.Tools.Extensions;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.Management
{
    public partial class LogicControl : BaseExtendedControl<LogicModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var model = Model;

            FillLists(model);
            ApplyViewMode(model);
        }

        private void FillLists(LogicModel model)
        {
            if (model.SourceType == "Table")
            {
                var query = (from n in DataContext.LP_Tables
                             where n.DateDeleted == null
                             orderby n.Name
                             select n);

                var list = query.ToList();

                cbxSource.DataSource = list;
                cbxSource.DataBind();
            }

            if (model.SourceType == "Logic")
            {
                var query = (from n in DataContext.LP_Logics
                             where n.DateDeleted == null
                             orderby n.Name
                             select n);

                var list = query.ToList();

                cbxSource.DataSource = list;
                cbxSource.DataBind();
            }

            cbxSource.TrySetSelectedValue(model.SourceID);
        }

        private void ApplyViewMode(LogicModel model)
        {
            if (model.Type == "Logic")
            {
                pnlLogic.Visible = true;
                pnlQuery.Visible = false;
            }
            else if (model.Type == "Query")
            {
                pnlLogic.Visible = false;
                pnlQuery.Visible = true;
            }
        }

        protected override void OnSetModel(object model, Type type)
        {
            var logicModel = (LogicModel)model;

            FillLists(logicModel);
            ApplyViewMode(logicModel);
        }
    }
}