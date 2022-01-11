using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infraestructura.Base
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void SetModified(object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }

    public class DbContextBase : DbContext, IDbContext
    {
        private readonly IConfiguration Config;
        public DbContextBase(DbContextOptions options, IConfiguration config) : base(options)
        {
            Config = config;
        }
        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

    }
}
