using Aplicacion.Http.Input;
using Aplicacion.Http.OutPut;
using AutoMapper;
using Dominio.Entities.ModelBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services.Mapping
{
    public class BookMapping:Profile
    {
        public BookMapping()
        {
            CreateMap<BookInput, Book>();
            CreateMap<Book, BookOutput>();
        }
    }
}
