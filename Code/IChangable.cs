namespace Sorting
{
    public interface IChangable<T> where T : IComparable
    {
        void CreateArray(T MinValue, T MaxValue, decimal SimilarElementsExpectancy = default, ArrayType arrayType = ArrayType.Random);
        void DoubleArray();
        void ReverseArray();
        void ShuffleArray();
        void SimilarArrayElements(decimal SimilarElementsExpectancy = 0.5M);
    }
}