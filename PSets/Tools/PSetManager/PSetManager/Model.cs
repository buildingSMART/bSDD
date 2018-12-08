using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace PSet2YamlConverter
{
    public class PropertySet
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string name { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public IfcVersion ifcVersion { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string ifdGuid { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string definition { get; set; }

        public List<Localization> localizations { get; set; }

        public List<ApplicableIfcClass> applicableIfcClasses { get; set; }

        public List<Property> properties { get; set; }

        public void PrepareTexts()
        {
            this.definition = Utils.CleanUp(this.definition);
            foreach (var prop in this.properties)
            {
                prop.definition = Utils.CleanUp(prop.definition);
                foreach (var lang in prop.localizations)
                {
                    lang.definition = Utils.CleanUp(lang.definition);
                }
            }
        }
    }

    public class IfcVersion
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string version { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string schema { get; set; }
    }
    public class Localization
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string language { get; set; }
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string name { get; set; }
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string definition { get; set; }
    }
    public class ApplicableIfcClass
    {
        public string name { get; set; }
        public string type { get; set; }
    }
    public class Property
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string name { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string definition { get; set; }

        public string ifdGuid { get; set; }

        public List<Localization> localizations { get; set; }

        public TypePropertyBoundedValue typePropertyBoundedValue { get; set; }

        public TypeComplexProperty typeComplexProperty { get; set; }

        public TypePropertyEnumeratedValue typePropertyEnumeratedValue { get; set; }

        public TypePropertyListValue typePropertyListValue { get; set; }

        public TypePropertyReferenceValue typePropertyReferenceValue { get; set; }

        public TypePropertySingleValue typePropertySingleValue { get; set; }

        public TypePropertyTableValue typePropertyTableValue { get; set; }

    }
    public class TypeOfValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string typeName { get; set; }
    }

    public class TypePropertyBoundedValue : TypeOfValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public IfcDataType dataType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string measureType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string unitType { get; set; }
        /// <summary>
        /// The lower bound edge value for the values of the property
        /// </summary>
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string LowerBoundValue { get; set; }

        /// <summary>
        /// The upper bound edge value for the values of the property
        /// </summary>
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string UpperBoundValue { get; set; }
    }
    public class TypeComplexProperty : TypeOfValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string name { get; set; }

        public List<Property> subProperties { get; set; }
    }
    public class TypePropertyEnumeratedValue : TypeOfValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string listName { get; set; }

        public List<EnumerationValue> enumerationValues { get; set; }
    }
    /// <summary>
    /// The container element of list value.
    /// </summary>
    public class TypePropertyListValue : TypeOfValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public IfcDataType dataType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string measureType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string unitType { get; set; }
        public List<string> listValues { get; set; }
    }
    public class TypePropertyReferenceValue : TypeOfValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public ReferenceClass refType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string url { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string guid { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string libraryName { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string sectionref { get; set; }
    }
    public class TypePropertySingleValue : TypeOfValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public IfcDataType dataType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string measureType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string unitType { get; set; }
    }
    public class TypePropertyTableValue : TypeOfValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string Expression { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public TableDefValues DefiningValue { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public TableDefValues DefinedValue { get; set; }
    }

public class TableDefValues
{
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public IfcDataType dataType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string measureType { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string unitType { get; set; }
}

    public enum IfcDataType {IfcAmountOfSubstanceMeasure,
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
                            IfcWarpingMomentMeasure}
    public enum ReferenceClass { IfcMaterialDefinition,
                                    IfcPerson,
                                    IfcOrganization,
                                    IfcPersonAndOrganization,
                                    IfcExternalReference,
                                    IfcTimeSeries,
                                    IfcAddress,
                                    IfcAppliedValue };

    public class EnumerationValue
    {
        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string name { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string definition { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.SingleQuoted)]
        public string ifdGuid { get; set; }

        public List<Localization> localizations { get; set; }
    }
}
