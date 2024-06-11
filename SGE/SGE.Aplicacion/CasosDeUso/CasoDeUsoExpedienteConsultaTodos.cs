namespace SGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaTodos(IExpedienteRepositorio repoExpediente)
{
    public void Ejecutar()
    {
        repoExpediente.ImprimirPantalla();
    }
}