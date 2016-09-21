using System;
using System.Text.RegularExpressions;
using CITI.EVO.Tools.Utils;

namespace CITI.EVO.UserManagement.Web.Units
{
	[Serializable]
	public class TreeNodeUnit
	{
		private static readonly Regex _keyRx;

		static TreeNodeUnit()
		{
			_keyRx = new Regex(@"(?<ID>.*)/(?<ParentID>.*)/(?<Type>.*)", RegexOptions.Compiled);
		}

		public String Key
		{
			get { return String.Format("{0}/{1}/{2}", ID, ParentID, Type); }
		}

		public Guid ID { get; set; }

		public Guid? ParentID { get; set; }

		public String Name { get; set; }

		public String Type { get; set; }

		public static TreeNodeUnit Parse(String key)
		{
			var match = _keyRx.Match(key);

			var strId = match.Groups["ID"].Value;
			var strType = match.Groups["Type"].Value;
			var strParentId = match.Groups["ParentID"].Value;

			var item = new TreeNodeUnit
			{
				ID = Guid.Parse(strId),
				Type = strType,
				ParentID = DataConverter.ToNullableGuid(strParentId),
			};

			return item;
		}
	}
}