﻿using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace BaiTap4.Controllers
{
    public class TheLoaiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            return Ok("Id: " + id);
        }
        public IActionResult Detail(int id, string ten)
        {
            // Ok("Id: " + id +" ; ten: "+ ten);
            return Content(String.Format("id: {0}; ten: {1}", id, ten));
        }
        public IActionResult Show(List<string> categories)
        {
            string content = "Danh sách thể loại: ";
            foreach (var category in categories)
            {
                content = content + " " + category + ", ";
            }
            return Content(content);
        }
    }
}
    

