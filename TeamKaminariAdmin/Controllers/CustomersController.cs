using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Owin.Security.Provider;
using TeamKaminariAdmin.Models;

namespace TeamKaminariAdmin.Controllers
{
    public class CustomersController : Controller
    {
        private TeamKaminariAdminEntities db = new TeamKaminariAdminEntities();

        // GET: Customers
        [Authorize]
        public ActionResult Index()
        {
            var customers = db.Customers.ToList();
            var today = DateTime.Today;

            // Loop through the customers and reset the License Paid flag to false if the licence reset date is equal to today or in the past.
            foreach (var customer in customers)
                customer.LicensePaid = customer.LicenseResetDate <= today;

            db.SaveChanges();

            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                ModelState.AddModelError("surnameEmpty", "The search field cannot be blank.");
                return View();
            }
                
            var customers = db.Customers.Where(c => c.Surname.StartsWith(query)).ToList();

            if (!customers.Any())
                ModelState.AddModelError("noCustomers", $"No customers that have a surname starting with '{query}' exist.");

            return View(customers);
        }

        // GET: Customers/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            customer.EmergencyContactList = customer.EmergencyContacts.ToList();
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize]
        public ActionResult Create()
        {
            var model = new Customer
            {
                BeltEnumerable = db.Belts.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.BeltColour, Selected = b.Id == 1 }).ToList()
            };

            return View(model);
        }

        [Authorize]
        public ActionResult CustomerBelts(Guid? customerId)
        {
            if (!customerId.HasValue)
                RedirectToAction("");

            var model = new CustomerBeltViewModel
            {
                CustomerBelts = db.CustomerBelts
                        .Where(cb => cb.CustomerId == customerId.Value)
                        .Include(c => c.Customer).ToList(),
                Belts = db.Belts.ToList()
            };

            // Set the boolean flags the checkbox will use.
            foreach (var customerBelt in model.CustomerBelts)
                customerBelt.Belt.BeltAchieved = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult BeltAcheived(CustomerBeltViewModel cb)
        {
            var belts = db.CustomerBelts.Where(c => c.CustomerId == cb.CustomerId).ToList();

            foreach (var belt in cb.Belts)
            {
                // get the cb already in database
                var customerBelt = belts.FirstOrDefault(b => b.BeltId == belt.Id);

                // If we don't have a customer belt and it has been achieved add it.
                if (customerBelt == null && belt.BeltAchieved)
                {
                    db.CustomerBelts.Add(new CustomerBelt
                    {
                        BeltId = belt.Id,
                        CustomerId = cb.CustomerId.Value,
                        Id = Guid.NewGuid(),
                        DateAchieved = DateTime.Now
                    });
                }

                // If a customer belt has been removed
                if (customerBelt != null && !belt.BeltAchieved)
                    db.CustomerBelts.Remove(customerBelt);
            }

            db.SaveChanges();
            return RedirectToAction("CustomerBelts", new {customerId = cb.CustomerId});
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Customer customer)
        {
            customer.BeltEnumerable = db.Belts.Select(b =>
                new SelectListItem { Value = b.Id.ToString(), Text = b.BeltColour, Selected = b.Id == 1 }).ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    customer.CustomerId = Guid.NewGuid();
                    customer.EmergencyContacts.Add(new EmergencyContact
                    {
                        EmergencyContactId = Guid.NewGuid(),
                        CustomerId = customer.CustomerId,
                        Forename = customer.EmergencyContact.Forename,
                        Surname = customer.EmergencyContact.Surname,
                        Tel1 = customer.EmergencyContact.Tel1,
                        Tel2 = customer.EmergencyContact.Tel2
                    });

                    // Create a customer belt instance
                    customer.CustomerBelts.Add(new CustomerBelt
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customer.CustomerId,
                        BeltId = byte.Parse(customer.BeltSelected),
                        DateAchieved = DateTime.Now
                    });

                    db.Customers.Add(customer);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            customer.BeltEnumerable = db.Belts.Select(b =>
                new SelectListItem { Value = b.Id.ToString(), Text = b.BeltColour, Selected = b.Id == 1 }).ToList();
            //customer.BeltSelected = customer.BeltColourId.ToString();
            customer.EmergencyContactList = customer.EmergencyContacts.ToList();
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                // Edit the emergency contacts
                var dbCustomer = db.Customers.Find(customer.CustomerId);
                var existingEmergencyContacts = dbCustomer?.EmergencyContacts;
                if (existingEmergencyContacts == null)
                    return View(customer);

                var ids = customer.EmergencyContactList.Select(ec => ec.EmergencyContactId).ToList();
                var existingContactsMatchingNewIds = existingEmergencyContacts.Where(c => ids.Contains(c.EmergencyContactId));

                foreach (var contact in existingContactsMatchingNewIds)
                {
                    var update =
                        customer.EmergencyContactList.FirstOrDefault(c =>
                            c.EmergencyContactId == contact.EmergencyContactId);

                    if (update != null)
                    {
                        contact.Forename = update.Forename;
                        contact.Surname = update.Surname;
                        contact.Tel1 = update.Tel1;
                        contact.Tel2 = update.Tel2;
                    }
                }

                //dbCustomer.BeltColourId = customer.BeltColourId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
