using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaAPI.Dao;
using PruebaTecnicaAPI.Model;

namespace PruebaTecnicaAPI.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDao _usuarioDao;

        public UsuarioController(UsuarioDao usuarioDao)
        {
            _usuarioDao = usuarioDao;
        }

        /// GET ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UsuarioData>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioDao.GetUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener usuarios", error = ex.Message });
            }
        }

        /// GET BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioData>> Get(int id)
        {
            try
            {
                var usuario = await _usuarioDao.GetUsuarioById(id);
                
                if (usuario == null)
                {
                    return NotFound(new { message = $"Usuario con ID {id} no encontrado" });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener usuario", error = ex.Message });
            }
        }

        /// CREATE
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioData>> Post([FromBody] UsuarioData usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrWhiteSpace(usuario.Nombre))
                {
                    return BadRequest(new { message = "El nombre es requerido" });
                }

                if (string.IsNullOrWhiteSpace(usuario.Sexo))
                {
                    return BadRequest(new { message = "El sexo es requerido" });
                }

                var nuevoId = await _usuarioDao.CreateUsuario(usuario);
                usuario.Id = nuevoId;

                return CreatedAtAction(nameof(Get), new { id = nuevoId }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear usuario", error = ex.Message });
            }
        }

        /// UPDATE
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioData>> Put(int id, [FromBody] UsuarioData usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrWhiteSpace(usuario.Nombre))
                {
                    return BadRequest(new { message = "El nombre es requerido" });
                }

                if (string.IsNullOrWhiteSpace(usuario.Sexo))
                {
                    return BadRequest(new { message = "El sexo es requerido" });
                }

                usuario.Id = id;

                var actualizado = await _usuarioDao.UpdateUsuario(usuario);

                if (!actualizado)
                {
                    return NotFound(new { message = $"Usuario con ID {id} no encontrado" });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar usuario", error = ex.Message });
            }
        }

        /// DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var eliminado = await _usuarioDao.DeleteUsuario(id);

                if (!eliminado)
                {
                    return NotFound(new { message = $"Usuario con ID {id} no encontrado" });
                }

                return Ok(new { message = "Usuario eliminado correctamente", id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar usuario", error = ex.Message });
            }
        }
    }
}
