/*

Uan funzione che prende in input un numero intero e restituisce il numero solo se è pari, 
altrimenti ritorna un messaggio d'uscita.


*/

int numero=RichiediNumero();

int RichiediNumero()
{   
    while(true)
    {
        Console.WriteLine("Inserisci un numero");
        string s=Console.ReadLine();
        bool res=int.TryParse(s, out int num);
        if(res)
        {
            if(NumeroPari(num))
                return num;
            else
                Console.WriteLine("Il numero non è pari.");
        }
        else
            Console.WriteLine("Input non valido.");
    }
}

bool NumeroPari(int numero)
{
    if(numero%2==0)
        return true;
    else
        return false;
}
