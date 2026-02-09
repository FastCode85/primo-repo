for(int i = 0; i <= 10; i++)
{
    Console.WriteLine($"numero {i}");
}

List<string> nomi=new List<string> {"Nome1","Nome2","Nome3"};
foreach(var variabile in nomi)
{   
    Console.WriteLine(variabile);

}

Dictionary<int,string> dizionario=new Dictionary<int,string>
{
    {1,"uno"},
    {2,"due"},
    {3,"tre"}
};

foreach(var kvp in dizionario)
{
    Console.WriteLine($"Chiave: {kvp.Key}, Valore: {kvp.Value}");
}

int n=1;
while (n <= 10)
{
    Console.WriteLine($"Numero {n}");
    n++;
}

int k=1;
do
{
    Console.WriteLine($"Numero {k}");
    k++;
} while (k <= 10);

for(int z = 0; z < 100; z++)
{
    Console.WriteLine($"Ciclo per numero: {z}");
    if(z%3==0 && z%5==0)
        Console.WriteLine("Fizzbuzz");
    else if(z%3==0 && z%5!=0)
        Console.WriteLine("Fizz");
    else if(z%3!=0 && z%5==0)
        Console.WriteLine("Buzz");
    else
        Console.WriteLine("Numero non divisibile per 3 e per 5");
}

int[] arrayDiNumeri={345,3563,34534};
foreach(var variabile in arrayDiNumeri)
{
    Console.WriteLine($"Ciclo per numero {variabile}");
    if(variabile%3==0 && variabile%5==0)
        Console.WriteLine("Fizzbuzz");
    else if(variabile%3==0 && variabile%5!=0)
        Console.WriteLine("Fizz");
    else if(variabile%3!=0 && variabile%5==0)
        Console.WriteLine("Buzz");
    else
        Console.WriteLine("Numero non divisibile per 3 e per 5");
}

for(int i = 0; i < 5; i++)
{
    Console.WriteLine("Inserisci un nuovo numero");
    string numeroStringa = Console.ReadLine();
    if(int.TryParse(numeroStringa, out int risultato))
        Console.WriteLine($"Il numero inserito è {risultato}");
    else
        Console.WriteLine("Il valore inserito non è un numero");
}

List<string> listaNumeriStr=new List<string>(); //dichiarazione di una lista
Console.WriteLine("Inserimento di una lista di numeri. Inserisci un numero");
string stringaInput=Console.ReadLine(); //lettura del valore da console
while(stringaInput!="stop")//ciclo di inserimento numeri
{
    listaNumeriStr.Add(stringaInput);//aggiunge la stringa alla lista
    Console.WriteLine("Inserisci un numero");
    stringaInput=Console.ReadLine();
}

List<int> listaNumeriInteri=new List<int>();
foreach(var variabile in listaNumeriStr)
{
    listaNumeriInteri.Add(int.Parse(variabile));//aggiunge variabile alla lista di interi
    
    Console.WriteLine($"Stampo numero {variabile}");

}