int[] arrayNumeri= {5,6,3,5,8};  //dichiarazione e inizializzazione
Console.WriteLine($"Ecco il primo numero dell'array: {arrayNumeri[0]}");  //stampa del primo numero

List<int> listaNumeri=new List<int> {10,20,30};
Console.WriteLine($"valore zero della lista: {listaNumeri[0]}");

List<string> listaStringhe=new List<string> {"Nome1","Nome2","Nome3"}; //crea una lista di stringhe
listaStringhe.Add("Nome4"); //aggiungo un nome alla lista
Console.WriteLine($"Stampa ultimo nome: {listaStringhe[3]}");//stampa ultimo nome

Dictionary<int,string> dizionario=new Dictionary<int,string>//creo e inizializzo il dizionario
{
    {1,"Valore 1"},
    {2,"Valore 2"},
    {3,"Valore 3"}

};

Console.WriteLine($"Valore due del dizionario: {dizionario[2]}");//stampa il valore con chiave 2