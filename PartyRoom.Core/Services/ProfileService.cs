﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.DTOs.Tag;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;
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
        private readonly ITagRepository _tagRepository;
        public ProfileService(IMapper mapper, IProfileRepository profileRepository, IUserRepository userRepository,
            IImageService imageService, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _imageService = imageService;
            _tagRepository = tagRepository;
        }

        public async Task AddTagAsync(Guid userId, TagCreateDTO tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException("Tag не может быть null");
            }

            var tagMap = _mapper.Map<Tag>(tag);
            if (tagMap == null)
            {
                throw new InvalidOperationException("Не удалось смаппить Tag");
            }

            tagMap.ApplicationUserId = userId;
            await _tagRepository.AddAsync(tagMap);
            await _tagRepository.SaveChangesAsync();

        }

        public async Task AddTagAsync(Guid userId, List<TagCreateDTO> tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException("Tags не может быть null");
            }

            var tagMap = _mapper.Map<List<Tag>>(tag);
            if (tagMap == null)
            {
                throw new InvalidOperationException("Не удалось смаппить Tag");
            }

            for(int i = 0; i < tagMap.Count(); i++)
            {
                tagMap[i].ApplicationUserId = userId;
            }
            await _tagRepository.AddAsync(tagMap);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(Guid userId, Guid tagId)
        {
            var tag = await _tagRepository.Models.FirstOrDefaultAsync(t => t.Id == tagId && t.ApplicationUserId == userId);
            _tagRepository.Delete(tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task<UserDTO> GetProfileAsync(Guid id)
        {
            var userFind = await _profileRepository.GetUserByIdAsync(id);
            var tags = _tagRepository.Get(id).ToList();
            var tagsMap = _mapper.Map<List<TagDTO>>(tags);

            var userMap = _mapper.Map<UserDTO>(userFind);
            userMap.Tags = tagsMap;
            return userMap;
        }

        public async Task<UserPublicDTO> GetProfilePublicAsync(Guid id)
        {
            var userFind = await _profileRepository.GetUserByIdAsync(id);
            var userMap = _mapper.Map<UserPublicDTO>(userFind);
            return userMap;
        }

        public async Task<UserPublicDTO> GetProfilePublicAsync(string username)
        {
            var userFind = await _profileRepository.GetUserByUsernameAsync(username);
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

        public async Task UpdateProfileAsync(UserProfileUpdateDTO userProfile, Guid userId)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException("Полфиль пустой");
            }
            //TODO Протестить
            var tags = userProfile.NewTags;

            if (tags != null)
            {
                await AddTagAsync(userId, tags);
            }
           
           

            var profile = _mapper.Map<UserProfile>(userProfile);
            profile.ApplicationUserId = userId;
            if (profile == null)
            {
                throw new InvalidCastException("Ошибка маппинга");
            }

            if (profile.ApplicationUserId == Guid.Empty)
            {
                throw new ArgumentNullException("Пустой пользователь");
            }

            _profileRepository.Update(profile);
            await _profileRepository.SaveChangesAsync();
        }
    }
}

