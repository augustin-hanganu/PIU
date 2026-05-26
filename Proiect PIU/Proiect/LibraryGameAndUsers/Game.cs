namespace LibraryGameAndUsers
{
    public enum GenJoc
    {
        RPG,
        FPS,
        Sport,
        Strategie,
        Aventura,
        Simulare
    }

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
        private const char SEPARATOR = '|';
        private const int IDX_ID = 0;
        private const int IDX_TITLU = 1;
        private const int IDX_GEN = 2;
        private const int IDX_DESCRIERE = 3;
        private const int IDX_PRET = 4;
        private const int IDX_PLATFORME = 5;

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

        public Game(string linieFisier)
        {
            string[] camp = linieFisier.Split(SEPARATOR);
            IdGame = Convert.ToInt32(camp[IDX_ID]);
            Titlu = camp[IDX_TITLU];
            Enum.TryParse(camp[IDX_GEN], out GenJoc gen);
            Gen = gen;
            Descriere = camp[IDX_DESCRIERE];
            Pret = Convert.ToDouble(camp[IDX_PRET]);
            Platforme = (PlatformaJoc)Convert.ToInt32(camp[IDX_PLATFORME]);
        }

        public string ConversieLaSirPentruFisier()
        {
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                SEPARATOR,
                IdGame,
                Titlu ?? string.Empty,
                Gen.ToString(),
                Descriere ?? string.Empty,
                Pret.ToString(),
                (int)Platforme);
        }

        public string Info()
        {
            return $"[{IdGame}] {Titlu} | Gen: {Gen} | Pret: {Pret:F2} RON " +
                   $"| Platforme: {Platforme} | {Descriere}";
        }
    }
}