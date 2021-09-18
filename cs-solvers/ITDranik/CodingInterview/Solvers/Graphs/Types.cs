using System.Collections.Generic;

namespace ITDranik.CodingInterview.Solvers.Graphs
{
    public interface IDigraphVertex<T>
    {
        T Value { get; set; }
        IEnumerable<IDigraphVertex<T>> Successors { get; }
    }

    public interface IDigraph<T>
    {
        IEnumerable<IDigraphVertex<T>> Vertices { get; }

        IDigraphVertex<T> AddVertex(T value);
        void AddArc(IDigraphVertex<T> from, IDigraphVertex<T> to);
        IEnumerable<IDigraphVertex<T>> GetSuccessors(IDigraphVertex<T> vertex);
    }

    public class DigraphVertex<T> : IDigraphVertex<T>
    {
        public DigraphVertex(IDigraph<T> graph, T value)
        {
            _graph = graph;
            Value = value;
        }

        public T Value { get; set; }
        public IEnumerable<IDigraphVertex<T>> Successors => _graph.GetSuccessors(this);
        private readonly IDigraph<T> _graph;
    }

    public class Digraph<T> : IDigraph<T>
    {
        public Digraph()
        {
            _arcs = new();
        }

        public IEnumerable<IDigraphVertex<T>> Vertices => _arcs.Keys;

        public void AddArc(IDigraphVertex<T> tail, IDigraphVertex<T> head)
        {
            if (!_arcs.TryGetValue(tail, out var successors))
            {
                throw new System.ArgumentException(
                    "The vertex doesn't belong to the graph",
                    nameof(tail)
                );
            }

            if (!_arcs.ContainsKey(head))
            {
                throw new System.ArgumentException(
                    "The vertex doesn't belong to the graph",
                    nameof(head)
                );
            }

            successors.Add(head);
        }

        public IDigraphVertex<T> AddVertex(T value)
        {
            var vertex = new DigraphVertex<T>(this, value);
            _arcs.Add(vertex, new());

            return vertex;
        }

        public IEnumerable<IDigraphVertex<T>> GetSuccessors(IDigraphVertex<T> vertex)
        {
            if (!_arcs.TryGetValue(vertex, out var neighbors))
            {
                throw new System.ArgumentException(
                    "The vertex doesn't belong to the graph",
                    nameof(vertex)
                );
            }

            return neighbors;
        }

        private readonly Dictionary<IDigraphVertex<T>, List<IDigraphVertex<T>>> _arcs;
    }
}
