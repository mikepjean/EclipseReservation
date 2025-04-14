using Microsoft.EntityFrameworkCore;
using EclipseLink.UserManagement;
using EclipseLink.EventManagement;
using EclipseLink.UserEventManagement;

namespace EclipseLink.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<UserEvent>()
                .HasKey(ue => ue.UserEvent_Id);

            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany()
                .HasForeignKey(ue => ue.User_Id);

            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany()
                .HasForeignKey(ue => ue.Event_Id);
        }
    }
}
