using System;
namespace mP13
{
    public class LinkedList<T>
    {
        int listLength;
        Node<T> head;
        Comparison greaterThan;

        public LinkedList(Comparison greaterThan)
        {
            listLength = 0;
            this.greaterThan = greaterThan;
        }

        public void AddNode(T data)
        {

            Node<T> node = new Node<T>(data);

            Node<T> lastNode = QueryList( n => n.NextNode == null || n == null);

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


        public void Delete(Node<T> node)
        {
            Node<T> nodeBefore = QueryList(n => n.NextNode == node);
            if(nodeBefore != null)
            {
                nodeBefore.NextNode = node.NextNode;
                listLength--;
            }
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

        public delegate bool Comparison(T data1, T data2);


    }
}
