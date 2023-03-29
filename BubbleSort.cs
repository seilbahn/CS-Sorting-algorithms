using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The bubble sort, sometimes referred to as sinking sort,
    /// is a simple sorting algorithm that repeatedly steps through the input list element by element,
    /// comparing the current element with the one after it, swapping their values if needed.<br/>
    /// These passes through the list are repeated until no swap had to be performed during a pass,
    /// meaning that the list has become fully sorted.<br/>
    /// The algorithm, which is a comparison sort, is named for the way the larger elements "bubble" up to the top of the list.<br/>
    /// This simple algorithm performs poorly in real world use and is used primarily as an educational tool. 
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class BubbleSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        public BubbleSort()
        {
            Name = SortingAlgorithm.BubbleSort;
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
            for (int i = 0; i < output.Length; i++)
            {
                for (int j = i + 1; j < output.Length; j++)
                {
                    if (Compare(output[i], output[j], sortingType) > 0)
                    {
                        Swap(output, i, j);
                    }
                }
            }
            Time.Stop();
            return output;
        }
    }
}