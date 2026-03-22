using LibrarieModele;
using NivelStocareData;

namespace EvidentaStudenti
{
    class Program
    {
        public static void Main()
        {
            AdministrareStudentiMemorie administrare = new AdministrareStudentiMemorie();
            Student? studentNou = null;
            string optiune;

            do
            {
                Console.WriteLine("\n=== EVIDENTA STUDENTI ===");
                Console.WriteLine("C. Citire informatii student de la tastatura");
                Console.WriteLine("I. Afisarea informatiilor despre ultimul student introdus");
                Console.WriteLine("A. Afisare studenti din lista");
                Console.WriteLine("F. Cauta student dupa nume si prenume");
                Console.WriteLine("N. Cauta studenti dupa nume");
                Console.WriteLine("S. Salvare student in lista");
                Console.WriteLine("Y. Schimbare note student dupa ID");
                Console.WriteLine("X. Inchidere program");
                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "C":
                        studentNou = AdministrareStudentiMemorie.CitireStudentTastatura();
                        break;

                    case "I":
                        AfisareStudent(studentNou);
                        break;

                    case "A":
                        AfisareStudenti(administrare.GetStudenti());
                        break;

                    case "S":
                        if (studentNou != null)
                        {
                            administrare.AdaugaStudent(studentNou);
                            Console.WriteLine("Student salvat.");
                        }
                        else
                            Console.WriteLine("Nu ati citit niciun student!");
                        break;

                    case "F":
                        CautareStudent(administrare.GetStudenti());
                        break;

                    case "N":
                        CautareStudentiNume(administrare.GetStudenti());
                        break;

                    case "Y":
                        Console.WriteLine("Introduceti ID-ul studentului:");
                        int.TryParse(Console.ReadLine(), out int id);
                        Console.WriteLine("Introduceti numarul de note noi:");
                        int.TryParse(Console.ReadLine(), out int nrNote);
                        int[] noteNoi = new int[nrNote];
                        for (int i = 0; i < nrNote; i++)
                        {
                            Console.Write($"Nota {i + 1}: ");
                            int.TryParse(Console.ReadLine(), out noteNoi[i]);
                        }
                        administrare.ModificaNoteStudent(noteNoi, id);
                        break;

                    case "X":
                        Console.WriteLine("Aplicatia va fi inchisa");
                        return;

                    default:
                        Console.WriteLine("Optiune inexistenta");
                        break;
                }
            } while (optiune.ToUpper() != "X");

            Console.ReadKey();
        }

        // ===== Functii de afisare mutate din AdministrareStudentiMemorie =====

        static void AfisareStudent(Student? student)
        {
            if (student == null)
                Console.WriteLine("Nu ati citit niciun student!");
            else
                Console.WriteLine(student.Info());
        }

        static void AfisareStudenti(List<Student> studenti)
        {
            if (studenti.Count == 0)
            {
                Console.WriteLine("Nu exista studenti in lista!");
                return;
            }
            Console.WriteLine("Studentii sunt:");
            foreach (Student student in studenti)
                AfisareStudent(student);
        }

        // ===== Functii de cautare mutate din AdministrareStudentiMemorie =====

        static void CautareStudent(List<Student> studenti)
        {
            Console.WriteLine("Dati numele si prenumele studentului cautat!");
            Console.Write("Numele: ");
            string nume = Console.ReadLine() ?? string.Empty;
            Console.Write("Prenumele: ");
            string prenume = Console.ReadLine() ?? string.Empty;

            bool gasit = false;
            foreach (Student student in studenti)
            {
                if (student.Nume == nume && student.Prenume == prenume)
                {
                    Console.WriteLine("Student gasit:");
                    AfisareStudent(student);
                    gasit = true;
                }
            }
            if (!gasit)
                Console.WriteLine("Studentul nu este in lista!");
        }

        static void CautareStudentiNume(List<Student> studenti)
        {
            Console.WriteLine("Introduceti numele studentului");
            Console.Write("Nume: ");
            string nume = Console.ReadLine() ?? string.Empty;

            bool gasit = false;
            foreach (Student student in studenti)
            {
                if (student.Nume == nume)
                {
                    Console.WriteLine("Student gasit:");
                    AfisareStudent(student);
                    gasit = true;
                }
            }
            if (!gasit)
                Console.WriteLine("Niciun student cu acest nume nu a fost gasit!");
        }
    }
}
