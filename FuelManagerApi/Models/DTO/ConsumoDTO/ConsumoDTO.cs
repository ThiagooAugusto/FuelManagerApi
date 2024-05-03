

namespace FuelManagerApi.Models.DTO.ConsumoDTO
{
    public class ConsumoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }

        public TipoCombustivel TipoCombustivel { get; set; }
        public int VeiculoId { get; set; }
    }
}
