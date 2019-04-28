using PracticaNETRoP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace PracticaNETRoP.Models.Binders
{
    public class ShoppingCardBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ShoppingCard shoppingCard = (ShoppingCard)controllerContext.HttpContext.Session["KEY"];

            if (shoppingCard == null)
            {
                shoppingCard = new ShoppingCard();
                controllerContext.HttpContext.Session["KEY"] = shoppingCard;
            }
            return shoppingCard;
        }
    }
}