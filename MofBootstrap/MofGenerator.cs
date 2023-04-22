using MetaDslx.Modeling;
using MofBootstrapLib.Model;
using System;
using System.Collections.Generic;
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

        MofGenerator(ImmutableModel model)
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

        public void GenerateEssentialMof(bool removeUnknownTypeElements)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Merge EMOF parts of UML into MOF::Reflection");

            Dictionary<MutableObject, MutableObject> umlToMof = MergeUmlToMofReflection();

            // need to merge MOF::CMOFReflection::Argument into MOF::Reflection, because of class Object
            ClassBuilder cmofArgument = CmofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == "Argument");
            ClassBuilder cmofArgClone = MergeHelper.CloneClassIntoMofModel(cmofArgument, MofFactory);
            MofReflection.PackagedElement.Add(cmofArgClone);
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
    }
}
