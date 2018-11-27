using Microsoft.EntityFrameworkCore.Migrations;
using PetStore.DAO;

namespace PetStore.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var db = new PetStoreContext())
            {
                db.Pets.Add(new Pet(1, "Zoey", "Cat"));
                db.Pets.Add(new Pet(2, "Norman", "Dog"));
                db.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
