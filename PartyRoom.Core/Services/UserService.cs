using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Core.Interfaces.Services;
using System.Security.Claims;

namespace PartyRoom.Core.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProfileRepository _profileRepository;
        public UserService(IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository, IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _profileRepository = profileRepository;
        }
        public async Task CreateUserAsync(UserRegistrationDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var userMap = _mapper.Map<ApplicationUser>(user);

            await _userRepository.AddAsync(userMap, user.Password);
            var role = await _roleRepository.Models.FirstOrDefaultAsync(r => r.Name.ToLower() == "user");
            await _userRepository.AddRoleAsync(userMap, role);
            var claim = new Claim("Role", role.Name);
            await _userRepository.AddClaimAsync(userMap, claim);
            await _profileRepository.AddAsync(new UserProfile
            {
                //ApplicationUser = userMap,
                ApplicationUserId = userMap.Id,
                About = userMap.PhoneNumber,
                ImagePath = ""
            });
            await _profileRepository.SaveChangesAsync();

        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

        }

        public Task UpdateUserAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            return user;
        }

        public async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = await _userRepository.GetClaimsAsync(user);
            return claims;
        }

     
    }
}
