using LibrarieModele;
namespace NivelStocareData
{
    public class AdministrareStudentiMemorie
    {
        public List<Student> studenti = new List<Student>();

        public static Student CitireStudentTastatura()
        {
            Console.WriteLine("Introduceti numele");
            string nume = Console.ReadLine();

            Console.WriteLine("Introduceti prenumele");
            string prenume = Console.ReadLine();

            Student student = new Student(0, nume, prenume);

            Console.WriteLine("Introduceti numarul de note:");
            int.TryParse(Console.ReadLine(), out int nrNote);
            while (nrNote <= 0)
            {
                Console.WriteLine("Numar invalid! Introduceti un numar pozitiv:");
                int.TryParse(Console.ReadLine(), out nrNote);
            }

            int[] note = new int[nrNote];
            for (int i = 0; i < nrNote; i++)
            {
                bool notaValida = false;
                while (!notaValida)
                {
                    Console.Write($"Nota {i + 1}: ");
                    bool rezultat = int.TryParse(Console.ReadLine(), out int nota);
                    if (!rezultat)
                        Console.WriteLine("Nota invalida! Introduceti un numar intreg.");
                    else if (nota < 1 || nota > 10)
                        Console.WriteLine("Nota invalida! Introduceti o nota intre 1 si 10.");
                    else
                    {
                        note[i] = nota;
                        notaValida = true;
                    }
                }
            }
            student.SetNote(note);
            return student;
        }

        public void AdaugaStudent(Student student)
        {
            student.IdStudent = studenti.Count + 1;
            studenti.Add(student);
        }

        public bool ModificaNoteStudent(int[] note, int idStudent)
        {
            foreach (Student student in studenti)
            {
                if (student.IdStudent == idStudent)
                {
                    student.SetNote(note);
                    Console.WriteLine($"Notele studentului {student.Nume} {student.Prenume} au fost modificate.");
                    return true;
                }
            }
            Console.WriteLine("Studentul cu acest ID nu a fost gasit!");
            return false;
        }

        public List<Student> GetStudenti() => studenti;
    }
}
