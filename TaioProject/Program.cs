using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaioProject
{
    public class Program
    {
        private static void Main()
        {
            var rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            var lines = File.ReadAllLines($@"{rootDir}Data.txt");

            var numberOfSetsInFirstSetFamily = int.Parse(lines[0]);
            var numberOfSetsInSecondSetFamily = int.Parse(lines[numberOfSetsInFirstSetFamily + 1]);

            var first = new List<HashSet<int>>();
            var second = new List<HashSet<int>>();

            for (var i = 1; i <= numberOfSetsInFirstSetFamily; i++)
            {
                var elementsInSet = Array.ConvertAll(lines[i].Split(" "), int.Parse);
                first.Add(new HashSet<int>(elementsInSet.Skip(1)));
            }

            for (var i = numberOfSetsInFirstSetFamily + 2; i < lines.Length; i++)
            {
                var elementsInSet = Array.ConvertAll(lines[i].Split(" "), int.Parse);
                second.Add(new HashSet<int>(elementsInSet.Skip(1)));
            }

            float avg = 0;

            for (var i = 0; i < 100; i++)
            {
                var family1 = Helper.GenerateFamilySet(3 * i);
                var family2 = Helper.GenerateFamilySet(3 * i + 1);

                var dist = Algorithm.Calculate(family1, family2);
                var distApproximation = ApproximationAlgorithm.Calculate(family1, family2);

                var relative = dist != 0 ? Math.Abs(distApproximation - dist) / (float) dist * 100 : 0;
                avg += relative;

                Console.WriteLine($"Precise: {dist} | Aproximation: {distApproximation} | Relative: {relative}");
            }

            Console.WriteLine($"Average error: {avg / 100}");
        }
    }
}