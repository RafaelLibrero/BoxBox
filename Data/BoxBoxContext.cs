using BoxBox.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxBox.Data
{
    public class BoxBoxContext: DbContext
    {
        public BoxBoxContext(DbContextOptions<BoxBoxContext> options)
        :base (options) { }

        public DbSet<Forum> forums { get; set; }
    }
}
