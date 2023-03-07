using System;
using Newtonsoft.Json;

namespace MVC_Address.Models
{
    public class CertificateModel
    {
        public bool HasRecommendation { get; set; }
        [JsonProperty("lmk-key")]
        public string LmkKey { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("address3")]
        public string Address3 { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("building-reference-number")]
        public string BuildingReferenceNumber { get; set; }

        [JsonProperty("current-operational-rating")]
        public string CurrentOperationalRating { get; set; } // double

        [JsonProperty("yr1-operational-rating")]
        public string Yr1OperationalRating { get; set; } //double

        [JsonProperty("yr2-operational-rating")]
        public string Yr2OperationalRating { get; set; } //double

        [JsonProperty("operational-rating-band")]
        public string OperationalRatingBand { get; set; }

        [JsonProperty("electric-co2")]
        public string ElectricCo2 { get; set; } // double/float

        [JsonProperty("heating-co2")]
        public string HeatingCo2 { get; set; } // double/float

        [JsonProperty("renewables-co2")]
        public string RenewablesCo2 { get; set; } // double/float

        [JsonProperty("property-type")]
        public string PropertyType { get; set; }

        [JsonProperty("inspection-date")]
        public string InspectionDate { get; set; } //date

        [JsonProperty("local-authority")]
        public string Local_authority { get; set; }

        [JsonProperty("constituency")]
        public string Constituency { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("lodgment-date")]
        public string? LodgementDate { get; set; } // date

        [JsonProperty("main-benchmark")]
        public string MainBenchmark { get; set; }

        [JsonProperty("main-heating-fuel")]
        public string MainHeatingFuel { get; set; }

        [JsonProperty("other-fuel")]
        public string OtherFuel { get; set; }

        [JsonProperty("special-energy-uses")]
        public string SpecialEnergyUses { get; set; }

        [JsonProperty("renewable-sources")]
        public string RenewableSources { get; set; }

        [JsonProperty("total-floor-area")]
        public string TotalFloorArea { get; set; } // int

        [JsonProperty("annual-thermal-fuel-usage")]
        public string AnnualThermalFuelUsage { get; set; }// int

        [JsonProperty("typical-thermal-fuel-usage")]
        public string TypicalThermalFuelUsage { get; set; }// int

        [JsonProperty("annual-electrical-fuel-usage")]
        public string AnnualElectricalFuelUsage { get; set; }// int

        [JsonProperty("typical-electrical-fuel-usage")]
        public string TypicalElectricalFuelUsage { get; set; } //int

        [JsonProperty("renewables-fuel-thermal")]
        public string RenewablesFuelThermal { get; set; }

        [JsonProperty("renewables-electrical")]
        public string RenewablesElectrical { get; set; }

        [JsonProperty("yr1-electricity-co2")]
        public string Yr1ElectricityCo2 { get; set; }

        [JsonProperty("yr2-electricity-co2")]
        public string Yr2ElectricityCo2 { get; set; }

        [JsonProperty("yr1-heating-co2")]
        public string Yr1HeatingCo2 { get; set; }

        [JsonProperty("yr2-heating-co2")]
        public string Yr2HeatingCo2 { get; set; }

        [JsonProperty("yr1-renewables-co2")]
        public string Yr1RenewablesCo2 { get; set; }

        [JsonProperty("yr2-renewables-co2")]
        public string Yr2RenewablesCo2 { get; set; }

        [JsonProperty("aircon-present")]
        public string AirconPresent { get; set; }

        [JsonProperty("aircon-kw-rating")]
        public string AirconKwRating { get; set; } //double

        [JsonProperty("estimated-aircon-kw-rating")]
        public string EstimatedAirconKwRating { get; set; }

        [JsonProperty("ac-inspection-commissioned")]
        public string AcInspectionCommissioned { get; set; }

        [JsonProperty("building-environment")]
        public string BuildingEnvironment { get; set; }

        [JsonProperty("building-category")]
        public string BuildingCategory { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("local-authority-label")]
        public string LocalAuthorityLabel { get; set; }

        [JsonProperty("constituency-label")]
        public string ConstituencyLabel { get; set; }

        [JsonProperty("posttown")]
        public string PostTown { get; set; }

        [JsonProperty("nominated-date")]
        public string NominatedDate { get; set; } //datetime parse

        [JsonProperty("or-assessment-end-date")]
        public string OrAssessmentEndDate { get; set; } //datetime parse

        [JsonProperty("lodgement-datetime")]
        public string LodgementDatetime { get; set; } //datetime parse

        [JsonProperty("occupancy-level")]
        public string OccupancyLevel { get; set; }

        [JsonProperty("uprn")]
        public string Uprn { get; set; }

        [JsonProperty("uprn_source")]
        public string UprnSource { get; set; }
    }
}

