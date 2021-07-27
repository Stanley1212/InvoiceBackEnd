using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        IQueryable<T> Get(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null);
        IEnumerable<D> GetWithRawSql<D>(string query, object[] parameters, Expression<Func<T, D>> selectFn = null);
        void Delete(T entity);
        void Delete(object id);
        Task<int> Commit();
    }
}
