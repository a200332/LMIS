using System;
using System.Data.Linq;

namespace Lmis.Portal.Web.Models
{
    [Serializable]
    public class ContentModel
    {
        public Guid? ID { get; set; }
        public String Type { get; set; }
        public String Title { get; set; }
        public byte[] Image { get; set; }
        public String FullText { get; set; }
        public String Language { get; set; }
        public String Description { get; set; }
        public byte[] Attachment { get; set; }
        public String AttachmentName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateChanged { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}