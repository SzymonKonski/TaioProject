using System;
using System.Collections.Generic;
using System.Linq;

namespace TaioProject
{
    public static class ApproximationAlgorithm
    {
        public static int Calculate(List<HashSet<int>> firstFamily, List<HashSet<int>> secondFamily)
        {
            var first = firstFamily.Select(x => new HashSet<int>(x)).ToList();
            var second = secondFamily.Select(x => new HashSet<int>(x)).ToList();

            var diff = first.Count - second.Count;
            if (diff > 0)
                second.AddRange(Enumerable.Repeat(new HashSet<int>(), diff));
            else if (diff < 0)
                first.AddRange(Enumerable.Repeat(new HashSet<int>(), -diff));

            first = first.OrderBy(x => x.Count).ToList();
            second = second.OrderBy(x => x.Count).ToList();

            var set = new HashSet<int>();
            var sum = Math.Abs(diff);
            var tmp = 0;

            for (var i = 0; i < first.Count; i++)
            {
                var minDist = int.MaxValue;

                for (var j = 0; j < second.Count; j++)
                {
                    if (set.Contains(j))
                        continue;

                    var dist = Algorithm.Distance(first[i], second[j]);

                    if (dist < minDist)
                    {
                        minDist = dist;
                        tmp = j;
                    }
                }

                set.Add(tmp);
                sum += minDist;
            }

            return sum;
        }
    }
}