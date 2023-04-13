using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The stooge sort is a recursive sorting algorithm.<br/>
    /// It is notable for its exceptionally bad time complexity of O(nlog 3 / log 1.5 ) = O(n2.7095...).<br/>
    /// The running time of the algorithm is thus slower compared to reasonable sorting algorithms,
    /// and is slower than bubble sort, a canonical example of a fairly inefficient sort.
    /// It is however more efficient than the slow sort.<br/>
    /// The algorithm is defined as follows:<br/>
    /// If the value at the start is larger than the value at the end, swap them.<br/>
    /// If there are 3 or more elements in the list, then:<br/>
    /// 1.Stooge sort the initial 2/3 of the list;<br/>
    /// 2.Stooge sort the final 2/3 of the list;<br/>
    /// 3.Stooge sort the initial 2/3 of the list again.<br/>
    /// It is important to get the integer sort size used in the recursive calls by rounding the 2/3 upwards,
    /// e.g. rounding 2/3 of 5 should give 4 rather than 3, as otherwise the sort can fail on certain data. 
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class StoogeSort<T> : Algorithm<T> where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the StoogeSort class.
        /// </summary>
        public StoogeSort()
        {
            Name = SortingAlgorithm.StoogeSort;
            Time = new Stopwatch();
            IsStabil = false;
            BestCase = "Ω(n^(log3/log1.5))";
            AverageCase = "θ(n^(log3/log1.5))";
            WorstCase = "O(n^(log3/log1.5))";
            WorstCaseSpaceComplexity = "O(n)";
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
            StartSort(output, sortingType);
            Time.Stop();
            return output;
        }

        private T[] StartSort(T[] array, int startIndex, int endIndex, SortingType sortingType)
        {
            if (Compare(array[startIndex], array[endIndex], sortingType) > 0)
            {
                Swap(array, startIndex, endIndex);
            }
            if (endIndex - startIndex > 1)
            {
                int len = (endIndex - startIndex + 1) / 3;
                StartSort(array, startIndex, endIndex - len, sortingType);
                StartSort(array, startIndex + len, endIndex, sortingType);
                StartSort(array, startIndex, endIndex - len, sortingType);
            }
            return array;
        }

        private T[] StartSort(T[] array, SortingType sortingType)
        {
            return StartSort(array, 0, array.Length - 1, sortingType);
        }
    }
}