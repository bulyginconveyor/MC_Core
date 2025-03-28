using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core_service.infrastructure.repository.postgresql.configurations;

public class CreditBankAccountConfiguration : IEntityTypeConfiguration<CreditBankAccount>
{
    public void Configure(EntityTypeBuilder<CreditBankAccount> builder)
    {
        builder.ToTable("credit_bank_accounts");

        builder.ComplexProperty(c => c.Amount, amountBuilder =>
        {
            amountBuilder.Property(a => a.Value).HasColumnName("amount");
        });
        builder.ComplexProperty(c => c.InitPayment, initPaymentBuilder =>
        {
            initPaymentBuilder.Property(i => i.Value).HasColumnName("init_payment");
        });
        builder.ComplexProperty(c => c.Percent, percentBuilder =>
        {
            percentBuilder.Property(p => p.Value).HasColumnName("percent");
        });
        builder.ComplexProperty(c => c.DateRange, dateRangeBuilder =>
        {
            dateRangeBuilder.Property(d => d.StartDate).HasColumnName("start_date");
            dateRangeBuilder.Property(d => d.EndDate).HasColumnName("end_date");
        });
        builder.ComplexProperty(c => c.PurposeLoan, purposeLoanBuilder =>
        {
            purposeLoanBuilder.Property(p => p.Value).HasColumnName("purpose_loan_name");
        });
        
        builder
            .Property(c => c.TypeCredit)
            .HasConversion<int>()
            .HasColumnName("type_credit");
        
        builder
            .HasOne(c => c.Term)
            .WithMany().HasForeignKey("term_id");;
        builder
            .HasOne(c => c.LoanObject)
            .WithMany().HasForeignKey("loan_object_id");;
    }
}
