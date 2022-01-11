using Aplicacion.Base.Response;
using Aplicacion.Base.Service;
using Aplicacion.Http.Input;
using Aplicacion.Http.OutPut;
using AutoMapper;
using Dominio.Contract;
using Dominio.Entities.ModelUser;
using Dominio.Repositories.RepostiroriesUser;
using Infraestructura.Auth;
using Infraestructura.Encryp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services.ServiceAuth
{
    public class ServiceAuth : Service<User>
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IConfiguration _configuration;

        public ServiceAuth(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(unitOfWork, mapper)
        {
            _repositoryUser = unitOfWork.RepositoryUser;
            _configuration = configuration;
        }

        public async Task<Response<UserLoginOutput>> Login(UserLoginInput userLoginInput)
        {
            try
            {
                return await DoAuth(userLoginInput);
            }
            catch (Exception e)
            {
                UnitOfWork.RollBack();
                return Response<UserLoginOutput>.CreateFailResponse(e, HttpStatusCode.BadRequest,
                   "Credenciales incorrectas");
            }
        }

        private async Task<Response<UserLoginOutput>> DoAuth(UserLoginInput usuarioLoginInput)
        {
            var userResponse = await ValidateExistsUser(usuarioLoginInput);
            if (!userResponse)
            {
                return Response<UserLoginOutput>.CreateResponse(true);
            }


            var responseUserCredentials = await ValidateCredentialsUser(usuarioLoginInput);

            if (!responseUserCredentials.Item1)
            {
                return Response<UserLoginOutput>.CreateResponse(true);
            }

            var authentication = new Auth(_configuration);


            var userLoginOutput = new UserLoginOutput
            {
                Id = responseUserCredentials.user.Id,
                Email = responseUserCredentials.user.Email,
                Name = responseUserCredentials.user.Name,
                Token = authentication.GenerateToken(responseUserCredentials.user)

            };

            return Response<UserLoginOutput>.CreateSuccessResponse(userLoginOutput, HttpStatusCode.OK,
                "Actividad creada con exito");
        }

        private async Task<bool> ValidateExistsUser(UserLoginInput userLoginInput)
        {


            return await _repositoryUser.ExisteRegistro(
                p => p.Email.Equals(userLoginInput.Email)
            );
        }

        private async Task<(bool, User user)> ValidateCredentialsUser(UserLoginInput userLoginInput)
        {

            var userFound = (await _repositoryUser.ObtenerPor(u =>
              u.Email.Equals(userLoginInput.Email))).First();

            var credencialesCorrectas = (Hash.GetSha256(userLoginInput.Password) == userFound.Password);
            return (credencialesCorrectas, userFound);

        }
    }
}
