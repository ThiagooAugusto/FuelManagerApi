using System.ComponentModel.DataAnnotations;

namespace FuelManagerApi.Models.DTO.ConsumoDTO
{
    public class ConsumoDTOUpdate
    {
        [Required(ErrorMessage ="Obrigatorio informar o id!")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório informar a descrição!")]
        [StringLength(50, ErrorMessage = "A descriçao deve ter no máximo 50 caracteres")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Obrigatório informar a data!")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o valor!")]
        [Range(0, 999, ErrorMessage = "Valores inválidos!")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o tipo de combustível!")]
        public TipoCombustivel TipoCombustivel { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Id do Veiculo!")]
        public int VeiculoId { get; set; }

    }
}
