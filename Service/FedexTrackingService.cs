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
                request.AddHeader("authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJDWFMiXSwiUGF5bG9hZCI6eyJjbGllbnRJZGVudGl0eSI6eyJjbGllbnRLZXkiOiJsN2YxYzFkMjMzMDA0YzQ3MTc5NTcwZGY3Y2FjOGEwOTA1In0sImF1dGhlbnRpY2F0aW9uUmVhbG0iOiJDTUFDIiwiYWRkaXRpb25hbElkZW50aXR5Ijp7InRpbWVTdGFtcCI6IjIyLUp1bC0yMDIyIDA4OjE3OjU4IEVTVCIsImFwaW1vZGUiOiJTYW5kYm94In0sInBlcnNvbmFUeXBlIjoiRGlyZWN0SW50ZWdyYXRvcl9CMkIifSwiZXhwIjoxNjU4NDk5NDc4LCJqdGkiOiJmNjFkZDQ2ZS1kMDE2LTRhNTgtOGZkYy05YzYyNWMxNDE4NzMiLCJjbGllbnRfaWQiOiJsN2YxYzFkMjMzMDA0YzQ3MTc5NTcwZGY3Y2FjOGEwOTA1In0.RuQfhUr2yZ47xVR4G07ODfbQ3c6fzrziy2xcZH7M6lOBbk52gL2ltYficC-4jA1dblAeioQcP4heta0uXV0QuabGchPF_q7eS23yC6gdLaT6NXwt6RdYetVBf_MPihNuDxGqeZsPx2FcAXbnyKV1YylVtLQtoSWvyiwEnWhvi5Gc5VFwbycSDoDAxQoe-1nWNnK0q4Qk5WYsIwAzSnseqgRSP7GS5C5k8DPpsb1UOwnxXRp782Qk5YV2hRuOEEbZuioZk08MyvbOzFHd0oPvrfr_t4sT9j0IyeZZjqTMoFAzmKUCLPEatOLoF7-YuLm0q-gFYiahxh69h5juwl4QfNkbCRyDG1xZBBhF2qqmNLDHz5Rvy6mt4tqE2xjxk80QSSWcbPjeS09wLcGcWfz9DY6Ws4q-OWk-x92YaN5z1HEPk-G04aF5K2D_VClAaYC-WYPjiTJSETIMJLdN9WnSBp6wwnA5sMuZGJ-Wt-_T8dHMy5Nk73Gs0OLmmWDCPzrX94A4oKbwTSYn_w18ezyetgiSho45nfJ0NOFI70GVTBGIlVcTrS-uYbIK4yZps6bOacxiww9GQSjBFXQMy9aQveWQYIQ-HjMhA8-cP1sqHlA-8AuSswTjk9hkxo36N9aGx1TkHdGwIU-vcWLbs3sJLLbYRAiM8GBx0LSw4FL5sxs");
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

        //public Task<FedexTrackingResponse> GetTrackingDetailByTrackingNumbers(FedexTrackingRequest fedexTrackingRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<FedexTrackingResponse> IFedexTrackingService.GetTrackingDetailByTrackingNumber(string trackingNumber)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<FedexTrackingResponse> IFedexTrackingService.GetTrackingDetailByTrackingNumbers(List<string> trackingNumbers)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
