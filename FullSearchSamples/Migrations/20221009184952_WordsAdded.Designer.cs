﻿// <auto-generated />
using FullSearchSamples;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FullSearchSamples.Migrations
{
    [DbContext(typeof(DocumentDbContext))]
    [Migration("20221009184952_WordsAdded")]
    partial class WordsAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FullSearchSamples.Entity.Document", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("FullSearchSamples.Entity.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("Words");
                });

            modelBuilder.Entity("FullSearchSamples.Entity.WordDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("WordId");

                    b.HasIndex("WordId", "DocumentId")
                        .IsUnique();

                    b.ToTable("WordDocuments");
                });

            modelBuilder.Entity("FullSearchSamples.Entity.WordDocument", b =>
                {
                    b.HasOne("FullSearchSamples.Entity.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FullSearchSamples.Entity.Word", "Word")
                        .WithMany()
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Word");
                });
#pragma warning restore 612, 618
        }
    }
}
