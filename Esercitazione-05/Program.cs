/*
Legge un elenci di frasi da una lista, per ognuna genera uno slug (versione della frase 
adatta ad essere usata in un url) e le stampa. Regole dello slug
- deve essere tutto in minuscolo
- sostituire gli spazi con i trattini
- deve rimuovere la punteggiatura
- deve rimuovere eventuali trattini doppi
*/

List<string> listaFrasi=new List<string>();
List<string> listaFrasiModificate=new List<string>();
char[] punteggiatura={',','.',';',':','!','?'};
listaFrasi.Add("Questa è--, -- una frase..");
listaFrasi.Add("Seconda frase, per l'esercitazione");

int index=0;
foreach(string frase in listaFrasi)
{
    string fraseModificata=frase.ToLower();
    Console.WriteLine($"Frase modificata toLower\n{fraseModificata}");
    fraseModificata=fraseModificata.Replace(' ','-');
    Console.WriteLine($"Frase modificata replace spazio\n{fraseModificata}");
    for(int i=0;i<punteggiatura.Length;i++)
    {
        Console.WriteLine($"Ricerca segno punteggiatura per carattere {punteggiatura[i]} i: {i}");
        while(fraseModificata.IndexOf(punteggiatura[i])!=-1)
        {
            Console.WriteLine($"trovato carattere da eliminare all'indice {fraseModificata.IndexOf(punteggiatura[i]) } frase: {fraseModificata} i: {i}");
            fraseModificata=fraseModificata.Remove(fraseModificata.IndexOf(punteggiatura[i]),1);
            Console.WriteLine($"carattere eliminato, frase: {fraseModificata} i: {i}");
            
        } 
    }          
    Console.WriteLine($"Frase modificata con punteggiatura\n{fraseModificata}");
    
    for(int i=0;i<fraseModificata.Length-1;i++)
    {
        
        if(fraseModificata[i]=='-' && fraseModificata[i+1]=='-')
        {
            fraseModificata=fraseModificata.Remove(i,1);
            //Console.WriteLine($"rimosso carattere {i} frase: {fraseModificata}");
            i--;
        }
    }
    
    Console.WriteLine($"Frase finale: {fraseModificata}");
}


