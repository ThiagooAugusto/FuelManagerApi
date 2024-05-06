using System.ComponentModel.DataAnnotations;

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
        public string Password {  get; set; }

        public ICollection<VeiculoUsuarios> Veiculos { get; set;}

    }
}
