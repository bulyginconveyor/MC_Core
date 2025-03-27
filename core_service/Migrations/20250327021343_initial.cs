using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core_service.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2025, 3, 27, 2, 13, 42, 666, DateTimeKind.Utc).AddTicks(114)),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    color = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2025, 3, 27, 2, 13, 42, 664, DateTimeKind.Utc).AddTicks(172)),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    image_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    iso_code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "periods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    type = table.Column<int>(type: "integer", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2025, 3, 27, 2, 13, 42, 664, DateTimeKind.Utc).AddTicks(8675)),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_periods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "terms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    unit = table.Column<int>(type: "integer", nullable: false),
                    count_units = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2025, 3, 27, 2, 13, 42, 664, DateTimeKind.Utc).AddTicks(3300)),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bank_accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    currency_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2025, 3, 27, 2, 13, 42, 667, DateTimeKind.Utc).AddTicks(5176)),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    is_maybe_negative = table.Column<bool>(type: "boolean", nullable: false),
                    color = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bank_accounts_currency_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "active_bank_accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    buy_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type_active = table.Column<int>(type: "integer", nullable: false),
                    buy_price = table.Column<decimal>(type: "numeric", nullable: false),
                    photo_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_active_bank_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_active_bank_accounts_bank_accounts_Id",
                        column: x => x.Id,
                        principalTable: "bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contribution_bank_accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    actual_closed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    type_contribution = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    percent_count_days = table.Column<int>(type: "integer", nullable: false),
                    percent = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contribution_bank_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contribution_bank_accounts_bank_accounts_Id",
                        column: x => x.Id,
                        principalTable: "bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "debet_bank_accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid())
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_debet_bank_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_debet_bank_accounts_bank_accounts_Id",
                        column: x => x.Id,
                        principalTable: "bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    period_id = table.Column<Guid>(type: "uuid", nullable: true),
                    credit_bank_account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    debet_bank_account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2025, 3, 27, 2, 13, 42, 677, DateTimeKind.Utc).AddTicks(4824)),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    BankAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_operations_bank_accounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "bank_accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_operations_bank_accounts_credit_bank_account_id",
                        column: x => x.credit_bank_account_id,
                        principalTable: "bank_accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_operations_bank_accounts_debet_bank_account_id",
                        column: x => x.debet_bank_account_id,
                        principalTable: "bank_accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_operations_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_operations_periods_period_id",
                        column: x => x.period_id,
                        principalTable: "periods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "credit_bank_accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: Guid.NewGuid()),
                    term_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_object_id = table.Column<Guid>(type: "uuid", nullable: true),
                    type_credit = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    init_payment = table.Column<decimal>(type: "numeric", nullable: false),
                    percent = table.Column<decimal>(type: "numeric", nullable: false),
                    purpose_loan_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credit_bank_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_credit_bank_accounts_active_bank_accounts_loan_object_id",
                        column: x => x.loan_object_id,
                        principalTable: "active_bank_accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_credit_bank_accounts_bank_accounts_Id",
                        column: x => x.Id,
                        principalTable: "bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_credit_bank_accounts_terms_term_id",
                        column: x => x.term_id,
                        principalTable: "terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bank_accounts_currency_id",
                table: "bank_accounts",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_category_id",
                table: "categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_credit_bank_accounts_loan_object_id",
                table: "credit_bank_accounts",
                column: "loan_object_id");

            migrationBuilder.CreateIndex(
                name: "IX_credit_bank_accounts_term_id",
                table: "credit_bank_accounts",
                column: "term_id");

            migrationBuilder.CreateIndex(
                name: "IX_operations_BankAccountId",
                table: "operations",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_operations_category_id",
                table: "operations",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_operations_credit_bank_account_id",
                table: "operations",
                column: "credit_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_operations_debet_bank_account_id",
                table: "operations",
                column: "debet_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_operations_period_id",
                table: "operations",
                column: "period_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contribution_bank_accounts");

            migrationBuilder.DropTable(
                name: "credit_bank_accounts");

            migrationBuilder.DropTable(
                name: "debet_bank_accounts");

            migrationBuilder.DropTable(
                name: "operations");

            migrationBuilder.DropTable(
                name: "active_bank_accounts");

            migrationBuilder.DropTable(
                name: "terms");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "periods");

            migrationBuilder.DropTable(
                name: "bank_accounts");

            migrationBuilder.DropTable(
                name: "currency");
        }
    }
}
