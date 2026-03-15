using LibraryGameAndUsers;

namespace NivelStoreData
{
    public class AdministrareMarketplace
    {
        public List<Game> games = new List<Game>();
        public List<User> users = new List<User>();

        public static Game CitireGameTastatura()
        {
            Console.Write("Titlu: ");
            string titlu = Console.ReadLine();

            Console.Write("Gen: ");
            string gen = Console.ReadLine();

            Console.Write("Descriere: ");
            string descriere = Console.ReadLine();

            Console.Write("Pret (RON): ");
            double.TryParse(Console.ReadLine(), out double pret);

            return new Game(0, titlu, gen, descriere, pret);
        }

        public void AdaugaGame(Game game)
        {
            game.IdGame = games.Count + 1;
            games.Add(game);
            Console.WriteLine("Joc adaugat cu succes!");
        }

        public static void AfisareGame(Game game)
        {
            if (game == null)
                Console.WriteLine("Jocul nu exista!");
            else
                Console.WriteLine(game.Info());
        }

        public void AfisareToateGameurile()
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

        public void CautareGameDupaTitlu()
        {
            Console.Write("Titlu cautat: ");
            string titlu = Console.ReadLine();
            bool gasit = false;

            foreach (Game g in games)
            {
                if (g.Titlu.ToLower().Contains(titlu.ToLower()))
                {
                    AfisareGame(g);
                    gasit = true;
                }
            }
            if (!gasit)
                Console.WriteLine("Niciun joc gasit!");
        }

        public void CautareGameDupaGen()
        {
            Console.Write("Gen cautat: ");
            string gen = Console.ReadLine();
            bool gasit = false;

            foreach (Game g in games)
            {
                if (g.Gen.ToLower() == gen.ToLower())
                {
                    AfisareGame(g);
                    gasit = true;
                }
            }
            if (!gasit)
                Console.WriteLine("Niciun joc gasit!");
        }

        public static User CitireUserTastatura()
        {
            Console.Write("Nume utilizator: ");
            string nume = Console.ReadLine();

            Console.Write("Sold initial (RON): ");
            double.TryParse(Console.ReadLine(), out double sold);

            return new User(0, nume, sold);
        }

        public void AdaugaUser(User user)
        {
            user.IdUser = users.Count + 1;
            users.Add(user);
            Console.WriteLine("Utilizator inregistrat!");
        }

        public void AfisareUtilizatori()
        {
            if (users.Count == 0)
            {
                Console.WriteLine("Nu exista utilizatori!");
                return;
            }
            foreach (User u in users)
                Console.WriteLine(u.Info());
        }

        public void CumparaJoc(int idUser, int idGame)
        {
            User user = null;
            Game game = null;

            foreach (User u in users)
                if (u.IdUser == idUser) user = u;

            foreach (Game g in games)
                if (g.IdGame == idGame) game = g;

            if (user == null) { Console.WriteLine("Utilizatorul nu exista!"); return; }
            if (game == null) { Console.WriteLine("Jocul nu exista!"); return; }

            foreach (Game g in user.Biblioteca)
            {
                if (g.IdGame == idGame)
                {
                    Console.WriteLine("Jocul este deja in biblioteca!");
                    return;
                }
            }

            if (user.Sold < game.Pret)
            {
                Console.WriteLine($"Sold insuficient! Ai nevoie de {game.Pret} RON.");
                return;
            }

            user.Sold -= game.Pret;
            user.Biblioteca.Add(game);
            Console.WriteLine($"Ai cumparat '{game.Titlu}' cu succes! Sold ramas: {user.Sold} RON");
        }

        public List<Game> GetGames() => games;
        public List<User> GetUsers() => users;
    }
}