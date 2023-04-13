using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The sorting algorithm is based on the generate and test paradigm.<br/>
    /// The function successively generates permutations of its input until it finds one that is sorted.<br/>
    /// It is not considered one of efficient algorithms for sorting.<br/>
    /// It is recommended to sort with this algorithm no more than 10 elements,
    /// because of the very bad time complexity (in case of using a standart low-mid desktop-CPU).<br/>
    /// The amount of comparizons is zero, because the algorithm does not compare keys,
    /// though there is a method for checking if the array is sorted.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class BogoSort<T> : Algorithm<T> where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the BogoSort class.
        /// </summary>
        public BogoSort()
        {
            Name = SortingAlgorithm.BogoSort;
            Time = new Stopwatch();
            IsStabil = false;
            BestCase = "Ω(n)";
            AverageCase = "θ((n+1)!)";
            WorstCase = "O(∞)";
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
            while (!AdvancedArray<T>.IsSorted(output))
            {
                output = RandomPermutation(output);
            }
            Time.Stop();
            return output;
        }

        private T[] RandomPermutation(T[] array)
        {
            Random random = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int i = random.Next(n + 1);
                Swap(array, i, n);
            }
            return array;
        }
    }
}