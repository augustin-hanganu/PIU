using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareStudentiMemorie
    {
        private List<Student> studenti;

        public AdministrareStudentiMemorie()
        {
            studenti = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            student.IdStudent = GetNextIdStudent();
            studenti.Add(student);
        }

        public List<Student> GetStudenti()
        {
            return studenti;
        }

        public Student? GetStudent(int idStudent)
        {
            // LINQ in loc de foreach
            return studenti.FirstOrDefault(s => s.IdStudent == idStudent);
        }

        // Cerinta 3 - cautare dupa nume si prenume cu LINQ
        public Student? GetStudent(string nume, string prenume)
        {
            return studenti.FirstOrDefault(student =>
                student.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase) &&
                student.Prenume.Equals(prenume, StringComparison.OrdinalIgnoreCase)
            );
        }

        // Cerinta 4 - cautare dupa nume, returneaza lista cu LINQ
        public List<Student> GetStudentiDupaNume(string nume)
        {
            return studenti
                .Where(s => s.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool UpdateStudent(Student s)
        {
            throw new Exception("Optiunea UpdateStudent nu este implementata");
        }

        public int GetNextIdStudent()
        {
            if (studenti.Count == 0)
                return 1;
            return studenti.Last().IdStudent + 1;
        }
    }
}