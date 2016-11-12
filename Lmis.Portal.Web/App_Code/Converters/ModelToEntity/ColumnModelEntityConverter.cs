using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
	public class ColumnModelEntityConverter : SingleModelConverterBase<ColumnModel, LP_Column>
	{
		public ColumnModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_Column Convert(ColumnModel source)
		{
		    var entity = new LP_Column
		    {
		        ID = Guid.NewGuid(),
		        DateCreated = DateTime.Now
		    };

		    FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_Column target, ColumnModel source)
		{
			//target.ID = source.ID.Value;
			target.Name = source.Name;
			target.Type = source.Type;
			target.TableID = source.TableID;
		}
	}
}