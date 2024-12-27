﻿// <auto-generated />
using System;
using Backend.Api.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241227192225_nullableUpdates")]
    partial class nullableUpdates
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.Api.DAL.Entities.AppUserEntity", b =>
                {
                    b.Property<Guid>("AppUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("app_user_id");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<string>("Subject")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("subject");

                    b.HasKey("AppUserId")
                        .HasName("pk_users");

                    b.HasIndex("Subject")
                        .HasDatabaseName("ix_users_subject");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Backend.Api.DAL.Entities.MeasurementsEntity", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<decimal>("HeightInCm")
                        .HasColumnType("numeric")
                        .HasColumnName("height_in_cm");

                    b.Property<decimal>("LengthInCm")
                        .HasColumnType("numeric")
                        .HasColumnName("length_in_cm");

                    b.Property<decimal>("WeightInKg")
                        .HasColumnType("numeric")
                        .HasColumnName("weight_in_kg");

                    b.Property<decimal>("WidthInCm")
                        .HasColumnType("numeric")
                        .HasColumnName("width_in_cm");

                    b.HasKey("ProductId")
                        .HasName("pk_measurements");

                    b.ToTable("Measurements", (string)null);
                });

            modelBuilder.Entity("Backend.Api.DAL.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("EAN")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("ean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_on");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("Backend.Api.DAL.Entities.MeasurementsEntity", b =>
                {
                    b.HasOne("Backend.Api.DAL.Entities.ProductEntity", "ProductEntity")
                        .WithOne("Measurements")
                        .HasForeignKey("Backend.Api.DAL.Entities.MeasurementsEntity", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_measurements_products_product_id");

                    b.Navigation("ProductEntity");
                });

            modelBuilder.Entity("Backend.Api.DAL.Entities.ProductEntity", b =>
                {
                    b.Navigation("Measurements")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
