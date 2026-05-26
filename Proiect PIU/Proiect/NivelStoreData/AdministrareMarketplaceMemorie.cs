using LibraryGameAndUsers;

namespace NivelStoreData
{
    public class AdministrareMarketplaceMemorie : IStocareData
    {
        private List<Game> _games = new List<Game>();
        private List<User> _users = new List<User>();

        // ===== GAMES =====

        public void AddGame(Game game)
        {
            game.IdGame = _games.Count + 1;
            _games.Add(game);
        }

        public List<Game> GetGames() => _games;

        public void UpdateGame(Game gameActualizat)
        {
            int index = _games.FindIndex(g => g.IdGame == gameActualizat.IdGame);
            if (index < 0) { Console.WriteLine("Jocul nu a fost gasit!"); return; }
            _games[index] = gameActualizat;
            Console.WriteLine("Joc actualizat!");
        }

        public void DeleteGame(int idGame)
        {
            int index = _games.FindIndex(g => g.IdGame == idGame);
            if (index < 0) { Console.WriteLine("Jocul nu a fost gasit!"); return; }
            _games.RemoveAt(index);
            Console.WriteLine("Joc sters!");
        }

        // ===== USERS =====

        public void AddUser(User user)
        {
            user.IdUser = _users.Count + 1;
            _users.Add(user);
        }

        public List<User> GetUsers() => _users;

        public void UpdateUser(User userActualizat)
        {
            int index = _users.FindIndex(u => u.IdUser == userActualizat.IdUser);
            if (index < 0) { Console.WriteLine("Utilizatorul nu a fost gasit!"); return; }
            _users[index] = userActualizat;
            Console.WriteLine("Utilizator actualizat!");
        }

        public void DeleteUser(int idUser)
        {
            int index = _users.FindIndex(u => u.IdUser == idUser);
            if (index < 0) { Console.WriteLine("Utilizatorul nu a fost gasit!"); return; }
            _users.RemoveAt(index);
            Console.WriteLine("Utilizator sters!");
        }
    }
}
