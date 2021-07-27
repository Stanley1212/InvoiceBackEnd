using Invoice.Domain;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core
{
    public abstract class BaseService<T>
        where T : class
    {
        private readonly IMapperExtension _mapperExtension;
        protected readonly IRepository<T> _repository;

        protected BaseService(IRepository<T> repository, IMapperExtension mapperExtension)
        {
            this._mapperExtension = mapperExtension;
            this._repository = repository;
        }

        public abstract IEnumerable<T> GetAll();
        public abstract T GetByID(object id);
        public abstract Task<int> Add(T data);
        public abstract Task<int> Update(T data);
        public abstract Task<int> Delete(object id);


        /// <summary>
        /// Ejecuta un query en base de datos
        /// </summary>
        /// <typeparam name="D">Tipo de Salida</typeparam>
        /// <param name="query">Query a ejecutar</param>
        /// <param name="parameters">Parametros</param>
        /// <param name="selectFn">Transformacion al objeto de salida</param>
        /// <returns>Enumerable<D></returns>
        public IEnumerable<D> GetWithRawSql<D>(string query, object[] parameters, Expression<Func<T, D>> selectFn = null)
            where D : class
        {
            return this._repository.GetWithRawSql(query, parameters, selectFn);
        }
        public IEnumerable<T> Filter(
            Expression<Func<T, bool>> Datafilter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            return _repository.Get(Datafilter, orderBy, includeProperties).AsEnumerable();

        }

        /// <summary>
        /// Retorna la Informacion Paginada de la entidad
        /// </summary>
        /// <param name="pageSize">Registros a Mostrar por Pagina</param>
        /// <param name="currentPage">Pagina Actual</param>
        /// <param name="filterExpresion">Filtros a aplicar</param>
        /// <param name="orderBy">Ordenar Datos</param>
        /// <param name="includeProperties">Incluir propiedades de Navegacion</param>
        /// <returns>Objeto con la Data, y la informacion de la paginacion</returns>
        public PagedData<E> GetAllWithPagination<E>(int pageSize,
            int currentPage,
            Expression<Func<T, bool>> filterExpresion,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null
           )
            where E : class

        {
            var query = this._repository.Get(filterExpresion, orderBy, includeProperties);

            int TotalRecords = query.Count();
            int skiped = (currentPage - 1) * pageSize;
            var data = query.Skip(skiped).Take(pageSize).ToList();

            if (typeof(E) == typeof(T))
            {
                return (new PagedData<T>(data, TotalRecords, currentPage, pageSize)) as PagedData<E>;
            }

            var dataMapped = this._mapperExtension.Map<IEnumerable<T>, IEnumerable<E>>(data);
            return new PagedData<E>(dataMapped, TotalRecords, currentPage, pageSize);
        }


        public PagedData<T> GetAllWithPagination(int pageSize,
                                       int currentPage,
                                       Expression<Func<T, bool>> filterExpresion,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null
                                       )

        {
            return this.GetAllWithPagination<T>(pageSize, currentPage, filterExpresion, orderBy, includeProperties);
        }

        public TDestination Map<TSource, TDestination>(TSource data)
           where TSource : class where TDestination : class
        {
            if (_mapperExtension is null)
            {
                throw new AppException(MessageCode.GeneralException, string.Format("No se puede realizar el Mixin del tipo '{0}' a '{1}' porque la implemntacion de {2} es nula", typeof(TSource).FullName, typeof(TDestination).FullName, typeof(IMapperExtension).FullName));
            }

            return _mapperExtension.Map<TSource, TDestination>(data);
        }

        public TDestination Map<TSource, TDestination>(TSource sourceData, TDestination dataDestination)
            where TSource : class where TDestination : class
        {
            if (_mapperExtension is null)
            {
                throw new AppException(MessageCode.AppException, string.Format("No se puede realizar el Mixin del tipo '{0}' a '{1}' porque la implemntacion de {2} es nula", typeof(TSource).FullName, typeof(TDestination).FullName, typeof(IMapperExtension).FullName));
            }

            return _mapperExtension.Map(sourceData, dataDestination);
        }
    }
}
