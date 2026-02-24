/*Вычислить среднее арифметическое значение всех элементов, хранящихся в списке. После
каждого элемента из списка, значение которого меньше среднего арифметического,
вставить элемент со значением х. */
using System;
using System.IO;

class Node 
{ 
    public int inf;      
    public Node next;     
    
    public Node(int data)
    {
        inf = data;
        next = null;
    }
}

class List
{
    private Node head;   
    private Node tail;    
    private Node temp;    
    
    public List()
    {
        head = null;
        tail = null;
        temp = null;
    }
    
    public void AddEnd(int nodeInfo)
    {
        Node r = new Node(nodeInfo);
        
        if (head == null)           
        {
            head = r;      
            tail = r;               
        }
        else                         
        {
            tail.next = r;       
            tail = r;                   
        }
    }
    
    public int TakeBegin()
    {
        if (head == null)
        {
            throw new Exception("Список пуст");
        }
        else
        {
            Node r = head;      
            head = head.next;    
            
            if (head == null)           
            {
                tail = null; 
            }
            
            return r.inf;           
        }
    }
    
    public bool IsEmpty
    {
        get
        {
            return head == null;
        }
    }
    
    public Node Find(int key)
    {
        Node r = head;                  
        
        while (r != null)            
        {
            if (r.inf == key)         
            {
                break;                    
            }
            else
            {
                r = r.next;               
            }
        }
        
        return r;                       
    }
    
    public void Insert(int key, int item)
    {
        Node r = Find(key);           
        
        if (r != null)                    
        {
            Node p = new Node(item);      
            p.next = r.next;               
            r.next = p;                     
            
            if (r == tail)                   
            {
                tail = p;                      
            }
        }
    }
    
    public void Delete(int key)
    {
        if (head == null)                    
        {
            throw new Exception("Список пуст");
        }
        else
        {
            if (head.inf == key)              
            {
                head = head.next;               
                
                if (head == null)             
                {
                    tail = null;                 
                }
            }
            else
            {
                Node r = head;                  
                
                while (r.next != null)          
                {
                    if (r.next.inf == key)        
                    {
                        r.next = r.next.next;    
                        
                        if (r.next == null)        
                        {
                            tail = r;            
                        }
                        
                        break;                      
                    }
                    else
                    {
                        r = r.next;                  
                    }
                }
            }
        }
    }
    
    public double Avg()
    {
        if (head == null) return 0;
        
        double sum = 0;
        int count = 0;
        temp = head;
        
        while (temp != null)
        {
            sum += temp.inf;
            count++;
            temp = temp.next;
        }
        
        return sum / count;
    }
    
    public void InsertAfterLess(int x, double avg)
    {
        temp = head;
        
        while (temp != null)
        {
            if (temp.inf < avg)
            {
                Node n = new Node(x);
                n.next = temp.next;
                temp.next = n;
                
                if (temp == tail)           
                {
                    tail = n;                
                }
                
                temp = n.next;         
            }
            else
            {
                temp = temp.next;            
            }
        }
    }
    

    public string Show()
    {
        string result = "";
        Node r = head;                       
        
        while (r != null)                     
        {
            result += r.inf + " ";              
            r = r.next;                          
        }
        
        return result;
    }
    
    public void Print()
    {
        Node r = head;
        
        while (r != null)
        {
            Console.Write(r.inf + " ");
            r = r.next;
        }
        
        Console.WriteLine();
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
        L.Print();
        
        Console.Write("x = ");
        int x = int.Parse(Console.ReadLine());
        
        double avg = L.Avg();
        Console.WriteLine("Среднее арифметическое: " + avg);
        
        L.InsertAfterLess(x, avg);
        
        Console.WriteLine("Итоговый список:");
        L.Print();
        
        File.WriteAllText("output.txt", original + "\n" + L.Show());
        
        Console.WriteLine("Результат сохранен в output.txt");
    }
}
