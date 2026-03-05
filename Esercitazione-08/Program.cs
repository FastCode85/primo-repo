

using System.Dynamic;

MiaClasseAutomatica classeAutomatica5=new MiaClasseAutomatica("Pippo",3);
MiaClasseAutomatica classeAutomatica1=new MiaClasseAutomatica("Mario",1);
MiaClasseAutomatica classeAutomatica2=new MiaClasseAutomatica("Marco",2);
MiaClasseAutomatica classeAutomatica3=new MiaClasseAutomatica("Mario",1);
MiaClasseAutomatica classeAutomatica4=classeAutomatica1;

List<MiaClasseAutomatica> lista=new List<MiaClasseAutomatica>();
lista.Add(classeAutomatica5);
lista.Add(classeAutomatica1);
lista.Add(classeAutomatica2);

Console.WriteLine($"a == b: {classeAutomatica1==classeAutomatica2} a equls b: {classeAutomatica1.Equals(classeAutomatica2)} a referenceEquals {object.ReferenceEquals(classeAutomatica1,classeAutomatica2)}");
Console.WriteLine($"a == b: {classeAutomatica1==classeAutomatica3} a equals b: {classeAutomatica1.Equals(classeAutomatica3)} a referenceEquals {object.ReferenceEquals(classeAutomatica1,classeAutomatica3)}");
Console.WriteLine($"a == b: {classeAutomatica1==classeAutomatica4} a equals b: {classeAutomatica1.Equals(classeAutomatica4)} a referenceEquals {object.ReferenceEquals(classeAutomatica1,classeAutomatica4)}");
//Console.WriteLine($"Remove from list: {lista.Remove(classeAutomatica3)}");

Console.WriteLine("Stampa della lista non ordinata");
foreach(MiaClasseAutomatica c in lista)
{
    Console.WriteLine(c);
}

lista.Sort();
Console.WriteLine("Stampa della lista ordinata");
foreach(MiaClasseAutomatica c in lista)
{
    Console.WriteLine(c);
}
public class MiaClasseAutomatica : IComparable<MiaClasseAutomatica>
{
    public string Nome {get;set;}
    public int Id {get;set;}
    
    public MiaClasseAutomatica(String nome, int id)
    {
        Nome=nome;
        Id=id;
    }

    public override bool Equals(object? o)
    {
        if(o is MiaClasseAutomatica)
        {
            MiaClasseAutomatica c=(MiaClasseAutomatica)o;
            if(this.Nome==c.Nome && this.Id==c.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Nome, Id);
    }

    public int CompareTo(MiaClasseAutomatica? c)
    {
        return this.Id.CompareTo(c.Id);
    }

    public override string ToString()
    {
        return $"{Nome} {Id}";
    }
}

