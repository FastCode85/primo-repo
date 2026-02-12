/*
Programma che:
- chiede all' utente di inserire nomi ad una lista finché non viene inserito 0
- viene estratto un nome casuale e stampato a video
- viene chiesto all' utente se vuole estrarre un altro nome casuale, se l'utente risponde "y" viene estratto e stampato 
  un altro nome casuale, altrimenti il programma termina
- il nome estratto deve essere rimosso dalla lista per evitare che venga estratto di nuovo
*/


List<string> listaNomi=new List<string>();
string risp="1";
//ciclo per l'inserimento dei nomi
while(risp!="0")
{
    Console.WriteLine("Inserisci un nome o premi 0 per uscire");
    risp=Console.ReadLine();
    if(risp!="0")
    {
        listaNomi.Add(risp);
    }
}

Random random=new Random();
risp="y";
while (risp == "y" && listaNomi.Count>0)
{
    int numeroRandom=random.Next(0,listaNomi.Count);
    Console.WriteLine($"Estratto un nome casuale: {listaNomi[numeroRandom]}. Premi y per estrarre un altro nome.");
    listaNomi.RemoveAt(numeroRandom);
    risp=Console.ReadLine();

    
}
