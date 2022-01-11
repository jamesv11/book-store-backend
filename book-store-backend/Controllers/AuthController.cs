using Aplicacion.Http.Input;
using Aplicacion.Services.ServiceAuth;
using AutoMapper;
using Dominio.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_store_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly ServiceAuth _serviceAuth;

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _serviceAuth = new ServiceAuth(unitOfWork, mapper, configuration);
        }

        // POST api/<AutenticacionController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserLoginInput request)
        {
            var respuesta = await _serviceAuth.Login(request);
            return StatusCode((int)respuesta.Code, respuesta.Data);
        }
    }
}
