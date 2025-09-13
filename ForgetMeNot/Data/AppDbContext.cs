using ForgetMeNot.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ForgetMeNot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<TaskItem> Tasks { get; set; } = default!;
    }
}
