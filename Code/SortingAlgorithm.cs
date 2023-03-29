namespace Sorting
{
    [System.Flags]
    public enum SortingAlgorithm : ulong
    {
        DefaultSort       = 0b_0000_0000_0000_0000,
        // Stabil sorting algorithms
        BubbleSort        = 0b_0000_0000_0000_0001,
        CocktailSort      = 0b_0000_0000_0000_0010,
        InsertionSort     = 0b_0000_0000_0000_0100,
        GnomeSort         = 0b_0000_0000_0000_1000,
        MergeSort         = 0b_0000_0000_0001_0000,
        TreeSort          = 0b_0000_0000_0010_0000,
        // Unstabil sorting algorithms
        SelectionSort     = 0b_0000_0000_0100_0000,
        CombSort          = 0b_0000_0000_1000_0000,
        ShellSort         = 0b_0000_0001_0000_0000,
        HeapSort          = 0b_0000_0010_0000_0000,
        QuickSort         = 0b_0000_0100_0000_0000,
        StoogeSort        = 0b_0000_1000_0000_0000,
        // Non-practical sorting algorithms
        BogoSort          = 0b_0001_0000_0000_0000
    }
}