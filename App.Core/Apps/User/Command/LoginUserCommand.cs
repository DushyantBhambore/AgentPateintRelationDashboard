using App.Core.Dto;
using App.Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Command
{
    public class LoginUserCommand : IRequest<string>
    {
        public LoginDto login { get; set; }
    }
    public class LoginUserCommandHandller : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandller(IAppDbContext appDbContext , IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var checkemail = await _appDbContext.Set<Domain.User>().FirstOrDefaultAsync(x=>x.Email == request.login.Email);
            if(checkemail == null)
            {
                return null;
            }
            var checkpassword = BCrypt.Net.BCrypt.Verify(request.login.Password, checkemail.Password);
            if(checkpassword == null)
            {
                return null;
            }

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim("Id", checkemail.UserId.ToString()),
                new Claim("Email",checkemail.Email),
                new Claim("FirstName",checkemail.FirstName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claim,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: signIn);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            //var obj = new Domain.User
            //{
            //    UserId = checkemail.UserId,
            //    FirstName = checkemail.FirstName,
            //    LastName = checkemail.LastName,
            //    Email = checkemail.Email,
            //    AgentId = checkemail.AgentId,
            //    Password = checkemail.Password
            //};

            return JsonSerializer.Serialize(new {success = true, token = jwt , user = checkemail});
        }
    }
}
