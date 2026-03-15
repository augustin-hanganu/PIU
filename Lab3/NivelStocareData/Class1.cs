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
                Console.WriteLine("Numar invalid! Introduceti un număr pozitiv:");
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
                    {
                        Console.WriteLine("Nota invalida! Introduceti un numar intreg.");
                    }
                    else if (nota < 1 || nota > 10)
                    {
                        Console.WriteLine("Nota invalida! Introduceti o nota intre 1 si 10.");
                    }
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

        public static void AfisareStudent(Student student)
        {
            if (student == null)
            {
                Console.WriteLine("Nu ati citit niciun student!");
            }
            else
            {
                Console.WriteLine(student.Info());
            }

        }

        public static void AfisareStudenti(List<Student> studenti)
        {
            Console.WriteLine("Studentii sunt:");

            foreach (Student student in studenti)
            {
                AfisareStudent(student);
            }
        }
        public static void CautareStudent(List<Student> studenti)
        {
            Console.WriteLine("Dati numele si prenumele studentului cautat!");
            Console.Write("Numele:");
            string nume = Console.ReadLine();
            Console.Write("Prenumele:");
            string prenume = Console.ReadLine();
            bool gasit = false;
            foreach (Student student in studenti)
            {
                if (student.Nume == nume && student.Prenume == prenume)
                {
                    Console.WriteLine("Student gasit:");
                    Console.WriteLine($"Nume: {student.Nume}, Prenume: {student.Prenume}");
                    Console.WriteLine(student.Info());
                    gasit = true;
                }
            }
            if (!gasit)
            {
                Console.WriteLine("Studentul nu este in lista!");
            }
        }
        public static void CautareStudentiNume(List<Student> studenti)
        {
            Console.WriteLine("Introduceti numele studentului");
            Console.Write("Nume:");
            string nume = Console.ReadLine();
            bool gasit = false;
            foreach (Student student in studenti)
            {
                if (student.Nume == nume)
                {
                    Console.WriteLine("Student Gasit");
                    Console.WriteLine($"Nume: {student.Nume}, Prenume: {student.Prenume}");
                    Console.WriteLine(student.Info());
                    gasit = true;
                }
            }
            if (!gasit)
            {
                Console.WriteLine("Studentul nu este in lista!");
            }
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

/* Implementați si o noua metoda List<Student> GetStudenti() care va returna
   întreaga lista de studenți.Utilizați noile metode relocate in implementarea clasei Program.
   Noi deja avem aceasta functie cand apelam functia A din meniu */
    }
}
