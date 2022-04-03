using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Persistance
{
    public partial class WeatherForecastDbContext : DbContext
    {
        public WeatherForecastDbContext()
        {
        }

        public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WeatherForecastTbl> WeatherForecastTbls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=TRAINEE-06;Database=WeatherForecastDb;User Id=SA;Password=MyPassword123;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecastTbl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WeatherForecastTbl");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Summary)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
