using Domain.Entity;

namespace Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> InsentAsync(T item);
    
    Task<T> UpdateAsync(T item);
    
    Task<bool> DeleteAsync(Guid id);
    
    Task<T> SelectAsync(Guid id);
    
    Task<IEnumerable<T>> SelectAsync();
    
    Task<bool> ExistAsync (Guid id); 
}