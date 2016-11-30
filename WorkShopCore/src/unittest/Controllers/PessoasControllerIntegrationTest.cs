using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WorkShopCore.Models;
using Xunit;

namespace UnitTest
{
    public class PessoaControllerIntegrationTest : BaseIntegrationTest
    {
        private const string BaseUrl = "api/Pessoas";
        public PessoaControllerIntegrationTest(BaseTestFixture fixture) : base(fixture)
        {
        }
        [Fact]
        public async Task DeveRetornarListaPessoasVazia()
        {
            //Given
            var response = await Client.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();
            //When
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Pessoa>>(responseString);
            //Then
            Assert.Equal(data.Count, 0);

        }
        [Fact]
        public async Task DeveRetornarListaDePessoas()
        {
            //Given
            var pessoa = new Pessoa
            {
                Nome = "Paulo",
                Twitter = "correiape"
            };
            await TestDataContext.AddAsync(pessoa);
            await TestDataContext.SaveChangesAsync();
            //When
            var response = await Client.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Pessoa>>(responseString);
            //Then
            Assert.Equal(data.Count, 1);
            Assert.Contains(data, x => x.Nome == pessoa.Nome);
        }
    }
}