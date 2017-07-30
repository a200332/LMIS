using System;

namespace Lmis.Portal.Web.Models
{
    [Serializable]
    public class ProjectModel
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

        public String Number { get; set; }
    }
}