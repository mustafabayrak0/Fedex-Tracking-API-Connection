using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Shiptoboxes.External.Fedex.Tracking.Request
{
    public class FedexTrackingRequest
    {
        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }

        [JsonProperty("X-locale")]
        public string Xlocale { get; set; }

        [JsonProperty("includeDetailedScans")]
        public bool IncludeDetailedScans { get; set; }

        [JsonProperty("trackingInfo")]
        public List<TrackingInfo> TrackingInfo { get; set; }
    }
    public class TrackingInfo
    {
        [JsonProperty("shipDateBegin")]
        public string ShipDateBegin { get; set; }

        [JsonProperty("shipDateEnd")]
        public string ShipDateEnd { get; set; }

        [JsonProperty("trackingNumberInfo")]
        public TrackingNumberInfo TrackingNumberInfo { get; set; }

    }
    public class TrackingNumberInfo
    {
        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }
    }
}
