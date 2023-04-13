namespace Sorting
{
    /// <summary>
    /// The class ArraysInfo is created to contain arrays information for printing.<br/>
    /// The class properties InputArray and OutputArray are dynamic types,<br/>
    /// but they should be digit or char arrays or AdvancedArray type.<br/>
    /// It does not matter whether the arrays are sorted or the same type.<br/>
    /// The class Printer will print them both if they match to the array or AdvancedArray type.
    /// </summary>
    public class ArraysInfo : IPrintableArraysInfo
    {
        /// <summary>
        /// The input array for printing.<br/>
        /// It should be a digit or char array or AdvancedArray type.
        /// </summary>
        public dynamic InputArray { get; }

        /// <summary>
        /// The output array for printing.<br/>
        /// It should be a digit or char array or AdvancedArray type.
        /// </summary>
        public dynamic OutputArray { get; }
        /// <summary>
        /// The length of the input array.
        /// </summary>
        public int InputArraySize { get; }

        /// <summary>
        /// The length of the output array.
        /// </summary>
        public int OutputArraySize { get; }

        /// <summary>
        /// Is the input array ascending sorted or not.
        /// </summary>
        public bool IsInputArraySorted { get; }

        /// <summary>
        /// Is the output array ascending sorted or not.
        /// </summary>
        public bool IsOutputArraySorted { get; }

        /// <summary>
        /// The type of the input array. It is created
        /// to show the character of the array.<br/>
        /// The arrays can be very different. Sometimes it is important to know
        /// which array type was beeing contained while calculating.
        /// </summary>
        public ArrayType InputArrayType { get; }

        /// <summary>
        /// The type of the output array. It is created
        /// to show the character of the array.<br/>
        /// The arrays can be very different. Sometimes it is important to know
        /// which array type was beeing contained while calculating.
        /// </summary>
        public ArrayType OutputArrayType { get; }

        /// <summary>
        /// Initializes a new instance of the ArraysInfo class.
        /// </summary>
        /// <param name="Input">The input array for printing.<br/>
        /// It should be a digit or char array or AdvancedArray type.</param>
        /// <param name="Output">The output array for printing.<br/>
        /// It should be a digit or char array or AdvancedArray type.</param>
        /// <exception cref="ArgumentException"></exception>
        public ArraysInfo(dynamic Input, dynamic Output)
        {
            bool isThisAdvancedArray =
                ((Input is AdvancedArray<byte>) ||
                    (Input is AdvancedArray<sbyte>) ||
                    (Input is AdvancedArray<byte>) ||
                    (Input is AdvancedArray<short>) ||
                    (Input is AdvancedArray<ushort>) ||
                    (Input is AdvancedArray<int>) ||
                    (Input is AdvancedArray<uint>) ||
                    (Input is AdvancedArray<long>) ||
                    (Input is AdvancedArray<ulong>) ||
                    (Input is AdvancedArray<float>) ||
                    (Input is AdvancedArray<double>) ||
                    (Input is AdvancedArray<decimal>) ||
                    (Input is AdvancedArray<char>)) &&
                ((Output is AdvancedArray<byte>) ||
                    (Output is AdvancedArray<sbyte>) ||
                    (Output is AdvancedArray<byte>) ||
                    (Output is AdvancedArray<short>) ||
                    (Output is AdvancedArray<ushort>) ||
                    (Output is AdvancedArray<int>) ||
                    (Output is AdvancedArray<uint>) ||
                    (Output is AdvancedArray<long>) ||
                    (Output is AdvancedArray<ulong>) ||
                    (Output is AdvancedArray<float>) ||
                    (Output is AdvancedArray<double>) ||
                    (Output is AdvancedArray<decimal>) ||
                    (Output is AdvancedArray<char>));

            bool isThisArray = (Input is sbyte[] ||
                Input is byte[] ||
                Input is short[] ||
                Input is ushort[] ||
                Input is int[] ||
                Input is uint[] ||
                Input is long[] ||
                Input is ulong[] ||
                Input is float[] ||
                Input is double[] ||
                Input is decimal[] ||
                Input is char[]) &&
                (Output is sbyte[] ||
                Output is byte[] ||
                Output is short[] ||
                Output is ushort[] ||
                Output is int[] ||
                Output is uint[] ||
                Output is long[] ||
                Output is ulong[] ||
                Output is float[] ||
                Output is double[] ||
                Output is decimal[] ||
                Output is char[]);

            if (isThisAdvancedArray)
            {
                InputArray = Input.TArray;
                OutputArray = Output.TArray;
                InputArraySize = Input.Size;
                OutputArraySize = Output.Size;
                IsInputArraySorted = Input.Sorted;
                IsOutputArraySorted = Output.Sorted;
                InputArrayType = Input.ArrayType;
                OutputArrayType = Output.ArrayType;
            }
            else if (isThisArray)
            {
                InputArray = Input;
                OutputArray = Output;
                InputArraySize = Input.Length;
                OutputArraySize = Input.Length;
                IsInputArraySorted = IsSorted(InputArray);
                IsOutputArraySorted = IsSorted(OutputArray);
                InputArrayType = ArrayType.Other;
                OutputArrayType = ArrayType.Other;
            }
            else
            {
                throw new ArgumentException("The arguments should be digit or char arrays or AdvancedArray type.");
            }
        }

        /// <summary>
        /// The method checks whether the input array is sorted.
        /// </summary>
        /// <param name="input">The array for checking.</param>
        /// <returns>True if the input array is sorted.<br/>
        /// False if the input array is not sorted.</returns>
        private static bool IsSorted<T>(T[] input) where T : IComparable
        {
            return AdvancedArray<T>.IsSorted(input);
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
        /// <returns>
        /// <c>
        /// Compare(x, y);<br/>
        /// if (x &gt; y) then it returns &gt; 0<br/>
        /// if (x == y) then it returns 0<br/>
        /// if (x &lt; y) then it returns &lt; 0
        /// </c>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If any of parameters is null,
        /// the method throws an argument
        /// exception.</exception>
        private static int Compare<T>(T? x, T? y) where T : IComparable
        {
            if ((x == null) || (y == null))
            {
                throw new ArgumentException();
            }
            return x.CompareTo(y);
        }
    }
}