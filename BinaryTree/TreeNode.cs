namespace BinaryTree
{
    internal class TreeNode
    {
        public float Value {  get; set; }
        public TreeNode? Left { get; set;}
        public TreeNode? Right { get; set;}

        public TreeNode(float value, TreeNode? left, TreeNode? right)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public TreeNode(float value): this (value, null, null)
        {}
    }
}