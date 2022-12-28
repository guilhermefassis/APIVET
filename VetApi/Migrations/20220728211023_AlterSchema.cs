using Microsoft.EntityFrameworkCore.Migrations;

namespace VetApi.Migrations
{
    public partial class AlterSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Animals",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adminIDAPI00213554856Pqwus",
                column: "ConcurrencyStamp",
                value: "3704a4d8-6ad3-466a-b0c0-ac4aa06e1712");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "regular123aosdm123bJASNd",
                column: "ConcurrencyStamp",
                value: "8cc1aaed-f6f7-4532-917d-55005c13ebbb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "vet125sasD31asdADA516as2da6A",
                column: "ConcurrencyStamp",
                value: "1000117d-ca0f-4005-9a75-8bf086b2772c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminIDAPI00213554856Pqwus",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b0c2356-183c-4582-907b-475b6adb61a8", "AQAAAAEAACcQAAAAEKM/nYwOSbSXwtk8LwNeITVB1l3XDNHywsW+hlUIGaIg4EsiVNpiIavnUj7p7d5FlA==", "2b4dc08a-f260-4a2e-a664-f409e37ea526" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Animals");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adminIDAPI00213554856Pqwus",
                column: "ConcurrencyStamp",
                value: "4715c61d-53d7-49b7-bc83-33015a560a5f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "regular123aosdm123bJASNd",
                column: "ConcurrencyStamp",
                value: "7f8543c8-46f7-4c65-ab06-2b24ee9c66ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "vet125sasD31asdADA516as2da6A",
                column: "ConcurrencyStamp",
                value: "a5a23698-2cb4-4a95-97d0-2a5b899f8f7f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminIDAPI00213554856Pqwus",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d44cc8bf-a9f0-4049-a8f5-40738245121b", "AQAAAAEAACcQAAAAEABs8Ji4NHtc5o1UoaAwHn4S+5lskykr9LsjtEdVBZPsTTPSURPKrjLxvI39b+yKbA==", "384e9c26-0217-409b-8a89-85fdcb8dd575" });
        }
    }
}
