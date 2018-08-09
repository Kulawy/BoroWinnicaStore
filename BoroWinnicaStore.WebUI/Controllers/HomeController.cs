using BoroWinnicaStore.Core.Contracts;
using BoroWinnicaStore.Core.Models;
using BoroWinnicaStore.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoroWinnicaStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        internal IRepository<Product> Context;
        internal IRepository<ProductCategory> ProductCategories;

        public HomeController(IRepository<Product> context, IRepository<ProductCategory> productCategoryContext)
        {
            Context = context;
            ProductCategories = productCategoryContext;

        }

        public ActionResult Index(string category=null)
        {
            List<Product> products;
            List<ProductCategory> categories = ProductCategories.ItemsCollection().ToList();

            if (category == null)
                products = Context.ItemsCollection().ToList();
            else
                products = Context.ItemsCollection().Where(p => p.Category == category ).ToList();

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            return View(model);
        }

        public ActionResult Details(string id)
        {
            Product product = Context.Find(id);
            if (product == null)
                return HttpNotFound();
            else
                return View(product);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}