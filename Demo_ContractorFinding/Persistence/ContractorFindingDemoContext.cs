using System;
using System.Collections.Generic;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class ContractorFindingDemoContext : DbContext
{
    public ContractorFindingDemoContext()
    {
    }

    public ContractorFindingDemoContext(DbContextOptions<ContractorFindingDemoContext> options)
        : base(options)
    {

    }

    public virtual DbSet<ContractorDetail> ContractorDetails { get; set; }

    public virtual DbSet<ServiceProviding> ServiceProvidings { get; set; }

    public virtual DbSet<TbBuilding> TbBuildings { get; set; }

    public virtual DbSet<TbCustomer> TbCustomers { get; set; }

    public virtual DbSet<TbGender> TbGenders { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }



//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=Contractor_FindingDemo;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContractorDetail>(entity =>
        {
            entity.HasKey(e => e.License).HasName("PK__Contract__A4E54DE5FAE3A5AB");

            entity.ToTable("Contractor_details");

            entity.Property(e => e.License)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("license");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

            entity.HasOne(d => d.Contractor).WithMany(p => p.ContractorDetails)
                .HasForeignKey(d => d.ContractorId)
                .HasConstraintName("FK__Contracto__Contr__2F10007B");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.ContractorDetails)
                .HasForeignKey(d => d.Gender)
                .HasConstraintName("FK__Contracto__Gende__300424B4");

            entity.HasOne(d => d.ServicesNavigation).WithMany(p => p.ContractorDetails)
                .HasForeignKey(d => d.Services)
                .HasConstraintName("FK__Contracto__Servi__30F848ED");
        });

        modelBuilder.Entity<ServiceProviding>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service___C51BB00ABA13A9B4");

            entity.ToTable("Service_providing");

            entity.HasIndex(e => e.ServiceName, "UQ__Service___A42B5F992D8FC1E8").IsUnique();

            entity.Property(e => e.ServiceId).ValueGeneratedNever();
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbBuilding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tb_Build__3214EC27728C9835");

            entity.ToTable("Tb_Building");

            entity.HasIndex(e => e.Building, "UQ__Tb_Build__553663718C6FAA28").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Building)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.RegistrationNo).HasName("PK__Tb_Custo__6EF5E043F70E9256");

            entity.ToTable("Tb_Customer");

            entity.Property(e => e.RegistrationNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BuildingType).HasColumnName("Building_Type");
            entity.Property(e => e.LandSqft).HasColumnName("Land_sqft");

            entity.HasOne(d => d.BuildingTypeNavigation).WithMany(p => p.TbCustomers)
                .HasForeignKey(d => d.BuildingType)
                .HasConstraintName("FK__Tb_Custom__Build__36B12243");
        });

        modelBuilder.Entity<TbGender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Tb_Gende__4E24E9F7C3BE85D7");

            entity.ToTable("Tb_Gender");

            entity.Property(e => e.GenderId).ValueGeneratedNever();
            entity.Property(e => e.GenderType)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Tb_User__1788CC4C6D3ED13A");

            entity.ToTable("Tb_User");

            entity.HasIndex(e => e.EmailId, "UQ__Tb_User__7ED91ACEFB1324C9").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.TypeUserNavigation).WithMany(p => p.TbUsers)
                .HasForeignKey(d => d.TypeUser)
                .HasConstraintName("FK__Tb_User__TypeUse__276EDEB3");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__User_Typ__F04DF13AE376873E");

            entity.ToTable("User_Type");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("typeId");
            entity.Property(e => e.Usertype1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("usertype");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
