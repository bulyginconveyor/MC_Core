using core_service.domain.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class DebetBankAccountConfiguration : IEntityTypeConfiguration<DebetBankAccount>
{
    public void Configure(EntityTypeBuilder<DebetBankAccount> builder)
    {
        builder.ToTable("debet_bank_accounts");
    }
}