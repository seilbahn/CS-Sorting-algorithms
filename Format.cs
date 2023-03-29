namespace Sorting
{
    /// <summary>
    /// The enumeration Format represents
    /// some types of the report.<br/>
    /// Txt - a simple txt-file<br/>
    /// Excel - .xlsx-format file<br/>
    /// </summary>
    [System.Flags]
    public enum Format : byte
    {
        Txt    = 0b_0,
        Excel  = 0b_1
    }
}
