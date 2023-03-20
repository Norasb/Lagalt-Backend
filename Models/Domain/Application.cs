﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Lagalt_Backend.Models.Domain
{
    [Table("Application")]
    public class Application
    {
        public int Id { get; set; }
        public string Motivation { get; set; } = null!;

        // Relationships
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
