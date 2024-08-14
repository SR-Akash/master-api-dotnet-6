using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using master_api_dotnet_6.Models;

namespace master_api_dotnet_6.DBContext;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AKASH-300174;Initial Catalog=CRUD_DB;Connect Timeout=30;Encrypt=False;Trusted_Connection=true;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("tblStudent");

            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("tblUser");

            entity.Property(e => e.ConfirmPassword).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LastActionDatetime).HasColumnType("datetime");
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.PreviousPassword).HasMaxLength(250);
            entity.Property(e => e.UserName).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
