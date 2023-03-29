using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The shellsort, also known as Shell sort or Shell's method, is an in-place comparison sort.<br/>
    /// It can be seen as either a generalization of sorting by exchange (bubble sort) or sorting by insertion (insertion sort).<br/>
    /// The method starts by sorting pairs of elements far apart from each other,
    /// then progressively reducing the gap between elements to be compared.<br/>
    /// By starting with far apart elements, it can move some out-of-place elements into position faster than a simple nearest neighbor exchange.<br/>
    /// The running time of Shellsort is heavily dependent on the gap sequence it uses.
    /// For many practical variants, determining their time complexity remains an open problem. 
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class ShellSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        public ShellSort()
        {
            Name = SortingAlgorithm.ShellSort;
            Time = new Stopwatch();
            IsStabil = false;
            BestCase = "Ω(nlog(n))";
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
            for (int interval = output.Length / 2; interval > 0; interval /= 2)
            {
                for (int i = interval; i < output.Length; i++)
                {
                    T currentKey = output[i];
                    int k = i;
                    while (k >= interval && Compare(output[k - interval], currentKey, sortingType) > 0)
                    {
                        output[k] = output[k - interval];
                        Swaps++;
                        k -= interval;
                    }
                    output[k] = currentKey;
                    Swaps++;
                }
            }
            Time.Stop();
            return output;
        }
    }
}