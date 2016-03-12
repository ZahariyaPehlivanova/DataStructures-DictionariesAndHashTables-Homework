namespace _04.OrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using global::OrderedSet;

    public class OrderedSet<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private Node<T> root;

        public OrderedSet()
        {
            this.Count = 0;
        }

        public int Count { get; set; }

        public void Add(T element)
        {
            Node<T> node = new Node<T>(element);

            if (this.root == null)
            {
                this.root = node;
                this.Count++;
            }
            else if (!this.Contains(element))
            {
                this.Insert(node, this.root, null);
                this.Count++;
            }
        }

        public bool Contains(T value)
        {
            return this.CheckIfTreeContainsElement(value, this.root);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.root.GetEnumerator();
        }

        public void Remove(T value)
        {
            Node<T> nodeToRemove = this.FindNode(value, this.root, null);

            if (nodeToRemove == null)
            {
                throw new ArgumentException("The node you want to remove doesn't exist!");
            }
            else
            {
                if (!nodeToRemove.Value.Equals(this.root.Value))
                {
                    if (nodeToRemove.Value.CompareTo(nodeToRemove.Parent.Value) > 0)
                    {
                        nodeToRemove.Parent.BigChild = null;
                    }
                    else
                    {
                        nodeToRemove.Parent.SmallChild = null;
                    }

                    nodeToRemove.Parent = null;
                }

                List<T> children = new List<T>();
                children = this.GetChildrenValues(nodeToRemove, children);
                this.Count -= children.Count;

                if (nodeToRemove.Value.Equals(this.root.Value))
                {
                    this.root = null;
                }

                foreach (var child in children)
                {
                    this.Add(child);
                }

                this.Count--;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool CheckIfTreeContainsElement(T value, Node<T> current)
        {
            if (current == null)
            {
                return false;
            }
            else if (current.Value.Equals(value))
            {
                return true;
            }
            else
            {
                if (value.CompareTo(current.Value) > 0)
                {
                    this.CheckIfTreeContainsElement(value, current.BigChild);
                }
                else
                {
                    this.CheckIfTreeContainsElement(value, current.SmallChild);
                }
            }

            return false;
        }

        private Node<T> FindNode(T value, Node<T> current, Node<T> found)
        {
            if (current == null)
            {
                return null;
            }
            else if (current.Value.Equals(value))
            {
                return current;
            }
            else
            {
                if (value.CompareTo(current.Value) > 0)
                {
                    found = this.FindNode(value, current.BigChild, found);
                }
                else
                {
                    found = this.FindNode(value, current.SmallChild, found);
                }
            }

            return found;
        }

        private List<T> GetChildrenValues(Node<T> current, List<T> list)
        {
            if (current.SmallChild != null)
            {
                list.Add(current.SmallChild.Value);
                list = this.GetChildrenValues(current.SmallChild, list);
            }

            if (current.BigChild != null)
            {
                list.Add(current.BigChild.Value);
                list = this.GetChildrenValues(current.BigChild, list);
            }

            return list;
        }

        private void Insert(Node<T> node, Node<T> currentElement, Node<T> parent)
        {
            if (currentElement == null)
            {
                if (node.Value.CompareTo(parent.Value) > 0)
                {
                    parent.BigChild = node;
                    parent.BigChild.Parent = parent;
                }
                else
                {
                    parent.SmallChild = node;
                    parent.SmallChild.Parent = parent;
                }

                return;
            }

            if (node.Value.CompareTo(currentElement.Value) > 0)
            {
                this.Insert(node, currentElement.BigChild, currentElement);
            }
            else
            {
                this.Insert(node, currentElement.SmallChild, currentElement);
            }
        }
    }
}