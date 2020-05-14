using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using DanhBaDienThoai.Models;

namespace DanhBaDienThoai.Controllers
{
    public class DBDienThoaiController : Controller
    {
        private DanhBaDienThoaiEntities db = new DanhBaDienThoaiEntities();
        private Uri Uri()
        {
            string domain = Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            return new Uri("http://danhbadienthoai.somee.com/api/");
        }

        // GET: DBDienThoai
        public ActionResult Index()
        {
            IEnumerable<ThongTinLienHe> ttlh = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = Uri();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "cXVhbnRzOnF1YW50cw==");
                //HTTP GET
                var responseTask = client.GetAsync("danhba");

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ThongTinLienHe>>();
                    readTask.Wait();

                    ttlh = readTask.Result;
                }
                else //web api sent error response 
                {
                    ttlh = Enumerable.Empty<ThongTinLienHe>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(ttlh);
        }

        // GET: DBDienThoai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinLienHe thongTinLienHe = new ThongTinLienHe();
            using (var client = new HttpClient())
            {
                client.BaseAddress = Uri();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "cXVhbnRzOnF1YW50cw==");
                //HTTP GET
                var responseTask = client.GetAsync("danhba/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ThongTinLienHe>();
                    readTask.Wait();

                    thongTinLienHe = readTask.Result;
                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(thongTinLienHe);
        }

        // GET: DBDienThoai/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DBDienThoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoTen,BietDanh,NgaySinh,SoDienThoai,Email,DiaChi")] ThongTinLienHe thongTinLienHe)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = Uri();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "cXVhbnRzOnF1YW50cw==");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ThongTinLienHe>("danhba", thongTinLienHe);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(thongTinLienHe);
        }

        // GET: DBDienThoai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinLienHe thongTinLienHe = db.ThongTinLienHes.Find(id);
            if (thongTinLienHe == null)
            {
                return HttpNotFound();
            }
            return View(thongTinLienHe);
        }

        // POST: DBDienThoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoTen,BietDanh,NgaySinh,SoDienThoai,Email,DiaChi")] ThongTinLienHe thongTinLienHe)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = Uri();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "cXVhbnRzOnF1YW50cw==");

                //HTTP POST

                var putTask = client.PutAsJsonAsync<ThongTinLienHe>("danhba/", thongTinLienHe);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(thongTinLienHe);
        }

        // GET: DBDienThoai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinLienHe thongTinLienHe = db.ThongTinLienHes.Find(id);
            if (thongTinLienHe == null)
            {
                return HttpNotFound();
            }
            return View(thongTinLienHe);
        }

        // POST: DBDienThoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = Uri();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "cXVhbnRzOnF1YW50cw==");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("danhba/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

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
