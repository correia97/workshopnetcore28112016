using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using WorkShopCore.Models;
using Xunit;

namespace UnitTest
{
    [Collection("Base collection")]
    public abstract class BaseIntegrationTest
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected readonly DataContext TestDataContext;

        protected BaseTestFixture Fixture { get; }

        protected BaseIntegrationTest(BaseTestFixture fixture)
        {
            Fixture = fixture;

            TestDataContext = fixture.TestDataContext;
            Server = fixture.Server;
            Client = fixture.Client;

            this.ClearDb().Wait();
        }
        private async Task ClearDb()
        {
            var commands = new[]
            {
                "EXEC sp_MsForEachTable 'Alter Table ? Nocheck constraint all'",
                "EXEC sp_MsForEachTable 'Delete from ?'",
                "EXEC sp_MsForEachTable 'Alter Table ? check constraint all'",
            };

            await TestDataContext.Database.OpenConnectionAsync();

            foreach (var command in commands)
            {
                await TestDataContext.Database.ExecuteSqlCommandAsync(command);
            }

            TestDataContext.Database.CloseConnection();
        }

    }
}