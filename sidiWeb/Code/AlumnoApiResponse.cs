public class AlumnoApiResponse
{
    public string dni { get; set; }
    public DatosAcademicos datosAcademicos { get; set; }
    public DatosIdiomas datosIdiomas { get; set; }
}


public class DatosAcademicos
{
    public string codigoUniversitario { get; set; }
    public string nombresCompletos { get; set; }
    public string tipoMatricula { get; set; }
    public string estadoAcademico { get; set; }
    public string periodoAcademico { get; set; }
    public string error { get; set; }
}


public class DatosIdiomas
{
    public string tipoEstudiante { get; set; }
    public string nombresCompletos { get; set; }
    public string codAlumno { get; set; }
    public string periodoMatriculado { get; set; }
    public string estado { get; set; }
    public string error { get; set; }
}