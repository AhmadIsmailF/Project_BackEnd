using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_API.Models;
using Project_API.Models.Dto;
using Project_API.Repository.IRepository;
using System.Diagnostics;

namespace Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepos;
        private readonly IWalletRepository _WalletRepos;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepos, IWalletRepository WalletRepos, IMapper mapper)
        {
            _userRepos = userRepos;
            _WalletRepos = WalletRepos;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticatianModel model)
        {
            var user =await _userRepos.Authenticate(model.Email, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = " username or password is incorrect" });
            }
            return Ok(user);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {


          // if(_userRepos.IsValidPassword(userDto.Password))


            bool ifUserNmeUnique =await _userRepos.IsUniqueUser(userDto.Email);
            if (!ifUserNmeUnique)
            {
                return BadRequest(new { message = " you are already registred" });
            }
            var userobj = _mapper.Map<User>(userDto);

            var user =await _userRepos.Register(userobj);
            var createdWallet = new Wallet()
            {
                UserId = user.UserId,
                Balance = 0,
                CoinId = 3
            };
            var wallet = await _WalletRepos.CreateWallet(createdWallet);

            if (user == null)
            {
                return BadRequest(new { message = "Error while registration" });
            }
            return Ok();
        }
    }
}
