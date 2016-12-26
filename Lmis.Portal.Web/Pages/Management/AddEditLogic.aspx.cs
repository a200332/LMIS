using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.BLL;
using Lmis.Portal.Web.Converters.EntityToModel;
using Lmis.Portal.Web.Converters.ModelToEntity;
using Lmis.Portal.Web.Utils;
using CITI.EVO.Tools.Extensions;

namespace Lmis.Portal.Web.Pages.Management
{
    public partial class AddEditLogic : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserUtil.GotoLoginIfNoSuperadmin();

            if (!IsPostBack)
            {
                var logicID = DataConverter.ToNullableGuid(Request["LogicID"]);
                if (logicID == null)
                    return;

                var entity = DataContext.LP_Logics.FirstOrDefault(n => n.ID == logicID);
                if (entity == null)
                    return;

                var converter = new LogicEntityModelConverter(DataContext);
                var model = converter.Convert(entity);

                logicControl.Model = model;
            }
        }

        protected void btnCancelLogic_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Management/LogicsList.aspx");
        }

        protected void btnSaveLogic_OnClick(object sender, EventArgs e)
        {
            var logicID = DataConverter.ToNullableGuid(Request["LogicID"]);

            var entity = DataContext.LP_Logics.FirstOrDefault(n => n.ID == logicID);

            var converter = new LogicModelEntityConverter(DataContext);

            var model = logicControl.Model;
            if (entity == null)
            {
                entity = converter.Convert(model);
                DataContext.LP_Logics.InsertOnSubmit(entity);
            }
            else
            {
                converter.FillObject(entity, model);
            }

            DataContext.SubmitChanges();

            Response.Redirect("~/Pages/Management/LogicsList.aspx");
        }

        protected void btnPreview_OnClick(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = String.Empty;

                var model = logicControl.Model;

                var queryGen = new QueryGenerator(DataContext, model);
                var sqlQuery = queryGen.SelectQuery();

                var connString = ConfigurationManager.ConnectionStrings["RepositoryConnectionString"];

                var sqlDs = new SqlDataSource
                {
                    ConnectionString = connString.ConnectionString,
                    SelectCommand = sqlQuery,
                    CacheKeyDependency = sqlQuery.ComputeMd5(),
                    CacheExpirationPolicy = DataSourceCacheExpiry.Sliding,
                    CacheDuration = 900,
                    EnableCaching = true,
                };

                gvData.DataSource = sqlDs;
                gvData.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
            }

            mpeAddEdit.Show();
        }
    }
}