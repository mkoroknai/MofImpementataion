using MetaDslx.Modeling;
using MofBootstrapLib.Generator;
using MofBootstrapLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MofBootstrap
{
    class MofGenerator
    {

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
            MutableModel mofModel = model.ToMutable();
            var mutableGroup = model.ModelGroup.ToMutable();
            UmlModel = mutableGroup.Models.First(m => m.Name.Contains("UML.xmi"));

            MofFactory = new MofFactory(mofModel, ModelFactoryFlags.DontMakeObjectsCreated);

            // grabbing packages
            MofReflection = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Reflection");
            MofExtension = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Extension");
            CmofReflection = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFReflection");
            CmofExtension = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFExtension");
            MofCommon = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Common");
            MofEmof = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "EMOF");
            MofIdentifiers = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Identifiers");
            Cmof = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOF");

            // renaming conflicting element names like string, object, etc.
            MergeHelper.RenameMofProperties(mofModel);
        }

        public void GenerateEssentialMof(bool removeUnknownTypedElements,
                                         string fileNameForGeneratedModel = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            //Console.WriteLine();
            //Console.WriteLine("-----------------");
            //Console.WriteLine();
            
            //Console.WriteLine("Merge EMOF parts of UML into MOF::Reflection");

            Dictionary<MutableObject, MutableObject> umlToMof = MergeUmlToMofReflection();

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
            Dictionary<MutableObject, MutableObject> umlToMof = MergeUmlToMofReflection();

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


            Console.WriteLine();
            Console.WriteLine("Complete MOF generated.");

            GenerateCmofmmFile(fileNameForGeneratedModel);
        }

        public Dictionary<MutableObject, MutableObject> MergeUmlToMofReflection()
        {
            return UmlToMofHelper.UmlToMofReflectionEmof(UmlModel, MofReflection, MofFactory);
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
            UmlToMofHelper.RemoveUnknownProperties(MofEmof);
        }

        public void AddAllReferencedClassesToEmof(Dictionary<MutableObject, MutableObject> umlToMof)
        {
            // adding classes that are referenced by elements in existing classes
            UmlToMofHelper.AddAllReferencedClasses(MofEmof, UmlModel, MofFactory, umlToMof);
            // setting up subsettedProperties in emof
            MergeHelper.SetSubsets(UmlModel, MofEmof, umlToMof);
        }

        public void RemoveUnknownTypedElementsFromCmof(Dictionary<MutableObject, MutableObject> umlToMof)
        {
            // setting up subsettedProperties in cmof
            MergeHelper.SetSubsets(UmlModel, Cmof, umlToMof);
            // removing unnecessary properties and methods
            UmlToMofHelper.RemoveUnknownProperties(Cmof);
        }

        public void AddAllReferencedClassesToCmof(Dictionary<MutableObject, MutableObject> umlToMof)
        {
            // adding classes that are referenced by elements in existing classes
            UmlToMofHelper.AddAllReferencedClasses(Cmof, UmlModel, MofFactory, umlToMof);
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
    }
}
