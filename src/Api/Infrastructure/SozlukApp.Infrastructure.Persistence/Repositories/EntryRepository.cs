using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Api.Domain.Models;

namespace SozlukApp.Infrastructure.Persistence.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(DbContext _context) : base(_context)
        {
        }
    }
}
