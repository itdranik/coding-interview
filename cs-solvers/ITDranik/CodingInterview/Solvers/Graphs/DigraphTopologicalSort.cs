using System.Collections.Generic;

namespace ITDranik.CodingInterview.Solvers.Graphs
{
    public class DigraphTopologicalSort<T>
    {
        public static IEnumerable<IDigraphVertex<T>> Execute(IDigraph<T> graph)
        {
            var indegreeByVertex = CalculateIndegreeByVertex(graph);
            return TopologicalSort(graph, indegreeByVertex);
        }

        private static Dictionary<IDigraphVertex<T>, int> CalculateIndegreeByVertex(
            IDigraph<T> graph)
        {
            var indegreeByVertex = new Dictionary<IDigraphVertex<T>, int>();

            foreach (var vertex in graph.Vertices)
            {
                indegreeByVertex[vertex] = 0;
            }

            foreach (var vertex in graph.Vertices)
            {
                foreach (var successor in vertex.Successors)
                {
                    indegreeByVertex[successor]++;
                }
            }

            return indegreeByVertex;
        }

        private static List<IDigraphVertex<T>> TopologicalSort(
            IDigraph<T> graph,
            Dictionary<IDigraphVertex<T>, int> indegreeByVertex)
        {
            var traverseQueue = new Queue<IDigraphVertex<T>>();

            foreach (var vertex in graph.Vertices)
            {
                if (indegreeByVertex[vertex] == 0)
                {
                    traverseQueue.Enqueue(vertex);
                }
            }

            var topologicalSort = new List<IDigraphVertex<T>>();

            while (traverseQueue.Count > 0)
            {
                var vertex = traverseQueue.Dequeue();
                topologicalSort.Add(vertex);

                foreach (var successor in vertex.Successors)
                {
                    indegreeByVertex[successor]--;
                    if (indegreeByVertex[successor] == 0)
                    {
                        traverseQueue.Enqueue(successor);
                    }
                }
            }

            return topologicalSort;
        }
    }
}
