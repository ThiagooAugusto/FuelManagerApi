using AutoMapper;
using FuelManagerApi.Models;
using FuelManagerApi.Models.DTO.ConsumoDTO;
using FuelManagerApi.Models.DTO.VeiculoDTO;
using System.Runtime.InteropServices;

namespace FuelManagerApi.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<VeiculoDTO,Veiculo>().ReverseMap();
            CreateMap<VeiculoDTOCreate, Veiculo>().ReverseMap();
            CreateMap<VeiculoDTOUpdate, Veiculo>().ReverseMap();
            CreateMap<Veiculo, VeiculoDTOUpdateResult>();
            CreateMap<ConsumoDTO, Consumo>().ReverseMap();
        }
    }
}
