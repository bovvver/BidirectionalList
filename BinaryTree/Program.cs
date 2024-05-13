namespace BinaryTree;

class Program
{
    static void Main(string[] args)
    {
        var tree = new BinaryTree();

        tree.Add(100);
        tree.Add(90);

        tree.Add(125);
        tree.Add(115);

        tree.Remove(100);

        tree.PrintPreorder();
        tree.PrintInorder();
        tree.PrintPostorder();
    }
}