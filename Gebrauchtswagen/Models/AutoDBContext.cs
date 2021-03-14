using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Gebrauchtswagen.Models
{
    public partial class AutoDBContext : DbContext
    {
        public AutoDBContext()
        {
        }

        public AutoDBContext(DbContextOptions<AutoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fahrzeug> Fahrzeugs { get; set; }
        public virtual DbSet<Kunde> Kundes { get; set; }
        public virtual DbSet<Rechnung> Rechnungs { get; set; }
        public virtual DbSet<Rechnungfahrzeug> Rechnungfahrzeugs { get; set; }
        public virtual DbSet<Verkaeufer> Verkaeufers { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AutoDB;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Fahrzeug>(entity =>
            {
                entity.ToTable("Fahrzeug");

                entity.Property(e => e.FahrzeugId).HasColumnName("fahrzeug_id");

                entity.Property(e => e.Bezeichnung)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bezeichnung")
                    .IsFixedLength(true);

                entity.Property(e => e.Preis)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("preis");

                entity.Property(e => e.Zustand)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("zustand")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Kunde>(entity =>
            {
                entity.ToTable("Kunde");

                entity.Property(e => e.KundeId).HasColumnName("kunde_id");

                entity.Property(e => e.Hn)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("hn")
                    .IsFixedLength(true);

                entity.Property(e => e.Nachname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nachname")
                    .IsFixedLength(true);

                entity.Property(e => e.Ort)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ort")
                    .IsFixedLength(true);

                entity.Property(e => e.Plz)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("plz")
                    .IsFixedLength(true);

                entity.Property(e => e.Strasse)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("strasse")
                    .IsFixedLength(true);

                entity.Property(e => e.Vorname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vorname")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Rechnung>(entity =>
            {
                entity.ToTable("Rechnung");

                entity.Property(e => e.RechnungId)
                    .ValueGeneratedNever()
                    .HasColumnName("rechnung_id");

                entity.Property(e => e.Datum)
                    .HasColumnType("date")
                    .HasColumnName("datum");

                entity.Property(e => e.KundeId).HasColumnName("kunde_id");

                entity.Property(e => e.RechnungfahrzeugId).HasColumnName("rechnungfahrzeug_id");

                entity.Property(e => e.Rechnungsnummer).HasColumnName("rechnungsnummer");

                entity.Property(e => e.VerkaeuferId).HasColumnName("verkaeufer_id");

                entity.HasOne(d => d.Kunde)
                    .WithMany(p => p.Rechnungs)
                    .HasForeignKey(d => d.KundeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rechnung_kunde");

                entity.HasOne(d => d.Rechnungfahrzeug)
                    .WithMany(p => p.Rechnungs)
                    .HasForeignKey(d => d.RechnungfahrzeugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rechnung_rechnungfahrzeug");

                entity.HasOne(d => d.Verkaeufer)
                    .WithMany(p => p.Rechnungs)
                    .HasForeignKey(d => d.VerkaeuferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rechnung_verkaeufer");
            });

            modelBuilder.Entity<Rechnungfahrzeug>(entity =>
            {
                entity.ToTable("Rechnungfahrzeug");

                entity.Property(e => e.RechnungfahrzeugId)
                    .ValueGeneratedNever()
                    .HasColumnName("rechnungfahrzeug_id");

                entity.Property(e => e.FahrzeugId).HasColumnName("fahrzeug_id");

                entity.Property(e => e.Menge).HasColumnName("menge");

                entity.Property(e => e.PreisBeiRechnungserstellung)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("preis_bei_rechnungserstellung");

                entity.HasOne(d => d.Fahrzeug)
                    .WithMany(p => p.Rechnungfahrzeugs)
                    .HasForeignKey(d => d.FahrzeugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rechnungfahrzeug_fahrzeug");
            });

            modelBuilder.Entity<Verkaeufer>(entity =>
            {
                entity.ToTable("Verkaeufer");

                entity.Property(e => e.VerkaeuferId).HasColumnName("verkaeufer_id");

                entity.Property(e => e.Nachname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nachname")
                    .IsFixedLength(true);

                entity.Property(e => e.Vorname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vorname")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
