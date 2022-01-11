using Dominio.Entities.ModelUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Initialization.models
{
    public class UserInitialization
    {
        public static void Initialize(PersistenceContext persistenceContext)
        {
            var user = new User()
            {
                Name = "James Vanstrahlen",
                Email = "jamesv@gmail.com",
                Password = "qwerty123",
            };

            persistenceContext.User.Add(user);
            persistenceContext.SaveChanges();
        }

    }
}
