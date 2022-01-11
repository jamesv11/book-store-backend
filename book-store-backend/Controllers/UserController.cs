﻿using Aplicacion.Base.Response;
using Aplicacion.Http.Input;
using Aplicacion.Http.OutPut;
using Aplicacion.Services.ServiceUser;
using AutoMapper;
using Dominio.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_store_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {

        private readonly ServiceUser _serviceUser;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _serviceUser = new ServiceUser(unitOfWork, mapper);
        }

        [AllowAnonymous]
        [HttpPost]

        public async Task<ActionResult<dynamic>> Create([FromBody] UserInput request)
        {
            var respuesta = await _serviceUser.Create(request);
            return GetResponse(respuesta);
        }

        private ActionResult<dynamic> GetResponse(Response<UserOutput> response)
        {
            if (!response.Error)
            {
                return StatusCode((int)response.Code, response.Data);
            }

            var mensaje = new
            {
                Message = response.Message
            };
            return StatusCode((int)response.Code, mensaje);
        }
    }
}