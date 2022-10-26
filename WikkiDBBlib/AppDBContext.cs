using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikkiDBBlib.Models;

namespace WikkiDBBlib
{
    // :DbContext wurde hier per Hand dazugefügt damit die Classe davon abgeleitet werden kann
    // DbContext ist vom NET Framework und wird von Microsoft zur Verfügung gestellt.
    // Microsoft.EntityFrameworkCore.SqlServer
    // Mit dieser Klasse dbContext kann ich dann mit dem SQL Server Verbindung aufnehmen.
    // DbContext ist eine Klasse von Visual Studio
    public class AppDBContext : DbContext
    {
        private readonly string _dbConnectionString = @"Server=NBRF01\SQLEXPRESS; Database=WIKKIDB; Integrated Security=True; TrustServerCertificate=True";
        // Alternative zum Zugriff auf SQL Server
        // private readonly string _dbConnectionString = @"Server=LocalHost";
        // private readonly string _dbConnectionString = @"Server=.";
        // private readonly string _dbConnectionString = @"Server=(LocalDB\MSSQLLocalDB";

        // Die Klasse DbContext beinhaltet eine Virtual Methode. d.h. man kann diese
        // Klasse einfach erweitern
        // base. heißt das die Basis dieser Klasse verändert wird

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionString);  // Diese zeile wurde selber erkannt von VS
            base.OnConfiguring(optionsBuilder);
        }
        // mit DbSet vereisen wir hier auf Person.cs und Stadt.cs aus dem Projekt
        public DbSet<Person>? Person { get; set; }
        public DbSet<Stadt>? Stadt { get; set; }
        // Zum starten des Projekte menü: EXTRAS -> NuGet Pak... -> Paket Manager Console
        // Unterhalb öffnet sich das Konsolenfenster
        // Wichtig ist nun das Standardprojekt zu wählen: WikkiDBBlib
        // Dort wird jetzt folgende Befehl ausgeführt:

        // Add-migration InitCreate     InitCreate ist ein eigener Festgeleger Name

        // Nach diesen Befehl wird das Projekt um den Ordner Migrations mit dem File InitCreate.cs
        // erstellt. Hier ist dann der Befehl vorhanden um die Datenbank anzulegen
        // wenn das script erfolgreich erstellt wurde, kann diese zum erstellen der Datenbank
        // hergenommen werden:

        // update-database 

        // Danach kann man die Datenbank im SQL Server sehen :-)
    }
}
