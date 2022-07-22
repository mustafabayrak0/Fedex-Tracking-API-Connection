using Shiptoboxes.External.Common;
using System;

namespace Shiptoboxes.External.Fedex.Tracking.Service
{
    public class FedexTrackingServiceConfigurer
    {
        public FedexTrackingServiceConfiguration Init(string url, string username, string password)
        {
            return new FedexTrackingServiceConfiguration(url, username, password);
        }
    }

    public static class FedexTrackingServiceFactoryExtensions
    {
        public static IFedexTrackingService CreateFedexTrackingService(this ServiceFactory source)
        {
            return new FedexTrackingService();
        }

        public static void Start(this IFedexTrackingService service, Func<FedexTrackingServiceConfigurer, FedexTrackingServiceConfiguration> configurationDelegate)
        {
            service.Configure(configurationDelegate(new FedexTrackingServiceConfigurer()));
        }
    }

    public class FedexTrackingServiceConfiguration : IExternalServiceConfiguration
    {
        public string Url { get; }
        public string Username { get; }
        public string Password { get; }
        internal FedexTrackingServiceConfiguration(string url, string username, string password)
        {
            Url = url;
            Username = username;
            Password = password;
        }
    }
}
