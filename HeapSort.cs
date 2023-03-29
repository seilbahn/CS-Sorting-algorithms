using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The heapsort can be thought of as an improved selection sort:
    /// like selection sort, heapsort divides its input into a sorted and an unsorted region,
    /// and it iteratively shrinks the unsorted region by extracting the largest element from it
    /// and inserting it into the sorted region.<br/>
    /// Unlike selection sort,heapsort does not waste time with a linear-time scan of the unsorted region;
    /// rather, heap sort maintains the unsorted region in a heap data structure to more quickly find the largest element in each step.<br/>
    /// Although somewhat slower in practice on most machines than a well-implemented quicksort,
    /// it has the advantage of a more favorable worst-case O(n log n) runtime.<br/>
    /// Heapsort is an in-place algorithm, but it is not a stable sort.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class HeapSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        public HeapSort()
        {
            Name = SortingAlgorithm.HeapSort;
            Time = new Stopwatch();
            IsStabil = false;
            BestCase = "Ω(nlog(n))";
            AverageCase = "θ(nlog(n))";
            WorstCase = "O(nlog(n))";
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
            if (output.Length <= 1)
            {
                return output;
            }
            for (int i = output.Length / 2 - 1; i >= 0; i--)
            {
                Heapify(output, output.Length, i, sortingType);
            }
            for (int i = output.Length - 1; i >= 0; i--)
            {
                Swap(output, 0, i);
                Heapify(output, i, 0, sortingType);
            }
            Time.Stop();
            return output;
        }

        private void Heapify(T[] array, int size, int index, SortingType sortingType)
        {
            int largestIndex = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;

            if (leftChild < size && Compare(array[leftChild], array[largestIndex], sortingType) > 0)
            {
                largestIndex = leftChild;
            }

            if (rightChild < size && Compare(array[rightChild], array[largestIndex], sortingType) > 0)
            {
                largestIndex = rightChild;
            }

            if (largestIndex != index)
            {
                Swap(array, index, largestIndex);
                Heapify(array, size, largestIndex, sortingType);
            }
        }
    }
}