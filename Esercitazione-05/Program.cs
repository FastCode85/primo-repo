/*

- implementazione dell' esercitazione del modulo11 metodi dizionario con funzioni

*/

Dictionary<string, string> rubrica = new Dictionary<string, string>()
{
    { "Nome 1", "1234567890" },
    { "Nome 2", "0987654321" },
    { "Nome 3", "5555555555" }
};

// gestione generale del programma
bool continua = true; // variabile per controllare se continuare o meno il programma


while (continua)
{

    StampaMenu();
    string scelta = Console.ReadLine().Trim();

    switch (scelta)
    {
        case "1":
            // codice per aggiungere un contatto
            AggiungiContatto();
            break;
        case "2":
            // codice per modificare un contatto
            ModificaContatto(rubrica);
            break;
        case "3":
            // codice per eliminare un contatto
            EliminaContatto(rubrica);
            break;
        case "4":
            // codice per visualizzare la rubrica
            StampaRubrica(rubrica);
            break;
        case "5":
            continua = false; // esce dal programma
            break;
        default:
            Console.WriteLine("Scelta non valida. Riprova.");
            break;
    }
}

void EliminaContatto(Dictionary<string,string> rubrica)
{
    StampaRubrica(rubrica);
    Console.WriteLine("Inserisci il nome del contatto da eliminare:");
    string nomeElimina = Console.ReadLine().Trim();
    if (rubrica.ContainsKey(nomeElimina))
    {
        rubrica.Remove(nomeElimina);
        Console.WriteLine("Contatto eliminato.");
    }
    else
    {
        Console.WriteLine("Contatto non trovato.");
    }
}
void StampaRubrica(Dictionary<string,string> rubrica)
{
    Console.WriteLine("Rubrica:");
    foreach (var kvp in rubrica)
    {
        Console.WriteLine($"Nome: {kvp.Key}\tNumero: {kvp.Value}");
    }
}
void ModificaContatto(Dictionary<string,string> rubrica)
{
    StampaRubrica(rubrica);
    Console.WriteLine("Inserisci il nome del contatto da modificare:");
    string nomeModifica = Console.ReadLine().Trim();

    if (rubrica.ContainsKey(nomeModifica))
    {
        Console.WriteLine("Inserisci il nuovo numero di telefono:");
        string nuovoNumero = Console.ReadLine().Trim();
        rubrica[nomeModifica] = nuovoNumero;
        Console.WriteLine("Contatto modificato.");
    }
    else
    {
        Console.WriteLine("Contatto non trovato.");
    }
}
void AggiungiContatto()
{
    Console.WriteLine("Inserisci il nome del contatto:");
    string nome = Console.ReadLine().Trim();

    Console.WriteLine("Inserisci il numero di telefono del contatto:");
    string numero = Console.ReadLine().Trim();

    if (rubrica.ContainsKey(nome))
    {
        Console.WriteLine("Il contatto esiste già. Vuoi aggiornare il numero di telefono? (s/n)");

        string risposta = Console.ReadLine().Trim().ToLower();
        if (risposta == "s")
        {
            rubrica[nome] = numero; // aggiorna il numero di telefono del contatto esistente
            Console.WriteLine("Numero di telefono aggiornato.");
        }
    }
    else
    {
        rubrica.Add(nome, numero);
        Console.WriteLine("Contatto aggiunto.");
    }
}

void StampaMenu()
{
    Console.WriteLine("Scegli un'opzione:");
    Console.WriteLine("1. Aggiungi un contatto");
    Console.WriteLine("2. Modifica un contatto");
    Console.WriteLine("3. Elimina un contatto");
    Console.WriteLine("4. Visualizza la rubrica");
    Console.WriteLine("5. Esci");
}