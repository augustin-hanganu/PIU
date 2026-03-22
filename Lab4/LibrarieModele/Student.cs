namespace LibrarieModele
{
    public enum ProgramStudiu
    {
        Licenta = 0,
        Master = 1,
        Doctorat = 2
    }

    public class Student
    {
        private const char SEPARATOR = ' ';
        private int[] note;

        public int IdStudent { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }

        // Cerinta 1 - proprietate ProgramStudiu
        public ProgramStudiu ProgramStudiu { get; set; }

        // Cerinta 3 - proprietate Medie cu LINQ
        public double Medie
        {
            get
            {
                if (note == null || note.Length == 0)
                    return 0;
                // LINQ - convertim vectorul la colectie si calculam media
                return note.AsEnumerable().Average();
            }
        }

        public void SetNote(int[] _note)
        {
            note = new int[_note.Length];
            _note.CopyTo(note, 0);
        }

        public int[] GetNote()
        {
            return (int[])note.Clone();
        }

        public Student()
        {
            Nume = string.Empty;
            Prenume = string.Empty;
            note = new int[0];
            ProgramStudiu = ProgramStudiu.Licenta;
        }

        public Student(int idStudent, string nume, string prenume)
        {
            IdStudent = idStudent;
            Nume = nume;
            Prenume = prenume;
            note = new int[0];
            ProgramStudiu = ProgramStudiu.Licenta;
        }

        public Student(int idStudent, string nume, string prenume, ProgramStudiu programStudiu)
        {
            IdStudent = idStudent;
            Nume = nume;
            Prenume = prenume;
            note = new int[0];
            ProgramStudiu = programStudiu;
        }

        public string Info()
        {
            string sNote = string.Empty;
            if (note != null)
                sNote = string.Join(SEPARATOR.ToString(), note);

            return $"Id:{IdStudent} Nume:{Nume ?? "NECUNOSCUT"} Prenume:{Prenume ?? "NECUNOSCUT"} " +
                   $"Program:{ProgramStudiu} Medie:{Medie:F2} Note:[{sNote}]";
        }
    }
}
