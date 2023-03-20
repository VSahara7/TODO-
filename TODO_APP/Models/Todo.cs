using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.Models
{
    public class Todo
    {
        [Key]
        public int taskId { get; set; }

        public string taskName { get; set; }

        public DateTime creationDate { get; set; }
        public DateTime completionDate { get; set; }
    }
}
