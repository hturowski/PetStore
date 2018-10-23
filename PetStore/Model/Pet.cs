using System.Runtime.Serialization;

namespace PetStore
{
    public class Pet
    {
        public Pet(int id, string name, string type)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
