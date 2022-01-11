using Dominio.Base;
using Dominio.Entities.ModelBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities.ModelUser
{
    public class User : Entity<int>
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Book> Books { get; set; }


        public User()
        {
            Books = new List<Book>();
        }

       
    }
}
