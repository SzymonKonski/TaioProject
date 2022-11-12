namespace TaioProject;

public static class Algorithm
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

        var tpl = Helper.GenerateMatches(first.Count);

        var distance = int.MaxValue;

        foreach (var list in tpl)
        {
            var sum = Math.Abs(diff);

            try
            {
                foreach (var p in list)
                    sum += Distance(first[p.Item1], second[p.Item2]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (sum < distance)
                distance = sum;
        }

        return distance;
    }

    public static int Distance(HashSet<int> s1, HashSet<int> s2)
    {
        var tmpS1 = new HashSet<int>(s1);
        var tmpS12 = new HashSet<int>(s1);
        tmpS12.IntersectWith(s2);
        tmpS1.UnionWith(s2);
        return tmpS1.Count - tmpS12.Count;
    }
}

public static class Solution
{
    // Start from its last element, traverse backward to find the first one with    
    // index i that satisfy num[i-1] < num[i]. So, elements from num[i] to num[n-1] 
    // is reversely sorted.
    // The last step is to make the remaining higher position part as small as 
    // possible, we just have to reversely sort the num[i,n-1]

    public static bool NextPermutation(List<int> nums)
    {
        var n = nums.Count;
        if (n < 2)
            return false;
        var index = n - 1;
        while (index > 0)
        {
            if (nums[index - 1] < nums[index])
                break;
            index--;
        }

        if (index == 0)
        {
            ReverseSort(nums, 0, n - 1);
            return false;
        }

        var val = nums[index - 1];
        var j = n - 1;
        while (j >= index)
        {
            if (nums[j] > val)
                break;
            j--;
        }

        Swap(nums, j, index - 1);
        ReverseSort(nums, index, n - 1);

        return true;
    }

    private static void Swap(List<int> num, int i, int j)
    {
        (num[i], num[j]) = (num[j], num[i]);
    }

    // using swap function to reverse
    private static void ReverseSort(List<int> num, int start, int end)
    {
        if (start > end)
            return;
        for (var i = start; i <= (end + start) / 2; i++)
            Swap(num, i, start + end - i);
    }
}