/*

- crea un dizionario che associa un nome ad un numero di telefono con qualche dato di default
- permette all' utente di aggiungere un numero di telefono
- permette all'utente di modificare il numero di telefono associato ad un nome esistente
- permette all' utente di eliminare un nome e il suo numero di telefono dal dizionario
- nel caso di inserimento di un nome già esistente, il programma chiede se si vuole aggiornare il numero associato
- nel caso di modifica e cancellazione deve prima stampare la rubrica in modo da poter visualizzare i nomi e i numeri prima di effettuare l'operazione
- nel caso di eliminazione di un nome inesistente, stampare un messaggio d'errore
*/

Dictionary<string,string> dizionarioNumeri=new Dictionary<string,string>()
{
   {"marco","12345"}, 
   {"andrea","54321"},
   {"gianni","62254"},
};


while(true)
{
    Console.WriteLine("Premi 1 per inserire, 2 per modificare, 3 per eliminare, 4 per visualizzare, altro per uscire");
    string risp=Console.ReadLine().Trim();
    if(risp=="1")
    {
        Console.WriteLine("Inserisci un nome");
        string nomeRubrica=Console.ReadLine();
        Console.WriteLine("Inserisci un numero");
        string numeroRubrica=Console.ReadLine();
        dizionarioNumeri.Add(nomeRubrica, numeroRubrica);
    }
    else if(risp=="2")
    {
        Console.WriteLine("Stampa del dizionario");
        foreach(var kvp in dizionarioNumeri)
        {
            Console.WriteLine($"nome: {kvp.Key}\tnumero: {kvp.Value}");
        }
        Console.WriteLine("Quale elemento vuoi modificare?");
        string nomeDaModificare=Console.ReadLine().Trim();
        if(dizionarioNumeri.ContainsKey(nomeDaModificare))
        {
            Console.WriteLine($"inserisci il nuovo numero per {nomeDaModificare}");
            string nuovoNumero=Console.ReadLine().Trim();
            if(dizionarioNumeri.ContainsKey(nomeDaModificare))
            {
                dizionarioNumeri[nomeDaModificare]=nuovoNumero;
                Console.WriteLine("Valore modificato");
            }
        }
        else
        {
            Console.WriteLine($"Nome {nomeDaModificare} inesistente");
        }
    }
    else if(risp=="3")
    {
        Console.WriteLine("Stampa del dizionario");
        foreach(var kvp in dizionarioNumeri)
        {
            Console.WriteLine($"nome: {kvp.Key}\tnumero: {kvp.Value}");
        }
        Console.WriteLine("Quale nome vuoi eliminare?");
        string nomeDaEliminare=Console.ReadLine();
        bool risultato=dizionarioNumeri.Remove(nomeDaEliminare);
        if(risultato)
            Console.WriteLine("Nome rimosso");
        else
            Console.WriteLine("Nome non trovato");
    }
    else if(risp=="4")
    {
        Console.WriteLine("Stampa del dizionario");
        foreach(var kvp in dizionarioNumeri)
        {
            Console.WriteLine($"nome: {kvp.Key}\tnumero: {kvp.Value}");
        }
    }
    else
        break;

}
