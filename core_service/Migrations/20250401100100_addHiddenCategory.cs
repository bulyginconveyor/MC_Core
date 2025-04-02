using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core_service.Migrations
{
    /// <inheritdoc />
    public partial class addHiddenCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "terms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 500, DateTimeKind.Utc).AddTicks(5612));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "terms",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("115bd335-b29a-4365-bf8f-51a68ce1d0a9"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "periods",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 501, DateTimeKind.Utc).AddTicks(889));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "periods",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("9df62390-d619-4df9-a119-853a9db00fb8"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "operations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 513, DateTimeKind.Utc).AddTicks(6126));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "operations",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("1c2322af-5fc2-4336-a4ce-0c5b107d924e"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "debet_bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "currency",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 500, DateTimeKind.Utc).AddTicks(2794));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "currency",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("f42bd12c-fdb5-4671-b201-5da9a993b23c"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "credit_bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "contribution_bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 502, DateTimeKind.Utc).AddTicks(2080));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "categories",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("900872d1-07b4-4b1c-92ee-ebda418120a7"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "bank_accounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 503, DateTimeKind.Utc).AddTicks(7125));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "active_bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"));

            migrationBuilder.CreateTable(
                name: "hidden_categories",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hidden_categories", x => new { x.category_id, x.user_id });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hidden_categories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "terms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 500, DateTimeKind.Utc).AddTicks(5612),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 10, 0, 59, 405, DateTimeKind.Utc).AddTicks(7175));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "terms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("115bd335-b29a-4365-bf8f-51a68ce1d0a9"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("1a62557a-209f-4171-b63f-11bb65ff3e86"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "periods",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 501, DateTimeKind.Utc).AddTicks(889),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 10, 0, 59, 406, DateTimeKind.Utc).AddTicks(2276));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "periods",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("9df62390-d619-4df9-a119-853a9db00fb8"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("4fa07266-fe66-4ba8-84ea-281547ca7e35"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "operations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 513, DateTimeKind.Utc).AddTicks(6126),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 10, 0, 59, 418, DateTimeKind.Utc).AddTicks(8067));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "operations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("1c2322af-5fc2-4336-a4ce-0c5b107d924e"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("9d1945ce-4b44-4dce-90d5-9e324b01fee4"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "debet_bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("3658ca2a-ccdb-4d9e-88c5-998428838d37"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "currency",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 500, DateTimeKind.Utc).AddTicks(2794),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 10, 0, 59, 405, DateTimeKind.Utc).AddTicks(4169));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "currency",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("f42bd12c-fdb5-4671-b201-5da9a993b23c"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("abda837a-2c3b-438a-bca3-c7bae0ae891d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "credit_bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("3658ca2a-ccdb-4d9e-88c5-998428838d37"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "contribution_bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("3658ca2a-ccdb-4d9e-88c5-998428838d37"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 502, DateTimeKind.Utc).AddTicks(2080),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 10, 0, 59, 407, DateTimeKind.Utc).AddTicks(3474));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("900872d1-07b4-4b1c-92ee-ebda418120a7"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("c8cc136b-f5dd-4957-a327-e9d324c4d8b4"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "bank_accounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 1, 9, 57, 31, 503, DateTimeKind.Utc).AddTicks(7125),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 4, 1, 10, 0, 59, 408, DateTimeKind.Utc).AddTicks(8645));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("3658ca2a-ccdb-4d9e-88c5-998428838d37"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "active_bank_accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("977b9415-5cec-429d-8e5a-e53a44253501"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("3658ca2a-ccdb-4d9e-88c5-998428838d37"));
        }
    }
}
