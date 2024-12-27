using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class ContributionBankAccountConfiguration : IEntityTypeConfiguration<ContributionBankAccount>
{
    public void Configure(EntityTypeBuilder<ContributionBankAccount> builder)
    {
        builder.ToTable("contribution_bank_accounts");

        builder.ComplexProperty(c => c.DateRange, dateRangeBuilder =>
        {
            dateRangeBuilder.Property(d => d.StartDate).HasColumnName("start_date");
            dateRangeBuilder.Property(d => d.EndDate).HasColumnName("end_date");
        });
        builder.Property(c => c.ActualСlosed).HasColumnName("actual_closed");
        builder.ComplexProperty(c => c.Percent, percentBuilder =>
        {
            percentBuilder.ComplexProperty(p => p.Percent, pBuilder =>
            {
                pBuilder.Property(p => p.Value).HasColumnName("percent");
            });
            percentBuilder.Property(p => p.CountDays).HasColumnName("percent_count_days");
        });
        builder
            .Property(c => c.TypeContribution)
            .HasConversion(v => v.ToString(),
                v => (TypeContributionBankAccount)Enum.Parse(typeof(TypeContributionBankAccount), v)).HasColumnName("type_contribution");
        builder.ComplexProperty(c => c.Amount, amountBuilder =>
        {
            amountBuilder.Property(a => a.Value).HasColumnName("amount");
        });
    }
}