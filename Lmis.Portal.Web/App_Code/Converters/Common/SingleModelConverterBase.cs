using Lmis.Portal.DAL.DAL;

namespace Lmis.Portal.Web.Converters.Common
{
	public abstract class SingleModelConverterBase<TSource, TTarget>
	{
		public SingleModelConverterBase(PortalDataContext dbContext)
		{
			DbContext = dbContext;
		}

		public PortalDataContext DbContext { get; private set; }

		public abstract TTarget Convert(TSource source);

		public abstract void FillObject(TTarget target, TSource source);
	}
}