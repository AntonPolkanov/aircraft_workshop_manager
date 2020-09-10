using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Awm.awmDb
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
        public virtual DbSet<Part> Part { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceTimer> ServiceTimer { get; set; }
        public virtual DbSet<Shift> Shift { get; set; }
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
                entity.ToTable("aircraft");

                entity.Property(e => e.AircraftId).HasColumnName("aircraftId");

                entity.Property(e => e.Engine)
                    .HasColumnName("engine")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastServiceDate).HasColumnName("lastServiceDate");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationNumber)
                    .HasColumnName("registrationNumber")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .HasColumnName("serialNumber")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AircraftImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PRIMARY");

                entity.ToTable("aircraftImage");

                entity.Property(e => e.ImageId).HasColumnName("imageID");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("longtext");

                entity.Property(e => e.DateTime).HasColumnName("dateTime");

                entity.Property(e => e.S3Path)
                    .HasColumnName("s3Path")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNmuber)
                    .HasColumnName("contactNmuber")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientNotes>(entity =>
            {
                entity.HasKey(e => e.IdclientNotesId)
                    .HasName("PRIMARY");

                entity.ToTable("clientNotes");

                entity.Property(e => e.IdclientNotesId).HasColumnName("idclientNotesId");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasColumnName("contactNumber")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.Idflight)
                    .HasName("PRIMARY");

                entity.ToTable("flight");

                entity.Property(e => e.Idflight).HasColumnName("idflight");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Hours)
                    .HasColumnName("hours")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("job");

                entity.Property(e => e.JobId).HasColumnName("jobId");

                entity.Property(e => e.ActualTimeTakenHrs).HasColumnName("actualTimeTakenHrs");

                entity.Property(e => e.Description)
                    .HasColumnName("description ")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeTakenhrs).HasColumnName("timeTakenhrs");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("material");

                entity.Property(e => e.MaterialId).HasColumnName("materialId");

                entity.Property(e => e.BestBeforeDate)
                    .HasColumnName("bestBeforeDate")
                    .HasColumnType("date");

                entity.Property(e => e.Gnr)
                    .HasColumnName("gnr")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IntakeDate)
                    .HasColumnName("intakeDate")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.ToTable("part");

                entity.Property(e => e.PartId).HasColumnName("partId");

                entity.Property(e => e.BestBeforeDate)
                    .HasColumnName("bestBeforeDate")
                    .HasColumnType("date");

                entity.Property(e => e.Gnr)
                    .HasColumnName("gnr")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IntakeDate)
                    .HasColumnName("intakeDate")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Idservice)
                    .HasName("PRIMARY");

                entity.ToTable("service");

                entity.Property(e => e.Idservice).HasColumnName("idservice");

                entity.Property(e => e.ClientQuotesHrs)
                    .HasColumnName("clientQuotesHrs")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ServiceTimer>(entity =>
            {
                entity.ToTable("serviceTimer");

                entity.Property(e => e.ServiceTimerId).HasColumnName("serviceTimerId");

                entity.Property(e => e.NextServiceDate)
                    .HasColumnName("nextServiceDate")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("shift");

                entity.Property(e => e.ShiftId).HasColumnName("shiftId");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.DurationHours).HasColumnName("durationHours");

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.StartTime).HasColumnName("startTime");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.EmailAddressId)
                    .HasName("PRIMARY");

                entity.ToTable("userAccount");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
