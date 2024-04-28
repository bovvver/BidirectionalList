namespace BidirectionalList
{
    /// <summary>
    /// Object representing single node inside a BidirectionalList.
    /// </summary>
    /// <typeparam name="T">Type of data stored in the node.</typeparam>
    /// <remarks>
    /// BidirectionalList implementation: <see cref="BidirectionalList{T}"/>
    /// </remarks>
    internal class ListNode<T>
    {
        /// <summary>
        /// Data stored in the node.
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Reference to the previous node in the list.
        /// </summary>
        public ListNode<T>? Prev { get; set; }
        /// <summary>
        /// Reference to the next node in the list.
        /// </summary>
        public ListNode<T>? Next {  get; set; }

        /// <summary>
        /// A class constructor that initializes the object with values in sequence: data, reference to previous node, reference to next node.
        /// </summary>
        /// <param name="data">Data stored in the node.</param>
        /// <param name="prev">Reference to the previous node in the list.</param>
        /// <param name="next">Reference to the next node in the list.</param>
        public ListNode(T data, ListNode<T>? prev, ListNode<T>? next)
        {
            Data = data;
            this.Prev = prev;
            this.Next = next;
        }

        /// <summary>
        /// Checks if node data is equal to the provided value.
        /// </summary>
        /// <param name="data">Data to compare.</param>
        /// <returns>True, if data parameter is equal to node data field; otherwise - False.</returns>
        public bool IsDataEqual(T data) {
            if (this.Data == null) return false;
            return this.Data.Equals(data);
        }
    }
}
