using AutoMapper;
using FuelManagerApi.Models;
using FuelManagerApi.Models.DTO.VeiculoDTO;
using FuelManagerApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuelManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly IBaseRepository<Veiculo> _repository;
        private readonly IMapper _mapper;

        public VeiculosController(IBaseRepository<Veiculo> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/<VeiculosController>
        [HttpGet]
        public ActionResult<IEnumerable<VeiculoDTO>> GetAll()
        {
            var veiculos = _repository.GetAll();
            var result = _mapper.Map<IEnumerable<VeiculoDTO>>(veiculos);
            return Ok(result);
        }

        // GET api/<VeiculosController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var veiculo = _repository.Get(v=>v.Id == id);
            if(veiculo == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<VeiculoDTO>(veiculo);
            return Ok(result);
        }

        // POST api/<VeiculosController>
        [HttpPost]
        public IActionResult Post([FromBody] VeiculoDTOCreate veiculo)
        {
            var veiculoEntity = _mapper.Map<Veiculo>(veiculo);
            var veiculoCriado = _repository.Add(veiculoEntity);
            var result = _mapper.Map<VeiculoDTO>(veiculoCriado);
            return new CreatedAtActionResult(nameof(Get),"Veiculos", new { Id = result.Id }, result);
        }

        // PUT api/<VeiculosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VeiculoDTOUpdate veiculo)
        {
            if(id != veiculo.Id)
                return BadRequest();

            var veiculoEntity = _mapper.Map<Veiculo>(veiculo);
            var veiculoModificado = _repository.Update(veiculoEntity);
            var result = _mapper.Map<VeiculoDTOUpdateResult>(veiculoModificado);
            return Ok(result);
        }

        // DELETE api/<VeiculosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var veiculoExistente = _repository.Get(v => v.Id == id);
            if (veiculoExistente == null)
                return NotFound();
            _repository.Delete(veiculoExistente);
            return NoContent();
        }
    }
}
