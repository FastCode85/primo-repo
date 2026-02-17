DateTime datetime=new DateTime(1990,1,1);
Console.WriteLine($"Datetime: {datetime}");
datetime=DateTime.Today;
Console.WriteLine($"Oggi è {datetime}");
Console.WriteLine($"Oggi è {datetime.ToShortDateString()}");
Console.WriteLine($"Oggi è {datetime.ToLongDateString()}");
Console.WriteLine($"Oggi è {datetime.ToString("dd/MM/yyyy")}");
string dateTimeString="2024-10-11";
DateTime dt=DateTime.Parse(dateTimeString);
bool result=DateTime.TryParse(dateTimeString, out DateTime s);
Console.WriteLine($"dt: {dt}");
if(result)
    Console.WriteLine($"s: {s}");
DateTimeOffset now=DateTimeOffset.UtcNow;
long timestamp=now.ToUnixTimeSeconds();
Console.WriteLine($"Il timestamp attuale è {timestamp}");
Console.WriteLine($"dt giorno: {dt}");
dt=dt.AddDays(1);
Console.WriteLine($"dt giorno dopo: {dt}");
Console.WriteLine($"dt giorno dopo: {dt:dddd}");
TimeSpan timeSpan=new TimeSpan(2,0,0,0);
DateTime traDueGiorni=DateTime.Today.Add(timeSpan);
Console.WriteLine($"Tra due giorni: {traDueGiorni}");

DateTime dataNascita=new DateTime(1985,3,20);
DateTime oggi=DateTime.Now;
TimeSpan diffTime=oggi.Subtract(dataNascita);
Console.WriteLine($"Giorni: {diffTime.Days} d {(diffTime.Days/365)}");

/*
programma che
- chiede all' utente di inserire il nome di un prodotto e la sua data di scadenza
- la data deve essere convertita in oggetto DateTime e devono essere gestiti eventuali errori di conversione
- aggiunge il prodotto ad un dizionario dove la chiave è il nome del prodotto e il valore la data di scadenza
- calcola quanti giorni mancano alla scadenza del prodotto
- stampa un indicazione a fianco al prodotto che indica se
- il prodotto è scaduto
- sta per scadere ( entro 3gg )
- è ancora utilizzabile
- stampa la data di scadenza in un formato leggibile (es. "31 dicembre 2024")

- il programma parte con tre prodotti di default
- il programma impedisce l'inserimento di prodotti con date di scadenza già passate
- visualizzare solo i prodotti che stanno per scadere entro 3 giorni
- implementazione di un menu che consente di visualizzare 
    tutti i prodotti 
    o solo quelli in scadenza
    o quelli scaduti

*/

Dictionary<string,DateTime> dizionarioProdotti=new Dictionary<string,DateTime>();
dizionarioProdotti.Add("p1",new DateTime(2026,02,17));
dizionarioProdotti.Add("p2",new DateTime(2022,02,10));
dizionarioProdotti.Add("p3",new DateTime(2027,01,18));

while(true)
{
    Console.WriteLine("Inserisci il nome di un prodotto o 0 per uscire");
    string nomeProdotto=Console.ReadLine();
    if(nomeProdotto=="0")
        break;
    bool dataValida=false;
    string dataScadenza=""; 
    DateTime dateTimeScadenza=DateTime.Now;
    while(!dataValida)
    {
        Console.WriteLine("Inserisci la data di scadenza");
        dataScadenza=Console.ReadLine();
        dataValida=DateTime.TryParse(dataScadenza, out dateTimeScadenza);
        if(!dataValida)
            Console.WriteLine("Impossibile convertire la data, riprova");
    }
    Console.WriteLine($"Nome prodotto: {nomeProdotto} data: {dateTimeScadenza.ToLongDateString()}");
    DateTime dateTimeNow=DateTime.Now;
    TimeSpan timeSpanDifferenza=dateTimeScadenza.Subtract(dateTimeNow);
    Console.WriteLine($"Giorni alla scadenza: {timeSpanDifferenza.Days}");
    if(timeSpanDifferenza.Days>0)
    {
        Console.WriteLine("Prodotto inserito nel dizionario");
        dizionarioProdotti.Add(nomeProdotto,dateTimeScadenza);
    }
    else
    {
        Console.WriteLine("Prodotto scaduto non inserito nel dizionario");
    }


    if(timeSpanDifferenza.Days<=3 && timeSpanDifferenza.Days>0)
        Console.WriteLine($"Il prodotto è in scadenza, differenza in giorni: {timeSpanDifferenza.Days}");
    else if(timeSpanDifferenza.Days<=0)
        Console.WriteLine($"Il prodotto è scaduto");
    else
        Console.WriteLine("Il prodotto è ancora utilizzabile");
}

string risposta="";
while(risposta!="0")
{
    Console.WriteLine("Inserisci 1 per vedere tutti i prodotti, 2 per quelli in scadenza, 3 per quelli scaduti, 0 per uscire");
    risposta=Console.ReadLine();
    if(risposta=="1")
    {
        Console.WriteLine("Stampa di tutti i prodotti");
        foreach(var kvp in dizionarioProdotti)
        {
            Console.WriteLine($"Nome prodotto: {kvp.Key} scadenza: {kvp.Value.ToLongDateString()}");
        }
    }
    else if(risposta=="2")
    {
        Console.WriteLine("Stampa prodotti in scadenza");
        foreach(var kvp in dizionarioProdotti)
        {
            DateTime dateTimeNow=DateTime.Now;
            TimeSpan timeSpanDifferenza=kvp.Value.Subtract(dateTimeNow);
            if(timeSpanDifferenza.Days<=3 && timeSpanDifferenza.Days>0)
                Console.WriteLine($"Nome prodotto: {kvp.Key} scadenza: {kvp.Value.ToLongDateString()}");
        }
        
    }
    else if(risposta=="3")
    {
        Console.WriteLine("Stampa prodotti scaduti");
        foreach(var kvp in dizionarioProdotti)
        {
            DateTime dateTimeNow=DateTime.Now;
            TimeSpan timeSpanDifferenza=kvp.Value.Subtract(dateTimeNow);
            if(timeSpanDifferenza.Days<=0)
                Console.WriteLine($"Nome prodotto: {kvp.Key} scadenza: {kvp.Value.ToLongDateString()}");
        }
    }
}

