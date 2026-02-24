/*string path=@"test.txt";
File.WriteAllText(path,"Hello world");
File.AppendAllText(path,"\nTesto aggiuntivo");

if(File.Exists(path))
{
    string content=File.ReadAllText(path);
    Console.WriteLine($"Contenuto del file: {content}");
}
else
{
    Console.WriteLine("Il file non esiste");
}

if(File.Exists(path))
{
    File.Delete(path);
}
else
{
    Console.WriteLine("Il file non esiste");
}

List<string> lines=new List<string>();
lines.Add("line1");
lines.Add("line2");
File.WriteAllLines(path,lines);


string[] arrayLines=File.ReadAllLines(path);
Console.WriteLine("Stampa di tutte le righe");
foreach(string s in arrayLines)
{
    Console.WriteLine(s);
}

string sourcePath=@"source.txt";
string destinationPath=@"destination.txt";
if(File.Exists(sourcePath))
{
    File.Copy(sourcePath,destinationPath);
}
else
{
    Console.WriteLine("Il file di origine non esiste");
}

string oldFileName=@"oldfilename.txt";
string newFileName=@"newfilename.txt";
if(File.Exists(oldFileName))
{
    File.Move(oldFileName,newFileName);
}
else
{
    Console.WriteLine("Il file da rinominare non esiste");
}

FileInfo info=new FileInfo(path);
Console.WriteLine(info.Length);
Console.WriteLine(info.CreationTime);
Console.WriteLine(info.LastWriteTime);
Console.WriteLine(info.Extension);
Console.WriteLine(info.Name);
Console.WriteLine(info.DirectoryName);

string dir=@"test";
Directory.CreateDirectory(dir);

if(Directory.Exists(dir))
{
    Console.WriteLine("Directory exists");
}
else
{
    Directory.CreateDirectory(dir);
    
}

if(Directory.Exists(dir))
{
    Directory.Delete(dir);
}
else
{
    Console.WriteLine("Il path non esiste");
}

string sourceDir=@"sourceDir";
string destinationDir=@"destinationDir";
if(Directory.Exists(dir))
{
    //DirectoryCopy(sourceDir, destinationDir,true);
}

if(Directory.Exists(dir))
{
    string[] files=Directory.GetFiles(dir);
    foreach(string file in files)
    {
        Console.WriteLine(file);
    }
}
else
{
    Console.WriteLine("La dir non esiste");
}

if(Directory.Exists(dir))
{
    string[] dirs=Directory.GetDirectories(dir);
    foreach(string direc in dirs)
    {
        Console.WriteLine(direc);
    }
}
else
{
    Console.WriteLine("La dir non esiste");
}

string absolutePath=@"C:\Users\sviluppatore2\Documents\projects";
string fileName=Path.GetFileName(absolutePath);
string directory=Path.GetDirectoryName(absolutePath);
string extension=Path.GetExtension(absolutePath);
string fileNameWithoutExtension=Path.GetFileNameWithoutExtension(absolutePath);
string combinedPath=Path.Combine("c:\\Users","Username","Documents","file.txt");
Console.WriteLine($"Dati: filename:{fileName}\tdirectory:{directory}\textension:{extension}\tfilenamewithoutextension{fileNameWithoutExtension}\t{combinedPath}");

DirectoryInfo infoDir=new DirectoryInfo(dir);
Console.WriteLine(infoDir.CreationTime);
Console.WriteLine(infoDir.LastWriteTime);
Console.WriteLine(infoDir.Name);
Console.WriteLine(infoDir.FullName);
//static void DirectoryCopy(string src, string dest,)

string path2=@"test.txt";
using(StreamReader reader=new StreamReader(path2))
{
    string content=reader.ReadToEnd();
    Console.WriteLine(content);
}
*/

/*

Creare un programma file manager che consenta all'utente di eseguire operazioni sui file e sulle directory. 
Il programma dovrebbe offrire un menu con le seguenti opzioni
- partire da una cartella chiamata Data all'interno del progetto
- stampare il percorso delle folders all'interno della cartella data
- fare selezionare una folder all'utente inserendo percorso relativo
- elencare i file e le sottodirectory presenti nella folder selezionata
- deve stampare le info sui file e sulle directory elencate
- creare una cartella di backup con il timestamp all'interno della folder selezionata
- copiare tutti i files presenti nella folder selezionata mantenendo la struttura delle sottodirectory
- deve spostare i file copiati dentro cartelle divisi per estensione
- deve eliminare i files originali dopo averli copiati

*/


bool continua=true;
string directorySelezionata="Data";

while(continua)
{
    
    StampaMenu(directorySelezionata);
    string risposta=Console.ReadLine();
    if(risposta=="1")
    {
        directorySelezionata=LeggiDirectory();
    }
    else if(risposta=="2")
    {
        ElencaInfoCartellaCorrente(directorySelezionata);
    }
    else if(risposta=="3")
    {
        string backupDirectory=CreateBackupDirectory();
        Backup(directorySelezionata,backupDirectory);

    }
    else if(risposta=="4")
    {
        string directoryFilesDivisi=CreateDividiPerEstensioneDirectory();
        DividiPerEstensione(directorySelezionata,directoryFilesDivisi);

    }
    else if(risposta=="5")
    {
        
        EliminaFiles(directorySelezionata);

    }
    else if(risposta=="6")
    {
        
        break;
    }
}

void EliminaFiles(string directorySelezionata)
{
    if(!Directory.Exists(directorySelezionata))
    {
        Console.WriteLine($"Attenzione, la cartella {directorySelezionata} non esiste, uscita da EliminaFiles()");
        return;
    }
    string[] directories=Directory.GetDirectories(directorySelezionata);
    Console.WriteLine($"Stampa di EliminaFiles() in directorySelezionata {directorySelezionata}");
    foreach(string currentDirectory in directories)
    {
        EliminaFiles(currentDirectory);

    }

    string[] files=Directory.GetFiles(directorySelezionata);
    foreach(string file in files)
    {
        string filePath=Path.Combine(Directory.GetCurrentDirectory(),file);
        Console.WriteLine($"Eliminazione del file {filePath}");
        if(File.Exists(filePath))
            File.Delete(filePath);
    }
    string cartellaDaEliminare=Path.Combine(Directory.GetCurrentDirectory(),directorySelezionata);
    Console.WriteLine($"Eliminazione cartella {cartellaDaEliminare}");
    if(Directory.Exists(cartellaDaEliminare))
        Directory.Delete(cartellaDaEliminare);
    
    
}

void DividiPerEstensione(string directorySelezionata, string directoryFilesDivisi)
{
    if(!Directory.Exists(directorySelezionata))
    {
        Console.WriteLine($"Attenzione, la cartella {directorySelezionata} non esiste, uscita da DividiPerEstensione()");
        return;
    }
    string[] directories=Directory.GetDirectories(directorySelezionata);

    Console.WriteLine($"Entrata nella funzione DividiPerEstensione. directorySelezionata: {directorySelezionata} directoryFilesDivisi {directoryFilesDivisi}");
    foreach(string currentDirectory in directories)
    {
        DividiPerEstensione(currentDirectory,directoryFilesDivisi);
    }

    string[] files=Directory.GetFiles(directorySelezionata);
    foreach(string file in files)
    {
        
        string extension=Path.GetExtension(file).TrimStart('.');
        Console.WriteLine($"File selezionato {file} estensione {extension.Remove(0,1)} file {Path.GetFileName(file)}");
        string targetFolder=Path.Combine(directoryFilesDivisi,extension);
        if(!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
            Console.WriteLine($"Creata cartella in {targetFolder}");
        }
        string sourcePath=Path.Combine(Directory.GetCurrentDirectory(),file);
        string targetPath=Path.Combine(targetFolder,Path.GetFileName(file));;
        Console.WriteLine($"File selezionato per la copia {file} estensione {extension.Remove(0,1)} file {Path.GetFileName(file)} targetPath {targetPath} sourcePath {sourcePath}");
        File.Copy(sourcePath,targetPath);
    }
}


void Backup(string directorySelezionata,string currentBackupDirectory)
{
    decimal.
    if(!Directory.Exists(directorySelezionata))
    {
        Console.WriteLine($"Attenzione, la cartella {directorySelezionata} non esiste, uscita da Backup()");
        return;
    }
    string[] directories=Directory.GetDirectories(directorySelezionata);
    Console.WriteLine($"Stampa metodo backup, directorySelezionata: {directorySelezionata} currentBackupDirectory: {currentBackupDirectory}");
    foreach(string currentDirectory in directories)
    {
        //Console.WriteLine($"{current}\t{directoryInfo.CreationTime}\t{directoryInfo.LastWriteTime}\t{directoryInfo.Name}\t{directoryInfo.FullName}");
        string targetPath=Path.Combine(currentBackupDirectory,currentDirectory);
        Directory.CreateDirectory(targetPath);
        Console.WriteLine($"Cartella copiata da currentDirectory: {currentDirectory} a targetPath: {targetPath}");
        //Console.WriteLine($"targetpath: {targetPath} currentDirectory: {currentDirectory} currentBackupDirectory: {currentBackupDirectory}");
        Console.WriteLine($"Next Backup() folder: {currentDirectory}");
        Backup(currentDirectory,currentBackupDirectory);
    }
    CopyFiles(directorySelezionata,currentBackupDirectory);
}

void CopyFiles(string fromFolder, string backupFolder)
{
    string[] files=Directory.GetFiles(fromFolder);
    Console.WriteLine($"Stampa files in folder {fromFolder} backupFolder: {backupFolder}");
    foreach(string file in files)
    {
        
        string targetPath=Path.Combine(backupFolder,file);
        string sourcePath=Path.Combine(Directory.GetCurrentDirectory(),file);
        Console.WriteLine($"targetPath {targetPath} sourcePath {sourcePath}");
        Console.WriteLine($"sourcePath exists: {File.Exists(sourcePath)}");
        
        File.Copy(sourcePath,targetPath);
        
    }
}

string CreateDividiPerEstensioneDirectory()
{
    DateTime currentTime = DateTime.UtcNow;
    long unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds(); //ottengo unixtime corrente
    string folder=Path.Combine(Directory.GetCurrentDirectory(),$"Files-Divisi {unixTime}");//creo il path completo della cartella backup
    Directory.CreateDirectory(folder);
    return folder;
}

string CreateBackupDirectory()
{
    DateTime currentTime = DateTime.UtcNow;
    long unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds(); //ottengo unixtime corrente
    string backupFolder=Path.Combine(Directory.GetCurrentDirectory(),$"Backup {unixTime}");//creo il path completo della cartella backup
    Directory.CreateDirectory(backupFolder);
    return backupFolder;
}

void ElencaInfoCartellaCorrente(string directorySelezionata)
{
    Console.WriteLine($"ElencaInfoCartellaCorrente: {directorySelezionata}");
    string[] directories=Directory.GetDirectories(directorySelezionata);
    string[] files=Directory.GetFiles(directorySelezionata);
    
    Console.WriteLine("Stampa delle cartelle");
    if(directories.Length==0)
        Console.WriteLine("Non ci sono cartelle");
    foreach(string current in directories)
    {
        DirectoryInfo directoryInfo=new DirectoryInfo(directorySelezionata);
        Console.WriteLine($"{current}\t{directoryInfo.CreationTime}\t{directoryInfo.LastWriteTime}\t{directoryInfo.Name}\t{directoryInfo.FullName}");
    }

    Console.WriteLine("Stampa dei files");
    if(files.Length==0)
        Console.WriteLine("Non ci sono files");
    foreach(string current in files)
    {
        string currentFilePath=Path.Combine(Directory.GetCurrentDirectory(),directorySelezionata,current);
        FileInfo fileInfo=new FileInfo(currentFilePath);
        Console.WriteLine($"{current}\t{fileInfo.CreationTime}\t{fileInfo.LastWriteTime}\t{fileInfo.Name}\t{fileInfo.FullName}");
        //Console.WriteLine($"file selezionato:{currentFilePath}");
    }
    
        
}

string LeggiDirectory()
{
    while(true)
    {
        Console.WriteLine("Inserisci la cartella da selezionare");
        string directory=Console.ReadLine();
        if(!string.IsNullOrWhiteSpace(directory))
        {
            return directory;
        }
        else
            Console.WriteLine("Cartella non valida");
    }
}
void StampaMenu(string directorySelezionata)
{
    Console.WriteLine($"Directory Selezionata {OutputDirectorySelezionata(directorySelezionata)}\nScegli una voce del menu");
    Console.WriteLine("1: seleziona una folder");
    Console.WriteLine("2: stampa la folder selezionata");
    Console.WriteLine("3: crea backup");
    Console.WriteLine("4: dividi files");
    Console.WriteLine("5: elimina files");
    Console.WriteLine("6: Esci");
}

string OutputDirectorySelezionata(string s)
{
    if(s.Length==0)
        return "nessuna";
    else
        return s;
}