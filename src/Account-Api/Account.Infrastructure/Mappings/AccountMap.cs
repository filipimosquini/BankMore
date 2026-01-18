using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure.Mappings;

public class AccountMap : IEntityTypeConfiguration<Core.AccountAggregate.Account>
{
    public void Configure(EntityTypeBuilder<Core.AccountAggregate.Account> builder)
    {
        builder.ToTable("CONTACORRENTE", schema: "ACCOUNT_BANK", t => t.HasComment("Tabela de conta correntes"));

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("IDCONTACORRENTE")
            .HasColumnType("VARCHAR2(32)")
            .HasComment("UUID da conta corrente")
            .HasMaxLength(32)
            .HasConversion(
                v => v.ToString("N"),
                v => Guid.ParseExact(v, "N"));

        builder.Property(e => e.Number)
            .HasColumnName("NUMERO")
            .HasComment("Número da conta corrente");

        builder.Property(e => e.Holder)
            .HasColumnName("NOME")
            .HasComment("Titular da conta corrente");

        builder
            .Property(e => e.Active)
            .HasColumnName("ATIVO")
            .HasComment("Indica se a conta corrente está ativa ou inativa")
            .HasConversion<int>();

        builder
            .Property(e => e.UserId)
            .HasColumnName("IDUSUARIO")
            .HasComment("UUID relacionado ao usuário")
            .HasColumnType("VARCHAR2(32)")
            .HasMaxLength(32)
            .HasConversion(
                v => v.ToString("N"),
                v => Guid.ParseExact(v, "N")); ;

        builder.HasMany(e => e.Movements)
            .WithOne(e => e.Account)
            .HasForeignKey(e => e.AccountId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}