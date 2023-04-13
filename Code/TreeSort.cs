#nullable disable

using System.Diagnostics;

namespace Sorting
{
    /// <summary>
    /// Tree sort is a sorting technique that is totally dependent on the data structure of a binary search tree.<br/>
    /// In this sorting technique first, the binary search tree is created from the given data.<br/>
    /// A binary search tree is a special type of binary tree in which,
    /// for each parent node, the left child will be lesser or smaller than the parent node
    /// and the right child will be equal or greater than the parent node.<br/>
    /// In the case of a binary search tree, the inorder traversal always displays the elements in sorted order.
    /// Hence, this property is used in this sorting.<br/>
    /// After creating the binary search tree, only the inorder traversal is performed to display the array in sorted order.
    /// </summary>
    /// <typeparam name="T">sbyte, byte, short, ushort, int, uint,
    /// long, ulong, float, double, decimal, char</typeparam>
    public class TreeSort<T> : Algorithm<T> where T : IComparable
    {
        private class Node
        {
            public T key;
            public Node left, right;

            public Node(T item)
            {
                key = item;
                left = right = null;
            }
        }

        private List<T> ResultedList { get; }

        /// <summary>
        /// Initializes a new instance of the BubbleSort class.
        /// </summary>
        public TreeSort()
        {
            Name = SortingAlgorithm.TreeSort;
            Time = new Stopwatch();
            IsStabil = true;
            BestCase = "Ω(nlog(n))";
            AverageCase = "θ(nlog(n))";
            WorstCase = "O(n^2)";
            WorstCaseSpaceComplexity = "O(n)";
            root = null;
            ResultedList = new List<T>();
        }

        /// <inheritdoc cref="Algorithm.Sort(sbyte[], SortingType)"/>
        public override T[] Sort(T[] input, SortingType sortingType = SortingType.Ascending)
        {
            T[] output = new T[input.Length];
            Array.Copy(input, output, input.Length);
            Comparisons = default;
            Swaps = default;
            Time = new Stopwatch();

            Time.Start();
            output = PreSort(output, sortingType);
            Time.Stop();
            return output;
        }

        private Node root;

        private void Insert(T key, SortingType sortingType)
        {
            root = InsertRec(root, key, sortingType);
        }

        private Node InsertRec(Node root, T key, SortingType sortingType)
        {
            if (root == null)
            {
                root = new Node(key);
                return root;
            }
            if (Compare(key, root.key, sortingType) <= 0)
            {
                root.left = InsertRec(root.left, key, sortingType);
            }
            else if (Compare(key, root.key, sortingType) > 0)
            {
                root.right = InsertRec(root.right, key, sortingType);
            }
            return root;
        }

        private void InorderRec(Node root)
        {
            if (root != null)
            {
                InorderRec(root.left);
                ResultedList.Add(root.key);
                InorderRec(root.right);
            }
        }
        private void Treeins(T[] arr, SortingType sortingType)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Insert(arr[i], sortingType);
                Swaps++;
            }
        }

        private T[] PreSort(T[] array, SortingType sortingType)
        {
            Treeins(array, sortingType);
            InorderRec(root);
            return ResultedList.ToArray<T>();
        }
    }
}