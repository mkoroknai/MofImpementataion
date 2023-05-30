using MetaDslx.Modeling;
using MofBootstrapLib.Generator;
using MofBootstrapLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace MofBootstrap
{
    public class UmlToMof
    {
        static readonly string removedFileName = "RemovedElements.txt";

        MergeHelper MergeHelper;
        Dictionary<MutableObject, MutableObject> UmlToMofDict;

        public UmlToMof(MergeHelper mergeHelper)
        {
            MergeHelper = mergeHelper;
            UmlToMofDict = new Dictionary<MutableObject, MutableObject>();
        }

        public void UmlToMofReflection(MutableModel umlModel, PackageBuilder mofReflection, MofFactory mofFactory)
        {

            Console.WriteLine("listing classes already in MOF::Reflection:");
            // listing classes already in MOF::Reflection
            foreach (var e in mofReflection.PackagedElement)
            {
                Console.WriteLine(e.Name);
            }
            Console.WriteLine("____");

            // dictionary for associations and subsetted properties
            Dictionary<MutableObject, MutableObject> umlToMof = new Dictionary<MutableObject, MutableObject>();


            foreach (string umlClsName in MergeHelper.classesToBeMergedFromUml)
            {

                if (mofReflection.PackagedElement.Where(pe => pe.Name == umlClsName).FirstOrDefault() != null)
                {
                    // the class is already in mofReflection, so it needs to be merged
                    Console.WriteLine("there is already a class called " + umlClsName + " in mofReflection");
                    //throw new Exception("there is already a class called " + umlClsName + " in mofReflection, so it needs to be merged");
                }
                else
                {
                    ClassBuilder umlClass = umlModel.Objects.OfType<ClassBuilder>().First(cb => cb.Name == umlClsName);
                    ClassBuilder clone = MergeHelper.CloneClassIntoMofModel(umlClass, mofFactory, mofReflection);

                    mofReflection.PackagedElement.Add(clone);

                    // adding to dictionary
                    umlToMof.Add(umlClass, clone);
                    UmlToMofDict.Add(umlClass, clone);
                    //Console.WriteLine("_:_:_:_ UML class: " + umlClsName + " added to mof");

                    // cloning superclasses and their superclasses too
                    int c;
                    if ((c = AddGeneralsToMof(umlModel, umlClass, mofReflection, mofFactory, umlToMof)) > 0)
                    {
                        Console.WriteLine("  " + c + " superclasses added in UmlToMofReflectionEmof()");
                    }
                }
            }

            // setting types
            AddRefs(umlModel, mofReflection, mofFactory, umlToMof);



            //// based on the MOF2.5.1 specification, the type of
            //// Operation::raisedException needs to be Class rather than Type:
            //ClassBuilder EmofOperation = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == "Operation");
            //PropertyBuilder Op_raisedEx = EmofOperation.OwnedAttribute.First(attr => attr.Name == "raisedException");
            //TypeBuilder typeClass = mofFactory.Type();
            //typeClass.Name = "Class";
            //Op_raisedEx.Type = typeClass;
            //
            //// the following properties must be empty:
            //for (int i = 0; i < Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_CMOF.Length; i += 2)
            //{
            //    string className = Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_CMOF[i];
            //    string propName = Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_CMOF[i + 1];
            //
            //    PropertyBuilder prop = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == className).
            //                                                                 OwnedAttribute.First(at => at.Name == propName);
            //    // set prop empty here ????
            //
            //}
            //
            //// based on the MOF2.5.1 specification
            //// the following properties must be false:
            //for (int i = 0; i < Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_CMOF.Length; i += 2)
            //{
            //    string className = Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_CMOF[i];
            //    string propName = Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_CMOF[i + 1];
            //
            //    PropertyBuilder prop = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == className).
            //                                                                 OwnedAttribute.First(at => at.Name == propName);
            //    // set prop false here ????
            //    ValueSpecificationBuilder defaultVal = mofFactory.ValueSpecification();
            //    //defaultVal.Type = mofFactory.LiteralBoolean();
            //    //prop.DefaultValue = 
            //}

            return;
        }

        public int AddRefs(MutableModel umlModel, PackageBuilder mof, MofFactory mofFactory, Dictionary<MutableObject, MutableObject> umlToMof)
        {
            int counter = 0;
            Dictionary<MutableObject, MutableObject> umlToMofTemp = new Dictionary<MutableObject, MutableObject>();

            foreach (var pair in umlToMof)
            {
                var cb = pair.Value as ClassBuilder;
                var cbUml = pair.Key as ClassBuilder;

                foreach (var a in cb.OwnedAttribute)
                {
                    if (a.Type == null)
                    {

                        var attrInUml = cbUml.OwnedAttribute.FirstOrDefault(ua => ua.Name == a.Name);

                        if (attrInUml.Type is ClassBuilder typeClassInUml)
                        {
                            var existingType = mof.PackagedElement.OfType<ClassBuilder>().FirstOrDefault(cbb => cbb.Name == typeClassInUml.Name);
                            if (existingType != null)
                            {
                                a.Type = existingType;
                            }
                            else
                            {
                                var clone = MergeHelper.CloneClassIntoMofModel(typeClassInUml, mofFactory, mof);
                                mof.PackagedElement.Add(clone);
                                a.Type = clone;
                                counter++;
                                umlToMofTemp.Add(typeClassInUml, clone);
                                UmlToMofDict.TryAdd(typeClassInUml, clone);
                                Console.WriteLine("added class " + clone.Name + " to MOF");

                                AddGeneralsToMof(umlModel, typeClassInUml, mof, mofFactory, umlToMofTemp);
                            }
                        }
                        else if(attrInUml.Type is EnumerationBuilder typeEnumInUml)
                        {
                            var existingType = mof.PackagedElement.OfType<EnumerationBuilder>().FirstOrDefault(ebb => ebb.Name == typeEnumInUml.Name);
                            if (existingType != null)
                            {
                                a.Type = existingType;
                            }
                            else
                            {
                                var clone = MergeHelper.CloneEnumIntoMofModel(typeEnumInUml, mofFactory);
                                mof.PackagedElement.Add(clone);
                                a.Type = clone;
                                counter++;
                                Console.WriteLine("added enum " + clone.Name + " to MOF");
                            }
                        }
                        else
                        {
                            Console.WriteLine(" not class nor enum");
                        }
                    }
                }

                foreach (var o in cb.OwnedOperation)
                {
                    foreach (var p in o.OwnedParameter)
                    {

                        if (p.Type == null)
                        {

                            var opInUml = cbUml.OwnedOperation.FirstOrDefault(uo => uo.Name == o.Name);

                            var parInUml = opInUml.OwnedParameter.FirstOrDefault(up => up.Name == p.Name);

                            if (parInUml.Type is ClassBuilder typeClassInUml)
                            {
                                var existingType = mof.PackagedElement.OfType<ClassBuilder>().FirstOrDefault(cbb => cbb.Name == typeClassInUml.Name);

                                if (existingType != null)
                                {
                                    p.Type = existingType;
                                }
                                else
                                {
                                    var clone = MergeHelper.CloneClassIntoMofModel(typeClassInUml, mofFactory, mof);
                                    mof.PackagedElement.Add(clone);
                                    p.Type = clone;
                                    counter++;
                                    umlToMofTemp.Add(typeClassInUml, clone);
                                    UmlToMofDict.TryAdd(typeClassInUml, clone);
                                    Console.WriteLine("added class " + clone.Name + " to MOF");
                                    AddGeneralsToMof(umlModel, typeClassInUml, mof, mofFactory, umlToMofTemp);
                                }
                            }
                            else if (parInUml.Type is EnumerationBuilder typeEnumInUml)
                            {
                                var existingType = mof.PackagedElement.OfType<EnumerationBuilder>().FirstOrDefault(ebb => ebb.Name == typeEnumInUml.Name);
                                if (existingType != null)
                                {
                                    p.Type = existingType;
                                }
                                else
                                {
                                    var clone = MergeHelper.CloneEnumIntoMofModel(typeEnumInUml, mofFactory);
                                    mof.PackagedElement.Add(clone);
                                    p.Type = clone;
                                    counter++;
                                    Console.WriteLine("added enum " + clone.Name + " to MOF");
                                }
                            }
                            else
                            {
                                Console.WriteLine(" not class nor enum");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Added " + counter + " types to MOF...");

            if (counter != 0) AddRefs(umlModel, mof, mofFactory, umlToMofTemp);

            return counter;
        }

        int AddGeneralsToMof(MutableModel umlModel, ClassBuilder umlClass,
                                    PackageBuilder mof, MofFactory mofFactory,
                                    Dictionary<MutableObject, MutableObject> umlToMof)
        {
            int counter = 0;
            // cloning superclasses too
            foreach (var g in umlClass.Generalization)
            {
                if (mof.PackagedElement.Where(pe => pe.Name == g.General.Name).FirstOrDefault() == null)
                {
                    ClassBuilder generalClass = umlModel.Objects.OfType<ClassBuilder>().First(cb => cb.Name == g.General.Name);
                    ClassBuilder generalClone = MergeHelper.CloneClassIntoMofModel(generalClass, mofFactory, mof);
                    mof.PackagedElement.Add(generalClone);
                    Console.WriteLine("    " + generalClone.Name + " superclass to " + umlClass.Name + " added to mof.");
                    // don't forget to add to dictionary for subsetted props and associations
                    umlToMof.Add(generalClass, generalClone);
                    UmlToMofDict.Add(generalClass, generalClone);
                    counter++;
                    counter += AddGeneralsToMof(umlModel, generalClass, mof, mofFactory, umlToMof);
                }
                else
                {
                    // there is already a class with the same name in the MOF model, so let's merge it
                    ClassBuilder classInMof = mof.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == g.General.Name);
                    ClassBuilder classInUml = umlModel.Objects.OfType<ClassBuilder>().First(cb => cb.Name == g.General.Name);
                    classInMof = MergeHelper.MergeClasses(classInMof, classInUml, mofFactory, mof);
                    umlToMof.TryAdd(classInUml, classInMof);
                    UmlToMofDict.TryAdd(classInUml, classInMof);
                    //Console.WriteLine("  ...;;; merged " + g.General.Name);
                }
            }
            return counter;
        }


        public int AddSuperClasses(PackageBuilder mof, MutableModel umlModel, MofFactory mofFactory, Dictionary<MutableObject, MutableObject> umlToMof)
        {
            int counter = 0;

            foreach(var cls in mof.PackagedElement.OfType<ClassBuilder>())
            {
                // cloning superclasses
                int c;
                if ((c = AddGeneralsToMof(umlModel, cls, mof, mofFactory, umlToMof)) > 0)
                {
                    Console.WriteLine("  " + c + " superclasses added in AddSuperClasses()");
                }
            }

            return counter;
        }


        public void SetSubsets(MutableModel uml, PackageBuilder cmof)
        {
            foreach (var pair in UmlToMofDict)
            {
                var key = pair.Key;
                if (key is ClassBuilder umlClass)
                {
                    ClassBuilder cmofClass = null;
                    cmofClass = cmof.PackagedElement.OfType<ClassBuilder>().First(c => c.Name == umlClass.Name);
                    // go through attributes
                    foreach (var umlAttr in umlClass.OwnedAttribute)
                    {
                        // if there is a subsettedProperty of the attribute, find the attribute in cmofClass
                        if (umlAttr.SubsettedProperty.Count > 0)
                        {
                            PropertyBuilder cmofAttr = null;
                            try
                            {
                                cmofAttr = cmofClass.OwnedAttribute.First(oa => oa.Name == umlAttr.Name);
                            }
                            catch
                            {
                                cmofAttr = cmofClass.OwnedAttribute.First(oa => oa.Name == "_" + umlAttr.Name);
                            }

                            // go through subsettedProperties
                            foreach (var umlSubsP in umlAttr.SubsettedProperty)
                            {
                                if (!(umlSubsP.MParent is AssociationBuilder))
                                {
                                    // look for it in cmof, then add it to cmofAttr
                                    ClassBuilder cmofSubsPClass = null;
                                    cmofSubsPClass = cmof.PackagedElement.OfType<ClassBuilder>().First(c => c.Name == umlSubsP.MParent.MName);

                                    PropertyBuilder cmofSubsProp = null;
                                    try
                                    {
                                        cmofSubsProp = cmofSubsPClass.OwnedAttribute.First(c => c.Name == umlSubsP.Name);
                                    }
                                    catch
                                    {
                                        cmofSubsProp = cmofSubsPClass.OwnedAttribute.First(c => c.Name == "_" + umlSubsP.Name);
                                    }

                                    cmofAttr.SubsettedProperty.Add(cmofSubsProp);

                                    //Console.WriteLine(umlSubsP.Name + "\t" + umlSubsP.MParent.MName);

                                    //cmofAttr.SubsettedProperty.Add(cmofSubsP);
                                }
                                else
                                {
                                    //Console.WriteLine("\t\t" + umlSubsP.MParent.MName + " is Association");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
