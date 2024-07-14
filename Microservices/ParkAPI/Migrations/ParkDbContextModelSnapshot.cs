﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkAPI.Data;

#nullable disable

namespace ParkAPI.Migrations
{
    [DbContext(typeof(ParkDbContext))]
    partial class ParkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ParkAPI.Model.Park", b =>
                {
                    b.Property<Guid>("ParkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("AvailableHours")
                        .HasColumnType("float");

                    b.Property<DateTime>("ExpectedReturnTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Flag")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerHour")
                        .HasColumnType("float");

                    b.Property<string>("VehicleDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("VehicleImage")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParkId");

                    b.ToTable("ParkTable");
                });
#pragma warning restore 612, 618
        }
    }
}
