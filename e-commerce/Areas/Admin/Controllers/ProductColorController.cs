﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Areas.Admin.Controllers
{
    public class ProductColorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
