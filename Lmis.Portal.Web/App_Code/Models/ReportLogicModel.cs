using System;

namespace Lmis.Portal.Web.Models
{
    [Serializable]
    public class ReportLogicModel
    {
        public Guid? ID { get; set; }

        public String Type { get; set; }

        public LogicModel Logic { get; set; }

        public BindingInfosModel Bindings { get; set; }
    }
}