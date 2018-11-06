using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore.DAO
{
    class PetStoreContext : DbContext
    {

        public DbSet<Pet> Pets { get; set; }

	    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	    {
            var dbHost = Environment.GetEnvironmentVariable("DBHOST") ?? "localhost";
            var dbName = Environment.GetEnvironmentVariable("DBNAME") ?? "petstore";
            var dbUsername = Environment.GetEnvironmentVariable("DBUSERBAME") ?? "root";
            var dbPassword = Environment.GetEnvironmentVariable("DBPASSWORD") ?? "root";

            var dbConnectionString = $"server={dbHost};database={dbName};user={dbUsername};password={dbPassword}";
            System.Console.WriteLine(dbConnectionString);
            optionsBuilder.UseMySql(dbConnectionString);
	    }

    }
}
