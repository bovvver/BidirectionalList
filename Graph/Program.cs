namespace Graph;

class Program
{
    static void Main(string[] args)
    {
        var graph = new Graph<int>();

        graph.AddNode(1);
        graph.AddNode(1, 2);
        graph.AddNode(1, 3);
        graph.AddNode(2, 3);

        graph.AddNode(2, 4);
        graph.AddNode(2, 5);

        graph.AddNode(3, 6);
        graph.AddNode(3, 7);
        graph.AddNode(7, 8);
        graph.AddNode(6, 8);

        graph.DFS(1, 4);
        graph.BFS(1, 4);
    }
}