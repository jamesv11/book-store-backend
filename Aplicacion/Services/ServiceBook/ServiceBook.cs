using Aplicacion.Base.Response;
using Aplicacion.Base.Service;
using Aplicacion.Http.Input;
using Aplicacion.Http.OutPut;
using AutoMapper;
using Dominio.Contract;
using Dominio.Entities.ModelBook;
using Dominio.Repositories.RepositoriesBook;
using Dominio.Repositories.RepostiroriesUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services.ServiceBook
{
    public class ServiceBook: Service<Book>
    {
        private readonly IRepositoryBook _repositoryBook;
        private readonly IRepositoryUser _repositoryUser;

        public ServiceBook(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _repositoryBook = unitOfWork.RepositoryBook;
            _repositoryUser = unitOfWork.RepositoryUser;
        }

        public async Task<Response<BookOutput>> Create(BookInput bookInput)
        {
            try
            {
                return await DoRegister(bookInput);
            }
            catch (Exception e)
            {
                UnitOfWork.RollBack();
                return Response<BookOutput>.CreateFailResponse(e, HttpStatusCode.BadRequest,
                   "Ha sucedido un error al crear la actividad");
            }
        }

        public async Task<Response<BookOutput>> DeleteBook(Guid id)
        {
            try
            {
                return await DoDeleteBook(id);
            }
            catch (Exception e)
            {
                UnitOfWork.RollBack();
                return Response<BookOutput>.CreateFailResponse(e, HttpStatusCode.BadRequest,
                   "Ha sucedido un error al crear la actividad");
            }
        }

        private async Task<Response<BookOutput>> DoRegister(BookInput bookInput)
        {

            var userResponse = await ValidateExistsUser(bookInput.EmailUsuario);

            if (!userResponse)
            {
                return Response<BookOutput>.CreateResponse(true);
            }

            var userFound = (await _repositoryUser.ObtenerPor(u =>
                 u.Email.Equals(bookInput.EmailUsuario))).First();


            var book = new Book
            {
                Author = bookInput.Author,
                Title = bookInput.Title,
                Genre = bookInput.Genre,
                Price = bookInput.Price,
                Publisher = bookInput.Publisher,
                User = userFound
            };

            await _repositoryBook.Agregar(book);
            var bookOutput = Mapper.Map<BookOutput>(book);
            return Response<BookOutput>.CreateSuccessResponse(bookOutput, HttpStatusCode.OK,
                "Actividad creada con exito");
        }

        private async Task<Response<BookOutput>> DoDeleteBook(Guid id)
        {

            var bookExists = (await _repositoryBook.ExisteRegistro(u =>
                u.Id.Equals(id)));


            if (!bookExists)
                return Response<BookOutput>.CreateResponse(true);

            var bookFound = (await _repositoryBook.ObtenerPor(u =>
                 u.Id.Equals(id))).First();



            await _repositoryBook.Remover(bookFound);
            var bookOutput  = Mapper.Map<BookOutput>(bookFound);
            return Response<BookOutput>.CreateSuccessResponse(bookOutput, HttpStatusCode.OK,
                "Activiad eliminada con exito");
        }
        private async Task<bool> ValidateExistsUser(string email)
        {
            return await _repositoryUser.ExisteRegistro(
                p => p.Email.Equals(email)
            );
        }

        public async Task<Response<List<BookOutput>>> GetBooks(string userEmail)
        {
            try
            {
                return await DoGetBooks(userEmail);
            }
            catch (Exception e)
            {
                UnitOfWork.RollBack();
                return Response<List<BookOutput>>.CreateFailResponse(e, HttpStatusCode.BadRequest,
                   "Ha sucedido un error al consultar actividades");
            }
        }

        private async Task<Response<List<BookOutput>>> DoGetBooks(string userEmail)
        {

            var booksFound = (await _repositoryBook.ObtenerPor(u =>
                 u.User.Email.Equals(userEmail)));


            var booksOutput = Mapper.Map<List<BookOutput>>(booksFound);
            return Response<List<BookOutput>>.CreateSuccessResponse(booksOutput, HttpStatusCode.OK,
                "Estado actividad actualizado con exito");
        }


    }
}
