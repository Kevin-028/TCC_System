﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TCC_System_Data;

namespace TCC_System_Data.Migrations
{
    [DbContext(typeof(TCC_Context))]
    partial class TCC_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TCC_System_Domain.Arduino.MessageAction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnName("Action")
                        .HasColumnType("VARCHAR(15)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RecordCreatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("RecordCreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("RecordUpdateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("RecordUpdatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("Type")
                        .HasColumnType("VARCHAR(20)");

                    b.HasKey("Id");

                    b.ToTable("MessageAction");
                });

            modelBuilder.Entity("TCC_System_Domain.Arduino.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RecordCreatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("RecordCreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("RecordUpdateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("RecordUpdatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("Type")
                        .HasColumnType("VARCHAR(20)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Ardu_Modulo");
                });

            modelBuilder.Entity("TCC_System_Domain.Arduino.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("RecordCreatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("RecordCreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("RecordUpdateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("RecordUpdatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("TCC_System_Domain.Blog.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordCreatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("RecordCreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("RecordUpdateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("RecordUpdatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<string>("Title")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Blog_Post");
                });

            modelBuilder.Entity("TCC_System_Domain.Management.Claims", b =>
                {
                    b.Property<int>("ClaimID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomeClaim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordCreatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("RecordCreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("RecordUpdateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("RecordUpdatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("ClaimID");

                    b.ToTable("AUTH_Claims");
                });

            modelBuilder.Entity("TCC_System_Domain.Management.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordCreatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("RecordCreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("RecordUpdateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("RecordUpdatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("Id");

                    b.ToTable("AUTH_Users");
                });

            modelBuilder.Entity("TCC_System_Domain.Management.UserClaims", b =>
                {
                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.Property<int>("ClaimID")
                        .HasColumnType("int");

                    b.Property<string>("RecordCreatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.Property<DateTime?>("RecordCreationDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("RecordUpdateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("RecordUpdatedBy")
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("UsuarioID", "ClaimID");

                    b.HasIndex("ClaimID");

                    b.ToTable("AUTH_UserClaims");
                });

            modelBuilder.Entity("TCC_System_Domain.Arduino.Module", b =>
                {
                    b.HasOne("TCC_System_Domain.Arduino.Product", "Product")
                        .WithMany("ProductModeles")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TCC_System_Domain.Management.UserClaims", b =>
                {
                    b.HasOne("TCC_System_Domain.Management.Claims", "Claims")
                        .WithMany("ClaimUsers")
                        .HasForeignKey("ClaimID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TCC_System_Domain.Management.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
