﻿namespace FuelManagerApi.Models.DTO.VeiculoDTO
{
    public class VeiculoDTO
    {
        public int Id { get; set; }
        public string Marca {  get; set; }
        public string Modelo {  get; set; }
        public string Placa {  get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }

        public ICollection<Consumo> Consumos { get; set; }
    }
}
