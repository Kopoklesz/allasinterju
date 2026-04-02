using System;
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

    public virtual DbSet<Ajanla> Ajanlas { get; set; }

    public virtual DbSet<Algorithm> Algorithms { get; set; }

    public virtual DbSet<Algorithmconstraint> Algorithmconstraints { get; set; }

    public virtual DbSet<Algorithmexample> Algorithmexamples { get; set; }

    public virtual DbSet<Algorithmhint> Algorithmhints { get; set; }

    public virtual DbSet<Algortihmtestcase> Algortihmtestcases { get; set; }

    public virtual DbSet<Alla> Allas { get; set; }

    public virtual DbSet<Allaskapcsolattarto> Allaskapcsolattartos { get; set; }

    public virtual DbSet<Allaskerdoiv> Allaskerdoivs { get; set; }

    public virtual DbSet<Allaskompetencium> Allaskompetencia { get; set; }

    public virtual DbSet<Allasvizsgalo> Allasvizsgalos { get; set; }

    public virtual DbSet<Ceg> Cegs { get; set; }

    public virtual DbSet<Cegtelephely> Cegtelephelies { get; set; }

    public virtual DbSet<Design> Designs { get; set; }

    public virtual DbSet<Designevaluation> Designevaluations { get; set; }

    public virtual DbSet<Designreference> Designreferences { get; set; }

    public virtual DbSet<Designreq> Designreqs { get; set; }

    public virtual DbSet<Devop> Devops { get; set; }

    public virtual DbSet<Devopscomponent> Devopscomponents { get; set; }

    public virtual DbSet<Devopsdeliverable> Devopsdeliverables { get; set; }

    public virtual DbSet<Devopsdocumentation> Devopsdocumentations { get; set; }

    public virtual DbSet<Devopsevaluation> Devopsevaluations { get; set; }

    public virtual DbSet<Devopsprereq> Devopsprereqs { get; set; }

    public virtual DbSet<Devopstask> Devopstasks { get; set; }

    public virtual DbSet<Devopstaskimplementation> Devopstaskimplementations { get; set; }

    public virtual DbSet<Dokumentum> Dokumenta { get; set; }

    public virtual DbSet<Felhasznalo> Felhasznalos { get; set; }

    public virtual DbSet<Felhasznalokompetencium> Felhasznalokompetencia { get; set; }

    public virtual DbSet<KProgramming> KProgrammings { get; set; }

    public virtual DbSet<KProgrammingtestcase> KProgrammingtestcases { get; set; }

    public virtual DbSet<KTobbi> KTobbis { get; set; }

    public virtual DbSet<Kerde> Kerdes { get; set; }

    public virtual DbSet<Kerdoiv> Kerdoivs { get; set; }

    public virtual DbSet<Kitoltottalla> Kitoltottallas { get; set; }

    public virtual DbSet<Kitoltottkerde> Kitoltottkerdes { get; set; }

    public virtual DbSet<Kitoltottkerdoiv> Kitoltottkerdoivs { get; set; }

    public virtual DbSet<Kitoltottvalasz> Kitoltottvalaszs { get; set; }

    public virtual DbSet<Kompetencium> Kompetencia { get; set; }

    public virtual DbSet<Lefutottteszteset> Lefutotttesztesets { get; set; }

    public virtual DbSet<Meghivokod> Meghivokods { get; set; }

    public virtual DbSet<Programming> Programmings { get; set; }

    public virtual DbSet<Programmingtestcase> Programmingtestcases { get; set; }

    public virtual DbSet<Testing> Testings { get; set; }

    public virtual DbSet<Testingcase> Testingcases { get; set; }

    public virtual DbSet<Testingcasestep> Testingcasesteps { get; set; }

    public virtual DbSet<Testingevaluation> Testingevaluations { get; set; }

    public virtual DbSet<Testingscenario> Testingscenarios { get; set; }

    public virtual DbSet<Testingtool> Testingtools { get; set; }

    public virtual DbSet<Teszteset> Tesztesets { get; set; }

    public virtual DbSet<Valasz> Valaszs { get; set; }

    public virtual DbSet<Vegzettseg> Vegzettsegs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=arch;initial catalog=allasinterju;user id=sa;password=Rootroot01;encrypt=false;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ajanla>(entity =>
        {
            entity.ToTable("ajanlas");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Allasid).HasColumnName("allasid");
            entity.Property(e => e.Allaskeresoid).HasColumnName("allaskeresoid");
            entity.Property(e => e.Jelentkezve).HasColumnName("jelentkezve");

            entity.HasOne(d => d.Allas).WithMany(p => p.Ajanlas)
                .HasForeignKey(d => d.Allasid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ajanlas_allas");

            entity.HasOne(d => d.Allaskereso).WithMany(p => p.Ajanlas)
                .HasForeignKey(d => d.Allaskeresoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ajanlas_felhasznalo");
        });

        modelBuilder.Entity<Algorithm>(entity =>
        {
            entity.ToTable("algorithm");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.Inputformat).HasColumnName("inputformat");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Memory).HasColumnName("memory");
            entity.Property(e => e.Outputformat).HasColumnName("outputformat");
            entity.Property(e => e.Problemdesc).HasColumnName("problemdesc");
            entity.Property(e => e.Samplesolution).HasColumnName("samplesolution");
            entity.Property(e => e.Spacecomplexity).HasColumnName("spacecomplexity");
            entity.Property(e => e.Timecomplexity).HasColumnName("timecomplexity");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Kerdoiv).WithMany(p => p.Algorithms)
                .HasForeignKey(d => d.Kerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_algorithm_kerdoiv");
        });

        modelBuilder.Entity<Algorithmconstraint>(entity =>
        {
            entity.ToTable("algorithmconstraint");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Algorithmid).HasColumnName("algorithmid");
            entity.Property(e => e.Constraint).HasColumnName("constraint");

            entity.HasOne(d => d.Algorithm).WithMany(p => p.Algorithmconstraints)
                .HasForeignKey(d => d.Algorithmid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_algorithmconstraint_algorithm");
        });

        modelBuilder.Entity<Algorithmexample>(entity =>
        {
            entity.ToTable("algorithmexample");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Algorithmid).HasColumnName("algorithmid");
            entity.Property(e => e.Explanation).HasColumnName("explanation");
            entity.Property(e => e.Input).HasColumnName("input");
            entity.Property(e => e.Output).HasColumnName("output");

            entity.HasOne(d => d.Algorithm).WithMany(p => p.Algorithmexamples)
                .HasForeignKey(d => d.Algorithmid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_algorithmexample_algorithm");
        });

        modelBuilder.Entity<Algorithmhint>(entity =>
        {
            entity.ToTable("algorithmhint");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Algorithmid).HasColumnName("algorithmid");
            entity.Property(e => e.Hint).HasColumnName("hint");

            entity.HasOne(d => d.Algorithm).WithMany(p => p.Algorithmhints)
                .HasForeignKey(d => d.Algorithmid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_algorithmhint_algorithm");
        });

        modelBuilder.Entity<Algortihmtestcase>(entity =>
        {
            entity.ToTable("algortihmtestcase");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Algorithmid).HasColumnName("algorithmid");
            entity.Property(e => e.Hidden).HasColumnName("hidden");
            entity.Property(e => e.Input).HasColumnName("input");
            entity.Property(e => e.Output).HasColumnName("output");
            entity.Property(e => e.Points).HasColumnName("points");

            entity.HasOne(d => d.Algorithm).WithMany(p => p.Algortihmtestcases)
                .HasForeignKey(d => d.Algorithmid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_algortihmtestcase_algorithm");
        });

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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Allasid).HasColumnName("allasid");
            entity.Property(e => e.Kompetenciaid).HasColumnName("kompetenciaid");
            entity.Property(e => e.Szint).HasColumnName("szint");

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

        modelBuilder.Entity<Design>(entity =>
        {
            entity.ToTable("design");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Deliverables).HasColumnName("deliverables");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Styleguide).HasColumnName("styleguide");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Kerdoiv).WithMany(p => p.Designs)
                .HasForeignKey(d => d.Kerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_design_kerdoiv");
        });

        modelBuilder.Entity<Designevaluation>(entity =>
        {
            entity.ToTable("designevaluation");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Designid).HasColumnName("designid");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Design).WithMany(p => p.Designevaluations)
                .HasForeignKey(d => d.Designid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_designevaluation_design");
        });

        modelBuilder.Entity<Designreference>(entity =>
        {
            entity.ToTable("designreference");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Designid).HasColumnName("designid");
            entity.Property(e => e.Url).HasColumnName("url");

            entity.HasOne(d => d.Design).WithMany(p => p.Designreferences)
                .HasForeignKey(d => d.Designid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_designreference_design");
        });

        modelBuilder.Entity<Designreq>(entity =>
        {
            entity.ToTable("designreq");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Designid).HasColumnName("designid");

            entity.HasOne(d => d.Design).WithMany(p => p.Designreqs)
                .HasForeignKey(d => d.Designid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_designreq_design");
        });

        modelBuilder.Entity<Devop>(entity =>
        {
            entity.ToTable("devops");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accessrequirements).HasColumnName("accessrequirements");
            entity.Property(e => e.Architecturedesc).HasColumnName("architecturedesc");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.Docformat).HasColumnName("docformat");
            entity.Property(e => e.Docrequired).HasColumnName("docrequired");
            entity.Property(e => e.Infraconstraints).HasColumnName("infraconstraints");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Platform).HasColumnName("platform");
            entity.Property(e => e.Resourcelimits).HasColumnName("resourcelimits");
            entity.Property(e => e.Systemrequirements).HasColumnName("systemrequirements");
            entity.Property(e => e.Taskdescription).HasColumnName("taskdescription");
            entity.Property(e => e.Tasktitle).HasColumnName("tasktitle");

            entity.HasOne(d => d.Kerdoiv).WithMany(p => p.DevopsNavigation)
                .HasForeignKey(d => d.Kerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devops_kerdoiv");
        });

        modelBuilder.Entity<Devopscomponent>(entity =>
        {
            entity.ToTable("devopscomponent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Configuration).HasColumnName("configuration");
            entity.Property(e => e.Devopsid).HasColumnName("devopsid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Devops).WithMany(p => p.Devopscomponents)
                .HasForeignKey(d => d.Devopsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devopscomponent_devops");
        });

        modelBuilder.Entity<Devopsdeliverable>(entity =>
        {
            entity.ToTable("devopsdeliverable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acceptance).HasColumnName("acceptance");
            entity.Property(e => e.Desc).HasColumnName("desc");
            entity.Property(e => e.Devopsid).HasColumnName("devopsid");
            entity.Property(e => e.Format).HasColumnName("format");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Devops).WithMany(p => p.Devopsdeliverables)
                .HasForeignKey(d => d.Devopsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devopsdeliverable_devops");
        });

        modelBuilder.Entity<Devopsdocumentation>(entity =>
        {
            entity.ToTable("devopsdocumentation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Devopsid).HasColumnName("devopsid");
            entity.Property(e => e.Requiredtemplate).HasColumnName("requiredtemplate");
            entity.Property(e => e.Templatecontent).HasColumnName("templatecontent");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Devops).WithMany(p => p.Devopsdocumentations)
                .HasForeignKey(d => d.Devopsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devopsdocumentation_devops");
        });

        modelBuilder.Entity<Devopsevaluation>(entity =>
        {
            entity.ToTable("devopsevaluation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Criterion).HasColumnName("criterion");
            entity.Property(e => e.Desc).HasColumnName("desc");
            entity.Property(e => e.Devopsid).HasColumnName("devopsid");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Devops).WithMany(p => p.Devopsevaluations)
                .HasForeignKey(d => d.Devopsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devopsevaluation_devops");
        });

        modelBuilder.Entity<Devopsprereq>(entity =>
        {
            entity.ToTable("devopsprereq");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Devopsid).HasColumnName("devopsid");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.Tool).HasColumnName("tool");
            entity.Property(e => e.Version).HasColumnName("version");

            entity.HasOne(d => d.Devops).WithMany(p => p.Devopsprereqs)
                .HasForeignKey(d => d.Devopsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devopsprereq_devops");
        });

        modelBuilder.Entity<Devopstask>(entity =>
        {
            entity.ToTable("devopstask");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Desc).HasColumnName("desc");
            entity.Property(e => e.Devopsid).HasColumnName("devopsid");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Validation).HasColumnName("validation");

            entity.HasOne(d => d.Devops).WithMany(p => p.Devopstasks)
                .HasForeignKey(d => d.Devopsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devopstask_devops");
        });

        modelBuilder.Entity<Devopstaskimplementation>(entity =>
        {
            entity.ToTable("devopstaskimplementation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Devopstaskid).HasColumnName("devopstaskid");
            entity.Property(e => e.Implementation).HasColumnName("implementation");

            entity.HasOne(d => d.Devopstask).WithMany(p => p.Devopstaskimplementations)
                .HasForeignKey(d => d.Devopstaskid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devopstaskimplementation_devopstask");
        });

        modelBuilder.Entity<Dokumentum>(entity =>
        {
            entity.ToTable("dokumentum");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.Szint).HasColumnName("szint");

            entity.HasOne(d => d.Felhasznalo).WithMany(p => p.Felhasznalokompetencia)
                .HasForeignKey(d => d.Felhasznaloid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_felhasznalokompetencia_felhasznalo");

            entity.HasOne(d => d.Kompetencia).WithMany(p => p.Felhasznalokompetencia)
                .HasForeignKey(d => d.Kompetenciaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_felhasznalokompetencia_kompetencia");
        });

        modelBuilder.Entity<KProgramming>(entity =>
        {
            entity.ToTable("k_programming");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Kitoltottkerdoivid).HasColumnName("kitoltottkerdoivid");
            entity.Property(e => e.Programkod).HasColumnName("programkod");
            entity.Property(e => e.Programmingid).HasColumnName("programmingid");

            entity.HasOne(d => d.Kitoltottkerdoiv).WithMany(p => p.KProgrammings)
                .HasForeignKey(d => d.Kitoltottkerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_k_programming_kitoltottkerdoiv");

            entity.HasOne(d => d.Programming).WithMany(p => p.KProgrammings)
                .HasForeignKey(d => d.Programmingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_k_programming_programming");
        });

        modelBuilder.Entity<KProgrammingtestcase>(entity =>
        {
            entity.ToTable("k_programmingtestcase");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Futasido).HasColumnName("futasido");
            entity.Property(e => e.Helyes).HasColumnName("helyes");
            entity.Property(e => e.KProgrammingid).HasColumnName("k_programmingid");
            entity.Property(e => e.Lefutott).HasColumnName("lefutott");
            entity.Property(e => e.Memoria).HasColumnName("memoria");
            entity.Property(e => e.Nemfutle).HasColumnName("nemfutle");
            entity.Property(e => e.Programmingtestcaseid).HasColumnName("programmingtestcaseid");
            entity.Property(e => e.Stderr).HasColumnName("stderr");
            entity.Property(e => e.Stdout).HasColumnName("stdout");

            entity.HasOne(d => d.KProgramming).WithMany(p => p.KProgrammingtestcases)
                .HasForeignKey(d => d.KProgrammingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_k_programmingtestcase_k_programming");

            entity.HasOne(d => d.Programmingtestcase).WithMany(p => p.KProgrammingtestcases)
                .HasForeignKey(d => d.Programmingtestcaseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_k_programmingtestcase_programmingtestcase");
        });

        modelBuilder.Entity<KTobbi>(entity =>
        {
            entity.ToTable("k_tobbi");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Algorithmid).HasColumnName("algorithmid");
            entity.Property(e => e.Designid).HasColumnName("designid");
            entity.Property(e => e.Devopsid).HasColumnName("devopsid");
            entity.Property(e => e.Kitoltottkerdoivid).HasColumnName("kitoltottkerdoivid");
            entity.Property(e => e.Szovegesvalasz).HasColumnName("szovegesvalasz");
            entity.Property(e => e.Testingid).HasColumnName("testingid");

            entity.HasOne(d => d.Algorithm).WithMany(p => p.KTobbis)
                .HasForeignKey(d => d.Algorithmid)
                .HasConstraintName("FK_k_tobbi_algorithm");

            entity.HasOne(d => d.Design).WithMany(p => p.KTobbis)
                .HasForeignKey(d => d.Designid)
                .HasConstraintName("FK_k_tobbi_design_1");

            entity.HasOne(d => d.Devops).WithMany(p => p.KTobbis)
                .HasForeignKey(d => d.Devopsid)
                .HasConstraintName("FK_k_tobbi_devops_2");

            entity.HasOne(d => d.Kitoltottkerdoiv).WithMany(p => p.KTobbis)
                .HasForeignKey(d => d.Kitoltottkerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_k_tobbi_kitoltottkerdoiv");

            entity.HasOne(d => d.Testing).WithMany(p => p.KTobbis)
                .HasForeignKey(d => d.Testingid)
                .HasConstraintName("FK_k_tobbi_testing_3");
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
            entity.Property(e => e.Programnyelv).HasColumnName("programnyelv");
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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Algorithm).HasColumnName("algorithm");
            entity.Property(e => e.Allasid).HasColumnName("allasid");
            entity.Property(e => e.Design).HasColumnName("design");
            entity.Property(e => e.Devops).HasColumnName("devops");
            entity.Property(e => e.Kitoltesperc).HasColumnName("kitoltesperc");
            entity.Property(e => e.Kor).HasColumnName("kor");
            entity.Property(e => e.Maxpont).HasColumnName("maxpont");
            entity.Property(e => e.Nev).HasColumnName("nev");
            entity.Property(e => e.Programming).HasColumnName("programming");
            entity.Property(e => e.Testing).HasColumnName("testing");

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
            entity.Property(e => e.Kivalasztva).HasColumnName("kivalasztva");
            entity.Property(e => e.Vegsoszazalek).HasColumnName("vegsoszazalek");

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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Befejezve).HasColumnName("befejezve");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Kitolteskezdet)
                .HasColumnType("datetime")
                .HasColumnName("kitolteskezdet");
            entity.Property(e => e.Kitoltottallasid).HasColumnName("kitoltottallasid");
            entity.Property(e => e.Miajanlas).HasColumnName("miajanlas");
            entity.Property(e => e.Miszazalek).HasColumnName("miszazalek");
            entity.Property(e => e.Szazalek).HasColumnName("szazalek");
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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Futasido).HasColumnName("futasido");
            entity.Property(e => e.Helyes).HasColumnName("helyes");
            entity.Property(e => e.Hibakimenet).HasColumnName("hibakimenet");
            entity.Property(e => e.Kimenet).HasColumnName("kimenet");
            entity.Property(e => e.Kitoltottkerdesid).HasColumnName("kitoltottkerdesid");
            entity.Property(e => e.Tesztesetid).HasColumnName("tesztesetid");
            entity.Property(e => e.Token).HasColumnName("token");

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

        modelBuilder.Entity<Programming>(entity =>
        {
            entity.ToTable("programming");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codetemplate).HasColumnName("codetemplate");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Kerdoiv).WithMany(p => p.Programmings)
                .HasForeignKey(d => d.Kerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_programming_kerdoiv");
        });

        modelBuilder.Entity<Programmingtestcase>(entity =>
        {
            entity.ToTable("programmingtestcase");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Input).HasColumnName("input");
            entity.Property(e => e.Output).HasColumnName("output");
            entity.Property(e => e.Programmingid).HasColumnName("programmingid");

            entity.HasOne(d => d.Programming).WithMany(p => p.Programmingtestcases)
                .HasForeignKey(d => d.Programmingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_programmingtestcase_programming");
        });

        modelBuilder.Entity<Testing>(entity =>
        {
            entity.ToTable("testing");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Actualresult).HasColumnName("actualresult");
            entity.Property(e => e.Additionalreq).HasColumnName("additionalreq");
            entity.Property(e => e.Appurl).HasColumnName("appurl");
            entity.Property(e => e.Browser).HasColumnName("browser");
            entity.Property(e => e.Defaultpriority).HasColumnName("defaultpriority");
            entity.Property(e => e.Defaultseverity).HasColumnName("defaultseverity");
            entity.Property(e => e.Expectedresult).HasColumnName("expectedresult");
            entity.Property(e => e.Kerdoivid).HasColumnName("kerdoivid");
            entity.Property(e => e.Os).HasColumnName("os");
            entity.Property(e => e.Requireattachments).HasColumnName("requireattachments");
            entity.Property(e => e.Resolution).HasColumnName("resolution");
            entity.Property(e => e.Stepstoreproduce).HasColumnName("stepstoreproduce");
            entity.Property(e => e.Taskdesc).HasColumnName("taskdesc");
            entity.Property(e => e.Testingtype).HasColumnName("testingtype");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Kerdoiv).WithMany(p => p.Testings)
                .HasForeignKey(d => d.Kerdoivid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testing_kerdoiv");
        });

        modelBuilder.Entity<Testingcase>(entity =>
        {
            entity.ToTable("testingcase");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Canbeautomated).HasColumnName("canbeautomated");
            entity.Property(e => e.Expectedresult).HasColumnName("expectedresult");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.Testdata).HasColumnName("testdata");
            entity.Property(e => e.Testingid).HasColumnName("testingid");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Testing).WithMany(p => p.Testingcases)
                .HasForeignKey(d => d.Testingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testingcase_testing");
        });

        modelBuilder.Entity<Testingcasestep>(entity =>
        {
            entity.ToTable("testingcasestep");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Testingcaseid).HasColumnName("testingcaseid");
            entity.Property(e => e.Teststep).HasColumnName("teststep");

            entity.HasOne(d => d.Testingcase).WithMany(p => p.Testingcasesteps)
                .HasForeignKey(d => d.Testingcaseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testingcasestep_testingcase");
        });

        modelBuilder.Entity<Testingevaluation>(entity =>
        {
            entity.ToTable("testingevaluation");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Criterion).HasColumnName("criterion");
            entity.Property(e => e.Desc).HasColumnName("desc");
            entity.Property(e => e.Testingid).HasColumnName("testingid");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Testing).WithMany(p => p.Testingevaluations)
                .HasForeignKey(d => d.Testingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testingevaluation_testing");
        });

        modelBuilder.Entity<Testingscenario>(entity =>
        {
            entity.ToTable("testingscenario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Desc).HasColumnName("desc");
            entity.Property(e => e.Prereq).HasColumnName("prereq");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Testingid).HasColumnName("testingid");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Testing).WithMany(p => p.Testingscenarios)
                .HasForeignKey(d => d.Testingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testingscenario_testing");
        });

        modelBuilder.Entity<Testingtool>(entity =>
        {
            entity.ToTable("testingtool");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.Testingid).HasColumnName("testingid");
            entity.Property(e => e.Version).HasColumnName("version");

            entity.HasOne(d => d.Testing).WithMany(p => p.Testingtools)
                .HasForeignKey(d => d.Testingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testingtool_testing");
        });

        modelBuilder.Entity<Teszteset>(entity =>
        {
            entity.ToTable("teszteset");

            entity.Property(e => e.Id).HasColumnName("id");
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

        modelBuilder.Entity<Vegzettseg>(entity =>
        {
            entity.ToTable("vegzettseg");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Felhasznaloid).HasColumnName("felhasznaloid");
            entity.Property(e => e.Hosszuleiras).HasColumnName("hosszuleiras");
            entity.Property(e => e.Rovidleiras).HasColumnName("rovidleiras");

            entity.HasOne(d => d.Felhasznalo).WithMany(p => p.Vegzettsegs)
                .HasForeignKey(d => d.Felhasznaloid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vegzettseg_felhasznalo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
