/*

- implementazione dell' esercitazione del modulo11 metodi dizionario con funzioni

*/

using System.Data.Common;




/*

in questa versione del programma, invece di associare un nome a un numero di telefono, associamo ad un id univoco
generato con il random un dizionario che contiene dizionari con il nome, il nomero di telefono, l' indirizzo, 
lo stato attivo disattivo. Deve esserci la possibilità di visualizzare la rubrica intera o solo i numeri attivi

*/
Dictionary<int,Dictionary<string,string>> rubricaAvanzata=new Dictionary<int,Dictionary<string,string>>
{
    {1,new Dictionary<string,string> { {"nome","Nome 1"},{"numero","1234567890"},{"indirizzo","Via Roma 1"},{"stato","true"}} },
    {2,new Dictionary<string,string> { {"nome","Nome 2"},{"numero","0987654321"},{"indirizzo","Via Milano 2"},{"stato","true"}} },
    {3,new Dictionary<string,string> { {"nome","Nome 3"},{"numero","1597531221"},{"indirizzo","Via Napoli 3"},{"stato","false"}} }
};

// gestione generale del programma
Random random=new Random();
bool continua = true; // variabile per controllare se continuare o meno il programma


while (continua)
{

    StampaMenu();
    string scelta=LeggiInput("");
    switch (scelta)
    {
        case "1":
            // codice per aggiungere un contatto
            AggiungiContatto(rubricaAvanzata,random);
            break;
        case "2":
            // codice per modificare un contatto
            ModificaContatto(rubricaAvanzata);
            break;
        case "3":
            // codice per eliminare un contatto
            EliminaContatto(rubricaAvanzata);
            break;
        case "4":
            // codice per visualizzare la rubrica
            StampaRubricaAvanzata(rubricaAvanzata,true);
            break;
        case "5":
            continua = false; // esce dal programma
            break;
        default:
            Console.WriteLine("Input non valido");
            break;
    }
}

void EliminaContatto(Dictionary<int,Dictionary<string,string>> rubricaAvanzata)
{
    StampaRubricaAvanzata(rubricaAvanzata,false);
    string input=LeggiInput("Inserisci l'ID da rimuovere:");
    bool isNumeric=int.TryParse(input, out int idDaRimuovere);
    if(isNumeric)
    {
        if (rubricaAvanzata.ContainsKey(idDaRimuovere))
        {
            rubricaAvanzata.Remove(idDaRimuovere);
            Console.WriteLine("Contatto eliminato.");
        }
        else
        {
            Console.WriteLine("Contatto non trovato.");
        }
    }
    else
        Console.WriteLine("L'ID inserito non è un numero");
}

void StampaRubrica(Dictionary<string,string> rubrica)
{
    Console.WriteLine("Rubrica:");

    foreach (var kvp in rubrica)
    {
        Console.WriteLine($"Nome: {kvp.Key}\tNumero: {kvp.Value}");
    }
}
void ModificaContatto(Dictionary<int,Dictionary<string,string>> rubricaAvanzata)
{
    StampaRubricaAvanzata(rubricaAvanzata,false);
    string input=LeggiInput("Inserisci l'ID del contatto da modificare:");
    bool isNumero=int.TryParse(input, out int idInserito);
    if(isNumero)
    {
        if (rubricaAvanzata.ContainsKey(idInserito))
        {

            string nome=LeggiInput("Inserisci il nome del contatto:");
            string numero = LeggiInput("Inserisci il numero di telefono del contatto:");
            string indirizzo=LeggiInput("Inserisci l'indirizzo del contatto:");
            string stato=LeggiStato();
            Dictionary<string,string> singoloContatto=rubricaAvanzata[idInserito];
            singoloContatto["nome"] = nome;
            singoloContatto["numero"] = numero;
            singoloContatto["indirizzo"] = indirizzo;
            singoloContatto["stato"] = stato;
            Console.WriteLine($"Contatto modificato. nome: {singoloContatto["nome"]} numero: {singoloContatto["numero"]}");
        }
        else
        {
            Console.WriteLine($"ID non trovato. ID: {idInserito}");
        }
    }
    else
        Console.WriteLine("L'ID inserito non è numerico");
}

string LeggiInput(string messaggio)
{
    Console.WriteLine(messaggio);
    string input = Console.ReadLine().Trim();
    //Console.Clear();
    return input;
}

string LeggiStato()
{
    string input=LeggiInput("Inserisci lo stato del contatto (attivo,non attivo):");
    Console.WriteLine($"stampa stato {input} stato valido: {StatoValido(input)}");
    while(!StatoValido(input))
    {
    Console.WriteLine($"2stampa stato {input} stato valido: {StatoValido(input)}");
    input=LeggiInput($"stato non valido, Inserisci lo stato del contatto (attivo,non attivo): {input}");
    }
    return input;
}

void AggiungiContatto(Dictionary<int,Dictionary<string,string>> rubricaAvanzata,Random random)
{
    int randomId=random.Next(1,1000);
    while(rubricaAvanzata.ContainsKey(randomId))
        randomId=random.Next(1,1000);
    string nome=LeggiInput("Inserisci il nome del contatto:");
    string numero = LeggiInput("Inserisci il numero di telefono del contatto:");
    string indirizzo=LeggiInput("Inserisci l'indirizzo del contatto:");
    string stato=LeggiStato();

    Dictionary<string,string> dettagli=new Dictionary<string,string>();
    dettagli.Add("nome",nome);
    dettagli.Add("numero",numero);
    dettagli.Add("indirizzo",indirizzo);
    dettagli.Add("stato",stato);
    rubricaAvanzata.Add(randomId,dettagli);
}

bool StatoValido(string stato)
{
    if(stato=="true" || stato=="false")
        return true;
    else
        return false;
}

bool StatoAttivo(string stato)
{
    bool parseOk=bool.TryParse(stato, out bool value);
    if(parseOk)
        return value;
    else 
        return false;
}

void StampaMenu()
{
    Console.WriteLine("Scegli un'opzione:");
    Console.WriteLine("1. Aggiungi un contatto");
    Console.WriteLine("2. Modifica un contatto");
    Console.WriteLine("3. Elimina un contatto");
    Console.WriteLine("4. Visualizza la rubrica (tutti)");
    Console.WriteLine("5. Esci");
}

void StampaRubricaAvanzata(Dictionary<int,Dictionary<string,string>> rubricaAvanzata, bool soloAttivi)
{
    Console.WriteLine($"Stampa rubrica avanzata, solo numeri attivi={soloAttivi}");
    foreach(var kvp in rubricaAvanzata)
    {
        bool stampa=false;
        if(!soloAttivi)
            stampa=true;
        else if(StatoAttivo(kvp.Value["stato"]))
            stampa=true;
        if(stampa)
            Console.WriteLine($"ID: {kvp.Key,-5} Nome: {kvp.Value["nome"],-20} Numero: {kvp.Value["numero"],20} Indirizzo: {kvp.Value["indirizzo"],20} Stato: {kvp.Value["stato"],10}");
    }
}