using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikeStore.Models;

namespace BikeStore.Controllers
{
    public class StaffController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();
        // GET: Staff
        public async Task<ActionResult> Index()
        {
            var staff = await db.staffs.Include(s => s.store).ToListAsync();
            return View(staff);
        }

        //GET: Staff/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staff staff = await db.staffs.FindAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }
        // GET: Staff/Create
        public ActionResult Create()
        {
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name");
            return PartialView("_Create"); //modal creation uses parial view
        }
        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "staff_id,first_name,last_name,email,phone,active,store_id,manager_id")] staff staff)
        {
            if (ModelState.IsValid)
            {
                db.staffs.Add(staff);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", staff.store_id);
            return PartialView("_Create", staff);
        }

        // GET: Staff/Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staff staff = await db.staffs.FindAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", staff.store_id);
            return PartialView("_Edit", staff);
        }

        // POST: Staff/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "staff_id,first_name,last_name,email,phone,active,store_id,manager_id")] staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", staff.store_id);
            return PartialView("_Edit", staff);
        }

        // POST: Staff/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            staff staff = await db.staffs.FindAsync(id);
            db.staffs.Remove(staff);
            await db.SaveChangesAsync();
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