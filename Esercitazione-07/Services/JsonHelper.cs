using Newtonsoft.Json;

public static class JsonHelper
{
    public static void Salva(string path, object obj)
    {
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        File.WriteAllText(path, json);
        
    }
    public static T Leggi<T>(string path)
    // T è u n tipo di dato generico, che può essere qualsiasi tipo di dato (ad esempio int, string, classi personalizzate, ecc.)
    // in questo caso il metodo Leggi restituisce un oggetto di tipo T, che viene deserializzato dal file JSON
    {

        try
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default(T);  //ritorno null se è una classe o 0 se è un dato primitovo
        }


    }
}