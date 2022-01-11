using Dominio.Entities.ModelBook;
using Dominio.Entities.ModelUser;
using Infraestructura.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura
{
    public class PersistenceContext : DbContextBase
    {
            
        public PersistenceContext(DbContextOptions options, IConfiguration config):base(options, config)
        {

        }

        #region DbSets
        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
