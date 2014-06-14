using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfvbaibai.CommonUtils
{
    public static class CollectionUtil
    {
        public static T WeightGet<T> (this IList <T> list, Func <T, int> weighter)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return list.WeightGet(weighter, totalWeight => R.Next(0, totalWeight));
        }

        public static T WeightGet<T> (this IList <T> list, Func <T, int> weighter, Func<int, int> tester)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (list.Count == 0)
            {
                return default(T);
            }
            if (list.Count == 1)
            {
                return list[0];
            }
            int totalWeight = list.Sum(weighter);
            int test = tester(totalWeight);
            int w0 = weighter(list[0]);
            if (test < w0)
            {
                return list[0];
            }
            for (int n = 1, k = w0; n < list.Count; ++n)
            {
                int wN = weighter(list[n]);
                if (test >= k && test < k + wN)
                {
                    return list[n];
                }
                k += wN;
            }
            throw new ApplicationException("Should not reach here! Please verify the tester is valid.");
        }

        public static decimal CenterAvg(this IList <decimal> items)
        {
            if (items.Count == 0)
            {
                return 0m;
            }
            if (items.Count == 1)
            {
                return items[0];
            }
            var sum = 0m;
            int halfCount = items.Count / 2;
            if (items.Count % 2 == 0)
            {
                for (int i = 0; i < halfCount; ++i)
                {
                    sum += items[i] * (i + 1);
                }
                for (int i = halfCount; i < items.Count; ++i)
                {
                    sum += items[i] * (items.Count - i);
                }
                return sum / halfCount / (halfCount + 1);
            }
            else
            {
                for (int i = 0; i < halfCount; ++i)
                {
                    sum += items[i] * (i + 1);
                }
                sum += items[halfCount] * (halfCount + 1);
                for (int i = halfCount + 1; i < items.Count; ++i)
                {
                    sum += items[i] * (items.Count - i);
                }
                return sum / (halfCount + 1) / (halfCount + 1);
            }
        }
    }
}
