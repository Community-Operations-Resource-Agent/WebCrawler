using System;
using Microsoft.AspNetCore.Mvc;

namespace CrawlerDEmoApp.Controllers
{
    public class DemoAppController : Controller
    {
        // GET: /<controller>/
        public IActionResult CalPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crawl()
        {
            try
            {
            }
            catch (Exception)
            {
                ViewBag.Result = "Crawl";
            }
            return View("CrawlerDemo");
        }

        [HttpPost]
        public IActionResult Query()
        {
            try
            {
            }
            catch (Exception)
            {
                ViewBag.Result = "Query";
            }
            return View("CrawlerDemo");
        }

        [HttpPost]
        public IActionResult Test()
        {
            try
            {
            }
            catch (Exception)
            {
                ViewBag.Result = "Test";
            }
            return View("CrawlerDemo");
        }

        [HttpPost]
        public IActionResult DeleteDB()
        {
            try
            {
            }
            catch (Exception)
            {
                ViewBag.Result = "DeleteDB";
            }
            return View("CrawlerDemo");
        }
    }
}
