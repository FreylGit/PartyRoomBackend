using System;
using PartyRoom.Core.Entities;

namespace PartyRoom.Core.Interfaces.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        ICollection<Tag> Get(Guid userId);
        Task AddAsync(List<Tag> tags);
    }
}

