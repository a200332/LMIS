using System;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class NewsModel
	{
		public Guid? ID { get; set; }

	    public String Title { get; set; }

	    public String Description { get; set; }

	    public String FullText { get; set; }

	    public byte[] Attachment { get; set; }

        public String AttachmentName { get; set; }

	    public byte[] Image { get; set; }

        public DateTime? NewsDate { get; set; }
    }
}