﻿// <auto-generated />
using AzureToolkit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AzureToolkit.Migrations
{
    [DbContext(typeof(AzureToolkitContext))]
    partial class AzureToolkitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AzureToolkit.Models.SavedImage", b =>
                {
                    b.Property<int>("SavedImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("StorageUrl");

                    b.Property<string>("UserId");

                    b.HasKey("SavedImageId");

                    b.ToTable("SavedImages");
                });

            modelBuilder.Entity("AzureToolkit.Models.SavedImageTag", b =>
                {
                    b.Property<int>("SavedImageTagId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SavedImageId");

                    b.Property<string>("Tag");

                    b.HasKey("SavedImageTagId");

                    b.HasIndex("SavedImageId");

                    b.ToTable("SavedImageTags");
                });

            modelBuilder.Entity("AzureToolkit.Models.SavedImageTag", b =>
                {
                    b.HasOne("AzureToolkit.Models.SavedImage")
                        .WithMany("Tags")
                        .HasForeignKey("SavedImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
