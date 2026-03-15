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
            int ID = 0;
            int note = 0;

            do
            {
                Console.WriteLine("C. Citire informatii student de la tastatura");
                Console.WriteLine("I. Afisarea informatiilor despre ultimul student introdus");
                Console.WriteLine("A. Afisare studenti din lista");
                Console.WriteLine("F. Cauta student dupa nume si prenume");
                Console.WriteLine("N. Cauta studenti dupa nume");
                Console.WriteLine("S. Salvare student in lista");
                Console.WriteLine("Y. Schimbare ID si Note studenti");
                Console.WriteLine("X. Inchidere program");

                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "C":
                        studentNou = AdministrareStudentiMemorie.CitireStudentTastatura();
                        break;

                    case "I":
                        AdministrareStudentiMemorie.AfisareStudent(studentNou);
                        break;

                    case "A":
                        AdministrareStudentiMemorie.AfisareStudenti(administrare.studenti);
                        break;

                    case "S":
                        studentNou.IdStudent = administrare.studenti.Count + 1;
                        administrare.studenti.Add(studentNou);
                        Console.WriteLine("Student salvat.");
                        break;
                    case "F":
                        AdministrareStudentiMemorie.CautareStudent(administrare.studenti);
                        break;
                    case "N":
                        AdministrareStudentiMemorie.CautareStudentiNume(administrare.studenti);
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

       
    }
}
