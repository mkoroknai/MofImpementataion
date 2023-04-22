using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MetaDslx.Modeling;

namespace MofImplementationLib.Model.Internal
{
    internal static class IEnumerableExtensions
    {
        public static HashSet<T> ToSet<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        }
    }

    class MofImplementation : MofImplementationBase
    {
        public override IReadOnlyCollection<TypeBuilder> Association_ComputeProperty_EndType(AssociationBuilder _this)
        {
            return _this.MemberEnd.Select(me => me.Type).ToSet();
        }

        public override IReadOnlyList<ParameterBuilder> BehavioralFeature_InputParameters(BehavioralFeatureBuilder _this)
        {
            return _this.OwnedParameter.Where(p => p.Direction == ParameterDirectionKind.In || p.Direction == ParameterDirectionKind.Inout).ToList();
        }

        public override bool BehavioralFeature_IsDistinguishableFrom(BehavioralFeatureBuilder _this, NamedElementBuilder n, NamespaceBuilder ns)
        {
            if (n is BehavioralFeatureBuilder nBehavior && ns.GetNamesOfMember(_this).Any(m => ns.GetNamesOfMember(n).Contains(m)))
            {
                if (_this.OwnedParameter.Count != nBehavior.OwnedParameter.Count) return true;
                for (int i = 0; i < _this.OwnedParameter.Count; i++)
                {
                    var thisParam = _this.OwnedParameter[i];
                    var otherParam = nBehavior.OwnedParameter[i];
                    if (thisParam.Name != otherParam.Name) return true;
                    if (thisParam.Type != otherParam.Type) return true;
                    if (thisParam.Effect != otherParam.Effect) return true;
                    if (thisParam.Direction != otherParam.Direction) return true;
                    if (thisParam.IsException != otherParam.IsException) return true;
                    if (thisParam.IsStream != otherParam.IsStream) return true;
                    if (thisParam.IsOrdered != otherParam.IsOrdered) return true;
                    if (thisParam.IsUnique != otherParam.IsUnique) return true;
                    if (thisParam.Lower != otherParam.Lower) return true;
                    if (thisParam.Upper != otherParam.Upper) return true;
                }
                return false;
            }
            return true;
        }

        public override IReadOnlyList<ParameterBuilder> BehavioralFeature_OutputParameters(BehavioralFeatureBuilder _this)
        {
            return _this.OwnedParameter.Where(p => p.Direction == ParameterDirectionKind.Out || p.Direction == ParameterDirectionKind.Inout || p.Direction == ParameterDirectionKind.Return).ToList();
        }

        public override BehavioredClassifierBuilder Behavior_BehavioredClassifier(BehaviorBuilder _this, ElementBuilder from)
        {
            if (from is BehavioredClassifierBuilder behavioredClassifier) return behavioredClassifier;
            else if (from.Owner == null) return null;
            else return _this.BehavioredClassifier(from.Owner);
        }

        public override BehavioredClassifierBuilder Behavior_ComputeProperty_Context(BehaviorBuilder _this)
        {
            if (_this.NestedClassifier != null) return null;
            var b = _this.BehavioredClassifier(_this.Owner);
            if (b is BehaviorBuilder behavior && behavior.Context != null) return behavior.Context;
            else return b;
        }

        public override IReadOnlyList<ParameterBuilder> Behavior_InputParameters(BehaviorBuilder _this)
        {
            return _this.OwnedParameter.Where(p => p.Direction == ParameterDirectionKind.In || p.Direction == ParameterDirectionKind.Inout).ToList();
        }

        public override IReadOnlyList<ParameterBuilder> Behavior_OutputParameters(BehaviorBuilder _this)
        {
            return _this.OwnedParameter.Where(p => p.Direction == ParameterDirectionKind.Out || p.Direction == ParameterDirectionKind.Inout || p.Direction == ParameterDirectionKind.Return).ToList();
        }

        public override IReadOnlyList<PropertyBuilder> Classifier_AllAttributes(ClassifierBuilder _this)
        {
            var result = new List<PropertyBuilder>(_this.Attribute);
            foreach (var parent in _this.Parents())
            {
                foreach (var attr in parent.AllAttributes())
                {
                    if (!result.Contains(attr)) result.Add(attr);
                }
            }
            return result.Where(p => _this.Member.Contains(p)).ToList();
        }

        public override IReadOnlyCollection<FeatureBuilder> Classifier_AllFeatures(ClassifierBuilder _this)
        {
            var members = _this.Member.OfType<FeatureBuilder>();
            var list = members.ToList();
            return _this.Member.OfType<FeatureBuilder>().ToSet();
        }

        public override IReadOnlyCollection<ClassifierBuilder> Classifier_AllParents(ClassifierBuilder _this)
        {
            var parents = _this.Parents();
            var result = new HashSet<ClassifierBuilder>(parents);
            foreach (var p in parents)
            {
                result.UnionWith(p.AllParents());
            }
            return result;
        }

        public override IReadOnlyCollection<InterfaceBuilder> Classifier_AllRealizedInterfaces(ClassifierBuilder _this)
        {
            var result = new HashSet<InterfaceBuilder>(_this.DirectlyRealizedInterfaces());
            foreach (var p in _this.AllParents())
            {
                result.UnionWith(p.DirectlyRealizedInterfaces());
            }
            return result;
        }

        public override IReadOnlyCollection<StructuralFeatureBuilder> Classifier_AllSlottableFeatures(ClassifierBuilder _this)
        {
            var result = new HashSet<StructuralFeatureBuilder>(_this.Member.OfType<StructuralFeatureBuilder>());
            foreach (var parent in _this.AllParents())
            {
                result.UnionWith(parent.Attribute);
            }
            return result;
        }

        public override IReadOnlyCollection<InterfaceBuilder> Classifier_AllUsedInterfaces(ClassifierBuilder _this)
        {
            var result = new HashSet<InterfaceBuilder>(_this.DirectlyUsedInterfaces());
            foreach (var p in _this.AllParents())
            {
                result.UnionWith(p.DirectlyUsedInterfaces());
            }
            return result;
        }

        public override IReadOnlyCollection<ClassifierBuilder> Classifier_ComputeProperty_General(ClassifierBuilder _this)
        {
            return _this.Parents();
        }

        public override IReadOnlyCollection<NamedElementBuilder> Classifier_ComputeProperty_InheritedMember(ClassifierBuilder _this)
        {
            return _this.Inherit(_this.Parents().SelectMany(p => p.InheritableMembers(_this)).ToSet()).ToSet();
        }

        public override bool Classifier_ConformsTo(ClassifierBuilder _this, TypeBuilder other)
        {
            return other is ClassifierBuilder otherClassifier && (_this == otherClassifier || _this.AllParents().Contains(otherClassifier));
        }

        public override IReadOnlyCollection<InterfaceBuilder> Classifier_DirectlyRealizedInterfaces(ClassifierBuilder _this)
        {
            return _this.ClientDependency.Where(dep => dep is RealizationBuilder && dep.Supplier.All(sup => sup is InterfaceBuilder)).SelectMany(dep => dep.Supplier.OfType<InterfaceBuilder>()).ToSet();
        }

        public override IReadOnlyCollection<InterfaceBuilder> Classifier_DirectlyUsedInterfaces(ClassifierBuilder _this)
        {
            throw new NotImplementedException();
            //return _this.ClientDependency.Where(dep => dep is UsageBuilder && dep.Client.All(sup => sup is InterfaceBuilder)).SelectMany(dep => dep.Client.OfType<InterfaceBuilder>()).ToSet();
        }

        public override bool Classifier_HasVisibilityOf(ClassifierBuilder _this, NamedElementBuilder n)
        {
            //Debug.Assert(_this.Member.Contains(n) || _this.AllParents().Any(p => p.Member.Contains(n)));
            return n.Visibility != VisibilityKind.Private;
        }

        public override IReadOnlyCollection<NamedElementBuilder> Classifier_Inherit(ClassifierBuilder _this, IReadOnlyCollection<NamedElementBuilder> inhs)
        {
            return inhs.Where(inh => !(inh is RedefinableElementBuilder re && !_this.OwnedMember.OfType<RedefinableElementBuilder>().Where(m => m.RedefinedElement.Contains(re)).Any())).ToSet();
        }

        public override IReadOnlyCollection<NamedElementBuilder> Classifier_InheritableMembers(ClassifierBuilder _this, ClassifierBuilder c)
        {
            //Debug.Assert(c.AllParents().Contains(_this));
            return _this.Member.Where(m => c.HasVisibilityOf(m)).ToSet();
        }

        public override bool Classifier_IsSubstitutableFor(ClassifierBuilder _this, ClassifierBuilder contract)
        {
            return _this.Substitution.Any(sub => sub.Contract == contract);
        }

        public override bool Classifier_IsTemplate(ClassifierBuilder _this)
        {
            return _this.OwnedTemplateSignature != null || _this.General.Any(g => g.IsTemplate());
        }

        public override bool Classifier_MaySpecializeType(ClassifierBuilder _this, ClassifierBuilder c)
        {
            return c.GetType().IsAssignableFrom(_this.GetType());
        }

        public override IReadOnlyCollection<ClassifierBuilder> Classifier_Parents(ClassifierBuilder _this)
        {
            return _this.Generalization.Select(g => g.General).ToSet();
        }

        public override IReadOnlyCollection<ExtensionBuilder> Class_ComputeProperty_Extension(ClassBuilder _this)
        {
            var result = new HashSet<ExtensionBuilder>();
            foreach (var ext in _this.MModel.Objects.OfType<ExtensionBuilder>())
            {
                var endTypes = ext.MemberEnd.Select(me => me.Type as ClassifierBuilder);
                if (endTypes.Contains(_this) || endTypes.Any(et => et.AllParents().Contains(_this)))
                {
                    result.Add(ext);
                }
            }
            return result;
        }

        public override IReadOnlyCollection<ClassBuilder> Class_ComputeProperty_SuperClass(ClassBuilder _this)
        {
            return _this.General.Where(cls => cls is ClassBuilder).Cast<ClassBuilder>().ToSet();
        }

        public override IReadOnlyCollection<ConnectorEndBuilder> ConnectableElement_ComputeProperty_End(ConnectableElementBuilder _this)
        {
            return _this.MModel.Objects.OfType<ConnectorEndBuilder>().Where(ce => ce.Role == _this).ToSet();
        }

        public override bool ConnectionPointReference_IsConsistentWith(ConnectionPointReferenceBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            return redefiningElement is ConnectionPointReferenceBuilder;
        }

        public override PropertyBuilder ConnectorEnd_ComputeProperty_DefiningEnd(ConnectorEndBuilder _this)
        {
            if (_this.Connector.Type == null) return null;
            int index = _this.Connector.End.ToList().IndexOf(_this);
            return _this.Connector.Type.MemberEnd[index];
        }

        public override ConnectorKind Connector_ComputeProperty_Kind(ConnectorBuilder _this)
        {
            if (_this.End.Any(e => e.Role is PortBuilder && e.PartWithPort == null && !((PortBuilder)e.Role).IsBehavior)) return ConnectorKind.Delegation;
            else return ConnectorKind.Assembly;
        }

        public override IReadOnlyCollection<PackageableElementBuilder> DeploymentTarget_ComputeProperty_DeployedElement(DeploymentTargetBuilder _this)
        {
            return _this.Deployment.SelectMany(d => d.DeployedArtifact).OfType<ArtifactBuilder>().SelectMany(a => a.Manifestation).Select(m => m.UtilizedElement).ToSet();
        }

        public override string ElementImport_GetName(ElementImportBuilder _this)
        {
            return _this.Alias ?? _this.ImportedElement.Name;
        }

        public override IReadOnlyCollection<ElementBuilder> Element_AllOwnedElements(ElementBuilder _this)
        {
            var result = new HashSet<ElementBuilder>(_this.OwnedElement);
            foreach (var e in _this.OwnedElement)
            {
                result.UnionWith(e.AllOwnedElements());
            }
            return result;
        }

        public override ClassBuilder Element_ComputeProperty_Metaclass(ElementBuilder _this)
        {
            //_this.MMetaClass as ClassBuilder;
            throw new NotImplementedException();
        }

        public override ElementBuilder Element_Container(ElementBuilder _this)
        {
            return _this.MParent as ElementBuilder;
            //throw new NotImplementedException();
        }

        public override void Element_Delete(ElementBuilder _this)
        {
            throw new NotImplementedException();
        }

        public override ClassBuilder Element_GetMetaClass(ElementBuilder _this)
        {
            throw new NotImplementedException();
        }

        public override bool Element_IsInstanceOfType(ElementBuilder _this, ClassBuilder type, bool includesSubtypes)
        {
            throw new NotImplementedException();
        }

        public override bool Element_MustBeOwned(ElementBuilder _this)
        {
            return true;
        }

        public override IReadOnlyCollection<PortBuilder> EncapsulatedClassifier_ComputeProperty_OwnedPort(EncapsulatedClassifierBuilder _this)
        {
            return _this.OwnedAttribute.OfType<PortBuilder>().Distinct().ToList();
        }

        public override EnumerationBuilder EnumerationLiteral_ComputeProperty_Classifier(EnumerationLiteralBuilder _this)
        {
            return _this.Enumeration;
        }

        public override int ExtensionEnd_ComputeProperty_Lower(ExtensionEndBuilder _this)
        {
            return 0;
        }

        public override int ExtensionEnd_LowerBound(ExtensionEndBuilder _this)
        {
            return _this.LowerValue?.IntegerValue() ?? 0;
        }

        public override bool Extension_ComputeProperty_IsRequired(ExtensionBuilder _this)
        {
            return _this.OwnedEnd.LowerBound() == 1;
        }

        public override ClassBuilder Extension_ComputeProperty_Metaclass(ExtensionBuilder _this)
        {
            return _this.MetaclassEnd().Type as ClassBuilder;
        }

        public override PropertyBuilder Extension_MetaclassEnd(ExtensionBuilder _this)
        {
            var assoc = _this as AssociationBuilder;
            return _this.MemberEnd.Where(p => !assoc.OwnedEnd.Contains((ExtensionEndBuilder)p)).First();
        }

        public override ReflectiveSequenceBuilder Extent_Elements(ExtentBuilder _this)
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyCollection<ElementBuilder> Extent_ElementsOfType(ExtentBuilder _this, ClassBuilder type, bool includesSubtypes)
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyCollection<ElementBuilder> Extent_LinkedElements(ExtentBuilder _this, AssociationBuilder Association, ElementBuilder endElement, bool end1ToEnd2Direction)
        {
            throw new NotImplementedException();
        }

        public override bool Extent_LinkExists(ExtentBuilder _this, AssociationBuilder Association, ElementBuilder firstElement, ElementBuilder secondElement)
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyCollection<LinkBuilder> Extent_LinksOfType(ExtentBuilder _this, AssociationBuilder type, bool includesSubtypes)
        {
            throw new NotImplementedException();
        }

        public override bool Extent_UseContainment(ExtentBuilder _this)
        {
            throw new NotImplementedException();
        }

        public override string Factory_ConvertToString(FactoryBuilder _this, DataTypeBuilder dataType, ObjectBuilder Object)
        {
            throw new NotImplementedException();
        }

        public override ElementBuilder Factory_Create(FactoryBuilder _this, ClassBuilder metaClass)
        {
            throw new NotImplementedException();
        }

        public override ElementBuilder Factory_CreateElement(FactoryBuilder _this, ClassBuilder Class, IReadOnlyCollection<ArgumentBuilder> arguments)
        {
            throw new NotImplementedException();
        }

        public override ObjectBuilder Factory_CreateFromString(FactoryBuilder _this, DataTypeBuilder dataType, string String)
        {
            throw new NotImplementedException();
        }

        public override LinkBuilder Factory_CreateLink(FactoryBuilder _this, AssociationBuilder Association, ElementBuilder firstElement, ElementBuilder secondElement)
        {
            throw new NotImplementedException();
        }

        public override void Link_Delete(LinkBuilder _this)
        {
            throw new NotImplementedException();
        }

        public override bool Link_Equals(LinkBuilder _this, LinkBuilder otherLink)
        {
            throw new NotImplementedException();
        }

        public override bool LiteralBoolean_BooleanValue(LiteralBooleanBuilder _this)
        {
            return _this.Value;
        }

        public override bool LiteralBoolean_IsComputable(LiteralBooleanBuilder _this)
        {
            return true;
        }

        public override int LiteralInteger_IntegerValue(LiteralIntegerBuilder _this)
        {
            return _this.Value;
        }

        public override bool LiteralInteger_IsComputable(LiteralIntegerBuilder _this)
        {
            return true;
        }

        public override bool LiteralNull_IsComputable(LiteralNullBuilder _this)
        {
            return true;
        }

        public override bool LiteralNull_IsNull(LiteralNullBuilder _this)
        {
            return true;
        }

        public override bool LiteralReal_IsComputable(LiteralRealBuilder _this)
        {
            return true;
        }

        public override double LiteralReal_RealValue(LiteralRealBuilder _this)
        {
            return _this.Value;
        }

        public override bool LiteralString_IsComputable(LiteralStringBuilder _this)
        {
            return true;
        }

        public override string LiteralString_StringValue(LiteralStringBuilder _this)
        {
            return _this.Value;
        }

        public override bool LiteralUnlimitedNatural_IsComputable(LiteralUnlimitedNaturalBuilder _this)
        {
            return true;
        }

        public override long LiteralUnlimitedNatural_UnlimitedValue(LiteralUnlimitedNaturalBuilder _this)
        {
            return _this.Value;
        }

        public override bool MultiplicityElement_CompatibleWith(MultiplicityElementBuilder _this, MultiplicityElementBuilder other)
        {
            var upperBound = _this.UpperBound();
            var otherUpperBound = other.UpperBound();
            return other.LowerBound() <= _this.LowerBound() && (otherUpperBound < 0 || (upperBound >= 0 && upperBound <= otherUpperBound));
        }

        public override int MultiplicityElement_ComputeProperty_Lower(MultiplicityElementBuilder _this)
        {
            //return ((LiteralInteger)_this.LowerValue)?.Value ?? 0;
            return _this.LowerBound();
        }

        public override long MultiplicityElement_ComputeProperty_Upper(MultiplicityElementBuilder _this)
        {
            //return ((LiteralUnlimitedNaturalBuilder)_this.UpperValue)?.Value ?? 1;
            return _this.UpperBound();
        }

        public override bool MultiplicityElement_IncludesMultiplicity(MultiplicityElementBuilder _this, MultiplicityElementBuilder M)
        {
            var upperBound = _this.UpperBound();
            var mUpperBound = M.UpperBound();
            return _this.LowerBound() <= M.LowerBound() && (upperBound < 0 || (mUpperBound >= 0 && mUpperBound <= upperBound));
        }

        public override bool MultiplicityElement_Is(MultiplicityElementBuilder _this, int lowerbound, long upperbound)
        {
            return _this.LowerBound() == lowerbound && _this.UpperBound() == upperbound;
        }

        public override bool MultiplicityElement_IsMultivalued(MultiplicityElementBuilder _this)
        {
            var upperBound = _this.UpperBound();
            return upperBound < 0 || upperBound > 1;
        }

        public override int MultiplicityElement_LowerBound(MultiplicityElementBuilder _this)
        {
            return _this.LowerValue?.IntegerValue() ?? 1;
        }

        public override long MultiplicityElement_UpperBound(MultiplicityElementBuilder _this)
        {
            return _this.UpperValue?.UnlimitedValue() ?? 1;
        }

        public override IReadOnlyList<NamespaceBuilder> NamedElement_AllNamespaces(NamedElementBuilder _this)
        {
            if (_this.Owner is TemplateParameterBuilder templateParameter && templateParameter.Signature.Template is NamespaceBuilder enclosingNamespace)
            {
                var result = new List<NamespaceBuilder>(enclosingNamespace.AllNamespaces());
                result.Insert(0, enclosingNamespace);
                return result;
            }
            else if (_this.Namespace != null)
            {
                var result = new List<NamespaceBuilder>(_this.Namespace.AllNamespaces());
                result.Insert(0, _this.Namespace);
                return result;
            }
            else
            {
                return ImmutableArray<NamespaceBuilder>.Empty;
            }
        }

        public override IReadOnlyCollection<PackageBuilder> NamedElement_AllOwningPackages(NamedElementBuilder _this)
        {
            if (_this.Namespace is PackageBuilder owningPackage)
            {
                var result = owningPackage.AllOwningPackages().ToSet();
                result.Add(owningPackage);
                return result;
            }
            return ImmutableArray<PackageBuilder>.Empty;
        }

        public override IReadOnlyCollection<DependencyBuilder> NamedElement_ComputeProperty_ClientDependency(NamedElementBuilder _this)
        {
            return _this.MModel.Objects.OfType<DependencyBuilder>().Where(d => d.Client.Contains(_this)).ToSet();
        }

        public override string NamedElement_ComputeProperty_QualifiedName(NamedElementBuilder _this)
        {
            if (_this.Name == null) return null;
            string result = _this.Name;
            foreach (var ns in _this.AllNamespaces())
            {
                if (ns.Name == null) return null;
                result = ns.Name + _this.Separator() + result;
            }
            return result;
        }

        public override bool NamedElement_IsDistinguishableFrom(NamedElementBuilder _this, NamedElementBuilder n, NamespaceBuilder ns)
        {
            return !_this.GetType().IsAssignableFrom(n.GetType()) && !n.GetType().IsAssignableFrom(_this.GetType()) || !ns.GetNamesOfMember(_this).Any(m => ns.GetNamesOfMember(n).Contains(m));
        }

        public override string NamedElement_Separator(NamedElementBuilder _this)
        {
            return "::";
        }

        public override IReadOnlyCollection<PackageableElementBuilder> Namespace_ComputeProperty_ImportedMember(NamespaceBuilder _this)
        {
            var result = _this.ElementImport.Select(ei => ei.ImportedElement).ToSet();
            foreach (var pi in _this.PackageImport)
            {
                result.UnionWith(pi.ImportedPackage.VisibleMembers());
            }
            return result;
        }

        public override IReadOnlyCollection<PackageableElementBuilder> Namespace_ExcludeCollisions(NamespaceBuilder _this, IReadOnlyCollection<PackageableElementBuilder> imps)
        {
            return imps.Where(imp1 => !imps.Any(imp2 => !imp1.IsDistinguishableFrom(imp2, _this))).ToSet();
        }

        public override IReadOnlyCollection<string> Namespace_GetNamesOfMember(NamespaceBuilder _this, NamedElementBuilder element)
        {
            if (_this.OwnedMember.Contains(element))
            {
                return ImmutableArray.Create(element.Name);
            }
            else
            {
                var elementImports = _this.ElementImport.Where(ei => ei.ImportedElement == element).Select(ei => ei.ImportedElement);
                if (elementImports.Any()) return elementImports.Select(el => el.Name).ToSet();
                else return _this.PackageImport.Where(pi => pi.ImportedPackage.VisibleMembers().OfType<NamedElementBuilder>().Contains(element)).SelectMany(pi => pi.ImportedPackage.GetNamesOfMember(element)).ToSet();
            }
        }

        public override IReadOnlyCollection<PackageableElementBuilder> Namespace_ImportMembers(NamespaceBuilder _this, IReadOnlyCollection<PackageableElementBuilder> imps)
        {
            return _this.ExcludeCollisions(imps).Where(imp => _this.OwnedMember.All(mem => imp.IsDistinguishableFrom(mem, _this))).ToSet();
        }

        public override bool Namespace_MembersAreDistinguishable(NamespaceBuilder _this)
        {
            return _this.Member.All(memb => _this.Member.Where(m => m != memb).All(other => memb.IsDistinguishableFrom(other, _this)));
        }

        public override bool Object_Equals(ObjectBuilder _this, ObjectBuilder element)
        {
            //if (_this == element) return true;
            //if(_this is ClassBuilder cThis)
            //{
            //    if(element is ClassBuilder cElement)
            //    {
            //        return cThis == cElement;
            //    }
            //    return false;
            //}
            //if(_this is DataTypeBuilder dtThis)
            //{
            //    if(element is DataTypeBuilder dtElement)
            //    {
            //        return dtThis.Name == dtElement.Name;
            //    }
            //    return false;
            //}
            //return false;
            throw new NotImplementedException();
        }

        public override ObjectBuilder Object_Get(ObjectBuilder _this, PropertyBuilder property)
        {
            throw new NotImplementedException();
        }

        public override ObjectBuilder Object_Invoke(ObjectBuilder _this, OperationBuilder op, IReadOnlyCollection<ArgumentBuilder> arguments)
        {
            throw new NotImplementedException();
        }

        public override bool Object_IsSet(ObjectBuilder _this, PropertyBuilder property)
        {
            throw new NotImplementedException();
        }

        public override void Object_Set(ObjectBuilder _this, PropertyBuilder property, ObjectBuilder value)
        {
            throw new NotImplementedException();
        }

        public override void Object_Unset(ObjectBuilder _this, PropertyBuilder property)
        {
            throw new NotImplementedException();
        }

        public override ParameterBuilder OpaqueExpression_ComputeProperty_Result(OpaqueExpressionBuilder _this)
        {
            return _this.Behavior?.OwnedParameter.FirstOrDefault();
        }

        public override bool OpaqueExpression_IsIntegral(OpaqueExpressionBuilder _this)
        {
            return false;
        }

        public override bool OpaqueExpression_IsNonNegative(OpaqueExpressionBuilder _this)
        {
            return false;
        }

        public override bool OpaqueExpression_IsPositive(OpaqueExpressionBuilder _this)
        {
            return false;
        }

        public override int OpaqueExpression_Value(OpaqueExpressionBuilder _this)
        {
            return 0;
        }

        public override bool Operation_ComputeProperty_IsOrdered(OperationBuilder _this)
        {
            return _this.ReturnResult().Any(res => res.IsOrdered);
        }

        public override bool Operation_ComputeProperty_IsUnique(OperationBuilder _this)
        {
            return _this.ReturnResult().Any(res => res.IsUnique);
        }

        public override int Operation_ComputeProperty_Lower(OperationBuilder _this)
        {
            return _this.ReturnResult().FirstOrDefault()?.Lower ?? 0;
        }

        public override TypeBuilder Operation_ComputeProperty_Type(OperationBuilder _this)
        {
            return _this.ReturnResult().FirstOrDefault()?.Type;
        }

        public override long Operation_ComputeProperty_Upper(OperationBuilder _this)
        {
            return _this.ReturnResult().FirstOrDefault()?.Upper ?? 0;
        }

        public override bool Operation_IsConsistentWith(OperationBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            //Debug.Assert(redefiningElement.IsRedefinitionContextValid(_this));
            var op = redefiningElement as OperationBuilder;
            if (op == null) return false;
            if (op.OwnedParameter.Count != _this.OwnedParameter.Count) return false;
            for (int i = 0; i < _this.OwnedParameter.Count; i++)
            {
                var redefiningParam = op.OwnedParameter[i];
                var redefinedParam = _this.OwnedParameter[i];
                if (redefiningParam.IsUnique != redefinedParam.IsUnique) return false;
                if (redefiningParam.IsOrdered != redefinedParam.IsOrdered) return false;
                if (redefiningParam.Direction != redefinedParam.Direction) return false;
                if (!redefiningParam.Type.ConformsTo(redefinedParam.Type) && !redefinedParam.Type.ConformsTo(redefiningParam.Type)) return false;
                if (redefiningParam.Direction != ParameterDirectionKind.Inout || redefinedParam.CompatibleWith(redefiningParam) && redefiningParam.CompatibleWith(redefinedParam)) return false;
                if (redefiningParam.Direction != ParameterDirectionKind.In || redefinedParam.CompatibleWith(redefiningParam)) return false;
                if (redefiningParam.Direction != ParameterDirectionKind.Out && redefiningParam.Direction != ParameterDirectionKind.Return || redefiningParam.CompatibleWith(redefinedParam)) return false;
            }
            return true;
        }

        public override IReadOnlyCollection<ParameterBuilder> Operation_ReturnResult(OperationBuilder _this)
        {
            return _this.OwnedParameter.Where(p => p.Direction == ParameterDirectionKind.Return).ToSet();
        }

        public override IReadOnlyCollection<StereotypeBuilder> Package_AllApplicableStereotypes(PackageBuilder _this)
        {
            var ownedPackages = _this.OwnedMember.OfType<PackageBuilder>();
            var result = _this.OwnedStereotype.ToSet();
            result.UnionWith(ownedPackages.SelectMany(p => p.AllApplicableStereotypes()));
            return result;
        }

        public override IReadOnlyCollection<PackageBuilder> Package_ComputeProperty_NestedPackage(PackageBuilder _this)
        {
            return _this.PackagedElement.OfType<PackageBuilder>().ToSet();
        }

        public override IReadOnlyCollection<StereotypeBuilder> Package_ComputeProperty_OwnedStereotype(PackageBuilder _this)
        {
            return _this.PackagedElement.OfType<StereotypeBuilder>().ToSet();
        }

        public override IReadOnlyCollection<TypeBuilder> Package_ComputeProperty_OwnedType(PackageBuilder _this)
        {
            return _this.PackagedElement.OfType<TypeBuilder>().ToSet();
        }

        public override ProfileBuilder Package_ContainingProfile(PackageBuilder _this)
        {
            if (_this is ProfileBuilder profile) return profile;
            else return (_this.Namespace as PackageBuilder)?.ContainingProfile();
        }

        public override bool Package_MakesVisible(PackageBuilder _this, NamedElementBuilder el)
        {
            //Debug.Assert(_this.Member.Contains(el));
            return _this.OwnedMember.Contains(el) ||
                _this.ElementImport.Any(ei => ei.ImportedElement.Visibility == VisibilityKind.Public && ei.ImportedElement == el) ||
                _this.PackageImport.Any(pi => pi.Visibility == VisibilityKind.Public && pi.ImportedPackage.Member.Contains(el));
        }

        public override bool Package_MustBeOwned(PackageBuilder _this)
        {
            return false;
        }

        public override IReadOnlyCollection<PackageableElementBuilder> Package_VisibleMembers(PackageBuilder _this)
        {
            return _this.Member.Where(m => m is PackageableElementBuilder && _this.MakesVisible(m)).Cast<PackageableElementBuilder>().ToSet();
        }

        public override bool ParameterableElement_IsCompatibleWith(ParameterableElementBuilder _this, ParameterableElementBuilder p)
        {
            return p.GetType().IsAssignableFrom(_this.GetType());
        }

        public override bool ParameterableElement_IsTemplateParameter(ParameterableElementBuilder _this)
        {
            return _this.TemplateParameter != null;
        }

        public override string Parameter_ComputeProperty_Default(ParameterBuilder _this)
        {
            return _this.Type == MofInstance.String ? _this.DefaultValue?.StringValue() : null;
        }

        public override IReadOnlyCollection<InterfaceBuilder> Port_BasicProvided(PortBuilder _this)
        {
            return _this.Type is InterfaceBuilder intf ? ImmutableArray.Create(intf) : (_this.Type as ClassifierBuilder)?.AllRealizedInterfaces() ?? ImmutableArray<InterfaceBuilder>.Empty;
        }

        public override IReadOnlyCollection<InterfaceBuilder> Port_BasicRequired(PortBuilder _this)
        {
            return ((ClassifierBuilder)_this.Type).AllUsedInterfaces();
        }

        public override IReadOnlyCollection<InterfaceBuilder> Port_ComputeProperty_Provided(PortBuilder _this)
        {
            return _this.IsConjugated ? _this.BasicRequired() : _this.BasicProvided();
        }

        public override IReadOnlyCollection<InterfaceBuilder> Port_ComputeProperty_Required(PortBuilder _this)
        {
            return _this.IsConjugated ? _this.BasicRequired() : _this.BasicProvided();
        }

        public override bool Property_ComputeProperty_IsComposite(PropertyBuilder _this)
        {
            return _this.Aggregation == AggregationKind.Composite;
        }

        public override PropertyBuilder Property_ComputeProperty_Opposite(PropertyBuilder _this)
        {
            if (_this.Association?.MemberEnd.Count == 2)
            {
                if (_this.Association.MemberEnd[0] == _this) return _this.Association.MemberEnd[1];
                if (_this.Association.MemberEnd[1] == _this) return _this.Association.MemberEnd[0];
            }
            return null;
        }

        public override bool Property_IsAttribute(PropertyBuilder _this)
        {
            return _this.Class != null;
        }

        public override bool Property_IsCompatibleWith(PropertyBuilder _this, ParameterableElementBuilder p)
        {
            return p.GetType().IsAssignableFrom(_this.GetType()) && (!(p is TypedElementBuilder te) || _this.Type.ConformsTo(te.Type));
        }

        public override bool Property_IsConsistentWith(PropertyBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            //Debug.Assert(redefiningElement.IsRedefinitionContextValid(_this));
            var prop = redefiningElement as PropertyBuilder;
            if (prop == null) return false;
            var thisUpper = _this.UpperBound();
            var propUpper = prop.UpperBound();
            return prop.Type.ConformsTo(_this.Type) && prop.LowerBound() >= _this.LowerBound() && (thisUpper < 0 || propUpper <= thisUpper) && (!_this.IsComposite || prop.IsComposite);
        }

        public override bool Property_IsNavigable(PropertyBuilder _this)
        {
            return _this.Class != null || _this.Association.NavigableOwnedEnd.Contains(_this);
        }

        public override IReadOnlyCollection<TypeBuilder> Property_SubsettingContext(PropertyBuilder _this)
        {
            if (_this.Association != null) return _this.Association.MemberEnd.Where(me => me != _this).Select(me => me.Type).ToSet();
            else if (_this.Class != null) return ImmutableArray.Create(_this.Class);
            else return ImmutableArray<TypeBuilder>.Empty;
        }

        public override bool Pseudostate_IsConsistentWith(PseudostateBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            //Debug.Assert(redefiningElement.IsRedefinitionContextValid(_this));
            return redefiningElement is PseudostateBuilder && ((PseudostateBuilder)redefiningElement).Kind == _this.Kind;
        }

        public override bool RedefinableElement_IsConsistentWith(RedefinableElementBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            //Debug.Assert(redefiningElement.IsRedefinitionContextValid(_this));
            return false;
        }

        public override bool RedefinableElement_IsRedefinitionContextValid(RedefinableElementBuilder _this, RedefinableElementBuilder redefinedElement)
        {
            foreach (var c in _this.RedefinitionContext)
            {
                var parents = c.AllParents();
                if (redefinedElement.RedefinitionContext.All(rc => parents.Contains(rc))) return true;
            }
            return false;
        }

        public override IReadOnlyCollection<TemplateParameterBuilder> RedefinableTemplateSignature_ComputeProperty_InheritedParameter(RedefinableTemplateSignatureBuilder _this)
        {
            return _this.ExtendedSignature.SelectMany(es => es.Parameter).ToSet();
        }

        public override bool RedefinableTemplateSignature_IsConsistentWith(RedefinableTemplateSignatureBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            Debug.Assert(redefiningElement.IsRedefinitionContextValid(_this));
            return redefiningElement is RedefinableTemplateSignatureBuilder;
        }

        public override bool ReflectiveCollection_Add(ReflectiveCollectionBuilder _this, ObjectBuilder Object)
        {
            throw new NotImplementedException();
        }

        public override bool ReflectiveCollection_AddAll(ReflectiveCollectionBuilder _this, ReflectiveCollectionBuilder objects)
        {
            throw new NotImplementedException();
        }

        public override void ReflectiveCollection_Clear(ReflectiveCollectionBuilder _this)
        {
            throw new NotImplementedException();
        }

        public override bool ReflectiveCollection_Remove(ReflectiveCollectionBuilder _this, ObjectBuilder Object)
        {
            throw new NotImplementedException();
        }

        public override int ReflectiveCollection_Size(ReflectiveCollectionBuilder _this)
        {
            throw new NotImplementedException();
        }

        public override void ReflectiveSequence_Add(ReflectiveSequenceBuilder _this, int index, ObjectBuilder Object)
        {
            throw new NotImplementedException();
        }

        public override ObjectBuilder ReflectiveSequence_Get(ReflectiveSequenceBuilder _this, int index)
        {
            throw new NotImplementedException();
        }

        public override bool ReflectiveSequence_Remove(ReflectiveSequenceBuilder _this, int index)
        {
            throw new NotImplementedException();
        }

        public override ObjectBuilder ReflectiveSequence_Set(ReflectiveSequenceBuilder _this, int index, ObjectBuilder Object)
        {
            throw new NotImplementedException();
        }

        public override bool Region_BelongsToPSM(RegionBuilder _this)
        {
            if (_this.StateMachine != null) return _this.StateMachine is ProtocolStateMachineBuilder;
            else return _this.State == null || _this.State.Container.BelongsToPSM();
        }

        public override ClassifierBuilder Region_ComputeProperty_RedefinitionContext(RegionBuilder _this)
        {
            return _this.ContainingStateMachine();
        }

        public override StateMachineBuilder Region_ContainingStateMachine(RegionBuilder _this)
        {
            return _this.StateMachine ?? _this.State.ContainingStateMachine();
        }

        public override bool Region_IsConsistentWith(RegionBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            //Debug.Assert(redefiningElement.IsRedefinitionContextValid(_this));
            return true;
        }

        public override bool Region_IsRedefinitionContextValid(RegionBuilder _this, RedefinableElementBuilder redefinedElement)
        {
            if (redefinedElement is RegionBuilder redefinedRegion)
            {
                if (_this.StateMachine == null) return (_this.State.RedefinedVertex as StateBuilder)?.Region.Contains(redefinedRegion) ?? false;
                else return _this.StateMachine.ExtendedStateMachine.Count > 0 && _this.StateMachine.ExtendedStateMachine.Any(sm => sm.Region.Contains(redefinedRegion));
            }
            return false;
        }

        public override bool StateMachine_Ancestor(StateMachineBuilder _this, VertexBuilder s1, VertexBuilder s2)
        {
            if (s2 == s1) return true;
            else if (s1.Container.StateMachine != null) return true;
            else if (s2.Container.StateMachine != null) return false;
            else return _this.Ancestor(s1, s2.Container.State);
        }

        public override bool StateMachine_IsConsistentWith(StateMachineBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            return true;
        }

        public override bool StateMachine_IsRedefinitionContextValid(StateMachineBuilder _this, RedefinableElementBuilder redefinedElement)
        {
            if (redefinedElement is StateMachineBuilder redefinedStateMachine)
            {
                var parentContext = redefinedStateMachine.Context;
                if (_this.Context == null) return parentContext == null && _this.AllParents().Contains(redefinedElement);
                else return parentContext != null && _this.Context.AllParents().Contains(parentContext);
            }
            return false;
        }

        public override RegionBuilder StateMachine_LCA(StateMachineBuilder _this, VertexBuilder s1, VertexBuilder s2)
        {
            if (_this.Ancestor(s1, s2)) return s2.Container;
            else if (_this.Ancestor(s2, s1)) return s1.Container;
            else return _this.LCA(s1.Container.State, s2.Container.State);
        }

        public override StateBuilder StateMachine_LCAState(StateMachineBuilder _this, VertexBuilder v1, VertexBuilder v2)
        {
            if (v2 is StateBuilder v2state && _this.Ancestor(v1, v2)) return v2state;
            else if (v1 is StateBuilder v1state && _this.Ancestor(v2, v1)) return v1state;
            else if (v1.Container.State == null || v2.Container.State == null) return null;
            else return _this.LCAState(v1.Container.State, v2.Container.State);
        }

        public override bool State_ComputeProperty_IsComposite(StateBuilder _this)
        {
            return _this.Region.Count > 0;
        }

        public override bool State_ComputeProperty_IsOrthogonal(StateBuilder _this)
        {
            return _this.Region.Count > 1;
        }

        public override bool State_ComputeProperty_IsSimple(StateBuilder _this)
        {
            return _this.Region.Count == 0 && !_this.IsSubmachineState;
        }

        public override bool State_ComputeProperty_IsSubmachineState(StateBuilder _this)
        {
            return _this.Submachine != null;
        }

        public override StateMachineBuilder State_ContainingStateMachine(StateBuilder _this)
        {
            return _this.Container.ContainingStateMachine();
        }

        public override bool State_IsConsistentWith(StateBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            //Debug.Assert(redefiningElement.IsRedefinitionContextValid(_this));
            return redefiningElement is StateBuilder redefiningState && (_this.Submachine == null || (redefiningState.Submachine != null && redefiningState.Submachine.ExtendedStateMachine.Contains(_this.Submachine)));
        }

        public override ProfileBuilder Stereotype_ComputeProperty_Profile(StereotypeBuilder _this)
        {
            return _this.ContainingProfile();
        }

        public override ProfileBuilder Stereotype_ContainingProfile(StereotypeBuilder _this)
        {
            return (_this.Namespace as PackageBuilder)?.ContainingProfile();
        }

        public override string StringExpression_StringValue(StringExpressionBuilder _this)
        {
            StringBuilder sb = new StringBuilder();
            if (_this.SubExpression.Count > 0)
            {
                foreach (var se in _this.SubExpression)
                {
                    sb.Append(se);
                }
            }
            else
            {
                foreach (var op in _this.Operand)
                {
                    sb.Append(op);
                }
            }
            return sb.ToString();
        }

        public override IReadOnlyCollection<ConnectableElementBuilder> StructuredClassifier_AllRoles(StructuredClassifierBuilder _this)
        {
            return _this.AllFeatures().OfType<ConnectableElementBuilder>().ToSet();
        }

        public override IReadOnlyCollection<PropertyBuilder> StructuredClassifier_ComputeProperty_Part(StructuredClassifierBuilder _this)
        {
            return _this.OwnedAttribute.Where(attr => attr.IsComposite).ToSet();
        }

        public override bool TemplateableElement_IsTemplate(TemplateableElementBuilder _this)
        {
            return _this.OwnedTemplateSignature != null;
        }

        public override IReadOnlyCollection<ParameterableElementBuilder> TemplateableElement_ParameterableElements(TemplateableElementBuilder _this)
        {
            return _this.AllOwnedElements().OfType<ParameterableElementBuilder>().ToSet();
        }

        public override ClassifierBuilder Transition_ComputeProperty_RedefinitionContext(TransitionBuilder _this)
        {
            return _this.ContainingStateMachine();
        }

        public override StateMachineBuilder Transition_ContainingStateMachine(TransitionBuilder _this)
        {
            return _this.Container.ContainingStateMachine();
        }

        public override bool Transition_IsConsistentWith(TransitionBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            return redefiningElement.IsRedefinitionContextValid(_this);
        }

        public override bool Type_ConformsTo(TypeBuilder _this, TypeBuilder other)
        {
            return false;
        }

        public override bool Type_IsInstance(TypeBuilder _this, ObjectBuilder Object)
        {
            throw new NotImplementedException();
        }

        public override string URIExtent_ContextURI(URIExtentBuilder _this)
        {
            throw new NotImplementedException();
        }

        public override ElementBuilder URIExtent_Element(URIExtentBuilder _this, string uri)
        {
            throw new NotImplementedException();
        }

        public override string URIExtent_Uri(URIExtentBuilder _this, ElementBuilder Object)
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyCollection<UseCaseBuilder> UseCase_AllIncludedUseCases(UseCaseBuilder _this)
        {
            var additions = _this.Include.Select(inc => inc.Addition);
            var result = additions.ToSet();
            foreach (var uc in additions)
            {
                result.UnionWith(uc.AllIncludedUseCases());
            }
            return result;
        }

        public override bool ValueSpecification_BooleanValue(ValueSpecificationBuilder _this)
        {
            return default;
        }

        public override int ValueSpecification_IntegerValue(ValueSpecificationBuilder _this)
        {
            return default;
        }

        public override bool ValueSpecification_IsCompatibleWith(ValueSpecificationBuilder _this, ParameterableElementBuilder p)
        {
            return p.GetType().IsAssignableFrom(_this.GetType()) && (!(p is TypedElementBuilder) || _this.Type.ConformsTo(((TypedElementBuilder)p).Type));
        }

        public override bool ValueSpecification_IsComputable(ValueSpecificationBuilder _this)
        {
            return default;
        }

        public override bool ValueSpecification_IsNull(ValueSpecificationBuilder _this)
        {
            return default;
        }

        public override double ValueSpecification_RealValue(ValueSpecificationBuilder _this)
        {
            return default;
        }

        public override string ValueSpecification_StringValue(ValueSpecificationBuilder _this)
        {
            return default;
        }

        public override long ValueSpecification_UnlimitedValue(ValueSpecificationBuilder _this)
        {
            return default;
        }

        public override IReadOnlyCollection<TransitionBuilder> Vertex_ComputeProperty_Incoming(VertexBuilder _this)
        {
            return _this.MModel.Objects.OfType<TransitionBuilder>().Where(t => t.Target == _this).ToSet();
        }

        public override IReadOnlyCollection<TransitionBuilder> Vertex_ComputeProperty_Outgoing(VertexBuilder _this)
        {
            return _this.MModel.Objects.OfType<TransitionBuilder>().Where(t => t.Source == _this).ToSet();
        }

        public override ClassifierBuilder Vertex_ComputeProperty_RedefinitionContext(VertexBuilder _this)
        {
            return _this.ContainingStateMachine();
        }

        public override StateMachineBuilder Vertex_ContainingStateMachine(VertexBuilder _this)
        {
            if (_this.Container != null) return _this.Container.ContainingStateMachine();
            else if (_this is PseudostateBuilder pseudostate && (pseudostate.Kind == PseudostateKind.EntryPoint || pseudostate.Kind == PseudostateKind.ExitPoint)) return pseudostate.StateMachine;
            else if (_this is ConnectionPointReferenceBuilder connectionPointReference) return connectionPointReference.State.ContainingStateMachine();
            else return null;
        }

        public override bool Vertex_IsConsistentWith(VertexBuilder _this, RedefinableElementBuilder redefiningElement)
        {
            //Debug.Assert(redefiningElement.IsRedefinitionContextValid(_this));
            return redefiningElement is VertexBuilder redefiningVertex && _this.Owner is RedefinableElementBuilder owner && owner.RedefinedElement.Contains(redefiningVertex.Owner);
        }

        public override bool Vertex_IsContainedInRegion(VertexBuilder _this, RegionBuilder r)
        {
            if (_this.Container == r) return true;
            else if (r.State == null) return false;
            else return _this.Container.State.IsContainedInRegion(r);
        }

        public override bool Vertex_IsContainedInState(VertexBuilder _this, StateBuilder s)
        {
            if (!s.IsComposite || _this.Container == null) return false;
            else if (_this.Container.State == s) return true;
            else return _this.Container.State.IsContainedInState(s);
        }
    }
}
