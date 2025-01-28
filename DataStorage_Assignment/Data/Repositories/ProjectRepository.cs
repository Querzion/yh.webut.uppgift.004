using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProductEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;
}
