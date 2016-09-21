using System;

namespace Lmis.Portal.Web.Models.Common
{
    [Serializable]
    public class NamedExpressionModel
    {
        public String Name { get; set; }
        public String Expression { get; set; }
        public String OutputType { get; set; }
    }
}