using Beleg3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Beleg3.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        public ActionResult Index()
        {
            using (var db = new Models.ApplicationDbContext())
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var loggedInUser = HttpContext.User.Identity.Name;
                    var user = db.Users.First(u => u.Email == loggedInUser);
                    var list = db.Todo.Where(b => b.Owner.Id == user.Id).ToList();
                    List<TodoModel> todoList = new List<TodoModel>();
                    foreach (var item in list){
                        var todo = new TodoModel();
                        todo = item;
                        todoList.Add(todo);


                    }
                    return View(todoList);
                 
                }
            }
                return View();
        }

        // GET: Todo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Todo/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        public ActionResult Create(TodoModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (var db = new Models.ApplicationDbContext())
                {
                    if (HttpContext.User.Identity.IsAuthenticated)
                    {
                        var loggedInUser = HttpContext.User.Identity.Name;
                        var user = db.Users.First(u => u.Id == loggedInUser);
                        model.Owner = user;
                        db.Todo.Add(model);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
//                return View();
            }
        }

        // GET: Todo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Todo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Todo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Todo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
