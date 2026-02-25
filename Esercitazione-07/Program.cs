using Newtonsoft.Json;

string path=@"test.json";
string json=File.ReadAllText(path);
var partecipante=JsonConvert.DeserializeObject<dynamic>(json);

Console.WriteLine($"Nome: {partecipante.nome}");
Console.WriteLine($"Età {partecipante.eta}");
Console.WriteLine($"Presente: {partecipante.presente}");


var partecipante2=new
{
    name="Partecipante 2",
    eta=50,
    presente=true
};

string json2=JsonConvert.SerializeObject(partecipante2,Formatting.Indented);
Console.WriteLine(json2);

string path3=@"test3.json";
string json3=File.ReadAllText(path3);
var partecipante3=JsonConvert.DeserializeObject<dynamic>(json3);

Console.WriteLine($"Nome: {partecipante3.nome}");
Console.WriteLine($"Età {partecipante3.eta}");
Console.WriteLine($"Presente: {partecipante3.presente}");
foreach(var interesse in partecipante3.interessi)
{
    Console.WriteLine($"- {interesse}");
}
Console.WriteLine(partecipante3.interessi[1]);

var partecipante4=new
{
    name="Partecipante 4",
    eta=50,
    presente=true,
    interessi=new List<string> {"programmazione","musica","sport"}
};

string serializzato=JsonConvert.SerializeObject(partecipante4,Formatting.Indented);
Console.WriteLine($"Stampa oggetto serializzato\n{serializzato}");

public class Partecipante
{
    public string nome { get; set; }
    public int eta { get; set; }
    public bool presente { get; set; }
}