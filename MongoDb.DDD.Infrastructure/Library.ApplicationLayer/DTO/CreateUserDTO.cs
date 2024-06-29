using System.ComponentModel.DataAnnotations;

namespace Library.ApplicationLayer.DTO
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
