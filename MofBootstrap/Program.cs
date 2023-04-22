using MetaDslx.Modeling;
using MofBootstrapLib.Generator;
using MofBootstrapLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MofBootstrap
{
    class Program
    {
        static void Main(string[] args)
        {
            //EmofXmi(true);
            CmofXmi(false);
        }

        /// <summary>
        /// Creates Mof.mm file that contains EMOF model
        /// if first input parameter is true, all the variables with unknown type,
        /// or functions that have parameters with unknown type
        /// (i.e. the definition of their type does not exist in MOF model)
        /// will be removed
        /// if first input parameter is false, all the metaclasses/enums will be added
        /// </summary>
        /// <param name="removeUnknownTypedElements"></param>
        static void EmofXmi(bool removeUnknownTypeElements)
        {

            var xmiSerializer = new MofXmiSerializer();
            ImmutableModel model = xmiSerializer.ReadModelFromFile("../../../MOF.xmi");

            var mutableGroup = model.ModelGroup.ToMutable();
            var mofModel = model.ToMutable();
            var mofFactory = new MofFactory(mofModel, ModelFactoryFlags.DontMakeObjectsCreated);
            var umlModel = mutableGroup.Models.First(m => m.Name.Contains("UML.xmi"));

            // grabbing packages
            PackageBuilder mofReflection  = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Reflection");
            PackageBuilder mofExtension   = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Extension");
            PackageBuilder cmofReflection = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFReflection");
            PackageBuilder mofCommon      = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Common");
            PackageBuilder mofEMOF        = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "EMOF");
            PackageBuilder mofIdentifiers = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Identifiers");

            MergeHelper.RenameMofProperties(mofModel);

            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Merge EMOF parts of UML into MOF::Reflection");


            Dictionary<MutableObject, MutableObject> umlToMof = null;

            umlToMof = UmlToMofHelper.UmlToMofReflectionEmof(umlModel, mofReflection, mofFactory);


            // need to merge MOF::CMOFReflection::Argument into MOF::Reflection, because of class Object
            // ????
            ClassBuilder cmofArgument = cmofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == "Argument");
            ClassBuilder cmofArgClone = MergeHelper.CloneClassIntoMofModel(cmofArgument, mofFactory);
            mofReflection.PackagedElement.Add(cmofArgClone);

            Console.WriteLine();
            // Merge MOF::Reflection into MOF::Extension ------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofExtension, mofReflection, mofFactory);


            // Merge MOF::Common into MOF::EMOF ---------------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofCommon, mofFactory);


            // Merge MOF::Identifiers into MOF::EMOF ----------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofIdentifiers, mofFactory);


            // Merge MOF::Reflection into MOF::EMOF -----------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofReflection, mofFactory);

            // Merge MOF::Extension into MOF::EMOF ------------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofExtension, mofFactory);


            if (removeUnknownTypeElements)
            {
                // setting up subsettedProperties in cmof
                MergeHelper.SetSubsets(umlModel, mofEMOF, umlToMof);
                // removing unnecessary properties and methods
                UmlToMofHelper.RemoveUnknownProperties(mofEMOF);
            }
            else
            {
                // adding classes that are referenced by elements in existing classes
                UmlToMofHelper.AddAllReferencedClasses(mofEMOF, umlModel, mofFactory, umlToMof);
                // setting up subsettedProperties in cmof
                MergeHelper.SetSubsets(umlModel, mofEMOF, umlToMof);
            }


            // setting up associations in emof
            //UmlToMofHelper.AssociationMatching(umlModel, mergePckgs, mofEMOF);



            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            var generator = new MofModelToMetaModelGenerator(mofEMOF.ToImmutable().PackagedElement);
            var generatedCode = generator.Generate("MofImplementationLib.Model", "Mof", "http://www.omg.org/spec/MOF");
            File.WriteAllText("../../../../MofImplementationLib/Model/Mof.mm", generatedCode);
        }


        /// <summary>
        /// Creates Mof.mm file that contains CMOF model
        /// if first input parameter is true, all the variables with unknown type,
        /// or functions that have parameters with unknown type
        /// (i.e. the definition of their type does not exist in MOF model)
        /// will be removed
        /// if first input parameter is false, all the metaclasses/enums will be added
        /// </summary>
        /// <param name="removeUnknownTypedElements"></param>
        static void CmofXmi(bool removeUnknownTypeElements)
        {

            var xmiSerializer = new MofXmiSerializer();
            ImmutableModel model = xmiSerializer.ReadModelFromFile("../../../MOF.xmi");

            var mutableGroup = model.ModelGroup.ToMutable();
            var mofModel = model.ToMutable();
            var mofFactory = new MofFactory(mofModel, ModelFactoryFlags.DontMakeObjectsCreated);
            var umlModel = mutableGroup.Models.First(m => m.Name.Contains("UML.xmi"));

            // grabbing packages
            PackageBuilder mofReflection  = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Reflection");
            PackageBuilder mofExtension   = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Extension");
            PackageBuilder cmofReflection = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFReflection");
            PackageBuilder cmofExtension  = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFExtension");
            PackageBuilder mofCommon      = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Common");
            PackageBuilder mofEMOF        = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "EMOF");
            PackageBuilder mofIdentifiers = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Identifiers");
            PackageBuilder cmof           = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOF");

            MergeHelper.RenameMofProperties(mofModel);

            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Merge EMOF parts of UML into MOF::Reflection");

            Dictionary<MutableObject, MutableObject> umlToMof = null;

            umlToMof = UmlToMofHelper.UmlToMofReflectionCmof(umlModel, mofReflection, mofFactory);

            // need to merge MOF::CMOFReflection::Argument into MOF::Reflection, because of class Object
            // ????
            ClassBuilder cmofArgument = cmofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == "Argument");
            ClassBuilder cmofArgClone = MergeHelper.CloneClassIntoMofModel(cmofArgument, mofFactory);
            mofReflection.PackagedElement.Add(cmofArgClone);

            Console.WriteLine();
            // Merge MOF::Reflection into MOF::Extension ------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofExtension, mofReflection, mofFactory);


            // Merge MOF::Common into MOF::EMOF ---------------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofCommon, mofFactory);


            // Merge MOF::Identifiers into MOF::EMOF ----------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofIdentifiers, mofFactory);


            // Merge MOF::Reflection into MOF::EMOF -----------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofReflection, mofFactory);


            // Merge MOF::Extension into MOF::EMOF ------------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofExtension, mofFactory);


            // ----- Finally ------
            // Merge MOF::Reflection into MOF::CMOFReflection -----------------------------------------------------------------------------------------

            MergeHelper.MergePackages(cmofReflection, mofReflection, mofFactory);


            // Merge MOF::Extension into MOF::CMOFExtension -----------------------------------------------------------------------------------------

            MergeHelper.MergePackages(cmofExtension, mofExtension, mofFactory);


            // Merge MOF::EMOF into MOF::CMOF -----------------------------------------------------------------------------------------

            MergeHelper.MergePackages(cmof, mofEMOF, mofFactory);


            // Merge MOF::CMOFExtension into MOF::CMOF --------------------------------------------------------------------------------

            MergeHelper.MergePackages(cmof, cmofExtension, mofFactory);


            // Merge MOF::CMOFReflection into MOF::CMOF -------------------------------------------------------------------------------

            MergeHelper.MergePackages(cmof, cmofReflection, mofFactory);


            if (removeUnknownTypeElements)
            {
                // setting up subsettedProperties in cmof
                MergeHelper.SetSubsets(umlModel, cmof, umlToMof);
                // removing unnecessary properties and methods
                UmlToMofHelper.RemoveUnknownProperties(cmof);
            }
            else
            {
                // adding classes that are referenced by elements in existing classes
                UmlToMofHelper.AddAllReferencedClasses(cmof, umlModel, mofFactory, umlToMof);
                // setting up subsettedProperties in cmof
                MergeHelper.SetSubsets(umlModel, cmof, umlToMof);
            }


            // setting up associations in cmof not yet implemented
            //UmlToMofHelper.AssociationMatching(umlModel, cmof, umlToMof);



            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            var generator = new MofModelToMetaModelGenerator(cmof.ToImmutable().PackagedElement);
            var generatedCode = generator.Generate("MofImplementationLib.Model", "Mof", "http://www.omg.org/spec/MOF");
            File.WriteAllText("../../../../MofImplementationLib/Model/Mof.mm", generatedCode);
        }


        /// <summary>
        /// Executing merges
        /// does not create new packages for UML packages within Reflection
        /// simply copies all the classes
        /// </summary>
        public static void MofXmi2()
        {

            var xmiSerializer = new MofXmiSerializer();
            ImmutableModel model = xmiSerializer.ReadModelFromFile("../../../MOF.xmi");

            var mutableGroup = model.ModelGroup.ToMutable();
            var mofModel = model.ToMutable();
            var mofFactory = new MofFactory(mofModel, ModelFactoryFlags.DontMakeObjectsCreated);
            var umlModel = mutableGroup.Models.First(m => m.Name.Contains("UML.xmi"));

            PackageBuilder mofReflection  = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Reflection");
            PackageBuilder mofExtension   = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Extension");
            PackageBuilder cmofReflection = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFReflection");
            PackageBuilder cmofExtension  = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOFExtension");
            PackageBuilder mofCommon      = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Common");
            PackageBuilder mofEMOF        = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "EMOF");
            PackageBuilder mofIdentifiers = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "Identifiers");
            PackageBuilder cmof           = mofModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == "CMOF");

            var tagExt = mofExtension.PackagedElement.OfType<ClassBuilder>().First(c => c.Name == "Tag");
            var tagCMOFExt = cmofExtension.PackagedElement.OfType<ClassBuilder>().First(c => c.Name == "Tag");

            // listing MOF packages and classes
            foreach (var pcks in mofModel.Objects.OfType<PackageBuilder>())
            {
                Console.WriteLine(pcks.Name);
                foreach (var classes in pcks.PackagedElement)
                {
                    Console.WriteLine("  " + classes.Name + "  " + classes.MMetaClass.Name);
                    MergeHelper.RenameMofProperties(classes as ClassBuilder);
                }
            }
            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine();

            Console.WriteLine("Merge UML into MOF::Reflection");
            // Merge UML into MOF::Reflection -----------------------------------------------------------------------------------------

            Dictionary<MutableObject, MutableObject> umlToMof = new Dictionary<MutableObject, MutableObject>();
            var mergePckgs = mofModel.Objects.OfType<PackageMergeBuilder>().First(mp => mp.MergedPackage.Name == "UML");

            // copying associations first
            // going through packages
            foreach (var pckg in mergePckgs.MergedPackage.PackagedElement)
            {
                Console.WriteLine("  " + pckg.Name + "  Type: " + pckg.GetType());

                if (pckg is PackageBuilder pckgpb)
                {
                    var umlPckg = umlModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == pckg.Name);

                    // going through elements of package, that can be ClassBuilder, EnumerationBuilder etc...
                    foreach (var pElem in umlPckg.PackagedElement)
                    {

                        //Console.WriteLine("    " + pElem.Name/* + "  Type: " + pElem.GetType()*/);

                        if (pElem is ClassBuilder cb)
                        {
                            // ignore
                        }
                        else if (pElem is EnumerationBuilder eb)
                        {
                            // ignore
                        }
                        else if (pElem is AssociationBuilder ab)
                        {
                            AssociationBuilder newAssoc = MergeHelper.CloneAssociation(ab, mofFactory);
                            mofReflection.PackagedElement.Add(newAssoc);
                            // adding to dictionary
                            //umlToMof.Add(ab, newAssoc);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("  ## Packaged Element is not of type PackageBuilder");
                }
            }

            // copying everything else
            // going through packages
            foreach (var pckg in mergePckgs.MergedPackage.PackagedElement)
            {
                Console.WriteLine("  " + pckg.Name + "  Type: " + pckg.GetType());

                if (pckg is PackageBuilder pckgpb)
                {
                    var umlPckg = umlModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == pckg.Name);

                    // going through elements of package, that can be ClassBuilder, EnumerationBuilder etc...
                    foreach (var pElem in umlPckg.PackagedElement)
                    {

                        //Console.WriteLine("    " + pElem.Name/* + "  Type: " + pElem.GetType()*/);

                        if (pElem is ClassBuilder cb)
                        {
                            ClassBuilder receiver;
                            if (mofReflection.PackagedElement.Where(pe => pe.Name == cb.Name).FirstOrDefault() != null)
                            {
                                Console.WriteLine("      ### Package already contains class: " + cb.Name);
                                // there is already a class with the same name in the package, so they have to be merged
                                receiver = mofReflection.PackagedElement.OfType<ClassBuilder>().First(pe => pe.Name == cb.Name);
                                mofReflection.PackagedElement.Add(MergeHelper.MergeClasses(receiver, cb, mofFactory));
                                Console.WriteLine("      ### " + cb.Name + " merged");
                            }
                            else
                            {
                                receiver = MergeHelper.CloneClassIntoMofModel(cb, mofFactory);
                                mofReflection.PackagedElement.Add(receiver);
                            }
                            // adding to dictionary
                            umlToMof.Add(cb, receiver);
                        }
                        else if (pElem is EnumerationBuilder eb)
                        {
                            EnumerationBuilder newEnum;
                            if (mofReflection.PackagedElement.Where(pe => pe.Name == eb.Name).FirstOrDefault() != null)
                            {
                                Console.WriteLine("      ### Package already contains enum: " + eb.Name);
                                // there is already an enumeration with the same name in the package, so they have to be merged
                                Console.WriteLine("      Enumeration merge needs to be implemented.");
                            }
                            else
                            {
                                newEnum = MergeHelper.CloneEnumIntoMofModel(eb, mofFactory);
                                mofReflection.PackagedElement.Add(newEnum);
                                // adding to dictionary
                                umlToMof.Add(eb, newEnum);
                            }
                        }
                        else if (pElem is AssociationBuilder ab)
                        {
                            // ignore, already done
                        }
                    }
                }
                else
                {
                    Console.WriteLine("  ## Packaged Element is not of type PackageBuilder");
                }
            }



            Console.WriteLine();
            // Merge MOF::Reflection into MOF::Extension ------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofExtension, mofReflection, mofFactory);


            // Merge MOF::Reflection into MOF::CMOFReflection -------------------------------------------------------------------------

            MergeHelper.MergePackages(cmofReflection, mofReflection , mofFactory);


            // Merge MOF::Extension into MOF::CMOFExtension ---------------------------------------------------------------------------

            MergeHelper.MergePackages(cmofExtension, mofExtension, mofFactory);


            // Merge MOF::Common into MOF::EMOF ---------------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofCommon, mofFactory);


            // Merge MOF::Identifiers into MOF::EMOF ----------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofIdentifiers, mofFactory);


            // Merge MOF::Reflection into MOF::EMOF -----------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofReflection, mofFactory);

            // Merge MOF::Extension into MOF::EMOF ------------------------------------------------------------------------------------

            MergeHelper.MergePackages(mofEMOF, mofExtension, mofFactory);


            // ----- Finally ------
            // Merge MOF::EMOF into MOF::CMOF -----------------------------------------------------------------------------------------

            MergeHelper.MergePackages(cmof, mofEMOF, mofFactory);


            // Merge MOF::CMOFExtension into MOF::CMOF --------------------------------------------------------------------------------

            MergeHelper.MergePackages(cmof, cmofExtension, mofFactory);


            // Merge MOF::CMOFReflection into MOF::CMOF -------------------------------------------------------------------------------

            MergeHelper.MergePackages(cmof, cmofReflection, mofFactory);


            // setting up associations in cmof
            MergeHelper.AssociationMatching(umlModel, mergePckgs, cmof);

            // setting up subsettedProperties in cmof
            MergeHelper.SetSubsets(umlModel, cmof, umlToMof);


            // generating file
            Console.WriteLine(Environment.NewLine + "Creating .mm file" + Environment.NewLine);
            //var generator = new MofModelToMetaModelGenerator(mofModel.ToImmutable().Objects);
            var generator = new MofModelToMetaModelGenerator(cmof.ToImmutable().PackagedElement);
            var generatedCode = generator.Generate("MofImplementationLib.Model", "Mof", "http://www.omg.org/spec/MOF");
            File.WriteAllText("../../../../MofImplementationLib/Model/Mof.mm", generatedCode);
        }
    }
}
