create or replace procedure dlg_upd_avisos(
fa_secuencia            IN NUMBER,
fa_titulo               IN VARCHAR2,
fa_contenido            IN VARCHAR2,
fa_fuente               IN NUMBER,
fa_imagenes             IN CLOB,
fa_enlaces              IN CLOB,
fa_fch_ini              IN DATE,
fa_fch_fin              IN DATE,
fa_Error                OUT NUMBER,
fa_Descripcion_Error    OUT VARCHAR2) AS
BEGIN
    fa_Error := 0;
    fa_Descripcion_Error := 'Exito';
    BEGIN
        UPDATE DLG_PARAM_AVISOS SET
            TITULO_AVISO = fa_titulo,
            CONTENIDO_AVISO = fa_contenido,
            FUENTE_AVISO = fa_fuente,
            URL_IMAGENES = fa_imagenes,
            ENLACES = fa_enlaces,
            FECHA_INICIO_VIGENCIA = fa_fch_ini,
            FECHA_FIN_VIGENCIA = fa_fch_fin
        WHERE SECUENCIA = fa_secuencia;
        commit;
        exception
        WHEN OTHERS THEN
            fa_Error             := 10;
            fa_Descripcion_Error := 'Error actualizando en DLG_PARAM_AVISOS: ' ||
                                    SUBSTR(f_elimina_caracter_especial(SQLERRM),
                                            1,
                                            100);
        --ROLLBACK;
        RETURN;
    END;
    exception
        WHEN OTHERS THEN
            fa_Error             := 20;
            fa_Descripcion_Error := 'Error actualizando en DLG_PARAM_AVISOS: ' ||
                                    SUBSTR(f_elimina_caracter_especial(SQLERRM),
                                            1,
                                            100);
        --ROLLBACK;
        RETURN;
END dlg_upd_avisos;