using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.User;

namespace dotnet_rpg.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> users = new List<User> {
            new User { Id = 0, Name = "Sam" }
        };
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            User character = _mapper.Map<User>(newUser);
            character.Id = users.Max(c => c.Id) + 1;
            users.Add(character);
            serviceResponse.Data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUserById(int id)
        {
            ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>>();

            try
            {
                User character = users.First(c => c.Id == id);
                users.Remove(character);
                response.Data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            return new ServiceResponse<List<GetUserDto>>
            {
                Data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var character = users.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();

            try
            {
                User user = users.FirstOrDefault(c => c.Id == updatedUser.Id);

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Phone = updatedUser.Phone;
                user.Type = updatedUser.Type;

                response.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}