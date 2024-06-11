namespace SGE.Aplicacion;

public class CasoDeUsoTramiteAlta(ITramiteRepositorio repoTramite, TramiteValidador validador, IServicioAutorizacion autorizacion, ServicioActualizacionEstado servicioActualizacion)
{
    public void Ejecutar(Tramite tramite, int idUsuario)
    {
        if(autorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteAlta))
        {
            
            if(!validador.ValidarTramite(tramite, out string msg))
            {
                throw new Exception(msg);
            }
            else
            {
                repoTramite.AgregarTramite(tramite);
                
                servicioActualizacion.Ejecutar(tramite.ExpedienteId, tramite.Etiqueta);
            }

        }
        else
        {

            throw new AutorizacionException("No posee los permisos necesarios para realizar esa operación.");

        }
    }
}