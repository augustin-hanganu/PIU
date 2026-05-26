using LibraryGameAndUsers;

namespace NivelStoreData
{
    public interface IStocareData
    {
        // Game
        void AddGame(Game g);
        List<Game> GetGames();
        void UpdateGame(Game g);
        void DeleteGame(int idGame);   

        // User
        void AddUser(User u);
        List<User> GetUsers();
        void UpdateUser(User u);
        void DeleteUser(int idUser);   
    }
}