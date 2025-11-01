using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDExample.Domain.Entities
{
  // Ejemplo: Product.cs
public class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    
    // Constructor y métodos de negocio
}
}