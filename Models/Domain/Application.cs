﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Application")]
    public class Application
    {
        public int Id { get; set; }
        public string Motivation { get; set; } = null!;
        public bool ApprovalStatus { get; set; }

        // Relationships
        [ForeignKey("User")]
        public string? UserId { get; set; }

        [InverseProperty("Applications")]
        public User? User { get; set; } = null!;

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }

        [InverseProperty("Applications")]
        public Project? Project { get; set; } = null!;
    }
}
