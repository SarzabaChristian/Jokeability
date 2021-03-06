﻿// <auto-generated />
using System;
using Jokeability.Backend.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jokeability.Backend.DataContext.Migrations
{
    [DbContext(typeof(JokeabilityDBContext))]
    partial class JokeabilityDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Jokeability.Backend.DataContext.Models.Joke", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("JokerID");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.HasIndex("JokerID");

                    b.ToTable("tblJokes","dbo");
                });

            modelBuilder.Entity("Jokeability.Backend.DataContext.Models.JokeStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JokeID");

                    b.Property<int>("JokerID");

                    b.Property<int>("ReactionID");

                    b.Property<int>("UserID");

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.HasIndex("JokeID");

                    b.ToTable("tblJokeStats","dbo");
                });

            modelBuilder.Entity("Jokeability.Backend.DataContext.Models.MasterSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Group");

                    b.Property<string>("Value");

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.ToTable("tblMasterSetting","GS");
                });

            modelBuilder.Entity("Jokeability.Backend.DataContext.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.Property<bool>("isWithBadge");

                    b.HasKey("Id");

                    b.ToTable("tblUsers","dbo");
                });

            modelBuilder.Entity("Jokeability.Backend.DataContext.Models.Joke", b =>
                {
                    b.HasOne("Jokeability.Backend.DataContext.Models.User", "Joker")
                        .WithMany()
                        .HasForeignKey("JokerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jokeability.Backend.DataContext.Models.JokeStats", b =>
                {
                    b.HasOne("Jokeability.Backend.DataContext.Models.Joke", "Joke")
                        .WithMany()
                        .HasForeignKey("JokeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
