/*

Programma che:
- genera una password casuale di lunghezza compresa tra i 5 e 8 caratteri, che deve contenere almeno una lettera maiuscola, 
una minuscola, un numero e un carattere speciale. La password non deve contenere spazi

*/

int numeroCaratteri;
int counter=0;
Random random=new Random();
numeroCaratteri=random.Next(5,9);


bool passwordValida=false;
Console.WriteLine($"Generazione di password di {numeroCaratteri} caratteri");
while(!passwordValida)
{
    bool haMaiuscola=false;
    bool haMinuscola=false;
    bool haNumero=false;
    bool haCarattereSpeciale=false; 
    string password="";
    counter=0;
    while(counter<numeroCaratteri)
    {
        char c=' ';
        while(c==' ')
        {
            int codiceCarattere=random.Next(50,255);
            c=(char)codiceCarattere;
            if(Char.IsAsciiLetter(c) && !Char.IsAsciiLetterLower(c))
            {
                haMaiuscola=true;
                Console.WriteLine($"Trovato carattere maiuscola: {c}");
            }
            else if(Char.IsAsciiLetter(c) && Char.IsAsciiLetterLower(c))
            {
                haMinuscola=true;
                Console.WriteLine($"Trovato carattere minuscola: {c}");
            }
            else if(Char.IsAsciiDigit(c))
            {
                haNumero=true;
                Console.WriteLine($"Trovato carattere numero: {c}");
            }
            else
            {
                haCarattereSpeciale=true;
                Console.WriteLine($"Trovato carattere speciale, codice: {codiceCarattere} carattere {c}");
            }
            
            Console.WriteLine($"carattere casuale: {c} numero: {codiceCarattere}");
            
        }
        password=password+c;
        Console.WriteLine($"Password: {password}");
        counter++;
    }
    if(haMaiuscola && haMinuscola && haNumero && haCarattereSpeciale)
    {
        passwordValida=true;
        Console.WriteLine($"Password generata: {password}. {haMaiuscola} {haMinuscola} {haNumero} {haCarattereSpeciale} Lunghezza password {password.Length}");
    }
    else
    {
        Console.WriteLine($"Password non valida. Generazione di un'altra password di {numeroCaratteri} caratteri. {haMaiuscola} {haMinuscola} {haNumero} {haCarattereSpeciale}");
    }
}