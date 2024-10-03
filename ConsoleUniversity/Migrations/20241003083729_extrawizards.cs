using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    /// <inheritdoc />
    public partial class extrawizards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprenticeID",
                table: "Instructors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeathCount",
                table: "Instructors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MagicLore",
                table: "Instructors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_ApprenticeID",
                table: "Instructors",
                column: "ApprenticeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Student_ApprenticeID",
                table: "Instructors",
                column: "ApprenticeID",
                principalTable: "Student",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Student_ApprenticeID",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_ApprenticeID",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "ApprenticeID",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "DeathCount",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "MagicLore",
                table: "Instructors");
        }
    }
}
