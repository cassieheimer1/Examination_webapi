using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using WebApi_Examinationen.Models;

#nullable disable

namespace WebApi_Examinationen.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20220413111604_products")]
    public partial class productsDesigner : Migration
    {
        public override IModel TargetModel => base.TargetModel;

        public override IReadOnlyList<MigrationOperation> UpOperations => base.UpOperations;

        public override IReadOnlyList<MigrationOperation> DownOperations => base.DownOperations;

        public override string ActiveProvider { get => base.ActiveProvider; set => base.ActiveProvider = value; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApi.Models.Product.ProductCategoryEntity", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("ProductCategories");
            });

            modelBuilder.Entity("WebApi.Models.Product.ProductEntity", b =>
            {
                b.Property<string>("ArticleNr")
                    .HasColumnType("nvarchar(450)");

                b.Property<int>("CategoryId")
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<decimal>("Price")
                    .HasColumnType("money");

                b.HasKey("ArticleNr");

                b.HasIndex("CategoryId");

                b.ToTable("Products");
            });

            modelBuilder.Entity("WebApi.Models.Product.ProductEntity", b =>
            {
                b.HasOne("WebApi.Models.Product.ProductCategoryEntity", "Category")
                    .WithMany("Products")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Category");
            });

            modelBuilder.Entity("WebApi.Models.Product.ProductCategoryEntity", b =>
            {
                b.Navigation("Products");
            });
#pragma warning restore 612, 618
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            base.Down(migrationBuilder);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
