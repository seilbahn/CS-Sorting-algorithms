using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The cocktail shaker sort, also known as bidirectional bubble sort,
    /// cocktail sort, shaker sort (which can also refer to a variant of selection sort),
    /// ripple sort, shuffle sort, or shuttle sort, is an extension of bubble sort.<br/>
    /// The algorithm extends bubble sort by operating in two directions.<br/>
    /// While it improves on bubble sort by more quickly moving items to the beginning of the list,
    /// it provides only marginal performance improvements.<br/>
    /// Like most variants of bubble sort, cocktail shaker sort is used primarily as an educational tool. 
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class CocktailSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        public CocktailSort()
        {
            Name = SortingAlgorithm.CocktailSort;
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
            int left = 0;
            int right = output.Length - 1;
            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if (Compare(output[i], output[i + 1], sortingType) > 0)
                    {
                        Swap(output, i, i + 1);
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (Compare(output[i - 1], output[i], sortingType) > 0)
                    {
                        Swap(output, i - 1, i);
                    }
                }
                left++;
            }
            Time.Stop();
            return output;
        }
    }
}