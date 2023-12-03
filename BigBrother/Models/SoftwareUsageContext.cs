using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BigBrother.Models;

public partial class SoftwareUsageContext : DbContext
{
    public SoftwareUsageContext()
    {
    }

    public SoftwareUsageContext(DbContextOptions<SoftwareUsageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SoftwareUsage> SoftwareUsages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=bit.uni-corvinus.hu;Initial Catalog=SoftwareUsage;Persist Security Info=True;User ID=hallgato;Password=Password123;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SoftwareUsage>(entity =>
        {
            entity.ToTable("SoftwareUsage");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
