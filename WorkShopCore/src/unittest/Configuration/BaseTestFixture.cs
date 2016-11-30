using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkShopCore;
using WorkShopCore.Models;

namespace UnitTest{
    public abstract class BaseTestFixture: IDisposable
    {
        public TestServer Server { get; private set;}
       public  HttpClient Client { get; private set;}
       public  DataContext TestDataContext { get; private set;}
       public  IConfigurationRoot Configuration { get; private set;}
       public BaseTestFixture()
       {
           var envNamr = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
           var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json",optional:true,reloadOnChange:true)
           .AddJsonFile($"appsettings.{envName}.json",optional:true)
           .AddEnvironmentVariables();
           Configuration = builder.Build();
           var opts = new DbContextOptionsBuilder<DataContext>();
           opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
           SetupDatabase();

           Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
           Client = Server.CreateClient();


       }

       private void SetupDatabase()
       {

       }
    }
}