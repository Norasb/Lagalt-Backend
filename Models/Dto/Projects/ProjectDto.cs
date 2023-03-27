﻿namespace Lagalt_Backend.Models.Dto.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Field { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public DateTime DOC { get; set; }
        public string Progress { get; set; } = null!;
        
        public int Owner { get; set; }
        public List<int> Contributors { get; set; } = new List<int>();
        public List<int> Images { get; set; } = new List<int>();
        public List<int> Tags { get; set; } = new List<int>();
        public List<int> Links { get; set; } = new List<int>();
        public List<int> Skills { get; set; } = new List<int>();
        public List<int> Portfolio { get; set; } = new List<int>();
        public List<int> Messages { get; set; } = new List<int>();
        public List<int> Applications { get; set; } = new List<int>();
    }
}
