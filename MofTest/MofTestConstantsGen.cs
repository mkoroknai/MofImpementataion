using MetaDslx.Modeling;
using MofImplementationLib.Generator;
using MofImplementationLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MofTest
{
    internal static class IEnumerableExtensions
    {
        public static HashSet<T> ToSet<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        }
    }

    class MofTestConstantsGen
    {
        string constants = "";
        TestInstances Instances;

        public MofTestConstantsGen(string mofXmiLocation = "../../../MOF.xmi")
        {
            var xmiSerializer = new MofXmiSerializer();
            var model = xmiSerializer.ReadModelFromFile(mofXmiLocation);


            var mutableGroup = model.ModelGroup.ToMutable();
            var umlModel = mutableGroup.Models.First(m => m.Name.Contains("UML.xmi"));

            Instances = new TestInstances(umlModel);

            constants += "using System;" + Environment.NewLine +
                         "using System.Collections.Generic;" + Environment.NewLine +
                         "using System.Linq;" + Environment.NewLine +
                         "using System.Text;" + Environment.NewLine +
                         "using System.Threading.Tasks;" + Environment.NewLine +
                         Environment.NewLine +
                         "namespace MofTestProjDotNetCore" + Environment.NewLine +
                         "{" + Environment.NewLine +
                         "\tstatic class TestConstants" + Environment.NewLine +
                         "\t{" + Environment.NewLine;

        }

        public void GenerateUmlMethodConstants(string outputFileLocation = "../../../../MofTestDotNetCore/TestConstants.cs")
        {

            GenAssociation();
            GenBehavioralFeature();
            GenBehavior(); // uml does not contain anything of type Behavior
            GenClassifier();
            GenClass();
            GenConnectableElement();
            GenConnectionPointReference(); // uml does not contain anything of type ConnectionPointReference
            GenConnectorEnd();
            GenConnector();
            GenDeploymentTarget();
            GenElementImport();
            GenElement();
            GenEncapsulatedClassifier();
            GenEnumerationLiteral();
            GenExtensionEnd();
            GenExtension();
            GenLiteralBoolean();
            GenLiteralInteger();
            GenLiteralNull();
            GenLiteralReal();
            GenLiteralString();
            GenLiteralUnlimitedNatural();
            GenMultiplicityElement();
            GenNamedElement();
            GenNamespace();
            GenOpaqueExpression();
            GenOperation();
            GenPackage();
            GenParameterableElement();
            GenParameter();
            GenPort();
            GenProperty();
            GenPseudostate();
            GenRedefinableElement();
            GenRegion();
            GenStateMachine();
            GenState();
            GenStereotype();
            GenStringExpression();
            GenStructuredClassifier();
            GenTemplateableElement();
            GenTransition();
            GenType();
            GenUseCase();
            GenValueSpecification();
            GenVertex();

            constants += Environment.NewLine + "\t}" + Environment.NewLine + "}";
            File.WriteAllText(outputFileLocation, constants);
        }

        public void GenAssociation()
        {
            var ass = Instances.association01;

            Console.WriteLine();
            Console.WriteLine("GenAssociation (" + ass.Name + ")");
            Console.WriteLine();


            Console.WriteLine("  EndType");
            Console.WriteLine(CreateFormat(ass.EndType, "Association", "EndType", ass.Name));
        }

        public void GenBehavioralFeature()
        {
            var bfeat = Instances.behavioralfeat01;
            var ne = Instances.namedelement01;

            Console.WriteLine();
            Console.WriteLine("GenBehavioralFeature (" + bfeat.Name + ")");
            Console.WriteLine();


            Console.WriteLine("  InputParameters");
            Console.WriteLine(CreateFormat(bfeat.InputParameters(), "BehavioralFeature", "InputParameters", bfeat.Name));

            Console.WriteLine("  IsDistinguishableFrom");
            Console.WriteLine(CreateFormat(bfeat.IsDistinguishableFrom(ne, ne.Namespace), "BehavioralFeature", "IsDistinguishableFrom", bfeat.Name));

            Console.WriteLine("  OutputParameters");
            Console.WriteLine(CreateFormat(bfeat.OutputParameters(), "BehavioralFeature", "OutputParameters", bfeat.Name));
        }

        public void GenBehavior()
        {
            BehaviorBuilder behavior = Instances.behavior01;
            BehavioredClassifierBuilder testContext01 = Instances.behavioredclassif01;
            var from = Instances.namedelement01;

            Console.WriteLine();
            Console.WriteLine("GenBehavior (" + behavior.Name + ")");
            Console.WriteLine();

            Console.WriteLine("  BehavioredClassifier");
            Console.WriteLine(CreateFormat(behavior.BehavioredClassifier(from).Name, "Behavior", "BehavioredClassifier", behavior.Name));

            //Console.WriteLine("  Context");
            //Console.WriteLine(CreateFormat(behavior.Context.Name, "Behavior", "Context", behavior.Name));

            Console.WriteLine("  InputParameters");
            Console.WriteLine(CreateFormat(behavior.InputParameters(), "Behavior", "InputParameters", behavior.Name));


            Console.WriteLine("  OutputParameters");
            Console.WriteLine(CreateFormat(behavior.OutputParameters(), "Behavior", "OutputParameters", behavior.Name));

            Console.WriteLine("  Context");
            if (behavior.Context == null)
                Console.WriteLine(CreateFormat("null", "Behavior", "Context", behavior.Name));
            else
                Console.WriteLine(CreateFormat(behavior.Context.Name, "Behavior", "Context", behavior.Name));
        }

        public void GenClassifier()
        {
            ClassifierBuilder classifier = Instances.classifier01;
            TypeBuilder other = Instances.type01;
            List<NamedElementBuilder> inhs = Instances.inhs;

            Console.WriteLine();
            Console.WriteLine("GenClassifier (" + classifier.Name + ")");
            Console.WriteLine();


            Console.WriteLine("  AllAttributes");
            Console.WriteLine(CreateFormat(classifier.AllAttributes(), "Classifier", "AllAttributes", classifier.Name));

            Console.WriteLine("  AllFeatures");
            Console.WriteLine(CreateFormat(classifier.AllFeatures(), "Classifier", "AllFeatures", classifier.Name));

            Console.WriteLine("  AllParents");
            Console.WriteLine(CreateFormat(classifier.AllParents(), "Classifier", "AllParents", classifier.Name));

            Console.WriteLine("  AllRealizedInterfaces");
            Console.WriteLine(CreateFormat(classifier.AllRealizedInterfaces(), "Classifier", "AllRealizedInterfaces", classifier.Name));

            Console.WriteLine("  AllSlottableFeatures");
            Console.WriteLine(CreateFormat(classifier.AllSlottableFeatures().Count, "Classifier", "AllSlottableFeatures_Count", classifier.Name));

            Console.WriteLine("  General");

            Console.WriteLine(CreateFormat(classifier.General, "Classifier", "General", classifier.Name));

            Console.WriteLine("  InheritedMember");

            Console.WriteLine(CreateFormat(classifier.InheritedMember, "Classifier", "InheritedMember", classifier.Name));

            Console.WriteLine("  ConformsTo");
            Console.WriteLine(CreateFormat(classifier.ConformsTo(other), "Classifier", "ConformsTo", classifier.Name));

            Console.WriteLine("  DirectlyRealizedInterfaces");
            Console.WriteLine(CreateFormat(classifier.DirectlyRealizedInterfaces(), "Classifier", "DirectlyRealizedInterfaces", classifier.Name));

            Console.WriteLine("  HasVisibilityOf");
            Console.WriteLine(CreateFormat(classifier.HasVisibilityOf(other), "Classifier", "HasVisibilityOf", classifier.Name));

            Console.WriteLine("  Inherit");
            Console.WriteLine(CreateFormat(classifier.Inherit(inhs), "Classifier", "Inherit", classifier.Name));

            Console.WriteLine("  InheritableMembers");
            Console.WriteLine(CreateFormat(classifier.InheritableMembers(other as ClassifierBuilder).Count, "Classifier", "InheritableMembers_Count", classifier.Name));

            Console.WriteLine("  IsSubstitutableFor");
            Console.WriteLine(CreateFormat(classifier.IsSubstitutableFor(other as ClassifierBuilder), "Classifier", "IsSubstitutableFor", classifier.Name));
            Console.WriteLine("  IsTemplate");
            Console.WriteLine(CreateFormat(classifier.IsTemplate(), "Classifier", "IsTemplate", classifier.Name));
            Console.WriteLine("  MaySpecializeType");
            Console.WriteLine(CreateFormat(classifier.MaySpecializeType(other as ClassifierBuilder), "Classifier", "MaySpecializeType", classifier.Name));
            Console.WriteLine("  Parents");
            Console.WriteLine(CreateFormat(classifier.Parents(), "Classifier", "Parents", classifier.Name));
        }


        public void GenClass()
        {
            ClassBuilder class_ = Instances.classifier01 as ClassBuilder;

            Console.WriteLine();
            Console.WriteLine("GenClass (" + class_.Name + ")");
            Console.WriteLine();


            Console.WriteLine("  Extension");
            Console.WriteLine(CreateFormat(class_.Extension, "Class", "Extension", class_.Name));

            Console.WriteLine("  SuperClass");
            Console.WriteLine(CreateFormat(class_.SuperClass, "Class", "SuperClass", class_.Name));
        }

        public void GenConnectableElement()
        {
            var ce = Instances.connectableelement01;

            Console.WriteLine();
            Console.WriteLine("GenConnectableElement (" + ce.Name + ")");
            Console.WriteLine();

            Console.WriteLine("  End");
            Console.WriteLine(CreateFormat(ce.End.Count, "ConnectableElement", "End_Count", ce.Name));
        }

        public void GenConnectionPointReference()
        {
            var cpr = Instances.connectionpointreference01;
            var re = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenConnectionPointReference (" + cpr.Name + ")");
            Console.WriteLine();

            Console.WriteLine("  IsConsistentWith");
            Console.WriteLine(CreateFormat(cpr.IsConsistentWith(re), "ConnectionPointReference", "IsConsistentWith", cpr.Name));
        }

        public void GenConnectorEnd()
        {
            var ce01 = Instances.connectorend01;

            Console.WriteLine();
            Console.WriteLine("GenConnectorEnd (" + ce01.MName + ")");
            Console.WriteLine();

            Console.WriteLine("  DefiningEnd");
            Console.WriteLine(CreateFormat(ce01.DefiningEnd.Name, "ConnectorEnd", "DefiningEnd", ce01.MName));
        }

        void GenConnector()
        {
            var c = Instances.connector01;

            Console.WriteLine();
            Console.WriteLine("GenConnector (" + c.Name + ")");
            Console.WriteLine();


            Console.WriteLine("  Kind");
            Console.WriteLine(CreateFormat(c.Kind.ToString(), "Connector", "Kind", c.Name));
        }

        void GenDeploymentTarget()
        {
            var dt = Instances.connectableelement01 as DeploymentTargetBuilder;

            Console.WriteLine();
            Console.WriteLine("GenDeploymentTarget (" + dt.Name + ")");
            Console.WriteLine();

            Console.WriteLine("  DeployedElement");

            Console.WriteLine(CreateFormat(dt.DeployedElement, "DeploymentTarget", "DeployedElement", dt.Name));
        }
        void GenElementImport()
        {
            var ei = Instances.elementimport01;

            Console.WriteLine();
            Console.WriteLine("GenElementImport (" + ei.Alias + ")");
            Console.WriteLine();

            Console.WriteLine("  GetName");
            Console.WriteLine("    " + ei.GetName());
            Console.WriteLine(CreateFormat(ei.GetName(), "ElementImport", "GetName", ei.GetName()));
        }

        void GenElement()
        {
            var e = Instances.namedelement01 as ElementBuilder;


            Console.WriteLine();
            Console.WriteLine("GenElement (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();


            Console.WriteLine("  AllOwnedElements");
            Console.WriteLine(CreateFormat(e.AllOwnedElements().Count, "Element", "AllOwnedElements_Count", (e as NamedElementBuilder).Name));

            Console.WriteLine(CreateFormat(e.MustBeOwned(), "Element", "MustBeOwned", (e as NamedElementBuilder).Name));
        }

        void GenEncapsulatedClassifier()
        {
            var e = Instances.classifier01 as EncapsulatedClassifierBuilder;

            Console.WriteLine();
            Console.WriteLine("GenEncapsulatedClassifier (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.OwnedPort, "EncapsulatedClassifier", "OwnedPort", e.Name));
        }
        void GenEnumerationLiteral()
        {
            var e = Instances.enumerationliteral01;

            Console.WriteLine();
            Console.WriteLine("GenEnumerationLiteral (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.Classifier.Name, "EnumerationLiteral", "Classifier", e.Name));
        }
        void GenExtensionEnd()
        {
            var e = Instances.extensionend01;

            Console.WriteLine();
            Console.WriteLine("GenExtensionEnd (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.Lower, "ExtensionEnd", "Lower", e.Name));
            Console.WriteLine(CreateFormat(e.LowerBound(), "ExtensionEnd", "LowerBound", e.Name));
        }
        void GenExtension()
        {
            var e = Instances.extension01;

            Console.WriteLine();
            Console.WriteLine("GenExtension (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsRequired, "Extension", "IsRequired", e.Name));
            Console.WriteLine(CreateFormat(e.Metaclass.Name, "Extension", "MetaClass", e.Name));
            Console.WriteLine(CreateFormat(e.MetaclassEnd().Name, "Extension", "MetaClassEnd", e.Name));


        }
        void GenLiteralBoolean()
        {
            var e = Instances.literalboolean01;

            Console.WriteLine();
            Console.WriteLine("GenLiteralBoolean (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.BooleanValue(), "LiteralBoolean", "BooleanValue", e.Name));
            Console.WriteLine(CreateFormat(e.IsComputable(), "LiteralBoolean", "IsComputable", e.Name));
        }

        void GenLiteralInteger()
        {
            var e = Instances.literalinteger01;

            Console.WriteLine();
            Console.WriteLine("GenLiteralInteger (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IntegerValue(), "LiteralInteger", "IntegerValue", e.Name));
            Console.WriteLine(CreateFormat(e.IsComputable(), "LiteralInteger", "IsComputable", e.Name));
        }

        void GenLiteralNull()
        {
            var e = Instances.literalnull01;

            Console.WriteLine();
            Console.WriteLine("GenLiteralNull (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsNull(), "LiteralNull", "IsNull", e.Name));
            Console.WriteLine(CreateFormat(e.IsComputable(), "LiteralNull", "IsComputable", e.Name));
        }

        void GenLiteralReal()
        {
            var e = Instances.literalreal01;

            Console.WriteLine();
            Console.WriteLine("GenLiteralReal (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.RealValue(), "LiteralReal", "RealValue", e.Name));
            Console.WriteLine(CreateFormat(e.IsComputable(), "LiteralReal", "IsComputable", e.Name));
        }

        void GenLiteralString()
        {
            var e = Instances.literalstring01;

            Console.WriteLine();
            Console.WriteLine("GenLiteralString (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.StringValue(), "LiteralString", "StringValue", e.Name));
            Console.WriteLine(CreateFormat(e.IsComputable(), "LiteralString", "IsComputable", e.Name));
        }
        void GenLiteralUnlimitedNatural()
        {
            var e = Instances.literalunlimitednatural01;

            Console.WriteLine();
            Console.WriteLine("GenLiteralUnlimitedNatural (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.UnlimitedValue(), "LiteralUnlimitedNatural", "UnlimitedValue", e.Name));
            Console.WriteLine(CreateFormat(e.IsComputable(), "LiteralUnlimitedNatural", "IsComputable", e.Name));
        }
        void GenMultiplicityElement()
        {
            var me = Instances.multiplicityElement01;

            Console.WriteLine();
            Console.WriteLine("GenMultiplicityElement (" + (me as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(me.CompatibleWith(me), "MultiplicityElement", "CompatibleWith", (me as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(me.Lower, "MultiplicityElement", "Lower", (me as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(me.Upper, "MultiplicityElement", "Upper", (me as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(me.IncludesMultiplicity(me), "MultiplicityElement", "IncludesMultiplicity", (me as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(me.Is(0, 100), "MultiplicityElement", "Is", (me as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(me.IsMultivalued(), "MultiplicityElement", "IsMultivalued", (me as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(me.LowerBound(), "MultiplicityElement", "LowerBound", (me as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(me.UpperBound(), "MultiplicityElement", "UpperBound", (me as NamedElementBuilder).Name));
        }

        void GenNamedElement()
        {
            var e = Instances.namedelement02;

            Console.WriteLine();
            Console.WriteLine("GenNamedElement (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.AllNamespaces(), "NamedElement", "AllNamespaces", e.Name));
            Console.WriteLine(CreateFormat(e.AllOwningPackages(), "NamedElement", "AllOwningPackages", e.Name));
            Console.WriteLine(CreateFormat(e.ClientDependency, "NamedElement", "ClientDependency", e.Name));
            Console.WriteLine(CreateFormat(e.QualifiedName, "NamedElement", "QualifiedName", e.Name));
            Console.WriteLine(CreateFormat(e.IsDistinguishableFrom(e, e.Namespace), "NamedElement", "IsDistinguishableFrom", e.Name));
            Console.WriteLine(CreateFormat(e.Separator(), "NamedElement", "Separator", e.Name));
        }

        void GenNamespace()
        {
            var ns = Instances.namespace01;
            var c = Instances.namedelement01;
            List<PackageableElementBuilder> list = Instances.list01;

            Console.WriteLine();
            Console.WriteLine("GenNamespace (" + ns.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(ns.ImportedMember.ToList(), "Namespace", "ImportedMember", ns.Name));
            Console.WriteLine(CreateFormat(ns.ExcludeCollisions(list), "Namespace", "ExcludeCollisions", ns.Name));
            Console.WriteLine(CreateFormat(ns.GetNamesOfMember(c), "Namespace", "GetNamesOfMember", ns.Name));
            Console.WriteLine(CreateFormat(ns.ImportMembers(list), "Namespace", "ImportMembers", ns.Name));
            Console.WriteLine(CreateFormat(ns.MembersAreDistinguishable(), "Namespace", "MembersAreDistinguishable", ns.Name));
        }

        void GenOpaqueExpression()
        {
            var ee = Instances.opaqueexpression01;

            Console.WriteLine();
            Console.WriteLine("GenOpaqueExpression (" + ee.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(ee.Result.Name, "OpaqueExpression", "Result", ee.Name));
            Console.WriteLine(CreateFormat(ee.IsIntegral(), "OpaqueExpression", "IsIntegral", ee.Name));
            Console.WriteLine(CreateFormat(ee.IsNonNegative(), "OpaqueExpression", "IsNonNegative", ee.Name));
            Console.WriteLine(CreateFormat(ee.IsPositive(), "OpaqueExpression", "IsPositive", ee.Name));
            Console.WriteLine(CreateFormat(ee.Value(), "OpaqueExpression", "Value", ee.Name));
        }
        void GenOperation()
        {
            var e = Instances.behavioralfeat01 as OperationBuilder;
            var red = Instances.classifier01 as RedefinableElementBuilder;

            Console.WriteLine();
            Console.WriteLine("GenOperation (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsOrdered, "Operation", "IsOrdered", e.Name));
            Console.WriteLine(CreateFormat(e.IsUnique, "Operation", "IsUnique", e.Name));
            Console.WriteLine(CreateFormat(e.Lower, "Operation", "Lower", e.Name));
            Console.WriteLine(CreateFormat(e.Type.Name, "Operation", "Type", e.Name));
            Console.WriteLine(CreateFormat(e.Upper, "Operation", "Upper", e.Name));
            Console.WriteLine(CreateFormat(e.IsConsistentWith(red), "Operation", "IsConsistentWith", e.Name));
            Console.WriteLine(CreateFormat(e.ReturnResult(), "Operation", "ReturnResult", e.Name));
        }
        void GenPackage()
        {
            var e = Instances.namedelement02 as PackageBuilder;
            var ne = Instances.namedelement03;

            Console.WriteLine();
            Console.WriteLine("GenPackage (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.AllApplicableStereotypes(), "Package", "AllApplicableStereotypes", e.Name));
            Console.WriteLine(CreateFormat(e.NestedPackage, "Package", "NestedPackage", e.Name));
            Console.WriteLine(CreateFormat(e.OwnedStereotype, "Package", "OwnedStereotype", e.Name));
            Console.WriteLine(CreateFormat(e.OwnedType, "Package", "OwnedType", e.Name));
            Console.WriteLine(CreateFormat(e.ContainingProfile().Name, "Package", "ContainingProfile", e.Name));
            Console.WriteLine(CreateFormat(e.MakesVisible(ne), "Package", "MakesVisible", e.Name));
            Console.WriteLine(CreateFormat(e.MustBeOwned(), "Package", "MustBeOwned", e.Name));
            Console.WriteLine(CreateFormat(e.VisibleMembers().Count, "Package", "VisibleMembers_Count", e.Name));
        }
        void GenParameterableElement()
        {
            var e = Instances.namedelement02 as ParameterableElementBuilder;
            var testPe = Instances.parameterableelement01;

            Console.WriteLine();
            Console.WriteLine("GenParameterableElement (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsCompatibleWith(testPe), "ParameterableElement", "IsCompatibleWith", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsTemplateParameter(), "ParameterableElement", "IsTemplateParameter", (e as NamedElementBuilder).Name));
        }
        void GenParameter()
        {
            var e = Instances.parameter04;

            Console.WriteLine();
            Console.WriteLine("GenParameter (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.Default, "Parameter", "Default", e.Name));
        }
        void GenPort()
        {
            var e = Instances.port01;

            Console.WriteLine();
            Console.WriteLine("GenPort (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.BasicProvided(), "Port", "BasicProvided", (e as NamedElementBuilder).Name));
            //Console.WriteLine(CreateFormat(e.BasicRequired(), "Port", "BasicRequired", (e as NamedElementBuilder).Name)); // UsageBuilder missing
            Console.WriteLine(CreateFormat(e.Provided, "Port", "Provided", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.Required, "Port", "Required", (e as NamedElementBuilder).Name));
        }
        void GenProperty()
        {
            var e = Instances.multiplicityElement01 as PropertyBuilder;
            var peb = Instances.namedelement03 as ParameterableElementBuilder;
            var reb = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenProperty (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsComposite, "Property", "IsComposite", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.Opposite.Name, "Property", "Opposite", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsAttribute(), "Property", "IsAttribute", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsCompatibleWith(peb), "Property", "IsCompatibleWith", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsConsistentWith(reb), "Property", "IsConsistentWith", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsNavigable(), "Property", "IsNavigable", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.SubsettingContext(), "Property", "SubsettingContext", (e as NamedElementBuilder).Name));
        }

        void GenPseudostate()
        {
            var e = Instances.pseudostate01;
            var reb = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenPseudostate (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsConsistentWith(reb), "Pseudostate", "IsConsistentWith", (e as NamedElementBuilder).Name));
        }

        void GenRedefinableElement()
        {
            var e = Instances.classifier01 as RedefinableElementBuilder;
            var reb = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenRedefinableElement (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsRedefinitionContextValid(reb), "RedefinableElement", "IsRedefinitionContextValid", (e as NamedElementBuilder).Name));
        }

        void GenRegion()
        {
            var e = Instances.region01;
            var reb = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenRegion (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.BelongsToPSM(), "Region", "BelongsToPSM", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.RedefinitionContext.Name, "Region", "RedefinitionContext", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.ContainingStateMachine().Name, "Region", "ContainingStateMachine", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsConsistentWith(reb), "Region", "IsConsistentWith", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsRedefinitionContextValid(reb), "Region", "IsRedefinitionContextValid", (e as NamedElementBuilder).Name));
        }
        void GenStateMachine()
        {
            var e = Instances.statemachine01;
            var v1 = Instances.vertex01;
            var v2 = Instances.vertex01;
            var red = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenStateMachine (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.Ancestor(v1, v2), "StateMachine", "Ancestor", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsConsistentWith(red), "StateMachine", "IsConsistentWith", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsRedefinitionContextValid(red), "StateMachine", "IsRedefinitionContextValid", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.LCA(v1, v2).Name, "StateMachine", "LCA", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.LCAState(v1, v2).Name, "StateMachine", "LCAState", (e as NamedElementBuilder).Name));
        }
        void GenState()
        {
            var e = Instances.state01;
            var red = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenState (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsComposite, "State", "IsComposite", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsOrthogonal, "State", "IsOrthogonal", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsSimple, "State", "IsSimple", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsSubmachineState, "State", "IsSubmachineState", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.ContainingStateMachine().Name, "State", "ContainingStateMachine", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsConsistentWith(red), "State", "IsConsistentWith", (e as NamedElementBuilder).Name));
        }
        void GenStereotype()
        {
            var e = Instances.stereotype01;

            Console.WriteLine();
            Console.WriteLine("GenStereotype (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.Profile.Name, "Stereotype", "Profile", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.ContainingProfile().Name, "Stereotype", "ContainingProfile", (e as NamedElementBuilder).Name));
        }
        void GenStringExpression()
        {
            var e = Instances.stringexpression01;

            Console.WriteLine();
            Console.WriteLine("GenStringExpression (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.StringValue(), "StringExpression", "StringValue", (e as NamedElementBuilder).Name));
        }


        void GenStructuredClassifier()
        {
            var e = Instances.classifier01 as StructuredClassifierBuilder;

            Console.WriteLine();
            Console.WriteLine("GenStructuredClassifier (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.AllRoles(), "StructuredClassifier", "AllRoles", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.Part, "StructuredClassifier", "Part", (e as NamedElementBuilder).Name));
        }

        void GenTemplateableElement()
        {
            var e = Instances.namedelement02 as TemplateableElementBuilder;

            Console.WriteLine();
            Console.WriteLine("GenTemplateableElement (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.IsTemplate(), "TemplateableElement", "IsTemplate", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.ParameterableElements().Count, "TemplateableElement", "ParameterableElements_Count", (e as NamedElementBuilder).Name));
        }

        void GenTransition()
        {
            var e = Instances.transition01;
            var red = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenTransition (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.RedefinitionContext.Name, "Transition", "RedefinitionContext", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.ContainingStateMachine().Name, "Transition", "ContainingStateMachine", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsConsistentWith(red), "Transition", "IsConsistentWith", (e as NamedElementBuilder).Name));
        }
        void GenType()
        {
            var e = Instances.type01;
            var t = Instances.statemachine01;

            Console.WriteLine();
            Console.WriteLine("GenType (" + (e as NamedElementBuilder).Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.ConformsTo(t), "Type", "ConformsTo", (e as NamedElementBuilder).Name));
        }
        void GenUseCase()
        {
            var e = Instances.usecase01;

            Console.WriteLine();
            Console.WriteLine("GenUseCase (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.AllIncludedUseCases(), "UseCase", "AllIncludedUseCases", (e as NamedElementBuilder).Name));
        }

        void GenValueSpecification()
        {
            var e = Instances.valuespecification01;

            Console.WriteLine();
            Console.WriteLine("GenValueSpecification (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.BooleanValue(), "ValueSpecification", "BooleanValue", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IntegerValue(), "ValueSpecification", "IntegerValue", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsCompatibleWith(e), "ValueSpecification", "IsCompatibleWith", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsComputable(), "ValueSpecification", "IsComputable", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsNull(), "ValueSpecification", "IsNull", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.RealValue(), "ValueSpecification", "RealValue", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.StringValue(), "ValueSpecification", "StringValue", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.UnlimitedValue(), "ValueSpecification", "UnlimitedValue", (e as NamedElementBuilder).Name));
        }
        void GenVertex()
        {
            var r = Instances.region01;
            var s = Instances.state01;
            var e = Instances.vertex01;
            var red = Instances.redefinableelement01;

            Console.WriteLine();
            Console.WriteLine("GenVertex (" + e.Name + ")");
            Console.WriteLine();

            Console.WriteLine(CreateFormat(e.Incoming, "Vertex", "Incoming", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.Outgoing, "Vertex", "Outgoing", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.RedefinitionContext.Name, "Vertex", "RedefinitionContext", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.ContainingStateMachine().Name, "Vertex", "ContainingStateMachine", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsConsistentWith(red), "Vertex", "IsConsistentWith", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsContainedInRegion(r), "Vertex", "IsContainedInRegion", (e as NamedElementBuilder).Name));
            Console.WriteLine(CreateFormat(e.IsContainedInState(s), "Vertex", "IsContainedInState", (e as NamedElementBuilder).Name));
        }


        /// <summary>
        /// String format methods-----------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="list"></param>
        /// <param name="elemName"></param>
        /// <param name="funcName"></param>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        string CreateFormat(IReadOnlyCollection<NamedElementBuilder> list, string elemName, string funcName, string instanceName)
        {
            string tobePrinted =
                "\t\tpublic static readonly string[] " + elemName + "_" + funcName + " =" + Environment.NewLine +
                "\t\t{" + Environment.NewLine;

            foreach (var elem in list)
            {
                tobePrinted += "\t\t\t\"" + elem.Name + "\"," + Environment.NewLine;
            }

            tobePrinted += "\t\t}; // (" + instanceName + ")" + Environment.NewLine
                + Environment.NewLine;

            constants += tobePrinted;

            return tobePrinted;
        }
        string CreateFormat(IReadOnlyCollection<string> list, string elemName, string funcName, string instanceName)
        {
            string tobePrinted =
                "\t\tpublic static readonly string[] " + elemName + "_" + funcName + " =" + Environment.NewLine +
                "\t\t{" + Environment.NewLine;

            foreach (var elem in list)
            {
                tobePrinted += "\t\t\t\"" + elem + "\"," + Environment.NewLine;
            }

            tobePrinted += "\t\t}; // (" + instanceName + ")" + Environment.NewLine
                + Environment.NewLine;

            constants += tobePrinted;

            return tobePrinted;
        }
        string CreateFormat(IReadOnlyCollection<ElementBuilder> list, string elemName, string funcName, string instanceName)
        {
            string tobePrinted =
                "\t\tpublic static readonly string[] " + elemName + "_" + funcName + " =" + Environment.NewLine +
                "\t\t{" + Environment.NewLine;

            foreach (var elem in list)
            {
                if (elem is NamedElementBuilder ne)
                {
                    tobePrinted += "\t\t\t\"" + ne.Name + "\"," + Environment.NewLine;
                }
                else
                    tobePrinted += "\t\t\t\"" + elem.MName + "\"," + Environment.NewLine;
            }

            tobePrinted += "\t\t}; // (" + instanceName + ")" + Environment.NewLine
                + Environment.NewLine;

            constants += tobePrinted;

            return tobePrinted;
        }

        string CreateFormat(int value, string elemName, string funcName, string instanceName)
        {
            string tobePrinted =
            //       "\t\tpublic static readonly int " + elemName + "_" + funcName + "_Count = " + length + "; // (" + instanceName + ")" + Environment.NewLine;
                   "\t\tpublic static readonly int " + elemName + "_" + funcName + " = " + value + "; // (" + instanceName + ")" + Environment.NewLine;

            tobePrinted += Environment.NewLine;

            constants += tobePrinted;

            return tobePrinted;
        }
        string CreateFormat(double value, string elemName, string funcName, string instanceName)
        {
            string tobePrinted =
            //       "\t\tpublic static readonly int " + elemName + "_" + funcName + "_Count = " + length + "; // (" + instanceName + ")" + Environment.NewLine;
                   "\t\tpublic static readonly double " + elemName + "_" + funcName + " = " + value + "; // (" + instanceName + ")" + Environment.NewLine;

            tobePrinted += Environment.NewLine;

            constants += tobePrinted;

            return tobePrinted;
        }

        string CreateFormat(string attrib, string elemName, string funcName, string instanceName)
        {
            string tobePrinted = "";
            if (attrib != null)
                tobePrinted =
                    "\t\tpublic static readonly string " + elemName + "_" + funcName + " = \"" + attrib + "\"; // (" + instanceName + ")" + Environment.NewLine;
            else
                tobePrinted =
                    "\t\tpublic static readonly string " + elemName + "_" + funcName + " = null; // (" + instanceName + ")" + Environment.NewLine;

            tobePrinted += Environment.NewLine;

            constants += tobePrinted;

            return tobePrinted;
        }

        string CreateFormat(bool attrib, string elemName, string funcName, string instanceName)
        {
            string attstring = attrib ? "true" : "false";
            string tobePrinted =
                   "\t\tpublic static readonly bool " + elemName + "_" + funcName + " = " + attstring + "; // (" + instanceName + ")" + Environment.NewLine;

            tobePrinted += Environment.NewLine;

            constants += tobePrinted;

            return tobePrinted;
        }
    }
}
