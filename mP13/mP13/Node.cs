using System;
namespace mP13
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> NextNode { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }
}
