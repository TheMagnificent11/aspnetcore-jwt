using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Jwt.Sample.Migrations
{
    /// <summary>
    /// Tenants Migration
    /// </summary>
    public partial class Tenants : Migration
    {
        /// <summary>
        /// Upgrades database
        /// </summary>
        /// <param name="migrationBuilder">Migration builder</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenantId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RoleTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantUserRole_TenantRole_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "TenantRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantUserRole_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantUserRole_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserRole_RoleTypeId",
                table: "TenantUserRole",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserRole_TenantId",
                table: "TenantUserRole",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserRole_UserId",
                table: "TenantUserRole",
                column: "UserId");
        }

        /// <summary>
        /// Downgrades database
        /// </summary>
        /// <param name="migrationBuilder">Migration builder</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantUserRole");

            migrationBuilder.DropTable(
                name: "TenantRole");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
