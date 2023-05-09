namespace MofImplementationLib.Model
{
    metamodel Mof(Uri="http://www.omg.org/spec/MOF"); 
    
    /// <summary>
    /// Boolean is used for logical expressions, consisting of the predefined values true and false.
    /// </summary>
    const PrimitiveType Boolean;
    /// <summary>
    /// Integer is a primitive type representing integer values.
    /// </summary>
    const PrimitiveType Integer;
    /// <summary>
    /// Real is a primitive type representing the mathematical concept of real.
    /// </summary>
    const PrimitiveType Real;
    /// <summary>
    /// String is a sequence of characters in some suitable character set used to display information about the model. Character sets may include non-Roman alphabets and characters.
    /// </summary>
    const PrimitiveType String;
    /// <summary>
    /// UnlimitedNatural is a primitive type representing unlimited natural values.
    /// </summary>
    const PrimitiveType UnlimitedNatural;

    /// <summary>
    /// VisibilityKind is an enumeration type that defines literals to determine the visibility of Elements in a model.
    /// </summary>
    enum VisibilityKind
    {
        /// <summary>
        /// A Named Element with public visibility is visible to all elements that can access the contents of the Namespace that owns it.
        /// </summary>
        Public,
        /// <summary>
        /// A NamedElement with private visibility is only visible inside the Namespace that owns it.
        /// </summary>
        Private,
        /// <summary>
        /// A NamedElement with protected visibility is visible to Elements that have a generalization relationship to the Namespace that owns it.
        /// </summary>
        Protected,
        /// <summary>
        /// A NamedElement with package visibility is visible to all Elements within the nearest enclosing Package (given that other owning Elements have proper visibility). Outside the nearest enclosing Package, a NamedElement marked as having package visibility is not visible.  Only NamedElements that are not owned by Packages can be marked as having package visibility. 
        /// </summary>
        Package
    }

    /// <summary>
    /// CallConcurrencyKind is an Enumeration used to specify the semantics of concurrent calls to a BehavioralFeature.
    /// </summary>
    enum CallConcurrencyKind
    {
        /// <summary>
        /// No concurrency management mechanism is associated with the BehavioralFeature and, therefore, concurrency conflicts may occur. Instances that invoke a BehavioralFeature need to coordinate so that only one invocation to a target on any BehavioralFeature occurs at once.
        /// </summary>
        Sequential,
        /// <summary>
        /// Multiple invocations of a BehavioralFeature that overlap in time may occur to one instance, but only one is allowed to commence. The others are blocked until the performance of the currently executing BehavioralFeature is complete. It is the responsibility of the system designer to ensure that deadlocks do not occur due to simultaneous blocking.
        /// </summary>
        Guarded,
        /// <summary>
        /// Multiple invocations of a BehavioralFeature that overlap in time may occur to one instance and all of them may proceed concurrently.
        /// </summary>
        Concurrent
    }

    /// <summary>
    /// ParameterDirectionKind is an Enumeration that defines literals used to specify direction of parameters.
    /// </summary>
    enum ParameterDirectionKind
    {
        /// <summary>
        /// Indicates that Parameter values are passed in by the caller. 
        /// </summary>
        In,
        /// <summary>
        /// Indicates that Parameter values are passed in by the caller and (possibly different) values passed out to the caller.
        /// </summary>
        Inout,
        /// <summary>
        /// Indicates that Parameter values are passed out to the caller.
        /// </summary>
        Out,
        /// <summary>
        /// Indicates that Parameter values are passed as return values back to the caller.
        /// </summary>
        Return
    }

    /// <summary>
    /// ParameterEffectKind is an Enumeration that indicates the effect of a Behavior on values passed in or out of its parameters.
    /// </summary>
    enum ParameterEffectKind
    {
        /// <summary>
        /// Indicates that the behavior creates values.
        /// </summary>
        Create,
        /// <summary>
        /// Indicates objects that are values of the parameter have values of their properties, or links in which they participate, or their classifiers retrieved during executions of the behavior.
        /// </summary>
        Read,
        /// <summary>
        /// Indicates objects that are values of the parameter have values of their properties, or links in which they participate, or their classification changed during executions of the behavior.
        /// </summary>
        Update,
        /// <summary>
        /// Indicates objects that are values of the parameter do not exist after executions of the behavior are finished.
        /// </summary>
        Delete
    }

    /// <summary>
    /// AggregationKind is an Enumeration for specifying the kind of aggregation of a Property.
    /// </summary>
    enum AggregationKind
    {
        /// <summary>
        /// Indicates that the Property has no aggregation.
        /// </summary>
        None,
        /// <summary>
        /// Indicates that the Property has shared aggregation.
        /// </summary>
        Shared,
        /// <summary>
        /// Indicates that the Property is aggregated compositely, i.e., the composite object has responsibility for the existence and storage of the composed objects (parts).
        /// </summary>
        Composite
    }

    /// <summary>
    /// ConnectorKind is an enumeration that defines whether a Connector is an assembly or a delegation.
    /// </summary>
    enum ConnectorKind
    {
        /// <summary>
        /// Indicates that the Connector is an assembly Connector.
        /// </summary>
        Assembly,
        /// <summary>
        /// Indicates that the Connector is a delegation Connector.
        /// </summary>
        Delegation
    }

    /// <summary>
    /// PseudostateKind is an Enumeration type that is used to differentiate various kinds of Pseudostates.
    /// </summary>
    enum PseudostateKind
    {
        Initial,
        DeepHistory,
        ShallowHistory,
        Join,
        Fork,
        Junction,
        Choice,
        EntryPoint,
        ExitPoint,
        Terminate
    }

    /// <summary>
    /// TransitionKind is an Enumeration type used to differentiate the various kinds of Transitions.
    /// </summary>
    enum TransitionKind
    {
        /// <summary>
        /// Implies that the Transition, if triggered, occurs without exiting or entering the source State (i.e., it does not cause a state change). This means that the entry or exit condition of the source State will not be invoked. An internal Transition can be taken even if the SateMachine is in one or more Regions nested within the associated State.
        /// </summary>
        Internal,
        /// <summary>
        /// Implies that the Transition, if triggered, will not exit the composite (source) State, but it will exit and re-enter any state within the composite State that is in the current state configuration.
        /// </summary>
        Local,
        /// <summary>
        /// Implies that the Transition, if triggered, will exit the composite (source) State.
        /// </summary>
        External
    }

    class ReflectiveSequence : ReflectiveCollection
    {
    	void Add(int index, Object Object);
    	readonly Object Get(int index);
    	bool Remove(int index);
    	Object Set(int index, Object Object);
    }

    class ReflectiveCollection : Object
    {
    	bool Add(Object Object);
    	bool AddAll(ReflectiveCollection objects);
    	void Clear();
    	bool Remove(Object Object);
    	readonly int Size();
    }

    class URIExtent : Extent
    {
    	readonly string ContextURI();
    	readonly string Uri(Element Object);
    	readonly Element Element(string uri);
    }

    class Extent : Object
    {
    	readonly bool UseContainment();
    	readonly ReflectiveSequence Elements();
    	readonly set<Element> ElementsOfType(Class type, bool includesSubtypes);
    	readonly set<Link> LinksOfType(Association type, bool includesSubtypes);
    	readonly set<Element> LinkedElements(Association Association, Element endElement, bool end1ToEnd2Direction);
    	readonly bool LinkExists(Association Association, Element firstElement, Element secondElement);
    }

    class Factory : Element
    {
    	Package Package;
    	Object CreateFromString(DataType dataType, string String);
    	string ConvertToString(DataType dataType, Object Object);
    	Element Create(Class metaClass);
    	Element CreateElement(Class Class, set<Argument> arguments);
    	Link CreateLink(Association Association, Element firstElement, Element secondElement);
    }

    abstract class Type : PackageableElement
    {
    	/// <summary>
    	/// Specifies the owning Package of this Type, if any.
    	/// </summary>
    	Package Package;
    	readonly bool IsInstance(Object Object);
    	/// <summary>
    	/// The query conformsTo() gives true for a Type that conforms to another. By default, two Types do not conform to each other. This query is intended to be redefined for specific conformance situations.
    	/// </summary>
    	// spec:
    	//     result = (false)
    	readonly bool ConformsTo(Type other);
    }

    class Object
    {
    	readonly Object Get(Property property);
    	readonly bool Equals(Object element);
    	void Set(Property property, Object value);
    	readonly bool IsSet(Property property);
    	void Unset(Property property);
    	Object Invoke(Operation op, set<Argument> arguments);
    }

    abstract class Element : Object
    {
    	derived Class Metaclass;
    	/// <summary>
    	/// The Comments owned by this Element.
    	/// </summary>
    	containment set<Comment> OwnedComment;
    	/// <summary>
    	/// The Elements owned by this Element.
    	/// </summary>
    	containment union set<Element> OwnedElement;
    	/// <summary>
    	/// The Element that owns this Element.
    	/// </summary>
    	Element Owner;
    	readonly Class GetMetaClass();
    	readonly Element Container();
    	readonly bool IsInstanceOfType(Class type, bool includesSubtypes);
    	/// <summary>
    	/// The query allOwnedElements() gives all of the direct and indirect ownedElements of an Element.
    	/// </summary>
    	// spec:
    	//     result = (ownedElement->union(ownedElement->collect(e | e.allOwnedElements()))->asSet())
    	readonly set<Element> AllOwnedElements();
    	/// <summary>
    	/// The query mustBeOwned() indicates whether Elements of this type must have an owner. Subclasses of Element that do not require an owner must override this operation.
    	/// </summary>
    	// spec:
    	//     result = (true)
    	readonly bool MustBeOwned();
    	void Delete();
    }

    /// <summary>
    /// A link is a tuple of values that refer to typed objects.  An Association classifies a set of links, each of which is an instance of the Association.  Each value in the link refers to an instance of the type of the corresponding end of the Association.
    /// </summary>
    class Association : Relationship, Classifier
    {
    	/// <summary>
    	/// The Classifiers that are used as types of the ends of the Association.
    	/// </summary>
    	// spec:
    	//     result = (memberEnd->collect(type)->asSet())
    	derived set<Type> EndType subsets Relationship.RelatedElement;
    	/// <summary>
    	/// Specifies whether the Association is derived from other model elements such as other Associations.
    	/// </summary>
    	bool IsDerived;
    	/// <summary>
    	/// Each end represents participation of instances of the Classifier connected to the end in links of the Association.
    	/// </summary>
    	list<Property> MemberEnd subsets Namespace.Member;
    	/// <summary>
    	/// The navigable ends that are owned by the Association itself.
    	/// </summary>
    	set<Property> NavigableOwnedEnd subsets Association.OwnedEnd;
    	/// <summary>
    	/// The ends that are owned by the Association itself.
    	/// </summary>
    	containment list<Property> OwnedEnd subsets Association.MemberEnd, Classifier.Feature, Namespace.OwnedMember;
    }

    /// <summary>
    /// Relationship is an abstract concept that specifies some kind of relationship between Elements.
    /// </summary>
    abstract class Relationship : Element
    {
    	/// <summary>
    	/// Specifies the elements related by the Relationship.
    	/// </summary>
    	union set<Element> RelatedElement;
    }

    /// <summary>
    /// A Classifier represents a classification of instances according to their Features.
    /// </summary>
    abstract class Classifier : Namespace, Type, TemplateableElement, RedefinableElement
    {
    	/// <summary>
    	/// All of the Properties that are direct (i.e., not inherited or imported) attributes of the Classifier.
    	/// </summary>
    	union list<Property> Attribute subsets Classifier.Feature;
    	/// <summary>
    	/// The CollaborationUses owned by the Classifier.
    	/// </summary>
    	containment set<CollaborationUse> CollaborationUse subsets Element.OwnedElement;
    	/// <summary>
    	/// Specifies each Feature directly defined in the classifier. Note that there may be members of the Classifier that are of the type Feature but are not included, e.g., inherited features.
    	/// </summary>
    	union set<Feature> Feature subsets Namespace.Member;
    	/// <summary>
    	/// The generalizing Classifiers for this Classifier.
    	/// </summary>
    	// spec:
    	//     result = (parents())
    	derived set<Classifier> General;
    	/// <summary>
    	/// The Generalization relationships for this Classifier. These Generalizations navigate to more general Classifiers in the generalization hierarchy.
    	/// </summary>
    	containment set<Generalization> Generalization subsets Element.OwnedElement;
    	/// <summary>
    	/// All elements inherited by this Classifier from its general Classifiers.
    	/// </summary>
    	// spec:
    	//     result = (inherit(parents()->collect(inheritableMembers(self))->asSet()))
    	derived set<NamedElement> InheritedMember subsets Namespace.Member;
    	/// <summary>
    	/// If true, the Classifier can only be instantiated by instantiating one of its specializations. An abstract Classifier is intended to be used by other Classifiers e.g., as the target of Associations or Generalizations.
    	/// </summary>
    	bool IsAbstract;
    	/// <summary>
    	/// If true, the Classifier cannot be specialized.
    	/// </summary>
    	bool IsFinalSpecialization;
    	/// <summary>
    	/// The optional RedefinableTemplateSignature specifying the formal template parameters.
    	/// </summary>
    	containment RedefinableTemplateSignature OwnedTemplateSignature redefines TemplateableElement.OwnedTemplateSignature;
    	/// <summary>
    	/// The UseCases owned by this classifier.
    	/// </summary>
    	containment set<UseCase> OwnedUseCase subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The GeneralizationSet of which this Classifier is a power type.
    	/// </summary>
    	set<GeneralizationSet> PowertypeExtent;
    	/// <summary>
    	/// The Classifiers redefined by this Classifier.
    	/// </summary>
    	set<Classifier> RedefinedClassifier subsets RedefinableElement.RedefinedElement;
    	/// <summary>
    	/// A CollaborationUse which indicates the Collaboration that represents this Classifier.
    	/// </summary>
    	CollaborationUse Representation subsets Classifier.CollaborationUse;
    	/// <summary>
    	/// The Substitutions owned by this Classifier.
    	/// </summary>
    	containment set<Substitution> Substitution subsets Element.OwnedElement, NamedElement.ClientDependency;
    	/// <summary>
    	/// TheClassifierTemplateParameter that exposes this element as a formal parameter.
    	/// </summary>
    	ClassifierTemplateParameter TemplateParameter redefines ParameterableElement.TemplateParameter;
    	/// <summary>
    	/// The set of UseCases for which this Classifier is the subject.
    	/// </summary>
    	set<UseCase> UseCase;
    	/// <summary>
    	/// The query allFeatures() gives all of the Features in the namespace of the Classifier. In general, through mechanisms such as inheritance, this will be a larger set than feature.
    	/// </summary>
    	// spec:
    	//     result = (member->select(oclIsKindOf(Feature))->collect(oclAsType(Feature))->asSet())
    	readonly set<Feature> AllFeatures();
    	/// <summary>
    	/// The query allParents() gives all of the direct and indirect ancestors of a generalized Classifier.
    	/// </summary>
    	// spec:
    	//     result = (parents()->union(parents()->collect(allParents())->asSet()))
    	readonly set<Classifier> AllParents();
    	/// <summary>
    	/// The query conformsTo() gives true for a Classifier that defines a type that conforms to another. This is used, for example, in the specification of signature conformance for operations.
    	/// </summary>
    	// spec:
    	//     result = (if other.oclIsKindOf(Classifier) then
    	//       let otherClassifier : Classifier = other.oclAsType(Classifier) in
    	//         self = otherClassifier or allParents()->includes(otherClassifier)
    	//     else
    	//       false
    	//     endif)
    	readonly bool ConformsTo(Type other);
    	/// <summary>
    	/// The query hasVisibilityOf() determines whether a NamedElement is visible in the classifier. Non-private members are visible. It is only called when the argument is something owned by a parent.
    	/// </summary>
    	// pre:
    	//     allParents()->including(self)->collect(member)->includes(n)
    	// spec:
    	//     result = (n.visibility <> VisibilityKind::private)
    	readonly bool HasVisibilityOf(NamedElement n);
    	/// <summary>
    	/// The query inherit() defines how to inherit a set of elements passed as its argument.  It excludes redefined elements from the result.
    	/// </summary>
    	// spec:
    	//     result = (inhs->reject(inh |
    	//       inh.oclIsKindOf(RedefinableElement) and
    	//       ownedMember->select(oclIsKindOf(RedefinableElement))->
    	//         select(redefinedElement->includes(inh.oclAsType(RedefinableElement)))
    	//            ->notEmpty()))
    	readonly set<NamedElement> Inherit(set<NamedElement> inhs);
    	/// <summary>
    	/// The query inheritableMembers() gives all of the members of a Classifier that may be inherited in one of its descendants, subject to whatever visibility restrictions apply.
    	/// </summary>
    	// pre:
    	//     c.allParents()->includes(self)
    	// spec:
    	//     result = (member->select(m | c.hasVisibilityOf(m)))
    	readonly set<NamedElement> InheritableMembers(Classifier c);
    	/// <summary>
    	/// The query isTemplate() returns whether this Classifier is actually a template.
    	/// </summary>
    	// spec:
    	//     result = (ownedTemplateSignature <> null or general->exists(g | g.isTemplate()))
    	readonly bool IsTemplate();
    	/// <summary>
    	/// The query maySpecializeType() determines whether this classifier may have a generalization relationship to classifiers of the specified type. By default a classifier may specialize classifiers of the same or a more general type. It is intended to be redefined by classifiers that have different specialization constraints.
    	/// </summary>
    	// spec:
    	//     result = (self.oclIsKindOf(c.oclType()))
    	readonly bool MaySpecializeType(Classifier c);
    	/// <summary>
    	/// The query parents() gives all of the immediate ancestors of a generalized Classifier.
    	/// </summary>
    	// spec:
    	//     result = (generalization.general->asSet())
    	readonly set<Classifier> Parents();
    	/// <summary>
    	/// The Interfaces directly realized by this Classifier
    	/// </summary>
    	// spec:
    	//     result = ((clientDependency->
    	//       select(oclIsKindOf(Realization) and supplier->forAll(oclIsKindOf(Interface))))->
    	//           collect(supplier.oclAsType(Interface))->asSet())
    	readonly set<Interface> DirectlyRealizedInterfaces();
    	/// <summary>
    	/// The Interfaces directly used by this Classifier
    	/// </summary>
    	// spec:
    	//     result = ((supplierDependency->
    	//       select(oclIsKindOf(Usage) and client->forAll(oclIsKindOf(Interface))))->
    	//         collect(client.oclAsType(Interface))->asSet())
    	readonly set<Interface> DirectlyUsedInterfaces();
    	/// <summary>
    	/// The Interfaces realized by this Classifier and all of its generalizations
    	/// </summary>
    	// spec:
    	//     result = (directlyRealizedInterfaces()->union(self.allParents()->collect(directlyRealizedInterfaces()))->asSet())
    	readonly set<Interface> AllRealizedInterfaces();
    	/// <summary>
    	/// The Interfaces used by this Classifier and all of its generalizations
    	/// </summary>
    	// spec:
    	//     result = (directlyUsedInterfaces()->union(self.allParents()->collect(directlyUsedInterfaces()))->asSet())
    	readonly set<Interface> AllUsedInterfaces();
    	// spec:
    	//     result = (substitution.contract->includes(contract))
    	readonly bool IsSubstitutableFor(Classifier contract);
    	/// <summary>
    	/// The query allAttributes gives an ordered set of all owned and inherited attributes of the Classifier. All owned attributes appear before any inherited attributes, and the attributes inherited from any more specific parent Classifier appear before those of any more general parent Classifier. However, if the Classifier has multiple immediate parents, then the relative ordering of the sets of attributes from those parents is not defined.
    	/// </summary>
    	// spec:
    	//     result = (attribute->asSequence()->union(parents()->asSequence().allAttributes())->select(p | member->includes(p))->asOrderedSet())
    	readonly list<Property> AllAttributes();
    	/// <summary>
    	/// All StructuralFeatures related to the Classifier that may have Slots, including direct attributes, inherited attributes, private attributes in generalizations, and memberEnds of Associations, but excluding redefined StructuralFeatures.
    	/// </summary>
    	// spec:
    	//     result = (member->select(oclIsKindOf(StructuralFeature))->
    	//       collect(oclAsType(StructuralFeature))->
    	//        union(self.inherit(self.allParents()->collect(p | p.attribute)->asSet())->
    	//          collect(oclAsType(StructuralFeature)))->asSet())
    	readonly set<StructuralFeature> AllSlottableFeatures();
    }

    /// <summary>
    /// A Namespace is an Element in a model that owns and/or imports a set of NamedElements that can be identified by name.
    /// </summary>
    abstract class Namespace : NamedElement
    {
    	/// <summary>
    	/// References the ElementImports owned by the Namespace.
    	/// </summary>
    	containment set<ElementImport> ElementImport subsets Element.OwnedElement;
    	/// <summary>
    	/// References the PackageableElements that are members of this Namespace as a result of either PackageImports or ElementImports.
    	/// </summary>
    	// spec:
    	//     result = (self.importMembers(elementImport.importedElement->asSet()->union(packageImport.importedPackage->collect(p | p.visibleMembers()))->asSet()))
    	derived set<PackageableElement> ImportedMember subsets Namespace.Member;
    	/// <summary>
    	/// A collection of NamedElements identifiable within the Namespace, either by being owned or by being introduced by importing or inheritance.
    	/// </summary>
    	union set<NamedElement> Member;
    	/// <summary>
    	/// A collection of NamedElements owned by the Namespace.
    	/// </summary>
    	containment union set<NamedElement> OwnedMember subsets Element.OwnedElement, Namespace.Member;
    	/// <summary>
    	/// Specifies a set of Constraints owned by this Namespace.
    	/// </summary>
    	containment set<Constraint> OwnedRule subsets Namespace.OwnedMember;
    	/// <summary>
    	/// References the PackageImports owned by the Namespace.
    	/// </summary>
    	containment set<PackageImport> PackageImport subsets Element.OwnedElement;
    	/// <summary>
    	/// The query excludeCollisions() excludes from a set of PackageableElements any that would not be distinguishable from each other in this Namespace.
    	/// </summary>
    	// spec:
    	//     result = (imps->reject(imp1  | imps->exists(imp2 | not imp1.isDistinguishableFrom(imp2, self))))
    	readonly set<PackageableElement> ExcludeCollisions(set<PackageableElement> imps);
    	/// <summary>
    	/// The query getNamesOfMember() gives a set of all of the names that a member would have in a Namespace, taking importing into account. In general a member can have multiple names in a Namespace if it is imported more than once with different aliases.
    	/// </summary>
    	// spec:
    	//     result = (if self.ownedMember ->includes(element)
    	//     then Set{element.name}
    	//     else let elementImports : Set(ElementImport) = self.elementImport->select(ei | ei.importedElement = element) in
    	//       if elementImports->notEmpty()
    	//       then
    	//          elementImports->collect(el | el.getName())->asSet()
    	//       else 
    	//          self.packageImport->select(pi | pi.importedPackage.visibleMembers().oclAsType(NamedElement)->includes(element))-> collect(pi | pi.importedPackage.getNamesOfMember(element))->asSet()
    	//       endif
    	//     endif)
    	readonly set<string> GetNamesOfMember(NamedElement element);
    	/// <summary>
    	/// The query importMembers() defines which of a set of PackageableElements are actually imported into the Namespace. This excludes hidden ones, i.e., those which have names that conflict with names of ownedMembers, and it also excludes PackageableElements that would have the indistinguishable names when imported.
    	/// </summary>
    	// spec:
    	//     result = (self.excludeCollisions(imps)->select(imp | self.ownedMember->forAll(mem | imp.isDistinguishableFrom(mem, self))))
    	readonly set<PackageableElement> ImportMembers(set<PackageableElement> imps);
    	/// <summary>
    	/// The Boolean query membersAreDistinguishable() determines whether all of the Namespace&apos;s members are distinguishable within it.
    	/// </summary>
    	// spec:
    	//     result = (member->forAll( memb |
    	//        member->excluding(memb)->forAll(other |
    	//            memb.isDistinguishableFrom(other, self))))
    	readonly bool MembersAreDistinguishable();
    }

    /// <summary>
    /// A NamedElement is an Element in a model that may have a name. The name may be given directly and/or via the use of a StringExpression.
    /// </summary>
    abstract class NamedElement : Element
    {
    	/// <summary>
    	/// Indicates the Dependencies that reference this NamedElement as a client.
    	/// </summary>
    	// spec:
    	//     result = (Dependency.allInstances()->select(d | d.client->includes(self)))
    	derived set<Dependency> ClientDependency;
    	/// <summary>
    	/// The name of the NamedElement.
    	/// </summary>
    	string Name;
    	/// <summary>
    	/// The StringExpression used to define the name of this NamedElement.
    	/// </summary>
    	containment StringExpression NameExpression subsets Element.OwnedElement;
    	/// <summary>
    	/// Specifies the Namespace that owns the NamedElement.
    	/// </summary>
    	Namespace Namespace subsets Element.Owner;
    	/// <summary>
    	/// A name that allows the NamedElement to be identified within a hierarchy of nested Namespaces. It is constructed from the names of the containing Namespaces starting at the root of the hierarchy and ending with the name of the NamedElement itself.
    	/// </summary>
    	// spec:
    	//     result = (if self.name <> null and self.allNamespaces()->select( ns | ns.name=null )->isEmpty()
    	//     then 
    	//         self.allNamespaces()->iterate( ns : Namespace; agg: String = self.name | ns.name.concat(self.separator()).concat(agg))
    	//     else
    	//        null
    	//     endif)
    	derived string QualifiedName;
    	/// <summary>
    	/// Determines whether and how the NamedElement is visible outside its owning Namespace.
    	/// </summary>
    	VisibilityKind Visibility;
    	/// <summary>
    	/// The query allNamespaces() gives the sequence of Namespaces in which the NamedElement is nested, working outwards.
    	/// </summary>
    	// spec:
    	//     result = (if owner.oclIsKindOf(TemplateParameter) and
    	//       owner.oclAsType(TemplateParameter).signature.template.oclIsKindOf(Namespace) then
    	//         let enclosingNamespace : Namespace =
    	//           owner.oclAsType(TemplateParameter).signature.template.oclAsType(Namespace) in
    	//             enclosingNamespace.allNamespaces()->prepend(enclosingNamespace)
    	//     else
    	//       if namespace->isEmpty()
    	//         then OrderedSet{}
    	//       else
    	//         namespace.allNamespaces()->prepend(namespace)
    	//       endif
    	//     endif)
    	readonly list<Namespace> AllNamespaces();
    	/// <summary>
    	/// The query allOwningPackages() returns the set of all the enclosing Namespaces of this NamedElement, working outwards, that are Packages, up to but not including the first such Namespace that is not a Package.
    	/// </summary>
    	// spec:
    	//     result = (if namespace.oclIsKindOf(Package)
    	//     then
    	//       let owningPackage : Package = namespace.oclAsType(Package) in
    	//         owningPackage->union(owningPackage.allOwningPackages())
    	//     else
    	//       null
    	//     endif)
    	readonly set<Package> AllOwningPackages();
    	/// <summary>
    	/// The query isDistinguishableFrom() determines whether two NamedElements may logically co-exist within a Namespace. By default, two named elements are distinguishable if (a) they have types neither of which is a kind of the other or (b) they have different names.
    	/// </summary>
    	// spec:
    	//     result = ((self.oclIsKindOf(n.oclType()) or n.oclIsKindOf(self.oclType())) implies
    	//         ns.getNamesOfMember(self)->intersection(ns.getNamesOfMember(n))->isEmpty()
    	//     )
    	readonly bool IsDistinguishableFrom(NamedElement n, Namespace ns);
    	/// <summary>
    	/// The query separator() gives the string that is used to separate names when constructing a qualifiedName.
    	/// </summary>
    	// spec:
    	//     result = ('::')
    	readonly string Separator();
    }

    /// <summary>
    /// A TemplateableElement is an Element that can optionally be defined as a template and bound to other templates.
    /// </summary>
    abstract class TemplateableElement : Element
    {
    	/// <summary>
    	/// The optional TemplateSignature specifying the formal TemplateParameters for this TemplateableElement. If a TemplateableElement has a TemplateSignature, then it is a template.
    	/// </summary>
    	containment TemplateSignature OwnedTemplateSignature subsets Element.OwnedElement;
    	/// <summary>
    	/// The optional TemplateBindings from this TemplateableElement to one or more templates.
    	/// </summary>
    	containment set<TemplateBinding> TemplateBinding subsets Element.OwnedElement;
    	/// <summary>
    	/// The query isTemplate() returns whether this TemplateableElement is actually a template.
    	/// </summary>
    	// spec:
    	//     result = (ownedTemplateSignature <> null)
    	readonly bool IsTemplate();
    	/// <summary>
    	/// The query parameterableElements() returns the set of ParameterableElements that may be used as the parameteredElements for a TemplateParameter of this TemplateableElement. By default, this set includes all the ownedElements. Subclasses may override this operation if they choose to restrict the set of ParameterableElements.
    	/// </summary>
    	// spec:
    	//     result = (self.allOwnedElements()->select(oclIsKindOf(ParameterableElement)).oclAsType(ParameterableElement)->asSet())
    	readonly set<ParameterableElement> ParameterableElements();
    }

    /// <summary>
    /// A RedefinableElement is an element that, when defined in the context of a Classifier, can be redefined more specifically or differently in the context of another Classifier that specializes (directly or indirectly) the context Classifier.
    /// </summary>
    abstract class RedefinableElement : NamedElement
    {
    	/// <summary>
    	/// Indicates whether it is possible to further redefine a RedefinableElement. If the value is true, then it is not possible to further redefine the RedefinableElement.
    	/// </summary>
    	bool IsLeaf;
    	/// <summary>
    	/// The RedefinableElement that is being redefined by this element.
    	/// </summary>
    	union set<RedefinableElement> RedefinedElement;
    	/// <summary>
    	/// The contexts that this element may be redefined from.
    	/// </summary>
    	union set<Classifier> RedefinitionContext;
    	/// <summary>
    	/// The query isConsistentWith() specifies, for any two RedefinableElements in a context in which redefinition is possible, whether redefinition would be logically consistent. By default, this is false; this operation must be overridden for subclasses of RedefinableElement to define the consistency conditions.
    	/// </summary>
    	// spec:
    	//     result = (false)
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    	/// <summary>
    	/// The query isRedefinitionContextValid() specifies whether the redefinition contexts of this RedefinableElement are properly related to the redefinition contexts of the specified RedefinableElement to allow this element to redefine the other. By default at least one of the redefinition contexts of this element must be a specialization of at least one of the redefinition contexts of the specified element.
    	/// </summary>
    	// spec:
    	//     result = (redefinitionContext->exists(c | c.allParents()->includesAll(redefinedElement.redefinitionContext)))
    	readonly bool IsRedefinitionContextValid(RedefinableElement redefinedElement);
    }

    /// <summary>
    /// A Class classifies a set of objects and specifies the features that characterize the structure and behavior of those objects.  A Class may have an internal structure and Ports.
    /// </summary>
    class Class : BehavioredClassifier, EncapsulatedClassifier
    {
    	/// <summary>
    	/// This property is used when the Class is acting as a metaclass. It references the Extensions that specify additional properties of the metaclass. The property is derived from the Extensions whose memberEnds are typed by the Class.
    	/// </summary>
    	// spec:
    	//     result = (Extension.allInstances()->select(ext | 
    	//       let endTypes : Sequence(Classifier) = ext.memberEnd->collect(type.oclAsType(Classifier)) in
    	//       endTypes->includes(self) or endTypes.allParents()->includes(self) ))
    	derived set<Extension> Extension;
    	/// <summary>
    	/// If true, the Class does not provide a complete declaration and cannot be instantiated. An abstract Class is typically used as a target of Associations or Generalizations.
    	/// </summary>
    	bool IsAbstract redefines Classifier.IsAbstract;
    	/// <summary>
    	/// Determines whether an object specified by this Class is active or not. If true, then the owning Class is referred to as an active Class. If false, then such a Class is referred to as a passive Class.
    	/// </summary>
    	bool IsActive;
    	/// <summary>
    	/// The Classifiers owned by the Class that are not ownedBehaviors.
    	/// </summary>
    	containment list<Classifier> NestedClassifier subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The attributes (i.e., the Properties) owned by the Class.
    	/// </summary>
    	containment list<Property> OwnedAttribute redefines StructuredClassifier.OwnedAttribute subsets Classifier.Attribute, Namespace.OwnedMember;
    	/// <summary>
    	/// The Operations owned by the Class.
    	/// </summary>
    	containment list<Operation> OwnedOperation subsets Classifier.Feature, Namespace.OwnedMember;
    	/// <summary>
    	/// The Receptions owned by the Class.
    	/// </summary>
    	containment set<Reception> OwnedReception subsets Classifier.Feature, Namespace.OwnedMember;
    	/// <summary>
    	/// The superclasses of a Class, derived from its Generalizations.
    	/// </summary>
    	// spec:
    	//     result = (self.general()->select(oclIsKindOf(Class))->collect(oclAsType(Class))->asSet())
    	derived set<Class> SuperClass redefines Classifier.General;
    }

    /// <summary>
    /// A BehavioredClassifier may have InterfaceRealizations, and owns a set of Behaviors one of which may specify the behavior of the BehavioredClassifier itself.
    /// </summary>
    abstract class BehavioredClassifier : Classifier
    {
    	/// <summary>
    	/// A Behavior that specifies the behavior of the BehavioredClassifier itself.
    	/// </summary>
    	Behavior ClassifierBehavior subsets BehavioredClassifier.OwnedBehavior;
    	/// <summary>
    	/// The set of InterfaceRealizations owned by the BehavioredClassifier. Interface realizations reference the Interfaces of which the BehavioredClassifier is an implementation.
    	/// </summary>
    	containment set<InterfaceRealization> InterfaceRealization subsets Element.OwnedElement, NamedElement.ClientDependency;
    	/// <summary>
    	/// Behaviors owned by a BehavioredClassifier.
    	/// </summary>
    	containment set<Behavior> OwnedBehavior subsets Namespace.OwnedMember;
    }

    /// <summary>
    /// An EncapsulatedClassifier may own Ports to specify typed interaction points.
    /// </summary>
    abstract class EncapsulatedClassifier : StructuredClassifier
    {
    	/// <summary>
    	/// The Ports owned by the EncapsulatedClassifier.
    	/// </summary>
    	// spec:
    	//     result = (ownedAttribute->select(oclIsKindOf(Port))->collect(oclAsType(Port))->asOrderedSet())
    	containment derived set<Port> OwnedPort subsets StructuredClassifier.OwnedAttribute;
    }

    /// <summary>
    /// StructuredClassifiers may contain an internal structure of connected elements each of which plays a role in the overall Behavior modeled by the StructuredClassifier.
    /// </summary>
    abstract class StructuredClassifier : Classifier
    {
    	/// <summary>
    	/// The Properties owned by the StructuredClassifier.
    	/// </summary>
    	containment list<Property> OwnedAttribute subsets Classifier.Attribute, Namespace.OwnedMember, StructuredClassifier.Role;
    	/// <summary>
    	/// The connectors owned by the StructuredClassifier.
    	/// </summary>
    	containment set<Connector> OwnedConnector subsets Classifier.Feature, Namespace.OwnedMember;
    	/// <summary>
    	/// The Properties specifying instances that the StructuredClassifier owns by composition. This collection is derived, selecting those owned Properties where isComposite is true.
    	/// </summary>
    	// spec:
    	//     result = (ownedAttribute->select(isComposite))
    	derived set<Property> Part;
    	/// <summary>
    	/// The roles that instances may play in this StructuredClassifier.
    	/// </summary>
    	union set<ConnectableElement> Role subsets Namespace.Member;
    	/// <summary>
    	/// All features of type ConnectableElement, equivalent to all direct and inherited roles.
    	/// </summary>
    	// spec:
    	//     result = (allFeatures()->select(oclIsKindOf(ConnectableElement))->collect(oclAsType(ConnectableElement))->asSet())
    	readonly set<ConnectableElement> AllRoles();
    }

    /// <summary>
    /// A Comment is a textual annotation that can be attached to a set of Elements.
    /// </summary>
    class Comment : Element
    {
    	/// <summary>
    	/// References the Element(s) being commented.
    	/// </summary>
    	set<Element> AnnotatedElement;
    	/// <summary>
    	/// Specifies a string that is the comment.
    	/// </summary>
    	string Body;
    }

    /// <summary>
    /// A DataType is a type whose instances are identified only by their value.
    /// </summary>
    class DataType : Classifier
    {
    	/// <summary>
    	/// The attributes owned by the DataType.
    	/// </summary>
    	containment list<Property> OwnedAttribute subsets Classifier.Attribute, Namespace.OwnedMember;
    	/// <summary>
    	/// The Operations owned by the DataType.
    	/// </summary>
    	containment list<Operation> OwnedOperation subsets Classifier.Feature, Namespace.OwnedMember;
    }

    /// <summary>
    /// An Enumeration is a DataType whose values are enumerated in the model as EnumerationLiterals.
    /// </summary>
    class Enumeration : DataType
    {
    	/// <summary>
    	/// The ordered set of literals owned by this Enumeration.
    	/// </summary>
    	containment list<EnumerationLiteral> OwnedLiteral subsets Namespace.OwnedMember;
    }

    /// <summary>
    /// An EnumerationLiteral is a user-defined data value for an Enumeration.
    /// </summary>
    class EnumerationLiteral : InstanceSpecification
    {
    	/// <summary>
    	/// The classifier of this EnumerationLiteral derived to be equal to its Enumeration.
    	/// </summary>
    	// spec:
    	//     result = (enumeration)
    	derived Enumeration Classifier redefines InstanceSpecification.Classifier;
    	/// <summary>
    	/// The Enumeration that this EnumerationLiteral is a member of.
    	/// </summary>
    	Enumeration Enumeration subsets NamedElement.Namespace;
    }

    /// <summary>
    /// An InstanceSpecification is a model element that represents an instance in a modeled system. An InstanceSpecification can act as a DeploymentTarget in a Deployment relationship, in the case that it represents an instance of a Node. It can also act as a DeployedArtifact, if it represents an instance of an Artifact.
    /// </summary>
    class InstanceSpecification : DeploymentTarget, PackageableElement, DeployedArtifact
    {
    	/// <summary>
    	/// The Classifier or Classifiers of the represented instance. If multiple Classifiers are specified, the instance is classified by all of them.
    	/// </summary>
    	set<Classifier> Classifier;
    	/// <summary>
    	/// A Slot giving the value or values of a StructuralFeature of the instance. An InstanceSpecification can have one Slot per StructuralFeature of its Classifiers, including inherited features. It is not necessary to model a Slot for every StructuralFeature, in which case the InstanceSpecification is a partial description.
    	/// </summary>
    	containment set<Slot> Slot subsets Element.OwnedElement;
    	/// <summary>
    	/// A specification of how to compute, derive, or construct the instance.
    	/// </summary>
    	containment ValueSpecification Specification subsets Element.OwnedElement;
    }

    /// <summary>
    /// A deployment target is the location for a deployed artifact.
    /// </summary>
    abstract class DeploymentTarget : NamedElement
    {
    	/// <summary>
    	/// The set of elements that are manifested in an Artifact that is involved in Deployment to a DeploymentTarget.
    	/// </summary>
    	// spec:
    	//     result = (deployment.deployedArtifact->select(oclIsKindOf(Artifact))->collect(oclAsType(Artifact).manifestation)->collect(utilizedElement)->asSet())
    	derived set<PackageableElement> DeployedElement;
    	/// <summary>
    	/// The set of Deployments for a DeploymentTarget.
    	/// </summary>
    	containment set<Deployment> Deployment subsets Element.OwnedElement, NamedElement.ClientDependency;
    }

    /// <summary>
    /// A PackageableElement is a NamedElement that may be owned directly by a Package. A PackageableElement is also able to serve as the parameteredElement of a TemplateParameter.
    /// </summary>
    abstract class PackageableElement : ParameterableElement, NamedElement
    {
    	/// <summary>
    	/// A PackageableElement must have a visibility specified if it is owned by a Namespace. The default visibility is public.
    	/// </summary>
    	VisibilityKind Visibility redefines NamedElement.Visibility;
    }

    /// <summary>
    /// A ParameterableElement is an Element that can be exposed as a formal TemplateParameter for a template, or specified as an actual parameter in a binding of a template.
    /// </summary>
    abstract class ParameterableElement : Element
    {
    	/// <summary>
    	/// The formal TemplateParameter that owns this ParameterableElement.
    	/// </summary>
    	TemplateParameter OwningTemplateParameter subsets Element.Owner, ParameterableElement.TemplateParameter;
    	/// <summary>
    	/// The TemplateParameter that exposes this ParameterableElement as a formal parameter.
    	/// </summary>
    	TemplateParameter TemplateParameter;
    	/// <summary>
    	/// The query isCompatibleWith() determines if this ParameterableElement is compatible with the specified ParameterableElement. By default, this ParameterableElement is compatible with another ParameterableElement p if the kind of this ParameterableElement is the same as or a subtype of the kind of p. Subclasses of ParameterableElement should override this operation to specify different compatibility constraints.
    	/// </summary>
    	// spec:
    	//     result = (self.oclIsKindOf(p.oclType()))
    	readonly bool IsCompatibleWith(ParameterableElement p);
    	/// <summary>
    	/// The query isTemplateParameter() determines if this ParameterableElement is exposed as a formal TemplateParameter.
    	/// </summary>
    	// spec:
    	//     result = (templateParameter->notEmpty())
    	readonly bool IsTemplateParameter();
    }

    /// <summary>
    /// A deployed artifact is an artifact or artifact instance that has been deployed to a deployment target.
    /// </summary>
    abstract class DeployedArtifact : NamedElement
    {
    }

    /// <summary>
    /// A Generalization is a taxonomic relationship between a more general Classifier and a more specific Classifier. Each instance of the specific Classifier is also an instance of the general Classifier. The specific Classifier inherits the features of the more general Classifier. A Generalization is owned by the specific Classifier.
    /// </summary>
    class Generalization : DirectedRelationship
    {
    	/// <summary>
    	/// The general classifier in the Generalization relationship.
    	/// </summary>
    	Classifier General subsets DirectedRelationship.Target;
    	/// <summary>
    	/// Represents a set of instances of Generalization.  A Generalization may appear in many GeneralizationSets.
    	/// </summary>
    	set<GeneralizationSet> GeneralizationSet;
    	/// <summary>
    	/// Indicates whether the specific Classifier can be used wherever the general Classifier can be used. If true, the execution traces of the specific Classifier shall be a superset of the execution traces of the general Classifier. If false, there is no such constraint on execution traces. If unset, the modeler has not stated whether there is such a constraint or not.
    	/// </summary>
    	bool IsSubstitutable;
    	/// <summary>
    	/// The specializing Classifier in the Generalization relationship.
    	/// </summary>
    	Classifier Specific subsets DirectedRelationship.Source, Element.Owner;
    }

    /// <summary>
    /// A DirectedRelationship represents a relationship between a collection of source model Elements and a collection of target model Elements.
    /// </summary>
    abstract class DirectedRelationship : Relationship
    {
    	/// <summary>
    	/// Specifies the source Element(s) of the DirectedRelationship.
    	/// </summary>
    	union set<Element> Source subsets Relationship.RelatedElement;
    	/// <summary>
    	/// Specifies the target Element(s) of the DirectedRelationship.
    	/// </summary>
    	union set<Element> Target subsets Relationship.RelatedElement;
    }

    /// <summary>
    /// An InstanceValue is a ValueSpecification that identifies an instance.
    /// </summary>
    class InstanceValue : ValueSpecification
    {
    	/// <summary>
    	/// The InstanceSpecification that represents the specified value.
    	/// </summary>
    	InstanceSpecification Instance;
    }

    /// <summary>
    /// A ValueSpecification is the specification of a (possibly empty) set of values. A ValueSpecification is a ParameterableElement that may be exposed as a formal TemplateParameter and provided as the actual parameter in the binding of a template.
    /// </summary>
    abstract class ValueSpecification : TypedElement, PackageableElement
    {
    	/// <summary>
    	/// The query booleanValue() gives a single Boolean value when one can be computed.
    	/// </summary>
    	// spec:
    	//     result = (null)
    	readonly bool BooleanValue();
    	/// <summary>
    	/// The query integerValue() gives a single Integer value when one can be computed.
    	/// </summary>
    	// spec:
    	//     result = (null)
    	readonly int IntegerValue();
    	/// <summary>
    	/// The query isCompatibleWith() determines if this ValueSpecification is compatible with the specified ParameterableElement. This ValueSpecification is compatible with ParameterableElement p if the kind of this ValueSpecification is the same as or a subtype of the kind of p. Further, if p is a TypedElement, then the type of this ValueSpecification must be conformant with the type of p.
    	/// </summary>
    	// spec:
    	//     result = (self.oclIsKindOf(p.oclType()) and (p.oclIsKindOf(TypedElement) implies 
    	//     self.type.conformsTo(p.oclAsType(TypedElement).type)))
    	readonly bool IsCompatibleWith(ParameterableElement p);
    	/// <summary>
    	/// The query isComputable() determines whether a value specification can be computed in a model. This operation cannot be fully defined in OCL. A conforming implementation is expected to deliver true for this operation for all ValueSpecifications that it can compute, and to compute all of those for which the operation is true. A conforming implementation is expected to be able to compute at least the value of all LiteralSpecifications.
    	/// </summary>
    	// spec:
    	//     result = (false)
    	readonly bool IsComputable();
    	/// <summary>
    	/// The query isNull() returns true when it can be computed that the value is null.
    	/// </summary>
    	// spec:
    	//     result = (false)
    	readonly bool IsNull();
    	/// <summary>
    	/// The query realValue() gives a single Real value when one can be computed.
    	/// </summary>
    	// spec:
    	//     result = (null)
    	readonly double RealValue();
    	/// <summary>
    	/// The query stringValue() gives a single String value when one can be computed.
    	/// </summary>
    	// spec:
    	//     result = (null)
    	readonly string StringValue();
    	/// <summary>
    	/// The query unlimitedValue() gives a single UnlimitedNatural value when one can be computed.
    	/// </summary>
    	// spec:
    	//     result = (null)
    	readonly long UnlimitedValue();
    }

    /// <summary>
    /// A TypedElement is a NamedElement that may have a Type specified for it.
    /// </summary>
    abstract class TypedElement : NamedElement
    {
    	/// <summary>
    	/// The type of the TypedElement.
    	/// </summary>
    	Type Type;
    }

    /// <summary>
    /// A LiteralBoolean is a specification of a Boolean value.
    /// </summary>
    class LiteralBoolean : LiteralSpecification
    {
    	/// <summary>
    	/// The specified Boolean value.
    	/// </summary>
    	bool Value;
    	/// <summary>
    	/// The query booleanValue() gives the value.
    	/// </summary>
    	// spec:
    	//     result = (value)
    	readonly bool BooleanValue();
    	/// <summary>
    	/// The query isComputable() is redefined to be true.
    	/// </summary>
    	// spec:
    	//     result = (true)
    	readonly bool IsComputable();
    }

    /// <summary>
    /// A LiteralSpecification identifies a literal constant being modeled.
    /// </summary>
    abstract class LiteralSpecification : ValueSpecification
    {
    }

    /// <summary>
    /// A LiteralInteger is a specification of an Integer value.
    /// </summary>
    class LiteralInteger : LiteralSpecification
    {
    	/// <summary>
    	/// The specified Integer value.
    	/// </summary>
    	int Value;
    	/// <summary>
    	/// The query integerValue() gives the value.
    	/// </summary>
    	// spec:
    	//     result = (value)
    	readonly int IntegerValue();
    	/// <summary>
    	/// The query isComputable() is redefined to be true.
    	/// </summary>
    	// spec:
    	//     result = (true)
    	readonly bool IsComputable();
    }

    /// <summary>
    /// A LiteralNull specifies the lack of a value.
    /// </summary>
    class LiteralNull : LiteralSpecification
    {
    	/// <summary>
    	/// The query isComputable() is redefined to be true.
    	/// </summary>
    	// spec:
    	//     result = (true)
    	readonly bool IsComputable();
    	/// <summary>
    	/// The query isNull() returns true.
    	/// </summary>
    	// spec:
    	//     result = (true)
    	readonly bool IsNull();
    }

    /// <summary>
    /// A LiteralReal is a specification of a Real value.
    /// </summary>
    class LiteralReal : LiteralSpecification
    {
    	/// <summary>
    	/// The specified Real value.
    	/// </summary>
    	double Value;
    	/// <summary>
    	/// The query isComputable() is redefined to be true.
    	/// </summary>
    	// spec:
    	//     result = (true)
    	readonly bool IsComputable();
    	/// <summary>
    	/// The query realValue() gives the value.
    	/// </summary>
    	// spec:
    	//     result = (value)
    	readonly double RealValue();
    }

    /// <summary>
    /// A LiteralString is a specification of a String value.
    /// </summary>
    class LiteralString : LiteralSpecification
    {
    	/// <summary>
    	/// The specified String value.
    	/// </summary>
    	string Value;
    	/// <summary>
    	/// The query isComputable() is redefined to be true.
    	/// </summary>
    	// spec:
    	//     result = (true)
    	readonly bool IsComputable();
    	/// <summary>
    	/// The query stringValue() gives the value.
    	/// </summary>
    	// spec:
    	//     result = (value)
    	readonly string StringValue();
    }

    /// <summary>
    /// A LiteralUnlimitedNatural is a specification of an UnlimitedNatural number.
    /// </summary>
    class LiteralUnlimitedNatural : LiteralSpecification
    {
    	/// <summary>
    	/// The specified UnlimitedNatural value.
    	/// </summary>
    	long Value;
    	/// <summary>
    	/// The query isComputable() is redefined to be true.
    	/// </summary>
    	// spec:
    	//     result = (true)
    	readonly bool IsComputable();
    	/// <summary>
    	/// The query unlimitedValue() gives the value.
    	/// </summary>
    	// spec:
    	//     result = (value)
    	readonly long UnlimitedValue();
    }

    /// <summary>
    /// An Operation is a BehavioralFeature of a Classifier that specifies the name, type, parameters, and constraints for invoking an associated Behavior. An Operation may invoke both the execution of method behaviors as well as other behavioral responses. Operation specializes TemplateableElement in order to support specification of template operations and bound operations. Operation specializes ParameterableElement to specify that an operation can be exposed as a formal template parameter, and provided as an actual parameter in a binding of a template.
    /// </summary>
    class Operation : TemplateableElement, ParameterableElement, BehavioralFeature
    {
    	/// <summary>
    	/// An optional Constraint on the result values of an invocation of this Operation.
    	/// </summary>
    	containment Constraint BodyCondition subsets Namespace.OwnedRule;
    	/// <summary>
    	/// The Class that owns this operation, if any.
    	/// </summary>
    	Class Class subsets Feature.FeaturingClassifier, NamedElement.Namespace, RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// The DataType that owns this Operation, if any.
    	/// </summary>
    	DataType Datatype subsets Feature.FeaturingClassifier, NamedElement.Namespace, RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// The Interface that owns this Operation, if any.
    	/// </summary>
    	Interface Interface subsets Feature.FeaturingClassifier, NamedElement.Namespace, RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// Specifies whether the return parameter is ordered or not, if present.  This information is derived from the return result for this Operation.
    	/// </summary>
    	// spec:
    	//     result = (if returnResult()->notEmpty() then returnResult()-> exists(isOrdered) else false endif)
    	derived bool IsOrdered;
    	/// <summary>
    	/// Specifies whether an execution of the BehavioralFeature leaves the state of the system unchanged (isQuery=true) or whether side effects may occur (isQuery=false).
    	/// </summary>
    	bool IsQuery;
    	/// <summary>
    	/// Specifies whether the return parameter is unique or not, if present. This information is derived from the return result for this Operation.
    	/// </summary>
    	// spec:
    	//     result = (if returnResult()->notEmpty() then returnResult()->exists(isUnique) else true endif)
    	derived bool IsUnique;
    	/// <summary>
    	/// Specifies the lower multiplicity of the return parameter, if present. This information is derived from the return result for this Operation.
    	/// </summary>
    	// spec:
    	//     result = (if returnResult()->notEmpty() then returnResult()->any(true).lower else null endif)
    	derived int Lower;
    	/// <summary>
    	/// The parameters owned by this Operation.
    	/// </summary>
    	containment list<Parameter> OwnedParameter redefines BehavioralFeature.OwnedParameter;
    	/// <summary>
    	/// An optional set of Constraints specifying the state of the system when the Operation is completed.
    	/// </summary>
    	containment set<Constraint> Postcondition subsets Namespace.OwnedRule;
    	/// <summary>
    	/// An optional set of Constraints on the state of the system when the Operation is invoked.
    	/// </summary>
    	containment set<Constraint> Precondition subsets Namespace.OwnedRule;
    	/// <summary>
    	/// The Types representing exceptions that may be raised during an invocation of this operation.
    	/// </summary>
    	set<Class> RaisedException redefines BehavioralFeature.RaisedException;
    	/// <summary>
    	/// The Operations that are redefined by this Operation.
    	/// </summary>
    	set<Operation> RedefinedOperation subsets RedefinableElement.RedefinedElement;
    	/// <summary>
    	/// The OperationTemplateParameter that exposes this element as a formal parameter.
    	/// </summary>
    	OperationTemplateParameter TemplateParameter redefines ParameterableElement.TemplateParameter;
    	/// <summary>
    	/// The return type of the operation, if present. This information is derived from the return result for this Operation.
    	/// </summary>
    	// spec:
    	//     result = (if returnResult()->notEmpty() then returnResult()->any(true).type else null endif)
    	derived Type Type;
    	/// <summary>
    	/// The upper multiplicity of the return parameter, if present. This information is derived from the return result for this Operation.
    	/// </summary>
    	// spec:
    	//     result = (if returnResult()->notEmpty() then returnResult()->any(true).upper else null endif)
    	derived long Upper;
    	/// <summary>
    	/// The query isConsistentWith() specifies, for any two Operations in a context in which redefinition is possible, whether redefinition would be consistent. A redefining operation is consistent with a redefined operation if
    	/// it has the same number of owned parameters, and for each parameter the following holds:
    	/// - Direction, ordering and uniqueness are the same.
    	/// - The corresponding types are covariant, contravariant or invariant.
    	/// - The multiplicities are compatible, depending on the parameter direction.
    	/// </summary>
    	// spec:
    	//     result = (redefiningElement.oclIsKindOf(Operation) and
    	//     let op : Operation = redefiningElement.oclAsType(Operation) in
    	//     	self.ownedParameter->size() = op.ownedParameter->size() and
    	//     	Sequence{1..self.ownedParameter->size()}->
    	//     		forAll(i |  
    	//     		  let redefiningParam : Parameter = op.ownedParameter->at(i),
    	//                    redefinedParam : Parameter = self.ownedParameter->at(i) in
    	//                      (redefiningParam.isUnique = redefinedParam.isUnique) and
    	//                      (redefiningParam.isOrdered = redefinedParam. isOrdered) and
    	//                      (redefiningParam.direction = redefinedParam.direction) and
    	//                      (redefiningParam.type.conformsTo(redefinedParam.type) or
    	//                          redefinedParam.type.conformsTo(redefiningParam.type)) and
    	//                      (redefiningParam.direction = ParameterDirectionKind::inout implies
    	//                              (redefinedParam.compatibleWith(redefiningParam) and
    	//                              redefiningParam.compatibleWith(redefinedParam))) and
    	//                      (redefiningParam.direction = ParameterDirectionKind::_'in' implies
    	//                              redefinedParam.compatibleWith(redefiningParam)) and
    	//                      ((redefiningParam.direction = ParameterDirectionKind::out or
    	//                           redefiningParam.direction = ParameterDirectionKind::return) implies
    	//                              redefiningParam.compatibleWith(redefinedParam))
    	//     		))
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    	/// <summary>
    	/// The query returnResult() returns the set containing the return parameter of the Operation if one exists, otherwise, it returns an empty set
    	/// </summary>
    	// spec:
    	//     result = (ownedParameter->select (direction = ParameterDirectionKind::return))
    	readonly set<Parameter> ReturnResult();
    }

    /// <summary>
    /// A BehavioralFeature is a feature of a Classifier that specifies an aspect of the behavior of its instances.  A BehavioralFeature is implemented (realized) by a Behavior. A BehavioralFeature specifies that a Classifier will respond to a designated request by invoking its implementing method.
    /// </summary>
    abstract class BehavioralFeature : Feature, Namespace
    {
    	/// <summary>
    	/// Specifies the semantics of concurrent calls to the same passive instance (i.e., an instance originating from a Class with isActive being false). Active instances control access to their own BehavioralFeatures.
    	/// </summary>
    	CallConcurrencyKind Concurrency;
    	/// <summary>
    	/// If true, then the BehavioralFeature does not have an implementation, and one must be supplied by a more specific Classifier. If false, the BehavioralFeature must have an implementation in the Classifier or one must be inherited.
    	/// </summary>
    	bool IsAbstract;
    	/// <summary>
    	/// A Behavior that implements the BehavioralFeature. There may be at most one Behavior for a particular pairing of a Classifier (as owner of the Behavior) and a BehavioralFeature (as specification of the Behavior).
    	/// </summary>
    	set<Behavior> Method;
    	/// <summary>
    	/// The ordered set of formal Parameters of this BehavioralFeature.
    	/// </summary>
    	containment list<Parameter> OwnedParameter subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The ParameterSets owned by this BehavioralFeature.
    	/// </summary>
    	containment set<ParameterSet> OwnedParameterSet subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The Types representing exceptions that may be raised during an invocation of this BehavioralFeature.
    	/// </summary>
    	set<Type> RaisedException;
    	/// <summary>
    	/// The query isDistinguishableFrom() determines whether two BehavioralFeatures may coexist in the same Namespace. It specifies that they must have different signatures.
    	/// </summary>
    	// spec:
    	//     result = ((n.oclIsKindOf(BehavioralFeature) and ns.getNamesOfMember(self)->intersection(ns.getNamesOfMember(n))->notEmpty()) implies
    	//       Set{self}->including(n.oclAsType(BehavioralFeature))->isUnique(ownedParameter->collect(p|
    	//       Tuple { name=p.name, type=p.type,effect=p.effect,direction=p.direction,isException=p.isException,
    	//                   isStream=p.isStream,isOrdered=p.isOrdered,isUnique=p.isUnique,lower=p.lower, upper=p.upper }))
    	//       )
    	readonly bool IsDistinguishableFrom(NamedElement n, Namespace ns);
    	/// <summary>
    	/// The ownedParameters with direction in and inout.
    	/// </summary>
    	// spec:
    	//     result = (ownedParameter->select(direction=ParameterDirectionKind::_'in' or direction=ParameterDirectionKind::inout))
    	readonly list<Parameter> InputParameters();
    	/// <summary>
    	/// The ownedParameters with direction out, inout, or return.
    	/// </summary>
    	// spec:
    	//     result = (ownedParameter->select(direction=ParameterDirectionKind::out or direction=ParameterDirectionKind::inout or direction=ParameterDirectionKind::return))
    	readonly list<Parameter> OutputParameters();
    }

    /// <summary>
    /// A Feature declares a behavioral or structural characteristic of Classifiers.
    /// </summary>
    abstract class Feature : RedefinableElement
    {
    	/// <summary>
    	/// The Classifiers that have this Feature as a feature.
    	/// </summary>
    	Classifier FeaturingClassifier;
    	/// <summary>
    	/// Specifies whether this Feature characterizes individual instances classified by the Classifier (false) or the Classifier itself (true).
    	/// </summary>
    	bool IsStatic;
    }

    /// <summary>
    /// A package can have one or more profile applications to indicate which profiles have been applied. Because a profile is a package, it is possible to apply a profile not only to packages, but also to profiles.
    /// Package specializes TemplateableElement and PackageableElement specializes ParameterableElement to specify that a package can be used as a template and a PackageableElement as a template parameter.
    /// A package is used to group elements, and provides a namespace for the grouped elements.
    /// </summary>
    class Package : PackageableElement, TemplateableElement, Namespace
    {
    	/// <summary>
    	/// Provides an identifier for the package that can be used for many purposes. A URI is the universally unique identification of the package following the IETF URI specification, RFC 2396 http://www.ietf.org/rfc/rfc2396.txt and it must comply with those syntax rules.
    	/// </summary>
    	string URI;
    	/// <summary>
    	/// References the packaged elements that are Packages.
    	/// </summary>
    	// spec:
    	//     result = (packagedElement->select(oclIsKindOf(Package))->collect(oclAsType(Package))->asSet())
    	containment derived set<Package> NestedPackage subsets Package.PackagedElement;
    	/// <summary>
    	/// References the Package that owns this Package.
    	/// </summary>
    	Package NestingPackage;
    	/// <summary>
    	/// References the Stereotypes that are owned by the Package.
    	/// </summary>
    	// spec:
    	//     result = (packagedElement->select(oclIsKindOf(Stereotype))->collect(oclAsType(Stereotype))->asSet())
    	containment derived set<Stereotype> OwnedStereotype subsets Package.PackagedElement;
    	/// <summary>
    	/// References the packaged elements that are Types.
    	/// </summary>
    	// spec:
    	//     result = (packagedElement->select(oclIsKindOf(Type))->collect(oclAsType(Type))->asSet())
    	containment derived set<Type> OwnedType subsets Package.PackagedElement;
    	/// <summary>
    	/// References the PackageMerges that are owned by this Package.
    	/// </summary>
    	containment set<PackageMerge> PackageMerge subsets Element.OwnedElement;
    	/// <summary>
    	/// Specifies the packageable elements that are owned by this Package.
    	/// </summary>
    	containment set<PackageableElement> PackagedElement subsets Namespace.OwnedMember;
    	/// <summary>
    	/// References the ProfileApplications that indicate which profiles have been applied to the Package.
    	/// </summary>
    	containment set<ProfileApplication> ProfileApplication subsets Element.OwnedElement;
    	/// <summary>
    	/// The query allApplicableStereotypes() returns all the directly or indirectly owned stereotypes, including stereotypes contained in sub-profiles.
    	/// </summary>
    	// spec:
    	//     result = (let ownedPackages : Bag(Package) = ownedMember->select(oclIsKindOf(Package))->collect(oclAsType(Package)) in
    	//      ownedStereotype->union(ownedPackages.allApplicableStereotypes())->flatten()->asSet()
    	//     )
    	readonly set<Stereotype> AllApplicableStereotypes();
    	/// <summary>
    	/// The query containingProfile() returns the closest profile directly or indirectly containing this package (or this package itself, if it is a profile).
    	/// </summary>
    	// spec:
    	//     result = (if self.oclIsKindOf(Profile) then 
    	//     	self.oclAsType(Profile)
    	//     else
    	//     	self.namespace.oclAsType(Package).containingProfile()
    	//     endif)
    	readonly Profile ContainingProfile();
    	/// <summary>
    	/// The query makesVisible() defines whether a Package makes an element visible outside itself. Elements with no visibility and elements with public visibility are made visible.
    	/// </summary>
    	// pre:
    	//     member->includes(el)
    	// spec:
    	//     result = (ownedMember->includes(el) or
    	//     (elementImport->select(ei|ei.importedElement = VisibilityKind::public)->collect(importedElement.oclAsType(NamedElement))->includes(el)) or
    	//     (packageImport->select(visibility = VisibilityKind::public)->collect(importedPackage.member->includes(el))->notEmpty()))
    	readonly bool MakesVisible(NamedElement el);
    	/// <summary>
    	/// The query mustBeOwned() indicates whether elements of this type must have an owner.
    	/// </summary>
    	// spec:
    	//     result = (false)
    	readonly bool MustBeOwned();
    	/// <summary>
    	/// The query visibleMembers() defines which members of a Package can be accessed outside it.
    	/// </summary>
    	// spec:
    	//     result = (member->select( m | m.oclIsKindOf(PackageableElement) and self.makesVisible(m))->collect(oclAsType(PackageableElement))->asSet())
    	readonly set<PackageableElement> VisibleMembers();
    }

    /// <summary>
    /// A Parameter is a specification of an argument used to pass information into or out of an invocation of a BehavioralFeature.  Parameters can be treated as ConnectableElements within Collaborations.
    /// </summary>
    class Parameter : MultiplicityElement, ConnectableElement
    {
    	/// <summary>
    	/// A String that represents a value to be used when no argument is supplied for the Parameter.
    	/// </summary>
    	// spec:
    	//     result = (if self.type = String then defaultValue.stringValue() else null endif)
    	derived string Default;
    	/// <summary>
    	/// Specifies a ValueSpecification that represents a value to be used when no argument is supplied for the Parameter.
    	/// </summary>
    	containment ValueSpecification DefaultValue subsets Element.OwnedElement;
    	/// <summary>
    	/// Indicates whether a parameter is being sent into or out of a behavioral element.
    	/// </summary>
    	ParameterDirectionKind Direction;
    	/// <summary>
    	/// Specifies the effect that executions of the owner of the Parameter have on objects passed in or out of the parameter.
    	/// </summary>
    	ParameterEffectKind Effect;
    	/// <summary>
    	/// Tells whether an output parameter may emit a value to the exclusion of the other outputs.
    	/// </summary>
    	bool IsException;
    	/// <summary>
    	/// Tells whether an input parameter may accept values while its behavior is executing, or whether an output parameter may post values while the behavior is executing.
    	/// </summary>
    	bool IsStream;
    	/// <summary>
    	/// The Operation owning this parameter.
    	/// </summary>
    	Operation Operation;
    	/// <summary>
    	/// The ParameterSets containing the parameter. See ParameterSet.
    	/// </summary>
    	set<ParameterSet> ParameterSet;
    }

    /// <summary>
    /// A multiplicity is a definition of an inclusive interval of non-negative integers beginning with a lower bound and ending with a (possibly infinite) upper bound. A MultiplicityElement embeds this information to specify the allowable cardinalities for an instantiation of the Element.
    /// </summary>
    abstract class MultiplicityElement : Element
    {
    	/// <summary>
    	/// For a multivalued multiplicity, this attribute specifies whether the values in an instantiation of this MultiplicityElement are sequentially ordered.
    	/// </summary>
    	bool IsOrdered;
    	/// <summary>
    	/// For a multivalued multiplicity, this attributes specifies whether the values in an instantiation of this MultiplicityElement are unique.
    	/// </summary>
    	bool IsUnique;
    	/// <summary>
    	/// The lower bound of the multiplicity interval.
    	/// </summary>
    	// spec:
    	//     result = (lowerBound())
    	derived int Lower;
    	/// <summary>
    	/// The specification of the lower bound for this multiplicity.
    	/// </summary>
    	containment ValueSpecification LowerValue subsets Element.OwnedElement;
    	/// <summary>
    	/// The upper bound of the multiplicity interval.
    	/// </summary>
    	// spec:
    	//     result = (upperBound())
    	derived long Upper;
    	/// <summary>
    	/// The specification of the upper bound for this multiplicity.
    	/// </summary>
    	containment ValueSpecification UpperValue subsets Element.OwnedElement;
    	/// <summary>
    	/// The operation compatibleWith takes another multiplicity as input. It returns true if the other multiplicity is wider than, or the same as, self.
    	/// </summary>
    	// spec:
    	//     result = ((other.lowerBound() <= self.lowerBound()) and ((other.upperBound() = *) or (self.upperBound() <= other.upperBound())))
    	readonly bool CompatibleWith(MultiplicityElement other);
    	/// <summary>
    	/// The query includesMultiplicity() checks whether this multiplicity includes all the cardinalities allowed by the specified multiplicity.
    	/// </summary>
    	// pre:
    	//     self.upperBound()->notEmpty() and self.lowerBound()->notEmpty() and M.upperBound()->notEmpty() and M.lowerBound()->notEmpty()
    	// spec:
    	//     result = ((self.lowerBound() <= M.lowerBound()) and (self.upperBound() >= M.upperBound()))
    	readonly bool IncludesMultiplicity(MultiplicityElement M);
    	/// <summary>
    	/// The operation is determines if the upper and lower bound of the ranges are the ones given.
    	/// </summary>
    	// spec:
    	//     result = (lowerbound = self.lowerBound() and upperbound = self.upperBound())
    	readonly bool Is(int lowerbound, long upperbound);
    	/// <summary>
    	/// The query isMultivalued() checks whether this multiplicity has an upper bound greater than one.
    	/// </summary>
    	// pre:
    	//     upperBound()->notEmpty()
    	// spec:
    	//     result = (upperBound() > 1)
    	readonly bool IsMultivalued();
    	/// <summary>
    	/// The query lowerBound() returns the lower bound of the multiplicity as an integer, which is the integerValue of lowerValue, if this is given, and 1 otherwise.
    	/// </summary>
    	// spec:
    	//     result = (if (lowerValue=null or lowerValue.integerValue()=null) then 1 else lowerValue.integerValue() endif)
    	readonly int LowerBound();
    	/// <summary>
    	/// The query upperBound() returns the upper bound of the multiplicity for a bounded multiplicity as an unlimited natural, which is the unlimitedNaturalValue of upperValue, if given, and 1, otherwise.
    	/// </summary>
    	// spec:
    	//     result = (if (upperValue=null or upperValue.unlimitedValue()=null) then 1 else upperValue.unlimitedValue() endif)
    	readonly long UpperBound();
    }

    /// <summary>
    /// ConnectableElement is an abstract metaclass representing a set of instances that play roles of a StructuredClassifier. ConnectableElements may be joined by attached Connectors and specify configurations of linked instances to be created within an instance of the containing StructuredClassifier.
    /// </summary>
    abstract class ConnectableElement : TypedElement, ParameterableElement
    {
    	/// <summary>
    	/// A set of ConnectorEnds that attach to this ConnectableElement.
    	/// </summary>
    	// spec:
    	//     result = (ConnectorEnd.allInstances()->select(role = self))
    	derived set<ConnectorEnd> End;
    	/// <summary>
    	/// The ConnectableElementTemplateParameter for this ConnectableElement parameter.
    	/// </summary>
    	ConnectableElementTemplateParameter TemplateParameter redefines ParameterableElement.TemplateParameter;
    }

    /// <summary>
    /// A PrimitiveType defines a predefined DataType, without any substructure. A PrimitiveType may have an algebra and operations defined outside of UML, for example, mathematically.
    /// </summary>
    class PrimitiveType : DataType
    {
    }

    /// <summary>
    /// A Property is a StructuralFeature. A Property related by ownedAttribute to a Classifier (other than an association) represents an attribute and might also represent an association end. It relates an instance of the Classifier to a value or set of values of the type of the attribute. A Property related by memberEnd to an Association represents an end of the Association. The type of the Property is the type of the end of the Association. A Property has the capability of being a DeploymentTarget in a Deployment relationship. This enables modeling the deployment to hierarchical nodes that have Properties functioning as internal parts.  Property specializes ParameterableElement to specify that a Property can be exposed as a formal template parameter, and provided as an actual parameter in a binding of a template.
    /// </summary>
    class Property : ConnectableElement, DeploymentTarget, StructuralFeature
    {
    	/// <summary>
    	/// Specifies the kind of aggregation that applies to the Property.
    	/// </summary>
    	AggregationKind Aggregation;
    	/// <summary>
    	/// The Association of which this Property is a member, if any.
    	/// </summary>
    	Association Association;
    	/// <summary>
    	/// Designates the optional association end that owns a qualifier attribute.
    	/// </summary>
    	Property AssociationEnd subsets Element.Owner;
    	/// <summary>
    	/// The Class that owns this Property, if any.
    	/// </summary>
    	Class Class subsets NamedElement.Namespace;
    	/// <summary>
    	/// The DataType that owns this Property, if any.
    	/// </summary>
    	DataType Datatype subsets NamedElement.Namespace;
    	/// <summary>
    	/// A ValueSpecification that is evaluated to give a default value for the Property when an instance of the owning Classifier is instantiated.
    	/// </summary>
    	containment ValueSpecification DefaultValue subsets Element.OwnedElement;
    	/// <summary>
    	/// The Interface that owns this Property, if any.
    	/// </summary>
    	Interface Interface subsets NamedElement.Namespace;
    	/// <summary>
    	/// If isComposite is true, the object containing the attribute is a container for the object or value contained in the attribute. This is a derived value, indicating whether the aggregation of the Property is composite or not.
    	/// </summary>
    	// spec:
    	//     result = (aggregation = AggregationKind::composite)
    	derived bool IsComposite;
    	/// <summary>
    	/// Specifies whether the Property is derived, i.e., whether its value or values can be computed from other information.
    	/// </summary>
    	bool IsDerived;
    	/// <summary>
    	/// Specifies whether the property is derived as the union of all of the Properties that are constrained to subset it.
    	/// </summary>
    	bool IsDerivedUnion;
    	/// <summary>
    	/// True indicates this property can be used to uniquely identify an instance of the containing Class.
    	/// </summary>
    	bool IsID;
    	/// <summary>
    	/// In the case where the Property is one end of a binary association this gives the other end.
    	/// </summary>
    	// spec:
    	//     result = (if association <> null and association.memberEnd->size() = 2
    	//     then
    	//         association.memberEnd->any(e | e <> self)
    	//     else
    	//         null
    	//     endif)
    	derived Property Opposite;
    	/// <summary>
    	/// The owning association of this property, if any.
    	/// </summary>
    	Association OwningAssociation subsets Feature.FeaturingClassifier, NamedElement.Namespace, Property.Association, RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// An optional list of ordered qualifier attributes for the end.
    	/// </summary>
    	containment list<Property> Qualifier subsets Element.OwnedElement;
    	/// <summary>
    	/// The properties that are redefined by this property, if any.
    	/// </summary>
    	set<Property> RedefinedProperty subsets RedefinableElement.RedefinedElement;
    	/// <summary>
    	/// The properties of which this Property is constrained to be a subset, if any.
    	/// </summary>
    	set<Property> SubsettedProperty;
    	/// <summary>
    	/// The query isAttribute() is true if the Property is defined as an attribute of some Classifier.
    	/// </summary>
    	// spec:
    	//     result = (not classifier->isEmpty())
    	readonly bool IsAttribute();
    	/// <summary>
    	/// The query isCompatibleWith() determines if this Property is compatible with the specified ParameterableElement. This Property is compatible with ParameterableElement p if the kind of this Property is thesame as or a subtype of the kind of p. Further, if p is a TypedElement, then the type of this Property must be conformant with the type of p.
    	/// </summary>
    	// spec:
    	//     result = (self.oclIsKindOf(p.oclType()) and (p.oclIsKindOf(TypeElement) implies
    	//     self.type.conformsTo(p.oclAsType(TypedElement).type)))
    	readonly bool IsCompatibleWith(ParameterableElement p);
    	/// <summary>
    	/// The query isConsistentWith() specifies, for any two Properties in a context in which redefinition is possible, whether redefinition would be logically consistent. A redefining Property is consistent with a redefined Property if the type of the redefining Property conforms to the type of the redefined Property, and the multiplicity of the redefining Property (if specified) is contained in the multiplicity of the redefined Property.
    	/// </summary>
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	// spec:
    	//     result = (redefiningElement.oclIsKindOf(Property) and 
    	//       let prop : Property = redefiningElement.oclAsType(Property) in 
    	//       (prop.type.conformsTo(self.type) and 
    	//       ((prop.lowerBound()->notEmpty() and self.lowerBound()->notEmpty()) implies prop.lowerBound() >= self.lowerBound()) and 
    	//       ((prop.upperBound()->notEmpty() and self.upperBound()->notEmpty()) implies prop.lowerBound() <= self.lowerBound()) and 
    	//       (self.isComposite implies prop.isComposite)))
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    	/// <summary>
    	/// The query isNavigable() indicates whether it is possible to navigate across the property.
    	/// </summary>
    	// spec:
    	//     result = (not classifier->isEmpty() or association.navigableOwnedEnd->includes(self))
    	readonly bool IsNavigable();
    	/// <summary>
    	/// The query subsettingContext() gives the context for subsetting a Property. It consists, in the case of an attribute, of the corresponding Classifier, and in the case of an association end, all of the Classifiers at the other ends.
    	/// </summary>
    	// spec:
    	//     result = (if association <> null
    	//     then association.memberEnd->excluding(self)->collect(type)->asSet()
    	//     else 
    	//       if classifier<>null
    	//       then classifier->asSet()
    	//       else Set{} 
    	//       endif
    	//     endif)
    	readonly set<Type> SubsettingContext();
    }

    /// <summary>
    /// A StructuralFeature is a typed feature of a Classifier that specifies the structure of instances of the Classifier.
    /// </summary>
    abstract class StructuralFeature : MultiplicityElement, TypedElement, Feature
    {
    	/// <summary>
    	/// If isReadOnly is true, the StructuralFeature may not be written to after initialization.
    	/// </summary>
    	bool IsReadOnly;
    }

    /// <summary>
    /// A Slot designates that an entity modeled by an InstanceSpecification has a value or values for a specific StructuralFeature.
    /// </summary>
    class Slot : Element
    {
    	/// <summary>
    	/// The StructuralFeature that specifies the values that may be held by the Slot.
    	/// </summary>
    	StructuralFeature DefiningFeature;
    	/// <summary>
    	/// The InstanceSpecification that owns this Slot.
    	/// </summary>
    	InstanceSpecification OwningInstance subsets Element.Owner;
    	/// <summary>
    	/// The value or values held by the Slot.
    	/// </summary>
    	containment list<ValueSpecification> Value subsets Element.OwnedElement;
    }

    class Argument
    {
    	string Name;
    	Object Value;
    }

    class Tag : Element
    {
    	string Name;
    	string Value;
    	set<Element> Element;
    	Element TagOwner;
    }

    class Link : Object
    {
    	Element FirstElement;
    	Element SecondElement;
    	Association Association;
    	readonly bool Equals(Link otherLink);
    	void Delete();
    }

    class Exception
    {
    	Element ObjectInError;
    	Element ElementInError;
    	string Description;
    }

    /// <summary>
    /// A CollaborationUse is used to specify the application of a pattern specified by a Collaboration to a specific situation.
    /// </summary>
    class CollaborationUse : NamedElement
    {
    	/// <summary>
    	/// A mapping between features of the Collaboration and features of the owning Classifier. This mapping indicates which ConnectableElement of the Classifier plays which role(s) in the Collaboration. A ConnectableElement may be bound to multiple roles in the same CollaborationUse (that is, it may play multiple roles).
    	/// </summary>
    	containment set<Dependency> RoleBinding subsets Element.OwnedElement;
    	/// <summary>
    	/// The Collaboration which is used in this CollaborationUse. The Collaboration defines the cooperation between its roles which are mapped to ConnectableElements relating to the Classifier owning the CollaborationUse.
    	/// </summary>
    	Collaboration Type;
    }

    /// <summary>
    /// A RedefinableTemplateSignature supports the addition of formal template parameters in a specialization of a template classifier.
    /// </summary>
    class RedefinableTemplateSignature : RedefinableElement, TemplateSignature
    {
    	/// <summary>
    	/// The Classifier that owns this RedefinableTemplateSignature.
    	/// </summary>
    	Classifier Classifier redefines TemplateSignature.Template subsets RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// The signatures extended by this RedefinableTemplateSignature.
    	/// </summary>
    	set<RedefinableTemplateSignature> ExtendedSignature subsets RedefinableElement.RedefinedElement;
    	/// <summary>
    	/// The formal template parameters of the extended signatures.
    	/// </summary>
    	// spec:
    	//     result = (if extendedSignature->isEmpty() then Set{} else extendedSignature.parameter->asSet() endif)
    	derived set<TemplateParameter> InheritedParameter subsets TemplateSignature.Parameter;
    	/// <summary>
    	/// The query isConsistentWith() specifies, for any two RedefinableTemplateSignatures in a context in which redefinition is possible, whether redefinition would be logically consistent. A redefining template signature is always consistent with a redefined template signature, as redefinition only adds new formal parameters.
    	/// </summary>
    	// spec:
    	//     result = (redefiningElement.oclIsKindOf(RedefinableTemplateSignature))
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    }

    /// <summary>
    /// A Template Signature bundles the set of formal TemplateParameters for a template.
    /// </summary>
    class TemplateSignature : Element
    {
    	/// <summary>
    	/// The formal parameters that are owned by this TemplateSignature.
    	/// </summary>
    	containment list<TemplateParameter> OwnedParameter subsets Element.OwnedElement, TemplateSignature.Parameter;
    	/// <summary>
    	/// The ordered set of all formal TemplateParameters for this TemplateSignature.
    	/// </summary>
    	list<TemplateParameter> Parameter;
    	/// <summary>
    	/// The TemplateableElement that owns this TemplateSignature.
    	/// </summary>
    	TemplateableElement Template subsets Element.Owner;
    }

    /// <summary>
    /// A UseCase specifies a set of actions performed by its subjects, which yields an observable result that is of value for one or more Actors or other stakeholders of each subject.
    /// </summary>
    class UseCase : BehavioredClassifier
    {
    	/// <summary>
    	/// The Extend relationships owned by this UseCase.
    	/// </summary>
    	containment set<Extend> Extend subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The ExtensionPoints owned by this UseCase.
    	/// </summary>
    	containment set<ExtensionPoint> ExtensionPoint subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The Include relationships owned by this UseCase.
    	/// </summary>
    	containment set<Include> Include subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The subjects to which this UseCase applies. Each subject or its parts realize all the UseCases that apply to it.
    	/// </summary>
    	set<Classifier> Subject;
    	/// <summary>
    	/// The query allIncludedUseCases() returns the transitive closure of all UseCases (directly or indirectly) included by this UseCase.
    	/// </summary>
    	// spec:
    	//     result = (self.include.addition->union(self.include.addition->collect(uc | uc.allIncludedUseCases()))->asSet())
    	readonly set<UseCase> AllIncludedUseCases();
    }

    /// <summary>
    /// A GeneralizationSet is a PackageableElement whose instances represent sets of Generalization relationships.
    /// </summary>
    class GeneralizationSet : PackageableElement
    {
    	/// <summary>
    	/// Designates the instances of Generalization that are members of this GeneralizationSet.
    	/// </summary>
    	set<Generalization> Generalization;
    	/// <summary>
    	/// Indicates (via the associated Generalizations) whether or not the set of specific Classifiers are covering for a particular general classifier. When isCovering is true, every instance of a particular general Classifier is also an instance of at least one of its specific Classifiers for the GeneralizationSet. When isCovering is false, there are one or more instances of the particular general Classifier that are not instances of at least one of its specific Classifiers defined for the GeneralizationSet.
    	/// </summary>
    	bool IsCovering;
    	/// <summary>
    	/// Indicates whether or not the set of specific Classifiers in a Generalization relationship have instance in common. If isDisjoint is true, the specific Classifiers for a particular GeneralizationSet have no members in common; that is, their intersection is empty. If isDisjoint is false, the specific Classifiers in a particular GeneralizationSet have one or more members in common; that is, their intersection is not empty.
    	/// </summary>
    	bool IsDisjoint;
    	/// <summary>
    	/// Designates the Classifier that is defined as the power type for the associated GeneralizationSet, if there is one.
    	/// </summary>
    	Classifier Powertype;
    }

    /// <summary>
    /// A substitution is a relationship between two classifiers signifying that the substituting classifier complies with the contract specified by the contract classifier. This implies that instances of the substituting classifier are runtime substitutable where instances of the contract classifier are expected.
    /// </summary>
    class Substitution : Realization
    {
    	/// <summary>
    	/// The contract with which the substituting classifier complies.
    	/// </summary>
    	Classifier Contract subsets Dependency.Supplier;
    	/// <summary>
    	/// Instances of the substituting classifier are runtime substitutable where instances of the contract classifier are expected.
    	/// </summary>
    	Classifier SubstitutingClassifier subsets Dependency.Client, Element.Owner;
    }

    /// <summary>
    /// Realization is a specialized Abstraction relationship between two sets of model Elements, one representing a specification (the supplier) and the other represents an implementation of the latter (the client). Realization can be used to model stepwise refinement, optimizations, transformations, templates, model synthesis, framework composition, etc.
    /// </summary>
    class Realization : Abstraction
    {
    }

    /// <summary>
    /// An Abstraction is a Relationship that relates two Elements or sets of Elements that represent the same concept at different levels of abstraction or from different viewpoints.
    /// </summary>
    class Abstraction : Dependency
    {
    	/// <summary>
    	/// An OpaqueExpression that states the abstraction relationship between the supplier(s) and the client(s). In some cases, such as derivation, it is usually formal and unidirectional; in other cases, such as trace, it is usually informal and bidirectional. The mapping expression is optional and may be omitted if the precise relationship between the Elements is not specified.
    	/// </summary>
    	containment OpaqueExpression Mapping subsets Element.OwnedElement;
    }

    /// <summary>
    /// A Dependency is a Relationship that signifies that a single model Element or a set of model Elements requires other model Elements for their specification or implementation. This means that the complete semantics of the client Element(s) are either semantically or structurally dependent on the definition of the supplier Element(s).
    /// </summary>
    class Dependency : DirectedRelationship, PackageableElement
    {
    	/// <summary>
    	/// The Element(s) dependent on the supplier Element(s). In some cases (such as a trace Abstraction) the assignment of direction (that is, the designation of the client Element) is at the discretion of the modeler and is a stipulation.
    	/// </summary>
    	set<NamedElement> Client subsets DirectedRelationship.Source;
    	/// <summary>
    	/// The Element(s) on which the client Element(s) depend in some respect. The modeler may stipulate a sense of Dependency direction suitable for their domain.
    	/// </summary>
    	set<NamedElement> Supplier subsets DirectedRelationship.Target;
    }

    /// <summary>
    /// A ClassifierTemplateParameter exposes a Classifier as a formal template parameter.
    /// </summary>
    class ClassifierTemplateParameter : TemplateParameter
    {
    	/// <summary>
    	/// Constrains the required relationship between an actual parameter and the parameteredElement for this formal parameter.
    	/// </summary>
    	bool AllowSubstitutable;
    	/// <summary>
    	/// The classifiers that constrain the argument that can be used for the parameter. If the allowSubstitutable attribute is true, then any Classifier that is compatible with this constraining Classifier can be substituted; otherwise, it must be either this Classifier or one of its specializations. If this property is empty, there are no constraints on the Classifier that can be used as an argument.
    	/// </summary>
    	set<Classifier> ConstrainingClassifier;
    	/// <summary>
    	/// The Classifier exposed by this ClassifierTemplateParameter.
    	/// </summary>
    	Classifier ParameteredElement redefines TemplateParameter.ParameteredElement;
    }

    /// <summary>
    /// A TemplateParameter exposes a ParameterableElement as a formal parameter of a template.
    /// </summary>
    class TemplateParameter : Element
    {
    	/// <summary>
    	/// The ParameterableElement that is the default for this formal TemplateParameter.
    	/// </summary>
    	ParameterableElement Default;
    	/// <summary>
    	/// The ParameterableElement that is owned by this TemplateParameter for the purpose of providing a default.
    	/// </summary>
    	containment ParameterableElement OwnedDefault subsets Element.OwnedElement, TemplateParameter.Default;
    	/// <summary>
    	/// The ParameterableElement that is owned by this TemplateParameter for the purpose of exposing it as the parameteredElement.
    	/// </summary>
    	containment ParameterableElement OwnedParameteredElement subsets Element.OwnedElement, TemplateParameter.ParameteredElement;
    	/// <summary>
    	/// The ParameterableElement exposed by this TemplateParameter.
    	/// </summary>
    	ParameterableElement ParameteredElement;
    	/// <summary>
    	/// The TemplateSignature that owns this TemplateParameter.
    	/// </summary>
    	TemplateSignature Signature subsets Element.Owner;
    }

    /// <summary>
    /// Interfaces declare coherent services that are implemented by BehavioredClassifiers that implement the Interfaces via InterfaceRealizations.
    /// </summary>
    class Interface : Classifier
    {
    	/// <summary>
    	/// References all the Classifiers that are defined (nested) within the Interface.
    	/// </summary>
    	containment list<Classifier> NestedClassifier subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The attributes (i.e., the Properties) owned by the Interface.
    	/// </summary>
    	containment list<Property> OwnedAttribute subsets Classifier.Attribute, Namespace.OwnedMember;
    	/// <summary>
    	/// The Operations owned by the Interface.
    	/// </summary>
    	containment list<Operation> OwnedOperation subsets Classifier.Feature, Namespace.OwnedMember;
    	/// <summary>
    	/// Receptions that objects providing this Interface are willing to accept.
    	/// </summary>
    	containment set<Reception> OwnedReception subsets Classifier.Feature, Namespace.OwnedMember;
    	/// <summary>
    	/// References a ProtocolStateMachine specifying the legal sequences of the invocation of the BehavioralFeatures described in the Interface.
    	/// </summary>
    	containment ProtocolStateMachine Protocol subsets Namespace.OwnedMember;
    	/// <summary>
    	/// References all the Interfaces redefined by this Interface.
    	/// </summary>
    	set<Interface> RedefinedInterface subsets Classifier.RedefinedClassifier;
    }

    /// <summary>
    /// An ElementImport identifies a NamedElement in a Namespace other than the one that owns that NamedElement and allows the NamedElement to be referenced using an unqualified name in the Namespace owning the ElementImport.
    /// </summary>
    class ElementImport : DirectedRelationship
    {
    	/// <summary>
    	/// Specifies the name that should be added to the importing Namespace in lieu of the name of the imported PackagableElement. The alias must not clash with any other member in the importing Namespace. By default, no alias is used.
    	/// </summary>
    	string Alias;
    	/// <summary>
    	/// Specifies the PackageableElement whose name is to be added to a Namespace.
    	/// </summary>
    	PackageableElement ImportedElement subsets DirectedRelationship.Target;
    	/// <summary>
    	/// Specifies the Namespace that imports a PackageableElement from another Namespace.
    	/// </summary>
    	Namespace ImportingNamespace subsets DirectedRelationship.Source, Element.Owner;
    	/// <summary>
    	/// Specifies the visibility of the imported PackageableElement within the importingNamespace, i.e., whether the  importedElement will in turn be visible to other Namespaces. If the ElementImport is public, the importedElement will be visible outside the importingNamespace while, if the ElementImport is private, it will not.
    	/// </summary>
    	VisibilityKind Visibility;
    	/// <summary>
    	/// The query getName() returns the name under which the imported PackageableElement will be known in the importing namespace.
    	/// </summary>
    	// spec:
    	//     result = (if alias->notEmpty() then
    	//       alias
    	//     else
    	//       importedElement.name
    	//     endif)
    	readonly string GetName();
    }

    /// <summary>
    /// A Constraint is a condition or restriction expressed in natural language text or in a machine readable language for the purpose of declaring some of the semantics of an Element or set of Elements.
    /// </summary>
    class Constraint : PackageableElement
    {
    	/// <summary>
    	/// The ordered set of Elements referenced by this Constraint.
    	/// </summary>
    	list<Element> ConstrainedElement;
    	/// <summary>
    	/// Specifies the Namespace that owns the Constraint.
    	/// </summary>
    	Namespace Context subsets NamedElement.Namespace;
    	/// <summary>
    	/// A condition that must be true when evaluated in order for the Constraint to be satisfied.
    	/// </summary>
    	containment ValueSpecification Specification subsets Element.OwnedElement;
    }

    /// <summary>
    /// A PackageImport is a Relationship that imports all the non-private members of a Package into the Namespace owning the PackageImport, so that those Elements may be referred to by their unqualified names in the importingNamespace.
    /// </summary>
    class PackageImport : DirectedRelationship
    {
    	/// <summary>
    	/// Specifies the Package whose members are imported into a Namespace.
    	/// </summary>
    	Package ImportedPackage subsets DirectedRelationship.Target;
    	/// <summary>
    	/// Specifies the Namespace that imports the members from a Package.
    	/// </summary>
    	Namespace ImportingNamespace subsets DirectedRelationship.Source, Element.Owner;
    	/// <summary>
    	/// Specifies the visibility of the imported PackageableElements within the importingNamespace, i.e., whether imported Elements will in turn be visible to other Namespaces. If the PackageImport is public, the imported Elements will be visible outside the importingNamespace, while, if the PackageImport is private, they will not.
    	/// </summary>
    	VisibilityKind Visibility;
    }

    /// <summary>
    /// A StringExpression is an Expression that specifies a String value that is derived by concatenating a sequence of operands with String values or a sequence of subExpressions, some of which might be template parameters.
    /// </summary>
    class StringExpression : TemplateableElement, Expression
    {
    	/// <summary>
    	/// The StringExpression of which this StringExpression is a subExpression.
    	/// </summary>
    	StringExpression OwningExpression subsets Element.Owner;
    	/// <summary>
    	/// The StringExpressions that constitute this StringExpression.
    	/// </summary>
    	containment list<StringExpression> SubExpression subsets Element.OwnedElement;
    	/// <summary>
    	/// The query stringValue() returns the String resulting from concatenating, in order, all the component String values of all the operands or subExpressions that are part of the StringExpression.
    	/// </summary>
    	// spec:
    	//     result = (if subExpression->notEmpty()
    	//     then subExpression->iterate(se; stringValue: String = '' | stringValue.concat(se.stringValue()))
    	//     else operand->iterate(op; stringValue: String = '' | stringValue.concat(op.stringValue()))
    	//     endif)
    	readonly string StringValue();
    }

    /// <summary>
    /// An Expression represents a node in an expression tree, which may be non-terminal or terminal. It defines a symbol, and has a possibly empty sequence of operands that are ValueSpecifications. It denotes a (possibly empty) set of values when evaluated in a context.
    /// </summary>
    class Expression : ValueSpecification
    {
    	/// <summary>
    	/// Specifies a sequence of operand ValueSpecifications.
    	/// </summary>
    	containment list<ValueSpecification> Operand subsets Element.OwnedElement;
    	/// <summary>
    	/// The symbol associated with this node in the expression tree.
    	/// </summary>
    	string Symbol;
    }

    /// <summary>
    /// A TemplateBinding is a DirectedRelationship between a TemplateableElement and a template. A TemplateBinding specifies the TemplateParameterSubstitutions of actual parameters for the formal parameters of the template.
    /// </summary>
    class TemplateBinding : DirectedRelationship
    {
    	/// <summary>
    	/// The TemplateableElement that is bound by this TemplateBinding.
    	/// </summary>
    	TemplateableElement BoundElement subsets DirectedRelationship.Source, Element.Owner;
    	/// <summary>
    	/// The TemplateParameterSubstitutions owned by this TemplateBinding.
    	/// </summary>
    	containment set<TemplateParameterSubstitution> ParameterSubstitution subsets Element.OwnedElement;
    	/// <summary>
    	/// The TemplateSignature for the template that is the target of this TemplateBinding.
    	/// </summary>
    	TemplateSignature Signature subsets DirectedRelationship.Target;
    }

    /// <summary>
    /// An extension is used to indicate that the properties of a metaclass are extended through a stereotype, and gives the ability to flexibly add (and later remove) stereotypes to classes.
    /// </summary>
    class Extension : Association
    {
    	/// <summary>
    	/// Indicates whether an instance of the extending stereotype must be created when an instance of the extended class is created. The attribute value is derived from the value of the lower property of the ExtensionEnd referenced by Extension::ownedEnd; a lower value of 1 means that isRequired is true, but otherwise it is false. Since the default value of ExtensionEnd::lower is 0, the default value of isRequired is false.
    	/// </summary>
    	// spec:
    	//     result = (ownedEnd.lowerBound() = 1)
    	derived bool IsRequired;
    	/// <summary>
    	/// References the Class that is extended through an Extension. The property is derived from the type of the memberEnd that is not the ownedEnd.
    	/// </summary>
    	// spec:
    	//     result = (metaclassEnd().type.oclAsType(Class))
    	derived Class Metaclass;
    	/// <summary>
    	/// References the end of the extension that is typed by a Stereotype.
    	/// </summary>
    	containment ExtensionEnd OwnedEnd redefines Association.OwnedEnd;
    	/// <summary>
    	/// The query metaclassEnd() returns the Property that is typed by a metaclass (as opposed to a stereotype).
    	/// </summary>
    	// spec:
    	//     result = (memberEnd->reject(p | ownedEnd->includes(p.oclAsType(ExtensionEnd)))->any(true))
    	readonly Property MetaclassEnd();
    }

    /// <summary>
    /// A Reception is a declaration stating that a Classifier is prepared to react to the receipt of a Signal.
    /// </summary>
    class Reception : BehavioralFeature
    {
    	/// <summary>
    	/// The Signal that this Reception handles.
    	/// </summary>
    	Signal Signal;
    }

    /// <summary>
    /// Behavior is a specification of how its context BehavioredClassifier changes state over time. This specification may be either a definition of possible behavior execution or emergent behavior, or a selective illustration of an interesting subset of possible executions. The latter form is typically used for capturing examples, such as a trace of a particular execution.
    /// </summary>
    abstract class Behavior : Class
    {
    	/// <summary>
    	/// The BehavioredClassifier that is the context for the execution of the Behavior. A Behavior that is directly owned as a nestedClassifier does not have a context. Otherwise, to determine the context of a Behavior, find the first BehavioredClassifier reached by following the chain of owner relationships from the Behavior, if any. If there is such a BehavioredClassifier, then it is the context, unless it is itself a Behavior with a non-empty context, in which case that is also the context for the original Behavior. For example, following this algorithm, the context of an entry Behavior in a StateMachine is the BehavioredClassifier that owns the StateMachine. The features of the context BehavioredClassifier as well as the Elements visible to the context Classifier are visible to the Behavior.
    	/// </summary>
    	// spec:
    	//     result = (if nestingClass <> null then
    	//         null
    	//     else
    	//         let b:BehavioredClassifier = self.behavioredClassifier(self.owner) in
    	//         if b.oclIsKindOf(Behavior) and b.oclAsType(Behavior)._'context' <> null then 
    	//             b.oclAsType(Behavior)._'context'
    	//         else 
    	//             b 
    	//         endif
    	//     endif
    	//             )
    	derived BehavioredClassifier Context subsets RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// Tells whether the Behavior can be invoked while it is still executing from a previous invocation.
    	/// </summary>
    	bool IsReentrant;
    	/// <summary>
    	/// References a list of Parameters to the Behavior which describes the order and type of arguments that can be given when the Behavior is invoked and of the values which will be returned when the Behavior completes its execution.
    	/// </summary>
    	containment list<Parameter> OwnedParameter subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The ParameterSets owned by this Behavior.
    	/// </summary>
    	containment set<ParameterSet> OwnedParameterSet subsets Namespace.OwnedMember;
    	/// <summary>
    	/// An optional set of Constraints specifying what is fulfilled after the execution of the Behavior is completed, if its precondition was fulfilled before its invocation.
    	/// </summary>
    	containment set<Constraint> Postcondition subsets Namespace.OwnedRule;
    	/// <summary>
    	/// An optional set of Constraints specifying what must be fulfilled before the Behavior is invoked.
    	/// </summary>
    	containment set<Constraint> Precondition subsets Namespace.OwnedRule;
    	/// <summary>
    	/// Designates a BehavioralFeature that the Behavior implements. The BehavioralFeature must be owned by the BehavioredClassifier that owns the Behavior or be inherited by it. The Parameters of the BehavioralFeature and the implementing Behavior must match. A Behavior does not need to have a specification, in which case it either is the classifierBehavior of a BehavioredClassifier or it can only be invoked by another Behavior of the Classifier.
    	/// </summary>
    	BehavioralFeature Specification;
    	/// <summary>
    	/// References the Behavior that this Behavior redefines. A subtype of Behavior may redefine any other subtype of Behavior. If the Behavior implements a BehavioralFeature, it replaces the redefined Behavior. If the Behavior is a classifierBehavior, it extends the redefined Behavior.
    	/// </summary>
    	set<Behavior> RedefinedBehavior subsets Classifier.RedefinedClassifier;
    	/// <summary>
    	/// The first BehavioredClassifier reached by following the chain of owner relationships from the Behavior, if any.
    	/// </summary>
    	// spec:
    	//     if from.oclIsKindOf(BehavioredClassifier) then
    	//         from.oclAsType(BehavioredClassifier)
    	//     else if from.owner = null then
    	//         null
    	//     else
    	//         self.behavioredClassifier(from.owner)
    	//     endif
    	//     endif
    	//         
    	readonly BehavioredClassifier BehavioredClassifier(Element from);
    	/// <summary>
    	/// The in and inout ownedParameters of the Behavior.
    	/// </summary>
    	// spec:
    	//     result = (ownedParameter->select(direction=ParameterDirectionKind::_'in' or direction=ParameterDirectionKind::inout))
    	readonly list<Parameter> InputParameters();
    	/// <summary>
    	/// The out, inout and return ownedParameters.
    	/// </summary>
    	// spec:
    	//     result = (ownedParameter->select(direction=ParameterDirectionKind::out or direction=ParameterDirectionKind::inout or direction=ParameterDirectionKind::return))
    	readonly list<Parameter> OutputParameters();
    }

    /// <summary>
    /// An InterfaceRealization is a specialized realization relationship between a BehavioredClassifier and an Interface. This relationship signifies that the realizing BehavioredClassifier conforms to the contract specified by the Interface.
    /// </summary>
    class InterfaceRealization : Realization
    {
    	/// <summary>
    	/// References the Interface specifying the conformance contract.
    	/// </summary>
    	Interface Contract subsets Dependency.Supplier;
    	/// <summary>
    	/// References the BehavioredClassifier that owns this InterfaceRealization, i.e., the BehavioredClassifier that realizes the Interface to which it refers.
    	/// </summary>
    	BehavioredClassifier ImplementingClassifier subsets Dependency.Client, Element.Owner;
    }

    /// <summary>
    /// A Port is a property of an EncapsulatedClassifier that specifies a distinct interaction point between that EncapsulatedClassifier and its environment or between the (behavior of the) EncapsulatedClassifier and its internal parts. Ports are connected to Properties of the EncapsulatedClassifier by Connectors through which requests can be made to invoke BehavioralFeatures. A Port may specify the services an EncapsulatedClassifier provides (offers) to its environment as well as the services that an EncapsulatedClassifier expects (requires) of its environment.  A Port may have an associated ProtocolStateMachine.
    /// </summary>
    class Port : Property
    {
    	/// <summary>
    	/// Specifies whether requests arriving at this Port are sent to the classifier behavior of this EncapsulatedClassifier. Such a Port is referred to as a behavior Port. Any invocation of a BehavioralFeature targeted at a behavior Port will be handled by the instance of the owning EncapsulatedClassifier itself, rather than by any instances that it may contain.
    	/// </summary>
    	bool IsBehavior;
    	/// <summary>
    	/// Specifies the way that the provided and required Interfaces are derived from the Port’s Type.
    	/// </summary>
    	bool IsConjugated;
    	/// <summary>
    	/// If true, indicates that this Port is used to provide the published functionality of an EncapsulatedClassifier.  If false, this Port is used to implement the EncapsulatedClassifier but is not part of the essential externally-visible functionality of the EncapsulatedClassifier and can, therefore, be altered or deleted along with the internal implementation of the EncapsulatedClassifier and other properties that are considered part of its implementation.
    	/// </summary>
    	bool IsService;
    	/// <summary>
    	/// An optional ProtocolStateMachine which describes valid interactions at this interaction point.
    	/// </summary>
    	ProtocolStateMachine Protocol;
    	/// <summary>
    	/// The Interfaces specifying the set of Operations and Receptions that the EncapsulatedCclassifier offers to its environment via this Port, and which it will handle either directly or by forwarding it to a part of its internal structure. This association is derived according to the value of isConjugated. If isConjugated is false, provided is derived as the union of the sets of Interfaces realized by the type of the port and its supertypes, or directly from the type of the Port if the Port is typed by an Interface. If isConjugated is true, it is derived as the union of the sets of Interfaces used by the type of the Port and its supertypes.
    	/// </summary>
    	// spec:
    	//     result = (if isConjugated then basicRequired() else basicProvided() endif)
    	derived set<Interface> Provided;
    	/// <summary>
    	/// A Port may be redefined when its containing EncapsulatedClassifier is specialized. The redefining Port may have additional Interfaces to those that are associated with the redefined Port or it may replace an Interface by one of its subtypes.
    	/// </summary>
    	set<Port> RedefinedPort subsets Property.RedefinedProperty;
    	/// <summary>
    	/// The Interfaces specifying the set of Operations and Receptions that the EncapsulatedCassifier expects its environment to handle via this port. This association is derived according to the value of isConjugated. If isConjugated is false, required is derived as the union of the sets of Interfaces used by the type of the Port and its supertypes. If isConjugated is true, it is derived as the union of the sets of Interfaces realized by the type of the Port and its supertypes, or directly from the type of the Port if the Port is typed by an Interface.
    	/// </summary>
    	// spec:
    	//     result = (if isConjugated then basicProvided() else basicRequired() endif)
    	derived set<Interface> Required;
    	/// <summary>
    	/// The union of the sets of Interfaces realized by the type of the Port and its supertypes, or directly the type of the Port if the Port is typed by an Interface.
    	/// </summary>
    	// spec:
    	//     result = (if type.oclIsKindOf(Interface) 
    	//     then type.oclAsType(Interface)->asSet() 
    	//     else type.oclAsType(Classifier).allRealizedInterfaces() 
    	//     endif)
    	readonly set<Interface> BasicProvided();
    	/// <summary>
    	/// The union of the sets of Interfaces used by the type of the Port and its supertypes.
    	/// </summary>
    	// spec:
    	//     result = ( type.oclAsType(Classifier).allUsedInterfaces() )
    	readonly set<Interface> BasicRequired();
    }

    /// <summary>
    /// A Connector specifies links that enables communication between two or more instances. In contrast to Associations, which specify links between any instance of the associated Classifiers, Connectors specify links between instances playing the connected parts only.
    /// </summary>
    class Connector : Feature
    {
    	/// <summary>
    	/// The set of Behaviors that specify the valid interaction patterns across the Connector.
    	/// </summary>
    	set<Behavior> Contract;
    	/// <summary>
    	/// A Connector has at least two ConnectorEnds, each representing the participation of instances of the Classifiers typing the ConnectableElements attached to the end. The set of ConnectorEnds is ordered.
    	/// </summary>
    	containment list<ConnectorEnd> End subsets Element.OwnedElement;
    	/// <summary>
    	/// Indicates the kind of Connector. This is derived: a Connector with one or more ends connected to a Port which is not on a Part and which is not a behavior port is a delegation; otherwise it is an assembly.
    	/// </summary>
    	// spec:
    	//     result = (if end->exists(
    	//     		role.oclIsKindOf(Port) 
    	//     		and partWithPort->isEmpty()
    	//     		and not role.oclAsType(Port).isBehavior)
    	//     then ConnectorKind::delegation 
    	//     else ConnectorKind::assembly 
    	//     endif)
    	derived ConnectorKind Kind;
    	/// <summary>
    	/// A Connector may be redefined when its containing Classifier is specialized. The redefining Connector may have a type that specializes the type of the redefined Connector. The types of the ConnectorEnds of the redefining Connector may specialize the types of the ConnectorEnds of the redefined Connector. The properties of the ConnectorEnds of the redefining Connector may be replaced.
    	/// </summary>
    	set<Connector> RedefinedConnector subsets RedefinableElement.RedefinedElement;
    	/// <summary>
    	/// An optional Association that classifies links corresponding to this Connector.
    	/// </summary>
    	Association Type;
    }

    /// <summary>
    /// A deployment is the allocation of an artifact or artifact instance to a deployment target.
    /// A component deployment is the deployment of one or more artifacts or artifact instances to a deployment target, optionally parameterized by a deployment specification. Examples are executables and configuration files.
    /// </summary>
    class Deployment : Dependency
    {
    	/// <summary>
    	/// The specification of properties that parameterize the deployment and execution of one or more Artifacts.
    	/// </summary>
    	containment set<DeploymentSpecification> Configuration subsets Element.OwnedElement;
    	/// <summary>
    	/// The Artifacts that are deployed onto a Node. This association specializes the supplier association.
    	/// </summary>
    	set<DeployedArtifact> DeployedArtifact subsets Dependency.Supplier;
    	/// <summary>
    	/// The DeployedTarget which is the target of a Deployment.
    	/// </summary>
    	DeploymentTarget Location subsets Dependency.Client, Element.Owner;
    }

    /// <summary>
    /// An OperationTemplateParameter exposes an Operation as a formal parameter for a template.
    /// </summary>
    class OperationTemplateParameter : TemplateParameter
    {
    	/// <summary>
    	/// The Operation exposed by this OperationTemplateParameter.
    	/// </summary>
    	Operation ParameteredElement redefines TemplateParameter.ParameteredElement;
    }

    /// <summary>
    /// A ParameterSet designates alternative sets of inputs or outputs that a Behavior may use.
    /// </summary>
    class ParameterSet : NamedElement
    {
    	/// <summary>
    	/// A constraint that should be satisfied for the owner of the Parameters in an input ParameterSet to start execution using the values provided for those Parameters, or the owner of the Parameters in an output ParameterSet to end execution providing the values for those Parameters, if all preconditions and conditions on input ParameterSets were satisfied.
    	/// </summary>
    	containment set<Constraint> Condition subsets Element.OwnedElement;
    	/// <summary>
    	/// Parameters in the ParameterSet.
    	/// </summary>
    	set<Parameter> Parameter;
    }

    /// <summary>
    /// A stereotype defines how an existing metaclass may be extended, and enables the use of platform or domain specific terminology or notation in place of, or in addition to, the ones used for the extended metaclass.
    /// </summary>
    class Stereotype : Class
    {
    	/// <summary>
    	/// Stereotype can change the graphical appearance of the extended model element by using attached icons. When this association is not null, it references the location of the icon content to be displayed within diagrams presenting the extended model elements.
    	/// </summary>
    	containment set<Image> Icon subsets Element.OwnedElement;
    	/// <summary>
    	/// The profile that directly or indirectly contains this stereotype.
    	/// </summary>
    	// spec:
    	//     result = (self.containingProfile())
    	derived Profile Profile;
    	/// <summary>
    	/// The query containingProfile returns the closest profile directly or indirectly containing this stereotype.
    	/// </summary>
    	// spec:
    	//     result = (self.namespace.oclAsType(Package).containingProfile())
    	readonly Profile ContainingProfile();
    }

    /// <summary>
    /// A package merge defines how the contents of one package are extended by the contents of another package.
    /// </summary>
    class PackageMerge : DirectedRelationship
    {
    	/// <summary>
    	/// References the Package that is to be merged with the receiving package of the PackageMerge.
    	/// </summary>
    	Package MergedPackage subsets DirectedRelationship.Target;
    	/// <summary>
    	/// References the Package that is being extended with the contents of the merged package of the PackageMerge.
    	/// </summary>
    	Package ReceivingPackage subsets DirectedRelationship.Source, Element.Owner;
    }

    /// <summary>
    /// A profile application is used to show which profiles have been applied to a package.
    /// </summary>
    class ProfileApplication : DirectedRelationship
    {
    	/// <summary>
    	/// References the Profiles that are applied to a Package through this ProfileApplication.
    	/// </summary>
    	Profile AppliedProfile subsets DirectedRelationship.Target;
    	/// <summary>
    	/// The package that owns the profile application.
    	/// </summary>
    	Package ApplyingPackage subsets DirectedRelationship.Source, Element.Owner;
    	/// <summary>
    	/// Specifies that the Profile filtering rules for the metaclasses of the referenced metamodel shall be strictly applied.
    	/// </summary>
    	bool IsStrict;
    }

    /// <summary>
    /// A profile defines limited extensions to a reference metamodel with the purpose of adapting the metamodel to a specific platform or domain.
    /// </summary>
    class Profile : Package
    {
    	/// <summary>
    	/// References a metaclass that may be extended.
    	/// </summary>
    	containment set<ElementImport> MetaclassReference subsets Namespace.ElementImport;
    	/// <summary>
    	/// References a package containing (directly or indirectly) metaclasses that may be extended.
    	/// </summary>
    	containment set<PackageImport> MetamodelReference subsets Namespace.PackageImport;
    }

    /// <summary>
    /// A ConnectorEnd is an endpoint of a Connector, which attaches the Connector to a ConnectableElement.
    /// </summary>
    class ConnectorEnd : MultiplicityElement
    {
        /// <summary>
        /// The Connector of which the ConnectorEnd is the endpoint.
        /// </summary>
        Connector Connector subsets Element.Owner;
        /// <summary>
    	/// <summary>
    	/// A derived property referencing the corresponding end on the Association which types the Connector owing this ConnectorEnd, if any. It is derived by selecting the end at the same place in the ordering of Association ends as this ConnectorEnd.
    	/// </summary>
    	// spec:
    	//     result = (if connector.type = null 
    	//     then
    	//       null 
    	//     else
    	//       let index : Integer = connector.end->indexOf(self) in
    	//         connector.type.memberEnd->at(index)
    	//     endif)
    	derived Property DefiningEnd;
    	/// <summary>
    	/// Indicates the role of the internal structure of a Classifier with the Port to which the ConnectorEnd is attached.
    	/// </summary>
    	Property PartWithPort;
    	/// <summary>
    	/// The ConnectableElement attached at this ConnectorEnd. When an instance of the containing Classifier is created, a link may (depending on the multiplicities) be created to an instance of the Classifier that types this ConnectableElement.
    	/// </summary>
    	ConnectableElement Role;
    }

    /// <summary>
    /// A ConnectableElementTemplateParameter exposes a ConnectableElement as a formal parameter for a template.
    /// </summary>
    class ConnectableElementTemplateParameter : TemplateParameter
    {
    	/// <summary>
    	/// The ConnectableElement for this ConnectableElementTemplateParameter.
    	/// </summary>
    	ConnectableElement ParameteredElement redefines TemplateParameter.ParameteredElement;
    }

    /// <summary>
    /// A Collaboration describes a structure of collaborating elements (roles), each performing a specialized function, which collectively accomplish some desired functionality. 
    /// </summary>
    class Collaboration : StructuredClassifier, BehavioredClassifier
    {
    	/// <summary>
    	/// Represents the participants in the Collaboration.
    	/// </summary>
    	set<ConnectableElement> CollaborationRole subsets StructuredClassifier.Role;
    }

    /// <summary>
    /// A relationship from an extending UseCase to an extended UseCase that specifies how and when the behavior defined in the extending UseCase can be inserted into the behavior defined in the extended UseCase.
    /// </summary>
    class Extend : NamedElement, DirectedRelationship
    {
    	/// <summary>
    	/// References the condition that must hold when the first ExtensionPoint is reached for the extension to take place. If no constraint is associated with the Extend relationship, the extension is unconditional.
    	/// </summary>
    	containment Constraint Condition subsets Element.OwnedElement;
    	/// <summary>
    	/// The UseCase that is being extended.
    	/// </summary>
    	UseCase ExtendedCase subsets DirectedRelationship.Target;
    	/// <summary>
    	/// The UseCase that represents the extension and owns the Extend relationship.
    	/// </summary>
    	UseCase Extension subsets DirectedRelationship.Source, NamedElement.Namespace;
    	/// <summary>
    	/// An ordered list of ExtensionPoints belonging to the extended UseCase, specifying where the respective behavioral fragments of the extending UseCase are to be inserted. The first fragment in the extending UseCase is associated with the first extension point in the list, the second fragment with the second point, and so on. Note that, in most practical cases, the extending UseCase has just a single behavior fragment, so that the list of ExtensionPoints is trivial.
    	/// </summary>
    	list<ExtensionPoint> ExtensionLocation;
    }

    /// <summary>
    /// An ExtensionPoint identifies a point in the behavior of a UseCase where that behavior can be extended by the behavior of some other (extending) UseCase, as specified by an Extend relationship.
    /// </summary>
    class ExtensionPoint : RedefinableElement
    {
    	/// <summary>
    	/// The UseCase that owns this ExtensionPoint.
    	/// </summary>
    	UseCase UseCase subsets NamedElement.Namespace;
    }

    /// <summary>
    /// An Include relationship specifies that a UseCase contains the behavior defined in another UseCase.
    /// </summary>
    class Include : DirectedRelationship, NamedElement
    {
    	/// <summary>
    	/// The UseCase that is to be included.
    	/// </summary>
    	UseCase Addition subsets DirectedRelationship.Target;
    	/// <summary>
    	/// The UseCase which includes the addition and owns the Include relationship.
    	/// </summary>
    	UseCase IncludingCase subsets DirectedRelationship.Source, NamedElement.Namespace;
    }

    /// <summary>
    /// An OpaqueExpression is a ValueSpecification that specifies the computation of a collection of values either in terms of a UML Behavior or based on a textual statement in a language other than UML
    /// </summary>
    class OpaqueExpression : ValueSpecification
    {
    	/// <summary>
    	/// Specifies the behavior of the OpaqueExpression as a UML Behavior.
    	/// </summary>
    	Behavior Behavior;
    	/// <summary>
    	/// A textual definition of the behavior of the OpaqueExpression, possibly in multiple languages.
    	/// </summary>
    	multi_list<string> Body;
    	/// <summary>
    	/// Specifies the languages used to express the textual bodies of the OpaqueExpression.  Languages are matched to body Strings by order. The interpretation of the body depends on the languages. If the languages are unspecified, they may be implicit from the expression body or the context.
    	/// </summary>
    	list<string> Language;
    	/// <summary>
    	/// If an OpaqueExpression is specified using a UML Behavior, then this refers to the single required return Parameter of that Behavior. When the Behavior completes execution, the values on this Parameter give the result of evaluating the OpaqueExpression.
    	/// </summary>
    	// spec:
    	//     result = (if behavior = null then
    	//     	null
    	//     else
    	//     	behavior.ownedParameter->first()
    	//     endif)
    	derived Parameter Result;
    	/// <summary>
    	/// The query isIntegral() tells whether an expression is intended to produce an Integer.
    	/// </summary>
    	// spec:
    	//     result = (false)
    	readonly bool IsIntegral();
    	/// <summary>
    	/// The query isNonNegative() tells whether an integer expression has a non-negative value.
    	/// </summary>
    	// pre:
    	//     self.isIntegral()
    	// spec:
    	//     result = (false)
    	readonly bool IsNonNegative();
    	/// <summary>
    	/// The query isPositive() tells whether an integer expression has a positive value.
    	/// </summary>
    	// spec:
    	//     result = (false)
    	// pre:
    	//     self.isIntegral()
    	readonly bool IsPositive();
    	/// <summary>
    	/// The query value() gives an integer value for an expression intended to produce one.
    	/// </summary>
    	// pre:
    	//     self.isIntegral()
    	// spec:
    	//     result = (0)
    	readonly int Value();
    }

    /// <summary>
    /// A ProtocolStateMachine is always defined in the context of a Classifier. It specifies which BehavioralFeatures of the Classifier can be called in which State and under which conditions, thus specifying the allowed invocation sequences on the Classifier&apos;s BehavioralFeatures. A ProtocolStateMachine specifies the possible and permitted Transitions on the instances of its context Classifier, together with the BehavioralFeatures that carry the Transitions. In this manner, an instance lifecycle can be specified for a Classifier, by defining the order in which the BehavioralFeatures can be activated and the States through which an instance progresses during its existence.
    /// </summary>
    class ProtocolStateMachine : StateMachine
    {
    	/// <summary>
    	/// Conformance between ProtocolStateMachine 
    	/// </summary>
    	containment set<ProtocolConformance> Conformance subsets Element.OwnedElement;
    }

    /// <summary>
    /// StateMachines can be used to express event-driven behaviors of parts of a system. Behavior is modeled as a traversal of a graph of Vertices interconnected by one or more joined Transition arcs that are triggered by the dispatching of successive Event occurrences. During this traversal, the StateMachine may execute a sequence of Behaviors associated with various elements of the StateMachine.
    /// </summary>
    class StateMachine : Behavior
    {
    	/// <summary>
    	/// The connection points defined for this StateMachine. They represent the interface of the StateMachine when used as part of submachine State
    	/// </summary>
    	containment set<Pseudostate> ConnectionPoint subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The StateMachines of which this is an extension.
    	/// </summary>
    	set<StateMachine> ExtendedStateMachine redefines Behavior.RedefinedBehavior;
    	/// <summary>
    	/// The Regions owned directly by the StateMachine.
    	/// </summary>
    	containment set<Region> Region subsets Namespace.OwnedMember;
    	/// <summary>
    	/// References the submachine(s) in case of a submachine State. Multiple machines are referenced in case of a concurrent State.
    	/// </summary>
    	set<State> SubmachineState;
    	/// <summary>
    	/// The operation LCA(s1,s2) returns the Region that is the least common ancestor of Vertices s1 and s2, based on the StateMachine containment hierarchy.
    	/// </summary>
    	// spec:
    	//     result = (if ancestor(s1, s2) then 
    	//         s2.container
    	//     else
    	//     	if ancestor(s2, s1) then
    	//     	    s1.container 
    	//     	else 
    	//     	    LCA(s1.container.state, s2.container.state)
    	//     	endif
    	//     endif)
    	readonly Region LCA(Vertex s1, Vertex s2);
    	/// <summary>
    	/// The query ancestor(s1, s2) checks whether Vertex s2 is an ancestor of Vertex s1.
    	/// </summary>
    	// spec:
    	//     result = (if (s2 = s1) then 
    	//     	true 
    	//     else 
    	//     	if s1.container.stateMachine->notEmpty() then 
    	//     	    true
    	//     	else 
    	//     	    if s2.container.stateMachine->notEmpty() then 
    	//     	        false
    	//     	    else
    	//     	        ancestor(s1, s2.container.state)
    	//     	     endif
    	//     	 endif
    	//     endif  )
    	readonly bool Ancestor(Vertex s1, Vertex s2);
    	/// <summary>
    	/// The query isConsistentWith specifies that a StateMachine can be redefined by any other StateMachine for which the redefinition context is valid (see the isRedefinitionContextValid operation). Note that consistency requirements for the redefinition of Regions and connectionPoint Pseudostates owned by a StateMachine are specified by the isConsistentWith and isRedefinitionContextValid operations for Region and Vertex (and its subclass Pseudostate).
    	/// </summary>
    	// spec:
    	//     result = true
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    	/// <summary>
    	/// The query isRedefinitionContextValid specifies whether the redefinition context of a StateMachine is properly related to the redefinition contexts of a StateMachine it redefines. The requirement is that the context BehavioredClassifier of a redefining StateMachine must specialize the context Classifier of the redefined StateMachine. If the redefining StateMachine does not have a context BehavioredClassifier, then then the redefining StateMachine also must not have a context BehavioredClassifier but must, instead, specialize the redefining StateMachine.
    	/// </summary>
    	// spec:
    	//     result = (redefinedElement.oclIsKindOf(StateMachine) and
    	//       let parentContext : BehavioredClassifier =
    	//         redefinedElement.oclAsType(StateMachine).context in
    	//       if context = null then
    	//         parentContext = null and self.allParents()→includes(redefinedElement)
    	//       else
    	//         parentContext <> null and context.allParents()->includes(parentContext)
    	//       endif)
    	readonly bool IsRedefinitionContextValid(RedefinableElement redefinedElement);
    	/// <summary>
    	/// This utility funciton is like the LCA, except that it returns the nearest composite State that contains both input Vertices.
    	/// </summary>
    	// spec:
    	//     result = (if v2.oclIsTypeOf(State) and ancestor(v1, v2) then
    	//     	v2.oclAsType(State)
    	//     else if v1.oclIsTypeOf(State) and ancestor(v2, v1) then
    	//     	v1.oclAsType(State)
    	//     else if (v1.container.state->isEmpty() or v2.container.state->isEmpty()) then 
    	//     	null.oclAsType(State)
    	//     else LCAState(v1.container.state, v2.container.state)
    	//     endif endif endif)
    	readonly State LCAState(Vertex v1, Vertex v2);
    }

    /// <summary>
    /// A TemplateParameterSubstitution relates the actual parameter to a formal TemplateParameter as part of a template binding.
    /// </summary>
    class TemplateParameterSubstitution : Element
    {
    	/// <summary>
    	/// The ParameterableElement that is the actual parameter for this TemplateParameterSubstitution.
    	/// </summary>
    	ParameterableElement Actual;
    	/// <summary>
    	/// The formal TemplateParameter that is associated with this TemplateParameterSubstitution.
    	/// </summary>
    	TemplateParameter Formal;
    	/// <summary>
    	/// The ParameterableElement that is owned by this TemplateParameterSubstitution as its actual parameter.
    	/// </summary>
    	containment ParameterableElement OwnedActual subsets Element.OwnedElement, TemplateParameterSubstitution.Actual;
    	/// <summary>
    	/// The TemplateBinding that owns this TemplateParameterSubstitution.
    	/// </summary>
    	TemplateBinding TemplateBinding subsets Element.Owner;
    }

    /// <summary>
    /// An extension end is used to tie an extension to a stereotype when extending a metaclass.
    /// The default multiplicity of an extension end is 0..1.
    /// </summary>
    class ExtensionEnd : Property
    {
    	/// <summary>
    	/// This redefinition changes the default multiplicity of association ends, since model elements are usually extended by 0 or 1 instance of the extension stereotype.
    	/// </summary>
    	derived int Lower redefines MultiplicityElement.Lower;
    	/// <summary>
    	/// References the type of the ExtensionEnd. Note that this association restricts the possible types of an ExtensionEnd to only be Stereotypes.
    	/// </summary>
    	Stereotype Type redefines TypedElement.Type;
    	/// <summary>
    	/// The query lowerBound() returns the lower bound of the multiplicity as an Integer. This is a redefinition of the default lower bound, which normally, for MultiplicityElements, evaluates to 1 if empty.
    	/// </summary>
    	// spec:
    	//     result = (if lowerValue=null then 0 else lowerValue.integerValue() endif)
    	readonly int LowerBound();
    }

    /// <summary>
    /// A Signal is a specification of a kind of communication between objects in which a reaction is asynchronously triggered in the receiver without a reply.
    /// </summary>
    class Signal : Classifier
    {
    	/// <summary>
    	/// The attributes owned by the Signal.
    	/// </summary>
    	containment list<Property> OwnedAttribute subsets Classifier.Attribute, Namespace.OwnedMember;
    }

    /// <summary>
    /// A deployment specification specifies a set of properties that determine execution parameters of a component artifact that is deployed on a node. A deployment specification can be aimed at a specific type of container. An artifact that reifies or implements deployment specification properties is a deployment descriptor.
    /// </summary>
    class DeploymentSpecification : Artifact
    {
    	/// <summary>
    	/// The deployment with which the DeploymentSpecification is associated.
    	/// </summary>
    	Deployment Deployment subsets Element.Owner;
    	/// <summary>
    	/// The location where an Artifact is deployed onto a Node. This is typically a &apos;directory&apos; or &apos;memory address.&apos;
    	/// </summary>
    	string DeploymentLocation;
    	/// <summary>
    	/// The location where a component Artifact executes. This may be a local or remote location.
    	/// </summary>
    	string ExecutionLocation;
    }

    /// <summary>
    /// An artifact is the specification of a physical piece of information that is used or produced by a software development process, or by deployment and operation of a system. Examples of artifacts include model files, source files, scripts, and binary executable files, a table in a database system, a development deliverable, or a word-processing document, a mail message.
    /// An artifact is the source of a deployment to a node.
    /// </summary>
    class Artifact : Classifier, DeployedArtifact
    {
    	/// <summary>
    	/// A concrete name that is used to refer to the Artifact in a physical context. Example: file system name, universal resource locator.
    	/// </summary>
    	string FileName;
    	/// <summary>
    	/// The set of model elements that are manifested in the Artifact. That is, these model elements are utilized in the construction (or generation) of the artifact.
    	/// </summary>
    	containment set<Manifestation> Manifestation subsets Element.OwnedElement, NamedElement.ClientDependency;
    	/// <summary>
    	/// The Artifacts that are defined (nested) within the Artifact. The association is a specialization of the ownedMember association from Namespace to NamedElement.
    	/// </summary>
    	containment set<Artifact> NestedArtifact subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The attributes or association ends defined for the Artifact. The association is a specialization of the ownedMember association.
    	/// </summary>
    	containment list<Property> OwnedAttribute subsets Classifier.Attribute, Namespace.OwnedMember;
    	/// <summary>
    	/// The Operations defined for the Artifact. The association is a specialization of the ownedMember association.
    	/// </summary>
    	containment list<Operation> OwnedOperation subsets Classifier.Feature, Namespace.OwnedMember;
    }

    /// <summary>
    /// Physical definition of a graphical image.
    /// </summary>
    class Image : Element
    {
    	/// <summary>
    	/// This contains the serialization of the image according to the format. The value could represent a bitmap, image such as a GIF file, or drawing &apos;instructions&apos; using a standard such as Scalable Vector Graphic (SVG) (which is XML based).
    	/// </summary>
    	string Content;
    	/// <summary>
    	/// This indicates the format of the content, which is how the string content should be interpreted. The following values are reserved: SVG, GIF, PNG, JPG, WMF, EMF, BMP. In addition the prefix &apos;MIME: &apos; is also reserved. This option can be used as an alternative to express the reserved values above, for example &quot;SVG&quot; could instead be expressed as &quot;MIME: image/svg+xml&quot;.
    	/// </summary>
    	string Format;
    	/// <summary>
    	/// This contains a location that can be used by a tool to locate the image as an alternative to embedding it in the stereotype.
    	/// </summary>
    	string Location;
    }

    /// <summary>
    /// A ProtocolStateMachine can be redefined into a more specific ProtocolStateMachine or into behavioral StateMachine. ProtocolConformance declares that the specific ProtocolStateMachine specifies a protocol that conforms to the general ProtocolStateMachine or that the specific behavioral StateMachine abides by the protocol of the general ProtocolStateMachine.
    /// </summary>
    class ProtocolConformance : DirectedRelationship
    {
    	/// <summary>
    	/// Specifies the ProtocolStateMachine to which the specific ProtocolStateMachine conforms.
    	/// </summary>
    	ProtocolStateMachine GeneralMachine subsets DirectedRelationship.Target;
    	/// <summary>
    	/// Specifies the ProtocolStateMachine which conforms to the general ProtocolStateMachine.
    	/// </summary>
    	ProtocolStateMachine SpecificMachine subsets DirectedRelationship.Source, Element.Owner;
    }

    /// <summary>
    /// A Pseudostate is an abstraction that encompasses different types of transient Vertices in the StateMachine graph. A StateMachine instance never comes to rest in a Pseudostate, instead, it will exit and enter the Pseudostate within a single run-to-completion step.
    /// </summary>
    class Pseudostate : Vertex
    {
    	/// <summary>
    	/// Determines the precise type of the Pseudostate and can be one of: entryPoint, exitPoint, initial, deepHistory, shallowHistory, join, fork, junction, terminate or choice.
    	/// </summary>
    	PseudostateKind Kind;
    	/// <summary>
    	/// The State that owns this Pseudostate and in which it appears.
    	/// </summary>
    	State State subsets NamedElement.Namespace;
    	/// <summary>
    	/// The StateMachine in which this Pseudostate is defined. This only applies to Pseudostates of the kind entryPoint or exitPoint.
    	/// </summary>
    	StateMachine StateMachine subsets NamedElement.Namespace;
    	/// <summary>
    	/// The query isConsistentWith() specifies a Pseudostate can only be redefined by a Pseudostate of the same kind.
    	/// </summary>
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	// spec:
    	//     result = (redefiningElement.oclIsKindOf(Pseudostate) and
    	//     redefiningElement.oclAsType(Pseudostate).kind = kind)
    	bool IsConsistentWith(RedefinableElement redefiningElement);
    }

    /// <summary>
    /// A Vertex is an abstraction of a node in a StateMachine graph. It can be the source or destination of any number of Transitions.
    /// </summary>
    abstract class Vertex : NamedElement, RedefinableElement
    {
    	/// <summary>
    	/// The Region that contains this Vertex.
    	/// </summary>
    	Region Container subsets NamedElement.Namespace;
    	/// <summary>
    	/// Specifies the Transitions entering this Vertex.
    	/// </summary>
    	// spec:
    	//     result = (Transition.allInstances()->select(target=self))
    	derived set<Transition> Incoming;
    	/// <summary>
    	/// Specifies the Transitions departing from this Vertex.
    	/// </summary>
    	// spec:
    	//     result = (Transition.allInstances()->select(source=self))
    	derived set<Transition> Outgoing;
    	/// <summary>
    	/// References the Classifier in which context this element may be redefined.
    	/// </summary>
    	// spec:
    	//     result = containingStateMachine()
    	derived Classifier RedefinitionContext redefines RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// The Vertex of which this Vertex is a redefinition.
    	/// </summary>
    	Vertex RedefinedVertex subsets RedefinableElement.RedefinedElement;
    	/// <summary>
    	/// The operation containingStateMachine() returns the StateMachine in which this Vertex is defined.
    	/// </summary>
    	// spec:
    	//     result = (if container <> null
    	//     then
    	//     -- the container is a region
    	//        container.containingStateMachine()
    	//     else 
    	//        if (self.oclIsKindOf(Pseudostate)) and ((self.oclAsType(Pseudostate).kind = PseudostateKind::entryPoint) or (self.oclAsType(Pseudostate).kind = PseudostateKind::exitPoint)) then
    	//           self.oclAsType(Pseudostate).stateMachine
    	//        else 
    	//           if (self.oclIsKindOf(ConnectionPointReference)) then
    	//               self.oclAsType(ConnectionPointReference).state.containingStateMachine() -- no other valid cases possible
    	//           else 
    	//               null
    	//           endif
    	//        endif
    	//     endif
    	//     )
    	readonly StateMachine ContainingStateMachine();
    	/// <summary>
    	/// This utility operation returns true if the Vertex is contained in the State s (input argument).
    	/// </summary>
    	// spec:
    	//     result = (if not s.isComposite() or container->isEmpty() then
    	//     	false
    	//     else
    	//     	if container.state = s then 
    	//     		true
    	//     	else
    	//     		container.state.isContainedInState(s)
    	//     	endif
    	//     endif)
    	readonly bool IsContainedInState(State s);
    	/// <summary>
    	/// This utility query returns true if the Vertex is contained in the Region r (input argument).
    	/// </summary>
    	// spec:
    	//     result = (if (container = r) then
    	//     	true
    	//     else
    	//     	if (r.state->isEmpty()) then
    	//     		false
    	//     	else
    	//     		container.state.isContainedInRegion(r)
    	//     	endif
    	//     endif)
    	readonly bool IsContainedInRegion(Region r);
    	/// <summary>
    	/// The query isRedefinitionContextValid specifies that the redefinition context of a redefining Vertex is properly related to the redefinition context of the redefined Vertex if the owner of the redefining Vertex is a redefinition of the owner of the redefined Vertex. Note that the owner of a Vertex may be a Region, a StateMachine (for a connectionPoint Pseudostate), or a State (for a connectionPoint Pseudostate or a connection ConnectionPointReference), all of which are RedefinableElements.
    	/// </summary>
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	// spec:
    	//     result = (redefinedElement.oclIsKindOf(Vertex) and
    	//       owner.oclAsType(RedefinableElement).redefinedElement->includes(redefinedElement.owner))
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    }

    /// <summary>
    /// A Region is a top-level part of a StateMachine or a composite State, that serves as a container for the Vertices and Transitions of the StateMachine. A StateMachine or composite State may contain multiple Regions representing behaviors that may occur in parallel.
    /// </summary>
    class Region : Namespace, RedefinableElement
    {
    	/// <summary>
    	/// The region of which this region is an extension.
    	/// </summary>
    	Region ExtendedRegion subsets RedefinableElement.RedefinedElement;
    	/// <summary>
    	/// References the Classifier in which context this element may be redefined.
    	/// </summary>
    	// spec:
    	//     result = containingStateMachine()
    	derived Classifier RedefinitionContext redefines RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// The State that owns the Region. If a Region is owned by a State, then it cannot also be owned by a StateMachine.
    	/// </summary>
    	State State subsets NamedElement.Namespace;
    	/// <summary>
    	/// The StateMachine that owns the Region. If a Region is owned by a StateMachine, then it cannot also be owned by a State.
    	/// </summary>
    	StateMachine StateMachine subsets NamedElement.Namespace;
    	/// <summary>
    	/// The set of Vertices that are owned by this Region.
    	/// </summary>
    	containment set<Vertex> Subvertex subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The set of Transitions owned by the Region.
    	/// </summary>
    	containment set<Transition> Transition subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The operation belongsToPSM () checks if the Region belongs to a ProtocolStateMachine.
    	/// </summary>
    	// spec:
    	//     result = (if  stateMachine <> null 
    	//     then
    	//       stateMachine.oclIsKindOf(ProtocolStateMachine)
    	//     else 
    	//       state <> null  implies  state.container.belongsToPSM()
    	//     endif )
    	readonly bool BelongsToPSM();
    	/// <summary>
    	/// The operation containingStateMachine() returns the StateMachine in which this Region is defined.
    	/// </summary>
    	// spec:
    	//     result = (if stateMachine = null 
    	//     then
    	//       state.containingStateMachine()
    	//     else
    	//       stateMachine
    	//     endif)
    	readonly StateMachine ContainingStateMachine();
    	/// <summary>
    	/// The query isConsistentWith specifies that a Region can be redefined by any Region for which the redefinition context is valid (see the isRedefinitionContextValid operation). Note that consistency requirements for the redefinition of Vertices and Transitions within a redefining Region are specified by the isConsistentWith and isRedefinitionContextValid operations for Vertex (and its subclasses) and Transition.
    	/// </summary>
    	// spec:
    	//     result = true
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    	/// <summary>
    	/// The query isRedefinitionContextValid() specifies whether the redefinition contexts of a Region are properly related to the redefinition contexts of the specified Region to allow this element to redefine the other. The containing StateMachine or State of a redefining Region must Redefine the containing StateMachine or State of the redefined Region.
    	/// </summary>
    	// spec:
    	//     result = (if redefinedElement.oclIsKindOf(Region) then
    	//       let redefinedRegion : Region = redefinedElement.oclAsType(Region) in
    	//         if stateMachine->isEmpty() then
    	//         -- the Region is owned by a State
    	//           (state.redefinedState->notEmpty() and state.redefinedState.region->includes(redefinedRegion))
    	//         else -- the region is owned by a StateMachine
    	//           (stateMachine.extendedStateMachine->notEmpty() and
    	//             stateMachine.extendedStateMachine->exists(sm : StateMachine |
    	//               sm.region->includes(redefinedRegion)))
    	//         endif
    	//     else
    	//       false
    	//     endif)
    	readonly bool IsRedefinitionContextValid(RedefinableElement redefinedElement);
    }

    /// <summary>
    /// A State models a situation during which some (usually implicit) invariant condition holds.
    /// </summary>
    class State : Namespace, Vertex
    {
    	/// <summary>
    	/// The entry and exit connection points used in conjunction with this (submachine) State, i.e., as targets and sources, respectively, in the Region with the submachine State. A connection point reference references the corresponding definition of a connection point Pseudostate in the StateMachine referenced by the submachine State.
    	/// </summary>
    	containment set<ConnectionPointReference> Connection subsets Namespace.OwnedMember;
    	/// <summary>
    	/// The entry and exit Pseudostates of a composite State. These can only be entry or exit Pseudostates, and they must have different names. They can only be defined for composite States.
    	/// </summary>
    	containment set<Pseudostate> ConnectionPoint subsets Namespace.OwnedMember;
    	/// <summary>
    	/// A list of Triggers that are candidates to be retained by the StateMachine if they trigger no Transitions out of the State (not consumed). A deferred Trigger is retained until the StateMachine reaches a State configuration where it is no longer deferred.
    	/// </summary>
    	containment set<Trigger> DeferrableTrigger subsets Element.OwnedElement;
    	/// <summary>
    	/// An optional Behavior that is executed while being in the State. The execution starts when this State is entered, and ceases either by itself when done, or when the State is exited, whichever comes first.
    	/// </summary>
    	containment Behavior DoActivity subsets Element.OwnedElement;
    	/// <summary>
    	/// An optional Behavior that is executed whenever this State is entered regardless of the Transition taken to reach the State. If defined, entry Behaviors are always executed to completion prior to any internal Behavior or Transitions performed within the State.
    	/// </summary>
    	containment Behavior Entry subsets Element.OwnedElement;
    	/// <summary>
    	/// An optional Behavior that is executed whenever this State is exited regardless of which Transition was taken out of the State. If defined, exit Behaviors are always executed to completion only after all internal and transition Behaviors have completed execution.
    	/// </summary>
    	containment Behavior Exit subsets Element.OwnedElement;
    	/// <summary>
    	/// A state with isComposite=true is said to be a composite State. A composite State is a State that contains at least one Region.
    	/// </summary>
    	// spec:
    	//     result = (region->notEmpty())
    	derived bool IsComposite;
    	/// <summary>
    	/// A State with isOrthogonal=true is said to be an orthogonal composite State An orthogonal composite State contains two or more Regions.
    	/// </summary>
    	// spec:
    	//     result = (region->size () > 1)
    	derived bool IsOrthogonal;
    	/// <summary>
    	/// A State with isSimple=true is said to be a simple State A simple State does not have any Regions and it does not refer to any submachine StateMachine.
    	/// </summary>
    	// spec:
    	//     result = ((region->isEmpty()) and not isSubmachineState())
    	derived bool IsSimple;
    	/// <summary>
    	/// A State with isSubmachineState=true is said to be a submachine State Such a State refers to another StateMachine(submachine).
    	/// </summary>
    	// spec:
    	//     result = (submachine <> null)
    	derived bool IsSubmachineState;
    	/// <summary>
    	/// The Regions owned directly by the State.
    	/// </summary>
    	containment set<Region> Region subsets Namespace.OwnedMember;
    	/// <summary>
    	/// Specifies conditions that are always true when this State is the current State. In ProtocolStateMachines state invariants are additional conditions to the preconditions of the outgoing Transitions, and to the postcondition of the incoming Transitions.
    	/// </summary>
    	containment Constraint StateInvariant subsets Namespace.OwnedRule;
    	/// <summary>
    	/// The StateMachine that is to be inserted in place of the (submachine) State.
    	/// </summary>
    	StateMachine Submachine;
    	/// <summary>
    	/// The query containingStateMachine() returns the StateMachine that contains the State either directly or transitively.
    	/// </summary>
    	// spec:
    	//     result = (container.containingStateMachine())
    	readonly StateMachine ContainingStateMachine();
    	/// <summary>
    	/// The query isConsistentWith specifies that a non-final State can only be redefined by a non-final State (this is overridden by FinalState to allow a FinalState to be redefined by a FinalState) and, if the redefined State is a submachine State, then the redefining State must be a submachine state whose submachine is a redefinition of the submachine of the redefined State. Note that consistency requirements for the redefinition of Regions and connectionPoint Pseudostates within a composite State and connection ConnectionPoints of a submachine State are specified by the isConsistentWith and isRedefinitionContextValid operations for Region and Vertex (and its subclasses, Pseudostate and ConnectionPointReference).
    	/// </summary>
    	// spec:
    	//     result = (redefiningElement.oclIsTypeOf(State) and 
    	//       let redefiningState : State = redefiningElement.oclAsType(State) in
    	//         submachine <> null implies (redefiningState.submachine <> null and
    	//           redefiningState.submachine.extendedStateMachine->includes(submachine)))
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    }

    /// <summary>
    /// A manifestation is the concrete physical rendering of one or more model elements by an artifact.
    /// </summary>
    class Manifestation : Abstraction
    {
    	/// <summary>
    	/// The model element that is utilized in the manifestation in an Artifact.
    	/// </summary>
    	PackageableElement UtilizedElement subsets Dependency.Supplier;
    }

    /// <summary>
    /// A Transition represents an arc between exactly one source Vertex and exactly one Target vertex (the source and targets may be the same Vertex). It may form part of a compound transition, which takes the StateMachine from one steady State configuration to another, representing the full response of the StateMachine to an occurrence of an Event that triggered it.
    /// </summary>
    class Transition : Namespace, RedefinableElement
    {
    	/// <summary>
    	/// Designates the Region that owns this Transition.
    	/// </summary>
    	Region Container subsets NamedElement.Namespace;
    	/// <summary>
    	/// Specifies an optional behavior to be performed when the Transition fires.
    	/// </summary>
    	containment Behavior Effect subsets Element.OwnedElement;
    	/// <summary>
    	/// A guard is a Constraint that provides a fine-grained control over the firing of the Transition. The guard is evaluated when an Event occurrence is dispatched by the StateMachine. If the guard is true at that time, the Transition may be enabled, otherwise, it is disabled. Guards should be pure expressions without side effects. Guard expressions with side effects are ill formed.
    	/// </summary>
    	containment Constraint Guard subsets Namespace.OwnedRule;
    	/// <summary>
    	/// Indicates the precise type of the Transition.
    	/// </summary>
    	TransitionKind Kind;
    	/// <summary>
    	/// The Transition that is redefined by this Transition.
    	/// </summary>
    	Transition RedefinedTransition subsets RedefinableElement.RedefinedElement;
    	/// <summary>
    	/// References the Classifier in which context this element may be redefined.
    	/// </summary>
    	// spec:
    	//     result = containingStateMachine()
    	derived Classifier RedefinitionContext redefines RedefinableElement.RedefinitionContext;
    	/// <summary>
    	/// Designates the originating Vertex (State or Pseudostate) of the Transition.
    	/// </summary>
    	Vertex Source;
    	/// <summary>
    	/// Designates the target Vertex that is reached when the Transition is taken.
    	/// </summary>
    	Vertex Target;
    	/// <summary>
    	/// Specifies the Triggers that may fire the transition.
    	/// </summary>
    	containment set<Trigger> Trigger subsets Element.OwnedElement;
    	/// <summary>
    	/// The query containingStateMachine() returns the StateMachine that contains the Transition either directly or transitively.
    	/// </summary>
    	// spec:
    	//     result = (container.containingStateMachine())
    	readonly StateMachine ContainingStateMachine();
    	/// <summary>
    	/// The query isConsistentWith specifies that a redefining Transition is consistent with a redefined Transition provided that the source Vertex of the redefining Transition redefines the source Vertex of the redefined Transition.
    	/// </summary>
    	// spec:
    	//     result = (redefiningElement.oclIsKindOf(Transition) and
    	//       redefiningElement.oclAsType(Transition).source.redefinedTransition = source)
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	readonly bool IsConsistentWith(RedefinableElement redefiningElement);
    }

    /// <summary>
    /// A ConnectionPointReference represents a usage (as part of a submachine State) of an entry/exit point Pseudostate defined in the StateMachine referenced by the submachine State.
    /// </summary>
    class ConnectionPointReference : Vertex
    {
    	/// <summary>
    	/// The entryPoint Pseudostates corresponding to this connection point.
    	/// </summary>
    	set<Pseudostate> Entry;
    	/// <summary>
    	/// The exitPoints kind Pseudostates corresponding to this connection point.
    	/// </summary>
    	set<Pseudostate> Exit;
    	/// <summary>
    	/// The State in which the ConnectionPointReference is defined.
    	/// </summary>
    	State State subsets NamedElement.Namespace;
    	/// <summary>
    	/// The query isConsistentWith() specifies a ConnectionPointReference can only be redefined by a ConnectionPointReference.
    	/// </summary>
    	// pre:
    	//     redefiningElement.isRedefinitionContextValid(self)
    	// spec:
    	//     result = redefiningElement.oclIsKindOf(ConnectionPointReference)
    	bool IsConsistentWith(RedefinableElement redefiningElement);
    }

    /// <summary>
    /// A Trigger specifies a specific point  at which an Event occurrence may trigger an effect in a Behavior. A Trigger may be qualified by the Port on which the Event occurred.
    /// </summary>
    class Trigger : NamedElement
    {
    	/// <summary>
    	/// The Event that detected by the Trigger.
    	/// </summary>
    	Event Event;
    	/// <summary>
    	/// A optional Port of through which the given effect is detected.
    	/// </summary>
    	set<Port> Port;
    }

    /// <summary>
    /// An Event is the specification of some occurrence that may potentially trigger effects by an object.
    /// </summary>
    abstract class Event : PackageableElement
    {
    }

}
