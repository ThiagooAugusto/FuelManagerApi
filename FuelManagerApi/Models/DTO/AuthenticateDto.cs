using System.ComponentModel.DataAnnotations;

namespace FuelManagerApi.Models.DTO
{
    public class AuthenticateDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="Campo Senha obrigatório!")]
        public string Password {  get; set; }

    }
}
