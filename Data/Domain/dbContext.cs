
using HOSPITALMANAGEMENT.Model;
using HOSPITALMANAGEMENT.Model.DbModels;
using Microsoft.EntityFrameworkCore;

namespace HOSPITALMANAGEMENT.Data.Domain
{
    public class dbContext : DbContext
    {

        public dbContext(DbContextOptions option) : base(option)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Doctor> Doctor { get; set; }

        public DbSet<Doctor_Patient_Disease> Doctor_Patient_Disease { get; set; }

        public DbSet<BookedAppointment> BookedAppointments { get; set; }

        public DbSet<ProductModel> ProductsTable { get; set; }

        public DbSet<CartModel> cartTable { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Doctor_Patient_Disease>()
         .HasKey(dpd => new { dpd.DoctorID, dpd.PatientID, dpd.DiseaseID });

            base.OnModelCreating(builder);

            builder.Entity<User>()
              .HasKey(u => new
              {
                  u.UserId
              });

            builder.Entity<Event>()
              .HasKey(e => new
              {
                  e.Id
              });

            builder.Entity<UserEvent>()
              .HasKey(ue => new
              {
                  ue.UserId,
                  ue.EventId
              });

            builder.Entity<UserEvent>()
              .HasOne(ue => ue.User)
              .WithMany(user => user.UserEvents)
              .HasForeignKey(u => u.UserId);

            builder.Entity<UserEvent>()
              .HasOne(uc => uc.Event)
              .WithMany(ev => ev.UserEvents)
              .HasForeignKey(ev => ev.EventId);

            builder.Entity<ProductModel>()
       .Property(p => p.Name)
       .HasMaxLength(255); // Adjust the length as needed
        }
    }
}
    