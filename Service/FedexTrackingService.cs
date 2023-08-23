using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Shiptoboxes.External.Fedex.Tracking.Models;
using Shiptoboxes.External.Fedex.Tracking.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Shiptoboxes.External.Common.Exceptions.ExternalExceptionResource;

namespace Shiptoboxes.External.Fedex.Tracking.Service
{
    public class FedexTrackingService : IFedexTrackingService
    {
        private FedexTrackingServiceConfiguration _cfg;

        public void Configure(FedexTrackingServiceConfiguration cfg)
        {
            _cfg = cfg;
        }

        public async Task<FedexTrackingResponse> GetTrackingDetailByTrackingNumber(string trackingNumber)
        {
            try
            {
                var restClient = new RestClient("https://apis-sandbox.fedex.com/track/v1/trackingnumbers");
                var request = new RestRequest(Method.POST);
                request.AddHeader("authorization", "Bearer X");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("X-locale", "en_US");
                var req = new FedexTrackingRequest();
                var tcnInfo = new TrackingInfo();
                var trackingNumberInfo = new TrackingNumberInfo();
                tcnInfo.TrackingNumberInfo= trackingNumberInfo;
                tcnInfo.TrackingNumberInfo.TrackingNumber = trackingNumber;
                tcnInfo.ShipDateEnd = DateTime.Now.ToString("yyyy-MM-dd"); ;
                tcnInfo.ShipDateBegin = DateTime.Now.AddDays(-50).ToString("yyyy-MM-dd");
                var trackingList = new List<TrackingInfo>();
                trackingList.Add(tcnInfo);
                req.TrackingInfo = trackingList;
                req.IncludeDetailedScans=true;
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(JsonConvert.SerializeObject(req));
                var response = await restClient.ExecuteAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new SystemException(response.Content);
                }

                return JsonConvert.DeserializeObject<FedexTrackingResponse>(response.Content);
            }
            catch (Exception ex)
            {
                throw new FedexException(ex);
            }
        }

        public Task<FedexTrackingResponse> GetTrackingDetailByTrackingNumbers(List<string> trackingNumbers)
        {
            throw new NotImplementedException();
        }
    }
}
