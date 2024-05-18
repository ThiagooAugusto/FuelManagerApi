using AutoMapper;
using FuelManagerApi.Models;
using FuelManagerApi.Models.DTO;
using FuelManagerApi.Models.DTO.ConsumoDTO;
using FuelManagerApi.Models.DTO.VeiculoDTO;
using FuelManagerApi.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuelManagerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumosController : ControllerBase
    {
        private readonly IBaseRepository<Consumo> _repository;
        private readonly IConsumosRepository _consumosRepository;
        private readonly IMapper _mapper;

        public ConsumosController(IBaseRepository<Consumo> repository, IConsumosRepository consumosRepository, IMapper mapper)
        {
            _repository = repository;
            _consumosRepository = consumosRepository;
            _mapper = mapper;
        }



        // GET: api/<ConsumosController>
        [HttpGet]
        public ActionResult<IEnumerable<ConsumoDTO>> GetAll()
        {
           var consumosEntity =  _repository.GetAll();
           var result = _mapper.Map<IEnumerable<ConsumoDTO>>(consumosEntity);
            return Ok(result);
            
        }

        // GET api/<ConsumosController>/5
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var consumo = _repository.Get(c=>c.Id == id);
            if(consumo == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ConsumoDTO>(consumo);
            GerarLinks(result);
            return Ok(result);
        }

        [HttpGet("consumos/{id}",Name = "GetByVeiculo")]
        public ActionResult GetByVeiculo(int id)
        {
            var consumos = _consumosRepository.GetConsumosPorVeiculo(id);
            if(consumos == null)
                return NotFound();
            var result = _mapper.Map<IEnumerable<ConsumoDTO>>(consumos);
            return Ok(result);
        }

        // POST api/<ConsumosController>
        [HttpPost]
        public IActionResult Post([FromBody] ConsumoDTOCreate consumoDto)
        {
            var consumo = _mapper.Map<Consumo>(consumoDto);
            var consumoCriado = _repository.Add(consumo);
            var result = _mapper.Map<ConsumoDTO>(consumoCriado);
            return new CreatedAtActionResult(nameof(Get), "Consumos", new {Id = result.Id} , result);
        }

        // PUT api/<ConsumosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ConsumoDTOUpdate consumoDto)
        {
            if (id != consumoDto.Id)
                return BadRequest();
            

            var consumo = _mapper.Map<Consumo>(consumoDto);
            var consumoModificado = _repository.Update(consumo);
            var result = _mapper.Map<ConsumoDTOUpdateResult>(consumoModificado);
            return Ok(result);
        }

        // DELETE api/<ConsumosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
          var consumo = _repository.Get(c=>id == c.Id); 
          if (consumo == null)
                return NotFound();
          return NoContent();

        }
        private void GerarLinks(ConsumoDTO model)
        {
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "Delete"));

        }
    }
}
