using LibraryGameAndUsers;
using NivelStoreData;

class Program
{
    static List<Game> games = new List<Game>();
    static List<User> users = new List<User>();

    static List<Game> GetGames() => games;
    static List<User> GetUsers() => users;

    static void Main()
    {
        AdministrareMarketplace administrare = new AdministrareMarketplace();

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
            Console.WriteLine("X. Iesire");
            Console.Write("Optiune: ");
            optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

            switch (optiune)
            {
                case "A":
                    Game gameNou = AdministrareMarketplace.CitireGameTastatura();
                    administrare.AdaugaGame(gameNou, games);
                    break;

                case "B":
                    AfisareToateGameurile(GetGames());
                    break;

                case "C":
                    CautareGameDupaTitlu(GetGames());
                    break;

                case "D":
                    CautareGameDupaGen(GetGames());
                    break;

                case "E":
                    User userNou = AdministrareMarketplace.CitireUserTastatura();
                    administrare.AdaugaUser(userNou, users);
                    break;

                case "F":
                    AfisareUtilizatori(GetUsers());
                    break;

                case "G":
                    Console.Write("ID utilizator: ");
                    int.TryParse(Console.ReadLine(), out int idUser);
                    Console.Write("ID joc: ");
                    int.TryParse(Console.ReadLine(), out int idGame);
                    administrare.CumparaJoc(idUser, idGame, users, games);
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

    // ===== Afisare =====

    static void AfisareGame(Game game)
    {
        if (game == null)
            Console.WriteLine("Jocul nu exista!");
        else
            Console.WriteLine(game.Info());
    }

    static void AfisareToateGameurile(List<Game> games)
    {
        if (games.Count == 0)
        {
            Console.WriteLine("Nu exista jocuri in marketplace!");
            return;
        }
        Console.WriteLine("=== Jocuri disponibile ===");
        foreach (Game g in games)
            AfisareGame(g);
    }

    static void AfisareUtilizatori(List<User> users)
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Nu exista utilizatori!");
            return;
        }
        Console.WriteLine("=== Utilizatori inregistrati ===");
        foreach (User u in users)
            Console.WriteLine(u.Info());
    }

    // ===== Cautare =====

    static void CautareGameDupaTitlu(List<Game> games)
    {
        Console.Write("Titlu cautat: ");
        string titlu = Console.ReadLine() ?? string.Empty;

        // LINQ - filtrare dupa titlu
        List<Game> rezultat = games.Where(g => g.Titlu.ToLower()
                                                .Contains(titlu.ToLower()))
                                   .ToList();
        if (rezultat.Count == 0)
            Console.WriteLine("Niciun joc gasit!");
        else
            foreach (Game g in rezultat)
                AfisareGame(g);
    }

    static void CautareGameDupaGen(List<Game> games)
    {
        Console.WriteLine("Gen cautat (0=RPG, 1=FPS, 2=Sport, 3=Strategie, 4=Aventura, 5=Simulare): ");
        Enum.TryParse(Console.ReadLine(), out GenJoc gen);

        // LINQ - filtrare dupa gen
        List<Game> rezultat = games.Where(g => g.Gen == gen).ToList();

        if (rezultat.Count == 0)
            Console.WriteLine("Niciun joc gasit!");
        else
            foreach (Game g in rezultat)
                AfisareGame(g);
    }
}