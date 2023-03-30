using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

            var project = new Project
            {
                Field = obj.Field,
                Title = obj.Title,
                Caption = obj.Caption,
                Description = obj.Description,
                Progress = obj.Progress,
                Owner = obj.Owner
            };

            var owner = await _context.Users.SingleOrDefaultAsync(u => u.Id == obj.Owner.Id);
            project.Owner = owner;

            foreach (var skillName in obj.Skills)
            {
                var skill = await _context.Skills.SingleOrDefaultAsync(s => s.Name == skillName.Name);
                project.Skills.Add(skill);
            }

            foreach (var tagName in obj.Tags)
            {
                var tag = await _context.Tags.SingleOrDefaultAsync(t => t.Name == tagName.Name);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName.Name };
                    _context.Tags.Add(tag);
                }
                project.Tags.Add(tag);
            }

            foreach (var linkUrl in obj.Links)
            {
                var link = await _context.Links.SingleOrDefaultAsync(l => l.URL == linkUrl.URL);
                if (link == null)
                {
                    link = new Link { URL = linkUrl.URL };
                    _context.Links.Add(link);
                }
                project.Links.Add(link);
            }

            foreach (var imageUrl in obj.Images)
            {
                var image = await _context.Images.SingleOrDefaultAsync(i => i.Url == imageUrl.Url);
                if (image == null)
                {
                    image = new Image { Url = imageUrl.Url };
                    _context.Images.Add(image);
                }
                project.Images.Add(image);
            }

            await _context.AddAsync(project);
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
                .Include(p => p.Owner)
                .Include(p => p.Contributors)
                .Include(p => p.Images)
                .Include(p => p.Tags)
                .Include(p => p.Skills)
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
            Project project = await _context.Projects
                .Where(p => p.Id == obj.Id)
                .Include(p => p.Tags)
                .Include(p => p.Skills)
                .Include(p => p.Images)
                .Include(p => p.Links)
                .Include(p => p.Contributors)
                .FirstAsync();

            if (project == null)
            {
                throw new Exception("Project not found");
            }
            project.Id = obj.Id;
            project.Title = obj.Title;
            project.Field = obj.Field;
            project.Caption = obj.Caption;
            project.Description = obj.Description;
            project.Progress = obj.Progress;

            foreach (var skillName in obj.Skills)
            {
                var skill = await _context.Skills.SingleOrDefaultAsync(s => s.Name == skillName.Name);
                project.Skills.Add(skill);
            }

            foreach (var contributor in obj.Contributors)
            {
                var user = await _context.Users.FindAsync(contributor.Id);
                project.Contributors.Add(user);
            }

            foreach (var imageUrl in obj.Images)
            {
                var image = await _context.Images.SingleOrDefaultAsync(i => i.Url == imageUrl.Url);
                if (image == null)
                {
                    image = new Image { Url = imageUrl.Url };
                    _context.Images.Add(image);
                }
                project.Images.Add(image);
            }

            foreach (var linkUrl in obj.Links)
            {
                var link = await _context.Links.SingleOrDefaultAsync(l => l.URL == linkUrl.URL);
                if (link == null)
                {
                    link = new Link { URL = linkUrl.URL };
                    _context.Links.Add(link);
                }
                project.Links.Add(link);
            }

            foreach (var tagName in obj.Tags)
            {
                var tag = await _context.Tags.SingleOrDefaultAsync(t => t.Name == tagName.Name);
                if (tag == null)
                {
                    tag = new Tag { Name = tagName.Name };
                    _context.Tags.Add(tag);
                }
                project.Tags.Add(tag);
            }



            //Ready if we want the availablity to remove something that is not presented in the input body
            // Remove any skills not present in the updated object
            //var skillsToRemove = project.Skills.Where(s => !obj.Skills.Any(os => os.Name == s.Name)).ToList();
            //foreach (var skill in skillsToRemove)
            //{
            //    project.Skills.Remove(skill);
            //}

            //// Remove any images not present in the updated object
            //var imagesToRemove = project.Images.Where(i => !obj.Images.Any(oi => oi.Id == i.Id)).ToList();
            //foreach (var image in imagesToRemove)
            //{
            //    project.Images.Remove(image);
            //}

            //// Remove any links not present in the updated object
            //var linksToRemove = project.Links.Where(l => !obj.Links.Any(ol => ol.Id == l.Id)).ToList();
            //foreach (var link in linksToRemove)
            //{
            //    project.Links.Remove(link);
            //}

            //// Remove any tags not present in the updated object
            //var tagsToRemove = project.Tags.Where(t => !obj.Tags.Any(ot => ot.Name == t.Name)).ToList();
            //foreach (var tag in tagsToRemove)
            //{
            //    project.Tags.Remove(tag);
            //}

            //// Remove any contributors not present in the updated object
            //var contributorsToRemove = project.Contributors.Where(c => !obj.Contributors.Any(oc => oc.Id == c.Id)).ToList();
            //foreach (var contributor in contributorsToRemove)
            //{
            //    project.Contributors.Remove(contributor);
            //}


            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContributorsAsync(Project obj)
        {
            Project project = await _context.Projects
                .Where(p => p.Id == obj.Id)
                .Include(p => p.Contributors)
                .FirstAsync();

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            foreach (var item in obj.Contributors)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == item.Id);
                project.Contributors.Add(user);
            }

            _context.Entry(project).State = EntityState.Modified;
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
                .Include(p => p.Owner)
                .Include(p => p.Contributors)
                .Include(p => p.Images)
                .Include(p => p.Tags)
                .Include(p => p.Skills)
                .ToListAsync();

            var remainingProjects = await _context.Projects
                .Where(p => !matchingSkills
                .Select(sp => sp.ProjectId)
                .Contains(p.Id))
                .Include(p => p.Owner)
                .Include(p => p.Contributors)
                .Include(p => p.Images)
                .Include(p => p.Tags)
                .Include(p => p.Skills)
                .ToListAsync();

            var allProjects = matchingProjects.Concat(remainingProjects).ToList();

            return allProjects;
        }

        public async Task<ICollection<Application>> GetNotApprovedApplications(int projectId)
        {
            return await _context.Applications
                .Where(a => a.ProjectId == projectId && a.ApprovalStatus == false)
                .Include(a => a.User)
                .Include(a => a.Project)
                .ToListAsync();
        }
    }
}
