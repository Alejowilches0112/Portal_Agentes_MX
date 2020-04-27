create or replace procedure dlg_delete_avisos(
fa_secuencia            IN NUMBER,
fa_Error                OUT NUMBER,
fa_Descripcion_Error    OUT VARCHAR2) AS
BEGIN
    fa_Error := 0;
    fa_Descripcion_Error := 'Exito';
    BEGIN
        DELETE FROM DLG_PARAM_AVISOS WHERE SECUENCIA = fa_secuencia;
        commit;
        exception
        WHEN OTHERS THEN
            fa_Error             := 10;
            fa_Descripcion_Error := 'Error eliminando en DLG_PARAM_AVISOS: ' ||
                                    SUBSTR(f_elimina_caracter_especial(SQLERRM),
                                            1,
                                            100);
        --ROLLBACK;
        RETURN;
    END;
    exception
        WHEN OTHERS THEN
            fa_Error             := 20;
            fa_Descripcion_Error := 'Error eliminando en DLG_PARAM_AVISOS: ' ||
                                    SUBSTR(f_elimina_caracter_especial(SQLERRM),
                                            1,
                                            100);
        --ROLLBACK;
        RETURN;
END dlg_delete_avisos;