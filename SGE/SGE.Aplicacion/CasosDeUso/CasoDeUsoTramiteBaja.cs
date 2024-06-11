namespace SGE.Aplicacion;
public class CasoDeUsoTramiteBaja(ITramiteRepositorio repoTramite, IServicioAutorizacion autorizacion, ServicioActualizacionEstado servicioActualizacion){
    public void Ejecutar(int idTramite, int idUsuario)
    {

        if(autorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteBaja))
        {
            Tramite t = repoTramite.BuscarTramite(idTramite);
            repoTramite.EliminarTramite(idTramite);
            
            servicioActualizacion.Ejecutar(t.ExpedienteId, t.Etiqueta);
        }
        else
        {

            throw new AutorizacionException("No posee los permisos necesarios para realizar esa operación.");

        }

    }
}