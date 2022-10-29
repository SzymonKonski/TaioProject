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