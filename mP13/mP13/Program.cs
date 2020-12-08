using System;

namespace mP13
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>((num1, num2) => num1 < num2);
            list.PrintList(node => Console.WriteLine(node.Data));

            list.AddNode(9);
            list.AddNode(10);
            list.AddNode(4);
            list.AddNode(1);
            list.AddNode(2);
            list.AddNode(3);
            Console.WriteLine("Unsorted List: ");
            list.PrintList(node => Console.WriteLine(node.Data));


        }
    }
}
