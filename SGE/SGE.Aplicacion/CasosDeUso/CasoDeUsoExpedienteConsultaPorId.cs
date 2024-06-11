namespace SGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaPorId(IExpedienteRepositorio repoExpediente, ITramiteRepositorio repoTramite)
{
    public void Ejecutar(int idE)
    {
        Expediente e = repoExpediente.BuscarExpedientePorId(idE);
        e.TramiteList = repoTramite.ListarPorExpediente(idE);
        repoExpediente.ImprimirPantallaPorId(e);
    }
}