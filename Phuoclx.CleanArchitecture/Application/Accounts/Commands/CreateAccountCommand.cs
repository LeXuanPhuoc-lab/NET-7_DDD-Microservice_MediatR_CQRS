using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Commands
{
    public record CreateAccountCommand(string Email, string Password, string Role) : IRequest<AccountDto>;

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly IMapper _mapper;
        private readonly IDriverLicenseLearningSupportContext _context;

        public CreateAccountCommandHandler(IMapper mapper,
            IDriverLicenseLearningSupportContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountDto = new AccountDto
            {
                Email = request.Email,
                Password = request.Password,
                IsActive = true,
                RoleId = _context.Roles.FirstOrDefault(x => x.Name!.Equals(request.Role))!.RoleId
            };
            await _context.Accounts.AddAsync(_mapper.Map<AccountDto, Account>(accountDto));
            await _context.SaveChangeAsync(cancellationToken);
            return accountDto;
        }
    }
}
