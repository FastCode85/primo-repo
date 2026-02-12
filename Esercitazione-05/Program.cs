using System.Collections;
using System.Xml.Schema;

List<int> lista=new List<int>();

lista.AddRange(1,2,5,4,3);
Console.WriteLine(string.Join(",",lista));

Console.WriteLine(lista.Contains(2));
Console.WriteLine(lista.Contains(30));
Console.WriteLine($"Indice del numero 2: {lista.IndexOf(2)}");
Console.WriteLine($"Indice del numero 40: {lista.IndexOf(40)}");
lista.Sort();
Console.WriteLine($"Lista ordinata: {string.Join(",",lista)}");
int[] numeriArray=lista.ToArray();
Console.WriteLine($"Array ordinato: {string.Join(",",numeriArray)}");
List<int> lista2=numeriArray.ToList();
Console.WriteLine($"Lista2: {string.Join(",",lista2)}");
Console.WriteLine($"Rimozione del valore 4: {lista2.Remove(4)}\nNuova lista: {string.Join(",",lista2)}");
Console.WriteLine($"Rimozione del valore 40: {lista2.Remove(40)}\nNuova lista: {string.Join(",",lista2)}");
lista2.RemoveAt(0);
Console.WriteLine($"Rimozione dell' indice zero\nNuova lista: {string.Join(",",lista2)}");
lista2.Clear();
Console.WriteLine($"Pulizia completa della lista.\nNuova lista: {string.Join(",",lista2)}");
lista.AddRange(6,7,8,9,10);
Console.WriteLine($"Lista modificata con aggiunte: {string.Join(",",lista)}");
lista.RemoveRange(1,5);
Console.WriteLine($"Capacità della lista: {lista.Capacity}");
Console.WriteLine($"Count della lista: {lista.Count}");
lista.TrimExcess();
Console.WriteLine($"Capacità della lista dopo trimexcess: {lista.Capacity}");
Console.WriteLine($"Count della lista dopo trimexcess: {lista.Count}");