﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using amorphie.tag.data;

#nullable disable

namespace amorphie.tag.data.Migrations
{
    [DbContext(typeof(TagDBContext))]
    partial class TagDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Domains");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b5b2d526-be36-4841-8a6a-76021afb57b6"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9486),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Domain 1 Description",
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9489),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Domain 1"
                        },
                        new
                        {
                            Id = new Guid("b5c39b67-5365-485e-9d59-3267379f42cd"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9494),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Domain 2 Description",
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9494),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Domain 2"
                        });
                });

            modelBuilder.Entity("Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("DomainId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Entities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e937c536-290b-4621-a261-23adf038abf7"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9735),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Entity 1 Description",
                            DomainId = new Guid("b5b2d526-be36-4841-8a6a-76021afb57b6"),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9736),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Entity 1"
                        },
                        new
                        {
                            Id = new Guid("b3101953-8e57-452f-9b74-41ff0cfe9b37"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9743),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Entity 2 Description",
                            DomainId = new Guid("b5c39b67-5365-485e-9d59-3267379f42cd"),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9744),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Entity 2"
                        });
                });

            modelBuilder.Entity("EntityData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<int?>("Ttl")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("EntityData");

                    b.HasData(
                        new
                        {
                            Id = new Guid("92dd2eb7-3f8f-4d38-90de-5c0be6d8ece4"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9770),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            EntityId = new Guid("e937c536-290b-4621-a261-23adf038abf7"),
                            Field = "Field 1",
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9770),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Ttl = 10
                        },
                        new
                        {
                            Id = new Guid("360e370c-4039-405e-988b-ce4cc618dc28"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9774),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            EntityId = new Guid("b3101953-8e57-452f-9b74-41ff0cfe9b37"),
                            Field = "Field 2",
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9775),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Ttl = 20
                        });
                });

            modelBuilder.Entity("EntityDataSource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("DataPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EntityDataId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EntityDataId");

                    b.HasIndex("TagId");

                    b.ToTable("EntityDataSource");

                    b.HasData(
                        new
                        {
                            Id = new Guid("229aaa10-0f5d-45bf-b825-22ba1e2d7e8d"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9799),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            DataPath = "Path 1",
                            EntityDataId = new Guid("92dd2eb7-3f8f-4d38-90de-5c0be6d8ece4"),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9799),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Order = 1
                        },
                        new
                        {
                            Id = new Guid("e54642a7-ada4-476c-907b-34e22c578aa5"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9803),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            DataPath = "Path 2",
                            EntityDataId = new Guid("360e370c-4039-405e-988b-ce4cc618dc28"),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9803),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Order = 2
                        });
                });

            modelBuilder.Entity("Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Ttl")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("758b0f5b-0cb7-44ec-b369-c83a8e6af47b"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9819),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            CreatedDate = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9821),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9819),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Tag 1",
                            Ttl = 30,
                            Url = "URL 1"
                        },
                        new
                        {
                            Id = new Guid("a9c2dd61-bca2-4d33-ab7e-7fce5511c4e2"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9827),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            CreatedDate = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9832),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9827),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Tag 2",
                            Ttl = 40,
                            Url = "URL 2"
                        });
                });

            modelBuilder.Entity("TagRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.ToTable("TagRelations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a7882f10-6658-45d6-81f2-c707805f44eb"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9845),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9845),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            OwnerName = "Owner 1",
                            TagId = new Guid("758b0f5b-0cb7-44ec-b369-c83a8e6af47b"),
                            TagName = ""
                        },
                        new
                        {
                            Id = new Guid("c984a74f-d411-469b-9cce-bf520b07f88e"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9849),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9849),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            OwnerName = "Owner 2",
                            TagId = new Guid("a9c2dd61-bca2-4d33-ab7e-7fce5511c4e2"),
                            TagName = ""
                        });
                });

            modelBuilder.Entity("View", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("ViewTemplateName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.ToTable("Views");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2ac7e3a8-3b88-49e2-8e5f-a670ae8ea31f"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9862),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9862),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            TagId = new Guid("758b0f5b-0cb7-44ec-b369-c83a8e6af47b"),
                            TagName = "",
                            Type = 0,
                            ViewTemplateName = "View 1"
                        },
                        new
                        {
                            Id = new Guid("dcabb69b-16c8-42c1-9d93-6671b0e3cb10"),
                            CreatedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9865),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            ModifiedAt = new DateTime(2023, 6, 1, 7, 35, 32, 144, DateTimeKind.Utc).AddTicks(9866),
                            ModifiedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            TagId = new Guid("a9c2dd61-bca2-4d33-ab7e-7fce5511c4e2"),
                            TagName = "",
                            Type = 5,
                            ViewTemplateName = "View 2"
                        });
                });

            modelBuilder.Entity("Entity", b =>
                {
                    b.HasOne("Domain", "Domain")
                        .WithMany("Entities")
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("EntityData", b =>
                {
                    b.HasOne("Entity", "Entity")
                        .WithMany("Data")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("EntityDataSource", b =>
                {
                    b.HasOne("EntityData", "EntityData")
                        .WithMany("Sources")
                        .HasForeignKey("EntityDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");

                    b.Navigation("EntityData");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("TagRelation", b =>
                {
                    b.HasOne("Tag", "Tag")
                        .WithMany("TagsRelations")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("View", b =>
                {
                    b.HasOne("Tag", "Tag")
                        .WithMany("Views")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Domain", b =>
                {
                    b.Navigation("Entities");
                });

            modelBuilder.Entity("Entity", b =>
                {
                    b.Navigation("Data");
                });

            modelBuilder.Entity("EntityData", b =>
                {
                    b.Navigation("Sources");
                });

            modelBuilder.Entity("Tag", b =>
                {
                    b.Navigation("TagsRelations");

                    b.Navigation("Views");
                });
#pragma warning restore 612, 618
        }
    }
}
