using System;
using Microsoft.EntityFrameworkCore;
using PartyRoom.Core.Entities;
using PartyRoom.Core.Interfaces.Repositories;
using PartyRoom.Infrastructure.Data;

namespace PartyRoom.Infrastructure.Repositories
{
    public class TagRepository : RepositoryBase<Tag>,ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ICollection<Tag> Get(Guid userId)
        {
            var tags = _context.Tags.AsNoTracking().Where(t => t.ApplicationUserId == userId).OrderBy(t => t.Important).ToList();
            return tags;
        }
    }
}

