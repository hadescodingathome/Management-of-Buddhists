using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_Infrastructure.Migrations
{
    public partial class intital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chua",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chua", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KieuThanhVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenKieu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KieuThanhVien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhatTu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenDem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhapDanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhChup = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayXuatGia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaHoanTuc = table.Column<bool>(type: "bit", nullable: false),
                    NgayHoanTuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    KieuThanhVienId = table.Column<int>(type: "int", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChuaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatTu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhatTu_Chua_ChuaId",
                        column: x => x.ChuaId,
                        principalTable: "Chua",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhatTu_KieuThanhVien_KieuThanhVienId",
                        column: x => x.KieuThanhVienId,
                        principalTable: "KieuThanhVien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DaoTrang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiToChuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoThanhVienThamGia = table.Column<int>(type: "int", nullable: false),
                    NguoiChuTriId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianToChuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaKetThuc = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaoTrang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaoTrang_PhatTu_NguoiChuTriId",
                        column: x => x.NguoiChuTriId,
                        principalTable: "PhatTu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonDangKy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false),
                    TrangThaiDon = table.Column<int>(type: "int", nullable: false),
                    NgayGuiDon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayXuLy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiXuLyId = table.Column<int>(type: "int", nullable: true),
                    DaoTrangId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonDangKy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonDangKy_DaoTrang_DaoTrangId",
                        column: x => x.DaoTrangId,
                        principalTable: "DaoTrang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonDangKy_PhatTu_Id",
                        column: x => x.Id,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonDangKy_PhatTu_NguoiXuLyId",
                        column: x => x.NguoiXuLyId,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhatTuDaoTrang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuId = table.Column<int>(type: "int", nullable: true),
                    DaoTrangId = table.Column<int>(type: "int", nullable: false),
                    DaThamGia = table.Column<bool>(type: "bit", nullable: false),
                    LyDoKhongThamGia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatTuDaoTrang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhatTuDaoTrang_DaoTrang_DaoTrangId",
                        column: x => x.DaoTrangId,
                        principalTable: "DaoTrang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhatTuDaoTrang_PhatTu_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DaoTrang_NguoiChuTriId",
                table: "DaoTrang",
                column: "NguoiChuTriId");

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_DaoTrangId",
                table: "DonDangKy",
                column: "DaoTrangId");

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_NguoiXuLyId",
                table: "DonDangKy",
                column: "NguoiXuLyId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_ChuaId",
                table: "PhatTu",
                column: "ChuaId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_KieuThanhVienId",
                table: "PhatTu",
                column: "KieuThanhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTuDaoTrang_DaoTrangId",
                table: "PhatTuDaoTrang",
                column: "DaoTrangId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTuDaoTrang_PhatTuId",
                table: "PhatTuDaoTrang",
                column: "PhatTuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DonDangKy");

            migrationBuilder.DropTable(
                name: "PhatTuDaoTrang");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DaoTrang");

            migrationBuilder.DropTable(
                name: "PhatTu");

            migrationBuilder.DropTable(
                name: "Chua");

            migrationBuilder.DropTable(
                name: "KieuThanhVien");
        }
    }
}
