namespace Sorting
{
    /// <summary>
    /// The class AlgorithmsInfo represents the way to add the algorithms information
    /// to the class Printer while creating a report.
    /// </summary>
    public class AlgorithmsInfo
    {
        /// <summary>
        /// The internal list of the sorting algorithms.
        /// It contains algorithms information for the report.
        /// </summary>
        public List<ISortable> AlgorithmsList { get; set; }

        /// <summary>
        /// The indexer for the list of algorithms.
        /// </summary>
        /// <param name="index">The index of the key.</param>
        /// <returns>The value at the index position.</returns>
        /// <exception cref="IndexOutOfRangeException">If the index
        /// is out of list borders, the IndexOutOfRangeException will be thrown.</exception>
        public ISortable this[int index]
        {
            get
            {
                if ((index < 0) || (index >= AlgorithmsList.Count))
                {
                    throw new IndexOutOfRangeException("Index out of range!");
                }
                return AlgorithmsList[index];
            }
            set
            {
                if ((index < 0) || (index >= AlgorithmsList.Count))
                {
                    throw new IndexOutOfRangeException("Index out of range!");
                }
                AlgorithmsList[index] = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the AlgorithmsList class.
        /// </summary>
        public AlgorithmsInfo()
        {
            AlgorithmsList = new List<ISortable>();
        }

        /// <summary>
        /// Initializes a new instance of the AlgorithmsList class.
        /// </summary>
        public AlgorithmsInfo(List<ISortable> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }
            else if (list.Count == 0)
            {
                throw new ArgumentException("The list is empty!");
            }
            else
            {
                AlgorithmsList = list;
            }
        }

        /// <summary>
        /// The method adds a reference to the instance of the
        /// algorithm to the internal list.
        /// </summary>
        /// <param name="algorithm">The algorithm for the report.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddAlgorithm(ISortable algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException("The value \'algorithm\' is null!");
            }
            AlgorithmsList.Add(algorithm);
        }

        /// <summary>
        /// The method deletes all algorithms in the internal list.
        /// </summary>
        public void DeleteAlgorithms()
        {
            AlgorithmsList.Clear();
        }
    }
}