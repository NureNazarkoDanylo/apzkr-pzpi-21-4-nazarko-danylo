using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashingMachineManagementApi.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Add_DeviceGroup_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_washing_machines_DeviceGroup_DeviceGroupId",
                schema: "domain",
                table: "washing_machines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceGroup",
                schema: "domain",
                table: "DeviceGroup");

            migrationBuilder.RenameTable(
                name: "DeviceGroup",
                schema: "domain",
                newName: "device_groups",
                newSchema: "domain");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "domain",
                table: "device_groups",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_device_groups",
                schema: "domain",
                table: "device_groups",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_washing_machines_device_groups_DeviceGroupId",
                schema: "domain",
                table: "washing_machines",
                column: "DeviceGroupId",
                principalSchema: "domain",
                principalTable: "device_groups",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_washing_machines_device_groups_DeviceGroupId",
                schema: "domain",
                table: "washing_machines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_device_groups",
                schema: "domain",
                table: "device_groups");

            migrationBuilder.RenameTable(
                name: "device_groups",
                schema: "domain",
                newName: "DeviceGroup",
                newSchema: "domain");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "domain",
                table: "DeviceGroup",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceGroup",
                schema: "domain",
                table: "DeviceGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_washing_machines_DeviceGroup_DeviceGroupId",
                schema: "domain",
                table: "washing_machines",
                column: "DeviceGroupId",
                principalSchema: "domain",
                principalTable: "DeviceGroup",
                principalColumn: "Id");
        }
    }
}
