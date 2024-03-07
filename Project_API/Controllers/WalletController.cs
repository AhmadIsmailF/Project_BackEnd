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
       /* [AllowAnonymous]
        [HttpPatch]
        public IActionResult UpdateWallet(int walletId, [FromBody] WalletDto walletDto)
        {
            if (walletDto == null || walletId != walletDto.WalletId)
            {
                return BadRequest(ModelState);
            }

            var walletobj = _mapper.Map<Wallet>(walletDto);

            if (!_walRepo.UpdateWallet(walletobj))
            {
                ModelState.AddModelError("", $"something went wrong when updating the record {walletobj.Balance}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        */

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
        [HttpPatch("Convet")]
        public async Task<IActionResult> Convert([FromBody] SellDto sellDto)
        {

            await _walRepo.Sell(sellDto.WalletId, sellDto.Amount);
            return Ok();
        }
    }
}
