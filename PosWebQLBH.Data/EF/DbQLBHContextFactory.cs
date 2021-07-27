using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Data.EF
{
    public class DbQLBHContextFactory : IDesignTimeDbContextFactory<DbQLBHContext>
    {
        public DbQLBHContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MyConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DbQLBHContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DbQLBHContext(optionsBuilder.Options);
        }
    }
}