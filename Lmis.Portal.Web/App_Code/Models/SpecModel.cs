using System;

namespace Lmis.Portal.Web.Models
{
    [Serializable]
    public class SpecModel
    {
        public Guid? ID { get; set; }

        public Guid? ParentID { get; set; }

        public String Title { get; set; }

        public String FullText { get; set; }

        public String Language { get; set; }

        public int? OrderIndex { get; set; }

        public bool? IsCategory { get; set; }
    }
}