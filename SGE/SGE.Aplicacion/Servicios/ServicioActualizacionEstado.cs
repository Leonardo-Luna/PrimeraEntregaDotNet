namespace SGE.Aplicacion;
public class ServicioActualizacionEstado(IExpedienteRepositorio repoExpediente, EspecificacionCambioEstado cambioEstado)
{
    public void Ejecutar(int idE, EtiquetaTramite etiqueta)
    {
        
        Expediente e = repoExpediente.BuscarExpedientePorId(idE);

        EstadoExpediente estado = cambioEstado.Ejecutar(etiqueta) ?? e.Estado;

        repoExpediente.ModificarEstadoExpediente(e, estado);
        
    }
}