using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class CapstoneRecipeDatabaseContext : DbContext
    {
        public CapstoneRecipeDatabaseContext()
        {
        }

        public CapstoneRecipeDatabaseContext(DbContextOptions<CapstoneRecipeDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountManager> AccountManagers { get; set; } = null!;
        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseList> CourseLists { get; set; } = null!;
        public virtual DbSet<Cuisine> Cuisines { get; set; } = null!;
        public virtual DbSet<Diet> Diets { get; set; } = null!;
        public virtual DbSet<DietList> DietLists { get; set; } = null!;
        public virtual DbSet<IngredientList> IngredientLists { get; set; } = null!;
        public virtual DbSet<Instruction> Instructions { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<PersonReview> PersonReviews { get; set; } = null!;
        public virtual DbSet<PublishState> PublishStates { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;
        public virtual DbSet<SeasonList> SeasonLists { get; set; } = null!;
        public virtual DbSet<Timing> Timings { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        */
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountManager>(entity =>
            {
                entity.ToTable("AccountManager");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastLogin).HasColumnType("date");

                entity.Property(e => e.LastLogout).HasColumnType("date");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.AccountManagers)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountManager_Person");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<CourseList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CourseList");

                entity.HasOne(d => d.Course)
                    .WithMany()
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseList_Course");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseList_Recipe");
            });

            modelBuilder.Entity<Cuisine>(entity =>
            {
                entity.ToTable("Cuisine");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.Region).HasMaxLength(255);
            });

            modelBuilder.Entity<Diet>(entity =>
            {
                entity.ToTable("Diet");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<DietList>(entity =>
            {
                entity.ToTable("DietList");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Diet)
                    .WithMany(p => p.DietLists)
                    .HasForeignKey(d => d.DietId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DietList_Diet");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.DietLists)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DietList_Recipe");
            });

            modelBuilder.Entity<IngredientList>(entity =>
            {
                entity.ToTable("IngredientList");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Instruction>(entity =>
            {
                entity.ToTable("Instruction");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.StepWithDoneness)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Instructions)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instruction_Recipe");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_AccountType");
            });

            modelBuilder.Entity<PersonReview>(entity =>
            {
                entity.ToTable("PersonReview");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonReviews)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonReview_Person");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.PersonReviews)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonReview_Recipe");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.PersonReviews)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonReview_Review");
            });

            modelBuilder.Entity<PublishState>(entity =>
            {
                entity.ToTable("PublishState");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.PublishStates)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublishState_Recipe");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.PublishStates)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublishState_Review");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Story).HasMaxLength(4000);

                entity.HasOne(d => d.Cuisine)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.CuisineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_Cuisine");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_Person");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.IhaveAquestion).HasColumnName("IHaveAQuestion");

                entity.Property(e => e.ImadeThis).HasColumnName("IMadeThis");

                entity.Property(e => e.PublishDate).HasColumnType("date");
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("Season");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SeasonName).HasMaxLength(255);
            });

            modelBuilder.Entity<SeasonList>(entity =>
            {
                entity.ToTable("SeasonList");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.SeasonLists)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SeasonList_Recipe");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.SeasonLists)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SeasonList_Season");
            });

            modelBuilder.Entity<Timing>(entity =>
            {
                entity.ToTable("Timing");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Timings)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Timing_Recipe");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
