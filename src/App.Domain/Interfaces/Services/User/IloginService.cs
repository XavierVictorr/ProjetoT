using Api.Domain.Entities;
using Domain.Dtos;

namespace Domain.Interfaces.Services.User;

public interface IloginService
{
    Task<object> FindByLogin(LoginDto user);
}