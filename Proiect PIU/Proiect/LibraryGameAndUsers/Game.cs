namespace LibraryGameAndUsers
{
    //Aici deja enum simplu pentru tipul jocului enumerate mai in jos
    public enum GenJoc
    {
        RPG,
        FPS,
        Sport,
        Strategie,
        Aventura,
        Simulare
    }
    //Deja un enum mai complex cu FLAGS unde un joc poate fi disponibil pe mai multe platforme,functioneaza in baza codului binar
    [Flags]
    public enum PlatformaJoc
    {
        None = 0,
        PC = 1,
        PlayStation = 2,
        Xbox = 4,
        Nintendo = 8
    }

    public class Game
    {
        public int IdGame { get; set; }
        public string Titlu { get; set; }
        public string Descriere { get; set; }
        public double Pret { get; set; }
        public GenJoc Gen { get; set; }
        public PlatformaJoc Platforme { get; set; }

        public Game()
        {
            Titlu = string.Empty;
            Descriere = string.Empty;
        }

        public Game(int id, string titlu, GenJoc gen, string descriere,
                    double pret, PlatformaJoc platforme)
        {
            IdGame = id;
            Titlu = titlu;
            Gen = gen;
            Descriere = descriere;
            Pret = pret;
            Platforme = platforme;
        }

        public string Info()
        {
            return $"[{IdGame}] {Titlu} | Gen: {Gen} | Pret: {Pret:F2} RON " +
                   $"| Platforme: {Platforme} | {Descriere}";
        }
    }
}