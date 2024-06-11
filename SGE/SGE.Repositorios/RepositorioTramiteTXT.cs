namespace SGE.Repositorios;
using SGE.Aplicacion;

public class RepositorioTramiteTXT : ITramiteRepositorio
{

    readonly string _nombreArch = @"..\SGE.Repositorios\tramites.txt";

    public void AgregarTramite(Tramite tramite)
    {
        int id = RepositorioTramiteID.conseguirID();
        tramite.IDTramite = id;
        EscribirTramite(tramite);
    }

        private void EscribirTramite(Tramite tramite)
    {
        if(File.Exists(_nombreArch))
        {    
            using (var sw = new StreamWriter(_nombreArch, true))
            {
                sw.WriteLine($"{tramite.IDTramite} || {tramite.ExpedienteId} || {tramite.idUsuario} || {tramite.Etiqueta} || {tramite.descripcion} || {tramite.fechaYhoraCreacion} || {tramite.fechaYhoraModificacion} || {tramite.idUsuario}");
            }
        }    
    }

    public List<Tramite> ListarTramite()
    {
        var resultado = new List<Tramite>();
        using (var sr = new StreamReader(_nombreArch))
        {
            while(!sr.EndOfStream)
            {
                Tramite tramiteCopi = new Tramite();
                string st = sr.ReadLine() ?? "";
                string[]? tr = (st.Split(" || ")) ?? null;
                if(tr != null)
                {
                    tramiteCopi.IDTramite = int.Parse(tr[0]);
                    tramiteCopi.ExpedienteId = int.Parse(tr[1]);
                    tramiteCopi.idUsuario = int.Parse(tr[2]);
                    tramiteCopi.Etiqueta = (EtiquetaTramite) Enum.Parse(typeof(EtiquetaTramite), tr[3]);
                    tramiteCopi.descripcion = tr[4];
                    tramiteCopi.fechaYhoraCreacion = DateTime.Parse(tr[5]);
                    tramiteCopi.fechaYhoraModificacion = DateTime.Parse(tr[6]);
                    tramiteCopi.idUsuario = int.Parse(tr[7]);

                    resultado.Add(tramiteCopi);
                }
            }
        }
        return resultado;
    }

    public void EliminarTramite(int idtramite)
    {
        if(File.Exists(_nombreArch))
        {
            List<Tramite> listTramite = ListarTramite();
            Tramite tramite;
            int i = 0;

            if(File.Exists(_nombreArch))
            {   
                
                while(i <= listTramite.Count && i != -1)
                {
                    tramite = listTramite[i];
                    if(tramite.IDTramite == idtramite)
                    {
                        listTramite.Remove(tramite);
                        i = -1;
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            if(i != -1)
            {
                throw new RepositorioException("No existe el tramite en cuestion");
            }

            SobrescribirListaTramites(listTramite);
        }
    }

    public void EliminarCompleto(int idE)
    {
        List<Tramite> listaTramite = ListarTramite();
        List<Tramite> listaTramiteCopia = ListarTramite();
        bool listaModificada = false;
        
        foreach(Tramite t in listaTramite)
        {

            if(t.ExpedienteId == idE)
            {

                listaTramiteCopia.Remove(t);
                listaModificada = true;

            }

        }

        if(listaModificada)
        {
            SobrescribirListaTramites(listaTramiteCopia);
        }
    }

    public Tramite BuscarUltimo(int idE)
    {
        List<Tramite> listaPorExpedientes = ListarPorExpediente(idE);
        Tramite maxTramite = new Tramite();

        maxTramite.IDTramite = -1;
        foreach(Tramite tActual in listaPorExpedientes)
        {
            if(maxTramite.IDTramite < tActual.IDTramite)
            {
                maxTramite = tActual;
            }
        }
        return maxTramite;
    }

    public List<Tramite> ListarPorExpediente(int idE)
    {
        List<Tramite> lista = ListarTramite();
        List<Tramite> listaPorExpediente = new List<Tramite>();
        foreach(Tramite tActual in lista)
        {
            if(tActual.ExpedienteId == idE)
            {
                listaPorExpediente.Add(tActual);   
            }
        }
        return listaPorExpediente;
    }

    public void ModificarTramite(Tramite t, string etiqueta)
    {
        List<Tramite> listaTramites = ListarTramite();
        Tramite tramite;
        int i = 0;
        if(Enum.IsDefined(typeof(EtiquetaTramite), etiqueta))
        {    
            EtiquetaTramite etiq = (EtiquetaTramite) Enum.Parse(typeof(EtiquetaTramite), etiqueta);
            while(i <= listaTramites.Count && i != -1)
            {
                tramite = listaTramites[i];
                if(tramite.IDTramite == t.IDTramite)
                {
                    tramite.Etiqueta = etiq;
                    tramite.fechaYhoraModificacion = DateTime.Now;
                    i = -1;
                }
                else
                {
                    i++;
                }
            }
            SobrescribirListaTramites(listaTramites);
        }
    }

    private void SobrescribirListaTramites(List<Tramite> listTramite)
    {

        if(File.Exists(_nombreArch))
        {
            using (var sw = new StreamWriter(_nombreArch))
            {
                foreach(Tramite tramite in listTramite)
                {    
                    sw.WriteLine($"{tramite.IDTramite} || {tramite.ExpedienteId} || {tramite.idUsuario} || {tramite.Etiqueta} || {tramite.descripcion} || {tramite.fechaYhoraCreacion} || {tramite.fechaYhoraModificacion} || {tramite.idUsuario}");
                }
            }
        }
    }

    public Tramite BuscarTramite(int idTramite)
    {

        List<Tramite> listaTramites = ListarTramite();
        Tramite tAux = new Tramite();

        foreach(Tramite aux in listaTramites)
        {

            if(aux.IDTramite == idTramite)
            {

                return aux;

            }

        }

        throw new RepositorioException("El expediente buscado no existe.");

    }

    public void ImprimirPantallaPorEtiqueta(string etiqueta)
    {
        List<Tramite> listaTramites = ListarTramite();
        if(Enum.IsDefined(typeof(EtiquetaTramite), etiqueta))
        {
            EtiquetaTramite etiq = (EtiquetaTramite) Enum.Parse(typeof(EtiquetaTramite), etiqueta);
            foreach(Tramite tramite in listaTramites)
            {
                if(tramite.Etiqueta == etiq)
                {
                    Console.WriteLine(tramite);
                }
            }
        }

    }

}
