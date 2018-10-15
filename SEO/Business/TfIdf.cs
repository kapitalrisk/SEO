using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEO.Business
{
    public static class TfIdf
    {
        public static IEnumerable<string> SortNGrams(IEnumerable<string> ngrams)
        {
            var occurencyDict = new ConcurrentDictionary<string, decimal>();
            Parallel.ForEach(ngrams.Distinct(), i => occurencyDict.TryAdd(i, (decimal)ngrams.Count(x => x == i) / (decimal)ngrams.Count()));
            return occurencyDict.OrderByDescending(x => x.Value).ThenBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value).Keys.ToList().GetRange(0, occurencyDict.Count < 20 ? occurencyDict.Count : 20);
        }
    }
}
