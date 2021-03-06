// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_commerce.Data;

namespace e_commerce.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210709153812_AddedSomeRelations")]
    partial class AddedSomeRelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("e_commerce.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("e_commerce.Models.CategoryChild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryChilds");
                });

            modelBuilder.Entity("e_commerce.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColorCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("e_commerce.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("e_commerce.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryChildId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("SpecsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryChildId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SpecsId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("e_commerce.Models.ProductColorImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductColorImages");
                });

            modelBuilder.Entity("e_commerce.Models.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("e_commerce.Models.Specs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OSForView")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OSValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProducerForView")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProducerValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductionYearForView")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductionYearValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeForView")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specs");
                });

            modelBuilder.Entity("e_commerce.Models.CategoryChild", b =>
                {
                    b.HasOne("e_commerce.Models.Category", "Category")
                        .WithMany("CategoryChild")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("e_commerce.Models.Image", b =>
                {
                    b.HasOne("e_commerce.Models.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("e_commerce.Models.Product", b =>
                {
                    b.HasOne("e_commerce.Models.CategoryChild", "CategoryChild")
                        .WithMany("Products")
                        .HasForeignKey("CategoryChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce.Models.Specs", "Specs")
                        .WithMany("Products")
                        .HasForeignKey("SpecsId");

                    b.Navigation("Category");

                    b.Navigation("CategoryChild");

                    b.Navigation("Specs");
                });

            modelBuilder.Entity("e_commerce.Models.ProductColorImage", b =>
                {
                    b.HasOne("e_commerce.Models.Color", "Color")
                        .WithMany("ProductColorImages")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce.Models.Image", "Image")
                        .WithMany("ProductColorImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce.Models.Product", "Product")
                        .WithMany("ProductColorImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("e_commerce.Models.Category", b =>
                {
                    b.Navigation("CategoryChild");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("e_commerce.Models.CategoryChild", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("e_commerce.Models.Color", b =>
                {
                    b.Navigation("ProductColorImages");
                });

            modelBuilder.Entity("e_commerce.Models.Image", b =>
                {
                    b.Navigation("ProductColorImages");
                });

            modelBuilder.Entity("e_commerce.Models.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ProductColorImages");
                });

            modelBuilder.Entity("e_commerce.Models.Specs", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
