namespace Sorting
{
    [System.Flags]
    public enum SortingType : byte
    {
        Ascending  = 0b_0,
        Descending = 0b_1
    }
}