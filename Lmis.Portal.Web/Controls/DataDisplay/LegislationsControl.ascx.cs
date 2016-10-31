using System;
using CITI.EVO.Tools.Utils;
using Lmis.Portal.Web.Bases;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Controls.DataDisplay
{
    public partial class LegislationsControl : BaseExtendedControl<LegislationsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnSetModel(object model, Type type)
        {
            var legislationsModel = model as LegislationsModel;
            if (legislationsModel == null)
                return;

            rptItems.DataSource = legislationsModel.List;
            rptItems.DataBind();
        }

        protected Object GetFileUrl(Object eval)
        {
            var id = DataConverter.ToNullableGuid(eval);
            if (id == null)
                return "#";

            var url = String.Format("~/Handlers/GetFile.ashx?Type=Legislation&ID={0}", id);
            return url;
        }
    }
}