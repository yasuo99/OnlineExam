using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LuyenThiOnline.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LuyenThiOnline.Data
{
    public class CRUDRepository : ICRUDRepository
    {
        private readonly ApplicationDbContext _db;
        public CRUDRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Create<T>(T subject) where T : class
        {
            _db.Add(subject);
        }
        public void Delete<T>(T subject) where T : class
        {
            var _dbSet = _db.Set<T>();
            if (_db.Entry(subject).State == EntityState.Detached)
            {
                _dbSet.Attach(subject);
            }           
            _dbSet.Remove(subject);
        }

        public async Task Delete<T>(object id) where T : class
        {
            var _dbSet = _db.Set<T>();
            var subject = await _dbSet.FindAsync(id);
            if (_db.Entry(subject).State == EntityState.Detached)
            {
                _db.Attach(subject);
            }
            _dbSet.Remove(subject);
        }

        public async Task<T> Detail<T>(object id, string includeProperties = "") where T : class
        {
            var _dbSet = _db.Set<T>();
            var query = _dbSet.Take(_dbSet.Count());
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return await query.FirstOrDefaultAsync(u => u.Equals(_dbSet.Find(id)));
        }     
        public bool Exists<T>(Dictionary<dynamic,dynamic> properties) where T : class
        {
            var _dbSet = _db.Set<T>();
            foreach(var item in _dbSet.ToList())
            {
                if(item.SameValue(properties))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Paging<T>> GetAll<T>(SubjectParams subjectParams, string includeProperties = "") where T : class
        {
            var _dbSet = _db.Set<T>();
            var query = _dbSet.Take(_dbSet.Count());
            if (includeProperties != null)
            {
                foreach (var properties in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(properties);
                }
            }
            return await Paging<T>.CreateAsync(query, subjectParams.PageNumber, subjectParams.PageSize);
        }

        public async Task<T> GetOneWithConditions<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var _dbSet = _db.Set<T>();
            var queryableData = _dbSet.Take(await _dbSet.CountAsync());
            if(expression != null)
            {
                queryableData = queryableData.Where(expression);
            }
            return await queryableData.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetWithConditions<T>(Expression<Func<T, bool>> filter = null, string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class
        {
            IQueryable<T> query = _db.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var properties in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(properties);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IEnumerable<T> GetWithMultiPro<T>(Dictionary<dynamic, dynamic> properties) where T : class
        {
            var _dbSet = _db.Set<T>();
            foreach(var item in _dbSet.ToList())
            {
                if(item.SameValue(properties))
                {
                    yield return item;
                }
            }
        }
        public async Task<IEnumerable<T>> GetWithRawSql<T>(string sqlCommand, object[] parameters) where T : class
        {
            return await _db.Set<T>().FromSqlRaw(sqlCommand, parameters).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<T> Update<T>(T subject) where T : class
        {
            var _dbSet = _db.Set<T>();
            _dbSet.Attach(subject);
            _db.Entry(subject).State = EntityState.Modified;
            return subject;
        }
    }
}