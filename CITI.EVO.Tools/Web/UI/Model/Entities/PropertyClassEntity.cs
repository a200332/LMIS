using System;

namespace CITI.EVO.Tools.Web.UI.Model.Entities
{
	public class PropertyClassEntity : PropertyPathEntity, IComparable<PropertyClassEntity>
	{
		public PropertyClassEntity(String className, String propertyName, String propertyParams) : base(propertyName, propertyParams)
		{
			ClassName = className;
		}

		public String ClassName { get; private set; }

		public int CompareTo(PropertyClassEntity other)
		{
			var order = base.CompareTo(other);
			if (order == 0)
				order = StringComparer.CurrentCulture.Compare(ClassName, other.ClassName);

			return order;
		}

		public override bool Equals(object obj)
		{
			return CompareTo((PropertyClassEntity)obj) == 0;
		}

		public override int GetHashCode()
		{
			var className = (ClassName ?? String.Empty);

			return base.GetHashCode() ^ StringComparer.CurrentCulture.GetHashCode(className);
		}
	}

}
