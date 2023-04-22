using MetaDslx.Modeling;
using MofImplementationLib.Generator;
using MofImplementationLib.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.Immutable;
using System;
using System.Linq;
using MofTest;


namespace MofTestProjDotNetCore
{
    internal static class IEnumerableExtensions
    {
        public static HashSet<T> ToSet<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        }
    }
    [TestClass]
    public class MOFTest
    {
        MutableModel umlModel;
        TestInstances Instances;

        [TestInitialize]
        public void Initialize()
        {
            var xmiSerializer = new MofXmiSerializer();
            var model = xmiSerializer.ReadModelFromFile("../../../../MofBootstrap/MOF.xmi");

            var mutableGroup = model.ModelGroup.ToMutable();
            umlModel = mutableGroup.Models.First(m => m.Name.Contains("UML.xmi"));

            Instances = new TestInstances(umlModel);
        }

        [TestMethod]
        public void TestAssociation()
        {
            var ass = Instances.association01;

            Console.WriteLine();
            Console.WriteLine("TestAssociation (" + ass.Name + ")");
            Console.WriteLine();

            TestList(ass.EndType, TstConstants.Association_EndType);
        }

        [TestMethod]
        public void TestBehavioralFeature()
        {
            var bfeat = Instances.behavioralfeat01;
            var ne = Instances.namedelement01;

            Console.WriteLine();
            Console.WriteLine("TestBehavioralFeature (" + bfeat.Name + ")");
            Console.WriteLine();

            TestList(bfeat.InputParameters(), TstConstants.BehavioralFeature_InputParameters);
            Assert.AreEqual(bfeat.IsDistinguishableFrom(ne, ne.Namespace), TstConstants.BehavioralFeature_IsDistinguishableFrom);

            TestList(bfeat.OutputParameters(), TstConstants.BehavioralFeature_OutputParameters);
        }


        [TestMethod]
        public void TestBehavior()
        {
            BehaviorBuilder behavior = Instances.behavior01;
            BehavioredClassifierBuilder testContext01 = Instances.behavioredclassif01;
            var from = Instances.namedelement01;


            Console.WriteLine();
            Console.WriteLine("TestBehavior (" + behavior.Name + ")");
            Console.WriteLine();

            Console.WriteLine("  BehavioredClassifier");
            Assert.AreEqual(behavior.BehavioredClassifier(from).Name, TstConstants.Behavior_BehavioredClassifier);
            Console.WriteLine("    " + behavior.BehavioredClassifier(from));

            Assert.IsTrue(behavior.BehavioredClassifier(from) is BehavioredClassifierBuilder);

            Console.WriteLine("  InputParameters");
            TestList(behavior.InputParameters(), TstConstants.Behavior_InputParameters);

            Console.WriteLine("  OutputParameters");
            TestList(behavior.OutputParameters(), TstConstants.Behavior_OutputParameters);

            if (TstConstants.Behavior_Context == "null")
                Assert.IsNull(behavior.Context);
            else
                Assert.AreEqual(behavior.Context, TstConstants.Behavior_Context);
        }


        [TestMethod]
        public void TestClassifier()
        {
            ClassifierBuilder classifier = Instances.classifier01;
            TypeBuilder other = Instances.type01;
            List<NamedElementBuilder> inhs = Instances.inhs;

            Console.WriteLine();
            Console.WriteLine("TestClassifier (" + classifier.Name + ")");
            Console.WriteLine();


            TestList(classifier.AllAttributes(), TstConstants.Classifier_AllAttributes);

            TestList(classifier.AllFeatures(), TstConstants.Classifier_AllFeatures);

            TestList(classifier.AllParents(), TstConstants.Classifier_AllParents);

            TestList(classifier.AllRealizedInterfaces(), TstConstants.Classifier_AllRealizedInterfaces);

            Assert.AreEqual(classifier.AllSlottableFeatures().Count, TstConstants.Classifier_AllSlottableFeatures_Count);

            TestList(classifier.General, TstConstants.Classifier_General);

            TestList(classifier.InheritedMember, TstConstants.Classifier_InheritedMember);

            Assert.AreEqual(classifier.ConformsTo(other), TstConstants.Classifier_ConformsTo);

            TestList(classifier.DirectlyRealizedInterfaces(), TstConstants.Classifier_DirectlyRealizedInterfaces);

            Assert.AreEqual(classifier.HasVisibilityOf(other), TstConstants.Classifier_HasVisibilityOf);

            TestList(classifier.Inherit(inhs), TstConstants.Classifier_Inherit);

            Assert.AreEqual(classifier.InheritableMembers(other as ClassifierBuilder).Count, TstConstants.Classifier_InheritableMembers_Count);

            Assert.AreEqual(classifier.IsSubstitutableFor(other as ClassifierBuilder), TstConstants.Classifier_IsSubstitutableFor);

            Assert.AreEqual(classifier.IsTemplate(), TstConstants.Classifier_IsTemplate);

            Assert.AreEqual(classifier.MaySpecializeType(other as ClassifierBuilder), TstConstants.Classifier_MaySpecializeType);

            TestList(classifier.Parents(), TstConstants.Classifier_Parents);
        }

        [TestMethod]
        public void TestClass()
        {
            ClassBuilder class_ = Instances.classifier01 as ClassBuilder;

            Console.WriteLine();
            Console.WriteLine("TestClass (" + class_.Name + ")");
            Console.WriteLine();


            TestList(class_.Extension, TstConstants.Class_Extension);

            TestList(class_.SuperClass, TstConstants.Class_SuperClass);

        }
        [TestMethod]
        public void TestConnectableElement()
        {
            var ce = Instances.connectableelement01;

            Console.WriteLine();
            Console.WriteLine("TestConnectableElement (" + ce.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(ce.End.Count, TstConstants.ConnectableElement_End_Count);
        }
        [TestMethod]
        public void TestConnectionPointReference()
        {
            var cpr = Instances.connectionpointreference01;
            var re = Instances.redefinableelement01;


            Console.WriteLine();
            Console.WriteLine("TestConnectionPointReference (" + cpr.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(cpr.IsConsistentWith(re), TstConstants.ConnectionPointReference_IsConsistentWith);
        }

        [TestMethod]
        public void TestConnectorEnd()
        {
            var ce01 = Instances.connectorend01;

            Console.WriteLine();
            Console.WriteLine("TestConnectorEnd (" + ce01.MName + ")");
            Console.WriteLine();


            Assert.AreEqual(ce01.DefiningEnd.Name, TstConstants.ConnectorEnd_DefiningEnd);
        }
        [TestMethod]
        public void TestConnector()
        {
            var c = Instances.connector01;

            Console.WriteLine();
            Console.WriteLine("TestConnector (" + c.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(c.Kind.ToString(), TstConstants.Connector_Kind);
        }
        [TestMethod]
        public void TestDeploymentTarget()
        {
            var dt = Instances.connectableelement01 as DeploymentTargetBuilder;

            Console.WriteLine();
            Console.WriteLine("TestDeploymentTarget (" + dt.Name + ")");
            Console.WriteLine();

            TestList(dt.DeployedElement, TstConstants.DeploymentTarget_DeployedElement);
        }
        [TestMethod]
        public void TestElementImport()
        {
            var ei = Instances.elementimport01;

            Console.WriteLine();
            Console.WriteLine("TestElementImport (" + ei.Alias + ")");
            Console.WriteLine();

            Assert.AreEqual(ei.GetName(), TstConstants.ElementImport_GetName);
        }
        [TestMethod]
        public void TestElement()
        {
            var e = Instances.namedelement01 as ElementBuilder;

            Console.WriteLine();
            Console.WriteLine("TestElement (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.AllOwnedElements().Count, TstConstants.Element_AllOwnedElements_Count);

            Assert.AreEqual(e.MustBeOwned(), TstConstants.Element_MustBeOwned);
        }
        [TestMethod]
        public void TestEncapsulatedClassifier()
        {
            var e = Instances.classifier01 as EncapsulatedClassifierBuilder;

            Console.WriteLine();
            Console.WriteLine("TestEncapsulatedClassifier (" + e.Name + ")");
            Console.WriteLine();

            TestList(e.OwnedPort, TstConstants.EncapsulatedClassifier_OwnedPort);
        }
        [TestMethod]
        public void TestEnumerationLiteral()
        {
            var e = Instances.enumerationliteral01;

            Console.WriteLine();
            Console.WriteLine("TestEnumerationLiteral (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.Classifier.Name, TstConstants.EnumerationLiteral_Classifier);
        }

        [TestMethod]
        public void TestExtensionEnd()
        {
            var e = Instances.extensionend01;

            Console.WriteLine();
            Console.WriteLine("TestExtensionEnd (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.Lower, TstConstants.ExtensionEnd_Lower);

            Assert.AreEqual(e.LowerBound(), TstConstants.ExtensionEnd_LowerBound);
        }
        [TestMethod]
        public void TestExtension()
        {
            var e = Instances.extension01;

            Console.WriteLine();
            Console.WriteLine("TestExtension (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsRequired, TstConstants.Extension_IsRequired);

            Assert.AreEqual(e.Metaclass.Name, TstConstants.Extension_MetaClass);

            Assert.AreEqual(e.MetaclassEnd().Name, TstConstants.Extension_MetaClassEnd);
        }
        [TestMethod]
        public void TestLiteralBoolean()
        {
            var e = Instances.literalboolean01;

            Console.WriteLine();
            Console.WriteLine("TestLiteralBoolean (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.BooleanValue(), TstConstants.LiteralBoolean_BooleanValue);

            Assert.AreEqual(e.IsComputable(), TstConstants.LiteralBoolean_IsComputable);
        }
        [TestMethod]
        public void TestLiteralInteger()
        {
            var e = Instances.literalinteger01;

            Console.WriteLine();
            Console.WriteLine("TestLiteralInteger (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IntegerValue(), TstConstants.LiteralInteger_IntegerValue);

            Assert.AreEqual(e.IsComputable(), TstConstants.LiteralInteger_IsComputable);
        }
        [TestMethod]
        public void TestLiteralNull()
        {
            var e = Instances.literalnull01;

            Console.WriteLine();
            Console.WriteLine("TestLiteralNull (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsNull(), TstConstants.LiteralNull_IsNull);

            Assert.AreEqual(e.IsComputable(), TstConstants.LiteralNull_IsComputable);
        }
        [TestMethod]
        public void TestLiteralReal()
        {
            var e = Instances.literalreal01;

            Console.WriteLine();
            Console.WriteLine("TestLiteralReal (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.RealValue(), TstConstants.LiteralReal_RealValue, 0.01);
            Assert.AreEqual(e.IsComputable(), TstConstants.LiteralReal_IsComputable);
        }
        [TestMethod]
        public void TestLiteralString()
        {
            var e = Instances.literalstring01;

            Console.WriteLine();
            Console.WriteLine("TestLiteralString (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.StringValue(), TstConstants.LiteralString_StringValue);
            Assert.AreEqual(e.IsComputable(), TstConstants.LiteralString_IsComputable);
        }
        [TestMethod]
        public void TestLiteralUnlimitedNatural()
        {
            var e = Instances.literalunlimitednatural01;

            Console.WriteLine();
            Console.WriteLine("TestLiteralUnlimitedNatural (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.UnlimitedValue(), TstConstants.LiteralUnlimitedNatural_UnlimitedValue);
            Assert.AreEqual(e.IsComputable(), TstConstants.LiteralUnlimitedNatural_IsComputable);
        }
        [TestMethod]
        public void TestMultiplicityElement()
        {
            var me = Instances.multiplicityElement01;

            Console.WriteLine();
            Console.WriteLine("TestMultiplicityElement (" + (me as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(me.CompatibleWith(me), TstConstants.MultiplicityElement_CompatibleWith);
            Assert.AreEqual(me.Lower, TstConstants.MultiplicityElement_Lower);
            Assert.AreEqual(me.Upper, TstConstants.MultiplicityElement_Upper);
            Assert.AreEqual(me.IncludesMultiplicity(me), TstConstants.MultiplicityElement_IncludesMultiplicity);
            Assert.AreEqual(me.Is(0, 100), TstConstants.MultiplicityElement_Is);
            Assert.AreEqual(me.IsMultivalued(), TstConstants.MultiplicityElement_IsMultivalued);
            Assert.AreEqual(me.LowerBound(), TstConstants.MultiplicityElement_LowerBound);
            Assert.AreEqual(me.UpperBound(), TstConstants.MultiplicityElement_UpperBound);
        }
        [TestMethod]
        public void TestNamedElement()
        {
            var e = Instances.namedelement02;

            Console.WriteLine();
            Console.WriteLine("TestNamedElement (" + e.Name + ")");
            Console.WriteLine();

            TestList(e.AllNamespaces(), TstConstants.NamedElement_AllNamespaces);
            TestList(e.AllOwningPackages(), TstConstants.NamedElement_AllOwningPackages);
            TestList(e.ClientDependency, TstConstants.NamedElement_ClientDependency);
            Assert.AreEqual(e.QualifiedName, TstConstants.NamedElement_QualifiedName);
            Assert.AreEqual(e.IsDistinguishableFrom(e, e.Namespace), TstConstants.NamedElement_IsDistinguishableFrom);
            Assert.AreEqual(e.Separator(), TstConstants.NamedElement_Separator);
        }
        [TestMethod]
        public void TestNamespace()
        {
            var ns = Instances.namespace01;
            var c = Instances.namedelement01;
            List<PackageableElementBuilder> list = Instances.list01;


            Console.WriteLine();
            Console.WriteLine("TestNamespace (" + ns.Name + ")");
            Console.WriteLine();

            TestList(ns.ImportedMember.ToList(), TstConstants.Namespace_ImportedMember);
            TestList(ns.ExcludeCollisions(list), TstConstants.Namespace_ExcludeCollisions);
            TestList(ns.GetNamesOfMember(c), TstConstants.Namespace_GetNamesOfMember);
            TestList(ns.ImportMembers(list), TstConstants.Namespace_ImportMembers);
            Assert.AreEqual(ns.MembersAreDistinguishable(), TstConstants.Namespace_MembersAreDistinguishable);
        }
        [TestMethod]
        public void TestOpaqueExpression()
        {
            var ee = Instances.opaqueexpression01;

            Console.WriteLine();
            Console.WriteLine("TestOpaqueExpression (" + ee.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(ee.Result.Name, TstConstants.OpaqueExpression_Result);
            Assert.AreEqual(ee.IsIntegral(), TstConstants.OpaqueExpression_IsIntegral);
            Assert.AreEqual(ee.IsNonNegative(), TstConstants.OpaqueExpression_IsNonNegative);
            Assert.AreEqual(ee.IsPositive(), TstConstants.OpaqueExpression_IsPositive);
            Assert.AreEqual(ee.Value(), TstConstants.OpaqueExpression_Value);
        }
        [TestMethod]
        public void TestOperation()
        {
            var e = Instances.behavioralfeat01 as OperationBuilder;
            var red = Instances.classifier01 as RedefinableElementBuilder;

            Console.WriteLine();
            Console.WriteLine("TestOperation (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsOrdered, TstConstants.Operation_IsOrdered);
            Assert.AreEqual(e.IsUnique, TstConstants.Operation_IsUnique);
            Assert.AreEqual(e.Lower, TstConstants.Operation_Lower);
            Assert.AreEqual(e.Type.Name, TstConstants.Operation_Type);
            Assert.AreEqual(e.Upper, TstConstants.Operation_Upper);
            Assert.AreEqual(e.IsConsistentWith(red), TstConstants.Operation_IsConsistentWith);
            TestList(e.ReturnResult(), TstConstants.Operation_ReturnResult);
        }
        [TestMethod]
        public void TestPackage()
        {
            var e = Instances.namedelement02 as PackageBuilder;
            var ne = Instances.namedelement03;

            Console.WriteLine();
            Console.WriteLine("TestPackage (" + e.Name + ")");
            Console.WriteLine();

            TestList(e.AllApplicableStereotypes(), TstConstants.Package_AllApplicableStereotypes);
            TestList(e.NestedPackage, TstConstants.Package_NestedPackage);
            TestList(e.OwnedStereotype, TstConstants.Package_OwnedStereotype);
            TestList(e.OwnedType, TstConstants.Package_OwnedType);
            Assert.AreEqual(e.ContainingProfile().Name, TstConstants.Package_ContainingProfile);
            Assert.AreEqual(e.MakesVisible(ne), TstConstants.Package_MakesVisible);
            Assert.AreEqual(e.MustBeOwned(), TstConstants.Package_MustBeOwned);
            Assert.AreEqual(e.VisibleMembers().Count, TstConstants.Package_VisibleMembers_Count);
        }
        [TestMethod]
        public void TestParameterableElement()
        {
            var e = Instances.namedelement02 as ParameterableElementBuilder;
            var testPe = Instances.parameterableelement01;

            Console.WriteLine();
            Console.WriteLine("TestParameterableElement (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsCompatibleWith(testPe), TstConstants.ParameterableElement_IsCompatibleWith);
            Assert.AreEqual(e.IsTemplateParameter(), TstConstants.ParameterableElement_IsTemplateParameter);
        }
        [TestMethod]
        public void TestParameter()
        {
            var e = Instances.parameter04;

            Console.WriteLine();
            Console.WriteLine("TestParameter (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.Default, TstConstants.Parameter_Default);
        }
        [TestMethod]
        public void TestPort()
        {
            var e = Instances.port01;

            Console.WriteLine();
            Console.WriteLine("TestPort (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            TestList(e.BasicProvided(), TstConstants.Port_BasicProvided);
            //TestList(e.BasicRequired(), TstConstants.Port_BasicRequired);  // UsageBuilder missing
            TestList(e.Provided, TstConstants.Port_Provided);
            TestList(e.Required, TstConstants.Port_Required);
        }
        [TestMethod]
        public void TestProperty()
        {
            var e = Instances.multiplicityElement01 as PropertyBuilder;
            var peb = Instances.namedelement03 as ParameterableElementBuilder;
            var reb = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("TestProperty (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsComposite, TstConstants.Property_IsComposite);
            Assert.AreEqual(e.Opposite.Name, TstConstants.Property_Opposite);
            Assert.AreEqual(e.IsAttribute(), TstConstants.Property_IsAttribute);
            Assert.AreEqual(e.IsCompatibleWith(peb), TstConstants.Property_IsCompatibleWith);
            Assert.AreEqual(e.IsConsistentWith(reb), TstConstants.Property_IsConsistentWith);
            Assert.AreEqual(e.IsNavigable(), TstConstants.Property_IsNavigable);
            TestList(e.SubsettingContext(), TstConstants.Property_SubsettingContext);
        }
        [TestMethod]
        public void TestPseudostate()
        {
            var e = Instances.pseudostate01;
            var reb = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("TestPseudostate (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsConsistentWith(reb), TstConstants.Pseudostate_IsConsistentWith);
        }
        [TestMethod]
        public void TestRedefinableElement()
        {
            var e = Instances.classifier01 as RedefinableElementBuilder;
            var reb = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("TestRedefinableElement (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsRedefinitionContextValid(reb), TstConstants.RedefinableElement_IsRedefinitionContextValid);
        }
        [TestMethod]
        public void TestRegion()
        {
            var e = Instances.region01;
            var reb = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("TestRegion (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.BelongsToPSM(), TstConstants.Region_BelongsToPSM);
            Assert.AreEqual(e.RedefinitionContext.Name, TstConstants.Region_RedefinitionContext);
            Assert.AreEqual(e.ContainingStateMachine().Name, TstConstants.Region_ContainingStateMachine);
            Assert.AreEqual(e.IsConsistentWith(reb), TstConstants.Region_IsConsistentWith);
            Assert.AreEqual(e.IsRedefinitionContextValid(reb), TstConstants.Region_IsRedefinitionContextValid);
        }
        [TestMethod]
        public void TestStateMachine()
        {
            var e = Instances.statemachine01;
            var v1 = Instances.vertex01;
            var v2 = Instances.vertex01;
            var red = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("TestStateMachine (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.Ancestor(v1, v2), TstConstants.StateMachine_Ancestor);
            Assert.AreEqual(e.IsConsistentWith(red), TstConstants.StateMachine_IsConsistentWith);
            Assert.AreEqual(e.IsRedefinitionContextValid(red), TstConstants.StateMachine_IsRedefinitionContextValid);
            Assert.AreEqual(e.LCA(v1, v2).Name, TstConstants.StateMachine_LCA);
            Assert.AreEqual(e.LCAState(v1, v2).Name, TstConstants.StateMachine_LCAState);
        }
        [TestMethod]
        public void TestState()
        {
            var e = Instances.state01;
            var red = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("TestState (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsComposite, TstConstants.State_IsComposite);
            Assert.AreEqual(e.IsOrthogonal, TstConstants.State_IsOrthogonal);
            Assert.AreEqual(e.IsSimple, TstConstants.State_IsSimple);
            Assert.AreEqual(e.IsSubmachineState, TstConstants.State_IsSubmachineState);
            Assert.AreEqual(e.ContainingStateMachine().Name, TstConstants.State_ContainingStateMachine);
            Assert.AreEqual(e.IsConsistentWith(red), TstConstants.State_IsConsistentWith);
        }
        [TestMethod]
        public void TestStereotype()
        {
            var e = Instances.stereotype01;

            Console.WriteLine();
            Console.WriteLine("TestStereotype (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.Profile.Name, TstConstants.Stereotype_Profile);
            Assert.AreEqual(e.ContainingProfile().Name, TstConstants.Stereotype_ContainingProfile);
        }
        [TestMethod]
        public void TestStringExpression()
        {
            var e = Instances.stringexpression01;

            Console.WriteLine();
            Console.WriteLine("TestStringExpression (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.StringValue(), TstConstants.StringExpression_StringValue);
        }
        [TestMethod]
        public void TestStructuredClassifier()
        {
            var e = Instances.classifier01 as StructuredClassifierBuilder;

            Console.WriteLine();
            Console.WriteLine("TestStructuredClassifier (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            TestList(e.AllRoles(), TstConstants.StructuredClassifier_AllRoles);
            TestList(e.Part, TstConstants.StructuredClassifier_Part);
        }
        [TestMethod]
        public void TestTemplateableElement()
        {
            var e = Instances.namedelement02 as TemplateableElementBuilder;

            Console.WriteLine();
            Console.WriteLine("TestTemplateableElement (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.IsTemplate(), TstConstants.TemplateableElement_IsTemplate);
            Assert.AreEqual(e.ParameterableElements().Count, TstConstants.TemplateableElement_ParameterableElements_Count);
        }
        [TestMethod]
        public void TestTransition()
        {
            var e = Instances.transition01;
            var red = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("TestTransition (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.RedefinitionContext.Name, TstConstants.Transition_RedefinitionContext);
            Assert.AreEqual(e.ContainingStateMachine().Name, TstConstants.Transition_ContainingStateMachine);
            Assert.AreEqual(e.IsConsistentWith(red), TstConstants.Transition_IsConsistentWith);
        }
        [TestMethod]
        public void TestType()
        {
            var e = Instances.type01;
            var t = Instances.statemachine01;

            Console.WriteLine();
            Console.WriteLine("TestType (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.ConformsTo(t), TstConstants.Type_ConformsTo);
        }
        [TestMethod]
        public void TestUseCase()
        {
            var e = Instances.usecase01;

            Console.WriteLine();
            Console.WriteLine("TestUseCase (" + e.Name + ")");
            Console.WriteLine();

            TestList(e.AllIncludedUseCases(), TstConstants.UseCase_AllIncludedUseCases);
        }
        [TestMethod]
        public void TestValueSpecification()
        {
            var e = Instances.valuespecification01;

            Console.WriteLine();
            Console.WriteLine("TestValueSpecification (" + e.Name + ")");
            Console.WriteLine();

            Assert.AreEqual(e.BooleanValue(), TstConstants.ValueSpecification_BooleanValue);
            Assert.AreEqual(e.IntegerValue(), TstConstants.ValueSpecification_IntegerValue);
            Assert.AreEqual(e.IsCompatibleWith(e), TstConstants.ValueSpecification_IsCompatibleWith);
            Assert.AreEqual(e.IsComputable(), TstConstants.ValueSpecification_IsComputable);
            Assert.AreEqual(e.IsNull(), TstConstants.ValueSpecification_IsNull);
            Assert.AreEqual(e.RealValue(), TstConstants.ValueSpecification_RealValue);
            Assert.AreEqual(e.StringValue(), TstConstants.ValueSpecification_StringValue);
            Assert.AreEqual(e.UnlimitedValue(), TstConstants.ValueSpecification_UnlimitedValue);
        }

        [TestMethod]
        public void TestVertex()
        {
            var r = Instances.region01;
            var s = Instances.state01;
            var e = Instances.vertex01;
            var red = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("TestVertex (" + e.Name + ")");
            Console.WriteLine();

            TestList(e.Incoming, TstConstants.Vertex_Incoming);
            TestList(e.Outgoing, TstConstants.Vertex_Outgoing);
            Assert.AreEqual(e.RedefinitionContext.Name, TstConstants.Vertex_RedefinitionContext);
            Assert.AreEqual(e.ContainingStateMachine().Name, TstConstants.Vertex_ContainingStateMachine);
            Assert.AreEqual(e.IsConsistentWith(red), TstConstants.Vertex_IsConsistentWith);
            Assert.AreEqual(e.IsContainedInRegion(r), TstConstants.Vertex_IsContainedInRegion);
            Assert.AreEqual(e.IsContainedInState(s), TstConstants.Vertex_IsContainedInState);
        }

        public void TestList(IReadOnlyCollection<NamedElementBuilder> list, string[] testList)
        {
            Assert.AreEqual(list.Count, testList.Length);

            int i = 0;
            foreach (var elem in list)
            {
                Assert.AreEqual(elem.Name, testList[i++]);
            }
        }
        public void TestList(IReadOnlyCollection<string> list, string[] testList)
        {
            Assert.AreEqual(list.Count, testList.Length);

            int i = 0;
            foreach (var elem in list)
            {
                Assert.AreEqual(elem, testList[i++]);
            }
        }
    }
}
