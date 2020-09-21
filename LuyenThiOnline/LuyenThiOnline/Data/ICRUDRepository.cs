using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LuyenThiOnline.Helpers;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.Data
{
    public interface ICRUDRepository
    {
        Task<Paging<T>> GetAll<T>(SubjectParams subjectParams,string includeProperties = "") where T : class;
        IEnumerable<T> GetWithMultiPro<T>(Dictionary<dynamic, dynamic> properties) where T : class;
        Task<IEnumerable<T>> GetWithConditions<T>(Expression<Func<T, bool>> filter = null,
            string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
            ) where T : class;
        Task<T> GetOneWithConditions<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<IEnumerable<T>> GetWithRawSql<T>(string sqlCommand, object[] parameters) where T : class;
        void Create<T>(T subject) where T : class;
        bool Exists<T>(Dictionary<dynamic, dynamic> properties) where T : class;
        Task<T> Detail<T>(object id, string includeProperties = "") where T : class;
        Task<T> Update<T>(T subject) where T : class;
        Task Delete<T>(object id) where T : class;
        void Delete<T>(T subject) where T : class;
        Task<bool> SaveAll();
    }
}