namespace Sorting
{
    /// <summary>
    /// The enumeration ArrayType represents
    /// types of the array.<br/>
    /// In practice it may be hard to define which type the array is.<br/>
    /// The randomized array has a chance to be sorted or reversed.
    /// Although the expectation will be very small in this case.
    /// The algorithm for the creating an array with the defined type will do the best
    /// to create an array which will be match the type.
    /// </summary>
    [System.Flags]
    public enum ArrayType : ulong
    {
        /// <summary>
        /// The default randomized array.<br/>
        /// It means that the array contains different keys and it is not sorted.
        /// </summary>
        Random         = 0b_0000_0000,

        /// <summary>
        /// The ascending sorted array.
        /// </summary>
        Sorted         = 0b_0000_0001,

        /// <summary>
        /// The asceinding sorted array, but some keys are swaped places.
        /// </summary>
        NearlySorted   = 0b_0000_0010,

        /// <summary>
        /// The descending sorted array.
        /// </summary>
        Reversed       = 0b_0000_0100,

        /// <summary>
        /// The definition FewUnique means that the array contains only few unique keys
        /// and it is also not sorted.
        /// </summary>
        FewUnique      = 0b_0000_1000,

        /// <summary>
        /// The other type of the array. The array may be entered by the user or it does not match any other type.
        /// </summary>
        Other          = 0b_0001_0000
    }
}