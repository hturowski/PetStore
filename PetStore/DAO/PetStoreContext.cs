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
            var dbName = Environment.GetEnvironmentVariable("BRANCH_DATABASE_NAME") ?? "pets";
            var dbUsername = Environment.GetEnvironmentVariable("DBUSERNAME") ?? "root";
            var dbPassword = Environment.GetEnvironmentVariable("DBPASSWORD") ?? "root";

            var dbConnectionString = $"server={dbHost};database={dbName};user={dbUsername};password={dbPassword}";
            System.Console.WriteLine(dbConnectionString);
            optionsBuilder.UseMySql(dbConnectionString);
	    }

    }
}
