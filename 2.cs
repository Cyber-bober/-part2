using System;
using System.IO;
using System.Collections.Generic;
//добавить узел, проверить новое дерево с добавлением
namespace BST_Balance_Check
{
    public class Node
    {
        public int inf;
        public Node left;
        public Node right;
        
        public Node(int value)
        {
            inf = value;
            left = null;
            right = null;
        }
        
        public static void Add(ref Node root, int value)
        {
            if (root == null)
            {
                root = new Node(value);
            }
            else if (value < root.inf)
            {
                Add(ref root.left, value);
            }
            else if (value > root.inf)
            {
                Add(ref root.right, value);
            }
        }
        
        public static int GetHeight(Node node)
        {
            if (node == null) return -1;
            return 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
        }
        
        public static bool IsBalanced(Node node)
        {
            if (node == null) return true;
            int lh = GetHeight(node.left);
            int rh = GetHeight(node.right);
            if (Math.Abs(lh - rh) > 1) return false;
            return IsBalanced(node.left) && IsBalanced(node.right);
        }
        
        public static Node Clone(Node original)
        {
            if (original == null) return null;
            Node copy = new Node(original.inf);
            copy.left = Clone(original.left);
            copy.right = Clone(original.right);
            return copy;
        }
        
        public static void CollectGaps(Node node, List<int> gaps)
        {
            if (node == null) return;
            
            if (node.left != null)
            {
                int candidate = node.left.inf + 1;
                if (candidate < node.inf) gaps.Add(candidate);
            }
            if (node.right != null)
            {
                int candidate = node.inf + 1;
                if (candidate < node.right.inf) gaps.Add(candidate);
            }
            
            CollectGaps(node.left, gaps);
            CollectGaps(node.right, gaps);
        }
        
        public static int FindMin(Node node)
        {
            while (node.left != null) node = node.left;
            return node.inf;
        }
        
        public static int FindMax(Node node)
        {
            while (node.right != null) node = node.right;
            return node.inf;
        }
    }
    
    public class BinaryTree
    {
        private Node tree;
        
        public BinaryTree() { tree = null; }
        
        public void Add(int value) { Node.Add(ref tree, value); }
        
        public bool CheckBalance() { return Node.IsBalanced(tree); }
        
        public int? FindBalancingValue()
        {
            if (Node.IsBalanced(tree))
            {
                if (tree == null) return 0;
                int min = Node.FindMin(tree);
                return min - 1;
            }
            
            List<int> candidates = new List<int>();
            Node.CollectGaps(tree, candidates);
            
            int minVal = Node.FindMin(tree);
            int maxVal = Node.FindMax(tree);
            candidates.Add(minVal - 1);
            candidates.Add(maxVal + 1);
            
            foreach (int val in candidates)
            {
                Node testRoot = Node.Clone(tree);
                Node.Add(ref testRoot, val);
                if (Node.IsBalanced(testRoot))
                {
                    return val;
                }
            }
            
            return null;
        }
    }
    
    class Program
    {
        static void Main()
        {
            BinaryTree tree = new BinaryTree();
            
            using (StreamReader fileIn = new StreamReader("input.txt"))
            {
                string line = fileIn.ReadToEnd();
                string[] data = line.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in data)
                {
                    tree.Add(int.Parse(item));
                }
            }
            
            using (StreamWriter fileOut = new StreamWriter("output.txt"))
            {
                if (tree.CheckBalance())
                {
                    fileOut.WriteLine("Tree is already balanced");
                    int? suggestion = tree.FindBalancingValue();
                    if (suggestion.HasValue)
                    {
                        fileOut.WriteLine($"Can add: {suggestion.Value}");
                    }
                }
                else
                {
                    int? result = tree.FindBalancingValue();
                    if (result.HasValue)
                    {
                        fileOut.WriteLine($"Yes, can add: {result.Value}");
                    }
                    else
                    {
                        fileOut.WriteLine("No single value can balance this tree");
                    }
                }
            }
        }
    }
}
