using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStore.Clases
{
    internal class Product
    {     
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;

    }
}
