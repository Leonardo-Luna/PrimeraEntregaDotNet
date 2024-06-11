using SGE.Aplicacion;
using SGE.Repositorios;

class Program
{
    public static void Main(string[] args)
    {
        // Instancias de casos de uso e inyecciones de dependencia
        IExpedienteRepositorio ExpedienteRepositorio = new RepositorioExpedienteTXT();
        ITramiteRepositorio TramiteRepositorio = new RepositorioTramiteTXT();
        ServicioActualizacionEstado ServicioActualizacion = new ServicioActualizacionEstado(ExpedienteRepositorio, new EspecificacionCambioEstado());

        var BajaExpediente = new CasoDeUsoExpedienteBaja(ExpedienteRepositorio, TramiteRepositorio, new ServicioAutorizacionProvisorio());
        var AltaExpediente = new CasoDeUsoExpedienteAlta(ExpedienteRepositorio, new ExpedienteValidador(), new ServicioAutorizacionProvisorio());
        var TodosExpedientes = new CasoDeUsoExpedienteConsultaTodos(ExpedienteRepositorio);
        var ExpedientesPorID = new CasoDeUsoExpedienteConsultaPorId(ExpedienteRepositorio, TramiteRepositorio);
        var ExpedienteModificacion = new CasoDeUsoExpedienteModificacion(ExpedienteRepositorio, new ServicioAutorizacionProvisorio());

        var BajaTramite = new CasoDeUsoTramiteBaja(TramiteRepositorio, new ServicioAutorizacionProvisorio(), ServicioActualizacion);
        var AltaTramite = new CasoDeUsoTramiteAlta(TramiteRepositorio, new TramiteValidador(), new ServicioAutorizacionProvisorio(), ServicioActualizacion);
        var TramitePorEtiqueta = new CasoDeUsoTramiteConsultaPorEtiqueta(TramiteRepositorio);
        var TramiteModificacion = new CasoDeUsoTramiteModificacion(TramiteRepositorio, new ServicioAutorizacionProvisorio(), ServicioActualizacion);
    
        try
        {
            AltaExpediente.Ejecutar(new Expediente("Carátula del Expediente", 1){}, 1);
            AltaExpediente.Ejecutar(new Expediente("Carátula del Expediente", 1){}, 1);

            AltaTramite.Ejecutar(new Tramite("Descripción del Tramite", 1, 1){}, 1);
            AltaTramite.Ejecutar(new Tramite("Descripción del Tramite", 1, 2){}, 1);

            ExpedientesPorID.Ejecutar(1);
            ExpedientesPorID.Ejecutar(2);
            
            TramiteModificacion.Ejecutar(1, "Pase_Estudio", 1);
            ExpedientesPorID.Ejecutar(1);
            
            
            AltaTramite.Ejecutar(new Tramite("Descripción del Tramite", 1, 1){}, 1);
            ExpedientesPorID.Ejecutar(1);

            AltaTramite.Ejecutar(new Tramite("Descripción del Tramite", 1, 2){}, 1);
            ExpedientesPorID.Ejecutar(2);
            BajaExpediente.Ejecutar(2, 1);

            TodosExpedientes.Ejecutar();
            ExpedientesPorID.Ejecutar(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}