using BoroWinnicaStore.Core.Contracts;
using BoroWinnicaStore.Core.Models;
using BoroWinnicaStore.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoroWinnicaStore.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategoriesContext;

        public ProductManagerController(IRepository<Product> Context, IRepository<ProductCategory> ProductCategoryContext)
        {
            context = Context;
            productCategoriesContext = ProductCategoryContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.ItemsCollection().ToList();
            return View(products);
        }

        //// GET: ProductManager/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ProductManager/Create
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategoriesContext.ItemsCollection();

            return View(viewModel);
        }

        // POST: ProductManager/Create
        [HttpPost]
        public ActionResult Create(Product Product, HttpPostedFileBase File)
        {
            if (!ModelState.IsValid)
            {
                return View();

                
            }
            else
            {
                if(File != null)
                {
                    Product.Image = Product.Id + Path.GetExtension(File.FileName);
                    File.SaveAs(Server.MapPath("//Content//ProductImages//") + Product.Image);
                }

                context.Insert(Product);
                context.Comit();

                return RedirectToAction("Index");
            }

        }

        // GET: ProductManager/Edit/5
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
                return HttpNotFound();
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategoriesContext.ItemsCollection();

                return View(viewModel);

            }

            
        }

        // POST: ProductManager/Edit/5
        [HttpPost]
        public ActionResult Edit(string Id, Product Product, HttpPostedFileBase File)
        {
            Product productToEdit = context.Find(Id);
            
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(Product);
                }

                if( File != null)
                {
                    productToEdit.Image = Product.Id + Path.GetExtension(File.FileName);
                    File.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = Product.Category;
                productToEdit.Description = Product.Description;
                productToEdit.Name = Product.Name;
                productToEdit.Price = Product.Price;

                context.Comit();

                return RedirectToAction("Index");
            }
            
        }

        // GET: ProductManager/Delete/5
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
                return View(productToDelete);
        }

        // POST: ProductManager/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete.Equals(null))
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
