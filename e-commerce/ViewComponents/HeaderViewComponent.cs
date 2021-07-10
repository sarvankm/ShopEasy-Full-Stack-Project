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
            List<int> Count = new List<int>();
         
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                Count.Add(products.Count());
            }
            else
            {
                Count.Add(0);
            }
            if (Request.Cookies["favorite"] != null)
            {
                List<FavoriteVM> products = JsonConvert.DeserializeObject<List<FavoriteVM>>(Request.Cookies["favorite"]);
                Count.Add(products.Count());
            }
            else
            {
                Count.Add(0);
            }
            ViewBag.Count = Count;
            return View(await Task.FromResult(ViewBag.Count));
        }
    }
}
