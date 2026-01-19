using Transfer.Core.Common.Indepotencies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Transfer.Infrastructure.Common.Idempotencies.Mappings;

public class IdempotencyMap : IEntityTypeConfiguration<Idempotency>
{
    public void Configure(EntityTypeBuilder<Idempotency> builder)
    {
        builder.ToTable("IDEMPOTENCIA", schema: "TRANSFER_BANK", t => t.HasComment("Tabela de idempotencia"));

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("CHAVE_IDEMPOTENCIA")
            .HasColumnType("VARCHAR2(32)")
            .HasComment("UUID relacionado a idempotencia")
            .HasMaxLength(32)
            .HasConversion(
                v => v.ToString("N"),
                v => Guid.ParseExact(v, "N"));

        builder.Property(x => x.RequestHash)
            .HasColumnName("REQUISICAO")
            .HasColumnType("VARCHAR2(1000)")
            .HasComment("Representação da requisição")
            .HasMaxLength(1000);

        builder.Property(x => x.Result)
            .HasColumnName("RESULTADO")
            .HasColumnType("VARCHAR2(1000)")
            .HasComment("Representação do resultado")
            .HasMaxLength(1000);
    }
}