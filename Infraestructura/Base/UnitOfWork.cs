using Dominio.Contract;
using Dominio.Repositories.RepositoriesBook;
using Dominio.Repositories.RepostiroriesUser;
using Infraestructura.Repository.RepositoryBook;
using Infraestructura.Repository.RepositoryUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Base
{
    public class UnitOfWork : IUnitOfWork
    {

        private IDbContext _dbContext;
        private IDbContextTransaction _transaccion;

        public UnitOfWork(IDbContext context)
        {
            _dbContext = context;
        }

        private IRepositoryBook _repositoryBook;
        private IRepositoryUser _repositoryUser;

        public IRepositoryBook RepositoryBook => _repositoryBook ??=
            new RepositoryBook(_dbContext);
        public IRepositoryUser RepositoryUser => _repositoryUser ??=
            new RepositoryUser(_dbContext);

        public void Dispose()

        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || _dbContext == null) return;
            ((DbContext)_dbContext).Dispose();
            _dbContext = null;
        }

        public void BeginTransaction()
        {
            _transaccion = ((PersistenceContext)_dbContext).Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaccion?.Commit();
        }

        public void RollBack()
        {
            _transaccion?.Rollback();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
