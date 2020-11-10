using Microsoft.EntityFrameworkCore;
using HomeHealth.Web.Data.Tables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HomeHealth.Web.Identity;


namespace HomeHealth.Web.Data
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
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Professionals> Professional { get; set; }
        public virtual DbSet<Messages> Message { get; set; }
        public virtual DbSet<Professional_Service> Professional_Service { get; set; }
        public virtual DbSet<Service> Service { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comments>(entity => {

                entity.ToTable("Comments");

                entity.HasKey( e => e.CommentsId);

                entity.Property( e => e.Content);

                entity.Property( e => e.SenderId);

                entity.Property(e => e.ProfessionalId);

                entity.Property( e => e.TimeStamp);


                entity.HasOne( d => d.Sender)
                    .WithMany( P => P.UsersComments)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne( d => d.Professional)
                    .WithMany( p => p.ProfComments)
                    .HasForeignKey( d => d.ProfessionalId);


            });

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.ToTable("appointment");

                entity.HasKey( e => e.AppointmentId);

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.AppDate)
                    .HasColumnName("app_date");
                    // .HasColumnType("date");

                entity.Property(e => e.AddressString)
                    .HasColumnName("Prof_Address1")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.lat)
                    .HasColumnName("lat");

                entity.Property(e => e.lng)      
                    .HasColumnName("lng");

                entity.Property(e => e.ishomevisit) 
                    .HasColumnName("isHomeVisist");

                entity.Property(e => e.AppReason)
                    .HasColumnName("app_reason")
                    .IsUnicode(false);

                entity.Property(e => e.AppTime)
                    .HasColumnName("app_time");
                    // .HasColumnType("datetime");


                entity.Property(e => e.ProfessionalId).HasColumnName("doctor_id");

                entity.Property(e => e.totalcost).HasColumnName("Total_cost");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Professional)
                    .WithMany(p => p.Appointmentsprof)
                    .HasForeignKey(d => d.ProfessionalId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("fk_User_Profesional_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.AppointmentsPati)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("fk_User_patient_id");

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
                    .HasConstraintName("fk_Charges_prof_service_id");
            });

            modelBuilder.Entity<Professionals>(entity =>
            {
                entity.HasKey(e => e.ProfessionalsId)
                    .HasName("pk_Duser_id");

                entity.ToTable("ProfessionalsId");

                entity.Property(e => e.userId).HasColumnName("user_id");

                entity.Property(e => e.Biography)
                    .HasColumnName("Biography")
                    .HasMaxLength(1000);


                entity.Property(e => e.AddressString)
                    .HasColumnName("Prof_Address1")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.lat)
                    .HasColumnName("lat");

                entity.Property(e => e.lng)
                
                    .HasColumnName("lng");
                    

                 entity.HasOne(d => d.user)
                    .WithOne(p => p.Professional)
                    .HasForeignKey<Professionals>(d => d.userId)
                    .HasConstraintName("fk_Prof_User");


            });

            

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.ToTable("message");

                entity.HasKey(e => e.MessageId).HasName("message_id");

                entity.Property( e => e.TimeStamp).HasColumnName("TimeStamp");

                entity.Property(e => e.MessageId).HasColumnName("message_id");
                entity.Property(e => e.Content).HasColumnName("Content");

                entity.Property(e => e.SenderId).HasColumnName("SenderId");

                entity.Property(e => e.ReceiverId).HasColumnName("ReceiverId");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessagesSent)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("fk_Messages_SenderUser");

                entity.HasOne(d => d.Reciever)
                    .WithMany(p => p.MessagesRec)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("fk_messages_recieverUser");
            });

            modelBuilder.Entity<Professional_Service>(entity =>
            {
                

                entity.ToTable("Professional_Service");

                entity.HasKey(e => e.Professional_ServiceId).HasName("Prof_service_id");

                entity.Property(e => e.ServiceCost).HasColumnName("Service_cost");

                entity.Property(e => e.ProfessionalId).HasColumnName("ProfessionalId");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.prof_services)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("fk_prof_service__service");

                entity.HasOne(d => d.Professional)
                    .WithMany(p => p.Prof_services)
                    .HasForeignKey(d => d.ProfessionalId)
                    .HasConstraintName("k_prof_service_Professional");

            });

            modelBuilder.Entity<HomeHealth.Web.Data.Tables.Service>(entity =>
            {

                entity.ToTable("Service");

                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceName).HasColumnName("Service_name");

            });

            




            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
