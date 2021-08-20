using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Data
{
  public class CardinfoItem2
  {
    [BsonId]
    public Guid Id { get; set; }
    public string CardName { get; set; }

    public bool Available { get; set; }

    public string Price { get; set; }

    public string Editionnumber { get; set; }

    public string Rarity { get; set; }

    public string Zustand { get; set; }
  }
}
