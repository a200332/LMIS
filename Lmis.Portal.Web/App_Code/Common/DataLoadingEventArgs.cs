using System;

namespace Lmis.Portal.Web.Common
{
	public class DataLoadingEventArgs : EventArgs
	{
		public DataLoadingEventArgs(Object destination, Guid? entityID)
		{
			Destination = destination;
			EntityID = entityID;
		}

		public Guid? EntityID { get; private set; }

		public Object Destination { get; private set; }
	}
}
