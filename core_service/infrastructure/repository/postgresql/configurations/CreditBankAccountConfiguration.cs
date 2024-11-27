using core_service.domain;
using core_service.domain.enums;
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
            .HasConversion(v => v.ToString(),
                v => (TypeCreditBankAccount)Enum.Parse(typeof(TypeCreditBankAccount), v)).HasColumnName("type_credit");
        
        builder
            .HasOne(c => c.Term)
            .WithMany();
        builder
            .HasOne(c => c.LoanObject)
            .WithMany();
    }
}