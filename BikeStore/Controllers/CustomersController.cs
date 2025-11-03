using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeStore.Controllers
{
    public class CustomersController : Controller
    {
        private BikeStoreContext db = new BikeStoreContext();
        // GET: Customers
        public async Task<ActionResult> Index()
        {
            var customers = await db.Customers.ToListAsync();
            return View(customers);
        }

        //GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        // GET: Customers/Create
        public async Task<ActionResult> Create()
        {
            return PartialView("_Create");
        }
        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "customer_Id,first_Name,last_Name,email,phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return PartialView("_Create", customer);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}