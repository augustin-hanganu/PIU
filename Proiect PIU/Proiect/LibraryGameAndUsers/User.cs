namespace LibraryGameAndUsers
{
    public class User
    {
        public int IdUser { get; set; }
        public string Nume { get; set; }
        public double Sold { get; set; }
        public List<Game> Biblioteca { get; set; }

        public User()
        {
            Nume = string.Empty;
            Biblioteca = new List<Game>();
        }

        public User(int id, string nume, double sold)
        {
            IdUser = id;
            Nume = nume;
            Sold = sold;
            Biblioteca = new List<Game>();
        }

        public string Info()
        {
            return $"[{IdUser}] {Nume} | Sold: {Sold} RON | Jocuri cumparate: {Biblioteca.Count}";
        }
    }
}