using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgetMeNot.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDueDateToUTC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Tasks",
                newName: "DueDateUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDateUtc",
                table: "Tasks",
                newName: "DueDate");
        }
    }
}
