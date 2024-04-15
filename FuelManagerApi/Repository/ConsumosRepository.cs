using FuelManagerApi.Context;
using FuelManagerApi.Models;
using FuelManagerApi.Repository.Interfaces;

namespace FuelManagerApi.Repository
{
    public class ConsumosRepository : BaseRepository<Consumo>, IConsumosRepository
    {
        public ConsumosRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Consumo> GetConsumosPorVeiculo(int id)
        {
            return GetAll().Where(c => c.VeiculoId == id);
        }
    }
}
