using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FileManagementAPI.Service.Models
{
    public partial class eTMSDataContextDefault : DbContext
    {
        public eTMSDataContextDefault()
        {
        }

        public eTMSDataContextDefault(DbContextOptions<eTMSDataContextDefault> options)
            : base(options)
        {
        }

        public virtual DbSet<CsDocument> CsDocument { get; set; }
        //public virtual DbSet<OpsDtbtransportRequestOrderItemRoute> OpsDtbtransportRequestOrderItemRoute { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-TSK64MU;Database=ITL-FileManagementAPI;User ID=DESKTOP-TSK64MU\\Asus;Password=;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<CsDocument>(entity =>
            {
                entity.ToTable("csDocument");

                entity.HasIndex(e => new { e.BranchId, e.ReferenceObject, e.DocType, e.FileName })
                    .HasName("U_Document")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.DatetimeCreated).HasColumnType("smalldatetime");

                entity.Property(e => e.DatetimeModified).HasColumnType("smalldatetime");

                entity.Property(e => e.DocType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileCheckSum).HasMaxLength(100);

                entity.Property(e => e.FileData);

                entity.Property(e => e.FileDescription).HasMaxLength(500);

                entity.Property(e => e.FileName).HasMaxLength(150);

                entity.Property(e => e.Icon).HasColumnType("image");

                entity.Property(e => e.InactiveOn).HasColumnType("smalldatetime");

                entity.Property(e => e.ReferenceObject).HasMaxLength(100);

                entity.Property(e => e.UserCreated)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserModified)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

        //    modelBuilder.Entity<OpsDtbtransportRequestOrderItemRoute>(entity =>
        //    {
        //        entity.ToTable("opsDTBTransportRequestOrderItemRoute", "dtb");

        //        entity.Property(e => e.Id)
        //            .HasColumnName("ID")
        //            .ValueGeneratedNever();

        //        entity.Property(e => e.CheckedCoddateTime)
        //            .HasColumnName("CheckedCODDateTime")
        //            .HasColumnType("datetime");

        //        entity.Property(e => e.CheckedCodnote)
        //            .HasColumnName("CheckedCODNote")
        //            .HasMaxLength(500);

        //        entity.Property(e => e.CheckedCoduser)
        //            .HasColumnName("CheckedCODUser")
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CheckingInRemark).HasMaxLength(500);

        //        entity.Property(e => e.Codvalue)
        //            .HasColumnName("CODValue")
        //            .HasColumnType("decimal(18, 4)");

        //        entity.Property(e => e.Collection).HasColumnType("decimal(18, 4)");

        //        entity.Property(e => e.CustomerSignature).HasColumnType("image");

        //        entity.Property(e => e.DatetimeCreated).HasColumnType("smalldatetime");

        //        entity.Property(e => e.DatetimeModified).HasColumnType("smalldatetime");

        //        entity.Property(e => e.DeliveredActualVolume).HasColumnType("decimal(18, 8)");

        //        entity.Property(e => e.DeliveredActualWeight).HasColumnType("decimal(18, 8)");

        //        entity.Property(e => e.DeliveryAllowance).HasColumnType("decimal(18, 4)");

        //        entity.Property(e => e.DeliveryArrivedTime).HasColumnType("smalldatetime");

        //        entity.Property(e => e.DeliveryLeftTime).HasColumnType("smalldatetime");

        //        entity.Property(e => e.DeliveryStatus)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.DropDownSurcharge).HasColumnType("decimal(18, 4)");

        //        entity.Property(e => e.FailedDeliveryDueTo).HasMaxLength(50);

        //        entity.Property(e => e.FailedDeliveryReason).HasMaxLength(250);

        //        entity.Property(e => e.IndicatedCodvalue)
        //            .HasColumnName("IndicatedCODValue")
        //            .HasColumnType("decimal(18, 4)");

        //        entity.Property(e => e.Note).HasMaxLength(500);

        //        entity.Property(e => e.OrderDropPointItemRouteId).HasColumnName("OrderDropPointItemRouteID");

        //        entity.Property(e => e.PickedUpActualVolume).HasColumnType("decimal(18, 8)");

        //        entity.Property(e => e.PickedUpActualWeight).HasColumnType("decimal(18, 8)");

        //        entity.Property(e => e.PickupArrivedTime).HasColumnType("smalldatetime");

        //        entity.Property(e => e.PickupLeftTime).HasColumnType("smalldatetime");

        //        entity.Property(e => e.PodreceivedDate)
        //            .HasColumnName("PODReceivedDate")
        //            .HasColumnType("smalldatetime");

        //        entity.Property(e => e.PodreturnedDate)
        //            .HasColumnName("PODReturnedDate")
        //            .HasColumnType("smalldatetime");

        //        entity.Property(e => e.Rating).HasColumnType("decimal(5, 3)");

        //        entity.Property(e => e.ReturnedCodvalue)
        //            .HasColumnName("ReturnedCODValue")
        //            .HasColumnType("decimal(18, 4)");

        //        entity.Property(e => e.ShipmentStatusId).HasColumnName("ShipmentStatusID");

        //        entity.Property(e => e.TransportRequestId).HasColumnName("TransportRequestID");

        //        entity.Property(e => e.UserCreated)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.UserModified)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.Volume).HasColumnType("decimal(18, 8)");

        //        entity.Property(e => e.Weight).HasColumnType("decimal(18, 8)");
        //    });
        }
    }
}
