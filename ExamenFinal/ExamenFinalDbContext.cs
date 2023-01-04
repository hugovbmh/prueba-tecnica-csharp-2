using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamenFinal
{
    public partial class ExamenFinalDbContext : DbContext
    {
        public ExamenFinalDbContext()
        {
        }

        public ExamenFinalDbContext(DbContextOptions<ExamenFinalDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClDepartamento> ClDepartamentos { get; set; } = null!;
        public virtual DbSet<ClEmpleado> ClEmpleados { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<ClDepartamento>(entity =>
            {
                entity.HasKey(e => e.IdDpto)
                    .HasName("PK__CL_DEPAR__55928FED8FB1A5CA");

                entity.Property(e => e.IdDpto).ValueGeneratedNever();
            });

            modelBuilder.Entity<ClEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__CL_EMPLE__922CA26974ADD907");

                entity.Property(e => e.IdEmpleado).ValueGeneratedNever();

                entity.HasOne(d => d.IdDptoNavigation)
                    .WithMany(p => p.ClEmpleados)
                    .HasForeignKey(d => d.IdDpto)
                    .HasConstraintName("FK_departamento");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
