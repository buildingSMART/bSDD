using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace bSDD.NET.Model.Objects {

  /// <remarks>
  ///  The Class IfdAPISession maintains state about each session.
  /// </remarks>
  /// <summary>
  ///  The Class IfdAPISession maintains state about each session.
  /// </summary>
  [SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdAPISession")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdAPISession")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdAPISession")]
  public partial class IfdAPISession : IfdBase {

    private bSDD.NET.Model.Objects.IfdUser _user;
    private DateTime? _timestamp;
    private bool _timestampSpecified;
    private bool _invalid;
    /// <summary>
    ///  the email
    /// </summary>
    [XmlElementAttribute(ElementName="user",Namespace="")]
    [SoapElementAttribute(ElementName="user")]
    public bSDD.NET.Model.Objects.IfdUser User {
      get {
        return this._user;
      }
      set {
        this._user = value;
      }
    }
    /// <summary>
    ///  the timestamp
    /// </summary>
    [XmlElementAttribute(ElementName="timestamp",Namespace="")]
    [SoapElementAttribute(ElementName="timestamp")]
    public DateTime Timestamp {
      get {
        return this._timestamp.GetValueOrDefault();
      }
      set {
        this._timestamp = value;
        this._timestampSpecified = true;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "Timestamp" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool TimestampSpecified {
      get {
        return this._timestampSpecified;
      }
      set {
        this._timestampSpecified = value;
      }
    }

    /// <summary>
    ///  true, if is invalid
    /// </summary>
    [XmlElementAttribute(ElementName="invalid",Namespace="")]
    [SoapElementAttribute(ElementName="invalid")]
    public bool Invalid {
      get {
        return this._invalid;
      }
      set {
        this._invalid = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdBase is the basic for all objects, and keeps the GUID.
  /// </remarks>
  /// <summary>
  ///  The Class IfdBase is the basic for all objects, and keeps the GUID.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdBase")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdBase")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdBase")]
  public partial class IfdBase {

    private string _guid;
    /// <summary>
    ///  the guid
    /// </summary>
    [XmlElementAttribute(ElementName="guid",Namespace="")]
    [SoapElementAttribute(ElementName="guid")]
    public string Guid {
      get {
        return this._guid;
      }
      set {
        this._guid = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdConBase.
  /// </remarks>
  /// <summary>
  ///  The Class IfdConBase.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdConBase")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdConBase")]
  public partial class IfdConBase : IfdBase {

    private string _versionId;
    private string _versionDate;
    private bSDD.NET.Model.Objects.IfdStatusEnum _status;
    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdName> _fullNames;
    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdDescription> _definitions;
    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdDescription> _comments;
    /// <summary>
    ///  the version id
    /// </summary>
    [XmlElementAttribute(ElementName="versionId",Namespace="")]
    [SoapElementAttribute(ElementName="versionId")]
    public string VersionId {
      get {
        return this._versionId;
      }
      set {
        this._versionId = value;
      }
    }
    /// <summary>
    ///  the version date
    /// </summary>
    [XmlElementAttribute(ElementName="versionDate",Namespace="")]
    [SoapElementAttribute(ElementName="versionDate")]
    public string VersionDate {
      get {
        return this._versionDate;
      }
      set {
        this._versionDate = value;
      }
    }
    /// <summary>
    ///  the status
    /// </summary>
    [XmlElementAttribute(ElementName="status",Namespace="")]
    [SoapElementAttribute(ElementName="status")]
    public IfdStatusEnum Status {
      get {
        return this._status;
      }
      set {
        this._status = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "Status" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool StatusSpecified {
      get {
        return this._status != bSDD.NET.Model.Objects.IfdStatusEnum.NULL;
      }
      set {
        if (!value) {
          this._status = bSDD.NET.Model.Objects.IfdStatusEnum.NULL;
        }
      }
    }
    /// <summary>
    ///  the full names
    /// </summary>
    [XmlElementAttribute(ElementName="fullNames",Namespace="")]
    [SoapElementAttribute(ElementName="fullNames")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdName> FullNames {
      get {
        return this._fullNames;
      }
      set {
        this._fullNames = value;
      }
    }
    /// <summary>
    ///  the definitions
    /// </summary>
    [XmlElementAttribute(ElementName="definitions",Namespace="")]
    [SoapElementAttribute(ElementName="definitions")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdDescription> Definitions {
      get {
        return this._definitions;
      }
      set {
        this._definitions = value;
      }
    }
    /// <summary>
    ///  the comments
    /// </summary>
    [XmlElementAttribute(ElementName="comments",Namespace="")]
    [SoapElementAttribute(ElementName="comments")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdDescription> Comments {
      get {
        return this._comments;
      }
      set {
        this._comments = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdConceptBase.
  /// </remarks>
  /// <summary>
  ///  The Class IfdConceptBase.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdConcept")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdConcept")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdConcept")]
  public partial class IfdConcept : IfdConBase {

    private IfdConceptTypeEnum _conceptType;
    private List<IfdName> _shortNames;
    private List<IfdName> _lexemes;
    private List<IfdIllustration> _illustrations;
    private IfdOrganization _owner;
    /// <summary>
    ///  the concept type
    /// </summary>
    [XmlElementAttribute(ElementName="conceptType",Namespace="")]
    [SoapElementAttribute(ElementName="conceptType")]
    public IfdConceptTypeEnum ConceptType {
      get {
        return this._conceptType;
      }
      set {
        this._conceptType = value;
      }
    }
    /// <summary>
    ///  Property for the XML serializer indicating whether the "ConceptType" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool ConceptTypeSpecified {
      get {
        return this._conceptType != IfdConceptTypeEnum.NULL;
      }
      set {
        if (!value) {
          this._conceptType = IfdConceptTypeEnum.NULL;
        }
      }
    }
    /// <summary>
    ///  the short names
    /// </summary>
    [XmlElementAttribute(ElementName="shortNames",Namespace="")]
    [SoapElementAttribute(ElementName="shortNames")]
    public List<IfdName> ShortNames {
      get {
        return this._shortNames;
      }
      set {
        this._shortNames = value;
      }
    }
    /// <summary>
    ///  the lexemes
    /// </summary>
    [XmlElementAttribute(ElementName="lexemes",Namespace="")]
    [SoapElementAttribute(ElementName="lexemes")]
    public List<IfdName> Lexemes {
      get {
        return this._lexemes;
      }
      set {
        this._lexemes = value;
      }
    }
    /// <summary>
    ///  the illustrations
    /// </summary>
    [XmlElementAttribute(ElementName="illustrations",Namespace="")]
    [SoapElementAttribute(ElementName="illustrations")]
    public List<IfdIllustration> Illustrations {
      get {
        return this._illustrations;
      }
      set {
        this._illustrations = value;
      }
    }
    /// <summary>
    ///  the owner
    /// </summary>
    [XmlElementAttribute(ElementName="owner",Namespace="")]
    [SoapElementAttribute(ElementName="owner")]
    public IfdOrganization Owner {
      get {
        return this._owner;
      }
      set {
        this._owner = value;
      }
    }
  }

  public partial class IfdConceptList
    {
        public List<IfdConcept> IfdConcept { get; set; }
    }

  /// <remarks>
  ///  The Class IfdConceptInRelationship.
  /// </remarks>
  /// <summary>
  ///  The Class IfdConceptInRelationship.
  /// </summary>
    [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdConceptInRelationship")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdConceptInRelationship")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdConceptInRelationship")]
  public partial class IfdConceptInRelationship : bSDD.NET.Model.Objects.IfdConcept {

    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdContext> _contexts;
    private bSDD.NET.Model.Objects.IfdRelationshipTypeEnum _relationshipType;
    /// <summary>
    ///  the contexts
    /// </summary>
    [XmlElementAttribute(ElementName="contexts",Namespace="")]
    [SoapElementAttribute(ElementName="contexts")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdContext> Contexts {
      get {
        return this._contexts;
      }
      set {
        this._contexts = value;
      }
    }
    /// <summary>
    ///  the relationship type
    /// </summary>
    [XmlElementAttribute(ElementName="relationshipType",Namespace="")]
    [SoapElementAttribute(ElementName="relationshipType")]
    public bSDD.NET.Model.Objects.IfdRelationshipTypeEnum RelationshipType {
      get {
        return this._relationshipType;
      }
      set {
        this._relationshipType = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "RelationshipType" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool RelationshipTypeSpecified {
      get {
        return this._relationshipType != bSDD.NET.Model.Objects.IfdRelationshipTypeEnum.NULL;
      }
      set {
        if (!value) {
          this._relationshipType = bSDD.NET.Model.Objects.IfdRelationshipTypeEnum.NULL;
        }
      }
    }
  }

  /// <remarks>
  ///  The list of available concept types in bsDD. Any concept added to bsDD should fit in one and only one of types.
  /// </remarks>
  /// <summary>
  ///  The list of available concept types in bsDD. Any concept added to bsDD should fit in one and only one of types.
  /// </summary>
  public enum IfdConceptTypeEnum {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [XmlEnumAttribute(Name="__NULL__")]
    [SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///   Actors are persons, professions, organizations. Actors are a roles external to the system,
    ///   and can be humans, groups of humans, machines, or devices.
    ///   As bsDD only speaks about types of things &quot;Picasso&quot; is not an actor concept in bsDD while &quot;painter&quot; is. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="ACTOR")]
    [SoapEnumAttribute(Name="ACTOR")]
    ACTOR,

    /// <summary>
    ///   Examples are any specific behavior, actions, organic human or machine processes,
    ///   chemical reactions and the action of natural forces &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="ACTIVITY")]
    [SoapEnumAttribute(Name="ACTIVITY")]
    ACTIVITY,

    /// <summary>
    ///   Anything serving as a representation of a person's thinking by means of symbolic marks.
    ///   A document is a writing that contains information. Any item (not necessarily on paper)
    ///   that can be indexed or cataloged. Examples of documents are standards, books, etc.
    ///   Please note that classifications have it's own type in bsDD &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="DOCUMENT")]
    [SoapEnumAttribute(Name="DOCUMENT")]
    DOCUMENT,

    /// <summary>
    ///   Properties or a property refers to the intrinsic or extrinsic qualities of objects.
    ///   It can be any characteristic or attribute of an object or substance. A property will always have
    ///   a measure in which the quantity can be expressed. A property is used to describe the quantity of
    ///   another concept. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="PROPERTY")]
    [SoapEnumAttribute(Name="PROPERTY")]
    PROPERTY,

    /// <summary>
    ///   A subject in bsDD is any physical or logical thing. Examples of subjects are doors, windows
    ///   roads, airports, software, control systems and lobby. A subject can be composed of other subjects
    ///   and will typically be described by its properties. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="SUBJECT")]
    [SoapEnumAttribute(Name="SUBJECT")]
    SUBJECT,

    /// <summary>
    ///   A unit of measurement: any division of quantity accepted as a standard of measurement or exchange.
    ///   Units can be monetary units, SI units, derived units, and concersion based units. Class is also
    ///   a unit. A unit is a physical quantity, with a value of one, which is used as a standard in terms
    ///   of which other quantities are expressed. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="UNIT")]
    [SoapEnumAttribute(Name="UNIT")]
    UNIT,

    /// <summary>
    ///   A measure is used to determine the extent or quantity of a concept.
    ///   It can be a specific extent or quantity of a substance, or a graduated scale
    ///   by which the dimensions or mass of an object or substance may be determined.
    ///   A measure in bsDD can have multiple values but only one unit.
    ///   Measures are named by including the unit in their name. Eg. positive length measure in mm &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="MEASURE")]
    [SoapEnumAttribute(Name="MEASURE")]
    MEASURE,

    /// <summary>
    ///   Values in bsDD are normally given where there exist typical values for a given property.
    ///   Examples are fire resistance classes, wood qualities, standard widths and heights for certain concepts.
    ///   Values are language dependent and could be strings as well as numbers. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="VALUE")]
    [SoapEnumAttribute(Name="VALUE")]
    VALUE,

    /// <summary>
    ///   A nest is a collection that only allows things of the same type to be members of the collection.
    ///   A nest can be collection of all other types of concepts but each nest can only contain concepts of
    ///   one particular type. A nest can also be a collection of other nests or bags. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="NEST")]
    [SoapEnumAttribute(Name="NEST")]
    NEST,

    /// <summary>
    ///   A bag is a collection that allows things of different types to be members of the collection.
    ///   A bag can be mixed collection of all other types of concepts also other nests and bags. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="BAG")]
    [SoapEnumAttribute(Name="BAG")]
    BAG,

    /// <summary>
    ///   Classifications are particular documents containing classification codes and structures.
    ///   A classification in bsDD is used to classify concepts of any of the other concept types.
    ///   The classification code is stored in the short-name of the classification concept, while the actual name
    ///   of the classification is stored in full-name. Classifications are modeled as structures by means of composition. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="CLASSIFICATION")]
    [SoapEnumAttribute(Name="CLASSIFICATION")]
    CLASSIFICATION,

    /// <summary>
    ///  This should never be used. Its provided for debug purposes. 
    /// </summary>
    [XmlEnumAttribute(Name="UNDEFINED")]
    [SoapEnumAttribute(Name="UNDEFINED")]
    UNDEFINED
  }

  /// <remarks>
  ///  The Class IfdContext.
  /// </remarks>
  /// <summary>
  ///  The Class IfdContext.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdContext")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdContext")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdContext")]
  public partial class IfdContext : bSDD.NET.Model.Objects.IfdConBase {

    private bool _restricted;
    private bool _readOnly;
    /// <summary>
    ///  true, if is restricted
    /// </summary>
    [XmlElementAttribute(ElementName="restricted",Namespace="")]
    [SoapElementAttribute(ElementName="restricted")]
    public bool Restricted {
      get {
        return this._restricted;
      }
      set {
        this._restricted = value;
      }
    }
    /// <summary>
    ///  true, if is read only
    /// </summary>
    [XmlElementAttribute(ElementName="readOnly",Namespace="")]
    [SoapElementAttribute(ElementName="readOnly")]
    public bool ReadOnly {
      get {
        return this._readOnly;
      }
      set {
        this._readOnly = value;
      }
    }
  }

  /// <remarks>
  ///  
  /// </remarks>
  /// <summary>
  ///  
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdCursor")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdCursor")]
  public partial class IfdCursor {

    private string _page;
    private string _previousPage;
    private string _nextPage;
    private int _pageSize;
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="page",Namespace="")]
    [SoapElementAttribute(ElementName="page")]
    public string Page {
      get {
        return this._page;
      }
      set {
        this._page = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="previousPage",Namespace="")]
    [SoapElementAttribute(ElementName="previousPage")]
    public string PreviousPage {
      get {
        return this._previousPage;
      }
      set {
        this._previousPage = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="nextPage",Namespace="")]
    [SoapElementAttribute(ElementName="nextPage")]
    public string NextPage {
      get {
        return this._nextPage;
      }
      set {
        this._nextPage = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="pageSize",Namespace="")]
    [SoapElementAttribute(ElementName="pageSize")]
    public int PageSize {
      get {
        return this._pageSize;
      }
      set {
        this._pageSize = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdDescription.
  /// </remarks>
  /// <summary>
  ///  The Class IfdDescription.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdDescription")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdDescription")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdDescription")]
  public partial class IfdDescription : bSDD.NET.Model.Objects.IfdLanguageRepresentationBase {

    private string _description;
    private bSDD.NET.Model.Objects.IfdDescriptionTypeEnum _descriptionType;
    /// <summary>
    ///  the description
    /// </summary>
    [XmlElementAttribute(ElementName="description",Namespace="")]
    [SoapElementAttribute(ElementName="description")]
    public string Description {
      get {
        return this._description;
      }
      set {
        this._description = value;
      }
    }
    /// <summary>
    ///  the description type
    /// </summary>
    [XmlElementAttribute(ElementName="descriptionType",Namespace="")]
    [SoapElementAttribute(ElementName="descriptionType")]
    public bSDD.NET.Model.Objects.IfdDescriptionTypeEnum DescriptionType {
      get {
        return this._descriptionType;
      }
      set {
        this._descriptionType = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "DescriptionType" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool DescriptionTypeSpecified {
      get {
        return this._descriptionType != bSDD.NET.Model.Objects.IfdDescriptionTypeEnum.NULL;
      }
      set {
        if (!value) {
          this._descriptionType = bSDD.NET.Model.Objects.IfdDescriptionTypeEnum.NULL;
        }
      }
    }
  }

  /// <remarks>
  ///  The Enum IfdDescriptionTypeEnum.
  /// </remarks>
  /// <summary>
  ///  The Enum IfdDescriptionTypeEnum.
  /// </summary>
  public enum IfdDescriptionTypeEnum {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [XmlEnumAttribute(Name="__NULL__")]
    [SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///  A description of type definition. Represents a definition of a concept. 
    /// </summary>
    [XmlEnumAttribute(Name="DEFINITION")]
    [SoapEnumAttribute(Name="DEFINITION")]
    DEFINITION,

    /// <summary>
    ///  A description of type comment. Represents a comment on a concept. 
    /// </summary>
    [XmlEnumAttribute(Name="COMMENT")]
    [SoapEnumAttribute(Name="COMMENT")]
    COMMENT,

    /// <summary>
    ///  The UNDEFINED. 
    /// </summary>
    [XmlEnumAttribute(Name="UNDEFINED")]
    [SoapEnumAttribute(Name="UNDEFINED")]
    UNDEFINED
  }

  /// <remarks>
  ///  The Class IfdError.
  /// </remarks>
  /// <summary>
  ///  The Class IfdError.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdError")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdError")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdError")]
  public partial class IfdError {

    private int _code;
    private string _message;
    /// <summary>
    ///  the code
    /// </summary>
    [XmlElementAttribute(ElementName="code",Namespace="")]
    [SoapElementAttribute(ElementName="code")]
    public int Code {
      get {
        return this._code;
      }
      set {
        this._code = value;
      }
    }
    /// <summary>
    ///  the message
    /// </summary>
    [XmlElementAttribute(ElementName="message",Namespace="")]
    [SoapElementAttribute(ElementName="message")]
    public string Message {
      get {
        return this._message;
      }
      set {
        this._message = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdIllustration.
  /// </remarks>
  /// <summary>
  ///  The Class IfdIllustration.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdIllustration")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdIllustration")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdIllustration")]
  public partial class IfdIllustration : bSDD.NET.Model.Objects.IfdLanguageRepresentationBase {

    private string _blobstoreKey;
    private string _illustrationUrl;
    /// <summary>
    ///  the blobstore key
    /// </summary>
    [XmlElementAttribute(ElementName="blobstoreKey",Namespace="")]
    [SoapElementAttribute(ElementName="blobstoreKey")]
    public string BlobstoreKey {
      get {
        return this._blobstoreKey;
      }
      set {
        this._blobstoreKey = value;
      }
    }
    /// <summary>
    ///  the illustrationUrl
    /// </summary>
    [XmlElementAttribute(ElementName="illustrationUrl",Namespace="")]
    [SoapElementAttribute(ElementName="illustrationUrl")]
    public string IllustrationUrl {
      get {
        return this._illustrationUrl;
      }
      set {
        this._illustrationUrl = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdLanguage.
  /// </remarks>
  /// <summary>
  ///  The Class IfdLanguage.
  /// </summary>
  [SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdLanguage")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdLanguage")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdLanguage")]
  public partial class IfdLanguage : IfdBase {

    private string _nameInEnglish;
    private string _nameInSelf;
    private string _languageCode;
    /// <summary>
    ///  the name in english
    /// </summary>
    [XmlElementAttribute(ElementName="nameInEnglish",Namespace="")]
    [SoapElementAttribute(ElementName="nameInEnglish")]
    public string NameInEnglish {
      get {
        return this._nameInEnglish;
      }
      set {
        this._nameInEnglish = value;
      }
    }
    /// <summary>
    ///  the name in self
    /// </summary>
    [XmlElementAttribute(ElementName="nameInSelf",Namespace="")]
    [SoapElementAttribute(ElementName="nameInSelf")]
    public string NameInSelf {
      get {
        return this._nameInSelf;
      }
      set {
        this._nameInSelf = value;
      }
    }
    /// <summary>
    ///  the language code
    /// </summary>
    [XmlElementAttribute(ElementName="languageCode",Namespace="")]
    [SoapElementAttribute(ElementName="languageCode")]
    public string LanguageCode {
      get {
        return this._languageCode;
      }
      set {
        this._languageCode = value;
      }
    }
  }
    public class IfdLanguageList
    {
        public List<IfdLanguage> IfdLanguage { get; set; }
    }

  /// <remarks>
  ///  The Class IfdLanguageRepresentationBase.
  /// </remarks>
  /// <summary>
  ///  The Class IfdLanguageRepresentationBase.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdLanguageRepresentationBase")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdLanguageRepresentationBase")]
  public partial class IfdLanguageRepresentationBase : IfdBase {

    private bSDD.NET.Model.Objects.IfdLanguage _language;
    private string _languageFamily;
    /// <summary>
    ///  the language
    /// </summary>
    [XmlElementAttribute(ElementName="language",Namespace="")]
    [SoapElementAttribute(ElementName="language")]
    public bSDD.NET.Model.Objects.IfdLanguage Language {
      get {
        return this._language;
      }
      set {
        this._language = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="languageFamily",Namespace="")]
    [SoapElementAttribute(ElementName="languageFamily")]
    public string LanguageFamily {
      get {
        return this._languageFamily;
      }
      set {
        this._languageFamily = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdLanguageRepresentationPreference.
  /// </remarks>
  /// <summary>
  ///  The Class IfdLanguageRepresentationPreference.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdLanguageRepresentationPreference")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdLanguageRepresentationPreference")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdLanguageRepresentationPreference")]
  public partial class IfdLanguageRepresentationPreference : IfdBase {

    private string _languageRepresentationGuid;
    private string _conceptGuid;
    private string _organizationGuid;
    /// <summary>
    ///  the language representation guid
    /// </summary>
    [XmlElementAttribute(ElementName="languageRepresentationGuid",Namespace="")]
    [SoapElementAttribute(ElementName="languageRepresentationGuid")]
    public string LanguageRepresentationGuid {
      get {
        return this._languageRepresentationGuid;
      }
      set {
        this._languageRepresentationGuid = value;
      }
    }
    /// <summary>
    ///  the concept guid
    /// </summary>
    [XmlElementAttribute(ElementName="conceptGuid",Namespace="")]
    [SoapElementAttribute(ElementName="conceptGuid")]
    public string ConceptGuid {
      get {
        return this._conceptGuid;
      }
      set {
        this._conceptGuid = value;
      }
    }
    /// <summary>
    ///  the organization guid
    /// </summary>
    [XmlElementAttribute(ElementName="organizationGuid",Namespace="")]
    [SoapElementAttribute(ElementName="organizationGuid")]
    public string OrganizationGuid {
      get {
        return this._organizationGuid;
      }
      set {
        this._organizationGuid = value;
      }
    }
  }
  /// <remarks>
  ///  The Class IfdName.
  /// </remarks>
  /// <summary>
  ///  The Class IfdName.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdName")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdName")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdName")]
  public partial class IfdName : IfdLanguageRepresentationBase {

    private string _name;
    private IfdNameTypeEnum _nameType;
    /// <summary>
    ///  the name
    /// </summary>
    [XmlElementAttribute(ElementName="name",Namespace="")]
    [SoapElementAttribute(ElementName="name")]
    public string Name {
      get {
        return this._name;
      }
      set {
        this._name = value;
      }
    }
    /// <summary>
    ///  the name type
    /// </summary>
    [XmlElementAttribute(ElementName="nameType",Namespace="")]
    [SoapElementAttribute(ElementName="nameType")]
    public IfdNameTypeEnum NameType {
      get {
        return this._nameType;
      }
      set {
        this._nameType = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "NameType" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool NameTypeSpecified {
      get {
        return this._nameType != IfdNameTypeEnum.NULL;
      }
      set {
        if (!value) {
          this._nameType = IfdNameTypeEnum.NULL;
        }
      }
    }
  }

    public class IfdNameList
    {
        public List<IfdName> IfdName { get; set; }
    }

    /// <remarks>
    ///  The Enum IfdNameTypeEnum.
    /// </remarks>
    /// <summary>
    ///  The Enum IfdNameTypeEnum.
    /// </summary>
    public enum IfdNameTypeEnum {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [XmlEnumAttribute(Name="__NULL__")]
    [SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///  A full name of a concept. The name without abbreviations. 
    /// </summary>
    [XmlEnumAttribute(Name="FULLNAME")]
    [SoapEnumAttribute(Name="FULLNAME")]
    FULLNAME,

    /// <summary>
    ///  A short, or abbreviated, version of a name of a concept. E.g &quot;mm&quot; is a short name for &quot;millimeter&quot;. 
    /// </summary>
    [XmlEnumAttribute(Name="SHORTNAME")]
    [SoapEnumAttribute(Name="SHORTNAME")]
    SHORTNAME,

    /// <summary>
    ///  The possible lexeme of a concept. This reprsents the stem of a word. 
    /// </summary>
    [XmlEnumAttribute(Name="LEXEME")]
    [SoapEnumAttribute(Name="LEXEME")]
    LEXEME,

    /// <summary>
    ///  The UNDEFINED. 
    /// </summary>
    [XmlEnumAttribute(Name="UNDEFINED")]
    [SoapEnumAttribute(Name="UNDEFINED")]
    UNDEFINED
  }

  /// <remarks>
  ///  The Class IfdOrganization.
  /// </remarks>
  /// <summary>
  ///  The Class IfdOrganization.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdOrganization")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdOrganization")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdOrganization")]
  public partial class IfdOrganization : IfdBase {

    private string _name;
    private string _URL;
    /// <summary>
    ///  the name
    /// </summary>
    [XmlElementAttribute(ElementName="name",Namespace="")]
    [SoapElementAttribute(ElementName="name")]
    public string Name {
      get {
        return this._name;
      }
      set {
        this._name = value;
      }
    }
    /// <summary>
    ///  the uRL
    /// </summary>
    [XmlElementAttribute(ElementName="URL",Namespace="")]
    [SoapElementAttribute(ElementName="URL")]
    public string URL {
      get {
        return this._URL;
      }
      set {
        this._URL = value;
      }
    }
  }

  /// <remarks>
  ///  
  /// </remarks>
  /// <summary>
  ///  
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdPageList")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdPageList")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdPageList")]
  public partial class IfdPageList {

    private System.Collections.Generic.List<object> _list;
    private bSDD.NET.Model.Objects.IfdCursor _cursor;
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="list",Namespace="")]
    [SoapElementAttribute(ElementName="list")]
    public System.Collections.Generic.List<object> List {
      get {
        return this._list;
      }
      set {
        this._list = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="cursor",Namespace="")]
    [SoapElementAttribute(ElementName="cursor")]
    public bSDD.NET.Model.Objects.IfdCursor Cursor {
      get {
        return this._cursor;
      }
      set {
        this._cursor = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdProperty.
  /// </remarks>
  /// <summary>
  ///  The Class IfdProperty.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdProperty")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdProperty")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdProperty")]
  public partial class IfdProperty : bSDD.NET.Model.Objects.IfdConceptInRelationship {

    private bSDD.NET.Model.Objects.IfdConcept _measure;
    private bSDD.NET.Model.Objects.IfdConcept _unit;
    /// <summary>
    ///  the measure
    /// </summary>
    [XmlElementAttribute(ElementName="measure",Namespace="")]
    [SoapElementAttribute(ElementName="measure")]
    public bSDD.NET.Model.Objects.IfdConcept Measure {
      get {
        return this._measure;
      }
      set {
        this._measure = value;
      }
    }
    /// <summary>
    ///  the unit
    /// </summary>
    [XmlElementAttribute(ElementName="unit",Namespace="")]
    [SoapElementAttribute(ElementName="unit")]
    public bSDD.NET.Model.Objects.IfdConcept Unit {
      get {
        return this._unit;
      }
      set {
        this._unit = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdPropertyWithValues.
  /// </remarks>
  /// <summary>
  ///  The Class IfdPropertyWithValues.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdPropertyWithValues")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdPropertyWithValues")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdPropertyWithValues")]
  public partial class IfdPropertyWithValues : bSDD.NET.Model.Objects.IfdProperty {

    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdValueRolePair> _valueRolePairs;
    /// <summary>
    ///  the value role pairs
    /// </summary>
    [XmlElementAttribute(ElementName="valueRolePairs",Namespace="")]
    [SoapElementAttribute(ElementName="valueRolePairs")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdValueRolePair> ValueRolePairs {
      get {
        return this._valueRolePairs;
      }
      set {
        this._valueRolePairs = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdConceptRelationship.
  /// </remarks>
  /// <summary>
  ///  The Class IfdConceptRelationship.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdRelationship")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdRelationship")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdRelationship")]
  public partial class IfdRelationship : IfdBase {

    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdContext> _contexts;
    private bSDD.NET.Model.Objects.IfdRelationshipTypeEnum _relationshipType;
    private bSDD.NET.Model.Objects.IfdConcept _parent;
    private bSDD.NET.Model.Objects.IfdConcept _child;
    /// <summary>
    ///  the context key
    /// </summary>
    [XmlElementAttribute(ElementName="contexts",Namespace="")]
    [SoapElementAttribute(ElementName="contexts")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdContext> Contexts {
      get {
        return this._contexts;
      }
      set {
        this._contexts = value;
      }
    }
    /// <summary>
    ///  the relationship type
    /// </summary>
    [XmlElementAttribute(ElementName="relationshipType",Namespace="")]
    [SoapElementAttribute(ElementName="relationshipType")]
    public bSDD.NET.Model.Objects.IfdRelationshipTypeEnum RelationshipType {
      get {
        return this._relationshipType;
      }
      set {
        this._relationshipType = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "RelationshipType" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool RelationshipTypeSpecified {
      get {
        return this._relationshipType != bSDD.NET.Model.Objects.IfdRelationshipTypeEnum.NULL;
      }
      set {
        if (!value) {
          this._relationshipType = bSDD.NET.Model.Objects.IfdRelationshipTypeEnum.NULL;
        }
      }
    }
    /// <summary>
    ///  the parent
    /// </summary>
    [XmlElementAttribute(ElementName="parent",Namespace="")]
    [SoapElementAttribute(ElementName="parent")]
    public bSDD.NET.Model.Objects.IfdConcept Parent {
      get {
        return this._parent;
      }
      set {
        this._parent = value;
      }
    }
    /// <summary>
    ///  the child
    /// </summary>
    [XmlElementAttribute(ElementName="child",Namespace="")]
    [SoapElementAttribute(ElementName="child")]
    public bSDD.NET.Model.Objects.IfdConcept Child {
      get {
        return this._child;
      }
      set {
        this._child = value;
      }
    }
  }

  /// <remarks>
  ///  The Enum IfdRelationshipTypeEnum.
  /// </remarks>
  /// <summary>
  ///  The Enum IfdRelationshipTypeEnum.
  /// </summary>
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects", ElementName="IfdRelationshipType")]
  public enum IfdRelationshipTypeEnum {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [XmlEnumAttribute(Name="__NULL__")]
    [SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///   A relationship to collect any concepts type into two types of collections; NEST or BAG.
    ///   When used in addChildren the concept must be of type NEST or BAG while the children can
    ///   be any of the available concept types. When used in addParents the concept can be any type
    ///   while the parents must be of type NEST or BAG.
    /// </summary>
    [XmlEnumAttribute(Name="COLLECTS")]
    [SoapEnumAttribute(Name="COLLECTS")]
    COLLECTS,

    /// <summary>
    ///   A relationship to assign a collection of type NEST or BAG to any other concept.
    ///   When used in addChildren the concept can be any type while the parents must be of type NEST or BAG.
    ///   When used in addParents the concept must be of type NEST or BAG while the children can
    ///   be any of the available concept types.
    /// </summary>
    [XmlEnumAttribute(Name="ASSIGNS_COLLECTIONS")]
    [SoapEnumAttribute(Name="ASSIGNS_COLLECTIONS")]
    ASSIGNS_COLLECTIONS,

    /// <summary>
    ///   Associates adds an informal relationship to something else.
    ///   Associates is a very generic relationship that only says that two things are somehow associated.
    ///   The actual meaning is hidden and must be understood by looking at the association. One possible
    ///   use is to eg associate two concepts that are somehow logically connected. Eg the light opening of a door
    ///   and the minimum width of an escape route. Other cases are between the SUBJECT door and the value
    ///   DOOR. Or the SUBJECT &quot;address&quot; and the PROPERTY &quot;address&quot;.
    /// </summary>
    [XmlEnumAttribute(Name="ASSOCIATES")]
    [SoapEnumAttribute(Name="ASSOCIATES")]
    ASSOCIATES,

    /// <summary>
    ///   Composes tells that something is a part of something else. Normally the parts (children) are
    ///   of the same type as the whole (parent) but this is not enforced by the API.
    /// </summary>
    [XmlEnumAttribute(Name="COMPOSES")]
    [SoapEnumAttribute(Name="COMPOSES")]
    COMPOSES,

    /// <summary>
    ///  Use of this is deprecated 
    /// </summary>
    [XmlEnumAttribute(Name="GROUPS")]
    [SoapEnumAttribute(Name="GROUPS")]
    GROUPS,

    /// <summary>
    ///   Specializes tells that something is a type of something else. Specialization does not enforce
    ///   inheritance of properties as often the case. Each contributor is free to add properties on
    ///   subtypes as she like. On concept can only be a specialization of another concept of the same
    ///   type. A concept can however be a specialization of many concepts.
    /// </summary>
    [XmlEnumAttribute(Name="SPECIALIZES")]
    [SoapEnumAttribute(Name="SPECIALIZES")]
    SPECIALIZES,

    /// <summary>
    ///   The acts upon relationship indicates that something is acting upon something else. For example,
    ///   &quot;A column supports a beam&quot; or &quot;A bricklayer lays bricks&quot;.
    /// </summary>
    [XmlEnumAttribute(Name="ACTS_UPON")]
    [SoapEnumAttribute(Name="ACTS_UPON")]
    ACTS_UPON,

    /// <summary>
    ///  Sequences indicates that something follows something else. Only an activity can sequence another activity. 
    /// </summary>
    [XmlEnumAttribute(Name="SEQUENCES")]
    [SoapEnumAttribute(Name="SEQUENCES")]
    SEQUENCES,

    /// <summary>
    ///  The DOCUMENTS relationship allow a document to document something else. 
    /// </summary>
    [XmlEnumAttribute(Name="DOCUMENTS")]
    [SoapEnumAttribute(Name="DOCUMENTS")]
    DOCUMENTS,

    /// <summary>
    ///  The CLASSIFIES relationship allows a classification item to classify something else. 
    /// </summary>
    [XmlEnumAttribute(Name="CLASSIFIES")]
    [SoapEnumAttribute(Name="CLASSIFIES")]
    CLASSIFIES,

    /// <summary>
    ///  The ASSIGNS_MEASURES relationship is used to connect measures to properties 
    /// </summary>
    [XmlEnumAttribute(Name="ASSIGNS_MEASURES")]
    [SoapEnumAttribute(Name="ASSIGNS_MEASURES")]
    ASSIGNS_MEASURES,

    /// <summary>
    ///  The ASSIGNS_PROPERTIES relationship allows concepts to have properties. 
    /// </summary>
    [XmlEnumAttribute(Name="ASSIGNS_PROPERTIES")]
    [SoapEnumAttribute(Name="ASSIGNS_PROPERTIES")]
    ASSIGNS_PROPERTIES,

    /// <summary>
    ///  A measure can have only one unit, and this relationship is used for that. 
    /// </summary>
    [XmlEnumAttribute(Name="ASSIGNS_UNITS")]
    [SoapEnumAttribute(Name="ASSIGNS_UNITS")]
    ASSIGNS_UNITS,

    /// <summary>
    ///  The ASSIGNS_VALUES relationship is used to connect measures with values. 
    /// </summary>
    [XmlEnumAttribute(Name="ASSIGNS_VALUES")]
    [SoapEnumAttribute(Name="ASSIGNS_VALUES")]
    ASSIGNS_VALUES,

    /// <summary>
    ///  The ASSIGNS_PROPERTY_WITH_VALUES relationship is used to constrain a property to certain values,
    ///   given a certain subject, actor or activity and a certain relevant property and measure 
    ///   (measures and values are handled elsewhere)
    /// </summary>
    [XmlEnumAttribute(Name="ASSIGNS_PROPERTY_WITH_VALUES")]
    [SoapEnumAttribute(Name="ASSIGNS_PROPERTY_WITH_VALUES")]
    ASSIGNS_PROPERTY_WITH_VALUES,

    /// <summary>
    ///  The UNDEFINED relationship is used where nothing else applies. Deprecated. 
    /// </summary>
    [XmlEnumAttribute(Name="UNDEFINED")]
    [SoapEnumAttribute(Name="UNDEFINED")]
    UNDEFINED
  }

  /// <remarks>
  ///  The Class IfdRelationshipWithValues.
  /// </remarks>
  /// <summary>
  ///  The Class IfdRelationshipWithValues.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdRelationshipWithValues")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdRelationshipWithValues")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdRelationshipWithValues")]
  public partial class IfdRelationshipWithValues : bSDD.NET.Model.Objects.IfdRelationship {

    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdValueRolePair> _valueRoles;
    private bSDD.NET.Model.Objects.IfdConcept _measure;
    /// <summary>
    ///  the value roles
    /// </summary>
    [XmlElementAttribute(ElementName="valueRoles",Namespace="")]
    [SoapElementAttribute(ElementName="valueRoles")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdValueRolePair> ValueRoles {
      get {
        return this._valueRoles;
      }
      set {
        this._valueRoles = value;
      }
    }
    /// <summary>
    ///  the measure
    /// </summary>
    [XmlElementAttribute(ElementName="measure",Namespace="")]
    [SoapElementAttribute(ElementName="measure")]
    public bSDD.NET.Model.Objects.IfdConcept Measure {
      get {
        return this._measure;
      }
      set {
        this._measure = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdReport.
  /// </remarks>
  /// <summary>
  ///  The Class IfdReport.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdReport")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdReport")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdReport")]
  public partial class IfdReport : IfdBase {

    private string _id;
    private string _name;
    private string _description;
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="id",Namespace="")]
    [SoapElementAttribute(ElementName="id")]
    public string Id {
      get {
        return this._id;
      }
      set {
        this._id = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="name",Namespace="")]
    [SoapElementAttribute(ElementName="name")]
    public string Name {
      get {
        return this._name;
      }
      set {
        this._name = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="description",Namespace="")]
    [SoapElementAttribute(ElementName="description")]
    public string Description {
      get {
        return this._description;
      }
      set {
        this._description = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdReportItem.
  /// </remarks>
  /// <summary>
  ///  The Class IfdReportItem.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdReportItem")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdReportItem")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdReportItem")]
  public partial class IfdReportItem : IfdBase {

  }

  /// <remarks>
  ///  The Class IfdSandbox.
  /// </remarks>
  /// <summary>
  ///  The Class IfdSandbox.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSandbox")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSandbox")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdSandbox")]
  public partial class IfdSandbox : IfdBase {

    private string _name;
    private bool _public;
    private bSDD.NET.Model.Objects.IfdOrganization _owner;
    /// <summary>
    ///  the name
    /// </summary>
    [XmlElementAttribute(ElementName="name",Namespace="")]
    [SoapElementAttribute(ElementName="name")]
    public string Name {
      get {
        return this._name;
      }
      set {
        this._name = value;
      }
    }
    /// <summary>
    ///  true, if is public
    /// </summary>
    [XmlElementAttribute(ElementName="public",Namespace="")]
    [SoapElementAttribute(ElementName="public")]
    public bool Public {
      get {
        return this._public;
      }
      set {
        this._public = value;
      }
    }
    /// <summary>
    ///  the owner
    /// </summary>
    [XmlElementAttribute(ElementName="owner",Namespace="")]
    [SoapElementAttribute(ElementName="owner")]
    public IfdOrganization Owner {
      get {
        return this._owner;
      }
      set {
        this._owner = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdSandboxConcept.
  /// </remarks>
  /// <summary>
  ///  The Class IfdSandboxConcept.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSandboxConcept")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSandboxConcept")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdSandboxConcept")]
  public partial class IfdSandboxConcept : IfdConcept {

    private bSDD.NET.Model.Objects.IfdSandbox _sandbox;
    /// <summary>
    ///  the sandbox
    /// </summary>
    [XmlElementAttribute(ElementName="sandbox",Namespace="")]
    [SoapElementAttribute(ElementName="sandbox")]
    public bSDD.NET.Model.Objects.IfdSandbox Sandbox {
      get {
        return this._sandbox;
      }
      set {
        this._sandbox = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdSandboxConceptInRelationship.
  /// </remarks>
  /// <summary>
  ///  The Class IfdSandboxConceptInRelationship.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSandboxConceptInRelationship")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSandboxConceptInRelationship")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdSandboxConceptInRelationship")]
  public partial class IfdSandboxConceptInRelationship : bSDD.NET.Model.Objects.IfdSandboxConcept {

    private bSDD.NET.Model.Objects.IfdRelationshipTypeEnum _relationshipType;
    /// <summary>
    ///  the relationship type
    /// </summary>
    [XmlElementAttribute(ElementName="relationshipType",Namespace="")]
    [SoapElementAttribute(ElementName="relationshipType")]
    public bSDD.NET.Model.Objects.IfdRelationshipTypeEnum RelationshipType {
      get {
        return this._relationshipType;
      }
      set {
        this._relationshipType = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "RelationshipType" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool RelationshipTypeSpecified {
      get {
        return this._relationshipType != bSDD.NET.Model.Objects.IfdRelationshipTypeEnum.NULL;
      }
      set {
        if (!value) {
          this._relationshipType = bSDD.NET.Model.Objects.IfdRelationshipTypeEnum.NULL;
        }
      }
    }
  }

  /// <remarks>
  ///  The Class IfdSimpleConcept.
  /// </remarks>
  /// <summary>
  ///  The Class IfdSimpleConcept.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSimpleConcept")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSimpleConcept")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdSimpleConcept")]
  public partial class IfdSimpleConcept : IfdBase {

    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdSimpleName> _names;
    /// <summary>
    ///  the names
    /// </summary>
    [XmlElementAttribute(ElementName="names",Namespace="")]
    [SoapElementAttribute(ElementName="names")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdSimpleName> Names {
      get {
        return this._names;
      }
      set {
        this._names = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdSimpleName.
  /// </remarks>
  /// <summary>
  ///  The Class IfdSimpleName.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSimpleName")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSimpleName")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdSimpleName")]
  public partial class IfdSimpleName {

    private string _name;
    private bSDD.NET.Model.Objects.IfdNameTypeEnum _nameType;
    /// <summary>
    ///  the name
    /// </summary>
    [XmlElementAttribute(ElementName="name",Namespace="")]
    [SoapElementAttribute(ElementName="name")]
    public string Name {
      get {
        return this._name;
      }
      set {
        this._name = value;
      }
    }
    /// <summary>
    ///  the name type
    /// </summary>
    [XmlElementAttribute(ElementName="nameType",Namespace="")]
    [SoapElementAttribute(ElementName="nameType")]
    public bSDD.NET.Model.Objects.IfdNameTypeEnum NameType {
      get {
        return this._nameType;
      }
      set {
        this._nameType = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "NameType" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool NameTypeSpecified {
      get {
        return this._nameType != bSDD.NET.Model.Objects.IfdNameTypeEnum.NULL;
      }
      set {
        if (!value) {
          this._nameType = bSDD.NET.Model.Objects.IfdNameTypeEnum.NULL;
        }
      }
    }
  }

  /// <remarks>
  ///  The Class IfdSimpleProperty.
  /// </remarks>
  /// <summary>
  ///  The Class IfdSimpleProperty.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSimpleProperty")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSimpleProperty")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdSimpleConceptValue")]
  public partial class IfdSimpleProperty : bSDD.NET.Model.Objects.IfdSimpleConcept {

    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdSimpleValue> _values;
    private bSDD.NET.Model.Objects.IfdSimpleConcept _unit;
    /// <summary>
    ///  the values
    /// </summary>
    [XmlElementAttribute(ElementName="values",Namespace="")]
    [SoapElementAttribute(ElementName="values")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdSimpleValue> Values {
      get {
        return this._values;
      }
      set {
        this._values = value;
      }
    }
    /// <summary>
    ///  the unit
    /// </summary>
    [XmlElementAttribute(ElementName="unit",Namespace="")]
    [SoapElementAttribute(ElementName="unit")]
    public bSDD.NET.Model.Objects.IfdSimpleConcept Unit {
      get {
        return this._unit;
      }
      set {
        this._unit = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdSimpleValue.
  /// </remarks>
  /// <summary>
  ///  The Class IfdSimpleValue.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSimpleValue")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdSimpleValue")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdSimpleValue")]
  public partial class IfdSimpleValue : bSDD.NET.Model.Objects.IfdSimpleConcept {

    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdSimpleName> _values;
    private bSDD.NET.Model.Objects.IfdValueRoleEnum _valueRole;
    /// <summary>
    ///  the value
    /// </summary>
    [XmlElementAttribute(ElementName="values",Namespace="")]
    [SoapElementAttribute(ElementName="values")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdSimpleName> Values {
      get {
        return this._values;
      }
      set {
        this._values = value;
      }
    }
    /// <summary>
    ///  the value role
    /// </summary>
    [XmlElementAttribute(ElementName="valueRole",Namespace="")]
    [SoapElementAttribute(ElementName="valueRole")]
    public bSDD.NET.Model.Objects.IfdValueRoleEnum ValueRole {
      get {
        return this._valueRole;
      }
      set {
        this._valueRole = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "ValueRole" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool ValueRoleSpecified {
      get {
        return this._valueRole != bSDD.NET.Model.Objects.IfdValueRoleEnum.NULL;
      }
      set {
        if (!value) {
          this._valueRole = bSDD.NET.Model.Objects.IfdValueRoleEnum.NULL;
        }
      }
    }
  }

  /// <remarks>
  ///  
  /// </remarks>
  /// <summary>
  ///  
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdStatistics")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdStatistics")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdStatistics")]
  public partial class IfdStatistics {

    private long _count;
    private string _objectName;
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="count",Namespace="")]
    [SoapElementAttribute(ElementName="count")]
    public long Count {
      get {
        return this._count;
      }
      set {
        this._count = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="objectName",Namespace="")]
    [SoapElementAttribute(ElementName="objectName")]
    public string ObjectName {
      get {
        return this._objectName;
      }
      set {
        this._objectName = value;
      }
    }
  }

  /// <remarks>
  ///  The list of statuses in bsDD.
  /// </remarks>
  /// <summary>
  ///  The list of statuses in bsDD.
  /// </summary>
  public enum IfdStatusEnum {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [XmlEnumAttribute(Name="__NULL__")]
    [SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///   For newly added concepts that are not checked by the responsible organisation. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="DRAFT")]
    [SoapEnumAttribute(Name="DRAFT")]
    DRAFT,

    /// <summary>
    ///   Checked by the responsible organisation &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="CHECKED")]
    [SoapEnumAttribute(Name="CHECKED")]
    CHECKED,

    /// <summary>
    ///   The concept is approved by the responsible organisation &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="APPROVED")]
    [SoapEnumAttribute(Name="APPROVED")]
    APPROVED,

    /// <summary>
    ///   The concept is regarded as no longer a part of the library. The API will return the object but with the status invalid. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="INVALID")]
    [SoapEnumAttribute(Name="INVALID")]
    INVALID,

    /// <summary>
    ///   The concept is merged with another concept and the guid will no longer be returned by the API. Instead the guid of the concept
    ///   replacing this concept is returned. The replacement guid will be stored in the VersionId of the transferred concept. &lt;br&gt;
    ///   &lt;br&gt;
    /// </summary>
    [XmlEnumAttribute(Name="TRANSFERRED")]
    [SoapEnumAttribute(Name="TRANSFERRED")]
    TRANSFERRED
  }

  /// <remarks>
  ///  The Class IfdTag.
  /// </remarks>
  /// <summary>
  ///  The Class IfdTag.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdTag")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdTag")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdTag")]
  public partial class IfdTag : IfdBase {

    private bSDD.NET.Model.Objects.IfdSimpleName _name;
    private System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdSimpleName> _synonyms;
    private string _definition;
    /// <summary>
    ///  the name
    /// </summary>
    [XmlElementAttribute(ElementName="name",Namespace="")]
    [SoapElementAttribute(ElementName="name")]
    public bSDD.NET.Model.Objects.IfdSimpleName Name {
      get {
        return this._name;
      }
      set {
        this._name = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="synonyms",Namespace="")]
    [SoapElementAttribute(ElementName="synonyms")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.IfdSimpleName> Synonyms {
      get {
        return this._synonyms;
      }
      set {
        this._synonyms = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="definition",Namespace="")]
    [SoapElementAttribute(ElementName="definition")]
    public string Definition {
      get {
        return this._definition;
      }
      set {
        this._definition = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdUser.
  /// </remarks>
  /// <summary>
  ///  The Class IfdUser.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdUser")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdUser")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdUser")]
  public partial class IfdUser : IfdBase {

    private string _name;
    private string _email;
    private string _createdDate;
    private bSDD.NET.Model.Objects.IfdUserRoleTypeEnum _role;
    private bSDD.NET.Model.Objects.IfdOrganization _memberOf;
    private string _preferredOrganization;
    /// <summary>
    ///  the name
    /// </summary>
    [XmlElementAttribute(ElementName="name",Namespace="")]
    [SoapElementAttribute(ElementName="name")]
    public string Name {
      get {
        return this._name;
      }
      set {
        this._name = value;
      }
    }
    /// <summary>
    ///  the email
    /// </summary>
    [XmlElementAttribute(ElementName="email",Namespace="")]
    [SoapElementAttribute(ElementName="email")]
    public string Email {
      get {
        return this._email;
      }
      set {
        this._email = value;
      }
    }
    /// <summary>
    ///  the created date
    /// </summary>
    [XmlElementAttribute(ElementName="createdDate",Namespace="")]
    [SoapElementAttribute(ElementName="createdDate")]
    public string CreatedDate {
      get {
        return this._createdDate;
      }
      set {
        this._createdDate = value;
      }
    }
    /// <summary>
    ///  the role
    /// </summary>
    [XmlElementAttribute(ElementName="role",Namespace="")]
    [SoapElementAttribute(ElementName="role")]
    public bSDD.NET.Model.Objects.IfdUserRoleTypeEnum Role {
      get {
        return this._role;
      }
      set {
        this._role = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "Role" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool RoleSpecified {
      get {
        return this._role != bSDD.NET.Model.Objects.IfdUserRoleTypeEnum.NULL;
      }
      set {
        if (!value) {
          this._role = bSDD.NET.Model.Objects.IfdUserRoleTypeEnum.NULL;
        }
      }
    }
    /// <summary>
    ///  the member of
    /// </summary>
    [XmlElementAttribute(ElementName="memberOf",Namespace="")]
    [SoapElementAttribute(ElementName="memberOf")]
    public bSDD.NET.Model.Objects.IfdOrganization MemberOf {
      get {
        return this._memberOf;
      }
      set {
        this._memberOf = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="preferredOrganization",Namespace="")]
    [SoapElementAttribute(ElementName="preferredOrganization")]
    public string PreferredOrganization {
      get {
        return this._preferredOrganization;
      }
      set {
        this._preferredOrganization = value;
      }
    }
  }

  /// <remarks>
  ///  The Enum IfdUserRoleTypeEnum.
  /// </remarks>
  /// <summary>
  ///  The Enum IfdUserRoleTypeEnum.
  /// </summary>
  public enum IfdUserRoleTypeEnum {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [XmlEnumAttribute(Name="__NULL__")]
    [SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///  An administrator has full rights in the system. 
    /// </summary>
    [XmlEnumAttribute(Name="IFD_ADMINISTRATOR")]
    [SoapEnumAttribute(Name="IFD_ADMINISTRATOR")]
    IFD_ADMINISTRATOR,

    /// <summary>
    ///  An editor can typically edit all the content in the system, but 
    ///   not content affecting contexts the user do not have edit access to. 
    /// </summary>
    [XmlEnumAttribute(Name="IFD_EDITOR")]
    [SoapEnumAttribute(Name="IFD_EDITOR")]
    IFD_EDITOR,

    /// <summary>
    ///  A user with read and comment rights, can read all content (except contexts he has no
    ///   access to) and add comments to concepts. 
    /// </summary>
    [XmlEnumAttribute(Name="IFD_READ_AND_COMMENT")]
    [SoapEnumAttribute(Name="IFD_READ_AND_COMMENT")]
    IFD_READ_AND_COMMENT,

    /// <summary>
    ///  A user with read only access can read all content (except contexts he has no
    ///   access to). 
    /// </summary>
    [XmlEnumAttribute(Name="IFD_READ_ONLY")]
    [SoapEnumAttribute(Name="IFD_READ_ONLY")]
    IFD_READ_ONLY,

    /// <summary>
    ///  The unknown. 
    /// </summary>
    [XmlEnumAttribute(Name="PUBLIC")]
    [SoapEnumAttribute(Name="PUBLIC")]
    PUBLIC,

    /// <summary>
    ///  An inactive user has no access at all. 
    /// </summary>
    [XmlEnumAttribute(Name="INACTIVE")]
    [SoapEnumAttribute(Name="INACTIVE")]
    INACTIVE
  }

  /// <remarks>
  ///  The Enum IfdValueRoleEnum.
  /// </remarks>
  /// <summary>
  ///  The Enum IfdValueRoleEnum.
  /// </summary>
  public enum IfdValueRoleEnum {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [XmlEnumAttribute(Name="__NULL__")]
    [SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///  The enumeration. 
    /// </summary>
    [XmlEnumAttribute(Name="ENUMERATION")]
    [SoapEnumAttribute(Name="ENUMERATION")]
    ENUMERATION,

    /// <summary>
    ///  The nominal. 
    /// </summary>
    [XmlEnumAttribute(Name="NOMINAL")]
    [SoapEnumAttribute(Name="NOMINAL")]
    NOMINAL,

    /// <summary>
    ///  The minimum. 
    /// </summary>
    [XmlEnumAttribute(Name="MINIMUM")]
    [SoapEnumAttribute(Name="MINIMUM")]
    MINIMUM,

    /// <summary>
    ///  The maximum. 
    /// </summary>
    [XmlEnumAttribute(Name="MAXIMUM")]
    [SoapEnumAttribute(Name="MAXIMUM")]
    MAXIMUM,

    /// <summary>
    ///  The lowertolerance. 
    /// </summary>
    [XmlEnumAttribute(Name="LOWERTOLERANCE")]
    [SoapEnumAttribute(Name="LOWERTOLERANCE")]
    LOWERTOLERANCE,

    /// <summary>
    ///  The uppertolerance. 
    /// </summary>
    [XmlEnumAttribute(Name="UPPERTOLERANCE")]
    [SoapEnumAttribute(Name="UPPERTOLERANCE")]
    UPPERTOLERANCE,

    /// <summary>
    ///  The defining. 
    /// </summary>
    [XmlEnumAttribute(Name="DEFINING")]
    [SoapEnumAttribute(Name="DEFINING")]
    DEFINING,

    /// <summary>
    ///  The defined. 
    /// </summary>
    [XmlEnumAttribute(Name="DEFINED")]
    [SoapEnumAttribute(Name="DEFINED")]
    DEFINED
  }

  /// <remarks>
  ///  The Class IfdValueRolePair.
  /// </remarks>
  /// <summary>
  ///  The Class IfdValueRolePair.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdValueRolePair")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects",TypeName="ifdValueRolePair")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects",ElementName="IfdValueRolePair")]
  public partial class IfdValueRolePair : IfdBase {

    private bSDD.NET.Model.Objects.IfdConcept _value;
    private bSDD.NET.Model.Objects.IfdValueRoleEnum _valueRole;
    /// <summary>
    ///  the value
    /// </summary>
    [XmlElementAttribute(ElementName="value",Namespace="")]
    [SoapElementAttribute(ElementName="value")]
    public bSDD.NET.Model.Objects.IfdConcept Value {
      get {
        return this._value;
      }
      set {
        this._value = value;
      }
    }
    /// <summary>
    ///  the value role
    /// </summary>
    [XmlElementAttribute(ElementName="valueRole",Namespace="")]
    [SoapElementAttribute(ElementName="valueRole")]
    public bSDD.NET.Model.Objects.IfdValueRoleEnum ValueRole {
      get {
        return this._valueRole;
      }
      set {
        this._valueRole = value;
      }
    }

    /// <summary>
    ///  Property for the XML serializer indicating whether the "ValueRole" property should be included in the output.
    /// </summary>
    [XmlIgnoreAttribute]
    [SoapIgnoreAttribute]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool ValueRoleSpecified {
      get {
        return this._valueRole != bSDD.NET.Model.Objects.IfdValueRoleEnum.NULL;
      }
      set {
        if (!value) {
          this._valueRole = bSDD.NET.Model.Objects.IfdValueRoleEnum.NULL;
        }
      }
    }
  }
}  

namespace bSDD.NET.Model.Objects.Pset {

  /// <remarks>
  ///  The Class IfdPSet.
  /// </remarks>
  /// <summary>
  ///  The Class IfdPSet.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSet")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSet")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects/pset",ElementName="IfdPSet")]
  public partial class IfdPSet : bSDD.NET.Model.Objects.Pset.IfdPSetBase {

    private string _ifcVersion;
    private string _applicability;
    private System.Collections.Generic.List<bSDD.NET.Model.Objects.Pset.IfdPSetProperty> _properties;
    private string _ifcName;
    private string _ifcDefinition;
    private System.Collections.Generic.List<string> _applicableClasses;
    private System.Collections.Generic.List<string> _applicableTypeValues;
    private string _schema;
    /// <summary>
    ///  the ifc version
    /// </summary>
    [XmlElementAttribute(ElementName="ifcVersion",Namespace="")]
    [SoapElementAttribute(ElementName="ifcVersion")]
    public string IfcVersion {
      get {
        return this._ifcVersion;
      }
      set {
        this._ifcVersion = value;
      }
    }
    /// <summary>
    ///  the applicability
    /// </summary>
    [XmlElementAttribute(ElementName="applicability",Namespace="")]
    [SoapElementAttribute(ElementName="applicability")]
    public string Applicability {
      get {
        return this._applicability;
      }
      set {
        this._applicability = value;
      }
    }
    /// <summary>
    ///  the properties
    /// </summary>
    [XmlElementAttribute(ElementName="properties",Namespace="")]
    [SoapElementAttribute(ElementName="properties")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.Pset.IfdPSetProperty> Properties {
      get {
        return this._properties;
      }
      set {
        this._properties = value;
      }
    }
    /// <summary>
    ///  the ifc name
    /// </summary>
    [XmlElementAttribute(ElementName="ifcName",Namespace="")]
    [SoapElementAttribute(ElementName="ifcName")]
    public string IfcName {
      get {
        return this._ifcName;
      }
      set {
        this._ifcName = value;
      }
    }
    /// <summary>
    ///  the ifc definition
    /// </summary>
    [XmlElementAttribute(ElementName="ifcDefinition",Namespace="")]
    [SoapElementAttribute(ElementName="ifcDefinition")]
    public string IfcDefinition {
      get {
        return this._ifcDefinition;
      }
      set {
        this._ifcDefinition = value;
      }
    }
    /// <summary>
    ///  the applicable classes
    /// </summary>
    [XmlElementAttribute(ElementName="applicableClasses",Namespace="")]
    [SoapElementAttribute(ElementName="applicableClasses")]
    public System.Collections.Generic.List<string> ApplicableClasses {
      get {
        return this._applicableClasses;
      }
      set {
        this._applicableClasses = value;
      }
    }
    /// <summary>
    ///  the applicable type values
    /// </summary>
    [XmlElementAttribute(ElementName="applicableTypeValues",Namespace="")]
    [SoapElementAttribute(ElementName="applicableTypeValues")]
    public System.Collections.Generic.List<string> ApplicableTypeValues {
      get {
        return this._applicableTypeValues;
      }
      set {
        this._applicableTypeValues = value;
      }
    }
    /// <summary>
    ///  the schema
    /// </summary>
    [XmlElementAttribute(ElementName="schema",Namespace="")]
    [SoapElementAttribute(ElementName="schema")]
    public string Schema {
      get {
        return this._schema;
      }
      set {
        this._schema = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdPSetBase.
  /// </remarks>
  /// <summary>
  ///  The Class IfdPSetBase.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetBase")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetBase")]
  public partial class IfdPSetBase : IfdBase {

    private IfdConcept _ifdMainConcept;
    private System.Collections.Generic.List<IfdPSetContentAliasItem> _nameAliases;
    private System.Collections.Generic.List<IfdPSetContentAliasItem> _definitionAliases;
    /// <summary>
    ///  the ifd main concept
    /// </summary>
    [XmlElementAttribute(ElementName="ifdMainConcept",Namespace="")]
    [SoapElementAttribute(ElementName="ifdMainConcept")]
    public IfdConcept IfdMainConcept {
      get {
        return this._ifdMainConcept;
      }
      set {
        this._ifdMainConcept = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="nameAliases",Namespace="")]
    [SoapElementAttribute(ElementName="nameAliases")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.Pset.IfdPSetContentAliasItem> NameAliases {
      get {
        return this._nameAliases;
      }
      set {
        this._nameAliases = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="definitionAliases",Namespace="")]
    [SoapElementAttribute(ElementName="definitionAliases")]
    public System.Collections.Generic.List<bSDD.NET.Model.Objects.Pset.IfdPSetContentAliasItem> DefinitionAliases {
      get {
        return this._definitionAliases;
      }
      set {
        this._definitionAliases = value;
      }
    }
  }

  /// <remarks>
  ///  
  /// </remarks>
  /// <summary>
  ///  
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetContentAliasItem")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetContentAliasItem")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects/pset",ElementName="IfdPSetContentAliasItem")]
  public partial class IfdPSetContentAliasItem {

    private string _content;
    private string _languageCode;
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="content",Namespace="")]
    [SoapElementAttribute(ElementName="content")]
    public string Content {
      get {
        return this._content;
      }
      set {
        this._content = value;
      }
    }
    /// <summary>
    ///  (no documentation provided)
    /// </summary>
    [XmlElementAttribute(ElementName="languageCode",Namespace="")]
    [SoapElementAttribute(ElementName="languageCode")]
    public string LanguageCode {
      get {
        return this._languageCode;
      }
      set {
        this._languageCode = value;
      }
    }
  }

  /// <remarks>
  ///  The Class IfdPSetProperty.
  /// </remarks>
  /// <summary>
  ///  The Class IfdPSetProperty.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetProperty")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetProperty")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects/pset",ElementName="IfdPSetProperty")]
  public partial class IfdPSetProperty : bSDD.NET.Model.Objects.Pset.IfdPSetBase {

    private bSDD.NET.Model.Objects.Pset.IfdPSetPropertyTypeInterface _propertyType;
    private string _ifcName;
    private string _ifcDefinition;
    /// <summary>
    ///  the property type
    /// </summary>
    [XmlElementAttribute(ElementName="propertyType",Namespace="")]
    [SoapElementAttribute(ElementName="propertyType")]
    public bSDD.NET.Model.Objects.Pset.IfdPSetPropertyTypeInterface PropertyType {
      get {
        return this._propertyType;
      }
      set {
        this._propertyType = value;
      }
    }
    /// <summary>
    ///  the ifc name
    /// </summary>
    [XmlElementAttribute(ElementName="ifcName",Namespace="")]
    [SoapElementAttribute(ElementName="ifcName")]
    public string IfcName {
      get {
        return this._ifcName;
      }
      set {
        this._ifcName = value;
      }
    }
    /// <summary>
    ///  the ifc definition
    /// </summary>
    [XmlElementAttribute(ElementName="ifcDefinition",Namespace="")]
    [SoapElementAttribute(ElementName="ifcDefinition")]
    public string IfcDefinition {
      get {
        return this._ifcDefinition;
      }
      set {
        this._ifcDefinition = value;
      }
    }
  }

  /// <remarks>
  ///  The Interface IfdPSetPropertyTypeInterface.
  /// </remarks>
  /// <summary>
  ///  The Interface IfdPSetPropertyTypeInterface.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetPropertyTypeInterface")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetPropertyTypeInterface")]
  public abstract partial class IfdPSetPropertyTypeInterface {

  }

  /// <remarks>
  ///  The Class IfdPSetPropertyTypeSingleValue.
  /// </remarks>
  /// <summary>
  ///  The Class IfdPSetPropertyTypeSingleValue.
  /// </summary>
  [System.SerializableAttribute()]
  [XmlTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetPropertyTypeSingleValue")]
  [SoapTypeAttribute(Namespace="http://peregrine.catenda.no/objects/pset",TypeName="ifdPSetPropertyTypeSingleValue")]
  [XmlRootAttribute(Namespace="http://peregrine.catenda.no/objects/pset",ElementName="IfdPSetPropertyTypeSingleValue")]
  public partial class IfdPSetPropertyTypeSingleValue : bSDD.NET.Model.Objects.Pset.IfdPSetPropertyTypeInterface {

    private string _dataType;
    private string _unitType;
    /// <summary>
    ///  the data type
    /// </summary>
    [XmlElementAttribute(ElementName="dataType",Namespace="")]
    [SoapElementAttribute(ElementName="dataType")]
    public string DataType {
      get {
        return this._dataType;
      }
      set {
        this._dataType = value;
      }
    }
    /// <summary>
    ///  the unit type
    /// </summary>
    [XmlElementAttribute(ElementName="unitType",Namespace="")]
    [SoapElementAttribute(ElementName="unitType")]
    public string UnitType {
      get {
        return this._unitType;
      }
      set {
        this._unitType = value;
      }
    }
  }
}  
