using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_API.Models;
using Project_API.Models.Dto;
using Project_API.Repository.IRepository;

namespace Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly InHistoryRepository _hisRepo;

        public HistoryController(InHistoryRepository userRepos)
        {
            _hisRepo = userRepos;
        }

        [HttpGet("all operation")]
        public async Task<IActionResult> GetWallets()
        {
            var objList = _hisRepo.GetHistories();
            var list = new List<History>();

            foreach (var obj in objList)
            {
                list.Add(obj);
            }

            return Ok(list);
        }

    }
}
