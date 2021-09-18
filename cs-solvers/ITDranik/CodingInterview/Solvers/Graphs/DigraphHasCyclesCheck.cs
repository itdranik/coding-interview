using System.Collections.Generic;

namespace ITDranik.CodingInterview.Solvers.Graphs
{
    public class DigraphHasCyclesCheck<T>
    {
        public static bool Execute(IDigraph<T> graph)
        {
            var marksByVertex = new Dictionary<IDigraphVertex<T>, VertexMark>();

            foreach (var vertex in graph.Vertices)
            {
                ReviewVertex(vertex, marksByVertex);
                if (marksByVertex[vertex] == VertexMark.UnderReview)
                {
                    return true;
                }
            }

            return false;
        }

        private static VertexMark ReviewVertex(
            IDigraphVertex<T> vertex,
            Dictionary<IDigraphVertex<T>, VertexMark> marksByVertex)
        {
            if (marksByVertex.TryGetValue(vertex, out var mark))
            {
                return mark;
            }

            marksByVertex[vertex] = VertexMark.UnderReview;

            foreach (var successor in vertex.Successors)
            {
                if (ReviewVertex(successor, marksByVertex) == VertexMark.UnderReview)
                {
                    return VertexMark.UnderReview;
                }
            }

            marksByVertex[vertex] = VertexMark.Reviewed;
            return VertexMark.Reviewed;
        }

        private enum VertexMark
        {
            UnderReview,
            Reviewed
        }
    }
}
