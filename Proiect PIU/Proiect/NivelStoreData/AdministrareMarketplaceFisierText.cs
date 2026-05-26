using LibraryGameAndUsers;

namespace NivelStoreData
{
    public class AdministrareMarketplaceFisierText : IStocareData
    {
        private string _fisierGames;
        private string _fisierUsers;

        public AdministrareMarketplaceFisierText(string numeFisierGames, string numeFisierUsers)
        {
            _fisierGames = numeFisierGames;
            _fisierUsers = numeFisierUsers;

            Stream sg = File.Open(_fisierGames, FileMode.OpenOrCreate);
            sg.Close();
            Stream su = File.Open(_fisierUsers, FileMode.OpenOrCreate);
            su.Close();
        }

        // ===== GAMES =====

        public void AddGame(Game game)
        {
            List<Game> existente = GetGames();
            game.IdGame = existente.Count + 1;
            using (StreamWriter sw = new StreamWriter(_fisierGames, append: true))
                sw.WriteLine(game.ConversieLaSirPentruFisier());
        }

        public List<Game> GetGames()
        {
            List<Game> games = new List<Game>();
            using (StreamReader sr = new StreamReader(_fisierGames))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                    if (!string.IsNullOrWhiteSpace(linie))
                        games.Add(new Game(linie));
            }
            return games;
        }

        public void UpdateGame(Game gameActualizat)
        {
            List<Game> games = GetGames();
            int index = games.FindIndex(g => g.IdGame == gameActualizat.IdGame);
            if (index < 0) { Console.WriteLine("Jocul nu a fost gasit!"); return; }
            games[index] = gameActualizat;
            RescrieFisierGames(games);
            Console.WriteLine("Joc actualizat cu succes!");
        }

        // Delete Game 
        public void DeleteGame(int idGame)
        {
            List<Game> games = GetGames();
            int inainte = games.Count;
            games = games.Where(g => g.IdGame != idGame).ToList();
            if (games.Count == inainte)
            {
                Console.WriteLine("Jocul nu a fost gasit!");
                return;
            }
            RescrieFisierGames(games);
            Console.WriteLine("Joc sters cu succes!");
        }

        private void RescrieFisierGames(List<Game> games)
        {
            using (StreamWriter sw = new StreamWriter(_fisierGames, append: false))
                foreach (Game g in games)
                    sw.WriteLine(g.ConversieLaSirPentruFisier());
        }

        // ===== USERS =====

        public void AddUser(User user)
        {
            List<User> existenti = GetUsers();
            user.IdUser = existenti.Count + 1;
            using (StreamWriter sw = new StreamWriter(_fisierUsers, append: true))
                sw.WriteLine(user.ConversieLaSirPentruFisier());
        }

        public List<User> GetUsers()
        {
            List<Game> toateJocurile = GetGames();
            List<User> users = new List<User>();
            using (StreamReader sr = new StreamReader(_fisierUsers))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linie))
                    {
                        User u = new User(linie);
                        u.RecontituieBiblioteca(toateJocurile);
                        users.Add(u);
                    }
                }
            }
            return users;
        }

        public void UpdateUser(User userActualizat)
        {
            List<User> users = GetUsers();
            int index = users.FindIndex(u => u.IdUser == userActualizat.IdUser);
            if (index < 0) { Console.WriteLine("Utilizatorul nu a fost gasit!"); return; }
            users[index] = userActualizat;
            RescrieFisierUsers(users);
            Console.WriteLine("Utilizator actualizat cu succes!");
        }

        // Delete User -  idUser 
        public void DeleteUser(int idUser)
        {
            List<User> users = GetUsers();
            int inainte = users.Count;
            users = users.Where(u => u.IdUser != idUser).ToList();
            if (users.Count == inainte)
            {
                Console.WriteLine("Utilizatorul nu a fost gasit!");
                return;
            }
            RescrieFisierUsers(users);
            Console.WriteLine("Utilizator sters cu succes!");
        }

        private void RescrieFisierUsers(List<User> users)
        {
            using (StreamWriter sw = new StreamWriter(_fisierUsers, append: false))
                foreach (User u in users)
                    sw.WriteLine(u.ConversieLaSirPentruFisier());
        }
    }
}