using System;
using System.Collections.Generic;
using System.Linq;

namespace TaioProject
{
    public static class Helper
    {
        public static List<List<(int, int)>> GenerateMatches(int n)
        {
            var A = Enumerable.Range(0, n).ToList();
            var B = Enumerable.Range(0, n).ToList();
            var matches = new List<List<(int, int)>>();
            // Generate all the permutations of m
            do
            {
                var match = new List<(int, int)>();
                for (var i = 0; i < n; ++i) match.Add((B[i], A[i]));

                matches.Add(match);
                // Generate next permutation.
            } while (Solution.NextPermutation(B));

            return matches;
        }

        public static List<HashSet<int>> GenerateFamilySet(int seed)
        {
            var familySet = new List<HashSet<int>>();
            var lowerBound = 2;
            var upperBound = 10;
            var random = new Random(seed);
            var numberOfSets1 = random.Next(lowerBound, upperBound);

            for (var i = 0; i < numberOfSets1; i++)
            {
                var numberOfElements = random.Next(0, 20);
                familySet.Add(new HashSet<int>(numberOfElements));

                for (var j = 0; j < numberOfElements; j++) familySet[i].Add(random.Next(0, 1000));
            }

            return familySet;
        }
    }
}