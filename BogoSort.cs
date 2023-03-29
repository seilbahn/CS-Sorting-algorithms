using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The sorting algorithm is based on the generate and test paradigm.<br/>
    /// The function successively generates permutations of its input until it finds one that is sorted.<br/>
    /// It is not considered one of efficient algorithms for sorting.<br/>
    /// It is recommended to sort with this algorithm no more than 10 elements,
    /// because of the very bad time complexity (in case of using a standart low-mid desktop-CPU).
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class BogoSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
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

        public override T[] Sort(T[] input, SortingType sortingType = SortingType.Ascending)
        {
            T[] output = new T[input.Length];
            Array.Copy(input, output, input.Length);
            Comparisons = default;
            Swaps = default;
            Time = new Stopwatch();

            Time.Start();
            ToSort(output);
            Time.Stop();
            return output;
        }

        private bool IsSorted(T[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (Compare(array[i], array[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;
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

        private T[] ToSort(T[] array)
        {
            while (!IsSorted(array))
            {
                array = RandomPermutation(array);
            }
            return array;
        }
    }
}