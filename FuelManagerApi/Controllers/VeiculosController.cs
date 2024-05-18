using AutoMapper;
using FuelManagerApi.Models;
using FuelManagerApi.Models.DTO;
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
    public class VeiculosController : ControllerBase
    {
        private readonly IBaseRepository<Veiculo> _repository;
        private readonly IBaseRepository<VeiculoUsuarios> _veiculoUsuariosRepository;
        private readonly IVeiculosRepository _veiculosRepository;
        private readonly IMapper _mapper;

        public VeiculosController(IBaseRepository<Veiculo> repository,IBaseRepository<VeiculoUsuarios> veiculoUsuariosRepository,IVeiculosRepository veiculosRepository, IMapper mapper)
        {
            _repository = repository;
            _veiculosRepository = veiculosRepository;
            _veiculoUsuariosRepository = veiculoUsuariosRepository;
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
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var veiculo = await _veiculosRepository.GetVeiculoById(id);
            if(veiculo == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<VeiculoDTO>(veiculo);
            GerarLinks(result);
            return Ok(result);
        }

        // POST api/<VeiculosController>
        [HttpPost]
        public IActionResult Post([FromBody] VeiculoDTOCreate veiculo)
        {
            var veiculoEntity = _mapper.Map<Veiculo>(veiculo);
            var veiculoCriado = _repository.Add(veiculoEntity);
            var result = _mapper.Map<VeiculoDTO>(veiculoCriado);
            return new CreatedAtActionResult(nameof(Get),"Veiculos", new { id = result.Id }, result);
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

        private void GerarLinks(VeiculoDTO model)
        {
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "Delete"));

        }
        [HttpPost("{id}/usuarios")]
        public ActionResult AdicionarUsuario(int id,[FromBody] VeiculoUsuarios model)
        {
           if(id!= model.VeiculoId)
                return BadRequest();
            var usuarioAdicionado =  _veiculoUsuariosRepository.Add(model);
            return new CreatedAtActionResult(nameof(Get), "Veiculos", new { id = model.VeiculoId }, model);
            
        }

        [HttpDelete("{id}/usuarios/{usuarioId}")]
        public  ActionResult RemoverUsuario(int id, int usuarioId)
        {
            var model = _veiculoUsuariosRepository.Get(v => v.VeiculoId == id && v.UsuarioId == usuarioId);
            if(model==null) return NotFound();

            _veiculoUsuariosRepository.Delete(model);
            return NoContent();
        }
    }
}
