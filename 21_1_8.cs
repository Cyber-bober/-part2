/*Найти наибольшее из значений листьев*/

using System;
using System.IO;

namespace Example
{
    public class BinaryTree
    {
        public class Node
        {
            public int inf;
            public Node left;
            public Node right;

            public Node(int nodeInf)
            {
                inf = nodeInf;
                left = null;
                right = null;
            }

            public static void Add(ref Node r, int nodeInf)
            {
                if (r == null)
                {
                    r = new Node(nodeInf);
                }
                else
                {
                    if (r.inf > nodeInf)
                    {
                        Add(ref r.left, nodeInf);
                    }
                    else if (r.inf < nodeInf)
                    {
                        Add(ref r.right, nodeInf);
                    }
                }
            }

            public static void MaxLeaf(Node t, ref int maxVal, ref bool found)
            {
                if (t != null)
                {
                    if (t.left == null && t.right == null)
                    {
                        if (!found || t.inf > maxVal)
                        {
                            maxVal = t.inf;
                            found = true;
                        }
                    }
                    else
                    {
                        MaxLeaf(t.left, ref maxVal, ref found);
                        MaxLeaf(t.right, ref maxVal, ref found);
                    }
                }
            }

            public static void Preorder(Node t)
            {
                if (t != null)
                {
                    Console.Write("{0} ", t.inf);
                    Preorder(t.left);
                    Preorder(t.right);
                }
            }
        }

        private Node tree;

        public BinaryTree()
        {
            tree = null;
        }

        public void Add(int nodeInf)
        {
            Node.Add(ref tree, nodeInf);
        }

        public void Preorder()
        {
            Node.Preorder(tree);
            Console.WriteLine();
        }

        public int MaxLeaf()
        {
            int maxVal = 0;
            bool found = false;
            Node.MaxLeaf(tree, ref maxVal, ref found);
            
            if (!found)
            {
                throw new Exception("Дерево пустое или не имеет листьев");
            }
            
            return maxVal;
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
                string[] data = line.Split(' ');
                
                foreach (string item in data)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        tree.Add(int.Parse(item));
                    }
                }
            }

            Console.WriteLine("Дерево (прямой обход):");
            tree.Preorder();

            int maxLeaf = tree.MaxLeaf();
            Console.WriteLine("Наибольшее из значений листьев: " + maxLeaf);

            File.WriteAllText("output.txt", "Наибольшее из значений листьев: " + maxLeaf);
            Console.WriteLine("Результат сохранен в output.txt");
        }
    }
}
