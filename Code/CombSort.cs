using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The comb sort is mainly an improvement over the bubble sort.<br/>
    /// The bubble sort always compares adjacent values.
    /// So all inversions are removed one by one.<br/>
    /// The comb sort improves on the bubble sort by using a gap of the size of more than 1.<br/>
    /// The gap starts with a large value and shrinks by a factor of 1.3 in every iteration until it reaches the value 1.<br/>
    /// Thus the comb sort removes more than one inversion count with one swap and performs better than Bubble Sort.
    /// The shrink factor has been empirically found to be 1.3.<br/>
    /// Although it works better than Bubble Sort on average, worst-case remains O(n^2).
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class CombSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        public CombSort()
        {
            Name = SortingAlgorithm.CombSort;
            Time = new Stopwatch();
            IsStabil = false;
            BestCase = "Ω(n^2/2^p)";
            AverageCase = "θ(nlog(n))";
            WorstCase = "O(n^2)";
            WorstCaseSpaceComplexity = "O(1)";
        }

        public override T[] Sort(T[] input, SortingType sortingType = SortingType.Ascending)
        {
            T[] output = new T[input.Length];
            Array.Copy(input, output, input.Length);
            Comparisons = default;
            Swaps = default;
            Time = new Stopwatch();

            Time.Start();
            double gap = input.Length;
            bool swaps = true;
            while (gap > 1 || swaps)
            {
                gap /= 1.247330950103979;
                if (gap < 1)
                {
                    gap = 1;
                }
                int i = 0;
                swaps = false;
                while (i + gap < input.Length)
                {
                    int igap = i + (int)gap;
                    if (Compare(output[i], output[igap], sortingType) > 0)
                    {
                        Swap(output, i, igap);
                        swaps = true;
                    }
                    i++;
                }
            }
            Time.Stop();
            return output;
        }
    }
}