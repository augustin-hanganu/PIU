using LibrarieModele;
using NivelStocareDate;

namespace EvidentaStudenti
{
    class Program
    {
        public static void Main()
        {
            AdministrareStudentiMemorie adminStudenti = new AdministrareStudentiMemorie();
            Student? studentNou = null;
            string optiune;

            List<Student> studenti = adminStudenti.GetStudenti();

            do
            {
                Console.WriteLine("\n=== EVIDENTA STUDENTI ===");
                Console.WriteLine("C. Citire informatii student de la tastatura");
                Console.WriteLine("I. Afisarea informatiilor despre ultimul student introdus");
                Console.WriteLine("A. Afisare studenti din lista");
                Console.WriteLine("Z. Afisare studenti fara note din lista");
                Console.WriteLine("S. Salvare student in lista");
                Console.WriteLine("N. Cauta student dupa nume si prenume");
                Console.WriteLine("L. Cauta studenti dupa nume (lista)");
                Console.WriteLine("X. Inchidere program");

                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "C":
                        studentNou = CitireStudentTastatura();
                        break;

                    case "I":
                        AfisareStudent(studentNou);
                        break;

                    case "A":
                        AfisareStudenti(studenti);
                        break;

                    case "Z":
                        AfisareStudentiFaraNote(studenti);
                        break;

                    case "S":
                        if (studentNou != null)
                        {
                            adminStudenti.AddStudent(studentNou);
                            Console.WriteLine("Student salvat.");
                        }
                        else
                        {
                            Console.WriteLine("Studentul nu a fost initializat");
                        }
                        break;

                    // Cerinta 3 - cautare dupa nume si prenume
                    case "N":
                        Console.Write("Nume: ");
                        string nume = Console.ReadLine() ?? string.Empty;
                        Console.Write("Prenume: ");
                        string prenume = Console.ReadLine() ?? string.Empty;
                        Student? gasit = adminStudenti.GetStudent(nume, prenume);
                        if (gasit != null)
                            AfisareStudent(gasit);
                        else
                            Console.WriteLine("Studentul nu a fost gasit!");
                        break;

                    // Cerinta 4 - cautare dupa nume, returneaza lista
                    case "L":
                        Console.Write("Nume cautat: ");
                        string numeCautat = Console.ReadLine() ?? string.Empty;
                        List<Student> rezultat = adminStudenti.GetStudentiDupaNume(numeCautat);
                        if (rezultat.Count == 0)
                            Console.WriteLine("Niciun student gasit!");
                        else
                            AfisareStudenti(rezultat);
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

        // Cerinta 1 + 2 - citire cu ProgramStudiu si tratare exceptii
        public static Student CitireStudentTastatura()
        {
            Console.WriteLine("Introduceti numele");
            string nume = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Introduceti prenumele");
            string prenume = Console.ReadLine() ?? string.Empty;

            // Cerinta 1 - afisare optiuni din enumerare
            Console.WriteLine("Alegeti programul de studiu:");
            foreach (ProgramStudiu ps in Enum.GetValues(typeof(ProgramStudiu)))
            {
                Console.WriteLine($"  {(int)ps} - {ps}");
            }

            // Cerinta 2 - tratare exceptii la citirea programului de studiu
            ProgramStudiu programStudiu = ProgramStudiu.Licenta;
            bool programValid = false;
            while (!programValid)
            {
                try
                {
                    Console.Write("Optiune program studiu: ");
                    string input = Console.ReadLine() ?? string.Empty;

                    // incearca sa parseze fie numarul (0) fie numele (Licenta)
                    if (!Enum.TryParse(input, ignoreCase: true, out programStudiu))
                    {
                        throw new ArgumentException($"Valoarea '{input}' nu este valida!");
                    }

                    // verifica daca valoarea e definita in enumerare
                    if (!Enum.IsDefined(typeof(ProgramStudiu), programStudiu))
                    {
                        throw new ArgumentException($"Programul de studiu ales nu exista!");
                    }

                    programValid = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Eroare: {ex.Message}");
                    Console.WriteLine("Va rugam introduceti o valoare valida!");
                }
            }

            Student student = new Student(0, nume, prenume, programStudiu);

            Console.WriteLine("Introduceti numarul de note:");
            int.TryParse(Console.ReadLine(), out int nrNote);

            int[] note = new int[nrNote];
            for (int i = 0; i < nrNote; i++)
            {
                Console.Write($"Nota {i + 1}: ");
                bool rezultat = int.TryParse(Console.ReadLine(), out int nota);
                if (rezultat)
                    note[i] = nota;
                else
                {
                    Console.WriteLine("Nota invalida, se va seta la 0.");
                    note[i] = 0;
                }
            }
            student.SetNote(note);

            return student;
        }

        public static void AfisareStudent(Student? student)
        {
            if (student == null)
            {
                Console.WriteLine("Studentul nu a fost initializat!");
                return;
            }
            Console.WriteLine(student.Info());
        }

        public static void AfisareStudenti(List<Student> studenti)
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

        public static void AfisareStudentiFaraNote(List<Student> studenti)
        {
            Console.WriteLine("Studenti fara note (mai putin de 2):");
            var studentiSelectati = studenti
                                    .Where(student => student.GetNote().Length < 2);

            bool gasit = false;
            foreach (Student student in studentiSelectati)
            {
                AfisareStudent(student);
                gasit = true;
            }
            if (!gasit)
                Console.WriteLine("Toti studentii au note!");
        }
    }
}
