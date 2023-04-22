using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MofTestProj
    {
        static class TestConstants
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

        public static readonly string[] BehavioralFeature_OutputParameters =
        {
            "result",
        }; // (isConsistentWith)

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

        public static readonly string[] Classifier_AllSlottableFeatures =
        {
            "edge",
            "group",
            "isReadOnly",
            "isSingleExecution",
            "node",
            "partition",
            "structuredNode",
            "variable",
            "context",
            "isReentrant",
            "ownedParameter",
            "ownedParameterSet",
            "postcondition",
            "precondition",
            "specification",
            "redefinedBehavior",
            "extension",
            "isAbstract",
            "isActive",
            "nestedClassifier",
            "ownedAttribute",
            "ownedOperation",
            "ownedReception",
            "superClass",
            "classifierBehavior",
            "interfaceRealization",
            "ownedBehavior",
            "ownedPort",
            "attribute",
            "collaborationUse",
            "feature",
            "general",
            "generalization",
            "inheritedMember",
            "isAbstract",
            "isFinalSpecialization",
            "ownedTemplateSignature",
            "ownedUseCase",
            "powertypeExtent",
            "redefinedClassifier",
            "representation",
            "substitution",
            "templateParameter",
            "useCase",
            "elementImport",
            "importedMember",
            "member",
            "ownedMember",
            "ownedRule",
            "packageImport",
            "package",
            "ownedTemplateSignature",
            "templateBinding",
            "isLeaf",
            "redefinedElement",
            "redefinitionContext",
            "clientDependency",
            "name",
            "nameExpression",
            "namespace",
            "qualifiedName",
            "visibility",
            "ownedComment",
            "ownedElement",
            "owner",
            "visibility",
            "owningTemplateParameter",
            "templateParameter",
            "ownedAttribute",
            "ownedConnector",
            "part",
            "role",
        }; // (Activity)

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

        public static readonly string[] Classifier_DirectlyRealizedInterfaces =
        {
        }; // (Activity)

        public static readonly string[] Classifier_Inherit =
        {
        }; // (Activity)

        public static readonly string[] Classifier_InheritableMembers =
        {
            "maximum_one_parameter_node",
            "maximum_two_parameter_nodes",
            "edge",
            "group",
            "isReadOnly",
            "isSingleExecution",
            "node",
            "partition",
            "structuredNode",
            "variable",
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

        public static readonly string[] DeploymentTarget_DeployedElement =
        {
        }; // (edge)

        public static readonly int Element_AllOwnedElements_Count = 38; // (Class)


}
}