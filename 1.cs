using System;
using System.IO;
using System.Collections.Generic;

namespace BST_Height
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
        
        public static int CalculateHeight(Node node, ref int count, ref int height)  //2 пример в решении задач, через count++
        {
            if (node == null) return -1;
            if (count > height) {
                height = count;
            }
            count++;
            
            
            
            // return 1 + Math.Max(CalculateHeight(node.left), CalculateHeight(node.right));
        }
        
        public static void TraverseWithHeight(Node node, List<string> output)
        {
            if (node != null)
            {
                TraverseWithHeight(node.left, output);
                int h = CalculateHeight(node);
                output.Add($"{node.inf} {h}");
                TraverseWithHeight(node.right, output);
            }
        }
    }
    
    public class BinaryTree
    {
        private Node tree;
        
        public BinaryTree() { tree = null; }
        
        public void Add(int value) { Node.Add(ref tree, value); }
        
        public List<string> GetNodesWithHeights()
        {
            List<string> result = new List<string>();
            Node.TraverseWithHeight(tree, result);
            return result;
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
            
            List<string> result = tree.GetNodesWithHeights();
            using (StreamWriter fileOut = new StreamWriter("output.txt"))
            {
                foreach (string item in result)
                {
                    fileOut.WriteLine(item);
                }
            }
        }
    }
}
