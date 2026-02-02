// Questo è un commento

/*
Commento multilinea
*/
Console.WriteLine("Hello, World!");
//Console è una classe predefinita di c# che mette a disposizione diversi metodi
//WriteLine è il metodo della classe Console che stampa a monitor
//Generalmente ogni metodo ha le parentesi all' interno delle quali si specificano i parametri o gli argomento

Console.Write("Inserisci il tuo nome: ");


string nome=Console.ReadLine(); //Acquisire l'input dell' utente in una variabile

//stampo il nome acquisito
Console.WriteLine("Nome acquisito: "+nome+"!");

Console.Write("Inserisci il cognome: ");
string cognome=Console.ReadLine(); //Acquisire l'input dell' utente in una variabile
//stampa tramite interpolazione di stringa
Console.WriteLine($"Ciao, {nome} {cognome}!");