﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aspnetserver.Data;

#nullable disable

namespace aspnetserver.Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("aspnetserver.Data.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(100000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("PostId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Content = "This is post no. 1. You are currently seeing its content.",
                            Title = "Post 1"
                        },
                        new
                        {
                            PostId = 2,
                            Content = "This is post no. 2. You are currently seeing its content.",
                            Title = "Post 2"
                        },
                        new
                        {
                            PostId = 3,
                            Content = "This is post no. 3. You are currently seeing its content.",
                            Title = "Post 3"
                        },
                        new
                        {
                            PostId = 4,
                            Content = "This is post no. 4. You are currently seeing its content.",
                            Title = "Post 4"
                        },
                        new
                        {
                            PostId = 5,
                            Content = "This is post no. 5. You are currently seeing its content.",
                            Title = "Post 5"
                        },
                        new
                        {
                            PostId = 6,
                            Content = "This is post no. 6. You are currently seeing its content.",
                            Title = "Post 6"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
