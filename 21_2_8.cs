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

            public static void Preorder(Node t)
            {
                if (t != null)
                {
                    Console.Write("{0} ", t.inf);
                    Preorder(t.left);
                    Preorder(t.right);
                }
            }

            public static void NodeHeight(Node t)
            {
                if (t != null)
                {
                    int height = GetHeight(t);
                    Console.WriteLine("Узел {0}: высота = {1}", t.inf, height);
                    NodeHeight(t.left);
                    NodeHeight(t.right);
                }
            }

            private static int GetHeight(Node t)
            {
                if (t == null)
                {
                    return -1;
                }
                
                int leftHeight = GetHeight(t.left);
                int rightHeight = GetHeight(t.right);

                return 1 + Math.Max(leftHeight, rightHeight);
            }

            public static void HeigthTree(Node t, ref int count, ref int height)
            {
                if (t != null)
                {
                    if (count > height)
                    {
                        height = count;
                    }
                    count++;
                    HeigthTree(t.left, ref count, ref height);
                    HeigthTree(t.right, ref count, ref height);
                    count--;
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

        public void NodeHeight()
        {
            Node.NodeHeight(tree);
        }

        public int HeigthTree()
        {
            int count = 0;
            int height = 0;
            Node.HeigthTree(tree, ref count, ref height);
            return height;
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

            Console.WriteLine("\nВысота для каждого узла дерева:");
            tree.NodeHeight();

            Console.WriteLine("\nВысота всего дерева: " + tree.HeigthTree());

            File.WriteAllText("output.txt", "Высота всего дерева: " + tree.HeigthTree());
            Console.WriteLine("\nРезультат сохранен в output.txt");
        }
    }
}
