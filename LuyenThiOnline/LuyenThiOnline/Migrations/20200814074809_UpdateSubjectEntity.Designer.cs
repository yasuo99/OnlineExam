﻿// <auto-generated />
using System;
using LuyenThiOnline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LuyenThiOnline.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200814074809_UpdateSubjectEntity")]
    partial class UpdateSubjectEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LuyenThiOnline.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("Exp")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime2");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.AccountBadge", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("BadgeId")
                        .HasColumnType("int");

                    b.HasKey("AccountId", "BadgeId");

                    b.HasIndex("BadgeId");

                    b.ToTable("AccountBadges");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.AccountCertificate", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("CertificateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AccountId", "CertificateId");

                    b.HasIndex("CertificateId");

                    b.ToTable("AccountCertificates");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Badge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("BadgeLogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Badgename")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Certificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CertificateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Enrolled", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPassed")
                        .HasColumnType("bit");

                    b.HasKey("SubjectId", "AccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("Enrolled");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DoneDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EndExp")
                        .HasColumnType("int");

                    b.Property<string>("LevelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StartExp")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("A")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("B")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionPoint")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Rank", b =>
                {
                    b.Property<int>("RankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RankName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RankId");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EnrolledCount")
                        .HasColumnType("int");

                    b.Property<int>("ExpGain")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassScore")
                        .HasColumnType("int");

                    b.Property<int>("RankId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RankId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Account", b =>
                {
                    b.HasOne("LuyenThiOnline.Models.Level", "Level")
                        .WithMany("Accounts")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuyenThiOnline.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.AccountBadge", b =>
                {
                    b.HasOne("LuyenThiOnline.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuyenThiOnline.Models.Badge", "Badge")
                        .WithMany("Accounts")
                        .HasForeignKey("BadgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LuyenThiOnline.Models.AccountCertificate", b =>
                {
                    b.HasOne("LuyenThiOnline.Models.Account", "Account")
                        .WithMany("Certificates")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuyenThiOnline.Models.Certificate", "Certificate")
                        .WithMany("Accounts")
                        .HasForeignKey("CertificateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Badge", b =>
                {
                    b.HasOne("LuyenThiOnline.Models.Account", null)
                        .WithMany("Badges")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Enrolled", b =>
                {
                    b.HasOne("LuyenThiOnline.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuyenThiOnline.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LuyenThiOnline.Models.History", b =>
                {
                    b.HasOne("LuyenThiOnline.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuyenThiOnline.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Question", b =>
                {
                    b.HasOne("LuyenThiOnline.Models.Subject", "Subject")
                        .WithMany("Questions")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LuyenThiOnline.Models.Subject", b =>
                {
                    b.HasOne("LuyenThiOnline.Models.Rank", "Rank")
                        .WithMany("Subjects")
                        .HasForeignKey("RankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
