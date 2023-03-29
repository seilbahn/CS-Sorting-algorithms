using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The selection sort is an in-place comparison sorting algorithm.<br/>
    /// It has an O(n^2) time complexity, which makes it inefficient on large lists,
    /// and generally performs worse than the similar insertion sort.
    /// It is noted for its simplicity and has performance advantages over more complicated algorithms in certain situations,
    /// particularly where auxiliary memory is limited.<br/>
    /// The algorithm divides the input list into two parts:
    /// a sorted sublist of items which is built up from left to right at the front (left) of the list
    /// and a sublist of the remaining unsorted items that occupy the rest of the list.<br/>
    /// Initially, the sorted sublist is empty and the unsorted sublist is the entire input list.<br/>
    /// The algorithm proceeds by finding the smallest (or largest, depending on sorting order) element in the unsorted sublist,
    /// exchanging (swapping) it with the leftmost unsorted element (putting it in sorted order),
    /// and moving the sublist boundaries one element to the right.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class SelectionSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        public SelectionSort()
        {
            Name = SortingAlgorithm.SelectionSort;
            Time = new Stopwatch();
            IsStabil = false;
            BestCase = "Ω(n^2)";
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
            for (int i = 0; i < output.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < output.Length; j++)
                {
                    if (Compare(output[j], output[min], sortingType) < 0)
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    Swap(output, min, i);
                }
            }
            Time.Stop();
            return output;
        }
    }
}