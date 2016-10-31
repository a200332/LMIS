using System;

namespace Lmis.Portal.Web.Entites
{
    /// <summary>
    /// Summary description for SearchEntry
    /// </summary>
    public class SearchEntry
    {
        public Guid? ID { get; set; }

        public String Url { get; set; }

        public String Type { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }
    }
}