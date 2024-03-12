using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_API.Models;
using Project_API.Models.Dto;
using Project_API.Repository.IRepository;
using System;

namespace Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private IWalletRepository _walRepo;
        private InHistoryRepository _hisRepo;
        private ICoinRepository _coinRepo;
        private readonly IMapper _mapper;
        

        public WalletController(IWalletRepository walRepo,InHistoryRepository hisRepo,ICoinRepository coinRepo, IMapper mapper)
        {
            _mapper = mapper;
            _walRepo = walRepo;
            _coinRepo = coinRepo;
            _hisRepo = hisRepo;
        }

        [AllowAnonymous]
        [HttpGet("Wallets")]
        //[Authorize]

        public async Task<IActionResult> GetWallets()
        {
            var objList = _walRepo.GetWallets();
            var objDto = new List<WalletDBDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<WalletDBDto>(obj));
            }
            
            return Ok(objDto);
        }

        [AllowAnonymous]
        [HttpPatch("Buy")]
        public async Task<IActionResult> Buy([FromBody] WalletDto walletDto)
        {
            await _walRepo.Buy(walletDto.WalletId, walletDto.Amount);
            var coin =await _coinRepo.Getcoin(walletDto.CoinId);
            var text = $"you bought {walletDto.Amount} {coin.Name} at {DateTime.Now}";
            await _hisRepo.Insert(walletDto.UserId,text);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Sell")]
        public async Task<IActionResult> Sell([FromBody] SellDto sellDto)
        {
            await _walRepo.Sell(sellDto.WalletId, sellDto.Amount);

            var wallet=_walRepo.GetWallet(sellDto.WalletId);
            var coin =await _coinRepo.Getcoin(sellDto.CoinId);
            var text = $"you sold {sellDto.Amount} type {coin.Name} at {DateTime.Now}";
            await _hisRepo.Insert(wallet.UserId, text);


            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Deposit")]
        public async Task<IActionResult> Deposite([FromBody] WalletDto walletDto)
        {
            await _walRepo.Deposit(walletDto.UserId, walletDto.Amount);
            var coin =await _coinRepo.Getcoin(walletDto.CoinId);
            var text = $"you deposited {walletDto.Amount} type {coin.Name} at {DateTime.Now}";
            await _hisRepo.Insert(walletDto.UserId, text);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawDto withdrawDto)
        {
            await _walRepo.Withdraw(withdrawDto.UserId, withdrawDto.Amount);
            var coin =await _coinRepo.Getcoin(withdrawDto.CoinId);
            var text = $"you withdrew {withdrawDto.Amount} type {coin.Name} at {DateTime.Now}";
            await _hisRepo.Insert(withdrawDto.UserId, text);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("Convert")]
        public async Task<IActionResult> Convert([FromBody] ConvertDto convertDto)
        {
            await _walRepo.Convert(convertDto.Coin1Id, convertDto.Coin2Id,convertDto.Amount,convertDto.UserId);
            var coinfrom =await _coinRepo.Getcoin(convertDto.Coin1Id);
            var cointo =await _coinRepo.Getcoin(convertDto.Coin2Id);
            var text = $"you converted {convertDto.Amount} from {coinfrom.Name} to {cointo.Name} at {DateTime.Now}";
            await _hisRepo.Insert(convertDto.UserId, text);
            return Ok();
        }
    }
}
