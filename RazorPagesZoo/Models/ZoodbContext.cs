using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace RazorPagesZoo.Models;

public partial class ZoodbContext : IdentityDbContext
{
    public ZoodbContext()
    {
    }

    public ZoodbContext(DbContextOptions<ZoodbContext> options)
        : base(options)
    {
    }

    [Required]
    public virtual DbSet<Animal> Animals { get; set; }
    public virtual DbSet<Cage> Cages { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<JournalOfTask> JournalOfTasks { get; set; }
    public virtual DbSet<SpeciesNote> SpeciesNotes { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }
    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;Database=zoodb;Username=postgres;Password=student;Persist Security Info=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.IdAnimal).HasName("animal_pkey");

            entity.ToTable("animal", "zoo_keepers");

            entity.HasIndex(e => e.Name, "animal_name_key").IsUnique();

            entity.Property(e => e.IdAnimal)
                .ValueGeneratedNever()
                .HasColumnName("id_animal");
            entity.Property(e => e.Dob).HasColumnName("dob");

            entity.Property(e => e.Features).HasColumnName("features");

            entity.Property(e => e.IdSpecies).HasColumnName("id_species");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.Property(entity => entity.IdCage).HasColumnName("id_cage");

            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .HasColumnName("sex");

            entity.HasOne(d => d.IdCageNavigation).WithMany(p => p.IdAnimals)
                .HasForeignKey(d => d.IdCage)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("animal_cagefk");

            entity.HasOne(d => d.IdSpeciesNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.IdSpecies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("animal_speciesfk");
        });

        modelBuilder.Entity<Cage>(entity =>
        {
            entity.HasKey(e => e.IdCage).HasName("cage_pkey");

            entity.ToTable("cage", "zoo_keepers");

            entity.Property(e => e.IdCage)
                .ValueGeneratedNever()
                .HasColumnName("id_cage");
            entity.Property(e => e.CageSize).HasColumnName("cage_size");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Temp).HasColumnName("temp");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasMany(c => c.IdAnimals) // У клетки много животных
                .WithOne(a => a.IdCageNavigation) // У животного одна клетка
                .HasForeignKey(a => a.IdCage) // Внешний ключ в Animal
                .OnDelete(DeleteBehavior.SetNull) // При удалении клетки CageId становится null
                .HasConstraintName("fk_animal_cage");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("employee_pkey");

            entity.ToTable("employee", "zoo_keepers");

            entity.Property(e => e.IdEmployee)
                .UseSerialColumn()
                .HasColumnName("id_employee");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.WorkEfficiency).HasColumnName("work_efficiency");


            entity.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(e => e.IdentityUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<JournalOfTask>(entity =>
        {
            entity.HasKey(e => new { e.IdEmployee, e.IdTask }).HasName("journalpk");

            entity.ToTable("journal_of_tasks", "zoo_keepers");

            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.IdentityUserID).HasColumnName("IdentityUserID");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.JournalOfTasks)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("journal_idemployeefk");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.JournalOfTasks)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("journal_idtaskfk");


        });

        modelBuilder.Entity<SpeciesNote>(entity =>
        {
            entity.HasKey(e => e.IdSpecies).HasName("species_note_pkey");

            entity.ToTable("species_note", "zoo_keepers");

            entity.HasIndex(e => e.Name, "species_note_name_key").IsUnique();

            entity.Property(e => e.IdSpecies)
                .ValueGeneratedNever()
                .HasColumnName("id_species");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.SpeciesGroup)
                .HasMaxLength(20)
                .HasColumnName("species_group");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("task_pkey");

            entity.ToTable("task", "zoo_keepers");

            entity.Property(e => e.IdTask)
                .ValueGeneratedNever()
                .HasColumnName("id_task");
            entity.Property(e => e.DateDrop).HasColumnName("date_drop");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdAnimal).HasColumnName("id_animal");
            entity.Property(e => e.IdCage).HasColumnName("id_cage");

            entity.HasOne(d => d.IdAnimalNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("task_idanimalfk");

            entity.HasOne(d => d.IdCageNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdCage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("task_idcagefk");

        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.IdVaccination).HasName("vaccination_pkey");

            entity.ToTable("vaccination", "zoo_keepers");

            entity.Property(e => e.IdVaccination)
                .ValueGeneratedNever()
                .HasColumnName("id_vaccination");
            entity.Property(e => e.DateLastVaccination).HasColumnName("date_last_vaccination");
            entity.Property(e => e.DateNextVaccination).HasColumnName("date_next_vaccination");
            entity.Property(e => e.IdAnimal).HasColumnName("id_animal");
            entity.Property(e => e.IdBatchLastVaccination).HasColumnName("id_batch_last_vaccination");
            entity.Property(e => e.NameVaccination)
                .HasMaxLength(50)
                .HasColumnName("name_vaccination");

            entity.HasOne(d => d.IdAnimalNavigation).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.IdAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vaccination_idanimalfk");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
    