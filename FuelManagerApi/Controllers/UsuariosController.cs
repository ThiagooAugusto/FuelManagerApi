using AutoMapper;
using FuelManagerApi.Models;
using FuelManagerApi.Models.DTO.UsuarioDTO;
using FuelManagerApi.Models.DTO.VeiculoDTO;
using FuelManagerApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuelManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IBaseRepository<Usuario> _repository;
        private readonly IMapper _mapper;

        public UsuariosController(IBaseRepository<Usuario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET: api/<VeiculosController>
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioDTO>> GetAll()
        {
            var usuarios = _repository.GetAll();
            var result = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(result);
        }

        // GET api/<VeiculosController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var usuario = _repository.Get(v => v.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(result);
        }

        // POST api/<VeiculosController>
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioDTOCreate model)
        {

            var usuarioEntity = _mapper.Map<Usuario>(model);
            usuarioEntity.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var usuarioCriado = _repository.Add(usuarioEntity);
            var result = _mapper.Map<UsuarioDTO>(usuarioCriado);
            return new CreatedAtActionResult(nameof(Get), "Usuarios", new { Id = result.Id }, result);
        }

        // PUT api/<VeiculosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioDTOUpdate model)
        {
            if (id != model.Id)
                return BadRequest();

            var usuarioEntity = _mapper.Map<Usuario>(model);
            usuarioEntity.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var usuarioModificado = _repository.Update(usuarioEntity);
            var result = _mapper.Map<UsuarioDTOUpdateResult>(usuarioModificado);
            return Ok(result);
        }

        // DELETE api/<VeiculosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuarioExistente = _repository.Get(v => v.Id == id);
            if (usuarioExistente == null)
                return NotFound();
            _repository.Delete(usuarioExistente);
            return NoContent();
        }
    }
}
