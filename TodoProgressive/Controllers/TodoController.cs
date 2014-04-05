using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoProgressive.Models;

namespace TodoProgressive.Controllers
{
    public class TodoController : Controller
    {
        //
        // GET: /Todo/

        public ActionResult Index()
        {
            using (var context = new TodoContext())
            {
                return View(context.Todos.ToList());
            }
        }

        //
        // GET: /Todo/Details/5

        public ActionResult Details(int id)
        {
            using (var context = new TodoContext())
            {
                return View(context.Todos.Single(t => t.TodoId == id));
            }
        }

        //
        // GET: /Todo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Todo/Create

        [HttpPost]
        //public ActionResult Create(FormCollection collection)
        public ActionResult Create(Todo todo)
        {
            try
            {
                using (var context = new TodoContext())
                {
                    context.Todos.Add(todo);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Todo/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new TodoContext())
            {
                return View(context.Todos.Single(t => t.TodoId == id));
            }
        }

        //
        // POST: /Todo/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Todo todo)
        {
            try
            {
                using (var context = new TodoContext())
                {
                    var oldTodo = context.Todos.Single(t => t.TodoId == id);
                    oldTodo.Text = todo.Text;
                    oldTodo.IsComplete = todo.IsComplete;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                using (var context = new TodoContext())
                {
                    return View(context.Todos.Single(t => t.TodoId == id));
                }
            }
        }

        [HttpGet]
        public ActionResult Complete(int id)
        {
            using (var context = new TodoContext())
            {
                return View(context.Todos.Single(t => t.TodoId == id));
            }
        }

        [HttpPost]
        public ActionResult Complete(int id, bool? isComplete)
        {
            try
            {
                using (var context = new TodoContext())
                {
                    var oldTodo = context.Todos.Single(t => t.TodoId == id);
                    oldTodo.IsComplete = !oldTodo.IsComplete;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                using (var context = new TodoContext())
                {
                    return View(context.Todos.Single(t => t.TodoId == id));
                }
            }
        }

        //
        // GET: /Todo/Delete/5

        public ActionResult Delete(int id)
        {
            using (var context = new TodoContext())
            {
                return View(context.Todos.Single(t => t.TodoId == id));
            }
        }

        //
        // POST: /Todo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var context = new TodoContext())
                {
                    var todo = context.Todos.Single(t => t.TodoId == id);
                    context.Todos.Remove(todo);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
