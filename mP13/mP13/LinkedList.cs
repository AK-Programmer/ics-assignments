using System;
namespace mP13
{
    public class LinkedList<T>
    {
        private int listLength;
        private  Comparison greaterThan;
        Node<T> head;

        public LinkedList(Comparison greaterThan)
        {
            listLength = 0;
            this.greaterThan = greaterThan;
        }

        public void AddNode(T data)
        {

            Node<T> node = new Node<T>(data);

            Node<T> lastNode = GetTail();

            if(lastNode == null)
            {
                head = node;
            }
            else
            {
                Node<T> currentNode = head;
                if(greaterThan(head.Data, data))
                {
                    node.NextNode = head;
                    head = node;
                }
                else
                {
                    for(int i = 0; i < listLength - 1; i++)
                    {
                        if(greaterThan(currentNode.NextNode.Data, node.Data))
                        {
                            node.NextNode = currentNode.NextNode;
                            currentNode.NextNode = node;
                            listLength++;
                            return;
                        }

                        currentNode = currentNode.NextNode;
                    }

                    lastNode.NextNode = node;
                }
            }

            listLength++;
        }


        public bool Delete(Func<Node<T>, bool> query)
        {
            Node<T> nodeBefore = QueryList(node => query(node.NextNode));
            if(nodeBefore != null)
            {
                nodeBefore.NextNode = nodeBefore.NextNode.NextNode;
                listLength--;
                return true;
            }

            return false;
        }

        public Node<T> QueryList(Func<Node<T>,bool> queryCondition)
        {
            Node<T> currentNode = head;

            for(int i = 0; i < listLength; i++)
            {
                if(queryCondition(currentNode))
                {
                    return currentNode;
                }

                currentNode = currentNode.NextNode;
            }

            return null;
        }

        public void PrintList(Action<Node<T>> printNode)
        {
            Node<T> currentNode = head;
            for(int i = 0; i < listLength; i++)
            {
                if(currentNode == null)
                {
                    break;
                }

                printNode(currentNode);
                currentNode = currentNode.NextNode;
            }
            Console.WriteLine("");
        }

        public Node<T> GetTail()
        {
            return QueryList((node) => node.NextNode == null || node == null);
        }

        public int GetLength()
        {
            return listLength;
        }

        public delegate bool Comparison(T data1, T data2);
    }


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
