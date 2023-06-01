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
    enum MofToGenerate
    {
        EMOF,
        CMOF
    }
    class MofGenerator
    {

        MutableModel MofModel{ get; }
        PackageBuilder MofReflection { get; }
        PackageBuilder MofExtension { get; }
        PackageBuilder CmofReflection { get; }
        PackageBuilder CmofExtension { get; }
        PackageBuilder MofCommon { get; }
        PackageBuilder MofEmof { get; }
        PackageBuilder MofIdentifiers { get; }
        PackageBuilder Cmof { get; }

        MutableModel UmlModel;

        MofFactory MofFactory { get; }

        MergeHelper MergeHelper;
        UmlToMof UmlToMof;
        bool IsEmofGenerated;

        public MofGenerator(ImmutableModel mofModel, MofToGenerate mofToGenerate)
        {
            MofModel = mofModel.ToMutable();
            var mutableGroup = mofModel.ModelGroup.ToMutable();
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


            IsEmofGenerated = mofToGenerate == MofToGenerate.EMOF;

            string[] ctbm = IsEmofGenerated ?
                Constants.UML_CLASSES_TO_BE_MERGED_INTO_EMOF :
                Constants.UML_CLASSES_TO_BE_MERGED_INTO_CMOF;

            MergeHelper = new MergeHelper(ctbm, UmlModel);
            UmlToMof = new UmlToMof(MergeHelper);


            // renaming conflicting element names like string, object, etc.
            RenameMofProperties();
        }

        public void Generate(string fileNameForGeneratedModel = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            if (IsEmofGenerated)
            {
                GenerateEssentialMof(fileNameForGeneratedModel);
                return;
            }
            GenerateCompleteMof(fileNameForGeneratedModel);
        }

        void GenerateEssentialMof(string fileNameForGeneratedModel = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            //Console.WriteLine();
            //Console.WriteLine("-----------------");
            //Console.WriteLine();
            
            //Console.WriteLine("Merge EMOF parts of UML into MOF::Reflection");

            MergeUmlToMofReflection();

            // need to merge MOF::CMOFReflection::Argument into MOF::Reflection, because of class Object
            //MergeArgumentIntoMof(umlToMof);

            Console.WriteLine();
            // Merge MOF::Reflection into MOF::Extension ------------------------------------------------------------------------------
            MergeMofReflectionIntoMofExtension();

            // Merge MOF::Reflection into MOF::EMOF -----------------------------------------------------------------------------------
            MergeMofReflectionIntoMofEmof();

            // Merge MOF::Common into MOF::EMOF ---------------------------------------------------------------------------------------
            MergeMofCommonIntoMofEmof();

            // Merge MOF::Identifiers into MOF::EMOF ----------------------------------------------------------------------------------
            MergeMofIdentifiersIntoMofEmof();

            // Merge MOF::Extension into MOF::EMOF ------------------------------------------------------------------------------------
            MergeMofExtensionIntoMofEmof();

            SetSubsets();

            AddAssociationsFromUmlToEmof();

            Console.WriteLine();
            Console.WriteLine("Essential MOF generated.");

            GeneratemmFile(fileNameForGeneratedModel);
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
        void GenerateCompleteMof(string fileNameForGeneratedModel = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            MergeUmlToMofReflection();

            // need to merge MOF::CMOFReflection::Argument into MOF::Reflection, because of class Object
            MergeArgumentIntoMof();

            // Merge MOF::Reflection into MOF::Extension ------------------------------------------------------------------------------
            MergeMofReflectionIntoMofExtension();

            // Merge MOF::Reflection into MOF::EMOF -----------------------------------------------------------------------------------
            MergeMofReflectionIntoMofEmof();

            // Merge MOF::Common into MOF::EMOF ---------------------------------------------------------------------------------------
            MergeMofCommonIntoMofEmof();

            // Merge MOF::Identifiers into MOF::EMOF ----------------------------------------------------------------------------------
            MergeMofIdentifiersIntoMofEmof();

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

            SetSubsets();

            AddAssociationsFromUmlToCmof();

            ClassBuilder multiplicityElement = Cmof.PackagedElement.OfType<ClassBuilder>().First(c => c.Name == "MultiplicityElement");
            ClassBuilder literalInteger = Cmof.PackagedElement.OfType<ClassBuilder>().First(c => c.Name == "LiteralInteger");
            ClassBuilder literalUnlimited = Cmof.PackagedElement.OfType<ClassBuilder>().First(c => c.Name == "LiteralUnlimitedNatural");

            PropertyBuilder lowerValue = multiplicityElement.OwnedAttribute.First(a => a.Name == "lowerValue");
            PropertyBuilder upperValue = multiplicityElement.OwnedAttribute.First(a => a.Name == "upperValue");

            lowerValue.Type = literalInteger;
            upperValue.Type = literalUnlimited;

            Console.WriteLine();
            Console.WriteLine("Complete MOF generated.");

            GeneratemmFile(fileNameForGeneratedModel);

            //foreach (var pair in umlToMof)
            //{
            //    Console.WriteLine((pair.Key as NamedElementBuilder).Name + " " + (pair.Value as NamedElementBuilder).Name);
            //}
        }

        public void MergeUmlToMofReflection()
        {
            UmlToMof.UmlToMofReflection(UmlModel, MofReflection, MofFactory);
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
            ClassBuilder cmofArgClone = MergeHelper.CloneClassIntoMofModel(cmofArgument, MofFactory, MofReflection);
            MofReflection.PackagedElement.Add(cmofArgClone);
            //umlToMof.Add()
        }


        public void SetSubsets()
        {
            if(IsEmofGenerated)
            {
                UmlToMof.SetSubsets(UmlModel, MofEmof);
                return;
            }
            UmlToMof.SetSubsets(UmlModel, Cmof);
        }

        public void GeneratemmFile(string fileName = "../../../../MofImplementationLib/Model/Mof.mm")
        {
            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            PackageBuilder mofToBeGenerated = IsEmofGenerated ? MofEmof : Cmof;
            var generator =  new MofModelToMetaModelGenerator(mofToBeGenerated.ToImmutable().PackagedElement);
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
            MergeHelper.CopyAssociationsFromUml(UmlModel, MofEmof, MofFactory);
        }
        public void AddAssociationsFromUmlToCmof()
        {
            MergeHelper.CopyAssociationsFromUml(UmlModel, Cmof, MofFactory);
        }
    }
}
