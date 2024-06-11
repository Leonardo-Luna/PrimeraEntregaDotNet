namespace SGE.Aplicacion;

public class Tramite
{


    public int ExpedienteId {get; set;}
    private int _id;
    public int IDTramite
    {
        get => _id;
        set => _id = value;
    }
    private EtiquetaTramite _etiqueta = EtiquetaTramite.Escrito_Presentado;
    public EtiquetaTramite Etiqueta
    {
        get => _etiqueta;
        set => _etiqueta = value;
    }
    public string? descripcion;
    public DateTime fechaYhoraCreacion{get;set;} = DateTime.Now;
    public DateTime fechaYhoraModificacion {get;set;}
    public int idUsuario {get;set;}


    public Tramite()
    {

    }

    public Tramite(string descripcion, int idUsuario, int expedienteId) 
    {

        this.descripcion = descripcion;
        this.fechaYhoraModificacion = fechaYhoraCreacion;
        this.idUsuario = idUsuario;
        this.ExpedienteId = expedienteId;

    }

    public override string ToString()
    {
        return $"""
        ID tramite: {IDTramite}
           ID expediente: {ExpedienteId}
           ID usuario: {idUsuario}
           Etiqueta: {Etiqueta}
           Descripción: {descripcion}
           Fecha y hora de:
             creacion: {fechaYhoraCreacion}
             modificacion: {fechaYhoraModificacion}
        """ + "\n";
    }

}