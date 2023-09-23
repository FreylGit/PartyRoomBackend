
namespace PartyRoom.Core.DTOs.Tag
{
	public struct TagDTO
	{
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Important { get; set; }

        public bool IsLike { get; set; }
    }
}

