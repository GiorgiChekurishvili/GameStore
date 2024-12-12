using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Authentication.Requests.Commands;
using GameStore.Domain.Entities.Authentication;
using GameStore.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Authentication.Handles.Commands
{
    public class LoginHandler : IRequestHandler<LoginRequest, string>
    {
        readonly IAuthRepository _authRepository;
        readonly IMapper _mapper;
        readonly IConfiguration _config;
        public LoginHandler(IAuthRepository authRepository, IMapper mapper, IConfiguration config)
        {
            _config = config;
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<User>(request.UserDTO);
            var data = await _authRepository.Login(map);
            if (data == null)
                throw new NotFoundException("User Not Found");
            if(!VerifyPasswordHash(request.UserDTO!.Password, data.PasswordHash!, data.PasswordSalt!))
                throw new BadRequestException("Password is incorrect");
            string token = CreateToken(data.Id, data.UserName, data.Email, data.Role!.RoleName);
            return token;
            
        }
        private string CreateToken(int id, string username, string email, string RoleName)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, RoleName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;


        }

        private bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = System.Text.Encoding.UTF8.GetBytes(password);
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
    }
}
