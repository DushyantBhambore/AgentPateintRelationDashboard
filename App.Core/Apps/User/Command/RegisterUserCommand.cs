using App.Core.Dto;
using App.Core.Interface;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Command
{
    public class RegisterUserCommand : IRequest<Domain.User>
    {
        public RegisterDto register { get; set; }
    }
    public class RegisterUserCommandHandller : IRequestHandler<RegisterUserCommand, Domain.User>
    {
        private readonly IAppDbContext _appDbContext;

        public RegisterUserCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Domain.User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var checkemail = await _appDbContext.Set<Domain.User>().AnyAsync(x=> x.Email == request.register.Email);
            if(checkemail)
            {

                return null; 
            }

            //var lastAgentId = _appDbContext.Set<Domain.User>().Where(x=> x.AgentId == request.register.AgentId).FirstOrDefault();
            var lastAgentId = await _appDbContext.Set<Domain.User>().OrderByDescending(x => x.AgentId).FirstOrDefaultAsync();
            string newAgentId ;
            if (lastAgentId == null) 
            {
                newAgentId  ="PE001";
            }
            else
            {
                var value = int.Parse(lastAgentId.AgentId.Substring(2));
                newAgentId = "PE" + (value + 1).ToString("D3");
            }

            var haspassword = BCrypt.Net.BCrypt.HashPassword(request.register.Password);

            var adduser = new Domain.User
            {
                UserId = request.register.UserId,
                AgentId = newAgentId,
                FirstName = request.register.FirstName,
                LastName = request.register.LastName,
                Email = request.register.Email,
                Password = haspassword,

            };
             await _appDbContext.Set<Domain.User>().AddAsync(adduser);
            await _appDbContext.SaveChangesAsync();
            return adduser.Adapt<Domain.User>();

        }
    }
}
