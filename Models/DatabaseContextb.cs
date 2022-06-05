using Microsoft.EntityFrameworkCore;

namespace LWAApi.Models
{
    public partial class DatabaseContextb:DbContext
    {
        public DatabaseContextb()
        {
        }

        public DatabaseContextb(DbContextOptions<DatabaseContextb> option)
               : base(option)
        {
        }

        public DbSet<Players>? Playerstbl { get; set; }
        public DbSet<Teams>? Teamstbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Players>(entity =>
            {
                entity.ToTable("Players");
                entity.Property(e => e.Playerid).HasColumnName("Playerid");
                entity.Property(e => e.Playername).HasMaxLength(15).IsUnicode(false);
                entity.Property(e => e.Teamid).HasMaxLength(100).IsUnicode(false);
                

            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.ToTable("Teams");
                entity.HasKey("Teamid");
                entity.Property(e => e.Teamid).HasColumnName("Teamid");
                entity.Property(e => e.Teamname).HasMaxLength(15).IsUnicode(false);
               

            });

        

            OnModelCreatingPartial(modelBuilder);
        }

        
        

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }


}
