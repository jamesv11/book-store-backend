using Dominio.Entities.ModelUser;
using Dominio.Repositories.RepostiroriesUser;
using Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repository.RepositoryUser
{
    public class RepositoryUser: RepositoryGeneric<User>, IRepositoryUser
    {
        public RepositoryUser(IDbContext context): base(context) { }
    }
}
