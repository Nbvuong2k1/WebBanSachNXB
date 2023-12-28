using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

public class Intro1Controller : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    //public IntroController(YourDbContext context)
    //{
    //    _context = context;
    //}

    // Action để hiển thị trang giới thiệu
    public ActionResult Index()
    {
        var intros = db.Intros.ToList();
        return View(intros);
    }

    public ActionResult MenuLeftIntro(int? id)
    {


        var intros = db.Intros.ToList(); // Lấy danh sách các đối tượng Intro từ cơ sở dữ liệu

        return PartialView("_MenuLeftIntro", intros);
    }

    // Action để hiển thị chi tiết bài giới thiệu
    public ActionResult Details(int id)
    {
        Intro intros = db.Intros.FirstOrDefault(i => i.Id == id);
        return PartialView("Details", intros);
    }
}
