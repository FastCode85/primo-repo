DateTime datetime=new DateTime(1990,1,1);
Console.WriteLine($"Datetime: {datetime}");
datetime=DateTime.Today;
Console.WriteLine($"Oggi è {datetime}");
Console.WriteLine($"Oggi è {datetime.ToShortDateString()}");
Console.WriteLine($"Oggi è {datetime.ToLongDateString()}");
Console.WriteLine($"Oggi è {datetime.ToString("dd/MM/yyyy")}");
string dateTimeString="2024-10-11";
DateTime dt=DateTime.Parse(dateTimeString);
bool result=DateTime.TryParse(dateTimeString, out DateTime s);
Console.WriteLine($"dt: {dt}");
if(result)
    Console.WriteLine($"s: {s}");
DateTimeOffset now=DateTimeOffset.UtcNow;
long timestamp=now.ToUnixTimeSeconds();
Console.WriteLine($"Il timestamp attuale è {timestamp}");
Console.WriteLine($"dt giorno: {dt}");
dt=dt.AddDays(1);
Console.WriteLine($"dt giorno dopo: {dt}");
Console.WriteLine($"dt giorno dopo: {dt:dddd}");
TimeSpan timeSpan=new TimeSpan(2,0,0,0);
DateTime traDueGiorni=DateTime.Today.Add(timeSpan);
Console.WriteLine($"Tra due giorni: {traDueGiorni}");

DateTime dataNascita=new DateTime(1985,3,20);
DateTime oggi=DateTime.Now;
TimeSpan diffTime=oggi.Subtract(dataNascita);
Console.WriteLine($"Giorni: {diffTime.Days} d {(diffTime.Days/365)}");
