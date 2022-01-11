using Dominio.Entities.ModelBook;
using Dominio.Repositories.RepositoriesBook;
using Infraestructura.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repository.RepositoryBook
{
    public class RepositoryBook:RepositoryGeneric<Book>, IRepositoryBook
    {
        public RepositoryBook(IDbContext context) : base(context)
        {

        }
    }
}
