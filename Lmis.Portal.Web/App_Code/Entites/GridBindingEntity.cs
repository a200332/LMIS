using System;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Entites
{
	public class GridBindingEntity
	{
		public String Key { get; set; }

		public String Caption { get; set; }

		public String Source { get; set; }

		public String Target { get; set; }

		public BindingInfoModel Binding { get; set; }
	}
}