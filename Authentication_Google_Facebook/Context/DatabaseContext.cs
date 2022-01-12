using Authentication_Google_Facebook.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication_Google_Facebook.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
         
    }
}
