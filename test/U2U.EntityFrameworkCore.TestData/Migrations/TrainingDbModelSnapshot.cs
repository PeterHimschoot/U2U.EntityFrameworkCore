﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using U2U.EntityFrameworkCore.TestData;

namespace U2U.EntityFrameworkCore.TestData.Migrations
{
    [DbContext(typeof(TrainingDb))]
    partial class TrainingDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("U2U.EntityFrameworkCore.TestData.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "UARCH",
                            Name = "Patterns and Practices"
                        },
                        new
                        {
                            Id = 2,
                            Code = "UWEBA",
                            Name = "Advanced Web Development"
                        },
                        new
                        {
                            Id = 3,
                            Code = "UCORE",
                            Name = "Upgrade to DotNet Core"
                        },
                        new
                        {
                            Id = 4,
                            Code = "UDEF",
                            Name = "Domain Driven Design With Entity Framework Core"
                        });
                });

            modelBuilder.Entity("U2U.EntityFrameworkCore.TestData.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Provider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Logins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Provider = "X",
                            StudentId = 1
                        },
                        new
                        {
                            Id = 2,
                            Provider = "X",
                            StudentId = 2
                        },
                        new
                        {
                            Id = 3,
                            Provider = "X",
                            StudentId = 3
                        });
                });

            modelBuilder.Entity("U2U.EntityFrameworkCore.TestData.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Sessions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 1,
                            EndDate = new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CourseId = 2,
                            EndDate = new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CourseId = 3,
                            EndDate = new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CourseId = 1,
                            EndDate = new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CourseId = 2,
                            EndDate = new DateTime(2020, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CourseId = 3,
                            EndDate = new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            CourseId = 1,
                            EndDate = new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            CourseId = 2,
                            EndDate = new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            CourseId = 3,
                            EndDate = new DateTime(2020, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("U2U.EntityFrameworkCore.TestData.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Joske",
                            LastName = "Vermeulen"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Eddy",
                            LastName = "Wally"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Sam",
                            LastName = "Goris"
                        });
                });

            modelBuilder.Entity("U2U.EntityFrameworkCore.TestData.StudentSession", b =>
                {
                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("SessionId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentSession");

                    b.HasData(
                        new
                        {
                            SessionId = 1,
                            StudentId = 1
                        },
                        new
                        {
                            SessionId = 1,
                            StudentId = 3
                        },
                        new
                        {
                            SessionId = 2,
                            StudentId = 2
                        },
                        new
                        {
                            SessionId = 3,
                            StudentId = 1
                        },
                        new
                        {
                            SessionId = 3,
                            StudentId = 2
                        },
                        new
                        {
                            SessionId = 3,
                            StudentId = 3
                        });
                });

            modelBuilder.Entity("U2U.EntityFrameworkCore.TestData.Login", b =>
                {
                    b.HasOne("U2U.EntityFrameworkCore.TestData.Student", "Student")
                        .WithOne("Login")
                        .HasForeignKey("U2U.EntityFrameworkCore.TestData.Login", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("U2U.EntityFrameworkCore.TestData.Session", b =>
                {
                    b.HasOne("U2U.EntityFrameworkCore.TestData.Course", "Course")
                        .WithMany("Sessions")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("U2U.EntityFrameworkCore.TestData.StudentSession", b =>
                {
                    b.HasOne("U2U.EntityFrameworkCore.TestData.Session", "Session")
                        .WithMany("Students")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("U2U.EntityFrameworkCore.TestData.Student", "Student")
                        .WithMany("Sessions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
