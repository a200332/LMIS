using System;
using System.Collections.Generic;
using CITI.EVO.Tools.Comparers;
using Lmis.Portal.DAL.DAL;

namespace Lmis.Portal.Web.Utils
{
    public static class CategoryUtil
    {
        public static void Sort(List<LP_Category> list)
        {
            var logicalComparer = new StringLogicalComparer();
            var comparisonComparer = new ComparisonComparer<LP_Category>((x, y) => CategoryComparer(logicalComparer, x, y));

            list.Sort(comparisonComparer);
        }

        public static int CategoryComparer(IComparer<String> comparer, LP_Category x, LP_Category y)
        {
            var order = comparer.Compare(x.Number, y.Number);
            if (order == 0)
                order = x.DateCreated.CompareTo(y.DateCreated);

            return order;
        }
    }
}