using Account.Core.MovementAggregate;
using Account.Core.MovementAggregate.Enumerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Account.Infrastructure.Mappings;

public class MovementMap : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.ToTable("MOVIMENTO", schema: "ACCOUNT_BANK", t => t.HasComment("Tabela de movimentos"));

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasColumnName("IDMOVIMENTO")
            .HasColumnType("VARCHAR2(32)")
            .HasComment("UUID do movimento")
            .HasMaxLength(32)
            .HasConversion(
                v => v.ToString("N"),
                v => Guid.ParseExact(v, "N"));

        builder
            .Property(e => e.AccountId)
            .HasColumnName("IDCONTACORRENTE")
            .HasColumnType("VARCHAR2(32)")
            .HasComment("UUID relacionado a conta corrente")
            .HasMaxLength(32)
            .HasConversion(
                v => v.ToString("N"),
                v => Guid.ParseExact(v, "N")); ;

        builder
            .Property(e => e.MovementDate)
            .HasColumnName("DATAMOVIMENTO")
            .HasComment("Data do movimento");

        builder.Property(e => e.MovementType)
            .HasColumnName("TIPOMOVIMENTO")
            .HasComment("Tipo do movimento (C)redito ou (D)ebito")
            .HasConversion(new MovementTypeConverter());

        builder.Property(e => e.Amount)
            .HasColumnName("VALOR")
            .HasPrecision(18, 2)
            .HasComment("Valor movimentado");
    }
}

internal class MovementTypeConverter() : ValueConverter<MovementTypeEnum, char>(
    v => v == MovementTypeEnum.C ? 'C' : 'D',
    v => v == 'C' ? MovementTypeEnum.C : MovementTypeEnum.D);