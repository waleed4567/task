using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project1.Data;
using project1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook<Books> _book;
        private readonly IBook<Auther> _auther;
        [Obsolete]
        private readonly IHostingEnvironment _hosting;
        public BookController(IBook<Books> book, IBook<Auther> auther, IHostingEnvironment hosting)
        {

            _book = book;
            _auther = auther;
            _hosting = hosting;
        }
        public IActionResult Export(int page = 1)
        {
            var list = _book.list();

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
            foreach (var book in data)
            {
                builder.AppendLine($"{book.Id},{book.Title}, {book.Description}, {book.autherId}, {book.Image}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "books.csv");
        }
        // GET: BookController
        public ActionResult BookList(int page = 1)
        {
            var list = _book.list();

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
            foreach(var book in data)
            {
                builder.AppendLine($"{book.Id},{book.Title}, {book.Description}, {book.autherId}, {book.Image}");
            }
            return View(data);
        }
        public ActionResult Search(string term)
        {
            var result = _book.Search(term);
            return View("BookList", result);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = _book.find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var bookList = _book.list();
            var modle = new BookAutherViewModles
            {

                Authers = selectList(),


            };

            return View(modle);
        }

        // POST: BookController/Create
        // POST: BookController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create([FromForm] BookAutherViewModles modle)
        {

            String fileName = String.Empty;


            if (ModelState.IsValid)
            {
                try
                {

                    if (modle.File != null)
                    {



                        string dir = Path.Combine(_hosting.WebRootPath, "images");
                        fileName = modle.File.FileName;

                        string fulPath = Path.Combine(dir, fileName);

                        modle.File.CopyTo(new FileStream(fulPath, FileMode.Create));


                    }

                    // modle.Id = autoId;
                    var Authers = _auther.find(modle.AutherId);

                    if (modle.AutherId == -1)
                    {

                        ViewBag.Masseage = "please selec one of the Authers";



                        return View(getallAuther());


                    }



                    var books = new Books
                    {




                        Title = modle.Title,

                        Description = modle.Description,

                        Image = fileName,

                        Auther = Authers,
                        Id = modle.Id, 




                    };

                    _book.add(books);
                    _book.SaveChanges();
                    return RedirectToAction(nameof(BookList));


                }
                catch
                {
                    return View();
                }

            }
            return RedirectToAction(nameof(BookList));

        }


        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {

            var bookData = _book.find(id);




            var modle = new BookAutherViewModles
            {


                Authers = _auther.list().ToList(),

                Title = bookData.Title,

                Description = bookData.Description,

                image = bookData.Image,






                AutherId = bookData.Auther.id,








            };

            //var book = Auther.find(id);
            return View(modle);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] BookAutherViewModles modle)
        {
            String fileName = String.Empty;


            if (ModelState.IsValid)
            {
                try
                {

                    if (modle.File != null)
                    {



                        string dir = Path.Combine(_hosting.WebRootPath, "images");
                        fileName = modle.File.FileName;

                        string fulPath = Path.Combine(dir, fileName);

                        modle.File.CopyTo(new FileStream(fulPath, FileMode.Create));


                    }

                    // modle.Id = autoId;
                    var Authers = _auther.find(modle.AutherId);

                    if (modle.AutherId == -1)
                    {

                        ViewBag.Masseage = "please selec one of the Authers";



                        return View(getallAuther());


                    }



                    var books = new Books
                    {


                    

                        Title = modle.Title,

                        Description = modle.Description,

                        Image = fileName,

                        Auther = Authers,
                        Id = modle.Id




                    };

                    _book.update(1,books);
                    _book.SaveChanges();
                    return RedirectToAction(nameof(BookList));


                }
                catch
                {
                    return View();
                }

            }
            return RedirectToAction(nameof(BookList));

       

    }

    // GET: BookController/Delete/5
    public ActionResult Delete(int id, String name)
        {
            var book = _book.find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _book.delete(id);
                _book.SaveChanges();
                return RedirectToAction(nameof(BookList));
            }
            catch
            {
                return View();
            }
        }
            List<Auther> selectList()
            {


                var autherList = _auther.list().ToList();

                autherList.Insert(0, new Auther { id = -1, FullName = "---please select one Auther ---" });


                return autherList.ToList();
            }
            BookAutherViewModles getallAuther()
            {

                var modle = new BookAutherViewModles
                {


                    Authers = selectList()
                };
                return modle;

            }
        }
    }


