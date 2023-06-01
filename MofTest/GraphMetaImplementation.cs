using SampleNamespace;
using SampleNamespace.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SampleNamespace.Internal
{
    class GraphMetaImplementation : GraphMetaImplementationBase
    {

        public override void UndirectedGraph_AddEdge(UndirectedGraphBuilder _this, EdgeBuilder edge)
        {
            if (edge.Ends.Count > 2) throw new ArgumentException("edge can't have more than two vertices");

            if (!_this.Edges.Contains(edge)) _this.Edges.Add(edge);

            foreach(var v in edge.Ends)
            {
                _this.AddVertex(v);
            }
        }

        public override void UndirectedGraph_AddVertex(UndirectedGraphBuilder _this, VertexBuilder Vertex)
        {
            if (!_this.Vertices.Contains(Vertex)) _this.Vertices.Add(Vertex);
        }

        public override void UndirectedGraph_RemoveEdge(UndirectedGraphBuilder _this, EdgeBuilder edge)
        {
            if (_this.Edges.Contains(edge)) _this.Edges.Remove(edge);
        }

        public override void UndirectedGraph_RemoveVertex(UndirectedGraphBuilder _this, VertexBuilder Vertex)
        {
            if (_this.Vertices.Contains(Vertex)) _this.Vertices.Remove(Vertex);

            foreach(var e in _this.Edges)
            {
                if (e.Ends.Contains(Vertex)) _this.Edges.Remove(e);
            }
        }

    }
}
