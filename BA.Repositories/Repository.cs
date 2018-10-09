using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BA.Entities;
using System.Linq.Expressions;
using System.Data.Entity;
using AutoMapper;
namespace BA.Repositories
{
    public interface IRepository<T,Model> where T:class where Model:class
    {
        T Add(T entity);
        Model AddWithModel(T entity);
        void Delete(object id);
        T Update(T entity);
        IQueryable<T> Get(Expression<Func<T, bool>> filter =null);
        Model GetModelById(object id);
        T GetById(object id);
    }
    public class Repository<T,Model> :IRepository<T,Model> where T:class where Model :class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<T, Model>());
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _mapper = config.CreateMapper();
        }
        public virtual T GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public virtual Model GetModelById(object id)
        {
            T entity = GetById(id);
            return _mapper.Map<T, Model>(entity);
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }
        public virtual T Add(T entity)
        {
            var newEntry = _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return newEntry;
        }
        public virtual Model AddWithModel(T entity)
        {
            entity = Add(entity);
            Model model = _mapper.Map<T,Model> (entity);
            return model;
        }
        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }
        void Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Deleted)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
        public virtual T Update(T entity)
        {
            var entry = _dbContext.Entry(entity);
            _dbSet.Attach(entity);
            entry.State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
