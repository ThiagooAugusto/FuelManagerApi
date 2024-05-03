using System.ComponentModel.DataAnnotations;

namespace FuelManagerApi.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Marca { get; set; }
        [Required]
        [StringLength(20)]
        public string Modelo {  get; set; }
        [Required]
        [StringLength(7)]
        public string Placa {  get; set; }
        [Required]
        [Range(1900,2024)]
        public int AnoFabricacao {  get; set; }
        [Required]
        [Range(1900, 2024)]
        public int AnoModelo {  get; set; }

        public ICollection<Consumo> Consumos { get; set; }

    }
}
