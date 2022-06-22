using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Trackito.ASP.NET.Data
{
    public partial class TrackitoContext : DbContext
    {
        public TrackitoContext()
        {
        }

        public TrackitoContext(DbContextOptions<TrackitoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aday> Adays { get; set; } = null!;
        public virtual DbSet<Aliment> Aliments { get; set; } = null!;
        public virtual DbSet<AlimentDay> AlimentDays { get; set; } = null!;
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<training> training { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aday>(entity =>
            {
                entity.ToTable("Aday");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Adays)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Aday_ToTable");
            });

            modelBuilder.Entity<Aliment>(entity =>
            {
                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<AlimentDay>(entity =>
            {
                entity.ToTable("AlimentDay");

                entity.HasOne(d => d.Aliment)
                    .WithMany(p => p.AlimentDays)
                    .HasForeignKey(d => d.AlimentId)
                    .HasConstraintName("FK_AlimentDay_ToTable_1");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.AlimentDays)
                    .HasForeignKey(d => d.DayId)
                    .HasConstraintName("FK_AlimentDay_ToTable");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.ToTable("Exercise");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryMuscle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.Sets)
                    .HasForeignKey(d => d.ExerciseId)
                    .HasConstraintName("FK_Sets_ToTable_1");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.Sets)
                    .HasForeignKey(d => d.TrainingId)
                    .HasConstraintName("FK_Sets_ToTable");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<training>(entity =>
            {
                entity.ToTable("Training");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DateTraining).HasColumnType("date");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.training)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Training_ToTable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
