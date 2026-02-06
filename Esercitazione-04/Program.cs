Console.WriteLine("Inserisci un numero intero");
string input=Console.ReadLine();
int numero=int.Parse(input);
Console.WriteLine($"Il numero inserito e' {numero}");
Console.WriteLine($"Il tipo di dato e' {numero.GetType()}");

Console.WriteLine("Inserisci un numero decimale");
string input2=Console.ReadLine();
double numeroDecimale=double.Parse(input2);
Console.WriteLine($"Il numero decimale e' {numeroDecimale}");

int numeroIntero=00123;
string numeroStringa=numeroIntero.ToString();
Console.WriteLine($"Il numero come stringa e' {numeroStringa}");

double numeroDecimale2=9.99;
int numeroIntero2=(int)numeroDecimale2; //casting da double a int
Console.WriteLine($"Il numero convertito e' {numeroIntero2}");

int x=5;
int y=10;
bool isEqual= (x==y);
bool isNotEqual=(x!=y);
bool isGreater=(x>y);
bool isLess=(x<y);
bool isGreaterOrEqual=(x>=y);
bool isLessOrEqual=(x<=y);

Console.WriteLine($"isEqual {isEqual} isNotEqual {isNotEqual} isGreater {isGreater} isLess {isLess} isGreaterOrEqual {isGreaterOrEqual} isLessOrEqual {isLessOrEqual}");

bool a=true;
bool b=false;
bool andResult=a && b;
bool orResult=a || b;
bool notA=!a;
Console.WriteLine($"andResult: {andResult} orResult: {orResult} notA: {notA}");

double num1;
Console.WriteLine("Inserisci il primo numero");
num1=int.Parse(Console.ReadLine());

double num2;
Console.WriteLine("Inserisci il secondo numero");
num2=int.Parse(Console.ReadLine());
Console.WriteLine($"somma: {num1+num2} differenza: {num1-num2} moltiplicazione: {num1*num2} quoziente: {num1/num2}");
Console.WriteLine("Quale operazione vuoi eseguire?");
char operazione=Console.ReadKey();
if(operazione=='+')

