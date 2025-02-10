using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _context.Projects
            .Include(p => p.Customer)
            .Include(p => p.Status)
            .Include(p => p.User)
            .Include(p => p.Product)
            .ToListAsync();
    }
}
