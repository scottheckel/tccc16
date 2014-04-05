using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoProgressive.Models;

namespace TodoProgressive.Api.Controllers
{
    public class TodoController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Todo> Get()
        {
            using (var context = new TodoContext())
            {
                return context.Todos.ToList();
            }
        }

        // GET api/<controller>/5
        public Todo Get(int id)
        {
            using (var context = new TodoContext())
            {
                return context.Todos.Single(t => t.TodoId == id);
            }
        }

        // POST api/<controller>
        public Todo Post([FromBody]Todo value)
        {
            using (var context = new TodoContext())
            {
                context.Todos.Add(value);
                context.SaveChanges();
                return value;
            }
        }

        // PUT api/<controller>/5
        public Todo Put(int id, [FromBody]Todo value)
        {
            using (var context = new TodoContext())
            {
                var oldTodo = context.Todos.Single(t => t.TodoId == value.TodoId);
                oldTodo.Text = value.Text;
                oldTodo.IsComplete = value.IsComplete;
                oldTodo.Location = value.Location;
                context.SaveChanges();
                return oldTodo;
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            using (var context = new TodoContext())
            {
                var todo = context.Todos.Single(t => t.TodoId == id);
                context.Todos.Remove(todo);
                context.SaveChanges();
            }
        }
    }
}