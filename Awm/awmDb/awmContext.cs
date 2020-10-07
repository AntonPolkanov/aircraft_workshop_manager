using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Awm.AwmDb
{
    public partial class awmContext : DbContext
    {
        public awmContext()
        {
        }

        public awmContext(DbContextOptions<awmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aircraft> Aircraft { get; set; }
        public virtual DbSet<AircraftImage> AircraftImage { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientNotes> ClientNotes { get; set; }
        public virtual DbSet<Flight> Flight { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceTimer> ServiceTimer { get; set; }
        public virtual DbSet<Shift> Shift { get; set; }
        public virtual DbSet<SparePart> SparePart { get; set; }
        public virtual DbSet<Timesheet> Timesheet { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=awm-db.cvpjqogof2on.ap-southeast-2.rds.amazonaws.com;port=3306;user=admin;password=Qwertyuiop;database=awm");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>(entity =>
            {
                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.Engine)
                    .HasColumnName("engine")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastServiceDate)
                    .HasColumnName("lastServiceDate")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationNumber)
                    .HasColumnName("registrationNumber")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .HasColumnName("serialNumber")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AircraftImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AircraftId)
                    .HasName("aircraftId");

                entity.Property(e => e.ImageId).HasColumnName("imageID");

                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("longtext");

                entity.Property(e => e.DateTime).HasColumnName("dateTime");

                entity.Property(e => e.S3Path)
                    .HasColumnName("s3Path")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aircraft)
                    .WithMany(p => p.AircraftImage)
                    .HasForeignKey(d => d.AircraftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AircraftImage_ibfk_1");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNmuber)
                    .HasColumnName("contactNmuber")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientNotes>(entity =>
            {
                entity.HasIndex(e => e.AircraftId)
                    .HasName("aircraftId");

                entity.HasIndex(e => e.ClientId)
                    .HasName("clientId");

                entity.Property(e => e.ClientNotesId).HasColumnName("clientNotesId");

                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.HasOne(d => d.Aircraft)
                    .WithMany(p => p.ClientNotes)
                    .HasForeignKey(d => d.AircraftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClientNotes_ibfk_2");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientNotes)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClientNotes_ibfk_1");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasIndex(e => e.AircraftId)
                    .HasName("aircraftId");

                entity.Property(e => e.FlightId).HasColumnName("flightId");

                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Hours)
                    .HasColumnName("hours")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aircraft)
                    .WithMany(p => p.Flight)
                    .HasForeignKey(d => d.AircraftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Flight_ibfk_1");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasIndex(e => e.EmailAddressId)
                    .HasName("emailAddressId");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("serviceId_idx");

                entity.HasIndex(e => e.ShiftId)
                    .HasName("shiftId_idx");

                entity.Property(e => e.JobId).HasColumnName("jobId");

                entity.Property(e => e.AllocatedHours).HasColumnName("allocatedHours");

                entity.Property(e => e.CumulativeHours).HasColumnName("cumulativeHours");

                entity.Property(e => e.EmailAddressId)
                    .IsRequired()
                    .HasColumnName("emailAddressId")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.JobDescription).HasColumnName("jobDescription");

                entity.Property(e => e.ServiceId).HasColumnName("serviceId");

                entity.Property(e => e.ShiftId).HasColumnName("shiftId");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("serviceId");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("shiftId");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasIndex(e => e.JobId)
                    .HasName("jobId");

                entity.Property(e => e.MaterialId).HasColumnName("materialId");

                entity.Property(e => e.BestBeforeDate)
                    .HasColumnName("bestBeforeDate")
                    .HasColumnType("date");

                entity.Property(e => e.Gnr)
                    .HasColumnName("gnr")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IntakeDate)
                    .HasColumnName("intakeDate")
                    .HasColumnType("date");

                entity.Property(e => e.JobId).HasColumnName("jobId");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Material_ibfk_1");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.AircraftId)
                    .HasName("aircraftId");

                entity.Property(e => e.ServiceId).HasColumnName("serviceId");

                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.ClientQuotesHrs)
                    .HasColumnName("clientQuotesHrs")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aircraft)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.AircraftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Service_ibfk_1");
            });

            modelBuilder.Entity<ServiceTimer>(entity =>
            {
                entity.HasIndex(e => e.AircraftId)
                    .HasName("aircraftId");

                entity.Property(e => e.ServiceTimerId).HasColumnName("serviceTimerId");

                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.NextServiceDate)
                    .HasColumnName("nextServiceDate")
                    .HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Aircraft)
                    .WithMany(p => p.ServiceTimer)
                    .HasForeignKey(d => d.AircraftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ServiceTimer_ibfk_1");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasIndex(e => e.EmailAddressId)
                    .HasName("emailAddressId");

                entity.Property(e => e.ShiftId).HasColumnName("shiftId");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.DurationHours).HasColumnName("durationHours");

                entity.Property(e => e.EmailAddressId)
                    .IsRequired()
                    .HasColumnName("emailAddressId")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.StartTime).HasColumnName("startTime");

                entity.HasOne(d => d.EmailAddress)
                    .WithMany(p => p.Shift)
                    .HasForeignKey(d => d.EmailAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Shift_ibfk_1");
            });

            modelBuilder.Entity<SparePart>(entity =>
            {
                entity.HasKey(e => e.PartId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.JobId)
                    .HasName("jobId");

                entity.Property(e => e.PartId).HasColumnName("partId");

                entity.Property(e => e.BestBeforeDate)
                    .HasColumnName("bestBeforeDate")
                    .HasColumnType("date");

                entity.Property(e => e.Gnr)
                    .HasColumnName("gnr")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IntakeDate)
                    .HasColumnName("intakeDate")
                    .HasColumnType("date");

                entity.Property(e => e.JobId).HasColumnName("jobId");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.SparePart)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SparePart_ibfk_1");
            });

            modelBuilder.Entity<Timesheet>(entity =>
            {
                entity.HasIndex(e => e.AircraftId)
                    .HasName("aircraftId");

                entity.HasIndex(e => e.EmailAddressId)
                    .HasName("emailAddressId_idx");

                entity.HasIndex(e => e.JobId)
                    .HasName("jobId");

                entity.HasIndex(e => e.ShiftId)
                    .HasName("shiftId");

                entity.Property(e => e.TimesheetId).HasColumnName("timesheetId");

                entity.Property(e => e.ActualHours)
                    .HasColumnName("actualHours")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.EmailAddressId)
                    .HasColumnName("emailAddressId")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.JobId).HasColumnName("jobId");

                entity.Property(e => e.ShiftId).HasColumnName("shiftId");

                entity.Property(e => e.StartTime).HasColumnName("startTime");

                entity.HasOne(d => d.Aircraft)
                    .WithMany(p => p.Timesheet)
                    .HasForeignKey(d => d.AircraftId)
                    .HasConstraintName("Timesheet_ibfk_1");

                entity.HasOne(d => d.EmailAddress)
                    .WithMany(p => p.Timesheet)
                    .HasForeignKey(d => d.EmailAddressId)
                    .HasConstraintName("emailAddressId");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Timesheet)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("Timesheet_ibfk_2");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Timesheet)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("Timesheet_ibfk_3");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.EmailAddressId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AircraftId)
                    .HasName("aircraftId_idx");

                entity.HasIndex(e => e.ClientId)
                    .HasName("clientId_idx");

                entity.Property(e => e.EmailAddressId)
                    .HasColumnName("emailAddressId")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aircraft)
                    .WithMany(p => p.UserAccount)
                    .HasForeignKey(d => d.AircraftId)
                    .HasConstraintName("aircraftId");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.UserAccount)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("clientId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
