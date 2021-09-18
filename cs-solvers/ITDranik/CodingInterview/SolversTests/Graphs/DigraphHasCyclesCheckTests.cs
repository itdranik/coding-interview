using System;
using System.Collections.Generic;
using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Graphs;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Graphs
{
    public class DigraphHasCyclesCheckTests
    {
        [Fact]
        public void EmptyGraphDoesNotHaveCycle()
        {
            var graph = new Digraph<string>();

            var hasCycles = graph.HasCycles();
            hasCycles.Should().Be(false);
        }

        [Fact]
        public void GraphWithSingleNodeDoesNotHaveCycle()
        {
            var graph = new Digraph<string>();
            graph.AddVertex("Single");

            var hasCycles = graph.HasCycles();
            hasCycles.Should().Be(false);
        }

        [Fact]
        public void GraphWithCycle()
        {
            var graph = new Digraph<string>();

            var v1 = graph.AddVertex("V1");
            var v2 = graph.AddVertex("V2");
            var v3 = graph.AddVertex("V3");

            graph.AddArc(v1, v2);
            graph.AddArc(v2, v3);
            graph.AddArc(v3, v1);

            var hasCycles = graph.HasCycles();
            hasCycles.Should().Be(true);
        }

        [Fact]
        public void GraphWithoutCycle()
        {
            var graph = new Digraph<string>();

            var predecessor = graph.AddVertex("Predecessor");
            var middle1 = graph.AddVertex("Middle1");
            var middle2 = graph.AddVertex("Middle2");
            var successor = graph.AddVertex("Successor");

            graph.AddArc(predecessor, middle1);
            graph.AddArc(predecessor, middle2);
            graph.AddArc(middle1, successor);
            graph.AddArc(middle2, successor);

            var hasCycles = graph.HasCycles();
            hasCycles.Should().Be(false);
        }

        [Fact]
        public void WhenAnyComponentHasCycle_GraphHasCycles()
        {
            var graph = new Digraph<string>();
            graph.AddVertex("Component1_v1");

            var c2v1 = graph.AddVertex("Component2_v1");
            var c2v2 = graph.AddVertex("Component2_v2");

            graph.AddArc(c2v1, c2v2);
            graph.AddArc(c2v2, c2v1);

            var hasCycles = graph.HasCycles();
            hasCycles.Should().Be(true);
        }
    }
}
