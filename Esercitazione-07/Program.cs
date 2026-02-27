using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    

    Console.WriteLine("Premi 1 per aggiungere un partecipante\nPremi 2 per vedere i partecipanti\nPremi 3 per modificare un partecipante\nPremi 0 per uscire");
    string risposta=Console.ReadLine();
    if(risposta=="0")
        break;
    else if(risposta=="1")
    {
        var nuovoPartecipante=LeggiPartecipante();
        Console.WriteLine($"TIPO DI DATO INTERESSI: {nuovoPartecipante.interessi.GetType()}");
        ScriviPartecipante(nuovoPartecipante);
        //Console.WriteLine($"nuovoPartecipante nome {nuovoPartecipante.nome}");
    }
    else if(risposta=="2")
    {
        StampaPartecipanti();
    }
    else if(risposta=="3")
    {
        StampaPartecipanti();
        Console.WriteLine("Inserisci il numero del partecipante da modificare");
        int numero=int.Parse(Console.ReadLine());
        ModificaPartecipante(numero);
    }

}

void ModificaPartecipante(int numeroPartecipante)
{
    
    List<dynamic> listaPartecipanti=new List<dynamic>();
    string contenutoFile=File.ReadAllText("partecipanti.json");
    listaPartecipanti=JsonConvert.DeserializeObject<List<dynamic>>(contenutoFile);  
    string scelta="";
    while(true)
    {
        Console.WriteLine("Inserisci 1 per modificare il nome\nInserisci 2 per modificare l'età\nnInserisci 3 per modificare la presenza\nnInserisci 4 per modificare gli interessi\nAltro per uscire");
        scelta=Console.ReadLine();
        if(scelta=="1")
        {
            Console.WriteLine("Inserisci il nuovo nome");
            string risposta=Console.ReadLine();
            listaPartecipanti[numeroPartecipante-1].nome=risposta;
        }
        else if(scelta=="2")
        {
            Console.WriteLine("Inserisci la nuova età");
            string risposta=Console.ReadLine();
            listaPartecipanti[numeroPartecipante-1].eta=int.Parse(risposta);
        }
        else if(scelta=="3")
        {
            Console.WriteLine("Inserisci la nuova presenza (true/false)");
            string risposta=Console.ReadLine();
            listaPartecipanti[numeroPartecipante-1].presente=bool.Parse(risposta);
        }
        else if(scelta=="4")
        {
            Console.WriteLine("Inserisci i nuovi interessi separati da virgola");
            string risposta=Console.ReadLine();
            List<string> interessiModificati=new List<string>();
            string[] interessiArray=risposta.Split(",");
            foreach(string s in interessiArray)
            {
                interessiModificati.Add(s.Trim());
            }
            
            //Quando assegnamo un array ad un JsonObject dobbiamo per forza fare il cast da array a JArray
            listaPartecipanti[numeroPartecipante-1].interessi=JArray.FromObject(interessiModificati);
            Console.WriteLine($"nome: {listaPartecipanti[numeroPartecipante-1].nome.GetType()}\neta: {listaPartecipanti[numeroPartecipante-1].eta.GetType()}\npresente: {listaPartecipanti[numeroPartecipante-1].presente.GetType()}\n interessi: {listaPartecipanti[numeroPartecipante-1].interessi.GetType()}");
        }
        else
            break;
        string serialized=JsonConvert.SerializeObject(listaPartecipanti,Formatting.Indented);
        if(File.Exists("partecipanti.json"))
        {
            File.WriteAllText("partecipanti.json",serialized);
            Console.WriteLine("Modifica scritta con successo");
        }
        else
            Console.WriteLine("impossibile aggiornare partecipanti.json, il file non esiste");

    }
}
void StampaPartecipanti()
{
    string contenutoFile=File.ReadAllText("partecipanti.json");
    var listaPartecipanti=JsonConvert.DeserializeObject<dynamic>(contenutoFile);
    Console.WriteLine("Stampa lista dei partecipanti");
    int index=1;
    foreach(var p in listaPartecipanti)
    {
        //Console.WriteLine($"aa {listaPartecipanti}");
        Console.WriteLine($"{index}:\t{p.nome}\t{p.eta}\t{p.presente}\t{string.Join(",",p.interessi)}");
        index++;
    }
}

void ScriviPartecipante(dynamic partecipante)
{
    string contenutoFile=File.ReadAllText("partecipanti.json");
    var listaPartecipanti=JsonConvert.DeserializeObject<dynamic>(contenutoFile);
    Console.WriteLine($"ScriviPartecipante contenuto file\n{contenutoFile}");
    Console.WriteLine("Stampa partecipanti");
    
    List<dynamic> lista=new List<dynamic>();
    foreach(var p in listaPartecipanti)
    {
        //Console.WriteLine($"aa {listaPartecipanti}");
        //Console.WriteLine($"{p.nome} {p.eta} {p.presenza}");
        lista.Add(p);
        
    }
    lista.Add(partecipante);
    int lastId=partecipante.id;
    string serialized=JsonConvert.SerializeObject(lista,Formatting.Indented);
    Console.WriteLine($"Stampa stringa json di tutti i partecipanti\n{serialized}");
    if(File.Exists("partecipanti.json"))
    {
        File.WriteAllText("partecipanti.json",serialized);
        ScriviId(lastId);
        Console.WriteLine("File partecipanti.json aggiornato con successo");
    }
    else
    {
        Console.WriteLine("impossibile aggiornare partecipanti.json, il file non esiste");
    }

}

int LeggiLastId()
{
    string testoFile=File.ReadAllText("lastId.json");
    var jsonVar=JsonConvert.DeserializeObject<dynamic>(testoFile);
    Console.WriteLine($"Id letto da file: {jsonVar.id}");
    return jsonVar.id;
}

void ScriviId(int nuovoId)
{
    var v=new
    {
        id=nuovoId
    };
    
    string s=JsonConvert.SerializeObject(v,Formatting.Indented);
    File.WriteAllText("lastId.json",s);
}

dynamic LeggiPartecipante()
{
    Console.WriteLine("Inserisci il nome");
    string nome=Console.ReadLine();

    Console.WriteLine("Inserisci l'età");
    string eta=Console.ReadLine();

    Console.WriteLine("Inserisci la presenza (true/false)");
    bool presenzaResult=false;
    bool presenza=false;
    while(!presenzaResult)
    {
        presenzaResult=bool.TryParse(Console.ReadLine(), out bool p);
        if(!presenzaResult)
            Console.WriteLine("Attenzione, presenza inserita non valida");
        presenza=p;
    }

    Console.WriteLine("Inserisci gli interessi separati da virgola");
    string interessi=Console.ReadLine();
    string[] interessiArray=interessi.Split(",");
    for(int i=0;i<interessiArray.Length;i++)
        interessiArray[i]=interessiArray[i].Trim();

    return new
    {
        id=LeggiLastId()+1,
        nome=nome,
        eta=eta,
        presente=presenza,
        interessi=interessiArray
    };

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

