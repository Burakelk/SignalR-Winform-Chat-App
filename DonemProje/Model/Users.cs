using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemProje.Model
{
    internal class Users
    {
        [Key]
        private int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }



    }
}
