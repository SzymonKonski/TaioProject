using TaioProject;

namespace TestProject.Tests;

public class Tests
{
    [Fact]
    public void IsValidMetric()
    {
        for (var i = 0; i < 100; i++)
        {
            var family1 = Helper.GenerateFamilySet(3 * i);
            var family2 = Helper.GenerateFamilySet(3 * i + 1);
            var family3 = Helper.GenerateFamilySet(6 * i + 2);

            var dist1 = Algorithm.Calculate(family1, family2);
            var dist2 = Algorithm.Calculate(family2, family3);
            var dist3 = Algorithm.Calculate(family1, family3);

            Assert.True(dist1 + dist2 >= dist3);
        }
    }

    [Fact]
    public void IsGoodApproximation()
    {
        for (var i = 0; i < 100; i++)
        {
            var family1 = Helper.GenerateFamilySet(3 * i);
            var family2 = Helper.GenerateFamilySet(3 * i + 1);

            var dist = Algorithm.Calculate(family1, family2);
            var distApproximation = ApproximationAlgorithm.Calculate(family1, family2);
            var relative = dist != 0 ? Math.Abs(distApproximation - dist) / (float) dist * 100 : 0;

            Assert.True(relative < 10);
        }
    }
}