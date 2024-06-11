namespace SGE.Repositorios;
using SGE.Aplicacion;

public class RepositorioTramiteID
{
    public static int conseguirID()
    {
        string nombreArch = @"..\SGE.Repositorios\TramiteID.txt";

        int id;
        using (var sr = new StreamReader(nombreArch))
        {   
            id = int.Parse(sr.ReadLine() ?? "");
        }
        
        id++;
        
        using (var sw = new StreamWriter(nombreArch))
        {
            sw.WriteLine(id);
        }

        return id;

    }

}