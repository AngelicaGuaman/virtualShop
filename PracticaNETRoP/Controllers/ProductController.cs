﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticaNETRoP.Models;

namespace PracticaNETRoP.Controllers
{
    public class ProductController : Controller
    {
        private VirtualShopEntities db = new VirtualShopEntities();
        private const int PRODUCT_WITHOUT_STOCK = 2;

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // Add: Products
        public ActionResult Add(ShoppingCard sc, int id)
        {
            int numberOfProducts = 0;

            foreach (Products art in sc)
            {
                if (art.Id == id) { numberOfProducts++; }
            }

            Products product = db.Products.Find(id);

            if (numberOfProducts >= product.stock)
            {
                TempData["notice_error"] = "No existe suficiente stock para el producto solicitado";
            }
            else
            {
                sc.Add(db.Products.Find(id));
                TempData["notice_success"] = "Se ha Añadido el artículo al carrito";
            }

            return RedirectToAction("Index");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,description,price,stock,image")] Products products)
        {
            if (ModelState.IsValid)
            {
                if (products.stock < PRODUCT_WITHOUT_STOCK)
                {
                    Stock stock = new Stock
                    {
                        idProduct = products.Id,
                        units = products.stock
                    };

                    products.Stock1.Add(stock);
                }

                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,description,price,stock,image")] Products products)
        {
            if (ModelState.IsValid)
            {
                if (products.Stock1 != null && products.stock > PRODUCT_WITHOUT_STOCK)
                {
                    //TODO la relacion debe ser 1 a 1 --> Deuda técnica
                    foreach (var stock in products.Stock1)
                    {
                        products.Stock1.Remove(stock);
                    }

                }
                else
                {
                    Stock stock = new Stock
                    {
                        idProduct = products.Id,
                        units = products.stock
                    };

                    products.Stock1.Add(stock);
                }

                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
