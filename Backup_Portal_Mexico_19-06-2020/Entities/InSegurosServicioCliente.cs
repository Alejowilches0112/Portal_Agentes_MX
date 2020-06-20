public class InSegurosServicioCliente
{
    public double cliente { get; set; }
    public string primerNombre { get; set; }
    public string segundoNombre { get; set; }
    public string primerApellido { get; set; }
    public string segundoApellido { get; set; }
    public string fechaNacimiento { get; set; }
    public string paisNacimiento { get; set; }
    public string entidadNacimiento { get; set; }
    //generales
    public string rfc { get; set; }
    public string curp { get; set; }
    public string genero { get; set; }
    public string nacionalidad { get; set; }
    public string email { get; set; }
    public double celular { get; set; }
    public double propioRecaudos { get; set; }
    //Domicilio
    public string calle { get; set; }
    public string numeroExterior { get; set; }
    public string numeroInterior { get; set; }
    public string entreCalles { get; set; }
    public string colonia { get; set; }
    public string municipio { get; set; }
    public string entidadDomicilio { get; set; }
    public string paisDomicilio { get; set; }
    public int codigoPostal { get; set; }
    public int tiempoResidecia { get; set; }
}