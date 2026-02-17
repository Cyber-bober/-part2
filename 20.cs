using System;
using System.IO;

class Node { public int inf; public Node next; }
class List
{
    Node head, tail, temp;
    
    public void Add(int x) 
    { 
        Node n = new Node { inf = x, next = null };
        if (head == null) head = tail = n;
        else { tail.next = n; tail = n; }
    }
    
    public double Avg()
    {
        if (head == null) return 0;
        double s = 0; int k = 0;
        temp = head;
        while (temp != null) { s += temp.inf; k++; temp = temp.next; }
        return s / k;
    }
    
    public void InsertAfterLess(int x, double avg)
    {
        temp = head;
        while (temp != null)
        {
            if (temp.inf < avg)
            {
                Node n = new Node { inf = x, next = temp.next };
                temp.next = n;
                if (temp == tail) tail = n;
                temp = n.next;
            }
            else temp = temp.next;
        }
    }
    
    public string Show()
    {
        string r = "";
        for (temp = head; temp != null; temp = temp.next)
            r += temp.inf + " ";
        return r;
    }
}

class Program
{
    static void Main()
    {
        List L = new List();
        string[] d = File.ReadAllText("input.txt").Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string s in d) L.Add(int.Parse(s));
        
        string orig = L.Show();
        Console.Write("x="); int x = int.Parse(Console.ReadLine());
        L.InsertAfterLess(x, L.Avg());
        
        File.WriteAllText("output.txt", orig + "\n" + L.Show());
    }
}
