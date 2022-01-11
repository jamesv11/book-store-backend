using Aplicacion.Http.Input;
using Aplicacion.Services.ServiceBook;
using AutoMapper;
using Dominio.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace book_store_backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class BookController:ControllerBase
    {
        private readonly ServiceBook _serviceBook;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _serviceBook = new ServiceBook(unitOfWork, mapper);
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Create([FromBody] BookInput request)
        {
            var response = await _serviceBook.Create(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("{userEmail}")]
        public async Task<ActionResult<dynamic>> GetBooks(string userEmail)
        {
            var response = await _serviceBook.GetBooks(userEmail);
            return StatusCode((int)response.Code, response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> DeleteBooks(Guid id)
        {
            var response = await _serviceBook.DeleteBook(id);
            return StatusCode((int)response.Code, response);
        }
    }
}
