using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Invoicer.Models
{
    public partial class invoicerContext : DbContext
    {
        public invoicerContext()
        {
        }

        public invoicerContext(DbContextOptions<invoicerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Invoicelineitems> Invoicelineitems { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Services> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Username=rania;Password=j$l*yDAaYYxv!wjRCve26Bnkz2;Host=invoicer.cu7ykist3otz.us-west-2.rds.amazonaws.com;Port=5432;Database=invoicer");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.ClientId)
                    .HasName("clients_pk");

                entity.ToTable("clients");

                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ContactAgent).HasMaxLength(250);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Invoicelineitems>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                    .HasName("invoicelineitems_pk");

                entity.ToTable("invoicelineitems");

                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.Property(e => e.Rate).HasColumnType("money");

                entity.Property(e => e.ServiceCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxAmount).HasColumnType("money");

                entity.Property(e => e.TotalInvoice).HasColumnType("money");

                entity.HasOne(d => d.ServiceCodeNavigation)
                    .WithMany(p => p.Invoicelineitems)
                    .HasForeignKey(d => d.ServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoicelineitems_fk");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                    .HasName("invoices_pk");

                entity.ToTable("invoices");

                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.Property(e => e.InvoiceAmount).HasColumnType("money");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceDueDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.Property(e => e.TotalCredit).HasColumnType("money");

                entity.Property(e => e.TotalPayment).HasColumnType("money");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoices_fk");
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.HasKey(e => e.ServiceCode)
                    .HasName("services_pk");

                entity.ToTable("services");

                entity.Property(e => e.ServiceCode).HasMaxLength(50);

                entity.Property(e => e.ServiceDescription).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
