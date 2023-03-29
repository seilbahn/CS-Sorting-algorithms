/*
 * This class contains dynamic types
 * therefore the code may be incorrect in some ways.
 * It's a temporary decission for the class Printer.
 */

namespace Sorting
{
    public class ArraysInfo
    {
        public dynamic InputArray { get; set; }
        public dynamic OutputArray { get; set; }
        public int InputArraySize { get; }
        public int OutputArraySize { get; }
        public bool IsInputArraySorted { get; }
        public bool IsOutputArraySorted { get; }
        public ArrayType InputArrayType { get; }
        public ArrayType OutputArrayType { get; }

        public ArraysInfo(dynamic Input, dynamic Output)
        {
            InputArray = Input.TArray;
            OutputArray = Output.TArray;
            InputArraySize = Input.Size;
            OutputArraySize = Output.Size;
            IsInputArraySorted = Input.Sorted;
            IsOutputArraySorted = Output.Sorted;
            InputArrayType = Input.ArrayType;
            OutputArrayType = Output.ArrayType;            
        }
    }
}