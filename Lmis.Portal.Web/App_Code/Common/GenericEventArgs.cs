namespace Lmis.Portal.Web.Common
{
	public class GenericEventArgs<TValue>
	{
		public GenericEventArgs(TValue value)
		{
			Value = value;
		}

		public TValue Value { get; private set; }
	}
}