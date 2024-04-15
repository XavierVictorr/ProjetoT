using System.Collections.Immutable;
using Api.Domain.Entities;
using Data.Context;
using Data.Repository;
using Domain;
using Domain.Interfaces.Services.User;
using Domain.Security;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Options;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddDbContext<MyContext>(
    options => options.UseMySql("server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=Teste@123") 
);*/

//injection
var mySqlConnection = "server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=Teste@123";
builder.Services.AddDbContextPool<MyContext>(options =>
    options.UseMySql(mySqlConnection,
        ServerVersion.AutoDetect(mySqlConnection)));


//builder.Services.AddTransient<IServiceCollection, ServiceCollection>();
builder.Services.AddTransient<IUserService, UserServices> ();
builder.Services.AddScoped(typeof(IRepository<UserEntity>),typeof(BaseRepository<UserEntity>));

   

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
