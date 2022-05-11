using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using project1.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace project1
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<DbContex>
    {
        public DbContex CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbContex>();
            optionsBuilder.UseMySql(configuration.GetConnectionString("backend"), ServerVersion.AutoDetect(configuration.GetConnectionString("backend")));

            return new DbContex(optionsBuilder.Options);
        }
    }
}
