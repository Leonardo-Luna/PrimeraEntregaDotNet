namespace SGE.Aplicacion;

public class ExpedienteValidador
{

    public bool Validar(Expediente e, out string errorMessage)
    {

        errorMessage = "";

        if(string.IsNullOrWhiteSpace(e.caratula))
        {
            errorMessage = "La carátula no puede estar vacía.\n" ;
        }

        if(e.usuarioID <= 0)
        {
            errorMessage += "La ID de usuario debe ser mayor a 0.\n" ;
        }

        return (errorMessage == "");

    }

}