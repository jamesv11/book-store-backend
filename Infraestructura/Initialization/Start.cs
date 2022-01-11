using Infraestructura.Initialization.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Initialization
{
    public class Start
    {
        private readonly PersistenceContext _context;

        public Start(PersistenceContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            if (!_context.User.Any()) UserInitialization.Initialize(_context);
        }
    }
}
