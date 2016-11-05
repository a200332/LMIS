using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
	public class TableModelEntityConverter : SingleModelConverterBase<TableModel, LP_Table>
	{
		public TableModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_Table Convert(TableModel source)
		{
			var entity = new LP_Table
			{
				ID = Guid.NewGuid(),
				DateCreated = DateTime.Now
			};

			FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_Table target, TableModel source)
		{
			target.Name = source.Name;
            target.Status = source.Status;
        }
	}
}