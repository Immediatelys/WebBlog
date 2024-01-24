using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using WebBlog.Models;

#nullable disable

namespace WebBlog.Migrations
{
    /// <inheritdoc />
    public partial class articledata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "ntext", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            Randomizer.Seed = new Random(8675309);

            var fakerArticle = new Faker<ArticleModel>();
            fakerArticle.RuleFor(a => a.Title, fk => $"Topic: " + fk.Lorem.Sentence(2, 5));
            fakerArticle.RuleFor(a => a.Email, fk => fk.Lorem.Sentence(1, 0) + $"@gmail.com");
            fakerArticle.RuleFor(a => a.Content, fk => fk.Lorem.Sentences(3) + "[fakeData]");

            for (int i = 0; i < 100; i++)
            {
                ArticleModel article = fakerArticle.Generate();

                migrationBuilder.InsertData(
                       table: "Articles",
                       columns: new[] { "Title", "Email", "Content" },
                       values: new object[]
                       {
                           article.Title,
                           article.Email,
                           article.Content
                       }
                    );
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
