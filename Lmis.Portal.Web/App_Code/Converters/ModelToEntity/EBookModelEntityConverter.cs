using System;
using Lmis.Portal.DAL.DAL;
using Lmis.Portal.Web.Converters.Common;
using Lmis.Portal.Web.Models;

namespace Lmis.Portal.Web.Converters.ModelToEntity
{
	public class EBookModelEntityConverter : SingleModelConverterBase<EBookModel, LP_EBook>
	{
		public EBookModelEntityConverter(PortalDataContext dbContext) : base(dbContext)
		{
		}

		public override LP_EBook Convert(EBookModel source)
		{
			var entity = new LP_EBook();
			entity.ID = Guid.NewGuid();
			entity.DateCreated = DateTime.Now;

			FillObject(entity, source);

			return entity;
		}

		public override void FillObject(LP_EBook target, EBookModel source)
		{
			//target.ID = source.ID.Value;
			target.Url = source.Url;
			target.Title = source.Title;
			target.Description = source.Description;
		    target.Language = source.Language;
		    target.OrderIndex = source.OrderIndex;
        }
    }
}