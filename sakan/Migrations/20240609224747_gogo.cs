using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sakan.Migrations
{
    /// <inheritdoc />
    public partial class gogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    passwared = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminHouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Photo2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Photo3 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Photo4 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NumOfRooms = table.Column<int>(type: "int", nullable: false),
                    NumOfBeds = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    phone = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseOwnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminHouses_HouseOwners_HouseOwnerID",
                        column: x => x.HouseOwnerID,
                        principalTable: "HouseOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Photo2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Photo3 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Photo4 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NumOfRooms = table.Column<int>(type: "int", nullable: false),
                    NumOfBeds = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    phone = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseOwnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_HouseOwners_HouseOwnerID",
                        column: x => x.HouseOwnerID,
                        principalTable: "HouseOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    HouseNum = table.Column<int>(type: "int", nullable: true),
                    HouseID = table.Column<int>(type: "int", nullable: true),
                    AdminHouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AdminHouses_AdminHouseId",
                        column: x => x.AdminHouseId,
                        principalTable: "AdminHouses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Students_Houses_HouseID",
                        column: x => x.HouseID,
                        principalTable: "Houses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminHouses_HouseOwnerID",
                table: "AdminHouses",
                column: "HouseOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_HouseOwners_Email",
                table: "HouseOwners",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Houses_HouseOwnerID",
                table: "Houses",
                column: "HouseOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AdminHouseId",
                table: "Students",
                column: "AdminHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_HouseID",
                table: "Students",
                column: "HouseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "AdminHouses");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "HouseOwners");
        }
    }
}
