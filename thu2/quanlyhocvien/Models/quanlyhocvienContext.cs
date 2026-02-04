using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace quanlyhocvien.Models
{
    public partial class quanlyhocvienContext : DbContext
    {
        public quanlyhocvienContext()
        {
        }

        public quanlyhocvienContext(DbContextOptions<quanlyhocvienContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Diemthi> Diemthis { get; set; } = null!;
        public virtual DbSet<Lop> Lops { get; set; } = null!;
        public virtual DbSet<Lylich> Lyliches { get; set; } = null!;
        public virtual DbSet<Monhoc> Monhocs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-TL2BT7J;Initial Catalog=quanlyhocvien;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diemthi>(entity =>
            {
                entity.HasKey(e => new { e.Mshv, e.Msmh });

                entity.ToTable("diemthi");

                entity.HasIndex(e => e.Msmh, "IX_diemthi_msmh");

                entity.Property(e => e.Mshv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mshv");

                entity.Property(e => e.Msmh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("msmh");

                entity.Property(e => e.Diem)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("diem");

                entity.HasOne(d => d.MshvNavigation)
                    .WithMany(p => p.Diemthis)
                    .HasForeignKey(d => d.Mshv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_diemthi_lylich");

                entity.HasOne(d => d.MsmhNavigation)
                    .WithMany(p => p.Diemthis)
                    .HasForeignKey(d => d.Msmh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_diemthi_monhoc");
            });

            modelBuilder.Entity<Lop>(entity =>
            {
                entity.HasKey(e => e.Malop);

                entity.ToTable("lop");

                entity.Property(e => e.Malop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("malop");

                entity.Property(e => e.Tenlop)
                    .HasMaxLength(50)
                    .HasColumnName("tenlop");
            });

            modelBuilder.Entity<Lylich>(entity =>
            {
                entity.HasKey(e => e.Mshv);

                entity.ToTable("lylich");

                entity.HasIndex(e => e.Malop, "IX_lylich_malop");

                entity.Property(e => e.Mshv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mshv");

                entity.Property(e => e.Malop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("malop");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Phai).HasColumnName("phai");

                entity.Property(e => e.Tenhv)
                    .HasMaxLength(50)
                    .HasColumnName("tenhv");

                entity.HasOne(d => d.MalopNavigation)
                    .WithMany(p => p.Lyliches)
                    .HasForeignKey(d => d.Malop)
                    .HasConstraintName("FK_lylich_lop");
            });

            modelBuilder.Entity<Monhoc>(entity =>
            {
                entity.HasKey(e => e.Msmh);

                entity.ToTable("monhoc");

                entity.Property(e => e.Msmh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("msmh");

                entity.Property(e => e.Sotiet).HasColumnName("sotiet");

                entity.Property(e => e.Tenmh)
                    .HasMaxLength(50)
                    .HasColumnName("tenmh");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
