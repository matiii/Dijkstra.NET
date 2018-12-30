using System;

namespace Dijkstra.NET.Graph
{
    public class Node<T, TEdgeCustom>: INode<T, TEdgeCustom> where TEdgeCustom: IEquatable<TEdgeCustom>
    {
        private Edge<T, TEdgeCustom>[] _children;

        public Node(uint key, T item)
        {
            Key = key;
            Item = item;
            _children = new Edge<T, TEdgeCustom>[5];
        }

        public uint Key { get; }

        public T Item { get; }

        public int ChildrenCount { get; internal set; }

        internal void AddChild(in Edge<T, TEdgeCustom> edge)
        {
            if (_children.Length == ChildrenCount)
            {
                int newSize = _children.Length;

                if (ChildrenCount < NodeConstants.MaxSize)
                {
                    newSize *= 2;
                }
                else
                {
                    long bigSize = (long) (newSize * 1.5);

                    newSize = bigSize < Int32.MaxValue ? (int)bigSize : Int32.MaxValue;
                }

                Array.Resize(ref _children, newSize);
            }

            _children[ChildrenCount] = edge;
            ChildrenCount++;
        }

        public void EachChild(ChildAction<T, TEdgeCustom> action)
        {
            for (int i = 0; i < ChildrenCount; i++)
            {
                ref Edge<T, TEdgeCustom> e = ref _children[i];

                action(in e);
            }
        }
    }
}
