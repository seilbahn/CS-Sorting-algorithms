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
    public class TreeSort<T> : Algorithm<T>, ISortable<T> where T : IComparable
    {
        private List<T> ResultedList;
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

        public class Node
        {
            public T key;
            public Node left, right;

            public Node(T item)
            {
                key = item;
                left = right = null;
            }
        }

        Node root;

        void insert(T key, SortingType sortingType)
        {
            root = insertRec(root, key, sortingType);
        }

        Node insertRec(Node root, T key, SortingType sortingType)
        {
            if (root == null)
            {
                root = new Node(key);
                return root;
            }
            if (Compare(key, root.key, sortingType) <= 0)
            {
                root.left = insertRec(root.left, key, sortingType);
            }
            else if (Compare(key, root.key, sortingType) > 0)
            {
                root.right = insertRec(root.right, key, sortingType);
            }
            return root;
        }

        void inorderRec(Node root)
        {
            if (root != null)
            {
                inorderRec(root.left);
                ResultedList.Add(root.key);
                inorderRec(root.right);
            }
        }
        void treeins(T[] arr, SortingType sortingType)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                insert(arr[i], sortingType);
                Swaps++;
            }
        }

        T[] PreSort(T[] array, SortingType sortingType)
        {
            treeins(array, sortingType);
            inorderRec(root);
            return ResultedList.ToArray<T>();
        }
    }
}