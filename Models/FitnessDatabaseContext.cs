using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Models;

public partial class FitnessDatabaseContext : DbContext
{
    public FitnessDatabaseContext()
    {
    }

    public FitnessDatabaseContext(DbContextOptions<FitnessDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Challenge> Challenges { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserRate> UserRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:FitnessDatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.HasKey(e => e.ChallangeId);

            entity.Property(e => e.ChallangeId).HasColumnName("Challange_Id");
            entity.Property(e => e.ChallangeCategory)
                .HasMaxLength(50)
                .HasColumnName("Challange_Category");
            entity.Property(e => e.ChallangeDesc).HasColumnName("Challange_Desc");
            entity.Property(e => e.ChallangeDifficulty).HasColumnName("Challange_Difficulty");
            entity.Property(e => e.ChallangeEndDate)
                .HasColumnType("datetime")
                .HasColumnName("Challange_EndDate");
            entity.Property(e => e.ChallangeIsDeleted).HasColumnName("Challange_isDeleted");
            entity.Property(e => e.ChallangeName)
                .HasMaxLength(50)
                .HasColumnName("Challange_Name");
            entity.Property(e => e.ChallangeStartDate)
                .HasColumnType("datetime")
                .HasColumnName("Challange_StartDate");
            entity.Property(e => e.ChallangeUserId)
                .HasMaxLength(450)
                .HasColumnName("Challange_UserId");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CommentId).HasColumnName("Comment_Id");
            entity.Property(e => e.CommentChallengeId).HasColumnName("Comment_Challenge_Id");
            entity.Property(e => e.CommentText).HasColumnName("Comment_Text");
            entity.Property(e => e.CommentUserId)
                .HasMaxLength(450)
                .HasColumnName("Comment_User_Id");
            entity.Property(e => e.CommentUserName)
                .HasMaxLength(100)
                .HasColumnName("Comment_UserName");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.Property(e => e.FavoriteId).HasColumnName("Favorite_id");
            entity.Property(e => e.FavoriteChallengeId).HasColumnName("Favorite_ChallengeId");
            entity.Property(e => e.FavoriteUserId)
                .HasMaxLength(450)
                .HasColumnName("Favorite_UserId");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.ToTable("UserDetail");

            entity.Property(e => e.UserDetailId).HasColumnName("UserDetail_Id");
            entity.Property(e => e.UserDetailBio).HasColumnName("UserDetail_Bio");
            entity.Property(e => e.UserDetailPhoto).HasColumnName("UserDetail_Photo");
            entity.Property(e => e.UserDetailUserId)
                .HasMaxLength(450)
                .HasColumnName("UserDetail_UserId");
        });

        modelBuilder.Entity<UserRate>(entity =>
        {
            entity.ToTable("UserRate");

            entity.Property(e => e.UserRateId).HasColumnName("UserRate_Id");
            entity.Property(e => e.UserRateChallengeId).HasColumnName("UserRate_ChallengeId");
            entity.Property(e => e.UserRateRate).HasColumnName("UserRate_Rate");
            entity.Property(e => e.UserRateUserId)
                .HasMaxLength(450)
                .HasColumnName("UserRate_UserId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
