using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Graphs;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Graphs
{
    public class DigraphTopologicalSortTests
    {
        [Fact]
        public void NodeWithMultipleChildrenIsProcessedFirst()
        {
            var graph = new Digraph<string>();

            var predecessor = graph.AddVertex("Predecessor");
            var successor1 = graph.AddVertex("Successor1");
            var successor2 = graph.AddVertex("Successor2");
            var successor3 = graph.AddVertex("Successor3");

            graph.AddArc(predecessor, successor1);
            graph.AddArc(predecessor, successor2);
            graph.AddArc(predecessor, successor3);

            var topologicalSort = graph.TopologicalSort();
            Compare(topologicalSort, new List<IDigraphVertex<string>>() {
                predecessor,
                successor1,
                successor2,
                successor3
            });
        }

        [Fact]
        public void NodeWithMultipleAncestorsIsProcessedLast()
        {
            var graph = new Digraph<string>();

            var predecessor = graph.AddVertex("Predecessor");
            var successor1 = graph.AddVertex("Successor1");
            var successor2 = graph.AddVertex("Successor2");
            var successor3 = graph.AddVertex("Successor3");

            graph.AddArc(predecessor, successor1);
            graph.AddArc(predecessor, successor2);
            graph.AddArc(predecessor, successor3);

            var topologicalSort = graph.TopologicalSort();
            Compare(topologicalSort, new List<IDigraphVertex<string>>() {
                predecessor,
                successor1,
                successor2,
                successor3
            });
        }

        private static void Compare<T>(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            actual.Should().BeEquivalentTo(expected,
                opts => opts.RespectingRuntimeTypes().WithStrictOrdering()
            );
        }
    }
}
