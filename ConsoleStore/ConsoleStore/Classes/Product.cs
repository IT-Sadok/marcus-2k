﻿
namespace ConsoleStore.Classes
{
    public class Product
    {
        public required int Id { get; set; }

        public required string Name { get; set; } = string.Empty;

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

    }
}
