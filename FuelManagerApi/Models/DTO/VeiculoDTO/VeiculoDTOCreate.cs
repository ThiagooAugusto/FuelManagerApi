using System.ComponentModel.DataAnnotations;

namespace FuelManagerApi.Models.DTO.VeiculoDTO
{
    public class VeiculoDTOCreate
    {
        [Required]
        [StringLength(20, ErrorMessage = "Máximo de 20 caracteres!")]
        public string Marca { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Máximo de 20 caracteres!")]
        public string Modelo { get; set; }
        [Required]
        [StringLength(7, ErrorMessage = "Máximo de 7 caracteres!")]
        public string Placa { get; set; }
        [Required]
        [Range(1900, 2024, ErrorMessage = "Ano de fabricação inválido!")]
        public int AnoFabricacao { get; set; }
        [Required]
        [Range(1900, 2024, ErrorMessage = "Ano de Modelo inválido!")]
        public int AnoModelo { get; set; }
    }
}
