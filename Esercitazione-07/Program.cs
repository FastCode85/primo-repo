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

/*

Il file partecipante.json avrà questa struttura
{
    "id":0,
    "nome":"Partecipante 1",
    "eta"=30,
    presente=true,
    "interessi":["programmazione","musica","sport"]
};
il programma deve:
- chiedere all' utente di inserire anche gli interessi del partecipante, che possono essere
inseriti come una stringa separata da virgole, ad esempio "programmazione, musica, sport"
e poi convertire questa stringa in una lista di stringhe da salvare nel file json.
- visualizzare i dati del partecipante in modo ordinato in forma tabellare, ad esempio con una tabella ascii
o con un formato leggibile.
- permettere all'utente di modificare i dati del partecipante dopo averli inseriti, ad esempio chiedendo
se vuole modificare il nome,l'età,la presenza o gli interessi.
- i parteipanti devono essere aggiunti ad un unico file partecipanti.json che contiene una lista di partecipanti,
in questo modo possiamo tenere traccia di tutti i partecipanti creati

*/
while(true)
{
    
    int lastId=LeggiUltimoId("lastId.json");
    Console.WriteLine($"Letto file json. lastID: {lastId}");
    Console.WriteLine("Inserisci un nuovo partecipante, oppure 0 per uscire");
    string risposta=Console.ReadLine();
    if(risposta=="0")
        break;
    else
    {
        lastId++;
        ScriviPartecipante(risposta,lastId);
        ScriviLastId(lastId);
        
    }

}

string CreaPartecipanteFileName(int id)
{
    return $"{id}.json";
}

void ScriviLastId(int newId)
{
    var jsonId=new
    {
        id=newId
    };
    string serialized=JsonConvert.SerializeObject(jsonId);
    File.WriteAllText("lastId.json",serialized);
}

void ScriviPartecipante(string nomePartecipante, int id)
{
    var singoloPartecipante=new
    {
        nome=nomePartecipante,
        id=id
    };

    string serialized=JsonConvert.SerializeObject(singoloPartecipante,Formatting.Indented);
    string partecipanteFileName=CreaPartecipanteFileName(id);
    if(!File.Exists(partecipanteFileName))
    {
        File.WriteAllText(partecipanteFileName,serialized);
        Console.WriteLine($"File {partecipanteFileName} scritto.");
    }
    else
        Console.WriteLine($"Errore, il file {partecipanteFileName} esiste già");


    
    Console.WriteLine($"singoloPartecipante serialized{serialized}");

}


int LeggiUltimoId(string path)
{
    string contenutoFile=File.ReadAllText(path);
    var contenutoJson=JsonConvert.DeserializeObject<dynamic>(contenutoFile);
    Console.WriteLine($"VALORE LeggiUltimoId: {contenutoFile}");
    return contenutoJson.id;
}

public class Partecipante
{
    public string nome { get; set; }
    public int eta { get; set; }
    public bool presente { get; set; }
}

public class Main
{

}


/*

Programma che usa un file json come metodo di persistenza dell'id dell' ultimo partecipante creato, 
in modo da poterlo incrementare ad ogni creazione di un nuovo partecipante.
Creare un file chiamato lastId.json con il seguente contenuto
{
    "lastId":0
}
dopodiché l'applicazione
leggerà il file json
deserializzerà il contenuto in un oggetto
incrementerà il valore di lastId ogni volta che viene creato un nuovo partecipante
serializzerà l'oggetto aggiornato
lo scriverà nuovamente su file

Possiamo lavorare con una struttura semplice fatta con un 
dizionario int string, dove la chiave è lastId e il valore è il nome del partecipante

*/

