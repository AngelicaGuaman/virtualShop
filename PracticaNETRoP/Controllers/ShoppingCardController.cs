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
            Products products = sc.Find(x => x.Id == id);
            if (products != null)
            {
                sc.Remove(products);
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateOrder(ShoppingCard sc)
        {
            Orders order = new Orders();
            decimal amount = 0;
            foreach (Products product in sc)
            {
                Products productDb = db.Products.Find(product.Id);
                order.Products.Add(productDb);
                amount = amount + productDb.price;
                productDb.stock--;

                if (productDb.stock == PRODUCT_WITHOUT_STOCK)
                {
                    Stock stockDb = new Stock
                    {
                        idProduct = productDb.Id,
                        units = productDb.stock
                    };

                    // TODO ver el tipo de datos de la imagen
                    // productDb.image = "img/noProduct.jpg";
                    productDb.Stock1.Add(stockDb);
                }

                db.Entry(productDb).State = EntityState.Modified;
            }

            order.dateCreation = DateTime.Now;
            string userId = User.Identity.GetUserId();
            order.idClient = userId;

            //TODO relación entre pedido y factura con esto ya sacamos al cliente
            Invoices invoice = new Invoices
            {
                dateInvoice = DateTime.Now,
                amount = amount
            };

            db.Orders.Add(order);
            db.SaveChanges();
            sc.Clear();

            return RedirectToAction("OrderCreated");
        }
    }
}