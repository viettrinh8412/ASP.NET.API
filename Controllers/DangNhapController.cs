using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DanhBaDienThoai.Controllers
{
    public class DangNhapController : Controller
    {
        public void Test()
        {
            string username = "trinhctv";
            string password = "trinhctv";
            var plainTextByte = System.Text.Encoding.ASCII.GetBytes(username + ":" + password);
            string encode = System.Convert.ToBase64String(plainTextByte);
            var request = WebRequest.Create(@"http://danhbadienthoai.somee.com/api/taikhoan");
            request.Headers.Add("Authorization", "Basic " + encode);
            try
            {
                var t = request.GetResponse();
            }
            catch (WebException we)
            {
                
            }
        }
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }
    }
}