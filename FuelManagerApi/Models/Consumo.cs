using System.ComponentModel.DataAnnotations;

namespace FuelManagerApi.Models
{
    public class Consumo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="A descriçao deve ter no máximo 50 caracteres")]
        public string Descricao { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public decimal Valor {  get; set; }
        [Required]
        public TipoCombustivel TipoCombustivel { get; set; }

        [Required]
        public int VeiculoId {  get; set; }
        public Veiculo Veiculo { get; set; }
    }
    public enum TipoCombustivel
    {
        Diesel,
        Etanol,
        Gasolina
    }
}
