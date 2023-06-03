using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentApp.Data.Migrations
{
    public partial class tablefiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StudentAssignmentAssignmentId = table.Column<int>(type: "int", nullable: false),
                    StudentAssignmentStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_Files_StudentAssignments_StudentAssignmentAssignmentId_StudentAssignmentStudentId",
                        columns: x => new { x.StudentAssignmentAssignmentId, x.StudentAssignmentStudentId },
                        principalTable: "StudentAssignments",
                        principalColumns: new[] { "AssignmentId", "StudentId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_StudentAssignmentAssignmentId_StudentAssignmentStudentId",
                table: "Files",
                columns: new[] { "StudentAssignmentAssignmentId", "StudentAssignmentStudentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
