using System.Text;

namespace BidirectionalList
{
    /// <summary>
    /// Represents a bidirectional list.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the list.</typeparam>
    internal class BidirectionalList<T>
    {
        /// <summary>
        /// The first node of the list.
        /// </summary>
        public ListNode<T>? Head { get; private set; }
        /// <summary>
        /// The last node of the list.
        /// </summary>
        public ListNode<T>? Tail { get; private set; }

        /// <summary>
        /// Default constructor without parameters. 
        /// Initializes a new instance of the BidirectionalList class with nulls (no reference to other nodes).
        /// </summary>
        public BidirectionalList()
        {
            this.Head = null;
            this.Tail = null;
        }

        /// <summary>
        /// Adds a new node with the specified value to the end of the list.
        /// </summary>
        /// <param name="value">The value to be added to the list.</param>
        public void Add(T value)
        {
            if (this.Head == null && this.Tail == null)
            {
                AddFirstNode(value);
            }
            else if (this.Head != null && this.Tail != null)
            {
                AddNewTail(value);
            }
        }

        /// <summary>
        /// Adds a new node with the specified value in place of the specified index.
        /// Method will add new node to the beginning of the list if index is below 0
        /// or to the end if index is higher than the list length. 
        /// </summary>
        /// <param name="value">The value to be added to the list.</param>
        /// <param name="value">Index, where value should be added.</param>
        public void Add(T value, int index)
        {
            if (this.Head == null)
            {
                Console.WriteLine("Information: List is empty. Adding to the beggining of the list.");
                AddFirstNode(value);
            }
            else if (index <= 0)
            {
                if (index < 0) Console.WriteLine("Information: Negative index. Adding to the beggining of the list.");
                AddNewHead(value);
            }
            else
            {
                AddMidList(value, index);
            }
        }

        /// <summary>
        /// Removes the first node found from the list with the specified value.
        /// </summary>
        /// <param name="value">Searched value.</param>
        /// <remarks>
        /// Base logic: <see cref="RemoveNode"/>
        /// </remarks>
        public void Remove(T value)
        {
            RemoveNode(value);
        }

        /// <summary>
        /// Removes each node found from the list with the specified value.
        /// </summary>
        /// <param name="value">Searched value.</param>
        /// <remarks>
        /// Base logic: <see cref="RemoveNode"/>
        /// </remarks>
        public void RemoveAll(T value)
        {
            RemoveNode(value, true);
        }

        /// <summary>
        /// Base method used for removing nodes with specific value from the list.
        /// </summary>
        /// <param name="value">Searched value.</param>
        /// <param name="removeAll">Boolean parameter defining if method should remove one or more nodes. True - remove all nodes; False - remove first.</param>
        private void RemoveNode(T value, bool removeAll = false)
        {
            var currentNode = this.Head;

            while (currentNode != null)
            {
                var tempNextNode = currentNode.Next;

                if (currentNode.IsDataEqual(value))
                {
                    if (currentNode == this.Head && currentNode == this.Tail) ResetListEdges();
                    else if (currentNode == this.Head) RemoveHead();
                    else if (currentNode == this.Tail) RemoveTail();
                    else RemoveMiddleNode(currentNode);

                    if (removeAll == false) return;
                }
                currentNode = tempNextNode;
                if (currentNode == null) return;
            }
        }

        /// <summary>
        /// Finds an index of the first node found with provided value as data.
        /// </summary>
        /// <param name="value">Searched value.</param>
        public int Find(T value)
        {
            var currentNode = this.Head;
            int counter = 0;

            while (currentNode != null)
            {
                if (currentNode.IsDataEqual(value))
                {
                    return counter;
                }
                currentNode = currentNode.Next;
                counter++;
            }
            return -1;
        }

        /// <summary>
        /// Finds indexes of every node with provided value as data.
        /// </summary>
        /// <param name="value">Searched value.</param>
        public string[] FindAll(T value)
        {
            var currentNode = this.Head;
            int counter = 0;

            StringBuilder builder = new("");

            while (currentNode != null)
            {
                if (currentNode.IsDataEqual(value))
                {
                    builder.Append($"{counter},");
                }
                currentNode = currentNode.Next;
                counter++;
            }

            string[] results = builder.ToString().Split(",");

            return results;
        }

        /// <summary>
        /// Prints out each node of the list in order from beginning to end.
        /// </summary>
        public void PrintAll()
        {
            PrintList(this.Head, node => node.Next);
        }

        /// <summary>
        /// Prints out each node of the list in order from end to beginning.
        /// </summary>
        public void PrintAllReversed()
        {
            PrintList(this.Tail, node => node.Prev);
        }

        /// <summary>
        /// Base find method logic used to print out nodes in specific order.
        /// </summary>
        /// <param name="startNode">The first node from which we start the iteration.</param>
        /// <param name="GetNextNode">A function argument that defines the direction of iteration.</param>
        private void PrintList(ListNode<T>? startNode, Func<ListNode<T>, ListNode<T>?> GetNextNode)
        {
            var currentNode = startNode;

            while (currentNode != null)
            {
                Console.Write($"[{currentNode.Data}] ");
                currentNode = GetNextNode(currentNode);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Adds node at the specific index of the list. 
        /// Method will add new tail if index is out of bound.
        /// Method will iterate from second element since first element addition is handled in <see cref="Add(T, int)"/> method specificaly.
        /// </summary>
        /// <param name="value">The value to be added to the list.</param>
        /// <param name="index">Index, where value should be added.</param>
        private void AddMidList(T value, int index)
        {
            if (this.Head == null) return;

            ListNode<T>? wantedNode = this.Head.Next;

            for (int i = 1; i <= index; i++)
            {
                if (wantedNode == null)
                {
                    Console.WriteLine("Information: Index out of bound. Adding to the end of the list.");
                    AddNewTail(value);
                    break;
                }
                else if (i == index && wantedNode.Prev != null)
                {
                    var newNode = new ListNode<T>(value, wantedNode.Prev, wantedNode);
                    wantedNode.Prev.Next = newNode;
                    wantedNode.Prev = newNode;
                }
                else wantedNode = wantedNode.Next;
            }
        }

        /// <summary>
        /// Adds first node to the empty list.
        /// </summary>
        /// <param name="value">The value to be added to the list.</param>
        private void AddFirstNode(T value)
        {
            var newNode = new ListNode<T>(value, null, null);

            this.Head = newNode;
            this.Tail = newNode;
        }

        /// <summary>
        /// Adds new Head to the list.
        /// </summary>
        /// <param name="value">The value to be added to the list.</param>
        private void AddNewHead(T value)
        {
            var newHead = new ListNode<T>(value, null, this.Head);

            this.Head!.Prev = newHead;
            this.Head = newHead;
        }

        /// <summary>
        /// Adds new Tail to the list.
        /// </summary>
        /// <param name="value">The value to be added to the list.</param>
        private void AddNewTail(T value)
        {
            var newTail = new ListNode<T>(value, this.Tail, null);

            this.Tail!.Next = newTail;
            this.Tail = newTail;
        }

        /// <summary>
        /// Sets Head and Tail of the list to the nulls. (removes references to middle nodes)
        /// </summary>
        private void ResetListEdges () 
        {
            this.Head = null;
            this.Tail = null;
        }

        /// <summary>
        /// Removes Head from the list.
        /// Method can handle empty or one node lists.
        /// References from deleted node are set to null so it can be collected by garbage collector and deleted.
        /// </summary>
        private void RemoveHead() 
        {
            if (this.Head == null) return;
            else if (this.Head.Next == null)
            {
                ResetListEdges();
                return;
            }

            var nextHead = this.Head.Next;

            nextHead.Prev = null;
            this.Head.Next = null;
            this.Head = nextHead;
        }

        /// <summary>
        /// Removes Tail from the list.
        /// Method can handle empty or one node lists.
        /// References from deleted node are set to null so it can be collected by garbage collector and deleted.
        /// </summary>
        private void RemoveTail() 
        {
            if (this.Tail == null) return;
            else if(this.Tail.Prev == null)
            {
                ResetListEdges();
                return;
            }

            var nextTail = this.Tail.Prev;

            nextTail.Next = null;
            this.Tail.Prev = null;
            this.Tail = nextTail;
        }

        /// <summary>
        /// Removes provided node from the list.
        /// Method is reconnecting the list after deletion.
        /// References from deleted node are set to null so it can be collected by garbage collector and deleted.
        /// </summary>
        /// <param name="node">Node to be removed.</param>
        private void RemoveMiddleNode(ListNode<T> node) 
        {
            if (node.Next == null)
            {
                RemoveTail();
                return;
            }
            else if(node.Prev == null)
            {
                RemoveHead();
                return;
            }

            node.Next.Prev = node.Prev;
            node.Prev.Next = node.Next;

            node.Next = null;
            node.Prev = null;
        }
    }
}