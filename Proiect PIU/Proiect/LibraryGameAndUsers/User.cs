namespace LibraryGameAndUsers
{
    // Enum simplu pentru sortarea tipuri conturi care pot beneficia de anumite functii/reduceri
    public enum TipCont
    {
        Standard,
        Premium,
        Admin
    }
    //Enum deja cu FLAGS pentru a alege mai multe valori simultan in acest sens un UTILIZATOR poate avea notificari,reduceri, sau sa reinoiasca abonamentul

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

        public string Info()
        {
            return $"[{IdUser}] {Nume} | Sold: {Sold:F2} RON | " +
                   $"Cont: {TipCont} | Preferinte: {Preferinte} | " +
                   $"Jocuri: {Biblioteca.Count}";
        }
    }
}