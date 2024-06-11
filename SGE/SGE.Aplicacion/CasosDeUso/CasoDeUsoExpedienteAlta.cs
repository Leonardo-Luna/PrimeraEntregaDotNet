namespace SGE.Aplicacion;

public class CasoDeUsoExpedienteAlta(IExpedienteRepositorio repo, ExpedienteValidador validador, IServicioAutorizacion autorizacion)
{
        
    public void Ejecutar(Expediente e, int idUsuario)
    {

        if(autorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteAlta))
        {

            if(validador.Validar(e, out string errorMessage) == false)
            {

                throw new ValidacionException(errorMessage);

            }
            
            repo.AgregarExpediente(e);

        }
        else
        {

            throw new AutorizacionException("No posee los permisos necesarios para realizar esa operación.");

        }

    } 
    
}