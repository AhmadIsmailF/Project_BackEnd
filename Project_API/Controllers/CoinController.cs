using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_API.Models;
using Project_API.Repository.IRepository;

namespace Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinRepository _coinRepo;

        public CoinController(ICoinRepository coinRepos)
        {
            _coinRepo = coinRepos;
        }


        [HttpGet("all operation")]
        public async Task<IActionResult> GetWallets()
        {
            var objList =await _coinRepo.GetCoins();
            var list = new List<Coin>();

            foreach (var obj in objList)
            {
                list.Add(obj);
            }

            return Ok(list);
        }
    }
}
