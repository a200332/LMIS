using System;
using CITI.EVO.Tools.Utils;
using CITI.EVO.Tools.Web.Bases;
using Lmis.Portal.DAL.DAL;

namespace Lmis.Portal.Web.Bases
{
    public class BasePage : PageBase
    {
        public BasePage()
        {
        }

        private PortalDataContext _dataContext;
        public PortalDataContext DataContext
        {
            get
            {
                _dataContext = (_dataContext ?? new PortalDataContext());
                return _dataContext;
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            DataContext.Dispose();
            base.OnUnload(e);
        }

        protected Guid? GetRequestID()
        {
            return DataConverter.ToNullableGuid(Request["ID"]);
        }

        protected String GetViewStateValue(String key)
        {
            return Convert.ToString(ViewState[key]);
        }

        protected void SetViewStateValue(String key, object value)
        {
            ViewState[key] = value;
        }

    }
}
