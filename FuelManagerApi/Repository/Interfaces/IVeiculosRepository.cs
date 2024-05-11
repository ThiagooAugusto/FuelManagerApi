using FuelManagerApi.Models;

namespace FuelManagerApi.Repository.Interfaces
{
    public interface IVeiculosRepository:IBaseRepository<Veiculo>
    {
        Task<Veiculo> GetVeiculoById(int id);
    }
}
