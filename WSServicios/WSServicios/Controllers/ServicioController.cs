using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSServicios.Models;

namespace WSServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public ServicioController(ApplicationDbContext context)
        {

            this.context = context;
        
        }
        [Route("ConsultarServicios")]
        [HttpPost()]
        public IActionResult ConsultarServicios()
        {

            return Ok(context.tblServicio);
        
        }

    }
}
