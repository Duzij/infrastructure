using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.ApplicationLayer
{
    public class CreateUserDTO
    {
        public Guid Id { get; set; }

        public CreateUserDTO(Guid id)
        {
            Id = id;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
