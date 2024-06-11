using System;
namespace SGE.Aplicacion;

public class Expediente
{

    private int _id;
    public int ID {get => _id;
                    set {_id = value; } }
    public string? caratula;
    public DateTime fechaYHoraCreacion {get; set;} = DateTime.Now;
    public DateTime fechaYHoraActualizacion {get; set;}
    public int usuarioID {get; set;}
    private EstadoExpediente _estado {get; set;} = EstadoExpediente.Recien_Iniciado;
    public EstadoExpediente Estado
    {
        get => _estado;
        set => _estado = value;
    }
    private List<Tramite> _tramiteList = new List<Tramite>();
    public List<Tramite> TramiteList
    {
        get => _tramiteList; 
        set => _tramiteList = value;
    }

    public Expediente(string caratula, int usuarioID) 
    {

        this.caratula = caratula;
        this.usuarioID = usuarioID;
        this.fechaYHoraActualizacion = this.fechaYHoraCreacion;

    }

    public Expediente() {}

    public override string ToString()
    {
        return $"""
        ID de Expediente: {ID}
           ID de Usuario: {usuarioID}
           Carátula: {caratula}
           Fecha y hora de:
             creación {fechaYHoraCreacion}
             modificacion {fechaYHoraActualizacion}
           Estado: {Estado} 
        """ + "\n";
    }
}