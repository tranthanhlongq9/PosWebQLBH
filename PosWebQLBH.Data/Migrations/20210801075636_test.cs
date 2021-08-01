using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PosWebQLBH.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID_Category = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name_Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__6DB3A68A23DB85E7", x => x.ID_Category);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID_Customer = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Customer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__2D8FDE5FC302B124", x => x.ID_Customer);
                });

            migrationBuilder.CreateTable(
                name: "FunctionList",
                columns: table => new
                {
                    ID_Function = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name_Function = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionList", x => x.ID_Function);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    ID_History = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_SellOrder = table.Column<long>(type: "bigint", nullable: false),
                    ID_Customer = table.Column<long>(type: "bigint", nullable: false),
                    ID_PurchaseOrder = table.Column<long>(type: "bigint", nullable: false),
                    ID_Product = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Employee = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Suppliers = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TotalAmount = table.Column<string>(type: "char(15)", unicode: false, fixedLength: true, maxLength: 15, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Discount = table.Column<string>(type: "char(15)", unicode: false, fixedLength: true, maxLength: 15, nullable: false),
                    PriceAfterDiscount = table.Column<string>(type: "char(15)", unicode: false, fixedLength: true, maxLength: 15, nullable: false),
                    Type = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__History__F51C56DC034F2B20", x => x.ID_History);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID_Supplier = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name_Supplier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Representative = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__3D9EE7AC0B95603E", x => x.ID_Supplier);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    ID_Unit = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name_Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Unit__EB4517D39276DF6A", x => x.ID_Unit);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID_Role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Function = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name_Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Feature_List = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__43DCD32D3B76F765", x => x.ID_Role);
                    table.ForeignKey(
                        name: "FK_Roles_FunctionList",
                        column: x => x.ID_Function,
                        principalTable: "FunctionList",
                        principalColumn: "ID_Function",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID_Product = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Unit = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Category = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name_Product = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__522DE496A4887A31", x => x.ID_Product);
                    table.ForeignKey(
                        name: "FK_Products_Category",
                        column: x => x.ID_Category,
                        principalTable: "Category",
                        principalColumn: "ID_Category",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Unit",
                        column: x => x.ID_Unit,
                        principalTable: "Unit",
                        principalColumn: "ID_Unit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID_Employee = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name_Employee = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__D9EE4F3660C4DC47", x => x.ID_Employee);
                    table.ForeignKey(
                        name: "FK_Employees_Roles",
                        column: x => x.ID_Role,
                        principalTable: "Roles",
                        principalColumn: "ID_Role",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ID_Inventory = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Product = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventor__2210F49E4001C41D", x => x.ID_Inventory);
                    table.ForeignKey(
                        name: "FK_Inventory_Products",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    ID_PurchaseOrder = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Employee = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Supplier = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Product = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PriceAfterDiscount = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Purchase__F1DAA6381B798B2D", x => x.ID_PurchaseOrder);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Employees",
                        column: x => x.ID_Employee,
                        principalTable: "Employees",
                        principalColumn: "ID_Employee",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Products",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Suppliers",
                        column: x => x.ID_Supplier,
                        principalTable: "Suppliers",
                        principalColumn: "ID_Supplier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellOrder",
                columns: table => new
                {
                    ID_SellOrder = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Employee = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_Customer = table.Column<long>(type: "bigint", nullable: false),
                    ID_Product = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PriceAfterDiscount = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SellOrde__9336A44AE17C6FD3", x => x.ID_SellOrder);
                    table.ForeignKey(
                        name: "FK_SellOrder_Customers",
                        column: x => x.ID_Customer,
                        principalTable: "Customers",
                        principalColumn: "ID_Customer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellOrder_Employees",
                        column: x => x.ID_Employee,
                        principalTable: "Employees",
                        principalColumn: "ID_Employee",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellOrder_Products",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Category__6DB3A68B2DE2309C",
                table: "Category",
                column: "ID_Category",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__2D8FDE5E1AA3C495",
                table: "Customers",
                column: "ID_Customer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ID_Role",
                table: "Employees",
                column: "ID_Role");

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__A9D10534CDAC7A07",
                table: "Employees",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__D9EE4F37E4C83A87",
                table: "Employees",
                column: "ID_Employee",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ID_Product",
                table: "Inventory",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ID_Category",
                table: "Products",
                column: "ID_Category");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ID_Unit",
                table: "Products",
                column: "ID_Unit");

            migrationBuilder.CreateIndex(
                name: "UQ__Products__522DE497C6210C13",
                table: "Products",
                column: "ID_Product",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_ID_Employee",
                table: "PurchaseOrder",
                column: "ID_Employee");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_ID_Product",
                table: "PurchaseOrder",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_ID_Supplier",
                table: "PurchaseOrder",
                column: "ID_Supplier");

            migrationBuilder.CreateIndex(
                name: "UQ__Purchase__390E8B9067C76FF4",
                table: "PurchaseOrder",
                column: "ID_PurchaseOrder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ID_Function",
                table: "Roles",
                column: "ID_Function");

            migrationBuilder.CreateIndex(
                name: "IX_SellOrder_ID_Customer",
                table: "SellOrder",
                column: "ID_Customer");

            migrationBuilder.CreateIndex(
                name: "IX_SellOrder_ID_Employee",
                table: "SellOrder",
                column: "ID_Employee");

            migrationBuilder.CreateIndex(
                name: "IX_SellOrder_ID_Product",
                table: "SellOrder",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "UQ__Supplier__408B709548A2BC3A",
                table: "Suppliers",
                column: "ID_Supplier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Unit__EB4517D25DF98ADB",
                table: "Unit",
                column: "ID_Unit",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "SellOrder");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "FunctionList");
        }
    }
}
