-- Schema: FEE_BANK

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
  -- TARIFA
  ----------------------------------------------------------------------
  ensure_table('FEE_BANK','TARIFA', q'[
    CREATE TABLE FEE_BANK.tarifa (
      idtarifa        VARCHAR2(37) NOT NULL,
      idcontacorrente VARCHAR2(37) NOT NULL,
      datamovimento   VARCHAR2(25) NOT NULL,
      valor           NUMBER(18,2) NOT NULL,
      CONSTRAINT pk_tarifa PRIMARY KEY (idtarifa)
      -- FK para CONTACORRENTE será adicionada no script final (cross-schema)
    )
  ]');

  -- índice útil
  SELECT COUNT(*)
    INTO v_count
    FROM all_indexes
   WHERE owner = 'FEE_BANK'
     AND index_name = 'IX_TARIFA_IDCONTA';

  IF v_count = 0 THEN
    EXECUTE IMMEDIATE 'CREATE INDEX FEE_BANK.ix_tarifa_idconta ON FEE_BANK.tarifa (idcontacorrente)';
  END IF;
END;
/
