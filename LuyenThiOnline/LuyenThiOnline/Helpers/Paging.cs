using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LuyenThiOnline.Helpers
{
    public class Paging<T> : List<T>
    {
        public int CurrentPages {get;set;}
        public int TotalPages {get;set;}
        public int PageSize {get;set;}
        public int TotalCount {get;set;}
        public Paging(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPages = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public static async Task<Paging<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize){
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();
            return new Paging<T>(items,count,pageNumber,pageSize);
        }
    }
}