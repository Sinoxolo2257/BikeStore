using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Threading.Tasks; 
using BikeStore.Models; 

namespace BikeStore.Controllers
{
    public class HomeController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();
        // GET: Home/Index
        public async Task<ActionResult> Index(string brandFilter, string categoryFilter)
        {
            var vm = new HomeVM();

            vm.Staff = await db.staffs.ToListAsync();
            vm.Customers = await db.customers.ToListAsync();

            var productsQuery = db.products.Include(p => p.brand).Include(p => p.category).AsQueryable();

            if (!string.IsNullOrEmpty(brandFilter))
            {
                productsQuery = productsQuery.Where(p => p.brand.brand_name == brandFilter);
                vm.SelectedBrand = brandFilter;
            }

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                productsQuery = productsQuery.Where(p => p.category.category_name == categoryFilter);
                vm.SelectedCategory = categoryFilter;
            }

            vm.Products = await productsQuery.ToListAsync();
            vm.Brands = await db.brands.OrderBy(b => b.brand_name).ToListAsync();
            vm.Categories = await db.categories.OrderBy(c => c.category_name).ToListAsync();

            return View(vm);
        }

        // GET: /Home/Manage
        public ActionResult Manage()
        {
            // This view will include partials/links to Staffs/Customers/Products maintain pages
            return View();
        }

        // GET: /Home/Reports
        public ActionResult Reports()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}