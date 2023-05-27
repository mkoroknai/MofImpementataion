namespace SampleNamespace
{
    metamodel UndirectedGraph(Uri="http://example.org/mytestlang/0.1"); 


    class Vertex
    {
    	string ID;
    	derived int NeighborCount;
    	list<Vertex> Neighbors;
    	void AddEdge(Vertex neighbor);
    	void RemoveEdge(Vertex neighbor);
    }

    class UndirectedGraph
    {
    	derived int Size;
    	list<Vertex> Vertices;
    	void AddVertex(Vertex vertex);
    	void RemoveVertex(Vertex vertex);
    	void AddPair(Vertex first, Vertex second);
    }

}
