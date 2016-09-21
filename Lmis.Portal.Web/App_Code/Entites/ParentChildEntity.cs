using System;

namespace Lmis.Portal.Web.Entites
{
	[Serializable]
	public class ParentChildEntity
	{
		public String Key { get; set; }

		public Guid? ID { get; set; }

		public String Name { get; set; }

		public Guid? ParentID { get; set; }
	}
}