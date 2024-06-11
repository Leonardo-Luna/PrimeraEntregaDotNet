namespace SGE.Aplicacion;

public class TramiteValidador
{
    public bool ValidarTramite(Tramite tramite, out string msg)
    {

        msg = "";

        if(string.IsNullOrWhiteSpace(tramite.descripcion))
        {
            msg = "La descripcion no puede estar vacía. ";
        }
        
        if(tramite.idUsuario <= 0)
        {
            msg += "El ID de usuario debe que ser mayor que 0.";
        }

        return (msg == "");

    }
}