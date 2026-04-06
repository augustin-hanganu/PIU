using System.Collections;
namespace LibrarieModele
{
    public enum ProgramStudiu
    {
        Licenta,
        Master,
        Doctorat
    }

    public class Student
    {
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const char SEPARATOR_SECUNDAR_FISIER = ' ';
        private const bool SUCCES = true;
        public const int NOTA_MINIMA = 1;
        public const int NOTA_MAXIMA = 10;

        private const int ID = 0;
        private const int NUME = 1;
        private const int PRENUME = 2;
        private const int GRUPA = 3;
        private const int PROGRAM_STUDIU = 4;
        private const int NOTE = 5;

        private int[] note;

        public int IdStudent { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Grupa { get; set; }
        public ProgramStudiu ProgramStudiu { get; set; }

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
            Grupa = string.Empty;
            ProgramStudiu = ProgramStudiu.Licenta;
            note = new int[0];
        }

        public Student(int idStudent, string nume, string prenume,
                       string grupa = "", ProgramStudiu programStudiu = ProgramStudiu.Licenta)
        {
            IdStudent = idStudent;
            Nume = nume;
            Prenume = prenume;
            Grupa = grupa;
            ProgramStudiu = programStudiu;
            note = new int[0];
        }

        // Cerinta 3: constructorul extrage si Grupa si ProgramStudiu din sir
        public Student(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            this.IdStudent = Convert.ToInt32(dateFisier[ID]);
            this.Nume = dateFisier[NUME];
            this.Prenume = dateFisier[PRENUME];
            this.Grupa = dateFisier[GRUPA];

            if (Enum.TryParse<ProgramStudiu>(dateFisier[PROGRAM_STUDIU], out ProgramStudiu ps))
                this.ProgramStudiu = ps;
            else
                this.ProgramStudiu = ProgramStudiu.Licenta;

            ExtrageNote(dateFisier[NOTE], SEPARATOR_SECUNDAR_FISIER);
        }

        public string Info()
        {
            string sNote = string.Empty;
            if (note != null)
                sNote = string.Join(SEPARATOR_SECUNDAR_FISIER.ToString(), note);

            // Cerinta 1: Grupa afisata alaturi de celelalte date
            return $"Id:{IdStudent} Nume:{Nume ?? "NECUNOSCUT"} Prenume:{Prenume ?? "NECUNOSCUT"} " +
                   $"Grupa:{Grupa ?? "NECUNOSCUTA"} Program:{ProgramStudiu}  Note: {sNote}";
        }

        // Cerinta 2: include Grupa si ProgramStudiu in fisier
        public string ConversieLaSirPentruFisier()
        {
            string sNote = string.Empty;
            if (note != null)
                sNote = string.Join(SEPARATOR_SECUNDAR_FISIER.ToString(), note);

            // Ordinea in fisier: ID;Nume;Prenume;Grupa;ProgramStudiu;Note
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdStudent.ToString(),
                Nume ?? "NECUNOSCUT",
                Prenume ?? "NECUNOSCUT",
                Grupa ?? "",
                ProgramStudiu.ToString(),
                sNote);
        }

        public void ExtrageNote(string sirNote, char delimitator = ' ')
        {
            List<int> listaNote = new List<int>();
            foreach (var element in sirNote.Split(delimitator))
            {
                if (int.TryParse(element, out int nota) && ValideazaNota(nota) == SUCCES)
                    listaNote.Add(nota);
            }
            note = listaNote.ToArray();
        }

        private bool ValideazaNota(int nota)
        {
            return nota >= NOTA_MINIMA && nota <= NOTA_MAXIMA;
        }
    }
}
