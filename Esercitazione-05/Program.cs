/*

Creare una funzione tipo String che si comporta come un ReadLine avanzato che implementa la validazione dell'input:
- trim: rimuove gli spazi all'inizio e alla fine
- IsNullOrEmpty: verifica se la stringa è null o vuota
- ToLower: converte la stringa in minuscolo

*/

Console.WriteLine(ReadLineAvanzato());

string ReadLineAvanzato()
{
    bool valido=false;
    string s="";
    while(!valido)
    {
        Console.WriteLine("Inserisci una frase");
        s=Console.ReadLine();
        s=s.Trim();
        Console.WriteLine(s);
        valido=string.IsNullOrEmpty(s);
        if(valido)
            Console.WriteLine("La stringa è null o vuota");
    }
    return s.ToLower();
}


