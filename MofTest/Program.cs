using MetaDslx.Modeling;
using MetaDslx.BuildTasks;
using MetaDslx.Languages.Antlr4Roslyn.Compilation;
using MetaDslx.Languages.Antlr4Roslyn.Syntax.InternalSyntax;
using MetaDslx.Languages.Meta;
using MetaDslx.Languages.MetaGenerator.Compilation;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using MofImplementationLib.Generator;
using MofImplementationLib.Model;
using System.Linq;
using System.IO;
using SampleNamespace;

namespace MofTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //MofTestConstantsGen constantsGenerator = new MofTestConstantsGen();
            //constantsGenerator.GenerateUmlMethodConstants();
            //Test001();
            //MofXmiSerializer xmiSerializer = new MofXmiSerializer();

            //var model = xmiSerializer.ReadModelFromFile("MOF.xmi");
            //var mutableGroup = model.ModelGroup.ToMutable();
            //var umlModel = mutableGroup.Models.First(m => m.Name.Contains("UML.xmi"));

            //umlModel.

            TestGraph();
            //TestTestGraph();

        }

        static void Test001()
        {
            //var xmiSerializer = new MofXmiSerializer();
            //var model = xmiSerializer.ReadModelFromFile("../../../MOF.xmi");
            //var generator = new MofModelToMetaModelGenerator(model.Objects);
            //var generatedCode = generator.Generate("MofImplementationLib.Model", "Mof", "http://www.omg.org/spec/MOF");
            //var expectedCode = File.ReadAllText("../../../../MofImplementationLib/Model/Mof.mm");
            //File.WriteAllText("../../../../MofImplementationLib/Model/MofTestGen.mm", generatedCode);
            //Console.WriteLine(generatedCode == expectedCode); // This should print "true"

            MutableModel model = new MutableModel("TestModel");
            MofFactory mofFactory = new MofFactory(model);

            TypeBuilder stringType = mofFactory.Type();
            TypeBuilder intType = mofFactory.Type();

            ClassBuilder class0 = mofFactory.Class();
            class0.Name = "Allat";
            PropertyBuilder class0Property = mofFactory.Property();
            class0Property.Name = "Faj";
            class0Property.Type = stringType;
            class0.OwnedAttribute.Add(class0Property);

            // class1
            var class1 = mofFactory.Class();
            class1.Name = "Celeb";
            PropertyBuilder class1Property1 = mofFactory.Property();
            PropertyBuilder class1Property2 = mofFactory.Property();

            OperationBuilder class1operation1 = mofFactory.Operation();

            class1operation1.Name = "ChangeNev";

            ParameterBuilder nameParam = mofFactory.Parameter();
            nameParam.Name = "ujNev";
            nameParam.Type = stringType;
            class1operation1.OwnedParameter.Add(nameParam);

            stringType.Name = "string";
            intType.Name = "int";
            class1Property1.Type = stringType;
            class1Property1.Name = "Nev";
            class1Property2.Type = intType;
            class1Property2.Name = "Eletkor";

            class1.OwnedAttribute.Add(class1Property1);
            class1.OwnedAttribute.Add(class1Property2);
            class1.OwnedOperation.Add(class1operation1);

            GeneralizationBuilder general1 = mofFactory.Generalization();
            general1.General = class0;

            class1.Generalization.Add(general1);

            // class2
            var class2 = mofFactory.Class();
            class2.Name = "HaziAllat";
            PropertyBuilder class2Property1 = mofFactory.Property();
            PropertyBuilder class2Property2 = mofFactory.Property();
            class2Property1.Type = stringType;
            class2Property2.Type = stringType;
            class2Property1.Name = "Nev";
            class2Property2.Name = "BundaSzin";

            class2.OwnedAttribute.Add(class2Property1);
            class2.OwnedAttribute.Add(class2Property2);

            GeneralizationBuilder general2 = mofFactory.Generalization();
            general2.General = class0;
            class2.Generalization.Add(general2);

            PackageBuilder package = mofFactory.Package();
            package.Name = "MOF_model_test";

            package.PackagedElement.Add(class0);
            package.PackagedElement.Add(class1);
            package.PackagedElement.Add(class2);


            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            var generator = new MofModelToMetaModelGenerator(package.ToImmutable().PackagedElement);
            var generatedCode = generator.Generate("SampleNamespace", "MOF_modell_teszt", "http://example.org/mytesztlang/0.1");
            File.WriteAllText("ModellTeszt.mm", generatedCode);
        }

        static void TestGraph()
        {
            MutableModel model = new MutableModel("TestGraph");
            MofFactory factory = new MofFactory(model);

            PrimitiveTypeBuilder stringType = factory.PrimitiveType();
            PrimitiveTypeBuilder intType = factory.PrimitiveType();
            stringType.Name = "String";
            intType.Name = "Integer";


            ClassBuilder vertex = factory.Class();
            vertex.Name = "Vertex";

            // vertex id property
            PropertyBuilder vertexId = factory.Property();
            vertexId.Type = stringType;
            vertexId.Name = "ID";

            // neighborCount
            PropertyBuilder vertexNeighborCount = factory.Property();
            vertexNeighborCount.Type = intType;
            vertexNeighborCount.Name = "NeighborCount";
            vertexNeighborCount.IsDerived = true;

            // vertexNeighbors
            PropertyBuilder vertexNeighbors = factory.Property();
            var vertexNeighborsUpperValue = factory.LiteralUnlimitedNatural();
            vertexNeighborsUpperValue.Value = long.MaxValue;
            vertexNeighbors.Type = vertex;
            vertexNeighbors.Name = "Neighbors";
            vertexNeighbors.IsOrdered = true;
            vertexNeighbors.IsUnique = true;
            vertexNeighbors.UpperValue = vertexNeighborsUpperValue;

            // adding attributes to vertex
            vertex.OwnedAttribute.Add(vertexId);
            vertex.OwnedAttribute.Add(vertexNeighborCount);
            vertex.OwnedAttribute.Add(vertexNeighbors);

            // add edge operation
            OperationBuilder vertexAddEdge = factory.Operation();
            vertexAddEdge.Name = "AddEdge";
            ParameterBuilder vertexAddEdgeInput = factory.Parameter();
            vertexAddEdgeInput.Name = "neighbor";
            vertexAddEdgeInput.Direction = ParameterDirectionKind.In;
            vertexAddEdgeInput.Type = vertex;

            vertexAddEdge.OwnedParameter.Add(vertexAddEdgeInput);


            // remove edge operation
            OperationBuilder vertexRemoveEdge = factory.Operation();
            vertexRemoveEdge.Name = "RemoveEdge";
            ParameterBuilder vertexRemoveEdgeInput = factory.Parameter();
            vertexRemoveEdgeInput.Name = "neighbor";
            vertexRemoveEdgeInput.Direction = ParameterDirectionKind.In;
            vertexRemoveEdgeInput.Type = vertex;

            vertexRemoveEdge.OwnedParameter.Add(vertexRemoveEdgeInput);

            vertex.OwnedOperation.Add(vertexAddEdge);
            vertex.OwnedOperation.Add(vertexRemoveEdge);



            ClassBuilder graph = factory.Class();
            graph.Name = "UndirectedGraph";

            PropertyBuilder graphSize = factory.Property();
            graphSize.Name = "Size";
            graphSize.Type = intType;
            graphSize.IsDerived = true;

            PropertyBuilder graphVertices = factory.Property();
            graphVertices.Type = vertex;
            graphVertices.Name = "Vertices";
            foreach(var sp in vertexNeighbors.SubsettedProperty)
            {
                Console.WriteLine(sp.Name);
            }
            graphVertices.IsOrdered = true;
            graphVertices.IsUnique = true;
            var graphVerticesUpperValue = factory.LiteralUnlimitedNatural();
            graphVerticesUpperValue.Value = long.MaxValue;
            graphVertices.UpperValue = graphVerticesUpperValue;

            graph.OwnedAttribute.Add(graphSize);
            graph.OwnedAttribute.Add(graphVertices);

            OperationBuilder graphAddVertex = factory.Operation();
            graphAddVertex.Name = "AddVertex";
            ParameterBuilder graphAddVertexInput = factory.Parameter();
            graphAddVertexInput.Name = "vertex";
            graphAddVertexInput.Type = vertex;
            graphAddVertexInput.Direction = ParameterDirectionKind.In;

            graphAddVertex.OwnedParameter.Add(graphAddVertexInput);

            OperationBuilder graphRemoveVertex = factory.Operation();
            graphRemoveVertex.Name = "RemoveVertex";
            ParameterBuilder graphRemoveVertexInput = factory.Parameter();
            graphRemoveVertexInput.Name = "vertex";
            graphRemoveVertexInput.Type = vertex;
            graphRemoveVertexInput.Direction = ParameterDirectionKind.In;

            graphRemoveVertex.OwnedParameter.Add(graphRemoveVertexInput);

            OperationBuilder graphAddPair = factory.Operation();
            graphAddPair.Name = "AddPair";
            ParameterBuilder graphAddPairFirstInput = factory.Parameter();
            ParameterBuilder graphAddPairSecondInput = factory.Parameter();
            graphAddPairFirstInput.Name = "first";
            graphAddPairSecondInput.Name = "second";
            graphAddPairFirstInput.Type = vertex;
            graphAddPairSecondInput.Type = vertex;
            graphAddPairFirstInput.Direction = ParameterDirectionKind.In;
            graphAddPairSecondInput.Direction = ParameterDirectionKind.In;

            graphAddPair.OwnedParameter.Add(graphAddPairFirstInput);
            graphAddPair.OwnedParameter.Add(graphAddPairSecondInput);


            graph.OwnedOperation.Add(graphAddVertex);
            graph.OwnedOperation.Add(graphRemoveVertex);
            graph.OwnedOperation.Add(graphAddPair);
            

            PackageBuilder package = factory.Package();
            package.Name = "UndirectedGraph";

            package.PackagedElement.Add(vertex);
            package.PackagedElement.Add(graph);


            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            var generator = new MofModelToMetaModelGenerator(package.ToImmutable().PackagedElement);
            var generatedCode = generator.Generate("SampleNamespace", "UndirectedGraph", "http://example.org/mytestlang/0.1");


            string mm_filepath = "../../../Model/UndirectedGraph.mm";
            File.WriteAllText(mm_filepath, generatedCode);

            string xmi_filepath = "../../../Model/UndirectedGraph.xmi";
            XmiSerializer xmiSerializer = new XmiSerializer();
            xmiSerializer.WriteModelToFile(xmi_filepath, model);

            //MetaGeneratorCompiler mc = new MetaGeneratorCompiler(mm_filepath, "../../../Model/");
            //mc.Compile();

        }

        static void TestTestGraph()
        {
            MutableModel model = new MutableModel("tst");

            var factory = new UndirectedGraphFactory(model);

            SampleNamespace.VertexBuilder vertex1 = factory.Vertex();
            vertex1.ID = "v1";
            SampleNamespace.VertexBuilder vertex2 = factory.Vertex();
            vertex2.ID = "v2";
            SampleNamespace.VertexBuilder vertex3 = factory.Vertex();
            vertex3.ID = "v3";

            vertex1.AddEdge(vertex2);
            vertex3.AddEdge(vertex2);

            SampleNamespace.UndirectedGraphBuilder graph1 = factory.UndirectedGraph();

            graph1.AddVertex(vertex1);
            graph1.AddVertex(vertex2);
            graph1.AddVertex(vertex3);

            SampleNamespace.VertexBuilder vertex4 = factory.Vertex();
            vertex4.ID = "v4";
            SampleNamespace.VertexBuilder vertex5 = factory.Vertex();
            vertex5.ID = "v5";

            graph1.AddPair(vertex4, vertex5);

            foreach (var n in vertex4.Neighbors)
            {
                Console.WriteLine(n.ID);
            }
        }
    }
}
