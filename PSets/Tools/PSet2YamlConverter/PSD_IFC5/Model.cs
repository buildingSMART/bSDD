using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace PSD_IFC5
{
    public class PropertySet
    {
        public string name { get; set; }

        public IfcVersion ifcVersion { get; set; }

        public string definition { get; set; }
        public DictionaryReference dictionaryReference { get; set; }
        public List<Localization> localizations { get; set; }

        public List<ApplicableIfcClass> applicableIfcClasses { get; set; }

        public List<Property> properties { get; set; }
    }
    public class IfcVersion
    {
        public string version { get; set; }

        public string schema { get; set; }
    }
    public class Localization
    {
        public string language { get; set; }

        public string name { get; set; }

        public string definition { get; set; }
        public string description { get; set; }
        public string example { get; set; }

    }
    public class ApplicableIfcClass
    {
        public string name { get; set; }
        public string type { get; set; }
    }
    public class Property
    {
        public string name { get; set; }
        public string definition { get; set; }
        public DictionaryReference dictionaryReference { get; set; }
        public List<Localization> localizations { get; set; }
        public TypePropertyBoundedValue typePropertyBoundedValue { get; set; }
        public TypeComplexProperty typeComplexProperty { get; set; }
        public TypePropertyEnumeratedValue typePropertyEnumeratedValue { get; set; }
        public TypePropertyListValue typePropertyListValue { get; set; }
        public TypePropertyReferenceValue typePropertyReferenceValue { get; set; }
        public TypePropertySingleValue typePropertySingleValue { get; set; }
        public TypePropertyTableValue typePropertyTableValue { get; set; }
        public PublicationStatus status{ get; set; }
    }
    public class TypeOfValue
    {
        public string typeName { get; set; }
    }
    public class TypePropertyBoundedValue : TypeOfValue
    {
        public IfcDataType dataType { get; set; }

        public string measureType { get; set; }

        public string unitType { get; set; }

        /// <summary>
        /// The lower bound edge value for the values of the property
        /// </summary>
        public string LowerBoundValue { get; set; }

        /// <summary>
        /// The upper bound edge value for the values of the property
        /// </summary>
        public string UpperBoundValue { get; set; }
    }
    public class TypeComplexProperty : TypeOfValue
    {
        public string name { get; set; }

        public List<Property> subProperties { get; set; }
    }
    public class TypePropertyEnumeratedValue : TypeOfValue
    {
        public string listName { get; set; }

        public List<EnumerationValue> enumerationValues { get; set; }
    }
    /// <summary>
    /// The container element of list value.
    /// </summary>
    public class TypePropertyListValue : TypeOfValue
    {
        public IfcDataType dataType { get; set; }

        public string measureType { get; set; }


        public string unitType { get; set; }

        public List<string> listValues { get; set; }
    }
    public class TypePropertyReferenceValue : TypeOfValue
    {
        public ReferenceClass refType { get; set; }

        public string url { get; set; }

        public string guid { get; set; }

        public string libraryName { get; set; }

        public string sectionref { get; set; }
    }
    public class TypePropertySingleValue : TypeOfValue
    {
        public IfcDataType dataType { get; set; }

        public string measureType { get; set; }

        public string unitType { get; set; }
    }
    public class TypePropertyTableValue : TypeOfValue
    {
        public string Expression { get; set; }

        public TableDefValues DefiningValue { get; set; }

        public TableDefValues DefinedValue { get; set; }
    }
    public class TableDefValues
    {
        public IfcDataType dataType { get; set; }

        public string measureType { get; set; }

        public string unitType { get; set; }
    }
    public enum IfcDataType
    {
        IfcAmountOfSubstanceMeasure,
        IfcContextDependentMeasure,
        IfcCountMeasure,
        IfcDescriptiveMeasure,
        IfcAreaMeasure,
        IfcComplexNumber,
        IfcElectricCurrentMeasure,
        IfcLengthMeasure,
        IfcLuminousIntensityMeasure,
        IfcMassMeasure,
        IfcNonNegativeLengthMeasure,
        IfcNormalisedRatioMeasure,
        IfcNumericMeasure,
        IfcParameterValue,
        IfcPlaneAngleMeasure,
        IfcPositiveLengthMeasure,
        IfcPositivePlaneAngleMeasure,
        IfcPositiveRatioMeasure,
        IfcRatioMeasure,
        IfcSolidAngleMeasure,
        IfcThermodynamicTemperatureMeasure,
        IfcTimeMeasure,
        IfcVolumeMeasure,
        IfcBoolean,
        IfcDate,
        IfcDateTime,
        IfcDuration,
        IfcIdentifier,
        IfcInteger,
        IfcLabel,
        IfcLogical,
        IfcReal,
        IfcText,
        IfcTime,
        IfcTimeStamp,
        IfcAbsorbedDoseMeasure,
        IfcAccelerationMeasure,
        IfcAngularVelocityMeasure,
        IfcAreaDensityMeasure,
        IfcCompoundPlaneAngleMeasure,
        IfcCurvatureMeasure,
        IfcDoseEquivalentMeasure,
        IfcDynamicViscosityMeasure,
        IfcElectricCapacitanceMeasure,
        IfcElectricChargeMeasure,
        IfcElectricConductanceMeasure,
        IfcElectricResistanceMeasure,
        IfcElectricVoltageMeasure,
        IfcEnergyMeasure,
        IfcForceMeasure,
        IfcFrequencyMeasure,
        IfcHeatFluxDensityMeasure,
        IfcHeatingValueMeasure,
        IfcIlluminanceMeasure,
        IfcInductanceMeasure,
        IfcIntegerCountRateMeasure,
        IfcIonConcentrationMeasure,
        IfcIsothermalMoistureCapacityMeasure,
        IfcKinematicViscosityMeasure,
        IfcLinearForceMeasure,
        IfcLinearMomentMeasure,
        IfcLinearStiffnessMeasure,
        IfcLinearVelocityMeasure,
        IfcLuminousFluxMeasure,
        IfcLuminousIntensityDistributionMeasure,
        IfcMagneticFluxDensityMeasure,
        IfcMagneticFluxMeasure,
        IfcMassDensityMeasure,
        IfcMassFlowRateMeasure,
        IfcMassPerLengthMeasure,
        IfcModulusOfElasticityMeasure,
        IfcModulusOfLinearSubgradeReactionMeasure,
        IfcModulusOfRotationalSubgradeReactionMeasure,
        IfcModulusOfSubgradeReactionMeasure,
        IfcMoistureDiffusivityMeasure,
        IfcMolecularWeightMeasure,
        IfcMomentOfInertiaMeasure,
        IfcMonetaryMeasure,
        IfcPHMeasure,
        IfcPlanarForceMeasure,
        IfcPowerMeasure,
        IfcPressureMeasure,
        IfcRadioActivityMeasure,
        IfcRotationalFrequencyMeasure,
        IfcRotationalMassMeasure,
        IfcRotationalStiffnessMeasure,
        IfcSectionModulusMeasure,
        IfcSectionalAreaIntegralMeasure,
        IfcShearModulusMeasure,
        IfcSoundPowerLevelMeasure,
        IfcSoundPowerMeasure,
        IfcSoundPressureLevelMeasure,
        IfcSoundPressureMeasure,
        IfcSpecificHeatCapacityMeasure,
        IfcTemperatureGradientMeasure,
        IfcTemperatureRateOfChangeMeasure,
        IfcThermalAdmittanceMeasure,
        IfcThermalConductivityMeasure,
        IfcThermalExpansionCoefficientMeasure,
        IfcThermalResistanceMeasure,
        IfcThermalTransmittanceMeasure,
        IfcTorqueMeasure,
        IfcVaporPermeabilityMeasure,
        IfcVolumetricFlowRateMeasure,
        IfcWarpingConstantMeasure,
        IfcWarpingMomentMeasure
    }
    public enum ReferenceClass
    {
        IfcMaterialDefinition,
        IfcPerson,
        IfcOrganization,
        IfcPersonAndOrganization,
        IfcExternalReference,
        IfcTimeSeries,
        IfcAddress,
        IfcAppliedValue
    };
    public class EnumerationValue
    {
        public string name { get; set; }

        public string definition { get; set; }

        public string ifdGuid { get; set; }

        public List<Localization> localizations { get; set; }
    }

    public class PublicationStatus
    {
        public int versionNumber { get; set; }
        public DateTime dateOfVersion { get; set; }
        public int revisionNumber { get; set; }
        public DateTime dateOfRevision { get; set; }
        public enum Status {Active, Inactive}
        public string status { get; set; }
        public DateTime dateOfCreation { get; set; }
        public DateTime dateOfActivation { get; set; }
        public DateTime dateOfLastChange { get; set; }    
        public DateTime dateOfDeactivation { get; set; }
        public string languageOfCreator { get; set; }
    }

    public class DictionaryReference
    {
        public string dictionaryIdentifier { get; set; }
        public string dictionaryNamespace { get; set; }
        public string dictionaryWebUri { get; set; }
        public string dictionaryApiUri { get; set; }
        public string ifdGuid { get; set; }
        public string legacyGuid { get; set; }
        public string legacyGuidAsIfcGlobalId { get; set; }
    }
}
