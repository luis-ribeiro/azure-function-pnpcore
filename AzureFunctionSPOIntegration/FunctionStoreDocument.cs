using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PnP.Core.Services;
using PnP.Core.Model.SharePoint;
using System.Linq;
using System.Collections.Specialized;
using System.Web;

namespace AzureFunctionSPOIntegration
{
    public class FunctionStoreDocument
    {
        private readonly IPnPContextFactory _pnpContextFactory;

        public FunctionStoreDocument(IPnPContextFactory pnpContextFactory)
        {
            this._pnpContextFactory = pnpContextFactory;
        }

        [FunctionName("FunctionStoreDocument")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,  ILogger _logger)
        {
            _logger.LogInformation("C# HTTP trigger function FunctionStoreDocument started");

            var file = req.Form.Files.FirstOrDefault();
            var country = req.Form["country"];
            var plant = req.Form["plant"];
            var machine = req.Form["machine"];

            using (var pnpContext = await _pnpContextFactory.CreateAsync("Default"))
            {
                IList sharedDocuments = await pnpContext.Web.Lists.GetByTitleAsync("Documents", l => l.RootFolder);

                var targetFolder = await sharedDocuments.RootFolder.EnsureFolderAsync($"/{country}/{plant}/{machine}");

                var uploadedDocument = await targetFolder.Files.AddAsync(file.FileName, file.OpenReadStream(), true);

                return new OkObjectResult(uploadedDocument.ServerRelativeUrl);
            }
        }
    }
}
