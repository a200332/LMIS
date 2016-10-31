using System;

namespace Lmis.Portal.Web.Models
{
    public class ProjectModel
    {
        public Guid? ID { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Language { get; set; }

        public int? OrderIndex { get; set; }

        public byte[] FileData { get; set; }

        public String FileName { get; set; }
    }
}