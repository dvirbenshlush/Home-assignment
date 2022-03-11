using Microsoft.EntityFrameworkCore.Migrations;

namespace Home_Assignment.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: true),
                    age = table.Column<double>(type: "REAL", nullable: false),
                    gpa = table.Column<double>(type: "REAL", nullable: false),
                    school_of_name = table.Column<string>(type: "TEXT", nullable: true),
                    school_address = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.first_name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
