using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MofTestProjDotNetCore
{
	static class TstConstants
	{
		public static readonly string[] Association_EndType =
		{
			"ObjectNode",
			"Behavior",
		}; // (A_selection_objectNode)

		public static readonly string[] BehavioralFeature_InputParameters =
		{
			"redefiningElement",
		}; // (isConsistentWith)

		public static readonly bool BehavioralFeature_IsDistinguishableFrom = true; // (isConsistentWith)

		public static readonly string[] BehavioralFeature_OutputParameters =
		{
			"result",
		}; // (isConsistentWith)

		public static readonly string Behavior_BehavioredClassifier = "Class"; // (BehaviorTestInstance)

		public static readonly string[] Behavior_InputParameters =
		{
			"testParameter01",
			"testParameter03",
		}; // (BehaviorTestInstance)

		public static readonly string[] Behavior_OutputParameters =
		{
			"testParameter02",
			"testParameter03",
		}; // (BehaviorTestInstance)

		public static readonly string Behavior_Context = "null"; // (BehaviorTestInstance)

		public static readonly string[] Classifier_AllAttributes =
		{
			"edge",
			"group",
			"isReadOnly",
			"isSingleExecution",
			"node",
			"partition",
			"structuredNode",
			"variable",
		}; // (Activity)

		public static readonly string[] Classifier_AllFeatures =
		{
			"edge",
			"group",
			"isReadOnly",
			"isSingleExecution",
			"node",
			"partition",
			"structuredNode",
			"variable",
		}; // (Activity)

		public static readonly string[] Classifier_AllParents =
		{
			"Behavior",
			"Class",
			"BehavioredClassifier",
			"EncapsulatedClassifier",
			"Classifier",
			"Namespace",
			"Type",
			"TemplateableElement",
			"RedefinableElement",
			"NamedElement",
			"Element",
			"PackageableElement",
			"ParameterableElement",
			"StructuredClassifier",
		}; // (Activity)

		public static readonly string[] Classifier_AllRealizedInterfaces =
		{
		}; // (Activity)

		public static readonly int Classifier_AllSlottableFeatures_Count = 72; // (Activity)

		public static readonly string[] Classifier_General =
		{
			"Behavior",
		}; // (Activity)

		public static readonly string[] Classifier_InheritedMember =
		{
			"most_one_behavior",
			"parameters_match",
			"feature_of_context_classifier",
			"passive_class",
			"class_behavior",
			"specialize_type",
			"maps_to_generalization_set",
			"non_final_parents",
			"no_cycles_in_generalization",
			"members_distinguishable",
			"cannot_import_self",
			"cannot_import_ownedMembers",
			"visibility_needs_ownership",
			"has_qualified_name",
			"has_no_qualified_name",
			"has_owner",
			"not_own_self",
			"namespace_needs_visibility",
			"redefinition_consistent",
			"non_leaf_redefinition",
			"redefinition_context_valid",
		}; // (Activity)

		public static readonly bool Classifier_ConformsTo = true; // (Activity)

		public static readonly string[] Classifier_DirectlyRealizedInterfaces =
		{
		}; // (Activity)

		public static readonly bool Classifier_HasVisibilityOf = true; // (Activity)

		public static readonly string[] Classifier_Inherit =
		{
			"Activities",
		}; // (Activity)

		public static readonly int Classifier_InheritableMembers_Count = 31; // (Activity)

		public static readonly bool Classifier_IsSubstitutableFor = false; // (Activity)

		public static readonly bool Classifier_IsTemplate = false; // (Activity)

		public static readonly bool Classifier_MaySpecializeType = true; // (Activity)

		public static readonly string[] Classifier_Parents =
		{
			"Behavior",
		}; // (Activity)

		public static readonly string[] Class_Extension =
		{
		}; // (Activity)

		public static readonly string[] Class_SuperClass =
		{
			"Behavior",
		}; // (Activity)

		public static readonly int ConnectableElement_End_Count = 0; // (variable)

		public static readonly bool ConnectionPointReference_IsConsistentWith = false; // (testConnectionPointReference01)

		public static readonly string ConnectorEnd_DefiningEnd = "variable"; // ()

		public static readonly string Connector_Kind = "Assembly"; // (testConnector01)

		public static readonly string[] DeploymentTarget_DeployedElement =
		{
		}; // (variable)

		public static readonly string ElementImport_GetName = "testElementImport01"; // (testElementImport01)

		public static readonly int Element_AllOwnedElements_Count = 38; // (Class)

		public static readonly bool Element_MustBeOwned = true; // (Class)

		public static readonly string[] EncapsulatedClassifier_OwnedPort =
		{
		}; // (Activity)

		public static readonly string EnumerationLiteral_Classifier = "testEnumeration69"; // ()

		public static readonly int ExtensionEnd_Lower = 0; // (testExtensionEnd01)

		public static readonly int ExtensionEnd_LowerBound = 0; // (testExtensionEnd01)

		public static readonly bool Extension_IsRequired = false; // (testExtension01)

		public static readonly string Extension_MetaClass = "testType01"; // (testExtension01)

		public static readonly string Extension_MetaClassEnd = "testExtensionEnd03"; // (testExtension01)

		public static readonly bool LiteralBoolean_BooleanValue = false; // (testLiteralBoolean01)

		public static readonly bool LiteralBoolean_IsComputable = true; // (testLiteralBoolean01)

		public static readonly int LiteralInteger_IntegerValue = 0; // (testLiteralInteger01)

		public static readonly bool LiteralInteger_IsComputable = true; // (testLiteralInteger01)

		public static readonly bool LiteralNull_IsNull = true; // (testLiteralNull01)

		public static readonly bool LiteralNull_IsComputable = true; // (testLiteralNull01)

		public static readonly double LiteralReal_RealValue = 1.02045; // (testLiteralReal01)

		public static readonly bool LiteralReal_IsComputable = true; // (testLiteralReal01)

		public static readonly string LiteralString_StringValue = "this is a test"; // (testLiteralString01)

		public static readonly bool LiteralString_IsComputable = true; // (testLiteralString01)

		public static readonly double LiteralUnlimitedNatural_UnlimitedValue = 99999999919; // (testLiteralUnlimitedNatural01)

		public static readonly bool LiteralUnlimitedNatural_IsComputable = true; // (testLiteralUnlimitedNatural01)

		public static readonly bool MultiplicityElement_CompatibleWith = true; // (edge)

		public static readonly int MultiplicityElement_Lower = 0; // (edge)

		public static readonly double MultiplicityElement_Upper = -1; // (edge)

		public static readonly bool MultiplicityElement_IncludesMultiplicity = true; // (edge)

		public static readonly bool MultiplicityElement_Is = false; // (edge)

		public static readonly bool MultiplicityElement_IsMultivalued = true; // (edge)

		public static readonly int MultiplicityElement_LowerBound = 0; // (edge)

		public static readonly double MultiplicityElement_UpperBound = -1; // (edge)

		public static readonly string[] NamedElement_AllNamespaces =
		{
			"testProfile01",
		}; // (UML)

		public static readonly string[] NamedElement_AllOwningPackages =
		{
			"testProfile01",
		}; // (UML)

		public static readonly string[] NamedElement_ClientDependency =
		{
		}; // (UML)

		public static readonly string NamedElement_QualifiedName = "testProfile01::UML"; // (UML)

		public static readonly bool NamedElement_IsDistinguishableFrom = true; // (UML)

		public static readonly string NamedElement_Separator = "::"; // (UML)

		public static readonly string[] Namespace_ImportedMember =
		{
		}; // ()

		public static readonly string[] Namespace_ExcludeCollisions =
		{
			"testPackageableElement01",
			"A_selection_objectNode",
		}; // ()

		public static readonly string[] Namespace_GetNamesOfMember =
		{
		}; // ()

		public static readonly string[] Namespace_ImportMembers =
		{
			"testPackageableElement01",
			"A_selection_objectNode",
		}; // ()

		public static readonly bool Namespace_MembersAreDistinguishable = true; // ()

		public static readonly string OpaqueExpression_Result = "testParameter04"; // (testOpaqueExpression01)

		public static readonly bool OpaqueExpression_IsIntegral = false; // (testOpaqueExpression01)

		public static readonly bool OpaqueExpression_IsNonNegative = false; // (testOpaqueExpression01)

		public static readonly bool OpaqueExpression_IsPositive = false; // (testOpaqueExpression01)

		public static readonly int OpaqueExpression_Value = 0; // (testOpaqueExpression01)

		public static readonly bool Operation_IsOrdered = false; // (isConsistentWith)

		public static readonly bool Operation_IsUnique = false; // (isConsistentWith)

		public static readonly int Operation_Lower = 1; // (isConsistentWith)

		public static readonly string Operation_Type = "Boolean"; // (isConsistentWith)

		public static readonly double Operation_Upper = 1; // (isConsistentWith)

		public static readonly bool Operation_IsConsistentWith = false; // (isConsistentWith)

		public static readonly string[] Operation_ReturnResult =
		{
			"result",
		}; // (isConsistentWith)

		public static readonly string[] Package_AllApplicableStereotypes =
		{
		}; // (UML)

		public static readonly string[] Package_NestedPackage =
		{
			"Activities",
			"Values",
			"UseCases",
			"StructuredClassifiers",
			"StateMachines",
			"SimpleClassifiers",
			"Packages",
			"Interactions",
			"InformationFlows",
			"Deployments",
			"CommonStructure",
			"CommonBehavior",
			"Classification",
			"Actions",
		}; // (UML)

		public static readonly string[] Package_OwnedStereotype =
		{
		}; // (UML)

		public static readonly string[] Package_OwnedType =
		{
		}; // (UML)

		public static readonly string Package_ContainingProfile = "testProfile01"; // (UML)

		public static readonly bool Package_MakesVisible = false; // (UML)

		public static readonly bool Package_MustBeOwned = false; // (UML)

		public static readonly int Package_VisibleMembers_Count = 687; // (UML)

		public static readonly bool ParameterableElement_IsCompatibleWith = false; // (UML)

		public static readonly bool ParameterableElement_IsTemplateParameter = false; // (UML)

		public static readonly string Parameter_Default = null; // (result)

		public static readonly string[] Port_BasicProvided =
		{
		}; // (testPort01)

		public static readonly string[] Port_Provided =
		{
		}; // (testPort01)

		public static readonly string[] Port_Required =
		{
		}; // (testPort01)

		public static readonly bool Property_IsComposite = true; // (edge)

		public static readonly string Property_Opposite = "activity"; // (edge)

		public static readonly bool Property_IsAttribute = false; // (edge)

		public static readonly bool Property_IsCompatibleWith = false; // (edge)

		public static readonly bool Property_IsConsistentWith = false; // (edge)

		public static readonly bool Property_IsNavigable = false; // (edge)

		public static readonly string[] Property_SubsettingContext =
		{
			"Activity",
		}; // (edge)

		public static readonly bool Pseudostate_IsConsistentWith = false; // (testPseudostate01)

		public static readonly bool RedefinableElement_IsRedefinitionContextValid = false; // (Activity)

		public static readonly bool Region_BelongsToPSM = false; // (testRegion01)

		public static readonly string Region_RedefinitionContext = "testStateMachine01"; // (testRegion01)

		public static readonly string Region_ContainingStateMachine = "testStateMachine01"; // (testRegion01)

		public static readonly bool Region_IsConsistentWith = true; // (testRegion01)

		public static readonly bool Region_IsRedefinitionContextValid = false; // (testRegion01)

		public static readonly bool StateMachine_Ancestor = true; // (testStateMachine02)

		public static readonly bool StateMachine_IsConsistentWith = true; // (testStateMachine02)

		public static readonly bool StateMachine_IsRedefinitionContextValid = false; // (testStateMachine02)

		public static readonly string StateMachine_LCA = "testRegion02"; // (testStateMachine02)

		public static readonly string StateMachine_LCAState = "testState01"; // (testStateMachine02)

		public static readonly bool State_IsComposite = false; // (testState01)

		public static readonly bool State_IsOrthogonal = false; // (testState01)

		public static readonly bool State_IsSimple = true; // (testState01)

		public static readonly bool State_IsSubmachineState = false; // (testState01)

		public static readonly string State_ContainingStateMachine = "testStateMachine01"; // (testState01)

		public static readonly bool State_IsConsistentWith = false; // (testState01)

		public static readonly string Stereotype_Profile = "testProfile02"; // (testStereotype01)

		public static readonly string Stereotype_ContainingProfile = "testProfile02"; // (testStereotype01)

		public static readonly string StringExpression_StringValue = "[StringExpression][StringExpression]"; // (testStringExpression01)

		public static readonly string[] StructuredClassifier_AllRoles =
		{
			"edge",
			"group",
			"isReadOnly",
			"isSingleExecution",
			"node",
			"partition",
			"structuredNode",
			"variable",
		}; // (Activity)

		public static readonly string[] StructuredClassifier_Part =
		{
			"edge",
			"group",
			"node",
			"structuredNode",
			"variable",
		}; // (Activity)

		public static readonly bool TemplateableElement_IsTemplate = false; // (UML)

		public static readonly int TemplateableElement_ParameterableElements_Count = 4840; // (UML)

		public static readonly string Transition_RedefinitionContext = "testStateMachine01"; // (testTransition01)

		public static readonly string Transition_ContainingStateMachine = "testStateMachine01"; // (testTransition01)

		public static readonly bool Transition_IsConsistentWith = false; // (testTransition01)

		public static readonly bool Type_ConformsTo = false; // (Activity)

		public static readonly string[] UseCase_AllIncludedUseCases =
		{
			"testUseCase02",
		}; // (testUseCase01)

		public static readonly bool ValueSpecification_BooleanValue = false; // (testLiteralReal02)

		public static readonly int ValueSpecification_IntegerValue = 0; // (testLiteralReal02)

		public static readonly bool ValueSpecification_IsCompatibleWith = false; // (testLiteralReal02)

		public static readonly bool ValueSpecification_IsComputable = true; // (testLiteralReal02)

		public static readonly bool ValueSpecification_IsNull = false; // (testLiteralReal02)

		public static readonly double ValueSpecification_RealValue = 69.6969; // (testLiteralReal02)

		public static readonly string ValueSpecification_StringValue = null; // (testLiteralReal02)

		public static readonly double ValueSpecification_UnlimitedValue = 0; // (testLiteralReal02)

		public static readonly string[] Vertex_Incoming =
		{
		}; // (testVertex01)

		public static readonly string[] Vertex_Outgoing =
		{
		}; // (testVertex01)

		public static readonly string Vertex_RedefinitionContext = "testStateMachine01"; // (testVertex01)

		public static readonly string Vertex_ContainingStateMachine = "testStateMachine01"; // (testVertex01)

		public static readonly bool Vertex_IsConsistentWith = false; // (testVertex01)

		public static readonly bool Vertex_IsContainedInRegion = false; // (testVertex01)

		public static readonly bool Vertex_IsContainedInState = false; // (testVertex01)


	}
}