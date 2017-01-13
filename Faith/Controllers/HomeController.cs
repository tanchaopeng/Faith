using Faith.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faith.Controllers
{
    public class HomeController : Controller
    {
        BookClass c = new BookClass();
        // GET: Home
        public ActionResult Index()
        {
           // var html = Common.HttpHelper.HttpGet("http://zhannei.baidu.com/cse/search?s=287293036948159515&q=%E5%85%A8%E8%81%8C%E6%B3%95%E5%B8%88");
            c.List("全职法师");
            return View();
        }
    }
}