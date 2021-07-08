using e_commerce.Data;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.BasketCount = 0;
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                ViewBag.BasketCount = products.Count;
            }

            return View(await Task.FromResult(ViewBag.BasketCount));
        }
    }
}
