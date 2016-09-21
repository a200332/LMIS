using System;

namespace CITI.EVO.Tools.Web.UI.Model.Entities
{
	public class PropertyPathEntity : IComparable<PropertyPathEntity>
	{
		public PropertyPathEntity(String propertyName, String propertyParams)
		{
			PropertyName = propertyName;
			PropertyParams = propertyParams;
		}

		public String PropertyName { get; private set; }

		public String PropertyParams { get; private set; }

		public int CompareTo(PropertyPathEntity other)
		{
			var order = StringComparer.CurrentCulture.Compare(PropertyName, other.PropertyName);
			if (order == 0)
				order = StringComparer.CurrentCulture.Compare(PropertyParams, other.PropertyParams);

			return order;
		}

		public override bool Equals(object obj)
		{
			return CompareTo((PropertyPathEntity)obj) == 0;
		}

		public override int GetHashCode()
		{
			var propertyName = (PropertyName ?? String.Empty);
			var propertyParams = (PropertyParams ?? String.Empty);

			return StringComparer.CurrentCulture.GetHashCode(propertyName) ^
				   StringComparer.CurrentCulture.GetHashCode(propertyParams);
		}
	}
}
