﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Hosting;
using CITI.EVO.Rpc.Formatters;

namespace CITI.EVO.Rpc.Helpers
{
    public static class ConcurrencyHelper
    {
        public static int GetDefaultConcurrency()
        {
            var concurrencyLevel = Environment.ProcessorCount * 4;

            if (HostingEnvironment.IsHosted)
            {
                concurrencyLevel = Math.Max(HostingEnvironment.MaxConcurrentThreadsPerCPU, concurrencyLevel);
            }

            return concurrencyLevel;
        }

        public static int GetDefaultCapacity()
        {
            var capacity = GetDefaultConcurrency() * 31;

            if (HostingEnvironment.IsHosted)
            {
                capacity = Math.Max(HostingEnvironment.MaxConcurrentRequestsPerCPU, capacity);
            }

            return capacity;
        }

        public static ConcurrentFormatter CreateFormatter(Func<IFormatter> initializer)
        {
            var concurrency = GetDefaultConcurrency();
            return new ConcurrentFormatter(concurrency, initializer);
        }

        public static ConcurrentDictionary<TKey, TValue> CreateDictionary<TKey, TValue>()
        {
            var capacity = GetDefaultCapacity();
            var concurrency = GetDefaultConcurrency();

            return new ConcurrentDictionary<TKey, TValue>(concurrency, capacity);
        }
        public static ConcurrentDictionary<TKey, TValue> CreateDictionary<TKey, TValue>(IEqualityComparer<TKey> comparer)
        {
            var capacity = GetDefaultCapacity();
            var concurrency = GetDefaultConcurrency();

            return new ConcurrentDictionary<TKey, TValue>(concurrency, capacity, comparer);
        }
    }

}
