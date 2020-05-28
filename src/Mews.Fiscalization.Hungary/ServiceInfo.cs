using System;
using System.Collections.Generic;

namespace Mews.Fiscalization.Hungary
{
    internal static class ServiceInfo
    {
        public static string Version  { get; }

        public static string XmlNamespace { get; }

        public static Dictionary<NavEnvironment, Uri> BaseUrls { get; }

        public static Uri RelativeServiceUrl { get; }

        static ServiceInfo()
        {
            Version = "2.0";
            XmlNamespace = "http://schemas.nav.gov.hu/OSA/2.0/api";
            BaseUrls = new Dictionary<NavEnvironment, Uri>
            {
                [NavEnvironment.Test] = new Uri("https://api-test.onlineszamla.nav.gov.hu"),
                [NavEnvironment.Live] = new Uri("https://api.onlineszamla.nav.gov.hu")
            };
            RelativeServiceUrl = new Uri("invoiceService/v2/", UriKind.Relative);
        }
    }
}