using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The interface ISortable defines some requires
    /// to the classes, which contain methods to sort an array.
    /// </summary>
    public interface ISortable
    {
        /// <summary>
        /// The name of the algorithm.<br/>The enumeration SortingAlgorithm contains
        /// some algorithm names. The name DefaultSort is used by default.
        /// </summary>
        public SortingAlgorithm Name { get; }

        /// <summary>
        /// The Stopwatch class instance.
        /// </summary>
        public Stopwatch Time { get; }

        /// <summary>
        /// Is the sorting algorithm stabil or not?<br/>
        /// Stable sorting algorithms maintain the relative order of records
        /// with equal keys.
        /// </summary>
        public bool IsStabil { get; }

        /// <summary>
        /// The amount of compare operations.
        /// </summary>
        public ulong Comparisons { get; }

        /// <summary>
        /// The amount of the changing keys position operations.
        /// </summary>
        public ulong Swaps { get; }

        /// <summary>
        /// The algorithm complexity in the best case.<br/>
        /// The array may be already sorted, and
        /// the best case will be "Ω(1)".<br/>
        /// It defines the best case of an algorithm’s time complexity,
        /// the Omega notation defines whether the set of functions will grow
        /// faster or at the same rate as the expression.<br/>
        /// Furthermore, it explains the minimum amount of time an algorithm
        /// requires to consider all input values.
        /// </summary>
        public string BestCase { get; }

        /// <summary>
        /// The algorithm complexity in the average case.<br/>
        /// It defines the average case of an algorithm’s time complexity,
        /// the Theta notation defines when the set of functions lies in both O(expression)
        /// and Omega(expression), then Theta notation is used.<br/>
        /// This is how we define a time complexity average case for an algorithm. 
        /// </summary>
        public string AverageCase { get; }

        /// <summary>
        /// The algorithm complexity in the worst case.<br/>
        /// It defines worst-case time complexity by using the Big-O notation,
        /// which determines the set of functions grows slower than or at the same rate as the expression.<br/>
        /// Furthermore, it explains the maximum amount of time an algorithm requires to consider all input values.
        /// </summary>
        public string WorstCase { get; }

        /// <summary>
        /// Memory usage (and use of other computer resources).<br/>
        /// In particular, some sorting algorithms are "in-place".<br/>
        /// Strictly, an in-place sort needs only O(1) memory beyond the items being sorted;
        /// sometimes O(log n) additional memory is considered "in-place".
        /// </summary>
        public string WorstCaseSpaceComplexity { get; }

        /// <summary>
        /// The sorting algorithm method.<br/>It creates a copy
        /// of the input array and works only with the new array.<br/>
        /// The method returns a reference to the new sorted array.
        /// </summary>
        /// <param name="input">The reference to the input array.</param>
        /// <param name="sortingType">The type of sorting.<br/>
        /// The algorithm can sort ascending also descending.<br/>
        /// This parameter should be one of the enumeration SortingType values: SortingType.Ascending or
        /// SortingType.Descending.<br/>
        /// The default value is SortingType.Ascending.</param>
        /// <returns>The reference to the new sorted array.
        /// The input array will stay the same.</returns>
        public sbyte[] Sort(sbyte[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public byte[] Sort(byte[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public short[] Sort(short[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public ushort[] Sort(ushort[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public int[] Sort(int[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public uint[] Sort(uint[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public long[] Sort(long[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public ulong[] Sort(ulong[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public float[] Sort(float[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public double[] Sort(double[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public decimal[] Sort(decimal[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        public char[] Sort(char[] input, SortingType sortingType = SortingType.Ascending);
    }
}