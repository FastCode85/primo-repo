/*

Programma calcolatrice basato sulle funzioni
- l'utente deve inserire una operazione da calcolare
- Il programma calcolerà l'operazione in base all'input se l'operazione è possibile
-l'input deve essere una stringa di input unica
- deve avere due numeri e solo un operatore

*/

Console.WriteLine("Inserisci l'operazione da calcolare nel formato \"numero1 numero2 operatore\"");
string input=Console.ReadLine();
string[] splittedInput=input.Split(" ");


if(OperatoreValido(Operatore(splittedInput)))
{
    if(NumeriValidi(splittedInput[0],splittedInput[1]))
    {
        double risultato=Calcola(
            double.Parse(splittedInput[0]),
            double.Parse(splittedInput[1]),
            splittedInput[2]
        );
        Console.WriteLine($"Risultato: {risultato}");
    }
}

double Calcola(double numero1, double numero2, string operatore)
{
    if(operatore=="+")
    {
        return numero1+numero2;
    }
    else if(operatore=="-")
    {
        return numero1-numero2;
    }
    else if(operatore=="*")
    {
        return numero1*numero2;
    }
    else if(operatore=="/")
    {
        if(numero2!=0)
            return numero1/numero2;
        else
        {
            Console.WriteLine("Attenzione, divisione per zero");
            return 0;
        }
    }
    else
    {
        Console.WriteLine("Errore nella funzione Calcola, operatore non valido");
        return 0;
    }
}

bool NumeriValidi(string numero1, string numero2)
{
    bool risultato1=double.TryParse(numero1,out double numeroDouble1);
    bool risultato2=double.TryParse(numero2,out double numeroDouble2);
    if(risultato1 && risultato2)
        return true;
    else
    {
        Console.WriteLine("Numeri non validi");
        return false;
    }
}

bool OperatoreValido(string operatore)
{
    if(operatore=="+" || operatore=="-" || operatore=="*" || operatore=="/")
        return true;
    else
        return false;
}

string Operatore(string[] input)
{
    if(input.Length!=3)
    {
        Console.WriteLine($"Problema di input nella funzione operatore, input.length: {input.Length}");
        return "";
    }
    else
        return input[2];
}