using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DDDExample.Domain.Entities
{
  // Ejemplo: Product.cs
public class Product 
{
     [BsonId]
        [BsonRepresentation(BsonType.String)] // 👈 importante
        public Guid Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("Stock")]
        public int Stock { get; set; }
}
}