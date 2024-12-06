using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Authentication.Requests.Commands;
using GameStore.Domain.Entities.Authentication;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Authentication.Handles.Commands
{
    public class LoginHandler : IRequestHandler<LoginRequest, string>
    {
        readonly IAuthRepository _authRepository;
        readonly IMapper _mapper;
        public LoginHandler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var data = await _authRepository.CheckUserByUserName(request.UserDTO!.UserName);
            if (data == null)
                throw new NotFoundException("User Not Found");
            if(!VerifyPasswordHash(request.UserDTO.Password, data.PasswordHash!, data.PasswordSalt!))
                throw new BadRequestException("Password is incorrect");
            
            var map = _mapper.Map<User>(request.UserDTO);
            var token = await _authRepository.Login(map);
            return token;
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
