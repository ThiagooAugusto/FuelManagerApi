using FuelManagerApi.Context;
using FuelManagerApi.Models;
using FuelManagerApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FuelManagerApi.Repository
{
    public class VeiculosRepository : BaseRepository<Veiculo>, IVeiculosRepository
    {
       
        
        public VeiculosRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Veiculo>? GetVeiculoById(int id)
        {
           return await _context.Veiculos
                .Include(t => t.Usuarios).ThenInclude(t => t.Usuario)
                .Include(t => t.Consumos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
