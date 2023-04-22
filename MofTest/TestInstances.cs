using MetaDslx.Modeling;
using MofImplementationLib.Generator;
using MofImplementationLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MofTest
{
    public class TestInstances
    {

        public AssociationBuilder association01;
        public BehavioralFeatureBuilder behavioralfeat01;
        public BehaviorBuilder behavior01;
        public NamedElementBuilder namedelement01, namedelement02, namedelement03;
        public ParameterBuilder parameter01, parameter02, parameter03, parameter04;
        public BehavioredClassifierBuilder behavioredclassif01;
        public ClassifierBuilder classifier01;
        public TypeBuilder type01;
        public List<NamedElementBuilder> inhs;
        public ConnectableElementBuilder connectableelement01;
        public ConnectionPointReferenceBuilder connectionpointreference01;
        public RedefinableElementBuilder redefinableelement01;
        public ConnectorEndBuilder connectorend01;
        public ConnectorBuilder connector01;
        public ElementImportBuilder elementimport01;
        public EnumerationLiteralBuilder enumerationliteral01;
        public ExtensionEndBuilder extensionend01;
        public ExtensionBuilder extension01;
        public LiteralBooleanBuilder literalboolean01;
        public LiteralIntegerBuilder literalinteger01;
        public LiteralNullBuilder literalnull01;
        public LiteralRealBuilder literalreal01;
        public LiteralStringBuilder literalstring01;
        public LiteralUnlimitedNaturalBuilder literalunlimitednatural01;
        public MultiplicityElementBuilder multiplicityElement01;
        public NamespaceBuilder namespace01;
        public List<PackageableElementBuilder> list01;
        public OpaqueExpressionBuilder opaqueexpression01;
        public ParameterableElementBuilder parameterableelement01;
        public PortBuilder port01;
        public PseudostateBuilder pseudostate01;
        public RegionBuilder region01;
        public StateMachineBuilder statemachine01;
        public VertexBuilder vertex01, vertex02;
        public StateBuilder state01;
        public StereotypeBuilder stereotype01;
        public StringExpressionBuilder stringexpression01;
        public TransitionBuilder transition01;
        public UseCaseBuilder usecase01;
        public LiteralRealBuilder valuespecification01;

        public TestInstances(MutableModel umlModel)
        {
            var factory = new MofFactory(umlModel);

            association01 = umlModel.Objects.OfType<AssociationBuilder>().First(a => a.Name == "A_selection_objectNode");
            behavioralfeat01 = umlModel.Objects.OfType<BehavioralFeatureBuilder>().First(bf => bf.Name == "isConsistentWith");

            behavior01 = factory.Behavior();
            behavior01.Name = "BehaviorTestInstance";

            namedelement01 = umlModel.Objects.OfType<NamedElementBuilder>().First(neb => neb.Name == "Class");

            parameter01 = factory.Parameter();
            parameter01.Name = "testParameter01";
            parameter01.Direction = ParameterDirectionKind.In;
            parameter02 = factory.Parameter();
            parameter02.Name = "testParameter02";
            parameter02.Direction = ParameterDirectionKind.Out;
            parameter03 = factory.Parameter();
            parameter03.Name = "testParameter03";
            parameter03.Direction = ParameterDirectionKind.Inout;

            behavior01.OwnedParameter.Add(parameter01);
            behavior01.OwnedParameter.Add(parameter02);
            behavior01.OwnedParameter.Add(parameter03);

            behavioredclassif01 = factory.BehavioredClassifier();
            behavioredclassif01.Name = "testBehavioredClassifier01";

            classifier01 = umlModel.Objects.OfType<ClassifierBuilder>().First(cb => cb.Name == "Activity");
            type01 = umlModel.Objects.OfType<TypeBuilder>().First(tb => tb.Name == "Activity");

            var ne01 = umlModel.Objects.OfType<NamedElementBuilder>().First(neb => neb.Name == "Activities");
            inhs = new List<NamedElementBuilder>();
            inhs.Add(ne01);
            inhs.Add(classifier01);

            connectableelement01 = umlModel.Objects.OfType<ConnectableElementBuilder>().First(ceb => ceb.Name == "variable");

            connectionpointreference01 = factory.ConnectionPointReference();
            connectionpointreference01.Name = "testConnectionPointReference01";
            redefinableelement01 = factory.RedefinableElement();

            connectorend01 = factory.ConnectorEnd();
            var ce02 = factory.ConnectorEnd();
            ConnectorBuilder Connector = factory.Connector();
            connectorend01.Connector = Connector;
            ce02.Connector = Connector;
            Connector.End.Add(connectorend01);
            Connector.End.Add(ce02);
            AssociationBuilder testAssociation01 = factory.Association();
            testAssociation01.Name = "testType01";
            Connector.Name = "testConnector01";
            Connector.Type = testAssociation01;
            testAssociation01.MemberEnd.Add(connectableelement01 as PropertyBuilder);

            connector01 = Connector;

            elementimport01 = factory.ElementImport();
            elementimport01.Alias = "testElementImport01";

            enumerationliteral01 = factory.EnumerationLiteral();
            var c = factory.Enumeration();
            c.Name = "testEnumeration69";
            enumerationliteral01.Enumeration = c;

            extensionend01 = factory.ExtensionEnd();
            extensionend01.Name = "testExtensionEnd01";

            CreateExtensionInstance(factory);

            literalboolean01 = factory.LiteralBoolean();
            literalboolean01.Name = "testLiteralBoolean01";

            literalinteger01 = factory.LiteralInteger();
            literalinteger01.Name = "testLiteralInteger01";

            literalnull01 = factory.LiteralNull();
            literalnull01.Name = "testLiteralNull01";

            literalreal01 = factory.LiteralReal();
            literalreal01.Value = 1.02045;
            literalreal01.Name = "testLiteralReal01";

            literalstring01 = factory.LiteralString();
            literalstring01.Value = "this is a test";
            literalstring01.Name = "testLiteralString01";

            literalunlimitednatural01 = factory.LiteralUnlimitedNatural();
            literalunlimitednatural01.Value = 99999999919;
            literalunlimitednatural01.Name = "testLiteralUnlimitedNatural01";

            multiplicityElement01 = umlModel.Objects.OfType<MultiplicityElementBuilder>().First(meb => meb is NamedElementBuilder neb && neb.Name == "edge");

            namedelement02 = umlModel.Objects.OfType<NamedElementBuilder>().First(ne => ne.Name == "UML");
            var ns = factory.Namespace();
            ns.Name = "testNamespace00";
            namedelement02.Namespace = ns;

            namespace01 = factory.Namespace();
            var p = umlModel.Objects.OfType<PackageableElementBuilder>().First(pe => pe.Name == "Activities");
            var elementI = factory.ElementImport();
            elementI.ImportedElement = p;
            ns.ElementImport.Add(elementI);
            var a = factory.PackageableElement();
            a.Name = "testPackageableElement01";
            var d = umlModel.Objects.OfType<PackageableElementBuilder>().First(pe => pe.Name == "A_selection_objectNode");
            var b = namedelement02 as PackageableElementBuilder;
            list01 = new List<PackageableElementBuilder>();
            list01.Add(a);
            //list01.Add(b);
            list01.Add(d);

            opaqueexpression01 = factory.OpaqueExpression();
            opaqueexpression01.Name = "testOpaqueExpression01";
            var par = factory.Parameter();
            par.Name = "testParameter04";
            opaqueexpression01.Behavior = factory.Behavior();
            opaqueexpression01.Behavior.OwnedParameter.Add(par);

            namedelement03 = umlModel.Objects.OfType<NamedElementBuilder>().First(ne => ne.Name == "maximum_one_parameter_node");
            var prof01 = factory.Profile();
            prof01.Name = "testProfile01";
            namedelement02.Namespace = prof01;

            parameterableelement01 = factory.ParameterableElement();

            parameter04 = umlModel.Objects.OfType<ParameterBuilder>().First(p => p.Name == "result");

            port01 = factory.Port();
            port01.Name = "testPort01";
            var type_ = factory.Classifier();
            type_.Name = "testClassifier01";
            port01.Type = type_;

            pseudostate01 = factory.Pseudostate();
            pseudostate01.Name = "testPseudostate01";

            region01 = factory.Region();
            var sm = factory.StateMachine();
            sm.Name = "testStateMachine01";
            region01.Name = "testRegion01";
            region01.StateMachine = sm;

            statemachine01 = factory.StateMachine();
            statemachine01.Name = "testStateMachine02";
            var r = factory.Region();
            r.Name = "testRegion02";
            r.StateMachine = statemachine01;
            var s = factory.State();
            s.Name = "testState01";
            r.State = s;
            vertex01 = factory.Vertex();
            vertex02 = factory.Vertex();
            vertex01.Container = r;
            vertex02.Container = r;
            vertex01.Name = "testVertex01";
            vertex02.Name = "testVertex02";

            state01 = factory.State();
            state01.Name = "testState01";
            var r2 = factory.Region();

            r2.StateMachine = sm;
            state01.Container = r2;

            stereotype01 = factory.Stereotype();
            stereotype01.Name = "testStereotype01";
            var prof02 = factory.Profile();
            prof02.Name = "testProfile02";
            stereotype01.Namespace = prof02;
            var r1 = factory.Region();
            r2.StateMachine = sm;

            stringexpression01 = factory.StringExpression();
            stringexpression01.Name = "testStringExpression01";
            var se01 = factory.StringExpression();
            var se02 = factory.StringExpression();
            stringexpression01.SubExpression.Add(se01);
            stringexpression01.SubExpression.Add(se02);

            transition01 = factory.Transition();
            transition01.Name = "testTransition01";
            transition01.Container = r;
            transition01.Container.StateMachine = sm;

            usecase01 = factory.UseCase();
            var uc02 = factory.UseCase();
            usecase01.Name = "testUseCase01";
            uc02.Name = "testUseCase02";
            var inc = factory.Include();
            inc.Addition = uc02;
            usecase01.Include.Add(inc);

            valuespecification01 = factory.LiteralReal();
            valuespecification01.Name = "testLiteralReal02";
            valuespecification01.Value = 69.6969;
            var t = factory.Type();
            t.Name = "testType09";
            valuespecification01.Type = t;
        }

        public void CreateExtensionInstance(MofFactory factory)
        {
            extension01 = factory.Extension();
            extension01.Name = "testExtension01";
            var ee02 = factory.ExtensionEnd();
            ee02.Name = "testExtensionEnd02";
            var ee03 = factory.ExtensionEnd();
            ee03.Name = "testExtensionEnd03";
            var type03 = factory.Stereotype();
            type03.Name = "testType01";
            ee02.Type = type03;
            ee03.Type = type03;
            extensionend01.Type = factory.Stereotype();
            extension01.OwnedEnd = extensionend01;
            extension01.MemberEnd.Add(extensionend01);
            extension01.MemberEnd.Add(ee03 as PropertyBuilder);
            extension01.MemberEnd.Add(ee02 as PropertyBuilder);
        }
    }
}
