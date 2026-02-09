const int NUMERO_PREDEFINITO=10;
Console.WriteLine("Inserisci un numero");
string numeroStringa=Console.ReadLine();
int numero;
bool res=int.TryParse(numeroStringa, out numero);
if (res)
{
    Console.WriteLine($"Somma: {NUMERO_PREDEFINITO+numero}");
}
else
{
    Console.WriteLine("Il valore inserito non è un numero");
}

Console.WriteLine("Inserisci un valore");
int valoreNumero=int.Parse(Console.ReadLine());
switch (valoreNumero%3)
{
    case 0:
        switch (valoreNumero % 5)
        {
            
        
        case 0:
            Console.WriteLine("Fizz buzz");
            break;
        default:
            Console.WriteLine("Fizz");
            break;
        }
    default:
        switch(valoreNumero%5)
        {
            case 0:
                Console.WriteLine("Buzz");
                break;
            default:
                Console.WriteLine("Valore non divisibile per 3 o per 5");
                break;
        }
        break;
}