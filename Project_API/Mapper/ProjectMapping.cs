using Project_API.Models.Dto;
using Project_API.Models;
using System.Data;
using System.Diagnostics;
using AutoMapper;

namespace Project_API.Mapper
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, SellDto>().ReverseMap();
            CreateMap<Wallet, ConvertDto>().ReverseMap();
            CreateMap<Wallet, WalletDBDto>().ReverseMap();
            CreateMap<Wallet, WithdrawDto>().ReverseMap();
        }
    }
}
