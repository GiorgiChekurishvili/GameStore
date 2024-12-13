using AutoMapper;
using GameStore.Application.DTOs.UserDTO;
using GameStore.Application.DTOs.UserDTO.Validators;
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
    public class RegisterHandler : IRequestHandler<RegisterRequest, Unit>
    {
        readonly IAuthRepository _authRepository;
        readonly IMapper _mapper;
        public RegisterHandler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var validator = new RegisterUserDTOValidator();
            var validationResult = await validator.ValidateAsync(request.User!);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHash(request.User!.Password!, out PasswordHash, out PasswordSalt);
            var map = _mapper.Map<User>(request.User);
            map.PasswordHash = PasswordHash;
            map.PasswordSalt = PasswordSalt;
            map.RoleId = 2;
            await _authRepository.Register(map);
            return Unit.Value;
        }

        public void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = System.Text.Encoding.UTF8.GetBytes(password);
            }
        } 
    }
}
