using FuelManagerApi.Models;

namespace FuelManagerApi.Repository.Interfaces
{
    public interface IConsumosRepository:IBaseRepository<Consumo>
    {
        public IEnumerable<Consumo> GetConsumosPorVeiculo(int id);
    }
}
