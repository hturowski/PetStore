namespace PetStore
{
    public struct Pet
    {
        public Pet(int id, string name, string type)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
        }

        public int Id { get; }
        public string Name { get; }
        public string Type { get; }
    }
}
