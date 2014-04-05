using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoProgressive.Models
{
    public class Todo
    {
        public int TodoId { get; set; }
        public string Text { get; set; }
        public bool IsComplete { get; set; }
        public string Location { get; set; }
    }
}