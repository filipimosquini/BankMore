-- Schema: TRANSFER_BANK

ALTER SESSION SET CONTAINER = FREEPDB1;

DECLARE
  v_count NUMBER;

  PROCEDURE ensure_table(p_owner VARCHAR2, p_table VARCHAR2, p_ddl CLOB) IS
  BEGIN
    SELECT COUNT(*)
      INTO v_count
      FROM all_tables
     WHERE owner = UPPER(p_owner)
       AND table_name = UPPER(p_table);

    IF v_count = 0 THEN
      EXECUTE IMMEDIATE p_ddl;
    END IF;
  END;

BEGIN
  ----------------------------------------------------------------------
  -- TRANSFERENCIA
  ----------------------------------------------------------------------
  ensure_table('TRANSFER_BANK','TRANSFERENCIA', q'[
    CREATE TABLE TRANSFER_BANK.transferencia (
      idtransferencia          VARCHAR2(32) NOT NULL,
      idcontacorrente_origem   VARCHAR2(32) NOT NULL,
      idcontacorrente_destino  VARCHAR2(32) NOT NULL,
      datamovimento   		   DATE         NOT NULL,
      valor                    NUMBER(18,2) NOT NULL,
      CONSTRAINT pk_transferencia PRIMARY KEY (idtransferencia)
      -- FKs para CONTACORRENTE serão adicionadas no script final (cross-schema)
    )
  ]');

  -- índices úteis
  SELECT COUNT(*) INTO v_count FROM all_indexes
   WHERE owner = 'TRANSFER_BANK' AND index_name = 'IX_TRF_ORIGEM';
  IF v_count = 0 THEN
    EXECUTE IMMEDIATE 'CREATE INDEX TRANSFER_BANK.ix_trf_origem ON TRANSFER_BANK.transferencia (idcontacorrente_origem)';
  END IF;

  SELECT COUNT(*) INTO v_count FROM all_indexes
   WHERE owner = 'TRANSFER_BANK' AND index_name = 'IX_TRF_DESTINO';
  IF v_count = 0 THEN
    EXECUTE IMMEDIATE 'CREATE INDEX TRANSFER_BANK.ix_trf_destino ON TRANSFER_BANK.transferencia (idcontacorrente_destino)';
  END IF;

  ----------------------------------------------------------------------
  -- IDEMPOTENCIA
  ----------------------------------------------------------------------
  ensure_table('TRANSFER_BANK','IDEMPOTENCIA', q'[
    CREATE TABLE TRANSFER_BANK.idempotencia (
      chave_idempotencia VARCHAR2(32) NOT NULL,
      requisicao         VARCHAR2(1000),
      resultado          VARCHAR2(1000),
      CONSTRAINT pk_trf_idempotencia PRIMARY KEY (chave_idempotencia)
    )
  ]');
END;
/
