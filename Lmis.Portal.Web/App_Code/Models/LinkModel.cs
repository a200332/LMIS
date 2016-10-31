using System;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class LinkModel
	{
		public Guid? ID { get; set; }

		public Guid? ParentID { get; set; }

		public String Title { get; set; }

		public byte[] Image { get; set; }

		public String Url { get; set; }

        public int? OrderIndex { get; set; }

        public String Description { get; set; }
	}
}