using Api.Domain.Entities;
using Data.Context;
using Data.Repository;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations;

public class UserImplementations : BaseRepository<UserEntity>, IUserRepository
{
    private DbSet<UserEntity> _dataSet;
    private IUserRepository _userRepositoryImplementation;

    public UserImplementations(MyContext context) : base(context)
    {
        _dataSet = context.Set<UserEntity>();
    }

    public async Task<UserEntity> FindByLogin(string email)
    {
        return await _dataSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }
}