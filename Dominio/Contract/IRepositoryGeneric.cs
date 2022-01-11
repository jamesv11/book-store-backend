using Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contract
{
    public interface IRepositoryGeneric<T> where T: DomainEntity
    {
        Task<List<T>> ObtenerTodos(Func<IQueryable<T>, IOrderedQueryable<T>> ordenarPor, int pagina = 1,
           int tamanio = int.MaxValue);
        Task<T> ObtenerPrimeroODeterminado(Expression<Func<T, bool>> predicado);
        Task<List<T>> ObtenerPor(Expression<Func<T, bool>> filtro = null, string propiedadesIncluidas = "",
            Func<IQueryable<T>, IOrderedQueryable<T>> ordenarPor = null);
        Task Agregar(T entidad);
        Task AgregarEnRango(List<T> entidades);
        Task Editar(T entidad);
        Task Remover(T entidad);
        Task RemoverEnRango(List<T> entidades);
        Task<IEnumerable<T>> ObtenerDonde(Expression<Func<T, bool>> predicado);
        Task<int> ContarTodos();
        Task<int> ContarDonde(Expression<Func<T, bool>> predicado);
        Task<bool> ExisteRegistro(Expression<Func<T, bool>> predicado);
    }
}
