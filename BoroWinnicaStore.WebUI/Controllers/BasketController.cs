using BoroWinnicaStore.Core.Contracts;
using BoroWinnicaStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoroWinnicaStore.WebUI.Controllers
{
    public class BasketController : Controller
    {

        IRepository<Customer> Customers;
        IBasketService BasketService;
        IOrderService OrderService;


        public BasketController(IBasketService basketService, IOrderService orderService, IRepository<Customer> customers)
        {
            Customers = customers;
            BasketService = basketService;
            OrderService = orderService;
        }

        // GET: Basket
        public ActionResult Index()
        {
            var model = BasketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            BasketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            BasketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = Customers.ItemsCollection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ZipCode = customer.ZipCode,
                };
                return View(order);
            }
            else
                return RedirectToAction("Error");

        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var basketItems = BasketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;

            //payment process

            order.OrderStatus = "Payment Processed";
            OrderService.CreateOrder(order, basketItems);
            BasketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Thanks", new { OrderId = order.Id});
        }

        public ActionResult Thanks(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
        
        
    }
}