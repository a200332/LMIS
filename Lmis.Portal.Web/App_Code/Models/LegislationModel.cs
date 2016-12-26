using System;

namespace Lmis.Portal.Web.Models
{
    public class LegislationModel
    {
        public Guid? ID { get; set; }

        public Guid? ParentID { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Language { get; set; }

        public int? OrderIndex { get; set; }

        public byte[] FileData { get; set; }

        public String FileName { get; set; }

        public byte[] Image { get; set; }
    }
}