using System.Configuration;
using System.IO;
using LibraryGameAndUsers;

namespace NivelStoreData
{
    
    public static class StocareFactory
    {
        private const string FORMAT_SALVARE = "FormatSalvare";
        private const string NUME_FISIER_GAMES = "NumeFisierGames";
        private const string NUME_FISIER_USERS = "NumeFisierUsers";

        public static IStocareData GetAdministratorStocare()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "memorie";
            string numeFisierGames = ConfigurationManager.AppSettings[NUME_FISIER_GAMES] ?? "Games";
            string numeFisierUsers = ConfigurationManager.AppSettings[NUME_FISIER_USERS] ?? "Users";

            
            string locatieRadacina = Directory
                .GetParent(Directory.GetCurrentDirectory())!
                .Parent!.Parent!.FullName;

            switch (formatSalvare.ToLower())
            {
                case "txt":
                    string caleGames = Path.Combine(locatieRadacina, numeFisierGames + ".txt");
                    string caleUsers = Path.Combine(locatieRadacina, numeFisierUsers + ".txt");
                    return new AdministrareMarketplaceFisierText(caleGames, caleUsers);

                default: 
                    return new AdministrareMarketplaceMemorie();
            }
        }
    }
}