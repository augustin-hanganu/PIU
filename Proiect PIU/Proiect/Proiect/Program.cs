using LibraryGameAndUsers;
using NivelStoreData;

namespace GameMarketplace
{
    class Program
    {
        static void Main()
        {
            AdministrareMarketplace administrare = new AdministrareMarketplace();
            string optiune;

            do
            {
                Console.WriteLine("\n=== GAME MARKETPLACE ===");
                Console.WriteLine("A. Adauga joc in marketplace");
                Console.WriteLine("B. Afiseaza toate jocurile");
                Console.WriteLine("C. Cauta joc dupa titlu");
                Console.WriteLine("D. Cauta joc dupa gen");
                Console.WriteLine("E. Inregistreaza utilizator");
                Console.WriteLine("F. Afiseaza utilizatori");
                Console.WriteLine("G. Cumpara joc");
                Console.WriteLine("X. Iesire");
                Console.Write("Optiune: ");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "A":
                        Game gameNou = AdministrareMarketplace.CitireGameTastatura();
                        administrare.AdaugaGame(gameNou);
                        break;
                    case "B":
                        administrare.AfisareToateGameurile();
                        break;
                    case "C":
                        administrare.CautareGameDupaTitlu();
                        break;
                    case "D":
                        administrare.CautareGameDupaGen();
                        break;
                    case "E":
                        User userNou = AdministrareMarketplace.CitireUserTastatura();
                        administrare.AdaugaUser(userNou);
                        break;
                    case "F":
                        administrare.AfisareUtilizatori();
                        break;
                    case "G":
                        Console.Write("ID utilizator: ");
                        int.TryParse(Console.ReadLine(), out int idUser);
                        Console.Write("ID joc: ");
                        int.TryParse(Console.ReadLine(), out int idGame);
                        administrare.CumparaJoc(idUser, idGame);
                        break;
                    case "X":
                        Console.WriteLine("La revedere!");
                        return;
                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }

            } while (optiune != "X");
        }
    }
}