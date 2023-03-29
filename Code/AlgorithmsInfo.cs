/*
 * This class contains dynamic types
 * therefore the code may be incorrect in some ways.
 * It's a temporary decission for the class Printer.
 */

namespace Sorting
{
    public class AlgorithmsInfo
    {
        public List<dynamic> AlgorithmsList { get; set; }
        public AlgorithmsInfo()
        {
            AlgorithmsList = new List<dynamic>();
        }
        public AlgorithmsInfo(List<dynamic> list)
        {
            AlgorithmsList = list;
        }
    }
}