using Api.Domain.Entities;

namespace Domain.Repository;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity> FindByLogin(string email);
}
