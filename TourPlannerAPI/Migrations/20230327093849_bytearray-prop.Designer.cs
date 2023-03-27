﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TourPlannerAPI.Data;

#nullable disable

namespace TourPlannerAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230327093849_bytearray-prop")]
    partial class bytearrayprop
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TourPlanner.Models.Tour", b =>
                {
                    b.Property<int?>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("TourId"));

                    b.Property<int>("TourInfoId")
                        .HasColumnType("integer");

                    b.Property<string>("TourName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TourId");

                    b.HasIndex("TourInfoId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TourPlanner.Models.TourInfo", b =>
                {
                    b.Property<int?>("TourInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("TourInfoId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<float?>("Distance")
                        .HasColumnType("real");

                    b.Property<float?>("EstimatedTime")
                        .HasColumnType("real");

                    b.Property<string>("From")
                        .HasColumnType("text");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("bytea");

                    b.Property<string>("To")
                        .HasColumnType("text");

                    b.Property<string>("TransportType")
                        .HasColumnType("text");

                    b.HasKey("TourInfoId");

                    b.ToTable("TourInfo");
                });

            modelBuilder.Entity("TourPlanner.Models.TourLog", b =>
                {
                    b.Property<int>("TourLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TourLogId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Distance")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<int>("TourId")
                        .HasColumnType("integer");

                    b.HasKey("TourLogId");

                    b.HasIndex("TourId");

                    b.ToTable("TourLog");
                });

            modelBuilder.Entity("TourPlanner.Models.Tour", b =>
                {
                    b.HasOne("TourPlanner.Models.TourInfo", "TourInfo")
                        .WithMany()
                        .HasForeignKey("TourInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TourInfo");
                });

            modelBuilder.Entity("TourPlanner.Models.TourLog", b =>
                {
                    b.HasOne("TourPlanner.Models.Tour", null)
                        .WithMany("TourLogs")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TourPlanner.Models.Tour", b =>
                {
                    b.Navigation("TourLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
