using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatGptBot.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _2snTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "History",
                table: "ChatHistories",
                newName: "Prompt");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "ChatHistories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "ChatHistories");

            migrationBuilder.RenameColumn(
                name: "Prompt",
                table: "ChatHistories",
                newName: "History");
        }
    }
}
