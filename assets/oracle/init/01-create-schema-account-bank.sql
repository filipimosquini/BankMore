-- =========================================================
-- Criação do usuário / schema da aplicação (IDEMPOTENTE)
-- Executado como SYS (SYSDBA)
-- IMPORTANTE: cria o usuário no PDB FREEPDB1
-- =========================================================

ALTER SESSION SET CONTAINER = FREEPDB1;

DECLARE
  v_user_count NUMBER;
BEGIN
  SELECT COUNT(*)
    INTO v_user_count
    FROM dba_users
   WHERE username = 'ACCOUNT_BANK';

  IF v_user_count = 0 THEN
    EXECUTE IMMEDIATE q'[
      CREATE USER ACCOUNT_BANK IDENTIFIED BY auth123
        DEFAULT TABLESPACE USERS
        TEMPORARY TABLESPACE TEMP
        QUOTA UNLIMITED ON USERS
    ]';

    -- Permissões básicas
    EXECUTE IMMEDIATE 'GRANT CONNECT, RESOURCE TO ACCOUNT_BANK';

    -- Permissões necessárias para EF Core / Identity
    EXECUTE IMMEDIATE 'GRANT CREATE TABLE, CREATE VIEW, CREATE SEQUENCE, CREATE PROCEDURE, CREATE TRIGGER TO ACCOUNT_BANK';
  END IF;
END;
/
-- =========================================================
-- Permissões TOTAIS (DEV) para SYSTEM (IDEMPOTENTE)
-- (válidas para TODOS os schemas – use só em DEV)
-- =========================================================

DECLARE
  PROCEDURE grant_if_missing(p_priv VARCHAR2) IS
    v_cnt NUMBER;
  BEGIN
    SELECT COUNT(*)
      INTO v_cnt
      FROM dba_sys_privs
     WHERE grantee   = 'SYSTEM'
       AND privilege = p_priv;

    IF v_cnt = 0 THEN
      EXECUTE IMMEDIATE 'GRANT ' || p_priv || ' TO SYSTEM';
    END IF;
  END;
BEGIN
  -- DML globais
  grant_if_missing('SELECT ANY TABLE');
  grant_if_missing('INSERT ANY TABLE');
  grant_if_missing('UPDATE ANY TABLE');
  grant_if_missing('DELETE ANY TABLE');

  -- DDL globais
  grant_if_missing('CREATE ANY TABLE');
  grant_if_missing('ALTER ANY TABLE');
  grant_if_missing('DROP ANY TABLE');

  grant_if_missing('CREATE ANY VIEW');
  grant_if_missing('DROP ANY VIEW');

  grant_if_missing('CREATE ANY SEQUENCE');
  grant_if_missing('DROP ANY SEQUENCE');

  grant_if_missing('CREATE ANY PROCEDURE');
  grant_if_missing('ALTER ANY PROCEDURE');
  grant_if_missing('DROP ANY PROCEDURE');

  grant_if_missing('CREATE ANY TRIGGER');
  grant_if_missing('DROP ANY TRIGGER');
END;
/
