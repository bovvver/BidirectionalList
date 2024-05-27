namespace Graph
{
    internal class Graph<T> where T : notnull
    {
        public Dictionary<T, GraphNode<T>> Nodes { get; set; } = [];

        public void AddNode(T value)
        {
            if (this.Nodes.ContainsKey(value)) return;

            this.Nodes[value] = new GraphNode<T>(value);
        }

        public void AddNode(T from, T value)
        {
            AddNode(value);
            AddEdge(from, value);
        }

        public void RemoveNode(T value)
        {
            if (!CheckForNodeExistance(value)) return;

            foreach (var node in this.Nodes[value].Neighbors.Values)
                node.Neighbors.Remove(value);

            this.Nodes.Remove(value);
        }

        public void AddEdge(T from, T to)
        {
            if(!CheckForNodeExistance(from) || !CheckForNodeExistance(to)) return;

            this.Nodes[from].Neighbors[to] = this.Nodes[to];
            this.Nodes[to].Neighbors[from] = this.Nodes[from];
        }

        public void RemoveEdge(T from, T to)
        {
            if(!CheckForNodeExistance(from) || !CheckForNodeExistance(to)) return;

            this.Nodes[from].Neighbors.Remove(to);
            this.Nodes[to].Neighbors.Remove(from);
        }

        public void BFS(T startingNode, T searchedValue)
        {
            if (!CheckForNodeExistance(startingNode)) return;

            var searchQueue = new Queue<T>();
            var visited = new HashSet<T>();

            searchQueue.Enqueue(startingNode);
            visited.Add(startingNode);

            while (searchQueue.Count > 0)
            {
                T currentNode = searchQueue.Dequeue();
                Console.Write($"{currentNode} ");

                if (CheckIfSearchedValue(currentNode, searchedValue)) return;

                foreach (var neighbor in this.Nodes[currentNode].Neighbors.Keys)
                {
                    if (!visited.Contains(neighbor))
                    {
                        searchQueue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
            Console.WriteLine($"- Node {searchedValue} not found.");
        }

        public void DFS(T startingNode, T searchedValue)
        {
            if (!CheckForNodeExistance(startingNode)) return;

            var searchStack = new Stack<T>();
            var visited = new HashSet<T>();

            searchStack.Push(startingNode);

            while(searchStack.Count > 0)
            {
                T currentNode = searchStack.Pop();

                if (!visited.Contains(currentNode))
                {
                    visited.Add(currentNode);
                    Console.Write($"{currentNode} ");

                    if (CheckIfSearchedValue(currentNode, searchedValue)) return;
                }

                foreach (var neighbor in this.Nodes[currentNode].Neighbors.Keys)
                {
                    if (!visited.Contains(neighbor))
                    {
                        searchStack.Push(neighbor);
                    }
                }
            }
            Console.WriteLine($"- Node {searchedValue} not found.");
        }

        private bool CheckForNodeExistance (T nodeKey) 
        {
            if (this.Nodes.ContainsKey(nodeKey)) return true;

            Console.WriteLine($"Node {nodeKey} doesn't exist in a graph.");
            return false;
        }

        private static bool CheckIfSearchedValue (T node, T searchedValue) 
        {
            if (!node.Equals(searchedValue)) return false;

            Console.WriteLine();
            return true;
        }
    }
}
