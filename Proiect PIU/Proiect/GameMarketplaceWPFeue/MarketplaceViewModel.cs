using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibraryGameAndUsers;
using NivelStoreData;

namespace GameMarketplaceWPF
{
    // ViewModel pentru Game Marketplace
    // Implementeaza INotifyPropertyChanged pentru a notifica UI-ul la modificari
    // Folosit conform pattern-ului MVVM (Model - View - ViewModel)
    public class MarketplaceViewModel : INotifyPropertyChanged
    {
        private readonly IStocareData _stocare;

        // ===== COLECTII OBSERVABLE =====
        // ObservableCollection notifica automat UI-ul la Add/Remove/Clear
        // fara a mai fi nevoie de resetarea manuala a ItemsSource

        private ObservableCollection<Game> _jocuri;
        public ObservableCollection<Game> Jocuri
        {
            get => _jocuri;
            set { _jocuri = value; OnPropertyChanged(); }
        }

        private ObservableCollection<User> _useri;
        public ObservableCollection<User> Useri
        {
            get => _useri;
            set { _useri = value; OnPropertyChanged(); }
        }

        // ===== PROPRIETATI PENTRU STATISTICI =====
        // Legate in XAML prin Binding — se actualizeaza automat

        public int NrJocuri => Jocuri?.Count ?? 0;
        public int NrUseri => Useri?.Count ?? 0;

        // ===== JOCUL SELECTAT (pentru Modifica Joc) =====

        private Game _jocSelectat;
        public Game JocSelectat
        {
            get => _jocSelectat;
            set
            {
                _jocSelectat = value;
                OnPropertyChanged();
                // Notifica si proprietatile derivate din JocSelectat
                OnPropertyChanged(nameof(AreJocSelectat));
            }
        }

        public bool AreJocSelectat => _jocSelectat != null;

        // ===== USERUL SELECTAT (pentru Modifica / Sterge User) =====

        private User _userSelectat;
        public User UserSelectat
        {
            get => _userSelectat;
            set
            {
                _userSelectat = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AreUserSelectat));
            }
        }

        public bool AreUserSelectat => _userSelectat != null;

        // ===== CONSTRUCTOR =====

        public MarketplaceViewModel(IStocareData stocare)
        {
            _stocare = stocare;
            _jocuri = new ObservableCollection<Game>();
            _useri = new ObservableCollection<User>();
        }

        // ===== METODE DE INCARCARE =====

        public void IncarcaJocuri()
        {
            Jocuri.Clear();
            foreach (Game g in _stocare.GetGames())
                Jocuri.Add(g);
            OnPropertyChanged(nameof(NrJocuri));
        }

        public void IncarcaUseri()
        {
            Useri.Clear();
            foreach (User u in _stocare.GetUsers())
                Useri.Add(u);
            OnPropertyChanged(nameof(NrUseri));
        }

        // ===== OPERATII CRUD JOCURI =====

        public void AdaugaJoc(Game joc)
        {
            _stocare.AddGame(joc);
            Jocuri.Add(joc);
            OnPropertyChanged(nameof(NrJocuri));
        }

        public void ActualizeazaJoc(Game joc)
        {
            _stocare.UpdateGame(joc);
            IncarcaJocuri();  // reincarca pentru a reflecta modificarile
        }

        public void StergeJoc(int idJoc)
        {
            _stocare.DeleteGame(idJoc);
            Game deEliminat = Jocuri.FirstOrDefault(g => g.IdGame == idJoc);
            if (deEliminat != null)
            {
                Jocuri.Remove(deEliminat);
                OnPropertyChanged(nameof(NrJocuri));
            }
        }

        // ===== OPERATII CRUD USERI =====

        public void AdaugaUser(User user)
        {
            _stocare.AddUser(user);
            Useri.Add(user);
            OnPropertyChanged(nameof(NrUseri));
        }

        public void ActualizeazaUser(User user)
        {
            _stocare.UpdateUser(user);
            IncarcaUseri();
        }

        public void StergeUser(int idUser)
        {
            _stocare.DeleteUser(idUser);
            User deEliminat = Useri.FirstOrDefault(u => u.IdUser == idUser);
            if (deEliminat != null)
            {
                Useri.Remove(deEliminat);
                OnPropertyChanged(nameof(NrUseri));
            }
        }

        // ===== CAUTARE =====

        public List<Game> CautaJocuri(string titlu, GenJoc? gen)
        {
            IEnumerable<Game> rezultat = _stocare.GetGames();

            if (!string.IsNullOrWhiteSpace(titlu))
                rezultat = rezultat.Where(g =>
                    g.Titlu.ToLower().Contains(titlu.ToLower()));

            if (gen.HasValue)
                rezultat = rezultat.Where(g => g.Gen == gen.Value);

            return rezultat.ToList();
        }

        public List<User> CautaUseri(string nume)
        {
            IEnumerable<User> rezultat = _stocare.GetUsers();

            if (!string.IsNullOrWhiteSpace(nume))
                rezultat = rezultat.Where(u =>
                    u.Nume.ToLower().Contains(nume.ToLower()));

            return rezultat.ToList();
        }

        // ===== INotifyPropertyChanged =====

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}