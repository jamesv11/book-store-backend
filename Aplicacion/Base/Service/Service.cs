using AutoMapper;
using Dominio.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Base.Service
{
    public abstract class Service<T>: BaseService, IService<T>
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected IMapper Mapper;
        protected Service(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }

    public class BaseService
    {

    }
}
