/**
chiede all' utente di lanciare un dado per 5 volte (premendo 1)
dopo ogni lancio chiede all utente se vuole tenere il risultato del lancio
aggiunge ad una lista i risultati dei lanci tenuti
stampa la lista deu risultati tenuti e la somma dei valori dei dadi
*/

int nVolte=0;
Random random=new Random();
List<int> listaLanci=new List<int>();
while (true)
{
    Console.WriteLine("Inserisci 1 per lanciare il dado");
    int valore=int.Parse(Console.ReadLine());
    
    if(valore==1)
    {
        nVolte++;
        int valoreRandom=random.Next(1,7);
        Console.WriteLine($"E' uscito il numero {valoreRandom}. Inserisci 1 per tenere il numero");
        int valoreTenere=int.Parse(Console.ReadLine());
        if(valoreTenere==1)
        {
            listaLanci.Add(valoreRandom);
        }
    }
    else
    {
        Console.WriteLine("Input non valido, riprova");
    }
    if(nVolte>=5)
        break;
}

int somma=0;
int counter=0;
foreach (var lancio in listaLanci)
{
    somma+=lancio;
    Console.WriteLine($"Valore numero {counter}: {lancio}");
}

Console.WriteLine($"La somma dei numeri tenuti è {somma}");