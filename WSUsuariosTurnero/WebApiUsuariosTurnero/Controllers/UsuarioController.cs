namespace WSUsuariosTurnero.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using WSUsuariosTurnero.Models;

    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext context;

        public UsuarioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Route("ConsultarUsuariosActivos")]
        [HttpPost]
        public IEnumerable<Usuario> ConsultarUsuariosActivos()
        {
            return context.Usuarios.Where(x=> x.Estado==1).ToList();

        }
        [Route("ConsultarUsuario")]
        [HttpPost]
        public IActionResult ConsultarUsuario([FromBody] Usuario Idusuario)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Identificacion == Idusuario.Identificacion);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        [Route("ConsultarUsuarioRol")]
        [HttpPost]
        public IActionResult ConsultarUsuarioRol([FromBody] Usuario RolUsuario)
        {
            var usuario = context.Usuarios.Where(x => x.Interaccion == RolUsuario.Interaccion).ToList();
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [Route("ConsultarUsuarioRolLogueado")]
        [HttpPost]
        public IActionResult ConsultarUsuarioRolLogueado([FromBody] Usuario RolUsuario)
        {
            var usuario = context.Usuarios.Where(x => x.Interaccion == RolUsuario.Interaccion && x.FechaLogueo==DateTime.Today).ToList();
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [Route("ObtenerAsesorePorID")]
        [HttpPost]
        public IActionResult ObtenerAsesorePorID(string asesoresPorAgrupador)
        {
            if (asesoresPorAgrupador == null)
            {
                return BadRequest();
            }
            List<string> asesorFiltrado = new List<string>();
            for (int i = 0; asesoresPorAgrupador.ToList().Count > i; i++)
            {
                asesorFiltrado.Add(context.Usuarios.FirstOrDefault(x => x.Id == asesoresPorAgrupador.First()).ToString());
            }
            return Ok(asesorFiltrado);
        }

        [Route("AgregarUsuario")]
        [HttpPost]
        public IActionResult AgregarUsuario([FromBody] Usuario usuario)
        {
            var existeUsuario = new Usuario();
            existeUsuario = context.Usuarios.FirstOrDefault(validUsuario => validUsuario.Identificacion == usuario.Identificacion);
            if (ModelState.IsValid && existeUsuario != null)
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return new CreatedAtRouteResult("usuarioCreado", new { id = usuario.Id });
            }
            else
            {
                return new CreatedAtRouteResult("usuarioNoCreado", new { id = 0 });
            }
        }
        [Route("ActualizarUsuario")]
        [HttpPost]
        public IActionResult ActualizarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.Identificacion == 0)
                {
                    return BadRequest();
                }
                context.Entry(usuario).State = EntityState.Modified;
                context.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
            return Ok(usuario);
        }

        [Route("ActualizarUsuarioEstado")]
        [HttpPost]
        public IActionResult ActualizarUsuarioEstado([FromBody] Usuario usuario)
        {
            if (usuario.Id == 0)
            {
                return BadRequest();
            }
            Usuario usu = new Usuario();
            usu = context.Usuarios.First(x => x.Id == usuario.Id);
            usu.Estado = usuario.Estado;
            context.Entry(usu).State = EntityState.Modified;
            context.SaveChanges();
            return Ok(usu);
        }

        [HttpPost]
        [Route("ObtenerIdUsuario")]
        public IActionResult ObtenerIdUsuario([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = context.Usuarios.FirstOrDefault(x => x.Email == userInfo.Email);
                if (usuario.Id != 0)
                {
                    usuario.Estado = 1;
                    usuario.FechaLogueo = DateTime.Today;
                    context.Entry(usuario).State = EntityState.Modified;
                    context.SaveChanges();
                    return Ok(usuario);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
   
}