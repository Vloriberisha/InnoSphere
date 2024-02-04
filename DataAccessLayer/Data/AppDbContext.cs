using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employer>()
            .HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<Employer>(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobSeeker>()
            .HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<JobSeeker>(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

           

            //1 user ka shume aplikime
            modelBuilder.Entity<JobSeeker>()
                .HasMany(u => u.JobApplications)
                .WithOne(ja => ja.JobSeeker)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 user qe eshte company ka shume postime
            modelBuilder.Entity<Employer>()
                .HasMany(u => u.JobsPosted)
                .WithOne(j => j.Company)
                .OnDelete(DeleteBehavior.Cascade);


            //Shume me shume per job application
            modelBuilder.Entity<JobApplication>()
                .HasKey(up => new { up.Id,up.JobSeekerId, up.JobId});
            modelBuilder.Entity<JobApplication>()
                .HasOne(u => u.JobSeeker)
                .WithMany(p => p.JobApplications)
                .HasForeignKey(u => u.JobSeekerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<JobApplication>()
                .HasOne(u => u.Job)
                .WithMany(p => p.Applications)
                .HasForeignKey(u => u.JobId)
                .OnDelete(DeleteBehavior.Cascade);






            // Ensure cascading delete for Identity-related entities
            modelBuilder.Entity<User>()
                .HasMany(u => u.Claims)
                .WithOne()
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
