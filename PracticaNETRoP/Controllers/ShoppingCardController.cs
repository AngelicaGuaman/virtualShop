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

        public ActionResult OrderCreated(ShoppingCard sc)
        {
            Order order = new Order();
            decimal amount = 0;
            foreach (Product product in sc)
            {
                Product productDb = db.Products.Find(product.Id);
                // order.Products.Add(productDb);
                amount = amount + productDb.price;
                productDb.stock--;

                if (productDb.stock <= PRODUCT_WITHOUT_STOCK)
                {
                    Stock stockDb = new Stock
                    {
                        idProduct = productDb.Id,
                        units = productDb.stock
                    };

                    // TODO ver el tipo de datos de la imagen
                    productDb.image = "../../img/noProduct.jpg";
                    productDb.Stocks.Add(stockDb);
                }

                ProductOrder productOrderDb = db.ProductOrders.Find(order.Id, productDb.Id);

                if (productOrderDb == null)
                {
                    ProductOrder productOrder = new ProductOrder
                    {
                        orderId = order.Id,
                        productId = productDb.Id,
                        units = 1
                    };

                    order.ProductOrders.Add(productOrder);
                }
                else
                {
                    productOrderDb.units++;
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
    }
}