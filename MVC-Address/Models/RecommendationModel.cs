using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Address.Models
{
    public class RecommendationModel
    {
        [JsonProperty("lmk-key")]
        public string LmkKey { get; set; }

        [JsonProperty("improvement-item")]
        public string ImprovementItem { get; set; }

        [JsonProperty("improvement-summary-text")]
        public string ImprovementSummaryText { get; set; }

        [JsonProperty("improvement-descr-text")]
        public string ImprovementDescrText { get; set; }

        [JsonProperty("improvement-id")]
        public string ImprovementId { get; set; }

        [JsonProperty("improvement-id-text")]
        public string ImprovementIdText { get; set; }

        [JsonProperty("indicative-cost")]
        public string IndicativeCost { get; set; }
    }
}

