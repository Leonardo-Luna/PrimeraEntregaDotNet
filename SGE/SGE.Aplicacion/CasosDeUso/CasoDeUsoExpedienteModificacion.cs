namespace SGE.Aplicacion;

public class CasoDeUsoExpedienteModificacion(IExpedienteRepositorio repoExpe, IServicioAutorizacion autorizacion)
{
    public void Ejecutar(int eId, int idUsuario, string caratula, string estado)
    {
        if (autorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteModificacion))
        {

            if(!string.IsNullOrWhiteSpace(caratula))
            {
                
                repoExpe.CambioDeInfo(eId, caratula, estado);
            }
            else
            {
                throw new ValidacionException("La carátula no puede estar vacía.\n");
            }
        }
        else
        {

            throw new AutorizacionException("No posee los permisos necesarios para realizar esa operación.");

        }
    }
}