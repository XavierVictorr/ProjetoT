using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=Teste@123";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            //optionsBuilder.UseMySql(connectionString);
            optionsBuilder.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString));
            return new MyContext(optionsBuilder.Options);
        }
    }
}