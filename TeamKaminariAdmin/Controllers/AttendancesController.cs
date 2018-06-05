using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamKaminariAdmin.Models;

namespace TeamKaminariAdmin.Controllers
{
    [Authorize]
    public class AttendancesController : Controller
    {
        private TeamKaminariAdminEntities db = new TeamKaminariAdminEntities();

        public ActionResult Index(Guid? sessionId)
        {
            Guid id;
            if (sessionId.HasValue)
                id = sessionId.Value;
            else if (!Guid.TryParse(Url.RequestContext.RouteData.Values["id"]?.ToString(), out id))
                return View();

            var attendances = db.Attendances.Include(a => a.Customer).Include(a => a.Session).Where(i => i.SessionId == id);
            return View(attendances.ToList());
        }

        // GET: Attendances/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return PartialView("Search");
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            var model = new AttendanceViewModel
            {
                Customers = db.Customers.Where(c => c.Surname.StartsWith(query)).Take(5).ToList()
            };
            return PartialView("SearchResults", model);
        }

        // GET: Attendances/Create
        public ActionResult Create(Guid? id)
        {
            var model = new Attendance();
            if (id != null) model.SessionId = id.Value;
            ViewBag.SignedIn = TempData["signedInMessage"]?.ToString();
            return View(model);
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(AttendanceViewModel attendance, Guid? sessionId)
        {
            TempData["signedInMessage"] = "";
            if (!sessionId.HasValue)
            {
                ModelState.AddModelError("sessionId", "Could not select the correct session.");
                return View();
            }

            if (attendance == null)
            {
                ModelState.AddModelError("nullAttendanceModel", "An error has occurred.");
                return View();
            }
                
            // Get all the customers that have been selected
            var customersSelected = attendance.Customers.Where(c => c.CustomerSelected).ToList();
            if (!customersSelected.Any())
            {
                ModelState.AddModelError("noCustomers", "No customers appear to have been selected to add to the session.");
                return View();
            }
                
            foreach (var customer in customersSelected)
            {
                // If we have the customer sign them in to the session    
                var at = new Attendance
                {
                    AttendanceId = Guid.NewGuid(),
                    CustomerId = customer.CustomerId,
                    SessionId = sessionId.Value
                };
                db.Attendances.Add(at);
            }

            db.SaveChanges();
            TempData["signedInMessage"] = "Customer(s) signed in.";
            return RedirectToAction("Create", "Attendances", new { id=sessionId });
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceId,CustomerId,SessionId")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                attendance.AttendanceId = Guid.NewGuid();
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Forename", attendance.CustomerId);
            ViewBag.SessionId = new SelectList(db.Sessions, "SessionId", "SessionId", attendance.SessionId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Forename", attendance.CustomerId);
            ViewBag.SessionId = new SelectList(db.Sessions, "SessionId", "SessionId", attendance.SessionId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceId,CustomerId,SessionId")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Forename", attendance.CustomerId);
            ViewBag.SessionId = new SelectList(db.Sessions, "SessionId", "SessionId", attendance.SessionId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
