﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Allasinterju.Database.Models;

public partial class AllasinterjuContext : DbContext
{
    public AllasinterjuContext()
    {
    }

    public AllasinterjuContext(DbContextOptions<AllasinterjuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alla> Allas { get; set; }

    public virtual DbSet<Allaskapcsolattarto> Allaskapcsolattartos { get; set; }

    public virtual DbSet<Allaskerdoiv> Allaskerdoivs { get; set; }

    public virtual DbSet<Allaskompetencium> Allaskompetencia { get; set; }

    public virtual DbSet<Allasvizsgalo> Allasvizsgalos { get; set; }

    public virtual DbSet<Ceg> Cegs { get; set; }

    public virtual DbSet<Cegtelephely> Cegtelephelies { get; set; }

    public virtual DbSet<Dokumentum> Dokumenta { get; set; }

    public virtual DbSet<Felhasznalo> Felhasznalos { get; set; }

    public virtual DbSet<Felhasznalokompetencium> Felhasznalokompetencia { get; set; }

    public virtual DbSet<Kerde> Kerdes { get; set; }

    public virtual DbSet<Kerdoiv> Kerdoivs { get; set; }

    public virtual DbSet<Kitoltottalla> Kitoltottallas { get; set; }

    public virtual DbSet<Kitoltottkerde> Kitoltottkerdes { get; set; }

    public virtual DbSet<Kitoltottkerdoiv> Kitoltottkerdoivs { get; set; }

    public virtual DbSet<Kitoltottvalasz> Kitoltottvalaszs { get; set; }

    public virtual DbSet<Kompetencium> Kompetencia { get; set; }

    public virtual DbSet<Lefutottteszteset> Lefutotttesztesets { get; set; }

    public virtual DbSet<Meghivokod> Meghivokods { get; set; }

    public virtual DbSet<Teszteset> Tesztesets { get; set; }

    public virtual DbSet<Valasz> Valaszs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-8DSO4K8;Initial Catalog=allasinterju;Encrypt=False;TrustServerCertificate=True;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alla>(entity =>
        {
            entity.ToTable("allas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cegid).HasColumnName("cegid");
            entity.Property(e => e.Cim).HasColumnName("cim");
            entity.Property(e => e.Hatarido)
                .HasColumnType("datetime")
                .HasColumnName("hatarido");
            entity.Property(e => e.Kitoltesido).HasColumnName("kitoltesido");
            entity.Property(e => e.Leiras).HasColumnName("leiras");
            entity.Property(e => e.Munkakor).HasColumnName("munkakor");
            entity.Property(e => e.Munkarend)
                .HasMaxLength(50)
                .HasColumnName("munkarend");
            entity.Property(e => e.Rovidleiras).HasColumnName("rovidleiras");
            entity.Property(e => e.Telephelyszoveg).HasColumnName("telephelyszoveg");

            entity.HasOne(d => d.Ceg).WithMany(p => p.Allas)
                .HasForeignKey(d => d.Cegid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allas_ceg");
        });

        modelBuilder.Entity<Allaskapcsolattarto>(entity =>
        {
            entity.ToTable("allaskapcsolattarto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Allasid).HasColumnName("allasid");
            entity.Property(e => e.Kapcsolattartoid).HasColumnName("kapcsolattartoid");

            entity.HasOne(d => d.Allas).WithMany(p => p.Allaskapcsolattartos)
                .HasForeignKey(d => d.Allasid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allaskapcsolattarto_allas");

            entity.HasOne(d => d.Kapcsolattarto).WithMany(p => p.Allaskapcsolattartos)
                .HasForeignKey(d => d.Kapcsolattartoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allaskapcsolattarto_felhasznalo");
        });

        modelBuilder.Entity<Allaskerdoiv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_allaskerdes");

            entity.ToTable("allaskerdoiv");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Kerdesid).HasColumnName("kerdesid");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Sorszam).HasColumnName("sorszam");

            entity.HasOne(d => d.Kerdes).WithMany(p => p.Allaskerdoivs)
                .HasForeignKey(d => d.Kerdesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allaskerdoiv_kerdes");

            entity.HasOne(d => d.Kerdoiv).WithMany(p => p.Allaskerdoivs)
                .HasForeignKey(d => d.Kerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allaskerdoiv_kerdoiv");
        });

        modelBuilder.Entity<Allaskompetencium>(entity =>
        {
            entity.ToTable("allaskompetencia");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Allasid).HasColumnName("allasid");
            entity.Property(e => e.Kompetenciaid).HasColumnName("kompetenciaid");

            entity.HasOne(d => d.Allas).WithMany(p => p.Allaskompetencia)
                .HasForeignKey(d => d.Allasid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allaskompetencia_allas");

            entity.HasOne(d => d.Kompetencia).WithMany(p => p.Allaskompetencia)
                .HasForeignKey(d => d.Kompetenciaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allaskompetencia_kompetencia");
        });

        modelBuilder.Entity<Allasvizsgalo>(entity =>
        {
            entity.ToTable("allasvizsgalo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Allasid).HasColumnName("allasid");
            entity.Property(e => e.Felhasznaloid).HasColumnName("felhasznaloid");

            entity.HasOne(d => d.Allas).WithMany(p => p.Allasvizsgalos)
                .HasForeignKey(d => d.Allasid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allasvizsgalo_allas");

            entity.HasOne(d => d.Felhasznalo).WithMany(p => p.Allasvizsgalos)
                .HasForeignKey(d => d.Felhasznaloid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_allasvizsgalo_felhasznalo");
        });

        modelBuilder.Entity<Ceg>(entity =>
        {
            entity.ToTable("ceg");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cegnev).HasColumnName("cegnev");
            entity.Property(e => e.Cegtipus)
                .HasMaxLength(50)
                .HasColumnName("cegtipus");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Jelszo).HasColumnName("jelszo");
            entity.Property(e => e.Kapcsolattarto).HasColumnName("kapcsolattarto");
            entity.Property(e => e.Kapcsolattartonev).HasColumnName("kapcsolattartonev");
            entity.Property(e => e.Kep).HasColumnName("kep");
            entity.Property(e => e.Leiras).HasColumnName("leiras");
            entity.Property(e => e.Levelezesicim).HasColumnName("levelezesicim");
            entity.Property(e => e.Mobiltelefon).HasColumnName("mobiltelefon");
            entity.Property(e => e.Telefon).HasColumnName("telefon");
            entity.Property(e => e.Telephely).HasColumnName("telephely");
        });

        modelBuilder.Entity<Cegtelephely>(entity =>
        {
            entity.ToTable("cegtelephely");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cegid).HasColumnName("cegid");
            entity.Property(e => e.Cimszoveg).HasColumnName("cimszoveg");
            entity.Property(e => e.Irsz).HasColumnName("irsz");
            entity.Property(e => e.Telepules).HasColumnName("telepules");
            entity.Property(e => e.Utcahazszam).HasColumnName("utcahazszam");
        });

        modelBuilder.Entity<Dokumentum>(entity =>
        {
            entity.ToTable("dokumentum");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Fajl).HasColumnName("fajl");
            entity.Property(e => e.Fajlnev).HasColumnName("fajlnev");
            entity.Property(e => e.Felhasznaloid).HasColumnName("felhasznaloid");
            entity.Property(e => e.Leiras).HasColumnName("leiras");

            entity.HasOne(d => d.Felhasznalo).WithMany(p => p.Dokumenta)
                .HasForeignKey(d => d.Felhasznaloid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dokumentum_felhasznalo");
        });

        modelBuilder.Entity<Felhasznalo>(entity =>
        {
            entity.ToTable("felhasznalo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adoszam).HasColumnName("adoszam");
            entity.Property(e => e.Allaskereso).HasColumnName("allaskereso");
            entity.Property(e => e.Anyjaneve).HasColumnName("anyjaneve");
            entity.Property(e => e.Cegid).HasColumnName("cegid");
            entity.Property(e => e.Dolgozo).HasColumnName("dolgozo");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Jelszo).HasColumnName("jelszo");
            entity.Property(e => e.Kep).HasColumnName("kep");
            entity.Property(e => e.Keresztnev)
                .HasMaxLength(50)
                .HasColumnName("keresztnev");
            entity.Property(e => e.Leetcode).HasColumnName("leetcode");
            entity.Property(e => e.Szuldat)
                .HasColumnType("datetime")
                .HasColumnName("szuldat");
            entity.Property(e => e.Szulhely).HasColumnName("szulhely");
            entity.Property(e => e.Szulirsz).HasColumnName("szulirsz");
            entity.Property(e => e.Vezeteknev)
                .HasMaxLength(50)
                .HasColumnName("vezeteknev");
        });

        modelBuilder.Entity<Felhasznalokompetencium>(entity =>
        {
            entity.ToTable("felhasznalokompetencia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Felhasznaloid).HasColumnName("felhasznaloid");
            entity.Property(e => e.Kompetenciaid).HasColumnName("kompetenciaid");

            entity.HasOne(d => d.Felhasznalo).WithMany(p => p.Felhasznalokompetencia)
                .HasForeignKey(d => d.Felhasznaloid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_felhasznalokompetencia_felhasznalo");

            entity.HasOne(d => d.Kompetencia).WithMany(p => p.Felhasznalokompetencia)
                .HasForeignKey(d => d.Kompetenciaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_felhasznalokompetencia_kompetencia");
        });

        modelBuilder.Entity<Kerde>(entity =>
        {
            entity.ToTable("kerdes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Feleletvalasztos).HasColumnName("feleletvalasztos");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Kifejtos).HasColumnName("kifejtos");
            entity.Property(e => e.Maxpont).HasColumnName("maxpont");
            entity.Property(e => e.Programalapszoveg).HasColumnName("programalapszoveg");
            entity.Property(e => e.Programozos).HasColumnName("programozos");
            entity.Property(e => e.Programteszteset).HasColumnName("programteszteset");
            entity.Property(e => e.Sorrendkerdes).HasColumnName("sorrendkerdes");
            entity.Property(e => e.Szoveg).HasColumnName("szoveg");

            entity.HasOne(d => d.Kerdoiv).WithMany(p => p.Kerdes)
                .HasForeignKey(d => d.Kerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kerdes_kerdoiv");
        });

        modelBuilder.Entity<Kerdoiv>(entity =>
        {
            entity.ToTable("kerdoiv");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Allasid).HasColumnName("allasid");
            entity.Property(e => e.Kitoltesperc).HasColumnName("kitoltesperc");
            entity.Property(e => e.Kor).HasColumnName("kor");
            entity.Property(e => e.Maxpont).HasColumnName("maxpont");
            entity.Property(e => e.Nev).HasColumnName("nev");

            entity.HasOne(d => d.Allas).WithMany(p => p.Kerdoivs)
                .HasForeignKey(d => d.Allasid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kerdoiv_allas");
        });

        modelBuilder.Entity<Kitoltottalla>(entity =>
        {
            entity.ToTable("kitoltottallas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Allasid).HasColumnName("allasid");
            entity.Property(e => e.Allaskeresoid).HasColumnName("allaskeresoid");
            entity.Property(e => e.Kitolteskezdet)
                .HasColumnType("datetime")
                .HasColumnName("kitolteskezdet");

            entity.HasOne(d => d.Allas).WithMany(p => p.Kitoltottallas)
                .HasForeignKey(d => d.Allasid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kitoltottallas_allas");

            entity.HasOne(d => d.Allaskereso).WithMany(p => p.Kitoltottallas)
                .HasForeignKey(d => d.Allaskeresoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kitoltottallas_felhasznalo");
        });

        modelBuilder.Entity<Kitoltottkerde>(entity =>
        {
            entity.ToTable("kitoltottkerdes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Elertpont).HasColumnName("elertpont");
            entity.Property(e => e.Kerdesid).HasColumnName("kerdesid");
            entity.Property(e => e.Kitoltottkerdoivid).HasColumnName("kitoltottkerdoivid");
            entity.Property(e => e.Programhelyes).HasColumnName("programhelyes");
            entity.Property(e => e.Szovegesvalasz).HasColumnName("szovegesvalasz");
            entity.Property(e => e.Valasztosid).HasColumnName("valasztosid");

            entity.HasOne(d => d.Kerdes).WithMany(p => p.Kitoltottkerdes)
                .HasForeignKey(d => d.Kerdesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kitoltottkerdes_kerdes");

            entity.HasOne(d => d.Kitoltottkerdoiv).WithMany(p => p.Kitoltottkerdes)
                .HasForeignKey(d => d.Kitoltottkerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kitoltottkerdes_kitoltottallas");

            entity.HasOne(d => d.Valasztos).WithMany(p => p.Kitoltottkerdes)
                .HasForeignKey(d => d.Valasztosid)
                .HasConstraintName("FK_kitoltottkerdes_valasz");
        });

        modelBuilder.Entity<Kitoltottkerdoiv>(entity =>
        {
            entity.ToTable("kitoltottkerdoiv");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Befejezve).HasColumnName("befejezve");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Kitolteskezdet)
                .HasColumnType("datetime")
                .HasColumnName("kitolteskezdet");
            entity.Property(e => e.Kitoltottallasid).HasColumnName("kitoltottallasid");
            entity.Property(e => e.Miajanlas).HasColumnName("miajanlas");
            entity.Property(e => e.Osszpont).HasColumnName("osszpont");
            entity.Property(e => e.Tovabbjut).HasColumnName("tovabbjut");

            entity.HasOne(d => d.Kerdoiv).WithMany(p => p.Kitoltottkerdoivs)
                .HasForeignKey(d => d.Kerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kitoltottkerdoiv_kerdoiv");

            entity.HasOne(d => d.Kitoltottallas).WithMany(p => p.Kitoltottkerdoivs)
                .HasForeignKey(d => d.Kitoltottallasid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kitoltottkerdoiv_kitoltottallas");
        });

        modelBuilder.Entity<Kitoltottvalasz>(entity =>
        {
            entity.ToTable("kitoltottvalasz");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Elertpont).HasColumnName("elertpont");
            entity.Property(e => e.Kitoltottkerdesid).HasColumnName("kitoltottkerdesid");
            entity.Property(e => e.Szovegesvalasz).HasColumnName("szovegesvalasz");
            entity.Property(e => e.Valaszid).HasColumnName("valaszid");

            entity.HasOne(d => d.Kitoltottkerdes).WithMany(p => p.Kitoltottvalaszs)
                .HasForeignKey(d => d.Kitoltottkerdesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kitoltottvalasz_kitoltottkerdes");

            entity.HasOne(d => d.Valasz).WithMany(p => p.Kitoltottvalaszs)
                .HasForeignKey(d => d.Valaszid)
                .HasConstraintName("FK_kitoltottvalasz_valasz");
        });

        modelBuilder.Entity<Kompetencium>(entity =>
        {
            entity.ToTable("kompetencia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipus).HasColumnName("tipus");
        });

        modelBuilder.Entity<Lefutottteszteset>(entity =>
        {
            entity.ToTable("lefutottteszteset");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Helyes).HasColumnName("helyes");
            entity.Property(e => e.Kimenet).HasColumnName("kimenet");
            entity.Property(e => e.Kitoltottkerdesid).HasColumnName("kitoltottkerdesid");
            entity.Property(e => e.Tesztesetid).HasColumnName("tesztesetid");

            entity.HasOne(d => d.Kitoltottkerdes).WithMany(p => p.Lefutotttesztesets)
                .HasForeignKey(d => d.Kitoltottkerdesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_lefutottteszteset_kitoltottkerdes");

            entity.HasOne(d => d.Teszteset).WithMany(p => p.Lefutotttesztesets)
                .HasForeignKey(d => d.Tesztesetid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_lefutottteszteset_teszteset");
        });

        modelBuilder.Entity<Meghivokod>(entity =>
        {
            entity.ToTable("meghivokod");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cegid).HasColumnName("cegid");
            entity.Property(e => e.Ervenyesseg)
                .HasColumnType("datetime")
                .HasColumnName("ervenyesseg");
            entity.Property(e => e.Kod)
                .HasMaxLength(50)
                .HasColumnName("kod");

            entity.HasOne(d => d.Ceg).WithMany(p => p.Meghivokods)
                .HasForeignKey(d => d.Cegid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_meghivokod_ceg");
        });

        modelBuilder.Entity<Teszteset>(entity =>
        {
            entity.ToTable("teszteset");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bemenet).HasColumnName("bemenet");
            entity.Property(e => e.Kerdesid).HasColumnName("kerdesid");
            entity.Property(e => e.Kimenet).HasColumnName("kimenet");

            entity.HasOne(d => d.Kerdes).WithMany(p => p.Tesztesets)
                .HasForeignKey(d => d.Kerdesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teszteset_kerdes");
        });

        modelBuilder.Entity<Valasz>(entity =>
        {
            entity.ToTable("valasz");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Helyes).HasColumnName("helyes");
            entity.Property(e => e.Kerdesid).HasColumnName("kerdesid");
            entity.Property(e => e.Pontszam).HasColumnName("pontszam");
            entity.Property(e => e.Szoveg).HasColumnName("szoveg");

            entity.HasOne(d => d.Kerdes).WithMany(p => p.Valaszs)
                .HasForeignKey(d => d.Kerdesid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_valasz_kerdes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}