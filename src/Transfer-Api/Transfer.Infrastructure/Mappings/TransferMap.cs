using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Transfer.Infrastructure.Mappings;

public class TransferMap : IEntityTypeConfiguration<Core.TransferAggregate.Transfer>
{
    public void Configure(EntityTypeBuilder<Core.TransferAggregate.Transfer> builder)
    {
        builder.ToTable("TRANSFERENCIA", schema: "TRANSFER_BANK", t => t.HasComment("Tabela de transferencias"));

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("IDTRANSFERENCIA")
            .HasColumnType("VARCHAR2(32)")
            .HasComment("UUID da conta corrente")
            .HasMaxLength(32)
            .HasConversion(
                v => v.ToString("N"),
                v => Guid.ParseExact(v, "N"));

        builder.Property(e => e.SourceAccountId)
            .HasColumnName("IDCONTACORRENTE_ORIGEM")
            .HasColumnType("VARCHAR2(32)")
            .HasComment("UUID da conta corrente de origem")
            .HasMaxLength(32)
            .HasConversion(
                v => v.ToString("N"),
                v => Guid.ParseExact(v, "N"));

        builder.Property(e => e.DestinationAccountId)
            .HasColumnName("IDCONTACORRENTE_DESTINO")
            .HasColumnType("VARCHAR2(32)")
            .HasComment("UUID da conta corrente de destino")
            .HasMaxLength(32)
            .HasConversion(
                v => v.ToString("N"),
                v => Guid.ParseExact(v, "N"));

        builder
            .Property(e => e.TransferDate)
            .HasColumnName("DATAMOVIMENTO")
            .HasComment("Data do movimento");

        builder.Property(e => e.Amount)
            .HasColumnName("VALOR")
            .HasPrecision(18, 2)
            .HasComment("Valor movimentado");
    }
}