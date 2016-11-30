using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using WorkShopCore.Models;
using Xunit;

namespace UnitTest{

    [Collection("Base Collection")]
    public abstract class BaseIntegrationTest{
        protected readonly TestServer Server;
       protected readonly HttpClient Client;
       protected readonly DataContext TestDataContext;

       protected readonly BaseTestFixture Fixture;

       public BaseIntegrationTest(BaseTestFixture fixture)
       {
           Fixture = fixture;
           TestDataContext = fixture.TestDataContext;
           Server = fixture.Server;
           Client = fixture.Client;

           ClearDb.Wait();
       }

       private async Task ClearDb(){
            var commands = new []
            {
                "EXEC sp_MsForEachTable 'Alter Table ? No'",
                "",
                "",
            }
            await TestDataContext.Database.OpenConnectionAsync();
            foreach(var command in commands){
                await TestDataContext.Database.ExecuteSqlCommandAsync(command);
            }
        TestDataContext.Database.CloseConnection();
       }
    }
}