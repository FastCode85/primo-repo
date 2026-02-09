Random random=new Random();  //creazione di un oggetto random
int var=1;
int nVolte=0;
List<int> listaNumeri=new List<int>();
while(var==1)
{
    if(nVolte>=5)
        break;
    int valore=random.Next(1,7);
    if(valore>=5)
        listaNumeri.Add(valore);
    Console.WriteLine($"Il lancio ha dato risultato {valore}. Premi 1 per rilanciare");
    var=int.Parse(Console.ReadLine());
    nVolte++;

}

foreach(var variabile in listaNumeri)
{
    Console.WriteLine($"Stampa di un numero {variabile}");
}
