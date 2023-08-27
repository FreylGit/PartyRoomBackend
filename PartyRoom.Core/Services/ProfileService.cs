using AutoMapper;
using Microsoft.AspNetCore.Http;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Core.Interfaces.Services;

namespace PartyRoom.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;
        public ProfileService(IMapper mapper, IProfileRepository profileRepository, IUserRepository userRepository, IImageService imageService)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _imageService = imageService;
        }
        public async Task<UserDTO> GetProfileAsync(Guid id)
        {
            var userFind = await _userRepository.GetProfileUserByIdAsync(id);
            var userMap = _mapper.Map<UserDTO>(userFind);
            return userMap;
        }

        public async Task<UserPublicDTO> GetProfilePublicAsync(Guid id)
        {
            var userFind = await _userRepository.GetProfileUserByIdAsync(id);
            var userMap = _mapper.Map<UserPublicDTO>(userFind);
            return userMap;
        }

        public async Task UpdateImageAsync(Guid userId, IFormFile image)
        {
            var userProfile = await _profileRepository.GetByIdAsync(userId);
            var path = await _imageService.SaveImageAsync(image);
            userProfile.ImagePath = path;
            _profileRepository.Update(userProfile);
            await _profileRepository.SaveChangesAsync();
        }
    }
}
