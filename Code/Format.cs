namespace Sorting
{
    /// <summary>
    /// The enumeration Format represents
    /// types of the report.
    /// </summary>
    [System.Flags]
    internal enum Format : byte
    {
        /// <summary>
        /// The .txt-file type of the report.
        /// </summary>
        Txt    = 0b_0000,
        /// <summary>
        /// The .xlsx-file type of the report.
        /// </summary>
        Excel  = 0b_0001
    }
}