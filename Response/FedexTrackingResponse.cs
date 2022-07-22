using Newtonsoft.Json;
using System.Collections.Generic;

namespace Shiptoboxes.External.Fedex.Tracking.Models
{
    public class FedexTrackingResponse
    {
        [JsonProperty("output")]
        public Output Output { get; set; }
    }
    public class Output
    {
        [JsonProperty("completeTrackResults")]
        public List<CompleteTrackResults> CompleteTrackResults { get; set; }
    }
    public class CompleteTrackResults
    {
        [JsonProperty("trackResults")]
        public List<TrackResults> TrackResults { get; set; }
    }
    public class TrackResults
    {
        [JsonProperty("latestStatusDetail")]
        public LatestStatusDetail LatestStatusDetail { get; set; }
    }
    public class LatestStatusDetail
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("derivedCode")]
        public string DerivedCode { get; set; }

        [JsonProperty("scanLocation")]
        public ScanLocation ScanLocation { get; set; }
    }
    public class ScanLocation
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("countryName")]
        public string Country { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}
