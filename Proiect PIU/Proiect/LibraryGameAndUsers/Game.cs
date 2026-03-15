namespace LibraryGameAndUsers
{
    public class Game
    {
        public int IdGame { get; set; }
        public string Titlu { get; set; }
        public string Gen { get; set; }
        public string Descriere { get; set; }
        public double Pret { get; set; }

        public Game()
        {
            Titlu = string.Empty;
            Gen = string.Empty;
            Descriere = string.Empty;
        }

        public Game(int id, string titlu, string gen, string descriere, double pret)
        {
            IdGame = id;
            Titlu = titlu;
            Gen = gen;
            Descriere = descriere;
            Pret = pret;
        }

        public string Info()
        {
            return $"[{IdGame}] {Titlu} | Gen: {Gen} | Pret: {Pret} RON | {Descriere}";
        }
    }
}
