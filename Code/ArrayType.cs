namespace Sorting
{
    /// <summary>
    /// The enumeration ArrayType represents
    /// some types of the array.<br/>
    /// Random - the randomized array<br/>
    /// Sorter - the ascending sorted array<br/>
    /// NearlySorted - the ascending sorted array with few shuffled elements<br/>
    /// Reversed - the descending sorted array<br/>
    /// FewUnique - the array with few unique elements
    /// </summary>
    [System.Flags]
    public enum ArrayType : byte
    {
        Random         = 0b_0000_0000,
        Sorted         = 0b_0000_0001,
        NearlySorted   = 0b_0000_0010,
        Reversed       = 0b_0000_0100,
        FewUnique      = 0b_0000_1000
    }
}