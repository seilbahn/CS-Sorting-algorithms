using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// The abstract class Algorithm is created to be a template
    /// for any other sorting algorithms.
    /// </summary>
    abstract public class Algorithm : ISortable
    {
        /// <summary>
        /// The name of the algorithm.<br/>The enumeration SortingAlgorithm contains
        /// some algorithm names. The name DefaultSort is used by default.
        /// </summary>
        public SortingAlgorithm Name { get; protected set; }

        /// <summary>
        /// The Stopwatch class instance.
        /// </summary>
        public Stopwatch Time { get; protected set; }

        /// <summary>
        /// Is the sorting algorithm stabil or not?<br/>
        /// Stable sorting algorithms maintain the relative order of records
        /// with equal keys.
        /// </summary>
        public bool IsStabil { get; protected set; }

        /// <summary>
        /// The amount of compare operations.
        /// </summary>
        public ulong Comparisons { get; protected set; }

        /// <summary>
        /// The amount of the changing keys position operations.
        /// </summary>
        public ulong Swaps { get; protected set; }

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
        public string BestCase { get; protected set; }

        /// <summary>
        /// The algorithm complexity in the average case.<br/>
        /// It defines the average case of an algorithm’s time complexity,
        /// the Theta notation defines when the set of functions lies in both O(expression)
        /// and Omega(expression), then Theta notation is used.<br/>
        /// This is how we define a time complexity average case for an algorithm. 
        /// </summary>
        public string AverageCase { get; protected set; }

        /// <summary>
        /// The algorithm complexity in the worst case.<br/>
        /// It defines worst-case time complexity by using the Big-O notation,
        /// which determines the set of functions grows slower than or at the same rate as the expression.<br/>
        /// Furthermore, it explains the maximum amount of time an algorithm requires to consider all input values.
        /// </summary>
        public string WorstCase { get; protected set; }

        /// <summary>
        /// Memory usage (and use of other computer resources).<br/>
        /// In particular, some sorting algorithms are "in-place".<br/>
        /// Strictly, an in-place sort needs only O(1) memory beyond the items being sorted;
        /// sometimes O(log n) additional memory is considered "in-place".
        /// </summary>
        public string WorstCaseSpaceComplexity { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the Algorithm class.
        /// </summary>
        protected Algorithm()
        {
            Name = SortingAlgorithm.DefaultSort;
            Time = Stopwatch.StartNew();
            IsStabil = default;
            Comparisons = default;
            Swaps = default;
            BestCase = string.Empty;
            AverageCase = string.Empty;
            WorstCase = string.Empty;
            WorstCaseSpaceComplexity = string.Empty;
        }

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
        abstract public sbyte[] Sort(sbyte[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public byte[] Sort(byte[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public short[] Sort(short[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public ushort[] Sort(ushort[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public int[] Sort(int[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public uint[] Sort(uint[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public long[] Sort(long[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public ulong[] Sort(ulong[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public float[] Sort(float[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public double[] Sort(double[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public decimal[] Sort(decimal[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Sort(sbyte[], SortingType)"/>
        abstract public char[] Sort(char[] input, SortingType sortingType = SortingType.Ascending);
    }

    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    /// <inheritdoc cref="Algorithm"/>
    abstract public class Algorithm<T> : Algorithm where T : IComparable
    {
        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        abstract public T[] Sort(T[] input, SortingType sortingType = SortingType.Ascending);

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override sbyte[] Sort(sbyte[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override byte[] Sort(byte[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override short[] Sort(short[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override ushort[] Sort(ushort[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override int[] Sort(int[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override uint[] Sort(uint[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override long[] Sort(long[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override ulong[] Sort(ulong[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override float[] Sort(float[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override double[] Sort(double[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override decimal[] Sort(decimal[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override char[] Sort(char[] input, SortingType sortingType = SortingType.Ascending)
        {
            return Sort(input, sortingType);
        }

        /// <summary>
        /// The method is created to compare two generic variables.<br/>
        /// <c>
        /// Compare(x, y);<br/>
        /// if (x &gt; y) then it returns &gt; 0<br/>
        /// if (x == y) then it returns 0<br/>
        /// if (x &lt; y) then it returns &lt; 0
        /// </c>
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <param name="sortingType">The algorithm can sort ascending also descending.<br/>
        /// If you need ascending sorting, you do not have to specify this parameter.<br/>
        /// If you need descending sorting, you should specify this parameter with SortingType.Descending.<br/>
        /// Warning: if you specify SortingType.Descending, the method will compare the second value with the first value.<br/>
        /// So, in this case, the method will work oppositely.
        /// </param>
        /// <returns>
        /// <c>
        /// Compare(x, y);<br/>
        /// if (x &gt; y) then it returns &gt; 0<br/>
        /// if (x == y) then it returns 0<br/>
        /// if (x &lt; y) then it returns &lt; 0
        /// </c>
        /// </returns>
        /// <exception cref="ArgumentException">If x or y argument is null, the argument exception will be thrown.</exception>
        protected int Compare(T? x, T? y, SortingType sortingType = SortingType.Ascending)
        {
            Comparisons++;
            if ((x == null) || (y == null))
            {
                throw new ArgumentException();
            }
            if (sortingType.Equals(SortingType.Ascending))
            {
                return x.CompareTo(y);
            }
            if (sortingType.Equals(SortingType.Descending))
            {
                return y.CompareTo(x);
            }
            return x.CompareTo(y);
        }

        /// <summary>
        /// The method swaps two keys in the array:
        /// array[i] ↔ array[j].
        /// </summary>
        /// <param name="input">The reference to the array.</param>
        /// <param name="i">The index of the first element.</param>
        /// <param name="j">The index of the second element.</param>
        protected void Swap(T[] input, int i, int j)
        {
            //T temp = input[i];
            //input[i] = input[j];
            //input[j] = temp;
            (input[j], input[i]) = (input[i], input[j]);
            Swaps++;
        }
    }
}