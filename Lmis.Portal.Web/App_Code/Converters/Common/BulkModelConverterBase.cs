using System.Collections.Generic;
using System.Linq;
using Lmis.Portal.DAL.DAL;

namespace Lmis.Portal.Web.Converters.Common
{
	public abstract class BulkModelConverterBase<TSource, TTarget>
	{
		public BulkModelConverterBase(PortalDataContext dbContext)
		{
			DbContext = dbContext;
		}

		public PortalDataContext DbContext { get; private set; }

		public abstract IEnumerable<TTarget> Convert(IQueryable<TSource> source);
	}
}