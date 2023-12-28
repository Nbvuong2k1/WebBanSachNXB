using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index()
        {
            var items = db.Products.ToList();

            return View(items);
        }



        public ActionResult Search(string searchString, int? categoryId)
        {
            List<Product> searchResults;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (categoryId.HasValue)
                {
                    searchResults = (from p in db.Products
                                     join cp in db.ProductCategories on p.ProductCategoryId equals cp.Id
                                     where (p.Title.Contains(searchString) || p.Author.Contains(searchString)) &&
                                           cp.Id == categoryId.Value
                                     select p).ToList();
                }
                else
                {
                    searchResults = db.Products
                        .Where(x => x.Title.Contains(searchString) || x.Author.Contains(searchString))
                        .ToList();
                }
            }
            else
            {
                if (categoryId.HasValue)
                {
                    searchResults = db.Products
                        .Where(x => x.ProductCategoryId == categoryId.Value)
                        .ToList();
                }
                else
                {
                    searchResults = db.Products.ToList();
                }
            }

            return View("_SearchResult", searchResults); // Trả về view với danh sách sản phẩm đã lọc
        }


        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            var countReview = db.Reviews.Where(x => x.ProductId == id).Count();
            ViewBag.CountReview = countReview;
            return View(item);
        }
        public ActionResult ProductCategory(string alias, int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }



        public ActionResult PlayAudio()
        {
            {
                // Lấy dữ liệu âm thanh từ sản phẩm đầu tiên trong cơ sở dữ liệu
                var audioData = db.Products.FirstOrDefault()?.AudioGioiThieu;

                if (audioData != null)
                {
                    // Trả về file âm thanh cho trình duyệt để phát
                    return File(audioData, "audio/mpeg");
                }
            }
            return HttpNotFound(); // Trả về 404 nếu không tìm thấy dữ liệu âm thanh
        }

    }
}