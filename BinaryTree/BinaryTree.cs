namespace BinaryTree
{
    internal class BinaryTree
    {
        public TreeNode? Root { get; private set; }

        public BinaryTree()
        {
            this.Root = null;
        }

        public void Add(float value)
        {
            var newNode = new TreeNode(value);

            if (this.Root == null) this.Root = newNode;
            else
            {
                var currentLeaf = this.Root;
                TreeNode? parent = null;

                while (currentLeaf != null)
                {
                    parent = currentLeaf;

                    if (currentLeaf.Value == value)
                    {
                        Console.WriteLine($"Value {value} already exists in the tree.");
                        return;
                    }
                    else if (currentLeaf.Value > value) currentLeaf = currentLeaf.Left;
                    else if (currentLeaf.Value < value) currentLeaf = currentLeaf.Right;
                }

                if (parent!.Value > value) parent.Left = newNode;
                else parent.Right = newNode;
            }
        }

        public void Remove(float value)
        {
            if (this.Root == null) return;

            TreeNode? currentNode = this.Root;
            TreeNode? parent = null;

            while (currentNode != null && currentNode.Value != value)
            {
                parent = currentNode;

                if (currentNode.Value < value)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    currentNode = currentNode.Left;
                }
            }

            if (currentNode == null) return;

            if (currentNode.Left == null && currentNode.Right == null)
            {
                RemoveNodeWithoutChildren(parent, currentNode);
            }
            else if (currentNode.Left != null && currentNode.Right != null)
            {
                RemoveNodeWithDoubleChildren(currentNode);
            }
            else
            {
                RemoveNodeWithOneChildren(parent, currentNode);
            }
        }

        private void RemoveNodeWithoutChildren(TreeNode? parent, TreeNode node)
        {
            if (parent != null)
            {
                UpdateSuccessor(parent, node);
            }
            else
            {
                this.Root = null;
            }
        }

        private void RemoveNodeWithOneChildren(TreeNode? parent, TreeNode node)
        {
            if (node.Left != null && node.Right == null)
            {
                if (parent != null)
                {
                    UpdateSuccessor(parent, node, node.Left);
                }
                else
                {
                    this.Root = node.Left;
                }
            }
            else if (node.Left == null && node.Right != null)
            {
                if (parent != null)
                {
                    UpdateSuccessor(parent, node, node.Right);
                }
                else
                {
                    this.Root = node.Right;
                }
            }
        }

        private void RemoveNodeWithDoubleChildren(TreeNode node)
        {
            TreeNode? successor = node.Right;
            TreeNode? successorParent = node;

            while (successor!.Left != null)
            {
                successorParent = successor;
                successor = successor.Left;
            }

            node.Value = successor.Value;

            if (successor.Right != null && node == successorParent)
            {
                successorParent.Right = successor.Right;
            }
            else if (successor.Right != null)
            {
                successorParent.Left = successor.Right;
            }
            else if (successor.Right == null && node == successorParent)
            {
                successorParent.Right = null;
            }
            else
            {
                successorParent.Left = null;
            }
        }

        private void UpdateSuccessor(TreeNode parent, TreeNode checkedNode, TreeNode? newValue = null)
        {
            if (parent.Left == checkedNode)
            {
                parent.Left = newValue;
            }
            else
            {
                parent.Right = newValue;
            }
        }


        public void PrintInorder()
        {
            Console.Write("INORDER: ");
            PrintInorderHelper(this.Root);
            Console.WriteLine();
        }

        private void PrintInorderHelper(TreeNode? node)
        {
            if (node == null) return;

            PrintInorderHelper(node.Left);
            Console.Write(node.Value + " ");
            PrintInorderHelper(node.Right);
        }

        public void PrintPreorder()
        {
            Console.Write("PREORDER: ");
            PrintPreorderHelper(this.Root);
            Console.WriteLine();
        }

        private void PrintPreorderHelper(TreeNode? node)
        {
            if (node == null) return;

            Console.Write(node.Value + " ");
            PrintPreorderHelper(node.Left);
            PrintPreorderHelper(node.Right);
        }

        public void PrintPostorder()
        {
            Console.Write("POSTORDER: ");
            PrintPostorderHelper(this.Root);
            Console.WriteLine();
        }

        private void PrintPostorderHelper(TreeNode? node)
        {
            if (node == null) return;

            PrintPostorderHelper(node.Left);
            PrintPostorderHelper(node.Right);
            Console.Write(node.Value + " ");
        }
    }
}
