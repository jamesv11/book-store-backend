using Aplicacion.Http.Input;
using Aplicacion.Http.OutPut;
using AutoMapper;
using Dominio.Entities.ModelUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services.Mapping
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<UserInput, User>();
            CreateMap<User, UserOutput>();
        }
    }
}
