﻿using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lagalt_Backend.Services.Projects
{
    public class ProjectService : IProjectService
    {

        private readonly LagAltDbContext _context;

        public ProjectService(LagAltDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Project obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                throw new EntryPointNotFoundException();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Project>> GetAllAsync()
        {
            return await _context.Projects
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Where(p => p.Id == id)
                .Include(p => p.Owner)
                .Include(p => p.Contributors)
                .Include(p => p.Images)
                .Include(p => p.Tags)
                .Include(p => p.Skills)
                .Include(p => p.Links)
                .FirstAsync();
        }

        public async Task UpdateAsync(Project obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Project>> GetProjectsBySkill(string userId)
        {
            var userSkills = await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Skills, (u, s) => new { UserId = u.Id, SkillId = s.Id })
                .ToListAsync();

            var projectSkills = await _context.Projects
                .SelectMany(p => p.Skills, (p, s) => new { ProjectId = p.Id, SkillId = s.Id })
                .ToListAsync();

            var matchingSkills = userSkills.Join(projectSkills,
                us => us.SkillId,
                ps => ps.SkillId,
                (us, ps) => new
                {
                    ps.ProjectId,
                    us.UserId,
                    ps.SkillId,
                })
                .GroupBy(p => p.ProjectId)
                .Select(g => new { ProjectId = g.Key, Matches = g.Select(p => p.SkillId).Distinct().Count() })
                .OrderByDescending(g => g.Matches);

            var matchingProjects = await _context.Projects
                .Where(p => matchingSkills
                .Select(sp => sp.ProjectId)
                .Contains(p.Id))
                .ToListAsync();

            var remainingProjects = await _context.Projects
                .Where(p => !matchingSkills
                .Select(sp => sp.ProjectId)
                .Contains(p.Id))
                .ToListAsync();

            var allProjects = matchingProjects.Concat(remainingProjects).ToList();

            return allProjects;
        }
    }
}
