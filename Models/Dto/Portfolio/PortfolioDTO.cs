﻿using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Models.Dto.Portfolio
{
    public class PortfolioDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        // Relationships
        public int UserId { get; set; }

        public int? ProjectId { get; set; }
        public List<int> Projects { get; set; } = new List<int>();
    }
}
