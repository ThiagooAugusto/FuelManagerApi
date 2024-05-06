using System.ComponentModel.DataAnnotations;

namespace FuelManagerApi.Models.DTO.UsuarioDTO
{
    public class UsuarioDTOUpdate
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Nome Obrigatório!")]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Email Obrigatório!")]
        [StringLength(50)]

        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Perfil Obrigatório!")]
        public Perfil Perfil { get; set; }
        [Required(ErrorMessage = "Campo Senha Obrigatório!")]
        public string Password { get; set; }
    }
}
