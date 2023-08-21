

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Task.Models

{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>


    {
      ////public   ApplicationDbContext()
      ////  { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   // optionsBuilder.UseSqlServer("Data Source=.;ProjectModels;Initial Catalog=ToDoList;Integrated Security=True; ");
        //    base.OnConfiguring(optionsBuilder);
        //}


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Todo>().Property(e=>e.Completed).HasDefaultValue(false);
            base.OnModelCreating(builder);
        }
        

        // mapping in database 
        public DbSet<Todo> Todos { get; set; }

    }
}
