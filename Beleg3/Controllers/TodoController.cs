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
                    var todoList = user.Todos.ToList();
                 /*Nachdem virtual Membervariable zu IdentityModels hinzugefügt habe, kann auf users Membervariable statt auf Datenbank zugegriffen werden
                  * kann auch noch bei den anderen Methoden implementiert werden.. 
                  *     var list = db.Todo.Where(b => b.Owner.Id == user.Id).ToList();
                        List<TodoModel> todoList = new List<TodoModel>();
                        foreach (var item in list){
                        var todo = new TodoModel();
                        todo = item;
                        todoList.Add(todo);
                    }*/
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
                        var user = db.Users.First(u => u.Email == loggedInUser);
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
            using (var db = new Models.ApplicationDbContext())
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var loggedInUser = HttpContext.User.Identity.Name;
                    var user = db.Users.First(u => u.Email == loggedInUser);
                    var list = db.Todo.Where(b => b.Id == id);
                return View(list.First());

                }
            }
            //else redirect to login?!
            return View();
        }

        // POST: Todo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TodoModel model)
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
                        var list = db.Todo.Where(b => b.Id == model.Id);
                        list.First().Titel = model.Titel;
                        list.First().Description = model.Description;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    } else
                    {
                        //redirect to login?!
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Todo/Delete/5
        public ActionResult Delete(int id)
        {
            try { 
            using (var db = new Models.ApplicationDbContext())
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var list = db.Todo.Where(b => b.Id == id);
                    return View(list.First());
                }
                else
                {
                    //redirect to login?!
                    return RedirectToAction("Index");
                }
            }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Todo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var db = new Models.ApplicationDbContext())
                {
                    if (HttpContext.User.Identity.IsAuthenticated)
                    {
                        var list = db.Todo.Where(b => b.Id == id);
                        db.Todo.Remove(list.First());
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //redirect to login?!
                        return RedirectToAction("Index");
                    }
                }  
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
