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
            var dbName = Environment.GetEnvironmentVariable("DBNAME");
            if(dbName == null)
            {
                dbName = "localhost";
            }
            else
            {
                dbName = "petStore-mysql";
            }

            var dbConnectionString = $"server={dbName};database=pets;user=root;password=root";

            optionsBuilder.UseMySql(dbConnectionString);
	    }

    }
}
