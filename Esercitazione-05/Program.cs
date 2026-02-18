/*
 Una funzione che unisce due liste di stringhe e restituisce una nuova lista con tutti gli elementi
 un'altra funzione che stampa la lista nuova
*/

List<string> lista1=new List<string> {"valore1","valore2","valore3"};
List<string> lista2=new List<string> {"valore4","valore5","valore6"};


StampaLista(ListaUnita(new List<string>(lista1),lista2));
StampaLista(lista1);
Console.WriteLine($"{StampaLista}");

List<string> ListaUnita(List<string> list1, List<string> list2)
{
    list1.AddRange(list2);
    return list1;
}

void StampaLista(List<string> lista)
{
    Console.WriteLine("Stampa della lista");
    foreach(string s in lista)
    {
        Console.WriteLine(s);
    }
}



