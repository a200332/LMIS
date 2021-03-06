﻿using System;
using System.Collections.Generic;
using System.Linq;
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

            var order = x.OrderIndex.GetValueOrDefault().CompareTo(y.OrderIndex.GetValueOrDefault());
            if (order == 0)
            {
                order = comparer.Compare(x.Number, y.Number);
                if (order == 0)
                    order = x.DateCreated.CompareTo(y.DateCreated);
            }

            return order;
        }
        public static IEnumerable<LP_Category> GetAllCategories(Guid? parentID, ILookup<Guid?, LP_Category> allEntitiesLp)
        {
            var entities = allEntitiesLp[parentID];
            foreach (var entity in entities)
            {
                yield return entity;

                var children = GetAllCategories(entity.ID, allEntitiesLp);
                foreach (var child in children)
                    yield return child;
            }
        }
    }
}