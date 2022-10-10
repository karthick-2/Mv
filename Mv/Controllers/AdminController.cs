using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Mv.Models;
using Mv.Repository;
using Mv.Unitofwork;
using Mv.Util;
using MvcPaging;
namespace Mv.Controllers
{
    public class AdminController : Controller
    {
        UnitOfwork utw;
        int defaultpage;
        IUtil _util;
        int width;
        int height;
        string path = String.Empty;
        /// <summary>
        ///            constructor 
        /// </summary>

        public AdminController()
        {
            this.width = Convert.ToInt32(ConfigurationManager.AppSettings["width"]);
            this.height = Convert.ToInt32(ConfigurationManager.AppSettings["height"]);
            this._util = new Utili();
            this.defaultpage = Convert.ToInt32(ConfigurationManager.AppSettings["defaultpage"]);
            this.utw = new UnitOfwork();
            this.path = ConfigurationManager.AppSettings["path"];
        }
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminModel use)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    webtbluser obj = new webtbluser();
                    obj = utw.userrepo.Get(f => f.username == use.username && f.userpassword == use.password && f.isactive == true && f.isdelete == false).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["user"] = use.username;
                        return RedirectToAction("Slider");
                    }
                }
            }
            catch { }
            return View();
        }
        [HttpGet]
        public ActionResult slider(string slidername, int? page)
        {
            if (Session["user"] == null)
                return RedirectToAction("Login");
            try
            {
                ViewData["slidername"] = slidername;
                IList<slider> obj = new List<slider>();
                obj = utw.sliderepo.Get(f => f.isactive == true && f.isdelete == false, order: e => e.OrderBy(f => f.id)).ToList();
                int currentpage = page.HasValue ? page.Value : 1;
                if (!string.IsNullOrWhiteSpace(slidername))
                {
                    obj.Where(f => f.slidername.ToLower().Contains(slidername)).ToList();
                }
                obj = obj.ToPagedList(currentpage, defaultpage);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("AjaxSlider", obj);
                }
                else
                {
                    return View(obj);
                }
            }
            catch { }
            return View();
        }

        public ActionResult bmslider(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Login");
            slider obj = new slider();
            try
            {
                int current = id.HasValue ? id.Value : 0;
                if (current > 0)
                    obj = utw.sliderepo.GetById(id);
                return View(obj);
            }
            catch { }
            return View(obj);
        }
        [HttpPost]
        public ActionResult bmslider(slider use, HttpPostedFileBase imgs)
        {
            if (Session["user"] == null)
                return RedirectToAction("Login");
            slider s = new slider();
            try
            {
                if (ModelState.IsValid)
                {
                    string filepath = "";
                    string filename = "";
                    Image image = null;
                    s = use;
                    if (imgs == null && use.sliderimage == null)
                    {
                        return RedirectToAction("bmslider", "Admin", new { id = use.id });
                    }
                    if (imgs != null)
                    {
                        string Extension = imgs.FileName.Remove(0, imgs.FileName.LastIndexOf('.'));
                        if (Extension != "")
                        {
                            if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".jpeg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".gif")
                            {
                                image = _util.ResizeImage(imgs.InputStream, width, height);
                                filename = Guid.NewGuid().ToString() + Extension.ToLower();
                                filepath = Server.MapPath("~/Up/") + filename;
                                s.sliderimage = filename;
                            }
                        }
                    }
                    if (imgs != null)
                    {
                        image.Save(filepath, ImageFormat.Png);
                    }

                    if (s.id == 0)
                    {
                        s.isdelete = false;
                        s.created = DateTime.Now;
                        utw.sliderepo.Insert(s);
                    }
                    else
                    {
                        s.isdelete = false;
                        s.Modified = DateTime.Now;
                        utw.sliderepo.Update(s);
                    }
                    utw.save();
                    return RedirectToAction("slider", "Admin");

                }
                else
                {
                    ViewBag.ErMsg = "please upload Png Format files";
                    return View(use);
                }
            }
            catch { }

            return View(s);
        }
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id > 0)
                {
                    slider obj = new slider();
                    obj = utw.sliderepo.GetById(id);
                    if (obj != null)
                    {
                        obj.isactive = false;
                        obj.isdelete = true;
                        utw.sliderepo.Update(obj);
                        utw.save();
                        return Json(new { Status = "true" });
                    }
                }
            }
            catch { }
            return null;
        }

        public ActionResult Logout()
        {

            if (Session["user"] == null)
                return RedirectToAction("Login");
            try
            {
                Session.Clear();
            }
            catch { }
            return RedirectToAction("Index", "Admin");
        }

    }
}