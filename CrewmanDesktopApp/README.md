# Crewman Desktop App

Windows desktop aplikacija za upravljanje pomorcima: pretraživanje, pregled u tablici te dodavanje, uređivanje i brisanje pomoraca.

- **Tehnologije:** C# / .NET Framework 4.8, Windows Forms, DevExpress WinForms komponente, MS SQL Server (pristup putem T-SQL-a i ADO.NET-a, bez ORM-a)
- **Sučelje:** hrvatski jezik, jedan glavni prozor + jedan dijalog za unos

## Što aplikacija radi

| Funkcija | Kako |
|---|---|
| Pretraga po imenu, prezimenu, rangu i brodu | Jedno polje za pretragu (rezultati se filtriraju dok tipkate) + padajući filtri **Rang** i **Brod** |
| Prikaz rezultata u tablici | Stupci: Ime, Prezime, Rang, Brod, Datum ukrcaja; klik na zaglavlje sortira |
| Dodavanje pomorca | Gumb **Dodaj pomorca** (ili `Ctrl+N`; `Insert` u tablici) |
| Uređivanje pomorca | Gumb **Uredi**, dvoklik na redak ili `Enter` u tablici |
| Brisanje pomorca | Gumb **Obriši** ili tipka `Delete` u tablici (uz potvrdu) |
| Podaci o pomorcu | Ime, prezime, datum rođenja, nacionalnost, e-mail, rang, brod, datum ukrcaja |
| Novi rang / brod | Gumb **+** uz padajući popis u dijalogu pomorca |

Pretraga radi tako da se svaka upisana riječ mora pojaviti u imenu, prezimenu, nazivu ranga ili nazivu broda.
Pretraga ne razlikuje velika i mala slova ni dijakritike (`peric` pronalazi Perića, `korcula` brod MT Korčula).
Primjer: `horvat master` pronalazi Ivana Horvata s rangom Master; `adriatic` pronalazi sve ukrcane na *MV Adriatic Star*.

Provjere pri unosu: obavezna polja (ime, prezime, datum rođenja, nacionalnost, rang), datum rođenja ne smije biti u budućnosti
i pomorac mora imati najmanje 16 godina, e-mail mora biti ispravnog oblika, a datum ukrcaja se unosi samo ako je odabran brod.

## Preduvjeti

1. **Visual Studio 2019 ili 2022** s radnim opterećenjem *.NET desktop development* (uključuje .NET Framework 4.8)
2. **DevExpress WinForms** komponente (v22.2 ili novije; probna verzija je dovoljna) - <https://www.devexpress.com/products/net/controls/winforms/>
3. **SQL Server** - bilo koje izdanje: Express, Developer ili LocalDB (dolazi uz Visual Studio)

## Pokretanje

1. Otvorite `CrewmanDesktopApp.sln` u Visual Studiju.
2. Po potrebi prilagodite connection string u `CrewmanDesktopApp/App.config` (zadano: `Server=.\SQLEXPRESS;Database=Crewman;Integrated Security=True;`).
   Za zadanu instancu SQL Servera upišite `Server=localhost`, za LocalDB `Server=(localdb)\MSSQLLocalDB`.
3. Pritisnite **F5**.
4. Pri prvom pokretanju aplikacija javlja da baza `Crewman` još ne postoji i nudi da je kreira zajedno s demo podacima
   (rangovi, brodovi i 16 pomoraca). Potvrdite s **Da** - i to je sve.

Bazu možete kreirati i ručno: skripta `Database/CrewmanDatabase.sql` pokreće se u SQL Server Management Studiju.
Skripta je idempotentna (može se pokrenuti više puta), a aplikacija pri prvom pokretanju izvršava upravo tu skriptu.

### DevExpress verzija

Projekt automatski prepoznaje instaliranu verziju DevExpressa (traži mapu `C:\Program Files\DevExpress XX.Y\Components\Bin\Framework`).
Ako je DevExpress instaliran na drugo mjesto ili se sklopovi ne pronađu, u `CrewmanDesktopApp.csproj` ručno postavite verziju,
npr. `<DevExpressVersion>25.1</DevExpressVersion>`, ili dodajte reference kroz *Add Reference* u Visual Studiju.

## Struktura projekta

```
CrewmanDesktopApp.sln
Database/
  CrewmanDatabase.sql          T-SQL skripta: baza, tablice, indeksi, ograničenja i demo podaci
CrewmanDesktopApp/
  Program.cs                   ulazna točka, postavke DevExpressa, provjera/kreiranje baze
  App.config                   connection string
  CroatianEditorsLocalizer.cs  hrvatski nazivi gumba u DevExpress dijalozima
  Models/                      Seafarer, SeafarerListItem, Rank, Vessel
  Data/
    Db.cs                      otvaranje veze (System.Data.SqlClient)
    DatabaseInitializer.cs     provjera baze i izvršavanje ugrađene SQL skripte
    SeafarerRepository.cs      pretraga, dohvat, insert, update, delete (parametrizirani T-SQL)
    LookupRepository.cs        rangovi i brodovi
  Forms/
    MainForm.cs                glavni prozor: pretraga, tablica, gumbi
    SeafarerEditForm.cs        dijalog za unos / uređivanje pomorca s validacijom
    LookupHelper.cs            zajedničke postavke LookUpEdit editora
```

## Baza podataka

| Tablica | Polja |
|---|---|
| Seafarers | Id, FirstName, LastName, DateOfBirth, Nationality, Email, RankId, VesselId, EmbarkationDate |
| Ranks | Id, Name |
| Vessels | Id, Name |

`RankId` i `VesselId` su strani ključevi na `Ranks` i `Vessels`. `VesselId` i `EmbarkationDate` mogu biti NULL (pomorac koji trenutno nije ukrcan),
a CHECK ograničenje osigurava da datum ukrcaja postoji samo uz brod. Svi upiti iz aplikacije su parametrizirani (nema SQL injectiona).

## Tipkovnički prečaci

| Tipka | Radnja |
|---|---|
| `Ctrl+N` (bilo gdje) / `Insert` (u tablici) | Novi pomorac |
| `Enter` / dvoklik (u tablici) | Uredi označenog pomorca |
| `Delete` (u tablici) | Obriši označenog pomorca |
| `Ctrl+F` | Fokus na polje za pretragu |
| `Esc` (u filterima) | Očisti sve filtre |
| `F5` | Osvježi popis |
