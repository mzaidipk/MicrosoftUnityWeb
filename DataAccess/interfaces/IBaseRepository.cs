using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IBaseRepository<T> where T : class
    {
        void Delete(T entity);
        void Delete(IList<T> entityList);
        void Delete(object id);
        T ExecuteSP(string spName, params SqlParameter[] parameters);
        IEnumerable<T> ExecuteSPList(string spName, params SqlParameter[] parameters);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, string orderBy = "", string includes = "");
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includes = "");
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, string orderBy = "", params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(out int totalRows, Expression<Func<T, bool>> filter = null, string orderBy = "", int startRowIndex = 0, int? maximumRows = default(int?), string includes = "");
        IQueryable<T> GetAll(out int totalRows, string filter = null, string orderBy = "", int startRowIndex = 0, int? maximumRows = default(int?), params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(out int totalRows, Expression<Func<T, bool>> filter = null, string orderBy = "", int startRowIndex = 0, int? maximumRows = default(int?), params Expression<Func<T, object>>[] includes);
        T GetById(object id);
        T GetOne(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);
        T GetOne(Expression<Func<T, bool>> condition, string includes = "");
        void Insert(IList<T> entityList);
        void Insert(T entity);
        void Update(IList<T> entityList);
        void Update(T entity);
    }
}
