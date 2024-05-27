namespace Graph
{
    internal class GraphNode<T> where T : notnull
    {
        public T Value { get; set; }
        public Dictionary<T, GraphNode<T>> Neighbors { get; set; } = [];

        public GraphNode(T value) 
        { 
            Value = value;
        }
    }
}