
using Microsoft.EntityFrameworkCore;


namespace LWAApi.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User>? User { get; set; }
        public virtual DbSet<Student>? Student { get; set; }
        public virtual DbSet<Department>? Department { get; set; }
        public object Users { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Usersregister");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.FirstName).HasMaxLength(15).IsUnicode(false);
                entity.Property(e => e.LastName).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(256).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Gender).HasMaxLength(1).IsUnicode(false);

            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");
                //entity.HasKey("StudentID");
                entity.Property(e => e.StudentID).HasColumnName("StudentID");
                entity.Property(e => e.Studentname).HasMaxLength(15).IsUnicode(false);
                entity.Property(e => e.Studentaddress).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.Studentmarks).HasMaxLength(256).IsUnicode(false);
                entity.Property(e => e.Studentdepartment).HasMaxLength(50).IsUnicode(false);

            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");
                entity.Property(e => e.DepID).HasColumnName("DepID");
                entity.Property(e => e.Depname).HasMaxLength(15).IsUnicode(false);

            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


