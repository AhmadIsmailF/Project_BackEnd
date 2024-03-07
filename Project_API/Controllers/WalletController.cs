using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_API.Migrations;
using Project_API.Models;
using Project_API.Models.Dto;
using Project_API.Repository.IRepository;

namespace Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private IWalletRepository _walRepo;
        private readonly IMapper _mapper;

        public WalletController(IWalletRepository npRepo, IMapper mapper)
        {
            _mapper = mapper;
            _walRepo = npRepo;
        }


        [HttpPost]
        //[ProducesResponseType(201, Type = typeof(NationalParkDto))]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesDefaultResponseType]
        public async Task< IActionResult> CreateNationalPark(int userId )
        {
            if (userId == null)
            {
                return BadRequest(ModelState);
            }
            var walletobj = new Wallet();
            walletobj.UserId = userId;
            walletobj.Balance= 0;
           walletobj.CoinId = 3;

            await _walRepo.CreateWallet(walletobj) ;
            //if (! )
            //{
            //    ModelState.AddModelError("", $"something went wrong when saving the record {walletobj.Balance}");
            //    return StatusCode(500, ModelState);
            //}
            //return CreatedAtRoute("GetNationalPark", new { walletkId = walletkobj.WalletId }, walletkobj);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Buy")]
        public async Task<IActionResult> Buy([FromBody] WalletDto walletDto)
        {
           await _walRepo.Buy(walletDto.WalletId, walletDto.Amount);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Sell")]
        public async Task<IActionResult> Sell([FromBody] SellDto sellDto)
        {
            
            await _walRepo.Sell(sellDto.WalletId, sellDto.Amount);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Deposit")]
        public async Task<IActionResult> Deposite([FromBody] WalletDto walletDto)
        {

            await _walRepo.Deposit(walletDto.UserId, walletDto.Amount);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WalletDto walletDto)
        {

            await _walRepo.Withdraw(walletDto.WalletId, walletDto.Amount);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Convert")]
        public async Task<IActionResult> Convert([FromBody] ConvertDto convertDto)
        {

            await _walRepo.Convert(convertDto.Coin1Id, convertDto.Coin2Id,convertDto.Amount,convertDto.userId);
            return Ok();
        }
    }
}
