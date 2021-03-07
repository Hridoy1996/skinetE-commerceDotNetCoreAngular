using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.Data;
using Core.Entities;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
           /* 
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("skinet_db");  
         
            string text = System.IO.File.ReadAllText("C:/Users/TM Hridoy/e-commerce/skinet/Infrastructure/Data/SeedData/products.json");
            var products = BsonSerializer.Deserialize<List<Product>>(text);

            var Product = database.GetCollection<Product>("Product");
            await Product.InsertManyAsync(products);
          */
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
