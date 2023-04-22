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
    public static class UmlToMofHelper
    {
        static readonly string removedFileName = "RemovedElements.txt";

        public static Dictionary<MutableObject, MutableObject> UmlToMofReflectionEmof
            (MutableModel umlModel, PackageBuilder mofReflection, MofFactory mofFactory)
        {

            // listing classes already in MOF::Reflection
            foreach(var e in mofReflection.PackagedElement)
            {
                Console.WriteLine(e.Name);
            }

            // dictionary for associations and subsetted properties
            Dictionary<MutableObject, MutableObject> umlToMof = new Dictionary<MutableObject, MutableObject>();


            foreach (string umlClsName in Constants.UML_CLASSES_TO_BE_MERGED_INTO_EMOF)
            {

                Console.WriteLine(umlClsName);
                
                if(mofReflection.PackagedElement.Where(pe => pe.Name == umlClsName).FirstOrDefault() != null)
                {
                    // the class is already in mofReflection, so it needs to be merged
                    Console.WriteLine("there is already a class called " + umlClsName + " in mofReflection, so it needs to be merged");
                    //throw new Exception("there is already a class called " + umlClsName + " in mofReflection, so it needs to be merged");
                }
                else
                {
                    ClassBuilder umlClass = umlModel.Objects.OfType<ClassBuilder>().First(cb => cb.Name == umlClsName);
                    ClassBuilder clone = MergeHelper.CloneClassIntoMofModel(umlClass, mofFactory);
                    mofReflection.PackagedElement.Add(clone);

                    // adding to dictionary
                    umlToMof.Add(umlClass, clone);

                    // cloning superclasses and their superclasses too
                    int c;
                    if((c = AddGeneralsToMof(umlModel, umlClass, mofReflection, mofFactory, umlToMof)) > 0)
                    {
                        Console.WriteLine("  " + c + " superclasses added in UmlToMofReflectionEmof()");
                    }
                }
            }





            // based on the MOF2.5.1 specification, the type of
            // Operation::raisedException needs to be Class rather than Type:
            ClassBuilder EmofOperation = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == "Operation");
            PropertyBuilder Op_raisedEx = EmofOperation.OwnedAttribute.First(attr => attr.Name == "raisedException");
            TypeBuilder typeClass = mofFactory.Type();
            typeClass.Name = "Class";
            Op_raisedEx.Type = typeClass;

            // based on the MOF2.5.1 specification
            // the following properties must be empty:
            for (int i = 0; i < Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_EMOF.Length; i += 2)
            {
                string className = Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_EMOF[i];
                string propName = Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_EMOF[i + 1];

                PropertyBuilder prop = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == className).
                                                                             OwnedAttribute.First(at => at.Name == propName);
                // set prop empty here ????
                
            }

            // based on the MOF2.5.1 specification
            // the following properties must be false:
            for (int i = 0; i < Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_EMOF.Length; i += 2)
            {
                string className = Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_EMOF[i];
                string propName = Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_EMOF[i + 1];

                PropertyBuilder prop = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == className).
                                                                             OwnedAttribute.First(at => at.Name == propName);
                // set prop false here ????
                ValueSpecificationBuilder defaultVal = mofFactory.ValueSpecification();
                //defaultVal.Name = "False";
                //prop.DefaultValue = defaultVal;
            }

            return umlToMof;
        }


        public static Dictionary<MutableObject, MutableObject> UmlToMofReflectionCmof
                     (MutableModel umlModel, PackageBuilder mofReflection, MofFactory mofFactory)
        {

            Console.WriteLine("listing classes already in MOF::Reflection:");
            // listing classes already in MOF::Reflection
            foreach (var e in mofReflection.PackagedElement)
            {
                Console.WriteLine(e.Name);
            }
            Console.WriteLine("____End");

            // dictionary for associations and subsetted properties
            Dictionary<MutableObject, MutableObject> umlToMof = new Dictionary<MutableObject, MutableObject>();


            foreach (string umlClsName in Constants.UML_CLASSES_TO_BE_MERGED_INTO_CMOF)
            {

                if (mofReflection.PackagedElement.Where(pe => pe.Name == umlClsName).FirstOrDefault() != null)
                {
                    // the class is already in mofReflection, so it needs to be merged
                    Console.WriteLine("there is already a class called " + umlClsName + " in mofReflection, so it needs to be merged");
                    //throw new Exception("there is already a class called " + umlClsName + " in mofReflection, so it needs to be merged");
                }
                else
                {
                    ClassBuilder umlClass = umlModel.Objects.OfType<ClassBuilder>().First(cb => cb.Name == umlClsName);
                    ClassBuilder clone = MergeHelper.CloneClassIntoMofModel(umlClass, mofFactory);
                    mofReflection.PackagedElement.Add(clone);

                    // adding to dictionary
                    umlToMof.Add(umlClass, clone);
                    Console.WriteLine("_:_:_:_ UML class: " + umlClsName + " added to mof");

                    // cloning superclasses and their superclasses too
                    int c;
                    if ((c = AddGeneralsToMof(umlModel, umlClass, mofReflection, mofFactory, umlToMof)) > 0)
                    {
                        Console.WriteLine("  " + c + " superclasses added in UmlToMofReflectionEmof()");
                    }
                }
            }





            // based on the MOF2.5.1 specification, the type of
            // Operation::raisedException needs to be Class rather than Type:
            ClassBuilder EmofOperation = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == "Operation");
            PropertyBuilder Op_raisedEx = EmofOperation.OwnedAttribute.First(attr => attr.Name == "raisedException");
            TypeBuilder typeClass = mofFactory.Type();
            typeClass.Name = "Class";
            Op_raisedEx.Type = typeClass;

            // the following properties must be empty:
            for (int i = 0; i < Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_CMOF.Length; i += 2)
            {
                string className = Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_CMOF[i];
                string propName = Constants.UML_CLASS_PROPERTIES_TO_BE_EMPTY_CMOF[i + 1];

                PropertyBuilder prop = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == className).
                                                                             OwnedAttribute.First(at => at.Name == propName);
                // set prop empty here ????

            }

            // based on the MOF2.5.1 specification
            // the following properties must be false:
            for (int i = 0; i < Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_CMOF.Length; i += 2)
            {
                string className = Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_CMOF[i];
                string propName = Constants.UML_CLASS_PROPERTIES_TO_BE_FALSE_CMOF[i + 1];

                PropertyBuilder prop = mofReflection.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == className).
                                                                             OwnedAttribute.First(at => at.Name == propName);
                // set prop false here ????
                ValueSpecificationBuilder defaultVal = mofFactory.ValueSpecification();
                //defaultVal.Type = mofFactory.LiteralBoolean();
                //prop.DefaultValue = 
            }

            return umlToMof;
        }

        /// <summary>
        /// removes class elements that reference non-existing classes in MOF
        /// </summary>
        /// <param name="mof"></param>
        public static void RemoveUnknownProperties(PackageBuilder mof)
        {
            ClassClassElementList cceList = new ClassClassElementList();

            foreach(var cls in mof.PackagedElement.OfType<ClassBuilder>())
            {
                foreach (var attr in cls.OwnedAttribute)
                {
                    TypeBuilder attrType = attr.Type;
                    if (!(attrType is PrimitiveTypeBuilder))
                    {
                        if (mof.PackagedElement.OfType<ClassBuilder>().Where(cb => cb.Name == attrType.Name).FirstOrDefault() == null)
                        {
                            // the class does not exist in MOF, so the class attribute needs to be removed
                            cceList.Add(new ClassClassElement(cls.Name, attr.Name, attrType.Name));
                            cls.OwnedAttribute.Remove(attr);
                        }
                    }
                }
                foreach (var op in cls.OwnedOperation)
                {
                    foreach (var par in op.OwnedParameter)
                    {
                        TypeBuilder parType = par.Type;
                        if (!(parType is PrimitiveTypeBuilder))
                        {
                            if (mof.PackagedElement.OfType<ClassBuilder>().Where(cb => cb.Name == parType.Name).FirstOrDefault() == null)
                            {
                                // the class does not exist in MOF, so the class operation needs to be removed
                                cceList.Add(new ClassClassElement(cls.Name, op.Name + "()", parType.Name));
                                cls.OwnedOperation.Remove(op);
                            }
                        }
                    }
                }
            }

            File.WriteAllText(removedFileName, cceList.ToString());
            Console.WriteLine();
            Console.WriteLine("The list of removed elements is in " + removedFileName);
            Console.WriteLine();
        }


        /// <summary>
        /// adds metaclasses that are not in MOF yet, but are referenced by other class variables
        /// </summary>
        /// <param name="mof"></param>
        /// <param name="umlModel"></param>
        /// <param name="mofFactory"></param>
        /// <param name="umlToMof"></param>
        /// <returns></returns>
        public static int AddReferencedClasses(PackageBuilder mof, MutableModel umlModel, MofFactory mofFactory, Dictionary<MutableObject, MutableObject> umlToMof)
        {
            int counter = 0;
            foreach (var cls in mof.PackagedElement.OfType<ClassBuilder>())
            {
                foreach (var attr in cls.OwnedAttribute)
                {
                    TypeBuilder attrType = attr.Type;
                    if (!(attrType is PrimitiveTypeBuilder))
                    {
                        if (mof.PackagedElement.Where(cb => cb.Name == attrType.Name).FirstOrDefault() == null)
                        {
                            // the class does not exist in MOF, so the metaclass of the attribute needs to be added
                            // look for it in UML

                            var umlElement = umlModel.Objects.First(cb => cb.MName == attrType.Name);

                            if(umlElement is ClassBuilder umlClass)
                            {
                                ClassBuilder clone = MergeHelper.CloneClassIntoMofModel(umlClass, mofFactory);
                                mof.PackagedElement.Add(clone);

                                // adding to dictionary
                                umlToMof.Add(umlClass, clone);
                                counter++;

                                // cloning superclasses and their superclasses too
                                int c;
                                if ((c = AddGeneralsToMof(umlModel, umlClass, mof, mofFactory, umlToMof)) > 0)
                                {
                                    Console.WriteLine("  " + c + " superclasses added in AddReferencedClasses()");
                                }
                            }

                            else if (umlElement is EnumerationBuilder umlEnum)
                            {
                                EnumerationBuilder clone = MergeHelper.CloneEnumIntoMofModel(umlEnum, mofFactory);
                                mof.PackagedElement.Add(clone);
                                counter++;
                            }
                            else
                            {
                                Console.WriteLine("\t" + attrType.Name + " not class nor enum");
                            }
                        }
                    }
                }
                foreach (var op in cls.OwnedOperation)
                {
                    foreach (var par in op.OwnedParameter)
                    {
                        TypeBuilder parType = par.Type;
                        if(!(parType is PrimitiveTypeBuilder))
                        {
                            if (mof.PackagedElement.Where(cb => cb.Name == parType.Name).FirstOrDefault() == null)
                            {
                                // the class does not exist in MOF, so the metaclass of the parameter of the operation needs to be added

                                // the class does not exist in MOF, so the metaclass of the attribute needs to be added
                                // look for it in UML

                                var umlElement = umlModel.Objects.First(cb => cb.MName == parType.Name);

                                if(umlElement is ClassBuilder umlClass)
                                {
                                    ClassBuilder clone = MergeHelper.CloneClassIntoMofModel(umlClass, mofFactory);
                                    mof.PackagedElement.Add(clone);

                                    // adding to dictionary
                                    umlToMof.Add(umlClass, clone);
                                    counter++;

                                    // cloning superclasses and their superclasses too
                                    int c;
                                    if ((c = AddGeneralsToMof(umlModel, umlClass, mof, mofFactory, umlToMof)) > 0)
                                    {
                                        Console.WriteLine("  " + c + " superclasses added in AddReferencedClasses()");
                                    }
                                }

                                else if(umlElement is EnumerationBuilder umlEnum)
                                {
                                    EnumerationBuilder clone = MergeHelper.CloneEnumIntoMofModel(umlEnum, mofFactory);
                                    mof.PackagedElement.Add(clone);
                                    counter++;
                                }
                                else
                                {
                                    Console.WriteLine("\t" + parType.Name + " not class nor enum");
                                }
                            }
                        }
                    }
                }
            }

            return counter;
        }

        static int AddGeneralsToMof(MutableModel umlModel, ClassBuilder umlClass,
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
                    ClassBuilder generalClone = MergeHelper.CloneClassIntoMofModel(generalClass, mofFactory);
                    mof.PackagedElement.Add(generalClone);
                    Console.WriteLine("    " + generalClone.Name + " superclass to " + umlClass.Name + " added to mof.");
                    // don't forget to add to dictionary for subsetted props and associations
                    umlToMof.Add(generalClass, generalClone);
                    counter++;
                    counter += AddGeneralsToMof(umlModel, generalClass, mof, mofFactory, umlToMof);
                }
                else
                {
                    // there is already a class with the same name in the MOF model, so let's merge it
                    ClassBuilder classInMof = mof.PackagedElement.OfType<ClassBuilder>().First(cb => cb.Name == g.General.Name);
                    ClassBuilder classInUml = umlModel.Objects.OfType<ClassBuilder>().First(cb => cb.Name == g.General.Name);
                    classInMof = MergeHelper.MergeClasses(classInMof, classInUml, mofFactory);

                    Console.WriteLine("  ...;;; " + g.General.Name);
                }
            }
            return counter;
        }

        public static void AddAllReferencedClasses(PackageBuilder mof, MutableModel umlModel, MofFactory mofFactory, Dictionary<MutableObject, MutableObject> umlToMof)
        {
            int c;
            while ((c = UmlToMofHelper.AddReferencedClasses(mof, umlModel, mofFactory, umlToMof)) != 0)
            {
                // adding classes that are referenced by new classes
                Console.WriteLine("\t " + c + " referenced classes or enums added");
            }
        }


        public static int AddSuperClasses(PackageBuilder mof, MutableModel umlModel, MofFactory mofFactory, Dictionary<MutableObject, MutableObject> umlToMof)
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
    }
}
