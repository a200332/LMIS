using System;

namespace CITI.EVO.Tools.Web.UI.Model.Interfaces
{
	public interface IComplexDataContainer : ISingleValueContainer
	{
		Object Data { get; set; }
	}
}
