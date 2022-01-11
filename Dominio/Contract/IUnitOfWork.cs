using Dominio.Repositories.RepositoriesBook;
using Dominio.Repositories.RepostiroriesUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contract
{
    public interface IUnitOfWork:IDisposable

    {
        #region[Respositorios]
        public IRepositoryBook RepositoryBook { get; }
        public IRepositoryUser RepositoryUser { get; }

#endregion

        void Commit();
        void RollBack();
        Task SaveChanges();
        void BeginTransaction();
    }
}
