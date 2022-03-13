﻿// <auto-generated />
using Home_Assignment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Home_Assignment.Migrations
{
    [DbContext(typeof(MvcStudentContext))]
    partial class MvcStudentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Home_Assignment.Models.Student", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("age")
                        .HasColumnType("REAL");

                    b.Property<string>("first_name")
                        .HasColumnType("TEXT");

                    b.Property<double>("gpa")
                        .HasColumnType("REAL");

                    b.Property<string>("last_name")
                        .HasColumnType("TEXT");

                    b.Property<string>("name_of_school")
                        .HasColumnType("TEXT");

                    b.Property<string>("school_address")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
