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
		optionsBuilder.UseMySql("server=localhost;database=pets;user=root;password=root");
	}

    }
}
