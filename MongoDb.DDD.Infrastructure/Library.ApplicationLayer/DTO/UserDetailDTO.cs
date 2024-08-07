﻿using System.ComponentModel.DataAnnotations;

namespace Library.ApplicationLayer.DTO
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
