using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TodoProgressive.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext()
            : base()
        {

        }

        public DbSet<Todo> Todos { get; set; }
    }
}