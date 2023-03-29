using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The insertion sort iterates, consuming one input element each repetition, and grows a sorted output list.<br/>
    /// At each iteration, insertion sort removes one element from the input data,
    /// finds the location it belongs within the sorted list, and inserts it there.<br/>
    /// It repeats until no input elements remain.<br/>
    /// Sorting is typically done in-place, by iterating up the array, growing the sorted list behind it.<br/>
    /// At each array-position, it checks the value there against the largest value in the sorted list
    /// (which happens to be next to it, in the previous array-position checked).<br/>
    /// If larger, it leaves the element in place and moves to the next.<br/>
    /// If smaller, it finds the correct position within the sorted list, shifts all the larger values up to make a space,
    /// and inserts into that correct position. 
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class InsertionSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        public InsertionSort()
        {
            Name = SortingAlgorithm.InsertionSort;
            Time = new Stopwatch();
            IsStabil = true;
            BestCase = "Ω(n)";
            AverageCase = "θ(n^2)";
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
            for (int i = 1; i < output.Length; i++)
            {
                T a = output[i];
                int b = i;
                while (b > 0 && Compare(output[b - 1], a, sortingType) > 0)
                {
                    Swap(output, b, b - 1);
                    b -= 1;
                }
                output[b] = a;
            }
            Time.Stop();
            return output;
        }
    }
}