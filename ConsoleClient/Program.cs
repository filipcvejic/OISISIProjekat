using ConsoleClient.View;
using Core.Services;

namespace ConsoleClient;

class Program
{
    static void Main()
    {
        PodaciSajma podaci = new PodaciSajma();

        AdresaServis adresaServis = new AdresaServis(podaci);
        PosetilacServis posetilacServis = new PosetilacServis(podaci);
        AutorServis autorServis = new AutorServis(podaci);
        KnjigaServis knjigaServis = new KnjigaServis(podaci);
        KupovinaServis kupovinaServis = new KupovinaServis(podaci);
        IzdavacServis izdavacServis = new IzdavacServis(podaci);

        GlavniMeni meni = new GlavniMeni(
            new PosetilacConsoleView(posetilacServis, adresaServis, knjigaServis),
            new AutorConsoleView(autorServis, adresaServis),
            new KnjigaConsoleView(knjigaServis, autorServis, izdavacServis),
            new KupovinaConsoleView(kupovinaServis, posetilacServis, knjigaServis),
            new IzdavacConsoleView(izdavacServis, autorServis),
            new AdresaConsoleView(adresaServis));

        meni.Pokreni();
    }
}