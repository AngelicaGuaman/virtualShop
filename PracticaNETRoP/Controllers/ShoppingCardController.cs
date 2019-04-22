using Microsoft.AspNet.Identity;
using PracticaNETRoP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PracticaNETRoP.Controllers
{
    public class ShoppingCardController : Controller
    {
        private VirtualShopEntities db = new VirtualShopEntities();

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
            string email = User.Identity.GetUserName();

         
            return RedirectToAction("OrderCreated");
        }
    }
}