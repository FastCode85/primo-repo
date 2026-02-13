/*

- chiede all' utente di inserire il nome di un prodotto il prezzo e la quantità intervallati da una virgola
- si ripulisce la stringa dagli spazi
- verifichiamo che l' input sia valido ( ossia quando ha tre parti )
- se l'input non è corretto richiede di nuovo l'input
- ogni prodotto viene aggiunto ad una lista di stringhe con il formato "nome: prodotto - prezzo: 10.5 - quantità: 2"
- viene chiesto ll' utente se vuole inserire un altro prodotto
- se l' utente risponde "y" continua, altrimenti esce
- la lista viene ordinata in ordine alfabetico e stampata a video
 */

int parti;
string risp;
List<string> listaProdotti=new List<string>();

while(true)
{
    Console.WriteLine("Inserisci un prodotto");
    risp=Console.ReadLine();
    risp=risp.Trim();
    string[] arrayString=risp.Split(",");
    parti=arrayString.Length;
    if(parti==3)
    {
        
        bool valid=true;
        foreach(string s in arrayString)
        {
            string trimmed=s.Trim();
            if(trimmed.Length==0)
            {
                valid=false;
                break;
            }
        }
        if(valid)
        {
            string[] trimmedArray=new string[arrayString.Length];
            int index=0;
            foreach(string s in arrayString)
            {
                trimmedArray[index]=s.Trim();
                index++;
            }
            listaProdotti.Add(string.Join(",",trimmedArray));
            break;
        }
    }
}

while(true)
{

    parti=risp.Split(",").Length;
    string[] arrayString=risp.Split(",");
    if(parti!=3)
    {
        Console.WriteLine("Input non valido, reinserisci il prodotto");
        risp=Console.ReadLine();
        parti=arrayString.Length;
        if(parti==3)
        {
            
            bool valid=true;
            foreach(string s in arrayString)
            {
                string trimmed=s.Trim();
                if(trimmed.Length==0)
                {
                    valid=false;
                    break;
                }
            }
            if(valid)
            {

                string[] trimmedArray=new string[arrayString.Length];
                int index=0;
                foreach(string s in arrayString)
                {
                    trimmedArray[index]=s.Trim();
                    Console.WriteLine($"trimmed: {trimmedArray[index]} original: {s}");
                    index++;
                }
                listaProdotti.Add(string.Join(",",trimmedArray));
                break;
            }
        }
    }
    else
    {
        Console.WriteLine("Premi y per aggiungere un nuovo prodotto");
        risp=Console.ReadLine();
        if(risp=="y")
        {
            Console.WriteLine("Inserisci un nuovo prodotto");
            risp=Console.ReadLine();
            arrayString=risp.Split(",");
            parti=arrayString.Length;
            if(parti==3)
            {

                bool valid=true;
                foreach(string s in arrayString)
                {
                    string trimmed=s.Trim();
                    if(trimmed.Length==0)
                    {
                        valid=false;
                        break;
                    }
                }
                if(valid)
                {

                    string[] trimmedArray=new string[arrayString.Length];
                    int index=0;
                    foreach(string s in arrayString)
                    {
                        trimmedArray[index]=s.Trim();

                        index++;
                    }
                    listaProdotti.Add(string.Join(",",trimmedArray));
                    
                }
            }
        }
        else
        {
            break;
        }
    }
}

listaProdotti.Sort();
Console.WriteLine($"Risultato lista: {string.Join("|",listaProdotti)}");
 