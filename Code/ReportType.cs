namespace Sorting
{
    /// <summary>
    /// The enumeration ReportType contains types of the report.<br/>
    /// The class Printer allows to create different types of the reports.
    /// In order to choose the require type there is this enumeration.<br/>
    /// The printing of the large array may take a while because oftentimes
    /// it is quicker to sort the whole array than  that is why
    /// it can be more rationally not to print the arrays.
    /// </summary>
    [System.Flags]
    public enum ReportType : byte
    {
        /// <summary>
        /// The report with input and output arrays.
        /// </summary>
        WithArray     = 0b_0000,

        /// <summary>
        /// The report without input and output arrays.
        /// </summary>
        WithoutArray  = 0b_0001     
    }
}