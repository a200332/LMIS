using System;

namespace Lmis.Portal.Web.Entites
{
	[Serializable]
	public class ParentChildEntity : IComparable, IComparable<ParentChildEntity>
	{
		public String Key { get; set; }

		public Guid? ID { get; set; }

		public String Name { get; set; }

		public Guid? ParentID { get; set; }

		public String Type { get; set; }

		public int CompareTo(object obj)
		{
			return CompareTo((ParentChildEntity)obj);
		}

		public int CompareTo(ParentChildEntity other)
		{
			if (other == null)
				return 1;

			var order = ID.GetValueOrDefault().CompareTo(other.ID.GetValueOrDefault());
			if (order != 0)
				return order;

			order = StringComparer.Ordinal.Compare(Name, other.Name);
			if (order != 0)
				return order;

			order = ParentID.GetValueOrDefault().CompareTo(other.ParentID.GetValueOrDefault());
			if (order != 0)
				return order;

			order = StringComparer.Ordinal.Compare(Type, other.Type);
			if (order != 0)
				return order;

			return 0;
		}

		public override int GetHashCode()
		{
			var data = String.Concat(Key, ID, ParentID, Name, Type);
			return data.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var order = CompareTo(obj);
			return (order == 0);
		}
	}
}