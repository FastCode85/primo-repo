/*
Programma che
- legge un elenco di email, maschera la parte dell' utente lasciando visibile la prima e
l'ultima lettera mantenendo visibile il dominio (es a*****o@gmail.com) e le stampa
*/



List<string> listaEmail=new List<string> {"marco@gmail.com","francesco@hotmail.it","matteo@gmail.it"};


foreach(string mail in listaEmail)
{
    int i=0;
    string nuovaMail="";
    bool modify=true;
    Console.WriteLine($"Stringa da modificare: {mail}");
    while(i<mail.Length)
    {
        char nextChar='a';
        if(i+1<mail.Length)
            nextChar=mail[i+1];
        if(i>0 && modify==true && nextChar!='@')
            nuovaMail+="*";
        else
            nuovaMail+=mail[i];
        i++;
        if(i<mail.Length)
            if(mail[i]=='@')
                modify=false;
    }
    Console.WriteLine($"Stringa modificata: {nuovaMail}");
}