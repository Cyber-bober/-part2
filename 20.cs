/*Вычислить среднее арифметическое значение всех элементов, хранящихся в списке. После
каждого элемента из списка, значение которого меньше среднего арифметического,
вставить элемент со значением х. */
using System;
using System.IO;

namespace Example
{
    public class List
    {
        public class Node 
        { 
            public int Inf; 
            public Node Next; 
        }
        
        private Node head;
        private Node tail;
        
        public List()
        {
            head = null;
            tail = null;
        }

        public void AddEnd(int value)
        {
            Node n = new Node();
            n.Inf = value;
            
            if (head == null)
            {
                head = n;
                tail = n;
            }
            else
            {
                tail.Next = n;  
                tail = n;       
            }
        }

        public double Avg()
        {
            if (head == null) return 0;
            double s = 0; 
            int k = 0;
            Node temp = head;
            while (temp != null) 
            { 
                s += temp.Inf; 
                k++; 
                temp = temp.Next; 
            }
            return s / k;
        }

        public void InsertAfterLess(int x, double avg)
        {
            Node temp = head;
            while (temp != null)
            {
                if (temp.Inf < avg)
                {
                    Node n = new Node { Inf = x, Next = temp.Next };
                    temp.Next = n;
                    if (temp == tail) 
                        tail = n;
                    temp = n.Next;
                }
                else 
                    temp = temp.Next;
            }
        }

        public string Show()
        {
            string show = "";
            Node n = head; 
            while (n != null) 
            {   
                show += n.Inf + " ";
                n = n.Next;
            }
            return show;
        }
    }

    class Program
    {
        static void Main()
        {
            List L = new List();
            
            string[] data = File.ReadAllText("input.txt").Split(
                new char[] { ' ', '\n', '\r' }, 
                StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string s in data)
            {
                L.AddEnd(int.Parse(s));
            }
            
            string original = L.Show();
            
            Console.WriteLine("Исходный список:");
            Console.WriteLine(original);
            
            Console.Write("x = ");
            int x = int.Parse(Console.ReadLine());
            
            double avg = L.Avg();
            Console.WriteLine("Среднее арифметическое: " + avg);
            
            L.InsertAfterLess(x, avg);
            
            Console.WriteLine("Итоговый список:");
            Console.WriteLine(L.Show());
            
            File.WriteAllText("output.txt", original + "\n" + L.Show());
            
            Console.WriteLine("Результат сохранен в output.txt");
        }
    }
}
