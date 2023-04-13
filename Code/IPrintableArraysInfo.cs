namespace Sorting
{
    /// <summary>
    /// The interface IPrintableArraysInfo defines required for the class Printer properties,
    /// which contain information about input and output arrays.
    /// </summary>
    public interface IPrintableArraysInfo
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
    }
}