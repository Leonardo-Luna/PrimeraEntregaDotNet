namespace SGE.Repositorios;
using SGE.Aplicacion;

public class RepositorioExpedienteTXT : IExpedienteRepositorio
{
    
    readonly string _nomArchivo = @"..\SGE.Repositorios\expedientes.txt";    

    public void AgregarExpediente(Expediente e)
    {
        
        int id = RepositorioExpedienteID.conseguirID();
        e.ID = id;
        EscribirExpediente(e);

    }

    public void EscribirExpediente(Expediente e)
    {

        if(File.Exists(_nomArchivo))
        {    
            using (var sw = new StreamWriter(_nomArchivo, true))
            {
                sw.WriteLine($"{e.ID} || {e.caratula} || {e.fechaYHoraCreacion} || {e.fechaYHoraActualizacion.ToString()} || {e.Estado} || {e.usuarioID}");
            }
        }

    }

    private List<Expediente> ListarExpedientes()
    {
        var resultado = new List<Expediente>();
        using (StreamReader sr = new StreamReader(_nomArchivo))
        {
            while(!sr.EndOfStream)
            {
                Expediente expedienteCopia = new Expediente();
                string st = sr.ReadLine() ?? "";
                string[]? exp = (st.Split(" || ")) ?? null;
                if(exp != null)
                {
                    expedienteCopia.ID = int.Parse(exp[0]);
                    expedienteCopia.caratula = exp[1];
                    expedienteCopia.fechaYHoraCreacion = DateTime.Parse(exp[2]);
                    expedienteCopia.fechaYHoraActualizacion = DateTime.Parse(exp[3]);
                    expedienteCopia.Estado = (EstadoExpediente) Enum.Parse(typeof(EstadoExpediente), exp[4]);
                    expedienteCopia.usuarioID = int.Parse(exp[5]);

                    resultado.Add(expedienteCopia);
                }
            }
        }
        return resultado;
    }

    public void EliminarExpediente(int eID)
    {

        Expediente e = BuscarExpedientePorId(eID);
        List<Expediente> listaExpedientes = ListarExpedientes();
        Expediente aux;
        int i = 0;

        bool encontre = false;

        while((i <= listaExpedientes.Count) && (!encontre))
        {

            aux = listaExpedientes[i];
            
            if(aux.ID == e.ID)
            {
                listaExpedientes.Remove(aux);
                encontre = true;
            }
            
            i++;

        }
  
    
        if(!encontre)
        {
            throw new RepositorioException("El expediente buscado no existe.");
        }
        else
        {
            SobrescribirListaExpediente(listaExpedientes);
        }

    }

    private void SobrescribirListaExpediente(List<Expediente> lista)
    {

        if(File.Exists(_nomArchivo))
        {    
            using (var sw = new StreamWriter(_nomArchivo))
            {

                foreach(Expediente e in lista)
                {

                    sw.WriteLine($"{e.ID} || {e.caratula} || {e.fechaYHoraCreacion} || {e.fechaYHoraActualizacion.ToString()} || {e.Estado} || {e.usuarioID}");

                }

            }
        }

    }

    public void ModificarEstadoExpediente(Expediente e, EstadoExpediente estado)
    {

        List<Expediente> lista = ListarExpedientes();
        Expediente aux;
        int i = 0;
        bool encontre = false;
        

        while((i <= lista.Count) && (!encontre))
        {

            aux = lista[i];

            if(aux.ID == e.ID)
            {

                aux.Estado =  estado;
                aux.fechaYHoraActualizacion = DateTime.Now;
                encontre = true;

            }

            i++;

        }        

        SobrescribirListaExpediente(lista);

    }

    public Expediente BuscarExpedientePorId(int eId)
    {

        List<Expediente> lista = ListarExpedientes();

        foreach(Expediente eAux in lista)
        {

            if(eAux.ID == eId)
            {

                return eAux;

            } 

        }

        throw new RepositorioException("El expediente buscado no existe.");

    }

    public void ImprimirPantalla()
    {
        List<Expediente> lista = ListarExpedientes();
        foreach(Expediente e in lista)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void ImprimirPantallaPorId(Expediente e)
    {
        Console.WriteLine(e);
        foreach(Tramite t in e.TramiteList)
        {
            Console.WriteLine(t);
        }
    }

    public void CambioDeInfo(int idE, string caratula, string estado)
    {
        List<Expediente> lista = ListarExpedientes();
        Expediente aux;
        int i = 0;
        bool encontre = false;

        while((i <= lista.Count) && (!encontre))
        {

            aux = lista[i];

            if(aux.ID == idE)
            {
                
                if(Enum.IsDefined(typeof(EstadoExpediente), estado))
                {

                    EstadoExpediente est = (EstadoExpediente) Enum.Parse(typeof(EstadoExpediente), estado);
                    aux.Estado = est;
                    
                }

                    aux.caratula =  caratula;
                    encontre = true;

            }

            i++;

        }

        SobrescribirListaExpediente(lista);
        
    }

}
