using Shiptoboxes.External.Fedex.Tracking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shiptoboxes.External.Fedex.Tracking.Service
{
    public interface IFedexTrackingService
    {
        void Configure(FedexTrackingServiceConfiguration configuration);
        Task<FedexTrackingResponse> GetTrackingDetailByTrackingNumbers(List<string> trackingNumbers);
        Task<FedexTrackingResponse> GetTrackingDetailByTrackingNumber(string trackingNumber);
    }
}
