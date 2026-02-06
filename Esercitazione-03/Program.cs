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

Console.Write("Inserisci il tuo nome: ");
string nome1=Console.ReadLine();
Console.Write("Inserisci il tuo nome: ");
string nome2=Console.ReadLine();
Console.Write("Inserisci il tuo nome: ");
string nome3=Console.ReadLine();
Dictionary<int,string> dizionario3=new Dictionary<int,string>();
dizionario3.Add(1,nome1);
dizionario3.Add(2,nome2);
dizionario3.Add(3,nome3);

Console.WriteLine($"Nome1: {dizionario3[1]}, Nome2: {dizionario3[2]}, Nome3: {dizionario3[3]}");
Console.WriteLine("Quale nome vuoi stampare?");
int nomeDaStampare=int.Parse(Console.ReadLine());
Console.WriteLine($"nome scelto: {dizionario3[nomeDaStampare]}");