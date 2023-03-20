using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_APP.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Mail")]
        public string UserMail { get; set; }
        [Required]
        [DisplayName("Password")]
        public string UserPassword { get; set; }
    }
}
