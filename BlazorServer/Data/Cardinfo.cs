using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorServer.Pages;

namespace BlazorServer.Data
{
  public class CardsellingItem
  {
    [BsonId]
    public Guid Id { get; set; }

    public string Seller { get; set; }
    public string CardName { get; set; }
    public bool Available { get; set; }
    public string Price { get; set; }
    public string EditionNumber { get; set; }
    public string Rarity { get; set; }
    public string Condition { get; set; }
      }


  public class Chatten
  {
    [BsonId]
    public Guid Id { get; set; }
    public string SourceUserName { get; set; }
    public string TargetUserName { get; set; }
    public string Content { get; set; }
    public DateTime SendTime { get; set; }
    [BsonElement("Seen")]
    public bool Seen { get; set; }
  }

  public class SingleChat
  {
    public string ColName { get; set; }
    public DateTime SendTime { get; set; }
  }

  public class MongoCRUD
  {
    private IMongoDatabase db;

    public MongoCRUD(string database)   //Connecting to MongoDB
    {
      var client = new MongoClient();
      db = client.GetDatabase(database);
    }

    public void InsertRecord<T>(string table, T record)   //Insert Record
    {
      var collection = db.GetCollection<T>(table);
      collection.InsertOne(record);
    }

    public void InsertRecord2<T>(string table, string table2, T record)   //Insert 2 records in 2 databases
    {
      var collection = db.GetCollection<T>(table);
      collection.InsertOne(record);
      var collection2 = db.GetCollection<T>(table2);
      collection2.InsertOne(record);
    }

    public List<T> LoadRecords<T>(string table)   //Load
    {
      var collection = db.GetCollection<T>(table);

      return collection.Find(new BsonDocument()).ToList();
    }

    public T LoadRecordById<T>(string table, Guid id)   //Load by Id
    {
      var collection = db.GetCollection<T>(table);
      var filter = Builders<T>.Filter.Eq("Id", id);

      return collection.Find(filter).First();
    }

    [Obsolete]
    public void UpsertRecord<T>(string table, Guid id, T record)    //Upsert
    {
      var collection = db.GetCollection<T>(table);

      var result = collection.ReplaceOne(
        new BsonDocument("_id", id),
        record,
        new UpdateOptions { IsUpsert = true });
    }

    public void UpdateRecordById<T>(string table, Guid id, string Cardname, string price, string editionnumber, string rarity, string condition)
    {
      var collection = db.GetCollection<T>(table);

      var filter = Builders<T>.Filter.Eq("Id", id);
      var update = Builders<T>.Update.Set("CardName", Cardname).Set("Price", price).Set("EditionNumber", editionnumber).Set("Rarity", rarity).Set("Condition", condition);

      collection.UpdateMany(filter, update);
    }

    public void UpdateRecordChat<T>(string table, string table2, Guid id)        //Update (zbs von Seen=false auf Seen=true)
    {
      var collection = db.GetCollection<T>(table);
      var collection2 = db.GetCollection<T>(table2);

      var filter = Builders<T>.Filter.Eq("Id", id);
      var update = Builders<T>.Update.Set("Seen", true);

      collection.UpdateMany(filter, update);
      collection2.UpdateMany(filter, update);
    }
    
    public void DeleteRecordById<T>(string table, Guid id)    // Delete By Record by Id
    {
      var collection = db.GetCollection<T>(table);
      var filter = Builders<T>.Filter.Eq("Id", id);
      collection.DeleteOne(filter);
    }
    
    public void DeleteCollection<T>(string col)
    {
      db.DropCollection(col);
    }
  }

}

