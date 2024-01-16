using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\CoreDemo;database=CoreBlogDb; integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(m => m.HomeMatches)
                .HasForeignKey(m => m.HomeTeamID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.GuestTeam)
                .WithMany(m => m.GuestMatches)
                .HasForeignKey(m => m.GuestTeamID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message2>()
                .HasOne(m => m.SenderUser)
                .WithMany(m => m.WriterSender)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message2>()
                .HasOne(m => m.ReceiverUser)
                .WithMany(m => m.WriterReceiver)
                .HasForeignKey(m => m.ReceiverID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // base.OnModelCreating(modelBuilder);
        }

        public DbSet<About> Abouts { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Writer> Writers { get; set; }

        public DbSet<NewsLetter> NewsLetters { get; set; }

        public DbSet<BlogRating> BlogRatings { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Message2> Message2s { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Team> Teams { get; set; }
    }
}