namespace SGE.Aplicacion;
public class CasoDeUsoTramiteConsultaPorEtiqueta(ITramiteRepositorio repoTramite)
{
    public void Ejecutar(string etiqueta)
    {
        repoTramite.ImprimirPantallaPorEtiqueta(etiqueta);
    }
}