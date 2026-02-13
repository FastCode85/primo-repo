Dictionary<int,string> dizionario=new Dictionary<int,string>()
{
    {1,"uno"},
    {2,"due"},
    {3,"tre"}
};

dizionario.Add(4,"quattro");
//dizionario.Add(1,"Uno aggiornato");
Console.WriteLine("Valore 1: ");
Console.WriteLine($"ContainsKey 1: {dizionario.ContainsKey(1)}");
Console.WriteLine($"ContainsValue due: {dizionario.ContainsValue("due")}");
bool tryGetBool=dizionario.TryGetValue(1, out string valore);
Console.WriteLine($"TryGetValue 1: {tryGetBool}");
dizionario[1]="uno modificato";
Console.WriteLine($"Valore con chiave 1 modificato: {dizionario[1]}");
dizionario.Remove(1);
Console.WriteLine("Eliminato il valore con chiave 1");
Console.WriteLine("Stampa dizionario");
foreach(var kvp in dizionario)
    Console.WriteLine($"Chiave: {kvp.Key} Valore: {kvp.Value}");
Dictionary<int,List<string>> dizionarioListe=new Dictionary<int,List<string>>()
{
    {1, new List<string> {"nome","prezzo"} },
    {2, new List<string> {"nome"} },
    {3, new List<string> {"nome","prezzo","quantità"}}
};
dizionarioListe[1].Add("quantità");
Console.WriteLine("Stampa dizionario liste");
foreach(var kvp in dizionarioListe)
    Console.WriteLine($"Chiave: {kvp.Key} Valore: {string.Join(",",kvp.Value)}");
 Dictionary<int, Dictionary<string,string>> dizionarioDizionari=new Dictionary<int, Dictionary<string,string>>()
 {
    { 1,new Dictionary<string,string> { {"nome","prodotto1"},{"prezzo","20"} }},
    { 2,new Dictionary<string,string> { {"nome","prodotto2"},{"prezzo","50"} }},
    { 3,new Dictionary<string,string> { {"nome","prodotto3"}, {"prezzo","100"}}}
 };

 dizionarioDizionari[1].Add("quantità","100");
 Console.WriteLine("Stampa dizionarioDizionary");
 foreach(var kvp in dizionarioDizionari)
{
     Console.WriteLine($"Stampa per chiave {kvp.Key}");
    foreach(var innerKvp in kvp.Value)
    {
        Console.WriteLine($"chiave: {innerKvp.Key} | valore: {innerKvp.Value}");
    }
}