using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FuelManagerApi.Models.DTO.UsuarioDTO
{
    public class UsuarioDTO
    {
       
        public int Id { get; set; }
       
        public string Nome { get; set; }
       
        public string Email { get; set; }

        public Perfil Perfil { get; set; }
        
        [JsonIgnore]
        public string Password {  get; set; }
        public ICollection<VeiculoUsuarios> Veiculos { get; set; }

    }
}
