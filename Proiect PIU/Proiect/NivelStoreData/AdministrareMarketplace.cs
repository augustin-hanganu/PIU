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
            int.TryParse(Console.ReadLine(), out int valPlatforma);
            PlatformaJoc platforme = (PlatformaJoc)valPlatforma;

            return new Game(0, titlu, gen, descriere, pret, platforme);
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
            int.TryParse(Console.ReadLine(), out int valPreferinte);
            PreferinteUser preferinte = (PreferinteUser)valPreferinte;

            return new User(0, nume, sold, tipCont, preferinte);
        }

        public void CumparaJoc(int idUser, int idGame, IStocareData stocare)
        {
            List<User> users = stocare.GetUsers();
            List<Game> games = stocare.GetGames();

            User? user = users.FirstOrDefault(u => u.IdUser == idUser);
            Game? game = games.FirstOrDefault(g => g.IdGame == idGame);

            if (user == null) { Console.WriteLine("Utilizatorul nu exista!"); return; }
            if (game == null) { Console.WriteLine("Jocul nu exista!"); return; }

            bool detineJoc = user.Biblioteca.Any(g => g.IdGame == idGame);
            if (detineJoc) { Console.WriteLine("Jocul este deja in biblioteca!"); return; }

            if (user.Sold < game.Pret)
            {
                Console.WriteLine($"Sold insuficient! Ai nevoie de {game.Pret:F2} RON.");
                return;
            }

            user.Sold -= game.Pret;
            user.Biblioteca.Add(game);
            stocare.UpdateUser(user);

            Console.WriteLine($"Ai cumparat '{game.Titlu}' cu succes! Sold ramas: {user.Sold:F2} RON");
        }

        public void ModificaGame(IStocareData stocare)
        {
            Console.Write("ID joc de modificat: ");
            int.TryParse(Console.ReadLine(), out int idGame);

            List<Game> games = stocare.GetGames();
            Game? game = games.FirstOrDefault(g => g.IdGame == idGame);

            if (game == null) { Console.WriteLine("Jocul nu exista!"); return; }

            Console.Write($"Pret nou (actual: {game.Pret:F2} RON, Enter pentru a pastra): ");
            string pretInput = Console.ReadLine() ?? string.Empty;
            if (double.TryParse(pretInput, out double pretNou))
                game.Pret = pretNou;

            Console.Write($"Descriere noua (actual: {game.Descriere}, Enter pentru a pastra): ");
            string descriereNoua = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(descriereNoua))
                game.Descriere = descriereNoua;

            stocare.UpdateGame(game);
        }

        public void ModificaUser(IStocareData stocare)
        {
            Console.Write("ID utilizator de modificat: ");
            int.TryParse(Console.ReadLine(), out int idUser);

            List<User> users = stocare.GetUsers();
            User? user = users.FirstOrDefault(u => u.IdUser == idUser);

            if (user == null) { Console.WriteLine("Utilizatorul nu exista!"); return; }

            Console.Write($"Sold nou (actual: {user.Sold:F2} RON, Enter pentru a pastra): ");
            string soldInput = Console.ReadLine() ?? string.Empty;
            if (double.TryParse(soldInput, out double soldNou))
                user.Sold = soldNou;

            stocare.UpdateUser(user);
        }
    }
}