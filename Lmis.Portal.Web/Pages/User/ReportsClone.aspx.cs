using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Entites;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Pages.User
{
    public partial class ReportsClone : BasePage
    {
        protected ISet<Guid> SelectedReports
        {
            get
            {
                var @set = Session["SelectedReports"] as ISet<Guid>;
                if (@set == null)
                {
                    @set = new HashSet<Guid>();
                    Session["SelectedReports"] = @set;
                }

                return @set;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillReportsTree();

            FillCategories();
            FillReports();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            SelectedReports.Clear();

            var checkedNodes = tvReports.CheckedNodes;
            foreach (TreeNode node in checkedNodes)
            {
                var value = DataConverter.ToNullableGuid(node.Value);
                if (value != null)
                    SelectedReports.Add(value.Value);
            }

            FillReports();

            mpeReports.Hide();
        }

        protected void btnReports_OnClick(object sender, EventArgs e)
        {
            mpeReports.Show();
        }

        protected void FillCategories()
        {
            var converter = new CategoryEntityModelConverter(DataContext);

            var entities = (from n in DataContext.LP_Categories
                            where n.DateDeleted == null
                            orderby n.OrderIndex, n.DateCreated
                            select n).ToList();

            var models = entities.Select(n => converter.Convert(n)).ToList();

            var categoriesModel = new CategoriesModel { List = models };
            categoriesControl.Model = categoriesModel;
        }

        protected void FillReportsTree()
        {
            var categories = (from n in DataContext.LP_Categories
                              where n.DateDeleted == null
                              orderby n.OrderIndex, n.Number, n.DateCreated
                              select new ParentChildEntity
                              {
                                  ID = n.ID,
                                  ParentID = n.ParentID,
                                  Type = "Category",
                                  Name = n.Name
                              }).ToList();

            var reports = (from n in DataContext.LP_Reports
                           where n.DateDeleted == null
                           select new ParentChildEntity
                           {
                               ID = n.ID,
                               ParentID = n.CategoryID,
                               Type = "Report",
                               Name = n.Name
                           }).ToList();

            var itemsSet = new HashSet<ParentChildEntity>();
            itemsSet.UnionWith(categories);
            itemsSet.UnionWith(reports);

            var itemsLp = itemsSet.ToLookup(n => n.ParentID);
            var parents = itemsSet.Where(n => n.ParentID == null);

            foreach (var entity in parents)
            {
                var node = new TreeNode
                {
                    Text = entity.Name,
                    Value = Convert.ToString(entity.ID),
                    ShowCheckBox = (entity.Type == "Report")
                };

                FillNode(node, entity.ID, itemsLp);

                tvReports.Nodes.Add(node);
            }
        }

        protected void FillNode(TreeNode parentNode, Guid? parentID, ILookup<Guid?, ParentChildEntity> lookup)
        {
            var children = lookup[parentID];

            foreach (var entity in children)
            {
                var node = new TreeNode
                {
                    Text = entity.Name,
                    Value = Convert.ToString(entity.ID),
                    ShowCheckBox = (entity.Type == "Report")
                };

                parentNode.ChildNodes.Add(node);

                FillNode(node, entity.ID, lookup);
            }
        }

        protected void FillReports()
        {
            var reports = (from n in DataContext.LP_Reports
                           where Enumerable.Contains(SelectedReports, n.ID)
                           select n).ToList();

            var converter = new ReportEntityUnitModelConverter(DataContext);

            var reportModels = reports.Select(n => converter.Convert(n));

            var reportUnits = new ReportUnitsModel
            {
                List = reportModels.ToList()
            };

            reportUnitsControl1.Model = reportUnits;
            reportUnitsControl2.Model = reportUnits;
        }
    }
}