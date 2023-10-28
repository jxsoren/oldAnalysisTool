using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AnalysisTool.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace AnalysisTool.Controllers
{
    

    public class HomeController : Controller
    {
        private const string MimeType = "text/csv";
        private const string MimeTypeZip = "application/zip";
        private const string MimeTypePDF = "application/pdf";

        private readonly ILogger<HomeController> _logger;

        readonly IDisposable _disposable;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Rates()
        {
            return View();
        }

        public IActionResult DHL()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetRates(RatesInputForm formData)
        {
            var rc = new RatesController();
            rc.ServicesRequest(formData.customer, formData.apiKey, formData.carrier, formData.maxWeight).Wait();
            rc.RatesRequests().Wait();
            //return View();
            return File(rc.archiveFile, MimeTypeZip, formData.customer + $" {formData.carrier} rate verification.zip");
        }

        [HttpPost]
        public IActionResult DHLManifest(DHLinfo formData)
        {
            var dhl = new DHLController();
            dhl.RequestToken(formData.clientSecret, formData.clientId, formData.pickup);
            return null;
        }

        public ActionResult Privacy(FormDataModel formData)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Summary(FormDataModel formData, IFormFile theFile)
        {
            var dc = new DataController();
            dc.LoadDataToTable(formData);

            //return File(dc.data, MimeType, dc.outputFileName);
            return File(dc.archiveFile, MimeTypeZip, dc.outputFileName.Replace(".csv", ".zip"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
