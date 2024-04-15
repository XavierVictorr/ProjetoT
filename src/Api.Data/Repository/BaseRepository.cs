using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.XPath;
using Api.Domain.Entities;
using Data.Context;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dataSet;
        public BaseRepository(MyContext context, DbSet<T> dataSet)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        protected BaseRepository(MyContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<T> InsertAsync (T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                item.CreatAt = DateTime.UtcNow;
                _dataSet.Add(item);
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

                if (result == null)
                    return null;

                item.UpDateAt = DateTime.UtcNow;
                item.CreatAt = result.CreatAt;
                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public async Task<bool> DeleteAsync (Guid id)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                   return false;
                
                _dataSet.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExistAsync(Guid id) 
        {
            return await _dataSet.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}