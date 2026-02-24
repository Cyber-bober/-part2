/*Вычислить среднее арифметическое значение всех элементов, хранящихся в списке. После
каждого элемента из списка, значение которого меньше среднего арифметического,
вставить элемент со значением х. */
using System;
using System.IO;

namespace Example
{
  
    public class List
    {
        public class Node { public int Inf; public Node Next; }
        private Node head;
        private Node tail;
        private Node temp;
        public List()
        {
            head = null;
            tail = null;
        }
        public void Add(object nodeInfo)
        {
            Node n = new Node(nodeInfo);
            if (head == null) head = tail = n;
            else { n.Next = head; head = n; }
        }

        public double Avg()
        {
            if (head == null) return 0;
            double s = 0; int k = 0;
            temp = head;
            while (temp != null) { s += temp.Inf; k++; temp = temp.Next; }
            return s / k;
        }

        public void InsertAfterLess(int x, double avg)
        {
            temp = head;
            while (temp != null)
            {
                if (temp.Inf < avg)
                {
                    Node n = new Node { Inf = x, Next = temp.Next };
                    temp.Next = n;
                    if (temp == tail) tail = n;
                    temp = n.Next;
                }
                else temp = temp.Next;
            }
        }

        public void AddEnd(object nodeInfo)
        {
            Node n = new Node(nodeInfo);
            if (head == null)
            {
                head = n;
                tail = n;
            }
            else
            {
                n.Next = head;
                head = n;
            }
        }
        public void Show()
        {
            Node n = head; 
            while (n != null) 
            {   
                Console.Write("{0} ", n.Inf);
                n = n.Next;
            }
        }
    }



    public class Program
    {
        static void Main()
        {
            List list = new List();
            using (StreamReader fileIn = new StreamReader("C:/Users/bykovvd/Desktop/Новая папка/20/input.txt"))
            {
                string line = fileIn.ReadToEnd();
                string[] data = line.Split(' ');
                foreach (string item in data)
                {
                    list.AddEnd(int.Parse(item));
                }
            }

            Console.Write("x="); int x = int.Parse(Console.ReadLine());
            list.InsertAfterLess(x, list.Avg());

            using (StreamWriter fileOut = new StreamWriter("C:/Users/bykovvd/Desktop/Новая папка/20/output.txt"))
            {
                string line = fileOut.Show();

            }
                Console.ReadKey();
        }
    }
}
