using MetaDslx.Modeling;
using MofBootstrapLib.Generator;
using MofBootstrapLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

// copy this to the beginning of Mof.mm
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

// copy this into ConnectorEnd in Mof.mm
///// <summary>
///// The Connector of which the ConnectorEnd is the endpoint.
///// </summary>
//Connector Connector subsets Element.Owner;
///// <summary>

namespace MofBootstrap
{
    public static class MergeHelper
    {
        /// <summary>
        /// renames properties of all classes in mofModel
        /// </summary>
        /// <param name="mofModel"></param>
        public static void RenameMofProperties(MutableModel mofModel)
        {
            foreach (var pcks in mofModel.Objects.OfType<PackageBuilder>())
            {
                foreach (var cls in pcks.PackagedElement)
                {
                    if(cls is ClassBuilder cb)
                    {
                        MergeHelper.RenameMofProperties(cb);
                    }
                }
            }
        }

        /// <summary>
        /// renames properties of one class
        /// </summary>
        /// <param name="mofClass"></param>
        public static void RenameMofProperties(ClassBuilder mofClass)
        {
            if(mofClass != null)
            {
                foreach (var attr in mofClass.OwnedAttribute)
                {
                    // renaming conflicting element names
                    if (attr.Name == "object" || attr.Name == "string" || attr.Name == "association" || attr.Name == "class")
                    {
                        attr.Name = attr.Name[0].ToString().ToUpper() + attr.Name[1..];
                    }
                    //else if (attr.Name == "element")
                    //{
                    //    attr.Name = "_element";
                    //}

                }

                foreach(var op in mofClass.OwnedOperation)
                {
                    foreach (var par in op.OwnedParameter)
                    {
                        if (par.Name == "object" || par.Name == "string" || par.Name == "association" || par.Name == "class")
                        {
                            par.Name = par.Name[0].ToString().ToUpper() + par.Name[1..];
                        }
                        //else if (par.Name == "element")
                        //{
                        //    par.Name = "_element";
                        //}

                    }
                }
            }

        }

        /// <summary>
        /// copies a class
        /// </summary>
        /// <param name="cb">class to be copied</param>
        /// <param name="mofFactory">this is where the new class is created</param>
        /// <returns>the copy of the class</returns>
        public static ClassBuilder CloneClassIntoMofModel(ClassBuilder cb, MofFactory mofFactory)
        {
            if(cb.Name == "RedefinableElement")
            {
                ;
            }
            ClassBuilder newClass = mofFactory.Class();

            newClass.Name = cb.Name;
            newClass.IsAbstract = cb.IsAbstract;
            newClass.Visibility = cb.Visibility;

            // copying operations
            foreach (var op in cb.OwnedOperation)
            {
                OperationBuilder newOp = CloneOperation(op, mofFactory);
                newClass.OwnedOperation.Add(newOp);
            }

            // copying attributes
            foreach (var attr in cb.OwnedAttribute)
            {
                PropertyBuilder newAttr = CloneAttribute(attr, mofFactory);
                newClass.OwnedAttribute.Add(newAttr);
            }

            // copying generalizations
            foreach (var inheritance in cb.Generalization)
            {
                GeneralizationBuilder newInherit = mofFactory.Generalization();
                newInherit.General = inheritance.General;
                newClass.Generalization.AddLazy(() => newInherit);
            }


            // adding comments to Class
            CopyComment(cb, newClass, mofFactory);

            return newClass;
        }

        /// <summary>
        /// copies an enumeration into MOF model
        /// </summary>
        /// <param name="eb">enumeration to be copied</param>
        /// <param name="mofFactory">to be copied here</param>
        /// <returns></returns>
        public static EnumerationBuilder CloneEnumIntoMofModel(EnumerationBuilder eb, MofFactory mofFactory)
        {
            EnumerationBuilder newEnum = mofFactory.Enumeration();

            newEnum.Name = eb.Name;
            newEnum.Visibility = eb.Visibility;

            // adding literals
            foreach (var enumLit in eb.OwnedLiteral)
            {
                EnumerationLiteralBuilder newLiteral = mofFactory.EnumerationLiteral();
                newLiteral.Name = enumLit.Name;

                // adding comments to literals
                CopyComment(enumLit, newLiteral, mofFactory);

                newEnum.OwnedLiteral.Add(newLiteral);
            }

            // adding comments to Enumeration
            CopyComment(eb, newEnum, mofFactory);

            return newEnum;
        }


        public static AssociationBuilder CloneAssociation(AssociationBuilder ab, MofFactory mofFactory)
        {
            AssociationBuilder newAssoc = mofFactory.Association();

            newAssoc.Name = ab.Name;
            newAssoc.Visibility = ab.Visibility;
            newAssoc.IsDerived = ab.IsDerived;
            newAssoc.IsAbstract = ab.IsAbstract;

            foreach(var ownedEnd in ab.OwnedEnd)
            {
                PropertyBuilder newOwnedEnd = mofFactory.Property();
                newOwnedEnd.Name = ownedEnd.Name;
                newOwnedEnd.Type = ownedEnd.Type;


                if(ownedEnd.LowerValue is LiteralIntegerBuilder oelv)
                {
                    LiteralIntegerBuilder newValue = mofFactory.LiteralInteger();
                    newValue.Value = oelv.Value;
                    newValue.Name = oelv.Name;
                    newOwnedEnd.LowerValue = newValue;
                }
                else
                {
                    if(ownedEnd.LowerValue != (null)) Console.WriteLine("                      Associacion.OwnedEnd.LowerValue is " + ownedEnd.LowerValue.MMetaClass);
                }

                if (ownedEnd.UpperValue is LiteralUnlimitedNaturalBuilder oeuv)
                {
                    LiteralUnlimitedNaturalBuilder newValue = mofFactory.LiteralUnlimitedNatural();
                    newValue.Value = oeuv.Value;
                    newValue.Name = oeuv.Name;
                    newOwnedEnd.UpperValue = newValue;
                }
                else
                {
                    if (ownedEnd.UpperValue != (null)) Console.WriteLine("                      Associacion.OwnedEnd.LowerValue is " + ownedEnd.UpperValue.MMetaClass);
                }
                newAssoc.OwnedEnd.Add(newOwnedEnd);
            }

            //foreach (var memberEnd in ab.MemberEnd)
            //{
            //    PropertyBuilder newMemberEnd = mofFactory.Property();
            //    newMemberEnd.Name = memberEnd.Name;
            //    newMemberEnd.Type = memberEnd.Type;
            //    if (newAssoc.MemberEnd.Where(me => me.Name == newMemberEnd.Name).FirstOrDefault() == null)
            //    {
            //        newAssoc.MemberEnd.Add(newMemberEnd);
            //    }
            //}

            return newAssoc;
        }

        // create a copy of the operation
        public static OperationBuilder CloneOperation(OperationBuilder op, MofFactory mofFactory)
        {
            OperationBuilder newOp = mofFactory.Operation();

            // setting important stuff
            newOp.Name = op.Name;
            newOp.Visibility = op.Visibility;
            newOp.IsReadOnly = op.IsReadOnly;
            newOp.IsAbstract = op.IsAbstract;
            newOp.IsQuery = op.IsQuery;

            // adding operation parameters
            foreach (var param in op.OwnedParameter)
            {
                ParameterBuilder newParam = mofFactory.Parameter();

                // setting important stuff
                newParam.Name = param.Name;
                newParam.Direction = param.Direction;
                newParam.IsOrdered = param.IsOrdered;
                newParam.IsStream = param.IsStream;
                newParam.IsUnique = param.IsUnique;
                newParam.Visibility = param.Visibility;
                newParam.Type = param.Type;

                // setting lower and upper values
                if (param.LowerValue is LiteralIntegerBuilder lv) // in the UML.xmi file, lowerValue always seems to be of type LiteralInteger
                {
                    LiteralIntegerBuilder newLowerValue = mofFactory.LiteralInteger();
                    newLowerValue.Value = lv.Value;
                    newParam.LowerValue = newLowerValue;
                }
                else if(param.LowerValue is LiteralStringBuilder lvs)
                {
                    LiteralStringBuilder newLowerValue = mofFactory.LiteralString();
                    newLowerValue.Value = lvs.Value;
                    newParam.LowerValue = newLowerValue;
                }
                else
                {
                    if (param.LowerValue != null) Console.WriteLine("!!!!!!!!!!!param.LowerValue is not of type LiteralInteger!!!!!!!!!!!");
                }
                if (param.UpperValue is LiteralUnlimitedNaturalBuilder uv) // in the UML.xmi file, upperValue always seems to be of type LiteralUnlimitedNatural
                {
                    LiteralUnlimitedNaturalBuilder newUpperValue = mofFactory.LiteralUnlimitedNatural();
                    newUpperValue.Value = uv.Value;
                    newParam.UpperValue = newUpperValue;
                }
                else
                {
                    if (param.UpperValue != null) Console.WriteLine("!!!!!!!!!!!param.UpperValue is not of type LiteralUnlimitedNatural!!!!!!!!!!!");
                }

                // copying comments
                CopyComment(param, newParam, mofFactory);

                // adding copied parameter to operation
                newOp.OwnedParameter.Add(newParam);
            }

            // adding exceptions
            //foreach(var ex in op.RaisedException)
            //{
            //    TypeBuilder newException = mofFctr.Type();

            //    newException.Name = ex.Name;
            //    newException.Visibility = ex.Visibility;

            // adding comments to exception
            //foreach(var comment in ex.OwnedComment)
            //{
            //    CommentBuilder newComment = mofFctr.Comment();

            //    foreach(var bod in comment.Body)
            //    {

            //    }
            //}
            //}

            // copying rules
            foreach(var rule in op.OwnedRule)
            {
                if(rule is ConstraintBuilder rcb)
                {
                    // !!!!!!!!! NEEDS MORE WORK !!!!!!!!!!
                    ConstraintBuilder newConstraint = mofFactory.Constraint();
                    newConstraint.Name = rcb.Name;
                    if(rcb.Specification is OpaqueExpressionBuilder roeb)
                    {
                        OpaqueExpressionBuilder newSpec = mofFactory.OpaqueExpression();
                        foreach(string b in roeb.Body)
                        {
                            newSpec.Body.Add(b);
                        }
                        newSpec.Language = roeb.Language;
                        newConstraint.Specification = newSpec;
                        newOp.OwnedRule.Add(newConstraint);
                    }
                    else
                    {
                        Console.WriteLine("!!!!!!!!!!!'" + rule.Name + ".Specification' is not of type OpaqueExpression!!!!!!!!!!!!");
                    }
                }
                else
                {
                    Console.WriteLine("!!!!!!!!!!!ownedRule '" + rule.Name + "' is not of type ConstraintBuilder!!!!!!!!!!!!");
                }
            }

            // adding comments to operation
            CopyComment(op, newOp, mofFactory);

            return newOp;
        }

        public static PropertyBuilder CloneAttribute(PropertyBuilder attr, MofFactory mofFactory)
        {
            //Console.WriteLine(".-.-.-.-.-.-. " + attr.Name);
            PropertyBuilder newAttr = mofFactory.Property();
            // renaming conflicting element names
            if(attr.Name == "object" || attr.Name == "string" || attr.Name == "association" || attr.Name == "class" || attr.Name == "element")
            {
                //newAttr.Name = "_" + attr.Name;
                newAttr.Name = attr.Name;
            }
            else
            {
                newAttr.Name = attr.Name;
            }
            newAttr.Type = attr.Type;
            newAttr.Visibility = attr.Visibility;
            newAttr.IsDerived = attr.IsDerived;
            newAttr.IsDerivedUnion = attr.IsDerivedUnion;
            newAttr.IsOrdered = attr.IsOrdered;
            newAttr.IsReadOnly = attr.IsReadOnly;
            newAttr.IsUnique = attr.IsUnique;
            newAttr.Aggregation = attr.Aggregation;
            //newAttr.RedefinedProperty = attr.RedefinedProperty;
            if(attr.Name == "redefinedElement")
            {
                ;
                newAttr.IsDerivedUnion = true;
            }
            var mofModel = mofFactory.MModel;
            //PackageBuilder mofPckg = mofModel.Objects.OfType<PackageBuilder>().First()

            // adding associations
            foreach (var assoc in attr.Association)
            {
                // I think the existing association needs to be added to newAttr list,
                // and a new one does not have to be created



                //AssociationBuilder newAssoc = CloneAssociation(assoc, mofFactory);

                //AssociationBuilder myAssoc = mofModel.Objects.OfType<AssociationBuilder>().First(assb => assb.Name == assoc.Name);

                //newAttr.Association.Add(myAssoc);
            }

            foreach (var redProp in attr.RedefinedProperty)
            {
                PropertyBuilder prop = redProp;
                newAttr.RedefinedProperty.Add(redProp); // ????????
            }

            // copying comments
            CopyComment(attr, newAttr, mofFactory);

            // copying subsetted properties
            //foreach(var subsetted in attr.SubsettedProperty)
            //{
            //    PropertyBuilder newSubset = mofFctr.Property();

            //    newSubset.Name = subsetted.Name;
            //    newSubset.Type = subsetted.Type;
            //    newSubset.Visibility = subsetted.Visibility;

            //    newAttr.SubsettedProperty.Add(newSubset);
            //}

            // setting lower and upper values
            if (attr.LowerValue is LiteralIntegerBuilder lv) // in the UML.xmi file, lowerValue always seems to be of type LiteralInteger
            {
                LiteralIntegerBuilder newLowerValue = mofFactory.LiteralInteger();
                newLowerValue.Value = lv.Value;
                newAttr.LowerValue = newLowerValue;
            }
            else if (attr.LowerValue is LiteralStringBuilder lsv) // in the UML.xmi file, lowerValue always seems to be of type LiteralInteger
            {
                LiteralStringBuilder newLowerValue = mofFactory.LiteralString();
                newLowerValue.Value = lsv.Value;
                newAttr.LowerValue = newLowerValue;
            }
            else
            {
                if (attr.LowerValue != null) Console.WriteLine("!!!!!!!!!!!attr.LowerValue is not of type LiteralInteger!!!!!!!!!!!");
            }
            if (attr.UpperValue is LiteralUnlimitedNaturalBuilder uv) // in the UML.xmi file, upperValue always seems to be of type LiteralUnlimitedNatural
            {
                LiteralUnlimitedNaturalBuilder newUpperValue = mofFactory.LiteralUnlimitedNatural();
                newUpperValue.Value = uv.Value;
                newAttr.UpperValue = newUpperValue;
            }
            else
            {
                if (attr.UpperValue != null) Console.WriteLine("!!!!!!!!!!!attr.UpperValue is not of type LiteralUnlimitedNatural!!!!!!!!!!!");
            }

            return newAttr;
        }

        /// <summary>
        /// copies the comments of any element
        /// </summary>
        /// <param name="toBeCopied">element whose comments are to be copied</param>
        /// <param name="copyHere">element where the comments are copied</param>
        /// <param name="mofFactory">this is where the new element is</param>
        /// <returns></returns>
        public static ElementBuilder CopyComment(ElementBuilder toBeCopied, ElementBuilder copyHere, MofFactory mofFactory)
        {

            foreach (var comment in toBeCopied.OwnedComment)
            {
                CommentBuilder newComment = mofFactory.Comment();
                newComment.Body = comment.Body;
                copyHere.OwnedComment.Add(newComment);
            }

            return copyHere;
        }

        /// <summary>
        /// merges two classes
        /// </summary>
        /// <param name="receiver">receiving class</param>
        /// <param name="donor">merged class</param>
        /// <param name="mofFactory">where receiving class is</param>
        /// <returns>what receiver becomes</returns>
        public static ClassBuilder MergeClasses(ClassBuilder receiver, ClassBuilder donor, MofFactory mofFactory)
        {
            if(receiver.Name != donor.Name)
            {
                throw new Exception("The two classes don't have the same name.");
            }
            // rules from https://www.omg.org/spec/UML
            if (donor.IsAbstract == false) receiver.IsAbstract = false;
            //if (donor.Visibility == VisibilityKind.Public) receiver.Visibility = VisibilityKind.Public; // everything is public

            // checking and merging operations
            foreach(var op in donor.OwnedOperation)
            {
                if (receiver.OwnedOperation.Where(o => o.Name == op.Name).FirstOrDefault() != null)
                {
                    Console.WriteLine("             Operation '" + receiver.Name + "." + op.Name +"' already contained!!!");
                    // check if parameters are the same
                    // needs to be implemented
                }
                else
                {
                    OperationBuilder newOp = CloneOperation(op, mofFactory);
                    receiver.OwnedOperation.Add(newOp);
                }
            }
            
            foreach(var attr in donor.OwnedAttribute)
            {
                if(receiver.OwnedAttribute.Where(a => a.Name == attr.Name).FirstOrDefault() != null)
                {
                    Console.WriteLine("             Attribute '" + receiver.Name + "." + attr.Name + "' already contained!!!");
                    // needs to be implemented
                }
                else
                {
                    PropertyBuilder newAttr = CloneAttribute(attr, mofFactory);
                    receiver.OwnedAttribute.Add(newAttr);
                }
            }


            // copying generalizations
            foreach (var inheritance in donor.Generalization)
            {
                if (receiver.Generalization.Where(pe => pe.General.Name == inheritance.General.Name).FirstOrDefault() != null)
                {
                    Console.WriteLine("             General '" + receiver.Name + "." + inheritance.General + "' already contained!!!");
                    // nothing to do
                }
                else
                {
                    GeneralizationBuilder newInherit = mofFactory.Generalization();
                    newInherit.General = inheritance.General;
                    receiver.Generalization.AddLazy(() => newInherit);
                }
            }

            return receiver;
        }

        /// <summary>
        /// Merges two packages
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="donor"></param>
        /// <param name="mofFactory"></param>
        /// <returns></returns>
        public static PackageBuilder MergePackages(PackageBuilder receiver, PackageBuilder donor, MofFactory mofFactory)
        {

            Console.WriteLine();
            Console.WriteLine("Merging " + donor + " into " + receiver + "...");
            Console.WriteLine();

            // copying associations first

            foreach (var element in donor.PackagedElement)
            {
                //Console.WriteLine("  " + element.Name);

                if (element is PackageBuilder epb)
                {
                    // ignore
                }
                else if (element is ClassBuilder ecb)
                {
                    // ignore
                }
                else if (element is EnumerationBuilder enumb)
                {
                    // ignore
                }
                else if (element is AssociationBuilder ab)
                {
                    if (receiver.PackagedElement.Where(pe => pe.Name == ab.Name).FirstOrDefault() != null)
                    {
                        Console.WriteLine("      ### Package already contains assoc: " + ab.Name);
                        //Console.WriteLine("      Association merge needs to be implemented!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    else
                    {
                        receiver.PackagedElement.Add(CloneAssociation(ab, mofFactory));
                    }
                }
                else
                {
                    Console.WriteLine("      element " + element + " was ignored.");
                }
            }

            foreach (var element in donor.PackagedElement)
            {
                //Console.WriteLine("  " + element.Name);

                if (element is PackageBuilder epb)
                {

                    foreach (var element2 in epb.PackagedElement)
                    {
                        if (element2 is ClassBuilder ecb)
                        {
                            if (receiver.PackagedElement.Where(pe => pe.Name == ecb.Name).FirstOrDefault() != null)
                            {
                                Console.WriteLine("      ### Package already contains class: " + ecb.Name);
                                // there is already a class with the same name in the package, so they have to be merged
                                ClassBuilder recClass = receiver.PackagedElement.OfType<ClassBuilder>().First(pe => pe.Name == ecb.Name);
                                receiver.PackagedElement.Add(MergeClasses(recClass, ecb, mofFactory));
                                Console.WriteLine("      ### " + ecb.Name + " merged");
                            }
                            else
                            {
                                if (receiver.PackagedElement.Where(pe => pe.Name == ecb.Name).FirstOrDefault() != null)
                                {
                                    Console.WriteLine("      ### Package already contains enum: " + ecb.Name);
                                    // there is already an enumeration with the same name in the package, so they have to be merged
                                    Console.WriteLine("      Enumeration merge needs to be implemented.");
                                }
                                else
                                {
                                    ClassBuilder newClass = CloneClassIntoMofModel(ecb, mofFactory);
                                    receiver.PackagedElement.Add(newClass);
                                }
                            }
                        }
                        else if (element2 is EnumerationBuilder enumb)
                        {
                            EnumerationBuilder newEnum = MergeHelper.CloneEnumIntoMofModel(enumb, mofFactory);
                        }
                        else
                        {
                            Console.WriteLine("      element " + element2 + " was ignored.");
                        }
                    }
                }
                else if (element is ClassBuilder ecb)
                {
                    if (receiver.PackagedElement.Where(pe => pe.Name == ecb.Name).FirstOrDefault() != null)
                    {
                        Console.WriteLine("      ### Package already contains class: " + ecb.Name);
                        // there is already a class with the same name in the package, so they have to be merged
                        ClassBuilder recClass = receiver.PackagedElement.OfType<ClassBuilder>().First(pe => pe.Name == ecb.Name);
                        receiver.PackagedElement.Add(MergeClasses(recClass, ecb, mofFactory));
                        Console.WriteLine("      ### " + ecb.Name + " merged");
                    }
                    else
                    {
                        ClassBuilder newClass = CloneClassIntoMofModel(ecb, mofFactory);
                        receiver.PackagedElement.Add(newClass);
                    }
                }
                else if (element is EnumerationBuilder enumb)
                {
                    if (receiver.PackagedElement.Where(pe => pe.Name == enumb.Name).FirstOrDefault() != null)
                    {
                        Console.WriteLine("      ### Package already contains enum: " + enumb.Name);
                        // there is already an enumeration with the same name in the package, so they have to be merged
                        //Console.WriteLine("      Enumeration merge needs to be implemented!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    else
                    {
                        EnumerationBuilder newEnum = CloneEnumIntoMofModel(enumb, mofFactory);
                        receiver.PackagedElement.Add(newEnum);
                    }
                }
                else if(element is AssociationBuilder ab)
                {
                    if (receiver.PackagedElement.Where(pe => pe.Name == ab.Name).FirstOrDefault() != null)
                    {
                        Console.WriteLine("      ### Package already contains assoc: " + ab.Name);
                        // there is already an association with the same name in the package, so they have to be merged
                        //Console.WriteLine("      Association merge needs to be implemented!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    else
                    {
                        receiver.PackagedElement.Add(CloneAssociation(ab, mofFactory));
                    }
                }
                else
                {
                    Console.WriteLine("      element " + element + " was ignored.");
                }
            }

            return receiver;
        }


        /// <summary>
        /// Matches the associations, and sets the MemberEnds
        /// </summary>
        /// <param name="umlModel"></param>
        /// <param name="mergePckgs"></param>
        /// <param name="cmof"></param>
        public static void AssociationMatching(MutableModel umlModel, PackageMergeBuilder mergePckgs, PackageBuilder cmof)
        {
            foreach(var pckg in mergePckgs.MergedPackage.PackagedElement)
            {

                Console.WriteLine("  " + pckg.Name + "  Type: " + pckg.GetType());

                if (pckg is PackageBuilder pckgpb)
                {
                    var umlPckg = umlModel.Objects.OfType<PackageBuilder>().First(pb => pb.Name == pckg.Name);

                    // going through elements of package, that can be ClassBuilder, EnumerationBuilder etc...
                    foreach (var pElem in umlPckg.PackagedElement)
                    {
                        if (pElem is ClassBuilder cb)
                        {
                            // find class in cmof

                            ClassBuilder cmofClass = null;

                            cmofClass = cmof.PackagedElement.OfType<ClassBuilder>().First(cmofcb => cmofcb.Name == cb.Name);

                            // find associations of class
                            foreach(var attr in cb.OwnedAttribute)
                            {
                                PropertyBuilder cmofAttr = null;
                                try
                                {
                                    cmofAttr = cmofClass.OwnedAttribute.First(oa => oa.Name == attr.Name);
                                }
                                catch
                                {
                                    cmofAttr = cmofClass.OwnedAttribute.First(oa => oa.Name == "_" + attr.Name);
                                }
                                foreach (var ass in attr.Association)
                                {
                                    AssociationBuilder cmofAss = cmof.PackagedElement.OfType<AssociationBuilder>().First(asb => asb.Name == ass.Name);
                                    // set the correct association
                                    cmofAttr.Association.Add(cmofAss);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("  ## Packaged Element is not of type PackageBuilder");
                }
            }
        }

        public static void SetSubsets(MutableModel uml, PackageBuilder cmof, Dictionary<MutableObject, MutableObject> umlToMof)
        {
            foreach(var pair in umlToMof)
            {
                var key = pair.Key;
                if(key is ClassBuilder umlClass)
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
                                if(!(umlSubsP.MParent is AssociationBuilder))
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

                                    Console.WriteLine(umlSubsP.Name + "\t" + umlSubsP.MParent.MName);

                                    //cmofAttr.SubsettedProperty.Add(cmofSubsP);
                                }
                                else
                                {
                                    Console.WriteLine("\t\t" + umlSubsP.MParent.MName + " is Association");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
