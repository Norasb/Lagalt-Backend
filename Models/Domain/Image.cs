﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Image")]
    public class Image
    {
        public int Id { get; set; }
        public string? Description { get; set; } = null!;
        public string Url { get; set; } = null!;

        // Relationships
        public int ProjectId { get; set; }

        [InverseProperty("Images")]
        public Project Project { get; set; } = null!;
    }
}
