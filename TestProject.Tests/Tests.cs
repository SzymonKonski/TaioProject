using TaioProject;

namespace TestProject.Tests;

public class Tests
{
    [Fact]
    public void IsValidMetric()
    {
        for (var i = 0; i < 100; i++)
        {
            var family1 = GenerateFamilySet(3 * i);
            var family2 = GenerateFamilySet(3 * i + 1);
            var family3 = GenerateFamilySet(6 * i + 2);

            var clone1 = GenerateFamilySet(3 * i);
            var clone2 = GenerateFamilySet(3 * i + 1);
            var clone3 = GenerateFamilySet(6 * i + 2);

            var dist1 = Algorithm.Calculate(family1, family2);
            var dist2 = Algorithm.Calculate(family2, family3);
            var dist3 = Algorithm.Calculate(family1, family3);

            if (dist1 + dist2 < dist3)
            {
                var t = 12;
            }

            Assert.True(dist1 + dist2 >= dist3);
        }
    }

    private List<HashSet<int>> GenerateFamilySet(int seed)
    {
        var familySet = new List<HashSet<int>>();
        var lowerBound = 0;
        var upperBound = 6;
        var random = new Random(seed);
        var numberOfSets1 = random.Next(lowerBound, upperBound);

        for (var i = 0; i < numberOfSets1; i++)
        {
            var numberOfElements = random.Next(0, 5);
            familySet.Add(new HashSet<int>(numberOfElements));

            for (var j = 0; j < numberOfElements; j++) familySet[i].Add(random.Next(0, 1000));
        }

        return familySet;
    }
}