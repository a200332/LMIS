using System;
using System.Web.UI;

namespace CITI.EVO.Tools.Web.UI.Model.Entities
{
	public class PropertyControlEntity : PropertyClassEntity, IComparable<PropertyControlEntity>
	{
		public PropertyControlEntity(PropertyClassEntity entity, Control control) : this(entity.ClassName, entity.PropertyName, entity.PropertyParams, control)
		{
		}
		public PropertyControlEntity(String className, String proppertyName, String propertyParams, Control control) : base(className, proppertyName, propertyParams)
		{
			Control = control;
		}

		public Control Control { get; private set; }

		public int CompareTo(PropertyControlEntity other)
		{
			var order = base.CompareTo(other);
			if (order == 0)
				order = StringComparer.CurrentCulture.Compare(Control.ClientID, other.Control.ClientID);

			return order;
		}

		public override bool Equals(object obj)
		{
			return CompareTo((PropertyControlEntity)obj) == 0;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Control.GetHashCode();
		}
	}

}
