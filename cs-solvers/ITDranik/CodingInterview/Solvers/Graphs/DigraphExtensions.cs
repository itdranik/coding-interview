using System.Collections.Generic;

namespace ITDranik.CodingInterview.Solvers.Graphs
{
    public static class DigraphExtensions
    {
        public static bool HasCycles<T>(this IDigraph<T> graph)
        {
            return DigraphHasCyclesCheck<T>.Execute(graph);
        }

        public static IEnumerable<IDigraphVertex<T>> TopologicalSort<T>(this IDigraph<T> graph)
        {
            return DigraphTopologicalSort<T>.Execute(graph);
        }
    }
}
