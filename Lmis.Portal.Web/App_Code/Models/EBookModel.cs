using System;

namespace Lmis.Portal.Web.Models
{
	[Serializable]
	public class EBookModel
	{
		public Guid? ID { get; set; }

		public String Title { get; set; }

		public String Url { get; set; }

		public String Description { get; set; }

	    public int? OrderIndex { get; set; }

	    public String Language { get; set; }
    }
}