using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
    public class Node
    {
        public Node Left;
        public Node Right;
        public Node ParentNode;
        public int Id;
        public string Name;
        public int? Lft = null;
        public int? Rgt = null;
        public Node(int Id,string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
    public class Tree
    {
        public Node Root { get; set; }
        public Tree(Node root)
        {
            this.Root = root;
        }
        public void TraverseByStack()
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(Root);
            int counter = 1;
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current == null) continue;
                Console.WriteLine(current.Name);
                current.Lft = counter++;
                stack.Push(current.Right);
                stack.Push(current.Left);
            }
        }
        public static void TraverseByRecursion(Node node, int counter = 1)
        {
            if (node == null) return;
        
            Console.WriteLine(node.Name);

            if (node.Lft == null)
            {
                node.Lft = counter++;
            }
            else if (node.Lft != null && node.Rgt == null)
            {
                node.Rgt = counter++;
            }

            // if node is root, lft and rgt is calculated
            if (node.ParentNode == null && node.Lft != null && node.Rgt != null) return;
            // check if node leaf
            if ((node.Left == null && node.Right == null))
            {
                node.Rgt = counter++;
            }



            if (node.ParentNode != null  &&  node.Rgt != null && node.Lft != null)
            {
                
                //go to right  sibling if sibling not visited
                if(node.ParentNode.Right!=null && node.ParentNode.Right.Rgt==null && node.ParentNode.Right.Lft == null)
                {
                    TraverseByRecursion(node.ParentNode.Right, counter);
                }
                else
                {
                    TraverseByRecursion(node.ParentNode, counter);

                }
            }
            else
            {
                TraverseByRecursion(node.Left, counter);
                TraverseByRecursion(node.Right, counter);
            }
            //if(node.Lft!=null && )


        }
    }
    class InputData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<InputData>();
            list.Add(new InputData { Id = 1, Name = "a", ParentId = null });
            list.Add(new InputData { Id = 2, Name = "b", ParentId = 1 });
            list.Add(new InputData { Id = 3, Name = "c", ParentId = 1});
            list.Add(new InputData { Id = 4, Name = "d", ParentId = 2});
            list.Add(new InputData { Id = 5, Name = "e", ParentId = 3});
            list.Add(new InputData { Id = 6, Name = "f", ParentId = 3});
            list.Add(new InputData { Id = 7, Name = "g", ParentId = 2});
            list.Add(new InputData { Id = 8, Name = "i", ParentId = 4});
            list.Add(new InputData { Id = 9, Name = "j", ParentId = 8});
            var nodes = new List<Node>();
            Node? root=null;
            foreach (var inputData in list)
            {
                var node = new Node(inputData.Id, inputData.Name);
                nodes.Add(node);
                if (inputData.ParentId == null)
                {
                    root = node;
                }
            }
            var tree = new Tree(root);

            foreach (var inputData in list)
            {
                var node = nodes.Where(e => e.Id == inputData.Id).FirstOrDefault();
                var parentNode = nodes.Where(e => e.Id == inputData.ParentId).FirstOrDefault();
                node.ParentNode = parentNode;
                if (parentNode == null)
                {
                    continue; //skip on root node
                }
                if (parentNode.Left == null)
                    parentNode.Left = node;
                else if (parentNode.Right == null)
                    parentNode.Right = node;
            }

            Tree.TraverseByRecursion(tree.Root,1);
            foreach (var node in nodes)
            {
                Console.WriteLine(string.Format("{0} : {1} : {2}", node.Name, node.Lft, node.Rgt));
            }
        }
    }
}
