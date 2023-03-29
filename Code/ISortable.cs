using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The interface ISortable defines some requires
    /// to the classes, which contain methods to sort an array.
    /// The sorting algorithm must have a name, complexity
    /// (best, average and worst cases), elapsed time (from
    /// start sorting to the end), is the algorithm stabil, amount of
    /// comparisons and swaps.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public interface ISortable<T> where T : IComparable
    {
        public SortingAlgorithm Name { get; }
        public Stopwatch Time { get; }
        public bool IsStabil { get; }
        public ulong Comparisons { get; }
        public ulong Swaps { get; }
        public string BestCase { get; }
        public string AverageCase { get; }
        public string WorstCase { get; }
        public string WorstCaseSpaceComplexity { get; }
        public T[] Sort(T[] input, SortingType sortingType = SortingType.Ascending);
    }
}