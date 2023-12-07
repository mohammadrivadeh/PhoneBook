using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    internal class Person
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Mobile { get; set; }
        public string Email { get; set; }

    }
}
