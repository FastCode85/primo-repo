using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

int[] mioArray={5,3,4,6,6};
int[] altroArray=new int[mioArray.Length];

Array.Copy(mioArray,altroArray,mioArray.Length);
Console.WriteLine($"Altro array: {altroArray}");
Console.WriteLine(string.Join(",",altroArray));

Array.Clear(altroArray,0,altroArray.Length);
Console.WriteLine(string.Join(",",altroArray));

Array.Copy(mioArray,altroArray,mioArray.Length);
Array.Reverse(altroArray);
Console.WriteLine(string.Join(",",altroArray));

Array.Copy(mioArray,altroArray,mioArray.Length);
Array.Sort(altroArray);
Console.WriteLine(string.Join(",",altroArray));

Array.Copy(mioArray,altroArray,mioArray.Length);
Array.Sort(altroArray);
Array.Reverse(altroArray);
Console.WriteLine(string.Join(",",altroArray));

int index=Array.IndexOf(mioArray,3);
Console.WriteLine($"Indice: {index}");

/*
Programma che
- crea un array di 5 elementi  numerici vuoti inizializzati a 0
- chiede all utente di lanciare un dado premendo 1
- Ad ogni lanco chiede se vuole tenere il risultato del lancio ( 1 si, 0 no )
- Se si aggiunge il risultato del lancio all' array, fino alla capienza dell' array
- ordina i lanci in modo decrescente 
- copia i lanci con valore maggiore o uguale a 5 in un nuovo array
*/

Random random=new Random();
int[] listaInteri=new int[5];
int listaInteriIndex=0;

while(true)
{
    if(listaInteriIndex<listaInteri.Length)
    {
        Console.WriteLine("Premi 1 per lanciare un dado, un altro carattere per uscire");
        string s=Console.ReadLine();
        if(s!="1")
            break;
        else
        {
            int valoreRandom=random.Next(1,7);
            Console.WriteLine($"Dado lanciato, risultato: {valoreRandom}. Premi 1 per tenere il lancio");
            string tenereLancio=Console.ReadLine();
            if(tenereLancio=="1")
            {
                listaInteri[listaInteriIndex]=valoreRandom;
                listaInteriIndex++;
            }
        }
    }
    else
    {
        break;
    }
}

int[] copiaArray=new int[listaInteri.Length];
int[] nuovoArray=new int[listaInteri.Length];
Array.Copy(listaInteri,copiaArray,listaInteri.Length);
Array.Clear(nuovoArray);
int indexNuovoArray=0;
foreach(int n in listaInteri)
{
    if(n>=5)
    {
        nuovoArray[indexNuovoArray]=n;
        indexNuovoArray++;
    }
}

Console.WriteLine($"Lista interi originale: {string.Join(",",listaInteri)}");
Array.Sort(copiaArray);
Array.Reverse(copiaArray);
Console.WriteLine($"Lista interi decrescente: {string.Join(",",copiaArray)}");
Console.WriteLine($"Lanci copiati: {string.Join(",",nuovoArray)}");



