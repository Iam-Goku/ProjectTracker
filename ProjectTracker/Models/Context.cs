using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public partial class Context:DbContext
    {
        public Context(DbContextOptions<Context>options)
            :base(options)
        {
        }
        public virtual DbSet<Employees> Employee { get; set; }
        public virtual DbSet<Positions> Position { get; set; }

        public virtual DbSet<Projects> Project { get; set; }
        public virtual DbSet<Customers> Customer { get; set; }
        public virtual DbSet<Products> Product { get; set; }
        public virtual DbSet<Typeclass> Types { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Attachments> Attachment { get; set; }

        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }

        public virtual DbSet<DevelopmentStatus> DevelopmentStatus { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employees>().HasKey(e => new { e.EmployeeID });

            modelBuilder.Entity<Positions>().HasKey(e => new { e.PositionID });


            modelBuilder.Entity<Projects>().HasKey(e => new { e.ProjectID });

            modelBuilder.Entity<Customers>().HasKey(e => new { e.CustomerID });

            modelBuilder.Entity<Products>().HasKey(e => new { e.ProductID });

            modelBuilder.Entity<Typeclass>().HasKey(e => new { e.TypeID });

            modelBuilder.Entity<Note>().HasKey(e => new { e.NoteID });

            modelBuilder.Entity<Attachments>().HasKey(e => new { e.AttachmentID });

            modelBuilder.Entity<ProjectStatus>().HasKey(e => new { e.ProjectStatusID });

            modelBuilder.Entity<DevelopmentStatus>().HasKey(e => new { e.DevelopmentStatusID });





            // modelBuilder.Entity<Projects>()
            //.HasRequired(f => f.customers)
            //.WithMany()
            //.HasForeignKey(f => f.AwayTeamId)
            //.WillCascadeOnDelete(false);

            // modelBuilder.Entity<Fixture>()
            //     .HasRequired(f => f.HomeTeam)
            //     .WithMany()
            //     .HasForeignKey(f => f.HomeTeamId)
            //     .WillCascadeOnDelete(false);

            // modelBuilder.Entity<Fixture>()
            //     .HasRequired(f => f.AwayCoach)
            //     .WithMany()
            //     .HasForeignKey(f => f.AwayCoachId)
            //     .WillCascadeOnDelete(false);

            // modelBuilder.Entity<Fixture>()
            //     .HasRequired(f => f.HomeCoach)
            //     .WithMany()
            //     .HasForeignKey(f => f.HomeCoachId)
            //     .WillCascadeOnDelete(false);



        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
