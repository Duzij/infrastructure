using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class User
    {
        public bool IsNotBanned => !IsBanned;
        public bool IsBanned { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}
