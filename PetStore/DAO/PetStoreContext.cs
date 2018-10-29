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
            var dbName = Environment.GetEnvironmentVariable("DBNAME") ?? "pets";
            var dbConnectionString = $"server={dbHost};database={dbName};user=root;password=root";
            System.Console.WriteLine(dbConnectionString);
            optionsBuilder.UseMySql(dbConnectionString);
	    }

    }
}
