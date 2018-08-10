using BoroWinnicaStore.Core.Contracts;
using BoroWinnicaStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoroWinnicaStore.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        internal IRepository<ProductCategory> context;

        public ProductCategoryController(IRepository<ProductCategory> ProductCategories)
        {
            context = ProductCategories;
        }


        // GET: ProductCategory
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.ItemsCollection().ToList();
            return View(productCategories);
        }

        //// GET: ProductCategory/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ProductCategory/Create
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        // POST: ProductCategory/Create
        [HttpPost]
        public ActionResult Create(ProductCategory ProdCategory)
        {
            if (!ModelState.IsValid)
                return View(ProdCategory);
            else
            {
                context.Insert(ProdCategory);
                context.Comit();
                return RedirectToAction("Index");
            }
            
        }

        // GET: ProductCategory/Edit/5
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
                return HttpNotFound();
            else
                return View(productCategoryToEdit);
        }

        // POST: ProductCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(string Id, ProductCategory ProdCategory)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                    return View(ProdCategory);

                productCategoryToEdit.Category = ProdCategory.Category;
                context.Comit();
                return RedirectToAction("Index");

            }
            
        }

        // GET: ProductCategory/Delete/5
        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
                return HttpNotFound();
            else
                return View(productCategoryToDelete);

        }

        // POST: ProductCategory/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
                return HttpNotFound();
            else
            {
                context.Delete(Id);
                context.Comit();
                return RedirectToAction("Index");
            }

        }
    }
}
