namespace SGE.Aplicacion;

public interface ITramiteRepositorio
{
    void AgregarTramite(Tramite Tramite);
    void EliminarTramite(int idTramite);
    void EliminarCompleto(int idExpediente);
    Tramite BuscarTramite(int idTramite);
    Tramite BuscarUltimo(int idExpediente);
    void ImprimirPantallaPorEtiqueta(string etiqueta);
    List<Tramite> ListarPorExpediente(int idExpediente);
    void ModificarTramite(Tramite t, string etiqueta);
}