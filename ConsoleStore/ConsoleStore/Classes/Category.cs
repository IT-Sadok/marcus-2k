namespace ConsoleStore.Classes
{
    public class Category
    {
        public required int Id { get; set; }

        public required string Name { get; set; } = string.Empty;

        public required string Description { get; set; } = string.Empty;
    }
}
