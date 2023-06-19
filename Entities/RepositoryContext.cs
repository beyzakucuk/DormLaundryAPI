using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }
        public RepositoryContext():base() { }
        public virtual DbSet<Student>? Students { get; set; }
        public virtual  DbSet<Turn>? Turns { get; set; }
        public virtual DbSet<Admin>? Admins { get; set; }
        public virtual  DbSet<Machine>? Machines { get; set; }

    }
}
