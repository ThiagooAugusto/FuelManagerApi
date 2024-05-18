using AutoMapper;
using FuelManagerApi.Models;
using FuelManagerApi.Models.DTO.UsuarioDTO;
using FuelManagerApi.Models.DTO.VeiculoDTO;
using FuelManagerApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using FuelManagerApi.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FuelManagerApi.Controllers
{
    [Authorize]
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
        // GET: api/<UsuariosController>
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioDTO>> GetAll()
        {
            var usuarios = _repository.GetAll();
            var result = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(result);
        }

        // GET api/<UsuariosController>/5
        [Authorize(Roles = "Administrador")]
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

        // POST api/<UsuariosController>
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioDTOCreate model)
        {

            var usuarioEntity = _mapper.Map<Usuario>(model);
            usuarioEntity.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var usuarioCriado = _repository.Add(usuarioEntity);
            var result = _mapper.Map<UsuarioDTO>(usuarioCriado);
            return new CreatedAtActionResult(nameof(Get), "Usuarios", new { Id = result.Id }, result);
        }

        // PUT api/<UsuariosController>/5
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

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuarioExistente = _repository.Get(v => v.Id == id);
            if (usuarioExistente == null)
                return NotFound();
            _repository.Delete(usuarioExistente);
            return NoContent();
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateDto model)
        {

            var usuarioEntity = _repository.Get(u=>model.Id ==  u.Id);
            if (usuarioEntity == null || !BCrypt.Net.BCrypt.Verify(model.Password,usuarioEntity.Password))
                return Unauthorized();
            var jwt = GenerateJwtToken(usuarioEntity);
            return Ok(new { jwtToken = jwt });
        }
        private string GenerateJwtToken(Usuario model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Ry74cBQva5dThwbwchR9jhbtRFnJxWSZ");
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Role, model.Perfil.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
