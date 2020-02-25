using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.ApplicationLayer
{
    public class UserDetailDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsBanned { get; set; }
    }
}
