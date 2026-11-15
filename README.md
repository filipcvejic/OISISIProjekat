# OISISIProjekat

Projekat iz predmeta OISISI, školska 2026/2027. Informacioni sistem uprave sajma knjiga, rađen u C# (.NET 10).

## Tim

- Filip Cvejić (Student 1)
- Ognjen Kostadinović (Student 2)

## Ko je šta radio

Filip Cvejić:
- solution i projekti Core, ConsoleClient i WpfClient, .gitignore
- serijalizacija (ISerializable, Serializer)
- modeli Posetilac, StatusPosetioca, Kupovina, Izdavac i IzdavacAutor
- DAO klase za te modele
- PodaciSajma (učitavanje podataka i povezivanje entiteta)
- servisi PosetilacServis, KupovinaServis i IzdavacServis
- konzolni prikaz za posetioce, kupovine i izdavače, glavni meni i Program.cs

Ognjen Kostadinović:
- čuvanje podataka u fajlove (Storage) i folder Data
- modeli Adresa, Autor, Knjiga, KnjigaAutor i StavkaListeZelja
- DAO klase za te modele
- servisi AdresaServis, AutorServis i KnjigaServis
- pomoćne metode za unos u konzoli (Unos)
- konzolni prikaz za adrese, autore i knjige

## Pokretanje

Otvoriti OISISIProjekat.slnx u Visual Studiju, izabrati ConsoleClient kao startni projekat i pokrenuti. Podaci se čuvaju kao .txt fajlovi u folderu Data.
