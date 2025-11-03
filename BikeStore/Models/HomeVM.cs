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

namespace BikeStore.Models
{
    public class HomeVM
    {
        public List<staff> Staff { get; set; }
        public List<customer> Customers { get; set; }
        public List<product> Products { get; set; }
        public List<brand> Brands { get; set; }
        public List<category> Categories { get; set; }

        //for filters
        public string SelectedBrand { get; set; }
        public string SelectedCategory { get; set; }
    }
}