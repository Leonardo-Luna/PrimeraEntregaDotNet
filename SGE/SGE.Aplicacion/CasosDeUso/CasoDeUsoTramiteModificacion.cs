using SGE.Aplicacion;

public class CasoDeUsoTramiteModificacion(ITramiteRepositorio repoTramite, IServicioAutorizacion autorizacion, ServicioActualizacionEstado servicioActualizacion)
{
    public void Ejecutar(int idTramite, string etiqueta, int idUsuario)
    {

        if(autorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteModificacion))
        {
            Tramite tAux = repoTramite.BuscarTramite(idTramite);
            
            repoTramite.ModificarTramite(tAux, etiqueta);

            Tramite tramite = repoTramite.BuscarTramite(idTramite);
            
            servicioActualizacion.Ejecutar(tramite.ExpedienteId, tramite.Etiqueta);
        }
        else
        {

            throw new AutorizacionException("No posee los permisos necesarios para realizar esa operación.");

        }
    }
}