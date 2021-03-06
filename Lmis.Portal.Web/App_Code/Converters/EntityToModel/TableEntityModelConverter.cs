﻿using System.Collections.Generic;
using System.Linq;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.EntityToModel
{
	public class TableEntityModelConverter : SingleModelConverterBase<LP_Table, TableModel>
	{
		public TableEntityModelConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override TableModel Convert(LP_Table source)
		{
			var target = new TableModel();
			FillObject(target, source);

			return target;
		}

		public override void FillObject(TableModel target, LP_Table source)
		{
			target.ID = source.ID;
			target.Name = source.Name.Trim();
            target.Status = source.Status;
            target.Columns = new List<ColumnModel>();

		    var columns = source.Columns.Where(n => n.DateDeleted == null);
            FillColumns(target.Columns, columns);
		}

		public void FillColumns(List<ColumnModel> models, IEnumerable<LP_Column> entities)
		{
			foreach (var entity in entities.OrderBy(n => n.DateCreated))
			{
				var model = new ColumnModel();
				FillColumn(model, entity);

				models.Add(model);
			}
		}
		public void FillColumn(ColumnModel model, LP_Column entity)
		{
			model.ID = entity.ID;
			model.Name = entity.Name.Trim();
            model.Type = entity.Type;
			model.TableID = entity.TableID;
		}
	}
}