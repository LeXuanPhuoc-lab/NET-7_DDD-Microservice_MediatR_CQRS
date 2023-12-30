using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Commands
{
    public record UpdateAccountCommand(string Email, string Password) : IRequest<AccountDto>;

    public class UpdateAccountCommandHandler 
        : IRequestHandler<UpdateAccountCommand, AccountDto>
    {
        private readonly IDriverLicenseLearningSupportContext _context;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(IDriverLicenseLearningSupportContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (account is null) return null!;

            // update fields
            account.Password = request.Password;
            // save changes
            await _context.SaveChangeAsync(cancellationToken);
            
            return _mapper.Map<Account, AccountDto>(account);
        }
    }
}
