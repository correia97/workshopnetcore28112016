using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workshopcore20161128.Models;

namespace workshopcore20161128.Controllers
{
    [Route("api/pessoas")]
    public class PessoasController
    {
 
 private readonly DataContext _dataContext;
    public PessoasController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
     [HttpGet]
        public async Task<IActionResult> ObterPessoas()
        {
            var pessoas = await _dataContext.Pessoas.ToListAsync();
            return Json(pessoas);
        }

        [HttpPost]
        public async Task<IActionResult> CriaPessoa([FromBody]Pessoa modelo)
        {
            await _dataContext.Pessoas.AddAsync(modelo);
            await _dataContext.SaveChangesAsync();

            return Json(modelo);
}
    }
}