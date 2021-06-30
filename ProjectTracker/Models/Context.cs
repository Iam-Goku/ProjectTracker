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
        public virtual DbSet<Projects> Project { get; set; }
        public virtual DbSet<Customers> Customer { get; set; }
        public virtual DbSet<Products> Product { get; set; }
        public virtual DbSet<Typeclass> Types { get; set; }
         public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Attachments> Attachment { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employees>().HasKey(e => new { e.EmployeeID });

            modelBuilder.Entity<Projects>().HasKey(e => new { e.ProjectID });

            modelBuilder.Entity<Customers>().HasKey(e => new { e.CustomerID });

            modelBuilder.Entity<Products>().HasKey(e => new { e.ProductID });

            modelBuilder.Entity<Typeclass>().HasKey(e => new { e.TypeID });

            modelBuilder.Entity<Note>().HasKey(e => new { e.NoteID });

            modelBuilder.Entity<Attachments>().HasKey(e => new { e.AttachmentID });




            //    modelBuilder.Entity<Notes>().HasKey(e => new { e.NoteID });

            //modelBuilder.Entity<Project>()
            //          .HasOne(mg => mg.customers)
            //          .WithMany(m => m.projects)
            //          .HasForeignKey(g => g.CustomerID);

            // modelBuilder.Entity<Customer>()
            //.HasMany(a => a.projects)
            //.WithOne(b => b.customers);




            //modelBuilder.Entity<Project>().HasKey(mg => new { mg.CustomerID });



        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
