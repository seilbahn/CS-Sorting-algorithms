namespace Sorting
{
    /// <summary>
    /// The enumeration SortingAlgorithm contains names of sorting algorithms.
    /// </summary>
    [System.Flags]
    public enum SortingAlgorithm : ulong
    {
        /// <summary>
        /// The default value for the sorting algorithm name.
        /// </summary>
        DefaultSort       = 0b_0000_0000_0000_0000,

        // Stabil sorting algorithms
        /// <inheritdoc cref="BubbleSort&lt;T&gt;"/>
        BubbleSort        = 0b_0000_0000_0000_0001,

        /// <inheritdoc cref="CocktailSort&lt;T&gt;"/>
        CocktailSort      = 0b_0000_0000_0000_0010,

        /// <inheritdoc cref="InsertionSort&lt;T&gt;"/>
        InsertionSort     = 0b_0000_0000_0000_0100,

        /// <inheritdoc cref="GnomeSort&lt;T&gt;"/>
        GnomeSort         = 0b_0000_0000_0000_1000,

        /// <inheritdoc cref="MergeSort&lt;T&gt;"/>
        MergeSort         = 0b_0000_0000_0001_0000,

        /// <inheritdoc cref="TreeSort&lt;T&gt;"/>
        TreeSort          = 0b_0000_0000_0010_0000,

        // Unstabil sorting algorithms
        /// <inheritdoc cref="SelectionSort&lt;T&gt;"/>
        SelectionSort     = 0b_0000_0000_0100_0000,

        /// <inheritdoc cref="CombSort&lt;T&gt;"/>
        CombSort          = 0b_0000_0000_1000_0000,

        /// <inheritdoc cref="ShellSort&lt;T&gt;"/>
        ShellSort         = 0b_0000_0001_0000_0000,

        /// <inheritdoc cref="HeapSort&lt;T&gt;"/>
        HeapSort          = 0b_0000_0010_0000_0000,

        /// <inheritdoc cref="QuickSort&lt;T&gt;"/>
        QuickSort         = 0b_0000_0100_0000_0000,

        /// <inheritdoc cref="StoogeSort&lt;T&gt;"/>
        StoogeSort        = 0b_0000_1000_0000_0000,

        // Non-practical sorting algorithms
        /// <inheritdoc cref="BogoSort&lt;T&gt;"/>
        BogoSort          = 0b_0001_0000_0000_0000
    }
}