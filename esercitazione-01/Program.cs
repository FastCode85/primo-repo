// Questo è un commento

/*
Commento multilinea
*/

//Console è una classe predefinita di c# che mette a disposizione diversi metodi
//WriteLine è il metodo della classe Console che stampa a monitor
//Generalmente ogni metodo ha le parentesi all' interno delle quali si specificano i parametri o gli argomento

Console.Write("Inserisci il tuo nome: "); //stampa per input
string nome=Console.ReadLine(); //Acquisire l'input dell' utente in una variabile nome


Console.Write("Inserisci il cognome: "); //stampa per input
string cognome=Console.ReadLine(); //Acquisire l'input dell' utente in una variabile cognome

//stampa tramite interpolazione di stringa
Console.WriteLine($"Ciao, {nome} {cognome}, benvenuto!");