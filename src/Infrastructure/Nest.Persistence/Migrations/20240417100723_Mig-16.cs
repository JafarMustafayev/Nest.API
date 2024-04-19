using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLike",
                table: "LikeDislike",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLike",
                table: "LikeDislike");
        }
    }
}