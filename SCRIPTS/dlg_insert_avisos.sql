create or replace procedure dlg_insert_aviso(
fa_titulo               IN VARCHAR2,
fa_contenido            IN VARCHAR2,
fa_fuente               IN NUMBER,
fa_imagenes             IN CLOB,
fa_enlaces              IN CLOB,
fa_fch_ini              IN DATE,
fa_fch_fin              IN DATE,
fa_Error                OUT NUMBER,
fa_Descripcion_Error    OUT VARCHAR2) AS

ldb_secuencia number := 0;
ldb_encuentra number := 0;
BEGIN
    fa_Error := 0;
    fa_Descripcion_Error := 'Exito'
    BEGIN
     select dlg_seq_avisos.nextval
     into ldb_secuencia from dual;
    END;
    BEGIN
        SELECT COUNT(*) 
        into ldb_encuentra
        FROM DLG_PARAM_AVISOS 
        WHERE fa_fch_ini BETWEEN FECHA_INICIO_VIGENCIA AND FECHA_FIN_VIGENCIA
        and fa_fch_fin BETWEEN FECHA_INICIO_VIGENCIA AND FECHA_FIN_VIGENCIA;
            exception
        WHEN OTHERS THEN
            fa_Error             := 10;
            fa_Descripcion_Error := 'Error insertando en DLG_PARAM_AVISOS: ' ||
                                    SUBSTR(f_elimina_caracter_especial(SQLERRM),
                                            1,
                                            100);
        ROLLBACK;
        RETURN;
    END;
    
    if ldb_encuentra <> 0 then
        fa_Error             := 20;
        fa_Descripcion_Error := ' Ya hay un aviso configurado en las fechas establecidad. Favor Cambie las Fechas de Vigencia ';
        ROLLBACK;
        RETURN;
    end if;
    BEGIN
        INSERT INTO DLG_PARAM_AVISOS(
            SECUENCIA,
            TITULO_AVISO,
            CONTENIDO_AVISO,
            FUENTE_AVISO,
            URL_IMAGENES,
            ENLACES,
            FECHA_INICIO_VIGENCIA,
            FECHA_FIN_VIGENCIA)
        VALUES(
            ldb_secuencia,
            fa_titulo,
            fa_contenido,
            fa_fuente,
            fa_imagenes,
            fa_enlaces,
            fa_fch_ini,
            fa_fch_fin);
        COMMIT;
        exception
        WHEN OTHERS THEN
            fa_Error             := 30;
            fa_Descripcion_Error := 'Error insertando en DLG_PARAM_AVISOS: ' ||
                                    SUBSTR(f_elimina_caracter_especial(SQLERRM),
                                            1,
                                            100);
        ROLLBACK;
        RETURN;
    END;
    exception
        WHEN OTHERS THEN
            fa_Error             := 40;
            fa_Descripcion_Error := 'Error insertando en DLG_PARAM_AVISOS: ' ||
                                    SUBSTR(f_elimina_caracter_especial(SQLERRM),
                                            1,
                                            100);
        ROLLBACK;
        RETURN;
END dlg_insert_aviso;