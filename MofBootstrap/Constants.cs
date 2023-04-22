using System;
using System.Collections.Generic;
using System.Text;

namespace MofBootstrap
{
    public static class Constants
    {
        public static readonly string[] UML_CLASSES_TO_BE_MERGED_INTO_EMOF =
        {
            // based on Mof2.5.1 specification, section 12.4
            "Association",
            "Class",
            "Comment",
            "DataType",
            "Enumeration",
            "EnumerationLiteral",
            "Generalization",
            "InstanceSpecification",
            "InstanceValue",
            "LiteralBoolean",
            "LiteralInteger",
            "LiteralNull",
            "LiteralReal",
            "LiteralString",
            "LiteralUnlimitedNatural",
            "Operation",
            "Package",
            "Parameter",
            "PrimitiveType",
            "Property",
            "Slot"
        };

        public static readonly string[] UML_CLASS_PROPERTIES_TO_BE_EMPTY_EMOF =
        {
            // based on Mof2.5.1 specification, section 12.4
            // starting 0, if element number is even, element is class
            // and the following element is its the property
            "Association",
            "navigableOwnedEnd",
            "Class",
            "nestedClassifier",
            //"Classifier", // / general for instances of Datatype ??
            "Operation",
            "bodyCondition",
            "Operation",
            "postcondition",
            "Operation",
            "precondition",
            "Operation",
            "redefinedOperation",
            "Parameter",
            "defaultValue",
            "Property",
            "qualifier",
            "Property",
            "redefinedProperty",
            "Property",
            "subsettedProperty"
        };

        public static readonly string[] UML_CLASS_PROPERTIES_TO_BE_FALSE_EMOF =
        {
            // based on Mof2.5.1 specification, section 12.4
            // starting 0, if element number is even, element is class
            // and the following element is its the property
            "Association",
            "isDerived",
            "Classifier",
            "isFinalSpecialization",
            "Feature",
            "isStatic",
            "Property",
            "isDerivedUnion",
            "RedefinableElement",
            "isLeaf"
        };

        public static readonly string[] UML_CLASSES_TO_BE_MERGED_INTO_CMOF =
        {
            // based on Mof2.5.1 specification, section 14.4
            "Association",
            "Class",
            "Comment",
            "Constraint",
            "DataType",
            "ElementImport",
            "Enumeration",
            "EnumerationLiteral",
            "Generalization",
            "InstanceSpecification",
            "InstanceValue",
            "LiteralBoolean",
            "LiteralInteger",
            "LiteralNull",
            "LiteralReal",
            "LiteralString",
            "LiteralUnlimitedNatural",
            "OpaqueExpression",
            "Operation",
            "Package",
            "PackageImport",
            "PackageMerge",
            "Parameter",
            "PrimitiveType",
            "Property",
            "Slot"
        };

        public static readonly string[] UML_CLASS_PROPERTIES_TO_BE_EMPTY_CMOF =
        {
            // based on Mof2.5.1 specification, section 12.4
            // starting 0, if element number is even, element is class
            // and the following element is its the property
            "Class",
            "nestedClassifier",
            "Property",
            "qualifier"
        };

        public static readonly string[] UML_CLASS_PROPERTIES_TO_BE_FALSE_CMOF =
        {
            // based on Mof2.5.1 specification, section 12.4
            // starting 0, if element number is even, element is class
            // and the following element is its the property
            "Feature",
            "isStatic"
        };
    }
}
