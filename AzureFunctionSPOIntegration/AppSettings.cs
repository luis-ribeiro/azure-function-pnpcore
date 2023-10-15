using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionSPOIntegration
{
    public class AppSettings
    {
        public string SiteUrl { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public StoreName CertificateStoreName { get; set; }
        public StoreLocation CertificateStoreLocation { get; set; }
        public string CertificateThumbprint { get; set; }
    }
}
