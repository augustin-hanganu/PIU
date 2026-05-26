using LibraryGameAndUsers;
using NivelStoreData;

class Program
{
    static IStocareData stocare = StocareFactory.GetAdministratorStocare();
    static AdministrareMarketplace administrare = new AdministrareMarketplace();

    static void Main()
    {
        string optiune;
        do
        {
            Console.WriteLine("\n=== GAME MARKETPLACE ===");
            Console.WriteLine("A. Adauga joc in marketplace");
            Console.WriteLine("B. Afiseaza toate jocurile");
            Console.WriteLine("C. Cauta joc dupa titlu");
            Console.WriteLine("D. Cauta joc dupa gen");
            Console.WriteLine("E. Inregistreaza utilizator");
            Console.WriteLine("F. Afiseaza utilizatori");
            Console.WriteLine("G. Cumpara joc");
            Console.WriteLine("H. Modifica joc");
            Console.WriteLine("I. Modifica utilizator");
            Console.WriteLine("X. Iesire");
            Console.Write("Optiune: ");
            optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

            switch (optiune)
            {
                case "A":
                    Game gameNou = AdministrareMarketplace.CitireGameTastatura();
                    stocare.AddGame(gameNou);
                    Console.WriteLine("Joc adaugat cu succes!");
                    break;

                case "B":
                    AfisareToateGameurile(stocare.GetGames());
                    break;

                case "C":
                    CautareGameDupaTitlu(stocare.GetGames());
                    break;

                case "D":
                    CautareGameDupaGen(stocare.GetGames());
                    break;

                case "E":
                    User userNou = AdministrareMarketplace.CitireUserTastatura();
                    stocare.AddUser(userNou);
                    Console.WriteLine("Utilizator inregistrat!");
                    break;

                case "F":
                    AfisareUtilizatori(stocare.GetUsers());
                    break;

                case "G":
                    Console.Write("ID utilizator: ");
                    int.TryParse(Console.ReadLine(), out int idUser);
                    Console.Write("ID joc: ");
                    int.TryParse(Console.ReadLine(), out int idGame);
                    administrare.CumparaJoc(idUser, idGame, stocare);
                    break;

                case "H":
                    administrare.ModificaGame(stocare);
                    break;

                case "I":
                    administrare.ModificaUser(stocare);
                    break;

                case "X":
                    Console.WriteLine("La revedere!");
                    return;

                default:
                    Console.WriteLine("Optiune invalida!");
                    break;
            }
        } while (optiune != "X");
    }

    static void AfisareToateGameurile(List<Game> games)
    {
        if (games.Count == 0) { Console.WriteLine("Nu exista jocuri!"); return; }
        Console.WriteLine("=== Jocuri disponibile ===");
        foreach (Game g in games)
            Console.WriteLine(g.Info());
    }

    static void AfisareUtilizatori(List<User> users)
    {
        if (users.Count == 0) { Console.WriteLine("Nu exista utilizatori!"); return; }
        Console.WriteLine("=== Utilizatori inregistrati ===");
        foreach (User u in users)
            Console.WriteLine(u.Info());
    }

    static void CautareGameDupaTitlu(List<Game> games)
    {
        Console.Write("Titlu cautat: ");
        string titlu = Console.ReadLine() ?? string.Empty;
        List<Game> rezultat = games
            .Where(g => g.Titlu.ToLower().Contains(titlu.ToLower()))
            .ToList();
        if (rezultat.Count == 0) Console.WriteLine("Niciun joc gasit!");
        else foreach (Game g in rezultat) Console.WriteLine(g.Info());
    }

    static void CautareGameDupaGen(List<Game> games)
    {
        Console.WriteLine("Gen cautat (0=RPG, 1=FPS, 2=Sport, 3=Strategie, 4=Aventura, 5=Simulare): ");
        Enum.TryParse(Console.ReadLine(), out GenJoc gen);
        List<Game> rezultat = games.Where(g => g.Gen == gen).ToList();
        if (rezultat.Count == 0) Console.WriteLine("Niciun joc gasit!");
        else foreach (Game g in rezultat) Console.WriteLine(g.Info());
    }
}