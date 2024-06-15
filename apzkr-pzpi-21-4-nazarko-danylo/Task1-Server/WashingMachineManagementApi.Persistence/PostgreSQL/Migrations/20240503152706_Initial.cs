using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashingMachineManagementApi.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "domain");

            migrationBuilder.CreateTable(
                name: "DeviceGroup",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "washing_machines",
                schema: "domain",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DeviceGroupId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_washing_machines", x => x.id);
                    table.ForeignKey(
                        name: "FK_washing_machines_DeviceGroup_DeviceGroupId",
                        column: x => x.DeviceGroupId,
                        principalSchema: "domain",
                        principalTable: "DeviceGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_washing_machines_DeviceGroupId",
                schema: "domain",
                table: "washing_machines",
                column: "DeviceGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "washing_machines",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "DeviceGroup",
                schema: "domain");
        }
    }
}
