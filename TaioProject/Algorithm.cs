namespace TaioProject;

public static class Algorithm
{
    public static int Calculate(List<HashSet<int>> first, List<HashSet<int>> second)
    {
        var diff = first.Count - second.Count;
        if (diff > 0)
            second.AddRange(Enumerable.Repeat(new HashSet<int>(), diff));
        else if (diff < 0)
            first.AddRange(Enumerable.Repeat(new HashSet<int>(), -diff));

        var tpl = Helper.GenerateMatches(first.Count, first.Count);

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
        s1.IntersectWith(s2);
        tmpS1.UnionWith(s2);
        var t = tmpS1.Count - s1.Count;

        return t;
    }
}

public static class Helper
{
    /**
     * @brief generateArray
     * @param size
     * @return A vector [0, 1, ..., size - 1]
     */
    private static List<int> GenerateArray(int size)
    {
        var arr = new List<int>();
        for (var i = 0; i < size; ++i) arr.Add(i);
        return arr;
    }

    /**
     * @brief generateMatches
     * @param n, cardinality of the vector X, where X = [0,1, ..., n - 1].
     * @param m, cardinality of the vector Y, where Y = [0,1, ..., m - 1].
     * @return All possible injective and non-surjective mappings 
     * from the smaller vector to the larger vector.
     */
    public static List<List<(int, int)>> GenerateMatches(int n, int m)
    {
        // Deal with n > m. Swap back when generating pairs.
        var swapped = false;
        if (n > m)
        {
            swapped = true;
            (m, n) = (n, m);
        }

        // Now n is smaller or equal to m
        var A = GenerateArray(n);
        var B = GenerateArray(m);
        var matches = new List<List<(int, int)>>();
        // Generate all the permutations of m
        do
        {
            var match = new List<(int, int)>();
            for (var i = 0; i < n; ++i)
            {
                (int, int) p;
                if (swapped)
                    // Swap back to the original order.
                    p = (A[i], B[i]);
                else
                    p = (B[i], A[i]);
                match.Add(p);
            }

            matches.Add(match);
            // Generate next permutation.
        } while (Solution.NextPermutation(B));

        return matches;
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

    // swap function
    private static void Swap(List<int> num, int i, int j)
    {
        var temp = 0;
        temp = num[i];
        num[i] = num[j];
        num[j] = temp;
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