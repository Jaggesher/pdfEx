using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConverter _converter;
        private readonly IConfiguration _configuration;

        public HomeController(IHostingEnvironment hostingEnvironment, IConverter converter, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _converter = converter;
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            var baseUrl = _configuration.GetValue<string>("BaseURL");
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 10 },
                    //Out = _hostingEnvironment.ContentRootPath + "Document_" + DateTime.Now.ToString("yyyy-MM-dd_HH-ss") + ".pdf"
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,           
                        HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                        FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" },
                        Page = "https://www.w3schools.com/bootstrap/trybs_theme_band_full.htm",
                    }
                }
            };

            var file = _converter.Convert(doc);
            return File(file, "application/pdf");

            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
