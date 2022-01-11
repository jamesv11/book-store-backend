using Aplicacion.Base.Response;
using Aplicacion.Base.Service;
using Aplicacion.Http.Input;
using Aplicacion.Http.OutPut;
using AutoMapper;
using Dominio.Contract;
using Dominio.Entities.ModelUser;
using Dominio.Repositories.RepostiroriesUser;
using Infraestructura.Encryp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services.ServiceUser
{
    public class ServiceUser : Service<User>
    {
        private readonly IRepositoryUser _repositoryUser;

        public ServiceUser(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _repositoryUser = unitOfWork.RepositoryUser;

        }

        public async Task<Response<UserOutput>> Create(UserInput userInput)
        {
            try
            {
                return await DoRegister(userInput);
            }
            catch (Exception e)
            {
                UnitOfWork.RollBack();
                return Response<UserOutput>.CreateFailResponse(e, HttpStatusCode.BadRequest,
                   "Ha sucedido un error al crear el usuario");
            }
        }

        private async Task<Response<UserOutput>> DoRegister(UserInput userInput)
        {

            var userResponse = await ValidateExistsUser(userInput);

            if (userResponse)
            {
                return Response<UserOutput>.CreateResponse(true);
            }

            var user = new User
            {
                Email = userInput.Email,
                Name = userInput.Name,
                Password = Hash.GetSha256(userInput.Password)
            };

            await _repositoryUser.Agregar(user);
            var usuarioOutput = Mapper.Map<UserOutput>(user);
            return Response<UserOutput>.CreateSuccessResponse(usuarioOutput, HttpStatusCode.OK,
                "Actividad creada con exito");
        }

        private async Task<bool> ValidateExistsUser(UserInput userInput)
        {
            return await _repositoryUser.ExisteRegistro(
                p => p.Email.Equals(userInput.Email)
            );
        }
    }
}
