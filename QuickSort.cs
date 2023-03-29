using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The quicksort is an efficient, general-purpose sorting algorithm.<br/>
    /// It was developed by British computer scientist Tony Hoare in 1959 and published in 1961.
    /// It is still a commonly used algorithm for sorting.<br/>
    /// Overall, it is slightly faster than merge sort and heapsort for randomized data, particularly on larger distributions.<br/>
    /// This is a divide-and-conquer algorithm.
    /// It works by selecting a 'pivot' element from the array
    /// and partitioning the other elements into two sub-arrays, according to whether they are less than or greater than the pivot.<br/>
    /// For this reason, it is sometimes called partition-exchange sort.<br/>
    /// The sub-arrays are then sorted recursively. This can be done in-place, requiring small additional amounts of memory to perform the sorting.
    /// Most implementations of quicksort are not stable, meaning that the relative order of equal sort items is not preserved.<br/>
    /// This implementation is also not stabil.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class QuickSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        public QuickSort()
        {
            Name = SortingAlgorithm.QuickSort;
            Time = new Stopwatch();
            IsStabil = false;
            BestCase = "Ω(nlog(n))";
            AverageCase = "θ(nlog(n))";
            WorstCase = "O(n^2)";
            WorstCaseSpaceComplexity = "O(n)";
        }

        public override T[] Sort(T[] input, SortingType sortingType = SortingType.Ascending)
        {
            T[] output = new T[input.Length];
            Array.Copy(input, output, input.Length);
            Comparisons = default;
            Swaps = default;
            Time = new Stopwatch();

            Time.Start();
            SortArray(output, 0, output.Length - 1, sortingType);
            Time.Stop();
            return output;
        }

        private T[] SortArray(T[] array, int leftIndex, int rightIndex, SortingType sortingType)
        {
            int i = leftIndex;
            int j = rightIndex;
            T pivot = array[leftIndex];

            while (i <= j)
            {
                while (Compare(array[i], pivot, sortingType) < 0)
                {
                    i++;
                }

                while (Compare(array[j], pivot, sortingType) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    Swap(array, i, j);
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                SortArray(array, leftIndex, j, sortingType);

            if (i < rightIndex)
                SortArray(array, i, rightIndex, sortingType);

            return array;
        }
    }
}