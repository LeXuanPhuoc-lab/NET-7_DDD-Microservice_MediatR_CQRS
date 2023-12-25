using Application.Accounts.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MappingExtensions : Profile
    {
        public MappingExtensions()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
