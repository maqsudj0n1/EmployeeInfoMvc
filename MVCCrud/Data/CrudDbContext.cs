using Microsoft.EntityFrameworkCore;
using MVCCrud.Models.Domain;

namespace MVCCrud.Data
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
