using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.SqlClient;
//using System.Linq.Dynamic;
using Utilities.Linq;

namespace DataAccess
{
    public sealed class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Variables

        /// <summary>
        /// The context
        /// </summary>
        internal DbContext context;

        /// <summary>
        /// The db set
        /// </summary>
        internal DbSet<T> dbSet;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            var objectContext = (this.context as System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = 60;
        }

        #endregion

        #region Implemented Methods

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Insert(T entityToInsert)
        {
            dbSet.Add(entityToInsert);
        }

        /// <summary>
        /// Inserts the specified entity list.
        /// </summary>
        /// <param name="entityList">The entity list.</param>
        public void Insert(IList<T> entityList)
        {
            foreach (T entity in entityList)
            {
                dbSet.Add(entity);
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        public void Update(T entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = context.Entry<T>(entityToUpdate);


            if (entry.State == EntityState.Detached)
            {
                var props = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));

                var set = context.Set<T>();
                T attachedEntity = set.Local.SingleOrDefault(e => props.All(p => p.GetValue(e).Equals(p.GetValue(entityToUpdate))));  // You need to have access to key

                if (attachedEntity != null && attachedEntity != entityToUpdate)
                {
                    var attachedEntry = context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entityToUpdate);
                }
                else
                {
                    dbSet.Attach(entityToUpdate);
                    context.Entry(entityToUpdate).State = EntityState.Modified;
                }
            }
        }

        /// <summary>
        /// Updates the specified entity list.
        /// </summary>
        /// <param name="entityList">The entity list.</param>
        public void Update(IList<T> entityList)
        {
            foreach (T entity in entityList)
            {
                this.Update(entity);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified entity list.
        /// </summary>
        /// <param name="entityList">The entity list.</param>
        public void Delete(IList<T> entityList)
        {
            foreach (T entity in entityList)
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Deletes the specified object by id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Gets the object by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public T GetOne(Expression<Func<T, bool>> condition, string includes = "")
        {
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrWhiteSpace(includes))
            {
                //includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ForEach(x => query = query.Include(x));
            }

            return query.FirstOrDefault(condition);
        }

        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public T GetOne(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            if (includes != null)
            {
                //IList<string> includelist = GetIncludeList(includes);

                includes.ForEach(x => query = query.Include(x));
            }

            //return query.AsNoTracking().FirstOrDefault(condition);
            return query.FirstOrDefault(condition);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includes = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                //query = query.AsExpandable().Where(filter);
            }

            foreach (var includeProperty in includes.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                //query = query.AsExpandable().Where(filter);
            }

            if (includes != null)
            {
                IList<string> includelist = GetIncludeList(includes);

                //includelist.ForEach(x => query = query.Include(x));
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            string orderBy = "",
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                //query = query.AsExpandable().Where(filter);
            }

            if (includes != null)
            {
                IList<string> includelist = GetIncludeList(includes);

                //includelist.ForEach(x => query = query.Include(x));
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                string sortColumn = orderBy;
                bool isDesc = false;

                if (orderBy.ToUpper().IndexOf("DESC") >= 0)
                {
                    sortColumn = orderBy.TrimEnd("DESC".ToCharArray()).Trim();
                    isDesc = true;
                }

                query = query.OrderBy(sortColumn, isDesc);
            }
            //else
            //{
            //    query = query.OrderBy(q => Guid.NewGuid());
            //}

            return query;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            string orderBy = "",
            string includes = "")
        {
            IQueryable<T> query = dbSet;//.AsNoTracking();
            //return query;

            foreach (var includeProperty in includes.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                string sortColumn = orderBy;
                bool isDesc = false;

                if (orderBy.ToUpper().IndexOf("DESC") >= 0)
                {
                    sortColumn = orderBy.TrimEnd("DESC".ToCharArray()).Trim();
                    isDesc = true;
                }

                query = query.OrderBy(sortColumn, isDesc);
                //query = query.OrderBy(q => Guid.NewGuid());
            }
            //else
            //{
            //    query = query.OrderBy(q => Guid.NewGuid());
            //}

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            return query;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="totalRows">The total rows.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(out int totalRows,
            Expression<Func<T, bool>> filter = null,
            string sortExpression = "",
            int startRowIndex = 0,
            int? maximumRows = null,
            string includes = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                //query = query.AsExpandable().Where(filter);
            }

            foreach (var includeProperty in includes.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            totalRows = query.Count();

            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                string sortColumn = sortExpression;
                bool isDesc = false;

                if (sortExpression.ToUpper().IndexOf("DESC") >= 0)
                {
                    sortColumn = sortExpression.TrimEnd("DESC".ToCharArray()).Trim();
                    isDesc = true;
                }

                query = query.OrderBy(sortColumn, isDesc);
            }
            //else
            //{
            //    query = query.OrderBy(q => Guid.NewGuid());
            //}

            if (maximumRows == null)
            {
                return query.Skip(startRowIndex);
            }

            return query.Skip(startRowIndex).Take((int)maximumRows);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="totalRows">The total rows.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(out int totalRows,
            Expression<Func<T, bool>> filter = null,
            string sortExpression = "",
            int startRowIndex = 0,
            int? maximumRows = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                //query = query.AsExpandable().Where(filter);
            }

            if (includes != null)
            {
                IList<string> includelist = GetIncludeList(includes);

                //includelist.ForEach(x => query = query.Include(x));
            }

            totalRows = query.Count();

            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                string sortColumn = sortExpression;
                bool isDesc = false;

                if (sortExpression.ToUpper().IndexOf("DESC") >= 0)
                {
                    sortColumn = sortExpression.TrimEnd("DESC".ToCharArray()).Trim();
                    isDesc = true;
                }

                query = query.OrderBy(sortColumn, isDesc);
            }
            //else
            //{
            //    query = query.OrderBy(q => Guid.NewGuid());
            //}

            if (maximumRows == null)
            {
                return query.Skip(startRowIndex);
            }

            return query.Skip(startRowIndex).Take((int)maximumRows);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="totalRows">The total rows.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(out int totalRows,
            string filter = null,
            string sortExpression = "",
            int startRowIndex = 0,
            int? maximumRows = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                //query = query.AsExpandable().Where(filter);
            }

            if (includes != null)
            {
                IList<string> includelist = GetIncludeList(includes);

                //includelist.ForEach(x => query = query.Include(x));
            }

            totalRows = query.Count();

            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                string sortColumn = sortExpression;
                bool isDesc = false;

                if (sortExpression.ToUpper().IndexOf("DESC") >= 0)
                {
                    sortColumn = sortExpression.TrimEnd("DESC".ToCharArray()).Trim();
                    isDesc = true;
                }

                query = query.OrderBy(sortColumn, isDesc);
            }
            //else
            //{
            //    query = query.OrderBy(q => Guid.NewGuid());
            //}

            if (maximumRows == null)
            {
                return query.Skip(startRowIndex);
            }

            return query.Skip(startRowIndex).Take((int)maximumRows);
        }

        /// <summary>
        /// Executes the SP with the specified name and parameters and returns an object of this type.
        /// </summary>
        /// <param name="spName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters (optional).</param>
        /// <returns>An object of this type</returns>
        public T ExecuteSP(string spName, params SqlParameter[] parameters)
        {
            string storedProcName = GetStoredProcNameWithParameters(spName, parameters) + " ";
            return this.context.Database.SqlQuery<T>(storedProcName, parameters).FirstOrDefault();
            //return dbSet.SqlQuery(storedProcName, parameters).FirstOrDefault();
        }

        /// <summary>
        /// Executes the SP with the specified name and parameters and returns an IEnumerable list of this type.
        /// </summary>
        /// <param name="spName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters (optional).</param>
        /// <returns>An IEnumerable list of this type</returns>
        public IEnumerable<T> ExecuteSPList(string spName, params SqlParameter[] parameters)
        {
            string storedProcName = GetStoredProcNameWithParameters(spName, parameters);

            return dbSet.SqlQuery(storedProcName, parameters).ToList<T>();
        }

        /// <summary>
        /// Gets the stored proc name with parameters.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private string GetStoredProcNameWithParameters(string spName, params SqlParameter[] parameters)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(spName);

            stringBuilder.Append(string.Join(",", parameters.Select(x => string.Format(" @{0}", x.ParameterName))));

            //foreach (SqlParameter sqlParameter in parameters)
            //{
            //    stringBuilder.AppendFormat(" @{0}, ", sqlParameter.ParameterName);
            //}

            return stringBuilder.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the include list.
        /// </summary>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">The body must be a member expression</exception>
        private IList<string> GetIncludeList(Expression<Func<T, object>>[] includes)
        {
            List<string> includelist = new List<string>();

            foreach (var item in includes)
            {
                MemberExpression body = item.Body as MemberExpression;

                if (body == null)
                {
                    throw new ArgumentException("The body must be a member expression");
                }

                includelist.Add(body.Member.Name);
            }

            return includelist;
        }

        #endregion Private Methods
    }
}

