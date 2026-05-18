using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<School> Schools { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── User (TPH hierarchy) ──────────────────────────────────────
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.RowVersion).IsRowVersion();

            entity.HasDiscriminator<string>("UserType")
                .HasValue<Student>("Student")
                .HasValue<Teacher>("Teacher")
                .HasValue<Admin>("Admin");

            entity.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(40).IsRequired();
                name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(40).IsRequired();
            });

            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("Email").HasMaxLength(256).IsRequired();
                email.HasIndex(e => e.Value).IsUnique();
            });

            entity.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.HashedValue).HasColumnName("Password").IsRequired();
            });

            entity.OwnsMany(u => u.Roles, r =>
            {
                r.ToTable("UserRoles");
                r.Property(ur => ur.RoleType).IsRequired();
            });
        });

        // Student-specific columns
        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(s => s.Knr).HasMaxLength(50);
            entity.Property(s => s.StartDate);
            entity.Property(s => s.EndDate);
        });

        // ── School ────────────────────────────────────────────────────
        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.RowVersion).IsRowVersion();

            entity.OwnsOne(s => s.Title, title =>
            {
                title.Property(t => t.Value).HasColumnName("Title").HasMaxLength(50).IsRequired();
            });

            entity.HasMany(s => s.Classes)
                .WithOne()
                .HasForeignKey("SchoolId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(s => s.SchoolResponsibles)
                .WithMany()
                .UsingEntity(j => j.ToTable("SchoolResponsibles"));
        });

        // ── Class ─────────────────────────────────────────────────────
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.RowVersion).IsRowVersion();

            entity.OwnsOne(c => c.Title, title =>
            {
                title.Property(t => t.Value).HasColumnName("Title").HasMaxLength(50).IsRequired();
            });

            entity.OwnsOne(c => c.ClassDateRange, dr =>
            {
                dr.Property(d => d.Start).HasColumnName("StartDate").IsRequired();
                dr.Property(d => d.End).HasColumnName("EndDate").IsRequired();
            });

            entity.HasMany(c => c.Teachers)
                .WithMany()
                .UsingEntity(j => j.ToTable("ClassTeachers"));

            entity.HasMany(c => c.Students)
                .WithMany()
                .UsingEntity(j => j.ToTable("ClassStudents"));

            entity.HasMany(c => c.Subjects)
                .WithMany()
                .UsingEntity(j => j.ToTable("ClassSubjects"));
        });

        // ── Subject ───────────────────────────────────────────────────
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.RowVersion).IsRowVersion();

            entity.OwnsOne(s => s.Title, title =>
            {
                title.Property(t => t.Value).HasColumnName("Title").HasMaxLength(50).IsRequired();
            });

            entity.Property(s => s.Level).IsRequired();

            entity.HasMany(s => s.Topics)
                .WithOne()
                .HasForeignKey("SubjectId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ── Topic ─────────────────────────────────────────────────────
        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.RowVersion).IsRowVersion();
            entity.Property(t => t.Name).HasMaxLength(100).IsRequired();
        });

        // ── Question ──────────────────────────────────────────────────
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(q => q.Id);
            entity.Property(q => q.RowVersion).IsRowVersion();

            entity.OwnsOne(q => q.Title, title =>
            {
                title.Property(t => t.Value).HasColumnName("Title").HasMaxLength(50).IsRequired();
            });

            entity.Property(q => q.Text).IsRequired();
            entity.Property(q => q.Points).IsRequired();
            entity.Property(q => q.ActiveStatus).IsRequired();
            entity.Property(q => q.CreatedByUserId).IsRequired();

            entity.HasOne(q => q.ParentQuestion)
                .WithMany()
                .HasForeignKey("ParentQuestionId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            entity.HasOne(q => q.Answer)
                .WithOne()
                .HasForeignKey<Answer>(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(q => q.Tags)
                .WithMany()
                .UsingEntity(j => j.ToTable("QuestionTags"));

            entity.HasMany(q => q.Subjects)
                .WithMany()
                .UsingEntity(j => j.ToTable("QuestionSubjects"));
        });

        // ── Answer ────────────────────────────────────────────────────
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.RowVersion).IsRowVersion();

            entity.OwnsOne(a => a.Title, title =>
            {
                title.Property(t => t.Value).HasColumnName("Title").HasMaxLength(50).IsRequired();
            });

            entity.Property(a => a.Text).IsRequired();
            entity.Property(a => a.QuestionId).IsRequired();
            entity.Property(a => a.CreatedByUserId).IsRequired();
        });

        // ── Tag ───────────────────────────────────────────────────────
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.RowVersion).IsRowVersion();

            entity.OwnsOne(t => t.Title, title =>
            {
                title.Property(t => t.Value).HasColumnName("Title").HasMaxLength(50).IsRequired();
            });

            entity.Property(t => t.Description).HasMaxLength(500);
        });
    }
}