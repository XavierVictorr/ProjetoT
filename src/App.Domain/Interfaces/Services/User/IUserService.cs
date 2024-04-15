using Domain.Entity;

namespace Domain.Interfaces.Services.User;

public interface IUserService
{
    Task<UserEntity> Get (Guid id); 
    Task<IEnumerable<UserEntity>> GetAll();
    Task<UserEntity> Post (UserEntity User);
    Task<UserEntity> Put (UserEntity user);
    Task<bool> Delete (Guid id);
}