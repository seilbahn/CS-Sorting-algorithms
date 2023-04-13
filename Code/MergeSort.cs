using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The merge sort is an efficient, general-purpose, and comparison-based sorting algorithm.<br/>
    /// Most implementations produce a stable sort, which means that the order of equal elements is the same in the input and output.
    /// Conceptually, a merge sort works as follows:<br/>
    /// 1.Dividing the unsorted list into n sublists, each containing one element (a list of one element is considered sorted).<br/>
    /// 2.Repeatedly merging sublists to produce new sorted sublists until there is only one sublist remaining.<br/>
    /// This is the sorted list.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class MergeSort<T> : Algorithm<T> where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the MergeSort class.
        /// </summary>
        public MergeSort()
        {
            Name = SortingAlgorithm.MergeSort;
            Time = new Stopwatch();
            IsStabil = true;
            BestCase = "Ω(nlog(n))";
            AverageCase = "θ(nlog(n))";
            WorstCase = "O(nlog(n))";
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
            StartSort(output, 0, output.Length - 1, sortingType);
            Time.Stop();
            return output;
        }

        private void Merge(T[] array, int lowIndex, int middleIndex, int highIndex, SortingType sortingType)
        {
            T[] tempArray = new T[highIndex - lowIndex + 1];
            int left = lowIndex;
            int right = middleIndex + 1;
            int index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (Compare(array[left], array[right], sortingType) < 0)
                {
                    tempArray[index] = array[left];
                    Swaps++;
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    Swaps++;
                    right++;
                }
                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                Swaps++;
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                Swaps++;
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
                Swaps++;
            }
        }

        private void StartSort(T[] array, int lowIndex, int highIndex, SortingType sortingType)
        {
            if (lowIndex < highIndex)
            {
                int middleIndex = (lowIndex + highIndex) / 2;
                StartSort(array, lowIndex, middleIndex, sortingType);
                StartSort(array, middleIndex + 1, highIndex, sortingType);
                Merge(array, lowIndex, middleIndex, highIndex, sortingType);
            }
        }
    }
}