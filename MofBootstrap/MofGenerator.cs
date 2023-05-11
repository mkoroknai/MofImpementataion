using MetaDslx.Modeling;
using MofBootstrapLib.Generator;
using MofBootstrapLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


// copy this to the beginning of Mof.mm without the first two slashes:
///// <summary>
///// Boolean is used for logical expressions, consisting of the predefined values true and false.
///// </summary>
//const PrimitiveType Boolean;
///// <summary>
///// Integer is a primitive type representing integer values.
///// </summary>
//const PrimitiveType Integer;
///// <summary>
///// Real is a primitive type representing the mathematical concept of real.
///// </summary>
//const PrimitiveType Real;
///// <summary>
///// String is a sequence of characters in some suitable character set used to display information about the model. Character sets may include non-Roman alphabets and characters.
///// </summary>
//const PrimitiveType String;
///// <summary>
///// UnlimitedNatural is a primitive type representing unlimited natural values.
///// </summary>
//const PrimitiveType UnlimitedNatural;

// copy this into ConnectorEnd in Mof.mm without the first two slashes:
///// <summary>
///// The Connector of which the ConnectorEnd is the endpoint.
///// </summary>
//Connector Connector subsets Element.Owner;
///// <summary>

namespace MofBootstrap
{
    class MofGenerator
    {

        MutableModel MofModel;
        PackageBuilder MofReflection;
        PackageBuilder MofExtension;
        PackageBuilder CmofReflection;
        PackageBuilder CmofExtension;
        PackageBuilder MofCommon;
        PackageBuilder MofEmof;
        PackageBuilder MofIdentifiers;
        PackageBuilder Cmof;

        MutableModel UmlModel;

        MofFactory MofFactory { get; }


        public MofGenerator(ImmutableModel model)
        {
            MofModel = model.ToMutable();
            var mutableGroup = model.ModelGroup.ToMutable();
            UmlModel = mutableGroup.Models.First(m => m.Name.Contains("UML.xmi"));

            MofFactory = new MofFactory(MofModel, ModelFactoryFlags.DontMakeObjectsCreated);

            // grabbing packages
            MofReflection = MofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Reflection");
            MofExtension = MofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Extension");
            CmofReflection = MofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFReflection");
            CmofExtension = MofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFExtension");
            MofCommon = MofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Common");
            MofEmof = MofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "EMOF");
            MofIdentifiers = MofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Identifiers");
            Cmof = MofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOF");

            // renaming conflicting element names like string, object, etc.
            RenameMofProperties();
        }

        public void GenerateEssentialMof(bool removeUnknownTypedElements,
                                         string fileNameForGeneratedModel = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            //Console.WriteLine();
            //Console.WriteLine("-----------------");
            //Console.WriteLine();
            
            //Console.WriteLine("Merge EMOF parts of UML into MOF::Reflection");

            Dictionary<MutableObject, MutableObject> umlToMof = MergeUmlToMofReflectionEmof();

            // need to merge MOF::CMOFReflection::Argument into MOF::Reflection, because of class Object
            MergeArgumentIntoMof();

            Console.WriteLine();
            // Merge MOF::Reflection into MOF::Extension ------------------------------------------------------------------------------
            MergeMofReflectionIntoMofExtension();

            // Merge MOF::Common into MOF::EMOF ---------------------------------------------------------------------------------------
            MergeMofCommonIntoMofEmof();

            // Merge MOF::Identifiers into MOF::EMOF ----------------------------------------------------------------------------------
            MergeMofIdentifiersIntoMofEmof();

            // Merge MOF::Reflection into MOF::EMOF -----------------------------------------------------------------------------------
            MergeMofReflectionIntoMofEmof();

            // Merge MOF::Extension into MOF::EMOF ------------------------------------------------------------------------------------
            MergeMofExtensionIntoMofEmof();

            if (removeUnknownTypedElements)
            {
                RemoveUnknownTypedElementsFromEmof(umlToMof);
            }
            else
            {
                AddAllReferencedClassesToEmof(umlToMof);
            }

            AddAssociationsFromUmlToEmof();

            Console.WriteLine();
            Console.WriteLine("Essential MOF generated.");

            GenerateEmofmmFile(fileNameForGeneratedModel);
        }

        /// <summary>
        /// Creates Mof.mm file that contains CMOF model
        /// if first input parameter is true, all the variables with unknown type,
        /// or functions that have parameters with unknown type
        /// (i.e. the definition of their type does not exist in MOF model)
        /// will be removed
        /// if first input parameter is false, all the metaclasses/enums will be added
        /// </summary>
        /// <param name="removeUnknownTypedElements">see above</param>
        /// <param name="fileNameForGeneratedModel">folder and name of .mm file</param>
        public void GenerateCompleteMof(bool removeUnknownTypedElements,
                                        string fileNameForGeneratedModel = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            Dictionary<MutableObject, MutableObject> umlToMof = MergeUmlToMofReflectionCmof();

            // need to merge MOF::CMOFReflection::Argument into MOF::Reflection, because of class Object
            MergeArgumentIntoMof();

            // Merge MOF::Reflection into MOF::Extension ------------------------------------------------------------------------------
            MergeMofReflectionIntoMofExtension();

            // Merge MOF::Common into MOF::EMOF ---------------------------------------------------------------------------------------
            MergeMofCommonIntoMofEmof();

            // Merge MOF::Identifiers into MOF::EMOF ----------------------------------------------------------------------------------
            MergeMofIdentifiersIntoMofEmof();

            // Merge MOF::Reflection into MOF::EMOF -----------------------------------------------------------------------------------
            MergeMofReflectionIntoMofEmof();

            // Merge MOF::Extension into MOF::EMOF ------------------------------------------------------------------------------------
            MergeMofExtensionIntoMofEmof();

            // Merge MOF::Reflection into MOF::CMOFReflection -------------------------------------------------------------------------
            MergeMofReflectionIntoMofCmofReflection();

            // Merge MOF::Extension into MOF::CMOFExtension ---------------------------------------------------------------------------
            MergeMofExtensionIntoMofCmofExtension();

            // Merge MOF::EMOF into MOF::CMOF -----------------------------------------------------------------------------------------
            MergeMofEmofIntoMofCmof();

            // Merge MOF::CMOFExtension into MOF::CMOF --------------------------------------------------------------------------------
            MergeMofCmofExtensionIntoMofCmof();

            // Merge MOF::CMOFReflection into MOF::CMOF -------------------------------------------------------------------------------
            MergeMofCmofReflectionIntoMofCmof();

            if (removeUnknownTypedElements)
            {
                RemoveUnknownTypedElementsFromCmof(umlToMof);
            }
            else
            {
                AddAllReferencedClassesToCmof(umlToMof);
            }

            AddAssociationsFromUmlToCmof();

            foreach(var a in Cmof.PackagedElement.OfType<AssociationBuilder>())
            {
                Console.WriteLine("\t\t" + a.Name + " member end count: " + a.MemberEnd.Count);
            }


            Console.WriteLine();
            Console.WriteLine("Complete MOF generated.");

            GenerateCmofmmFile(fileNameForGeneratedModel);
        }

        public Dictionary<MutableObject, MutableObject> MergeUmlToMofReflectionEmof()
        {
            return UmlToMof.UmlToMofReflectionEmof(UmlModel, MofReflection, MofFactory);
        }

        public Dictionary<MutableObject, MutableObject> MergeUmlToMofReflectionCmof()
        {
            return UmlToMof.UmlToMofReflectionCmof(UmlModel, MofReflection, MofFactory);
        }

        public void MergeMofReflectionIntoMofExtension()
        {
            MergeHelper.MergePackages(MofExtension, MofReflection, MofFactory);
        }

        public void MergeMofCommonIntoMofEmof()
        {
            MergeHelper.MergePackages(MofEmof, MofCommon, MofFactory);
        }

        public void MergeMofIdentifiersIntoMofEmof()
        {
            MergeHelper.MergePackages(MofEmof, MofIdentifiers, MofFactory);
        }

        public void MergeMofReflectionIntoMofEmof()
        {
            MergeHelper.MergePackages(MofEmof, MofReflection, MofFactory);
        }

        public void MergeMofExtensionIntoMofEmof()
        {
            MergeHelper.MergePackages(MofEmof, MofExtension, MofFactory);
        }
        
        public void MergeMofReflectionIntoMofCmofReflection()
        {
            MergeHelper.MergePackages(CmofReflection, MofReflection, MofFactory);
        }

        public void MergeMofExtensionIntoMofCmofExtension()
        {
            MergeHelper.MergePackages(CmofExtension, MofExtension, MofFactory);
        }

        public void MergeMofEmofIntoMofCmof()
        {
            MergeHelper.MergePackages(Cmof, MofEmof, MofFactory);
        }

        public void MergeMofCmofExtensionIntoMofCmof()
        {
            MergeHelper.MergePackages(Cmof, CmofExtension, MofFactory);
        }

        public void MergeMofCmofReflectionIntoMofCmof()
        {
            MergeHelper.MergePackages(Cmof, CmofReflection, MofFactory);
        }

        public void MergeArgumentIntoMof()
        {
            ClassBuilder cmofArgument = CmofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == "Argument");
            ClassBuilder cmofArgClone = MergeHelper.CloneClassIntoMofModel(cmofArgument, MofFactory);
            MofReflection.PackagedElement.Add(cmofArgClone);
        }

        public void RemoveUnknownTypedElementsFromEmof(Dictionary<MutableObject, MutableObject> umlToMof)
        {
            // setting up subsettedProperties in emof
            MergeHelper.SetSubsets(UmlModel, MofEmof, umlToMof);
            // removing unnecessary properties and methods
            UmlToMof.RemoveUnknownProperties(MofEmof);
        }

        public void AddAllReferencedClassesToEmof(Dictionary<MutableObject, MutableObject> umlToMof)
        {
            // adding classes that are referenced by elements in existing classes
            UmlToMof.AddAllReferencedClasses(MofEmof, UmlModel, MofFactory, umlToMof);
            // setting up subsettedProperties in emof
            MergeHelper.SetSubsets(UmlModel, MofEmof, umlToMof);
        }

        public void RemoveUnknownTypedElementsFromCmof(Dictionary<MutableObject, MutableObject> umlToMof)
        {
            // setting up subsettedProperties in cmof
            MergeHelper.SetSubsets(UmlModel, Cmof, umlToMof);
            // removing unnecessary properties and methods
            UmlToMof.RemoveUnknownProperties(Cmof);
        }

        public void AddAllReferencedClassesToCmof(Dictionary<MutableObject, MutableObject> umlToMof)
        {
            // adding classes that are referenced by elements in existing classes
            UmlToMof.AddAllReferencedClasses(Cmof, UmlModel, MofFactory, umlToMof);
            // setting up subsettedProperties in cmof
            MergeHelper.SetSubsets(UmlModel, Cmof, umlToMof);
        }

        public void GenerateEmofmmFile(string fileName = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            var generator = new MofModelToMetaModelGenerator(MofEmof.ToImmutable().PackagedElement);
            var generatedCode = generator.Generate("MofImplementationLib.Model", "Mof", "http://www.omg.org/spec/MOF");
            File.WriteAllText(fileName, generatedCode);
        }

        public void GenerateCmofmmFile(string fileName = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            var generator = new MofModelToMetaModelGenerator(Cmof.ToImmutable().PackagedElement);
            var generatedCode = generator.Generate("MofImplementationLib.Model", "Mof", "http://www.omg.org/spec/MOF");
            File.WriteAllText(fileName, generatedCode);
        }

        public void RenameMofProperties()
        {
            foreach (var pcks in MofModel.Objects.OfType<PackageBuilder>())
            {
                foreach (var cls in pcks.PackagedElement)
                {
                    if (cls is ClassBuilder cb)
                    {
                        MergeHelper.RenameMofProperties(cb);
                    }
                }
            }
        }

        public void AddAssociationsFromUmlToEmof()
        {
            MergeHelper.AssociationMatching(UmlModel, MofEmof, MofFactory);
        }
        public void AddAssociationsFromUmlToCmof()
        {
            MergeHelper.AssociationMatching(UmlModel, Cmof, MofFactory);
        }
    }
}
