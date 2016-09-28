using System;
using System.Collections.Generic;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Entites
{
	/// <summary>
	/// Summary description for BindingInfoEntity
	/// </summary>
	public class BindingInfoEntity
	{
		public String SqlQuery { get; set; }

		public String Type { get; set; }

		public IList<BindingInfoModel> Bindings { get; set; }
	}
}