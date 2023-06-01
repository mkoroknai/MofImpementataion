namespace SampleNamespace
{
    metamodel GraphMeta(Uri="http://example.org/mytestlang/0.1"); 


    class Vertex
    {
    	string ID;
    	UndirectedGraph OwnerGraph;
    }

    class Edge
    {
    	double Weight;
    	set<Vertex> Ends;
    	UndirectedGraph OwnerGraph;
    }

    class UndirectedGraph
    {
    	string Name;
    	set<Vertex> Vertices;
    	set<Edge> Edges;
    	void AddEdge(Edge edge);
    	void RemoveEdge(Edge edge);
    	void AddVertex(Vertex Vertex);
    	void RemoveVertex(Vertex Vertex);
    }

    association UndirectedGraph.Vertices with Vertex.OwnerGraph;
    association UndirectedGraph.Edges with Edge.OwnerGraph;
}
