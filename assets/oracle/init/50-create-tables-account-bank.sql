-- Schema: ACCOUNT_BANK

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
  -- CONTACORRENTE
  ----------------------------------------------------------------------
  ensure_table('ACCOUNT_BANK','CONTACORRENTE', q'[
    CREATE TABLE ACCOUNT_BANK.contacorrente (
      idcontacorrente VARCHAR2(32)  NOT NULL,
      numero          NUMBER(10)    NOT NULL,
      nome	          VARCHAR2(100) NOT NULL,
      ativo           NUMBER(1) DEFAULT 1 NOT NULL,
      idusuario		  VARCHAR2(32) 	NOT NULL,
      CONSTRAINT pk_contacorrente PRIMARY KEY (idcontacorrente),
      CONSTRAINT uq_contacorrente_numero UNIQUE (numero),
      CONSTRAINT ck_contacorrente_ativo CHECK (ativo IN (0,1))
    )
  ]');

  ----------------------------------------------------------------------
  -- MOVIMENTO
  ----------------------------------------------------------------------
  ensure_table('ACCOUNT_BANK','MOVIMENTO', q'[
    CREATE TABLE ACCOUNT_BANK.movimento (
      idmovimento     VARCHAR2(32) NOT NULL,
      idcontacorrente VARCHAR2(32) NOT NULL,
      datamovimento   DATE         NOT NULL,
      tipomovimento   CHAR(1)      NOT NULL,
      valor           NUMBER(18,2) NOT NULL,
      CONSTRAINT pk_movimento PRIMARY KEY (idmovimento),
      CONSTRAINT ck_movimento_tipo CHECK (tipomovimento IN ('C','D')),
      CONSTRAINT fk_movimento_conta FOREIGN KEY (idcontacorrente)
        REFERENCES ACCOUNT_BANK.contacorrente (idcontacorrente)
    )
  ]');

  ----------------------------------------------------------------------
  -- IDEMPOTENCIA
  ----------------------------------------------------------------------
  ensure_table('ACCOUNT_BANK','IDEMPOTENCIA', q'[
    CREATE TABLE ACCOUNT_BANK.idempotencia (
      chave_idempotencia VARCHAR2(32) NOT NULL,
      requisicao         VARCHAR2(1000),
      resultado          VARCHAR2(1000),
      CONSTRAINT pk_idempotencia PRIMARY KEY (chave_idempotencia)
    )
  ]');
END;
/
