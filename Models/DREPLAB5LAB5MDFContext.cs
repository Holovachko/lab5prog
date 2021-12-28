using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace lab5.Models
{
    public partial class DREPLAB5LAB5MDFContext : DbContext
    {
        public DREPLAB5LAB5MDFContext()
        {
        }

        public DREPLAB5LAB5MDFContext(DbContextOptions<DREPLAB5LAB5MDFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Color> Color { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=LAB5");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cards>(entity =>
            {
                entity.Property(e => e.Published).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.card)
                    .WithMany(p => p.Color)
                    .HasForeignKey(d => d.cardId)
                    .HasConstraintName("FK_Color_cards");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
