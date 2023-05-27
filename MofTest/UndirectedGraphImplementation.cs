using SampleNamespace;
using SampleNamespace.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleNamespace.Internal
{
    class UndirectedGraphImplementation : UndirectedGraphImplementationBase
    {
        public override void UndirectedGraph_AddPair(UndirectedGraphBuilder _this, VertexBuilder first, VertexBuilder second)
        {
            first.AddEdge(second);
            if (!_this.Vertices.Contains(first)) _this.Vertices.Add(first);
            if (!_this.Vertices.Contains(second)) _this.Vertices.Add(second);
            
        }

        public override void UndirectedGraph_AddVertex(UndirectedGraphBuilder _this, VertexBuilder vertex)
        {
            if (!_this.Vertices.Contains(vertex)) _this.Vertices.Add(vertex);
        }

        public override int UndirectedGraph_ComputeProperty_Size(UndirectedGraphBuilder _this)
        {
            return _this.Vertices.Count;
        }

        public override void UndirectedGraph_RemoveVertex(UndirectedGraphBuilder _this, VertexBuilder vertex)
        {
            if (_this.Vertices.Contains(vertex)) _this.Vertices.Remove(vertex);
        }

        public override void Vertex_AddEdge(VertexBuilder _this, VertexBuilder neighbor)
        {
            if (!_this.Neighbors.Contains(neighbor)) _this.Neighbors.Add(neighbor);
            if (!neighbor.Neighbors.Contains(_this)) neighbor.Neighbors.Add(_this);
        }

        public override int Vertex_ComputeProperty_NeighborCount(VertexBuilder _this)
        {
            return _this.Neighbors.Count;
        }

        public override void Vertex_RemoveEdge(VertexBuilder _this, VertexBuilder neighbor)
        {
            if (_this.Neighbors.Contains(neighbor)) _this.Neighbors.Remove(neighbor);
            if (neighbor.Neighbors.Contains(_this)) neighbor.Neighbors.Remove(_this);
        }
    }
}
