using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project1.Data;
using project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1.Controllers
{
    public class AutherController : Controller
    {
        public IActionResult Export(int page = 1)
        {
            var list = _auther.list();

            const int pageSize = 2;
            if (page < 1)
                page = 1;
            int recsCount = list.Count;
            var pager = new Pagger(recsCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;



            var builder = new StringBuilder();
            builder.AppendLine("Id,Title,Description,autherId,Image");
            foreach (var auther in data)
            {
                builder.AppendLine($"{auther.id},{auther.FullName}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "authers.csv");
        }
        public ActionResult Search(string term)
        {
            var result = _auther.Search(term);
            return View("AutherList", result);
        }
        // GET: AutherController
        public ActionResult AutherList(int page = 1)
        {
            var list = _auther.list();
            const int pageSize = 2;
            if (page < 1)
                page = 1;
            int recsCount = list.Count;
            var pager = new Pagger(recsCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            var builder = new StringBuilder();
            builder.AppendLine("FullName");
            foreach (var auther in data)
            {
                builder.AppendLine($"{auther.id},{auther.FullName}");
            }
            return View(data);
        }
        private readonly IBook<Auther> _auther;
        public AutherController(IBook<Auther> auther)
        {
            _auther = auther;
        }

        // GET: AutherController/Details/5
        public ActionResult Details(int id)
        {
            var auther = _auther.find(id);
            return View(auther);
        }

        // GET: AutherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Auther collection)
        {
            try
            {
                _auther.add(collection);
                _auther.SaveChanges();
                return RedirectToAction(nameof(AutherList));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutherController/Edit/5
        public ActionResult Edit(int id)
        {
            var auther = _auther.find(id);
            return View(auther);
        }

        // POST: AutherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Auther collection)
        {
            try
            {
                _auther.update(id, collection);
                _auther.SaveChanges();
                return RedirectToAction(nameof(AutherList));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutherController/Delete/5
        public ActionResult Delete(int id)
        {
            var auther = _auther.find(id);
            return View(auther);
        }

        // POST: AutherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Auther collection)
        {
            try
            {
                _auther.delete(id);
                _auther.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
