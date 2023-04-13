namespace Sorting
{
    /// <summary>
    /// The enumeration SortingType contains two sorting types:<br/>
    /// 1.Ascending sorting - upwards;<br/>
    /// 2.Descending sorting - downwards.
    /// </summary>
    [System.Flags]
    public enum SortingType : byte
    {
        /// <summary>
        /// Upwards sorting.
        /// </summary>
        Ascending  = 0b_0,
        /// <summary>
        /// Downwards sorting.
        /// </summary>
        Descending = 0b_1
    }
}