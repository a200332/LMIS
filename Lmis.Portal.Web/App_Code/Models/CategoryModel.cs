using System;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class CategoryModel
	{
		public Guid? ID { get; set; }

		public Guid? ParentID { get; set; }

		public String Number { get; set; }

		public String Name { get; set; }

		public byte[] Image { get; set; }

        public int? OrderIndex { get; set; }
    }
}