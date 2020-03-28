using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using HomeHealth.data.tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HomeHealth.Identity;


namespace HomeHealth.Data
{
    public partial class HomeHealthDbContext : IdentityDbContext<ApplicationUser>

    {
        public HomeHealthDbContext()
        {
        }

        public HomeHealthDbContext(DbContextOptions<HomeHealthDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointments> Appointment { get; set; }
        public virtual DbSet<Charges> Charge { get; set; }
        public virtual DbSet<Professionals> Professional { get; set; }
        public virtual DbSet<Messages> Message { get; set; }
        public virtual DbSet<Professional_Service> Professional_Service { get; set; }
        public virtual DbSet<HomeHealth.data.tables.Services> Service { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.ToTable("appointment");

                entity.HasKey( e => e.AppointmentId);

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.AppDate)
                    .HasColumnName("app_date")
                    .HasColumnType("date");

                entity.Property(e => e.AppReason)
                    .HasColumnName("app_reason")
                    .IsUnicode(false);

                entity.Property(e => e.AppTime)
                    .HasColumnName("app_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProfessionalId).HasColumnName("doctor_id");

                entity.Property(e => e.totalcost).HasColumnName("Total_cost");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Professional)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ProfessionalId)
                    .HasConstraintName("fk_User_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("fk_User_id");
            });

            modelBuilder.Entity<Charges>(entity =>
            {
                entity.ToTable("bill");

                entity.HasKey(e => e.ChargeId);

                entity.Property(e => e.Prof_serviceId).HasColumnName("service_id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.serviceCost).HasColumnName("Service_Cost");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Charges)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("fk_appointment_id");

                entity.HasOne(d => d.Professional_Service)
                    .WithMany(p => p.Charges)
                    .HasForeignKey(d => d.Prof_serviceId)
                    .HasConstraintName("fk_Prof_Service_id");
            });

            modelBuilder.Entity<Professionals>(entity =>
            {
                entity.HasKey(e => e.ProfessionalsId)
                    .HasName("pk_Duser_id");

                entity.ToTable("ProfessionalsId");

                entity.Property(e => e.userId).HasColumnName("user_id");


                entity.Property(e => e.DoctorsAddress1)
                    .HasColumnName("doctors_Address1")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DoctorsAddress2)
                    .HasColumnName("doctors_Address2")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

          

            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.ToTable("message1");

                entity.HasKey(e => e.Message1Id).HasName("message1_id");;

                entity.Property(e => e.Message1Id).HasColumnName("message1_id");

                entity.Property(e => e.SenderId).HasColumnName("doctor_id");

                entity.Property(e => e.RecieverId).HasColumnName("patient_id");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("fk_doctor_id1");

                entity.HasOne(d => d.Reciever)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.RecieverId)
                    .HasConstraintName("fk_patient_id1");
            });

             modelBuilder.Entity<Professional_Service>(entity =>
            {

                entity.ToTable("Prof_service_id");

                entity.HasKey(e => e.Professional_ServiceId);

                entity.Property(e => e.ServiceCost).HasColumnName("Service_cost");

                entity.Property(e => e.ProfessionalId).HasColumnName("ProfessionalId");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.prof_services)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("fk_doctor_id1");

                entity.HasOne(d => d.Professional)
                    .WithMany(p => p.Prof_services)
                    .HasForeignKey(d => d.ProfessionalId)
                    .HasConstraintName("fk_doctor_id1");

            });




            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
