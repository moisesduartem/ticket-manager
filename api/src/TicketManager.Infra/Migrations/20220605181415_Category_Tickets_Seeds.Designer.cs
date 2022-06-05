﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketManager.Infra.Database;

#nullable disable

namespace TicketManager.Infra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220605181415_Category_Tickets_Seeds")]
    partial class Category_Tickets_Seeds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TicketManager.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9092),
                            Name = "Notebook"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9094),
                            Name = "Network"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9094),
                            Name = "PC"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9095),
                            Name = "Printer"
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9096),
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("TicketManager.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TicketId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TicketManager.Domain.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSolved")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            CategoryId = 1,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9108),
                            Description = "I don't what happened, but it started to flame from nothing",
                            IsSolved = false,
                            Title = "My notebook is on fire!"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            CategoryId = 2,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9110),
                            Description = "",
                            IsSolved = false,
                            Title = "I'm without internet"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            CategoryId = 4,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9111),
                            Description = "",
                            IsSolved = false,
                            Title = "My printer isn't working"
                        });
                });

            modelBuilder.Entity("TicketManager.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(8958),
                            Email = "johnl@email.com",
                            Hash = "$2a$12$dNMivl8uiVUbJOanAbvggOThv0Psr6oaUEAf7dtgTYFB3x.PCfmr.",
                            Name = "John L.",
                            Role = 1,
                            Salt = "1d8cf748c6156545d92237c8ef115f25"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(8968),
                            Email = "georgeh@email.com",
                            Hash = "$2a$12$mc3HQ1zD.C8AAr/Lf2I29.XB.MJz6n5FLEXUOllaXPt0PdgveXU7C",
                            Name = "George H.",
                            Role = 0,
                            Salt = "ccea7ca67997fc7437b1c19a482143a7"
                        });
                });

            modelBuilder.Entity("TicketManager.Domain.Entities.Comment", b =>
                {
                    b.HasOne("TicketManager.Domain.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketManager.Domain.Entities.Ticket", "Ticket")
                        .WithMany("Comments")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("TicketManager.Domain.Entities.Ticket", b =>
                {
                    b.HasOne("TicketManager.Domain.Entities.User", "Author")
                        .WithMany("Tickets")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketManager.Domain.Entities.Category", "Category")
                        .WithMany("Tickets")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("TicketManager.Domain.Entities.Category", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketManager.Domain.Entities.Ticket", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("TicketManager.Domain.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
