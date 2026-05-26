namespace LibraryGameAndUsers
{
    public enum TipCont
    {
        Standard,
        Premium,
        Admin
    }

    [Flags]
    public enum PreferinteUser
    {
        None = 0,
        Notificari = 1,
        Newsletter = 2,
        Reduceri = 4,
        AutoRenew = 8
    }

    public class User
    {
        private const char SEPARATOR_PRINCIPAL = '|';
        private const char SEPARATOR_BIBLIOTECA = ';';
        private const int IDX_ID = 0;
        private const int IDX_NUME = 1;
        private const int IDX_SOLD = 2;
        private const int IDX_TIP_CONT = 3;
        private const int IDX_PREFERINTE = 4;
        private const int IDX_BIBLIOTECA = 5;

        public int IdUser { get; set; }
        public string Nume { get; set; }
        public double Sold { get; set; }
        public List<Game> Biblioteca { get; set; }
        public TipCont TipCont { get; set; }
        public PreferinteUser Preferinte { get; set; }

        public User()
        {
            Nume = string.Empty;
            Biblioteca = new List<Game>();
            TipCont = TipCont.Standard;
            Preferinte = PreferinteUser.None;
        }

        public User(int id, string nume, double sold,
                    TipCont tipCont, PreferinteUser preferinte)
        {
            IdUser = id;
            Nume = nume;
            Sold = sold;
            Biblioteca = new List<Game>();
            TipCont = tipCont;
            Preferinte = preferinte;
        }

        public User(string linieFisier)
        {
            string[] camp = linieFisier.Split(SEPARATOR_PRINCIPAL);

            IdUser = Convert.ToInt32(camp[IDX_ID]);
            Nume = camp[IDX_NUME];
            Sold = Convert.ToDouble(camp[IDX_SOLD]);
            Enum.TryParse(camp[IDX_TIP_CONT], out TipCont tipCont);
            TipCont = tipCont;
            Preferinte = (PreferinteUser)Convert.ToInt32(camp[IDX_PREFERINTE]);

            Biblioteca = new List<Game>();
            _idGamiBiblioteca = new List<int>();

            if (camp.Length > IDX_BIBLIOTECA && !string.IsNullOrWhiteSpace(camp[IDX_BIBLIOTECA]))
            {
                foreach (string idStr in camp[IDX_BIBLIOTECA].Split(SEPARATOR_BIBLIOTECA))
                {
                    if (int.TryParse(idStr, out int idGame))
                        _idGamiBiblioteca.Add(idGame);
                }
            }
        }

        private List<int> _idGamiBiblioteca = new List<int>();

        public void RecontituieBiblioteca(List<Game> toateJocurile)
        {
            Biblioteca = toateJocurile
                .Where(g => _idGamiBiblioteca.Contains(g.IdGame))
                .ToList();
        }

        public string ConversieLaSirPentruFisier()
        {
            string bibliotecaSir = string.Join(
                SEPARATOR_BIBLIOTECA.ToString(),
                Biblioteca.Select(g => g.IdGame.ToString()));

            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                SEPARATOR_PRINCIPAL,
                IdUser,
                Nume ?? string.Empty,
                Sold.ToString(),
                TipCont.ToString(),
                (int)Preferinte,
                bibliotecaSir);
        }

        public string Info()
        {
            return $"[{IdUser}] {Nume} | Sold: {Sold:F2} RON | " +
                   $"Cont: {TipCont} | Preferinte: {Preferinte} | " +
                   $"Jocuri: {Biblioteca.Count}";
        }
    }
}