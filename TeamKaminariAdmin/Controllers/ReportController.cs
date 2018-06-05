using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using TeamKaminariAdmin.Models;

namespace TeamKaminariAdmin.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private TeamKaminariAdminEntities _db = new TeamKaminariAdminEntities();

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            var model = new ReportViewModel()
            {
                StartDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now
            };

            ViewBag.NoModelMessage = "";
            return View(model);
        }

        public ActionResult GenerateReport()
        {
            // Report directory
            var reportDirRoot = Path.Combine(HttpRuntime.AppDomainAppPath, "Reports");
            if (!Directory.Exists(reportDirRoot))
                Directory.CreateDirectory(reportDirRoot);

            var filePath = Path.Combine(reportDirRoot, $@"CustomerReport_{DateTime.Now:yyyyMMdd}.csv");
            var customers = _db.SelectCustomers().ToList();

            using (TextWriter writer = new StreamWriter(filePath))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(customers);
            }

            var filedata = System.IO.File.ReadAllBytes(filePath);
            var contentType = MimeMapping.GetMimeMapping(filePath);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = Path.GetFileName(filePath),
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(filedata, contentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(DateTime? startDate, DateTime? endDate)
        {
            if (!endDate.HasValue)
                endDate = DateTime.Now;

            if (!startDate.HasValue)
                startDate = DateTime.Now.AddDays(-1);

            if (startDate > endDate)
            {
                ModelState.AddModelError("dateError","The start date cannot be greater than the end date.");
                return View();
            }

            var sessionData = _db.Sessions
                .Where(s => s.Date >= startDate.Value && s.Date <= endDate.Value)
                .Include(a => a.Attendances)
                .ToList();

            var attendanceIds = sessionData.SelectMany(sd => sd.Attendances.Select(s => s.CustomerId)).Distinct().ToList();
            var customers = _db.Customers.Where(f => attendanceIds.Contains(f.CustomerId)).ToList();
            
            var model = new ReportViewModel 
            {
                Customers = customers,
                Sessions = sessionData,
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };

            if (!customers.Any())
                ViewBag.NoModelMessage = "No results were returned for that date range.";
            
            return View(model);
        }
    }
}
