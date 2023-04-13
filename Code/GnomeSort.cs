using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The gnome sort is a variation of the insertion sort sorting algorithm that does not use nested loops.<br/>
    /// It was first called stupid sort (not to be confused with bogosort), and then later named gnome sort.<br/>
    /// The gnome sort performs at least as many comparisons as insertion sort and has the same asymptotic run time characteristics.<br/>
    /// The sort works by building a sorted list one element at a time, getting each item to the proper place in a series of swaps.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class GnomeSort<T> : Algorithm<T> where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the GnomeSort class.
        /// </summary>
        public GnomeSort()
        {
            Name = SortingAlgorithm.GnomeSort;
            Time = new Stopwatch();
            IsStabil = true;
            BestCase = "Ω(n)";
            AverageCase = "θ(n^2)";
            WorstCase = "O(n^2)";
            WorstCaseSpaceComplexity = "O(1)";
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override T[] Sort(T[] input, SortingType sortingType = SortingType.Ascending)
        {
            T[] output = new T[input.Length];
            Array.Copy(input, output, input.Length);
            Comparisons = default;
            Swaps = default;
            Time = new Stopwatch();

            Time.Start();
            int i = 1;
            int j = 2;
            while (i < output.Length)
            {
                if (Compare(output[i - 1], output[i], sortingType) < 0)
                {
                    i = j;
                    j += 1;
                }
                else
                {
                    Swap(output, i - 1, i);
                    i -= 1;
                    if (i == 0)
                    {
                        i = j;
                        j += 1;
                    }
                }
            }
            Time.Stop();
            return output;
        }
    }
}