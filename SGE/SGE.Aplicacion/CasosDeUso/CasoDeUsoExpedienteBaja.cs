namespace SGE.Aplicacion;

public class CasoDeUsoExpedienteBaja(IExpedienteRepositorio repoExpediente, ITramiteRepositorio repoTramite, IServicioAutorizacion autorizacion)
{

    public void Ejecutar(int idExpediente, int idUsuario)
    {

        if(autorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteAlta))
        {

            repoTramite.EliminarCompleto(idExpediente);

            repoExpediente.EliminarExpediente(idExpediente);

        }
        else
        {

            throw new AutorizacionException("No posee los permisos necesarios para realizar esa operación.");

        }

    }

}