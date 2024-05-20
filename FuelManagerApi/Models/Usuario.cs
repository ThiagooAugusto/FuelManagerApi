using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FuelManagerApi.Models
{
    public class Usuario
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [JsonIgnore]
        public string Password {  get; set; }

        public Perfil Perfil { get; set; }

        public ICollection<VeiculoUsuarios> Veiculos { get; set;}

    }

    public enum Perfil
    {
        Administrador = 1,
        Usuario = 2
    }
}
