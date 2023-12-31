﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PartyRoom.Core.Entities
{
	public class Tag
	{
		
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool Important { get; set; }

        public ApplicationUser ApplicationUser { get; set; } = null!;

        public Guid ApplicationUserId { get; set; }

        public bool IsLike { get; set; } = false;
    }
}

