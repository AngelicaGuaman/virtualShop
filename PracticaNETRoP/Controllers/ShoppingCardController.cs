using Microsoft.AspNet.Identity;
using PracticaNETRoP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PracticaNETRoP.Controllers
{
    public class ShoppingCardController : Controller
    {
        private VirtualShopEntities db = new VirtualShopEntities();
        private const int PRODUCT_WITHOUT_STOCK = 2;

        // GET: ShoppingCard
        public ActionResult Index(ShoppingCard sc)
        {
            return View(sc);
        }

        // GET: ShoppingCard/Delete/5
        public ActionResult Delete(int? id, ShoppingCard sc)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = sc.Find(x => x.Id == id);
            if (product != null)
            {
                sc.Remove(product);
            }
            return RedirectToAction("Index");
        }

        public ActionResult NewOrder(ShoppingCard sc)
        {
            Order order = new Order();
            decimal amount = 0;
            foreach (Product product in sc)
            {
                Product productDb = db.Products.Find(product.Id);
                // order.Products.Add(productDb);
                amount = amount + productDb.price;
                productDb.stock--;

                if (productDb.stock < PRODUCT_WITHOUT_STOCK)
                {
                    Stock stockDb = new Stock
                    {
                        idProduct = productDb.Id,
                        units = productDb.stock
                    };

                    //productDb.image = "~/Content/img/noProduct.jpg";
                    productDb.Stocks.Add(stockDb);
                }

                if (order.ProductOrders == null)
                {
                    ProductOrder productOrder = new ProductOrder
                    {
                        Order = order,
                        Product = product,
                        units = 1
                    };

                    order.ProductOrders.Add(productOrder);
                }
                else
                {
                    foreach (ProductOrder prodOrder in order.ProductOrders)
                    {
                        if (prodOrder.productId == productDb.Id)
                        {
                            prodOrder.units++;
                        }
                    }

                    db.Entry(order).State = EntityState.Modified;
                }

                db.Entry(productDb).State = EntityState.Modified;
            }

            order.creationDate = DateTime.Now;
            string userId = User.Identity.GetUserId();
            order.clientId = userId;

            Invoice invoice = new Invoice
            {
                creationDate = DateTime.Now,
                amount = amount
            };

            order.Invoices.Add(invoice);
            db.Orders.Add(order);
            db.SaveChanges();
            sc.Clear();

            return RedirectToAction("OrderCreated");
        }

        public ActionResult OrderCreated()
        {
            return View();
        }
    }
}