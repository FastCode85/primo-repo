Dictionary<int,string> dizionario=new Dictionary<int,string>//creo e inizializzo il dizionario
{
    {1,"Valore 1"},
    {2,"Valore 2"},
    {3,"Valore 3"}

};
Console.WriteLine($"Valore due del dizionario: {dizionario[2]}");//stampa il valore con chiave 2

Dictionary<string,int> dizionario2=new Dictionary<string,int>();
dizionario2.Add("chiave1",1);
dizionario2.Add("chiave2",2);
dizionario2.Add("chiave3",3);
Console.WriteLine($"Valore due del dizionario: {dizionario2["chiave2"]}");//stampa il valore con chiave 2