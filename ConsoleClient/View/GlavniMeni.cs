namespace ConsoleClient.View;

public class GlavniMeni
{
    private readonly PosetilacConsoleView _posetiociView;
    private readonly AutorConsoleView _autoriView;
    private readonly KnjigaConsoleView _knjigeView;
    private readonly KupovinaConsoleView _kupovineView;
    private readonly IzdavacConsoleView _izdavaciView;
    private readonly AdresaConsoleView _adreseView;

    public GlavniMeni(PosetilacConsoleView posetiociView, AutorConsoleView autoriView, KnjigaConsoleView knjigeView,
        KupovinaConsoleView kupovineView, IzdavacConsoleView izdavaciView, AdresaConsoleView adreseView)
    {
        _posetiociView = posetiociView;
        _autoriView = autoriView;
        _knjigeView = knjigeView;
        _kupovineView = kupovineView;
        _izdavaciView = izdavaciView;
        _adreseView = adreseView;
    }

    public void Pokreni()
    {
        while (true)
        {
            Console.WriteLine("\n===== SAJAM KNJIGA =====");
            Console.WriteLine("1: Posetioci");
            Console.WriteLine("2: Autori");
            Console.WriteLine("3: Knjige");
            Console.WriteLine("4: Kupovine");
            Console.WriteLine("5: Izdavaci");
            Console.WriteLine("6: Adrese");
            Console.WriteLine("0: Izlaz");

            string izbor = Console.ReadLine() ?? "0";
            switch (izbor)
            {
                case "0": return;
                case "1": _posetiociView.Pokreni(); break;
                case "2": _autoriView.Pokreni(); break;
                case "3": _knjigeView.Pokreni(); break;
                case "4": _kupovineView.Pokreni(); break;
                case "5": _izdavaciView.Pokreni(); break;
                case "6": _adreseView.Pokreni(); break;
                default: Console.WriteLine("Nepostojeca opcija."); break;
            }
        }
    }
}