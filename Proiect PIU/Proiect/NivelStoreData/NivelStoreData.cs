using LibraryGameAndUsers;

namespace NivelStoreData
{
    public class AdministrareMarketplace
    {
        public static Game CitireGameTastatura()
        {
            Console.Write("Titlu: ");
            string titlu = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Gen (0=RPG, 1=FPS, 2=Sport, 3=Strategie, 4=Aventura, 5=Simulare): ");
            Enum.TryParse(Console.ReadLine(), out GenJoc gen);

            Console.Write("Descriere: ");
            string descriere = Console.ReadLine() ?? string.Empty;

            Console.Write("Pret (RON): ");
            double.TryParse(Console.ReadLine(), out double pret);

            Console.WriteLine("Platforme disponibile (aduna valorile dorite):");
            Console.WriteLine("  1 = PC");
            Console.WriteLine("  2 = PlayStation");
            Console.WriteLine("  4 = Xbox");
            Console.WriteLine("  8 = Nintendo");
            Console.WriteLine("  Ex: 3 = PC + PlayStation");
            int.TryParse(Console.ReadLine(), out int valPlatforma);
            PlatformaJoc platforme = (PlatformaJoc)valPlatforma;

            return new Game(0, titlu, gen, descriere, pret, platforme);
        }

        public void AdaugaGame(Game game, List<Game> games)
        {
            game.IdGame = games.Count + 1;
            games.Add(game);
            Console.WriteLine("Joc adaugat cu succes!");
        }

        public static User CitireUserTastatura()
        {
            Console.Write("Nume utilizator: ");
            string nume = Console.ReadLine() ?? string.Empty;

            Console.Write("Sold initial (RON): ");
            double.TryParse(Console.ReadLine(), out double sold);

            Console.WriteLine("Tip cont (0=Standard, 1=Premium, 2=Admin): ");
            Enum.TryParse(Console.ReadLine(), out TipCont tipCont);

            Console.WriteLine("Preferinte (aduna valorile dorite):");
            Console.WriteLine("  1 = Notificari");
            Console.WriteLine("  2 = Newsletter");
            Console.WriteLine("  4 = Reduceri");
            Console.WriteLine("  8 = AutoRenew");
            Console.WriteLine("  Ex: 3 = Notificari + Newsletter");
            int.TryParse(Console.ReadLine(), out int valPreferinte);
            PreferinteUser preferinte = (PreferinteUser)valPreferinte;

            return new User(0, nume, sold, tipCont, preferinte);
        }

        public void AdaugaUser(User user, List<User> users)
        {
            user.IdUser = users.Count + 1;
            users.Add(user);
            Console.WriteLine("Utilizator inregistrat!");
        }

        public void CumparaJoc(int idUser, int idGame, List<User> users, List<Game> games)
        {
            // LINQ in loc de foreach pentru cautare - actualizat confrom cerintelor din Lab4
            User user = users.FirstOrDefault(u => u.IdUser == idUser);
            Game game = games.FirstOrDefault(g => g.IdGame == idGame);

            if (user == null) { Console.WriteLine("Utilizatorul nu exista!"); return; }
            if (game == null) { Console.WriteLine("Jocul nu exista!"); return; }

            // LINQ pentru verificare biblioteca - acatualizat conform cerintelor din Lab4
            bool detineJoc = user.Biblioteca.Any(g => g.IdGame == idGame);
            if (detineJoc)
            {
                Console.WriteLine("Jocul este deja in biblioteca!");
                return;
            }

            if (user.Sold < game.Pret)
            {
                Console.WriteLine($"Sold insuficient! Ai nevoie de {game.Pret:F2} RON.");
                return;
            }

            user.Sold -= game.Pret;
            user.Biblioteca.Add(game);
            Console.WriteLine($"Ai cumparat '{game.Titlu}' cu succes! Sold ramas: {user.Sold:F2} RON");
        }
    }
}