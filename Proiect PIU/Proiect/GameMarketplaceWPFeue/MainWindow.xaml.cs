using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibraryGameAndUsers;
using NivelStoreData;

namespace GameMarketplaceWPF
{
    public partial class MainWindow : Window
    {
        // ===== CONSTANTE PENTRU VALIDARE (cerinta lab 7) =====
        private const int MAX_TITLU = 50;
        private const int MAX_DESCRIERE = 200;
        private const double PRET_MIN = 0;
        private const double PRET_MAX = 10000;
        private const int MAX_NUME_USER = 30;
        private const double SOLD_MIN = 0;
        private const double SOLD_MAX = 100000;

        // Culori pentru validare
        private static readonly SolidColorBrush CuloareEroare =
            new SolidColorBrush(Color.FromRgb(233, 69, 96));
        private static readonly SolidColorBrush CuloareNormala =
            new SolidColorBrush(Color.FromRgb(234, 234, 234));

        private IStocareData _stocare;
        private AdministrareMarketplace _administrare = new AdministrareMarketplace();

        public MainWindow()
        {
            InitializeComponent();
            _stocare = StocareFactory.GetAdministratorStocare();
            Loaded += (s, e) => { IncarcaJocuri(); IncarcaUseri(); };
        }

        // ===== INCARCARE DATE =====

        private void IncarcaJocuri()
        {
            var jocuri = _stocare.GetGames();
            lstJocuri.ItemsSource = null;
            lstJocuri.ItemsSource = jocuri;
            lblTotalJocuri.Content = $"{jocuri.Count} jocuri";
        }

        private void IncarcaUseri()
        {
            var useri = _stocare.GetUsers();
            lstUseri.ItemsSource = null;
            lstUseri.ItemsSource = useri;
            lblTotalUseri.Content = $"{useri.Count} utilizatori";
        }

        // ===== NAVIGARE MENIU VERTICAL (cerinta lab 8) =====

        private void AfiseazaPanel(Grid panelActiv)
        {
            panelJocuri.Visibility = Visibility.Collapsed;
            panelUseri.Visibility = Visibility.Collapsed;
            panelCauta.Visibility = Visibility.Collapsed;
            panelModificaJoc.Visibility = Visibility.Collapsed;
            panelModificaUser.Visibility = Visibility.Collapsed;
            panelCumpara.Visibility = Visibility.Collapsed;
            panelActiv.Visibility = Visibility.Visible;
        }

        private void ResetStilMeniu()
        {
            btnMenuJocuri.Style = (Style)FindResource("MenuBtn");
            btnMenuUseri.Style = (Style)FindResource("MenuBtn");
            btnMenuCauta.Style = (Style)FindResource("MenuBtn");
            btnMenuModificaJoc.Style = (Style)FindResource("MenuBtn");
            btnMenuModificaUser.Style = (Style)FindResource("MenuBtn");
            btnMenuCumpara.Style = (Style)FindResource("MenuBtn");
        }

        private void BtnMenuJocuri_Click(object sender, RoutedEventArgs e)
        {
            ResetStilMeniu();
            btnMenuJocuri.Style = (Style)FindResource("MenuBtnActiv");
            AfiseazaPanel(panelJocuri);
            IncarcaJocuri();
        }

        private void BtnMenuUseri_Click(object sender, RoutedEventArgs e)
        {
            ResetStilMeniu();
            btnMenuUseri.Style = (Style)FindResource("MenuBtnActiv");
            AfiseazaPanel(panelUseri);
            IncarcaUseri();
        }

        private void BtnMenuCauta_Click(object sender, RoutedEventArgs e)
        {
            ResetStilMeniu();
            btnMenuCauta.Style = (Style)FindResource("MenuBtnActiv");
            AfiseazaPanel(panelCauta);
            RuleazaCautare();
        }

        private void BtnMenuModificaJoc_Click(object sender, RoutedEventArgs e)
        {
            ResetStilMeniu();
            btnMenuModificaJoc.Style = (Style)FindResource("MenuBtnActiv");
            AfiseazaPanel(panelModificaJoc);
            // Incarca jocurile in ComboBox
            cmbSelecteazaJoc.ItemsSource = null;
            cmbSelecteazaJoc.ItemsSource = _stocare.GetGames();
            cmbSelecteazaJoc.SelectedItem = null;
            ResetFormModificaJoc();
        }

        private void BtnMenuModificaUser_Click(object sender, RoutedEventArgs e)
        {
            ResetStilMeniu();
            btnMenuModificaUser.Style = (Style)FindResource("MenuBtnActiv");
            AfiseazaPanel(panelModificaUser);
            // Incarca userii in ComboBox
            cmbSelecteazaUser.ItemsSource = null;
            cmbSelecteazaUser.ItemsSource = _stocare.GetUsers();
            cmbSelecteazaUser.SelectedItem = null;
            ResetFormModificaUser();
        }

        private void BtnMenuCumpara_Click(object sender, RoutedEventArgs e)
        {
            ResetStilMeniu();
            btnMenuCumpara.Style = (Style)FindResource("MenuBtnActiv");
            AfiseazaPanel(panelCumpara);
            lblRezultatCumparare.Content = string.Empty;
        }

        // ===== VALIDARE JOC (cerinta lab 7) =====

        private bool ValideazaDateGame()
        {
            bool valid = true;

            // Titlu
            if (string.IsNullOrWhiteSpace(txtTitlu.Text) ||
                txtTitlu.Text.Trim().Length > MAX_TITLU)
            {
                lblCampTitlu.Foreground = CuloareEroare;
                errTitlu.Text = txtTitlu.Text.Trim().Length > MAX_TITLU
                    ? $"Titlul nu poate depasi {MAX_TITLU} caractere!"
                    : "Titlul este obligatoriu!";
                errTitlu.Visibility = Visibility.Visible;
                valid = false;
            }
            else
            {
                lblCampTitlu.Foreground = CuloareNormala;
                errTitlu.Visibility = Visibility.Collapsed;
            }

            // Descriere
            if (string.IsNullOrWhiteSpace(txtDescriere.Text) ||
                txtDescriere.Text.Trim().Length > MAX_DESCRIERE)
            {
                lblCampDescriere.Foreground = CuloareEroare;
                errDescriere.Text = txtDescriere.Text.Trim().Length > MAX_DESCRIERE
                    ? $"Descrierea nu poate depasi {MAX_DESCRIERE} caractere!"
                    : "Descrierea este obligatorie!";
                errDescriere.Visibility = Visibility.Visible;
                valid = false;
            }
            else
            {
                lblCampDescriere.Foreground = CuloareNormala;
                errDescriere.Visibility = Visibility.Collapsed;
            }

            // Pret
            bool pretOk = double.TryParse(txtPret.Text, out double pret)
                          && pret >= PRET_MIN && pret <= PRET_MAX;
            lblCampPret.Foreground = pretOk ? CuloareNormala : CuloareEroare;
            errPret.Visibility = pretOk ? Visibility.Collapsed : Visibility.Visible;
            if (!pretOk) valid = false;

            // Platforme
            bool platformeOk = lstPlatformeAdauga.SelectedItems.Count > 0;
            lblCampPlatforme.Foreground = platformeOk ? CuloareNormala : CuloareEroare;
            errPlatforme.Visibility = platformeOk ? Visibility.Collapsed : Visibility.Visible;
            if (!platformeOk) valid = false;

            return valid;
        }

        // ===== VALIDARE UTILIZATOR (cerinta lab 7) =====

        private bool ValideazaDateUser()
        {
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtNumeUser.Text) ||
                txtNumeUser.Text.Trim().Length > MAX_NUME_USER)
            {
                lblCampNume.Foreground = CuloareEroare;
                errNume.Text = txtNumeUser.Text.Trim().Length > MAX_NUME_USER
                    ? $"Numele nu poate depasi {MAX_NUME_USER} caractere!"
                    : "Numele este obligatoriu!";
                errNume.Visibility = Visibility.Visible;
                valid = false;
            }
            else
            {
                lblCampNume.Foreground = CuloareNormala;
                errNume.Visibility = Visibility.Collapsed;
            }

            bool soldOk = double.TryParse(txtSold.Text, out double sold)
                          && sold >= SOLD_MIN && sold <= SOLD_MAX;
            lblCampSold.Foreground = soldOk ? CuloareNormala : CuloareEroare;
            errSold.Visibility = soldOk ? Visibility.Collapsed : Visibility.Visible;
            if (!soldOk) valid = false;

            return valid;
        }

        // ===== PRELUARE GEN DIN RADIOBUTTON (cerinta lab 8) =====

        private GenJoc GetGenSelectat()
        {
            if (rbFPS.IsChecked == true) return GenJoc.FPS;
            if (rbSport.IsChecked == true) return GenJoc.Sport;
            if (rbStrategie.IsChecked == true) return GenJoc.Strategie;
            if (rbAventura.IsChecked == true) return GenJoc.Aventura;
            if (rbSimulare.IsChecked == true) return GenJoc.Simulare;
            return GenJoc.RPG; // default
        }

        // ===== PRELUARE TIP CONT DIN RADIOBUTTON (cerinta lab 8) =====

        private TipCont GetTipContSelectat()
        {
            if (rbPremium.IsChecked == true) return TipCont.Premium;
            if (rbAdmin.IsChecked == true) return TipCont.Admin;
            return TipCont.Standard;
        }

        // ===== PRELUARE PLATFORME DIN LISTBOX (cerinta lab 9) =====

        private PlatformaJoc GetPlatformeSelectate(ListBox listBox)
        {
            PlatformaJoc platforme = PlatformaJoc.None;
            foreach (ListBoxItem item in listBox.SelectedItems)
            {
                switch (item.Content.ToString())
                {
                    case "PC": platforme |= PlatformaJoc.PC; break;
                    case "PlayStation": platforme |= PlatformaJoc.PlayStation; break;
                    case "Xbox": platforme |= PlatformaJoc.Xbox; break;
                    case "Nintendo": platforme |= PlatformaJoc.Nintendo; break;
                }
            }
            return platforme;
        }

        // ===== ADAUGA JOC =====

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (!ValideazaDateGame()) return;

            GenJoc gen = GetGenSelectat();
            PlatformaJoc platforme = GetPlatformeSelectate(lstPlatformeAdauga);
            double.TryParse(txtPret.Text, out double pret);

            Game jocNou = new Game(0, txtTitlu.Text.Trim(), gen,
                                   txtDescriere.Text.Trim(), pret, platforme);
            _stocare.AddGame(jocNou);
            ResetFormJoc();
            IncarcaJocuri();

            MessageBox.Show($"Jocul '{jocNou.Titlu}' a fost adaugat!",
                "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnResetFormJoc_Click(object sender, RoutedEventArgs e)
            => ResetFormJoc();

        private void ResetFormJoc()
        {
            txtTitlu.Text = string.Empty;
            txtDescriere.Text = string.Empty;
            txtPret.Text = string.Empty;
            rbRPG.IsChecked = true;
            lstPlatformeAdauga.SelectedItems.Clear();

            lblCampTitlu.Foreground = CuloareNormala;
            lblCampDescriere.Foreground = CuloareNormala;
            lblCampPret.Foreground = CuloareNormala;
            lblCampPlatforme.Foreground = CuloareNormala;

            errTitlu.Visibility = errDescriere.Visibility =
            errPret.Visibility = errPlatforme.Visibility = Visibility.Collapsed;
        }

        // ===== ADAUGA UTILIZATOR =====

        private void BtnAdaugaUser_Click(object sender, RoutedEventArgs e)
        {
            if (!ValideazaDateUser()) return;

            TipCont tipCont = GetTipContSelectat();

            PreferinteUser preferinte = PreferinteUser.None;
            if (chkNotificari.IsChecked == true) preferinte |= PreferinteUser.Notificari;
            if (chkNewsletter.IsChecked == true) preferinte |= PreferinteUser.Newsletter;
            if (chkReduceri.IsChecked == true) preferinte |= PreferinteUser.Reduceri;
            if (chkAutoRenew.IsChecked == true) preferinte |= PreferinteUser.AutoRenew;

            double.TryParse(txtSold.Text, out double sold);

            User userNou = new User(0, txtNumeUser.Text.Trim(), sold, tipCont, preferinte);
            _stocare.AddUser(userNou);
            ResetFormUser();
            IncarcaUseri();

            MessageBox.Show($"Utilizatorul '{userNou.Nume}' a fost inregistrat!",
                "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnResetFormUser_Click(object sender, RoutedEventArgs e)
            => ResetFormUser();

        private void ResetFormUser()
        {
            txtNumeUser.Text = string.Empty;
            txtSold.Text = string.Empty;
            rbStandard.IsChecked = true;
            chkNotificari.IsChecked = chkNewsletter.IsChecked =
            chkReduceri.IsChecked = chkAutoRenew.IsChecked = false;

            lblCampNume.Foreground = CuloareNormala;
            lblCampSold.Foreground = CuloareNormala;

            errNume.Visibility = errSold.Visibility = Visibility.Collapsed;
        }

        // ===== CAUTARE (cerinta lab 8: dupa titlu joc si dupa nume user, DataGrid) =====

        private void BtnCauta_Click(object sender, RoutedEventArgs e)
            => RuleazaCautare();

        private void BtnResetCauta_Click(object sender, RoutedEventArgs e)
        {
            txtCautaTitlu.Text = string.Empty;
            txtCautaNume.Text = string.Empty;
            cmbCautaGen.SelectedIndex = 0;
            RuleazaCautare();
        }

        private void RuleazaCautare()
        {
            // Cautare jocuri dupa titlu si gen
            string titlu = txtCautaTitlu.Text.Trim().ToLower();

            IEnumerable<Game> jocuri = _stocare.GetGames();

            if (!string.IsNullOrEmpty(titlu))
                jocuri = jocuri.Where(g => g.Titlu.ToLower().Contains(titlu));

            if (cmbCautaGen.SelectedIndex > 0)
            {
                GenJoc gen = (GenJoc)(cmbCautaGen.SelectedIndex - 1);
                jocuri = jocuri.Where(g => g.Gen == gen);
            }

            var listaJocuri = jocuri.ToList();
            dgJocuri.ItemsSource = listaJocuri;
            lblNiciunJoc.Visibility = listaJocuri.Count == 0
                                       ? Visibility.Visible : Visibility.Collapsed;

            // Cautare utilizatori dupa nume
            string nume = txtCautaNume.Text.Trim().ToLower();

            IEnumerable<User> useri = _stocare.GetUsers();

            if (!string.IsNullOrEmpty(nume))
                useri = useri.Where(u => u.Nume.ToLower().Contains(nume));

            var listaUseri = useri.ToList();
            dgUseri.ItemsSource = listaUseri;
            lblNiciunUser.Visibility = listaUseri.Count == 0
                                       ? Visibility.Visible : Visibility.Collapsed;
        }

        // ===== CUMPARA JOC =====

        private void BtnCumpara_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdUser.Text, out int idUser))
            {
                lblRezultatCumparare.Foreground = CuloareEroare;
                lblRezultatCumparare.Content = "ID utilizator invalid!";
                return;
            }
            if (!int.TryParse(txtIdGame.Text, out int idGame))
            {
                lblRezultatCumparare.Foreground = CuloareEroare;
                lblRezultatCumparare.Content = "ID joc invalid!";
                return;
            }

            var oldOut = Console.Out;
            using var sw = new StringWriter();
            Console.SetOut(sw);
            _administrare.CumparaJoc(idUser, idGame, _stocare);
            Console.SetOut(oldOut);
            string mesaj = sw.ToString().Trim();

            bool succes = mesaj.Contains("succes", StringComparison.OrdinalIgnoreCase);
            lblRezultatCumparare.Foreground = succes
                ? new SolidColorBrush(Color.FromRgb(78, 204, 163))
                : CuloareEroare;
            lblRezultatCumparare.Content = mesaj;

            if (succes)
            {
                txtIdUser.Text = string.Empty;
                txtIdGame.Text = string.Empty;
                IncarcaUseri();
            }
        }

        // ===== MODIFICA JOC (cerinta lab 9) =====

        // La selectia unui joc din ComboBox, campurile se completeaza automat
        private void CmbSelecteazaJoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSelecteazaJoc.SelectedItem is not Game joc) return;

            txtModTitlu.Text = joc.Titlu;
            txtModPret.Text = joc.Pret.ToString();
            txtModDescriere.Text = joc.Descriere;
            lblRezModJoc.Content = string.Empty;

            // Marcam platformele curente in ListBox
            lstPlatformeModifica.SelectedItems.Clear();
            foreach (ListBoxItem item in lstPlatformeModifica.Items)
            {
                string continut = item.Content.ToString() ?? string.Empty;
                bool selectat = continut switch
                {
                    "PC" => joc.Platforme.HasFlag(PlatformaJoc.PC),
                    "PlayStation" => joc.Platforme.HasFlag(PlatformaJoc.PlayStation),
                    "Xbox" => joc.Platforme.HasFlag(PlatformaJoc.Xbox),
                    "Nintendo" => joc.Platforme.HasFlag(PlatformaJoc.Nintendo),
                    _ => false
                };
                if (selectat)
                    lstPlatformeModifica.SelectedItems.Add(item);
            }

            // Reset erori
            errModTitlu.Visibility = errModPret.Visibility =
            errModDescriere.Visibility = Visibility.Collapsed;
            lblModTitlu.Foreground = lblModPret.Foreground =
            lblModDescriere.Foreground = CuloareNormala;
        }

        private void BtnActualizeazaJoc_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSelecteazaJoc.SelectedItem is not Game jocSelectat)
            {
                MessageBox.Show("Selecteaza un joc din lista!", "Atentie",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validare
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtModTitlu.Text) ||
                txtModTitlu.Text.Trim().Length > MAX_TITLU)
            {
                lblModTitlu.Foreground = CuloareEroare;
                errModTitlu.Visibility = Visibility.Visible;
                valid = false;
            }
            else
            {
                lblModTitlu.Foreground = CuloareNormala;
                errModTitlu.Visibility = Visibility.Collapsed;
            }

            bool pretOk = double.TryParse(txtModPret.Text, out double pretNou)
                          && pretNou >= PRET_MIN && pretNou <= PRET_MAX;
            lblModPret.Foreground = pretOk ? CuloareNormala : CuloareEroare;
            errModPret.Visibility = pretOk ? Visibility.Collapsed : Visibility.Visible;
            if (!pretOk) valid = false;

            if (string.IsNullOrWhiteSpace(txtModDescriere.Text) ||
                txtModDescriere.Text.Trim().Length > MAX_DESCRIERE)
            {
                lblModDescriere.Foreground = CuloareEroare;
                errModDescriere.Visibility = Visibility.Visible;
                valid = false;
            }
            else
            {
                lblModDescriere.Foreground = CuloareNormala;
                errModDescriere.Visibility = Visibility.Collapsed;
            }

            if (!valid) return;

            // Aplicam modificarile pe obiectul selectat
            jocSelectat.Titlu = txtModTitlu.Text.Trim();
            jocSelectat.Pret = pretNou;
            jocSelectat.Descriere = txtModDescriere.Text.Trim();
            jocSelectat.Platforme = GetPlatformeSelectate(lstPlatformeModifica);

            _stocare.UpdateGame(jocSelectat);

            // Reincarca ComboBox cu datele actualizate
            cmbSelecteazaJoc.ItemsSource = null;
            cmbSelecteazaJoc.ItemsSource = _stocare.GetGames();
            cmbSelecteazaJoc.SelectedItem = null;

            lblRezModJoc.Content = $"Jocul '{jocSelectat.Titlu}' a fost actualizat!";
            ResetFormModificaJoc();
        }

        private void ResetFormModificaJoc()
        {
            txtModTitlu.Text = string.Empty;
            txtModPret.Text = string.Empty;
            txtModDescriere.Text = string.Empty;
            lstPlatformeModifica.SelectedItems.Clear();

            lblModTitlu.Foreground = lblModPret.Foreground =
            lblModDescriere.Foreground = CuloareNormala;
            errModTitlu.Visibility = errModPret.Visibility =
            errModDescriere.Visibility = Visibility.Collapsed;
        }

        // ===== MODIFICA USER (cerinta lab 9) =====

        // La selectia unui user din ComboBox, campurile se completeaza automat
        private void CmbSelecteazaUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSelecteazaUser.SelectedItem is not User user)
            {
                btnStergeUser.IsEnabled = false;
                return;
            }

            txtModNume.Text = user.Nume;
            txtModSold.Text = user.Sold.ToString();
            lblRezModUser.Content = string.Empty;

            rbModStandard.IsChecked = user.TipCont == TipCont.Standard;
            rbModPremium.IsChecked = user.TipCont == TipCont.Premium;
            rbModAdmin.IsChecked = user.TipCont == TipCont.Admin;

            dtpModDataInregistrare.SelectedDate = DateTime.Today;

            // Activeaza butonul Sterge cand e un user selectat
            btnStergeUser.IsEnabled = true;

            errModNume.Visibility = errModSold.Visibility = Visibility.Collapsed;
            lblModNume.Foreground = lblModSold.Foreground = CuloareNormala;
        }

        private void BtnActualizeazaUser_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSelecteazaUser.SelectedItem is not User userSelectat)
            {
                MessageBox.Show("Selecteaza un utilizator din lista!", "Atentie",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validare
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtModNume.Text) ||
                txtModNume.Text.Trim().Length > MAX_NUME_USER)
            {
                lblModNume.Foreground = CuloareEroare;
                errModNume.Visibility = Visibility.Visible;
                valid = false;
            }
            else
            {
                lblModNume.Foreground = CuloareNormala;
                errModNume.Visibility = Visibility.Collapsed;
            }

            bool soldOk = double.TryParse(txtModSold.Text, out double soldNou)
                          && soldNou >= SOLD_MIN && soldNou <= SOLD_MAX;
            lblModSold.Foreground = soldOk ? CuloareNormala : CuloareEroare;
            errModSold.Visibility = soldOk ? Visibility.Collapsed : Visibility.Visible;
            if (!soldOk) valid = false;

            if (!valid) return;

            // Data din DatePicker (cerinta lab 9)
            DateTime dataInregistrare = dtpModDataInregistrare.SelectedDate ?? DateTime.Today;

            // Tip cont din RadioButton
            TipCont tipContNou = TipCont.Standard;
            if (rbModPremium.IsChecked == true) tipContNou = TipCont.Premium;
            if (rbModAdmin.IsChecked == true) tipContNou = TipCont.Admin;

            // Aplicam modificarile
            userSelectat.Nume = txtModNume.Text.Trim();
            userSelectat.Sold = soldNou;
            userSelectat.TipCont = tipContNou;

            _stocare.UpdateUser(userSelectat);

            // Reincarca ComboBox
            cmbSelecteazaUser.ItemsSource = null;
            cmbSelecteazaUser.ItemsSource = _stocare.GetUsers();
            cmbSelecteazaUser.SelectedItem = null;

            lblRezModUser.Content = $"Utilizatorul '{userSelectat.Nume}' a fost actualizat!" +
                                    $" (Data: {dataInregistrare:dd.MM.yyyy})";
            ResetFormModificaUser();
            IncarcaUseri();
        }

        private void ResetFormModificaUser()
        {
            txtModNume.Text = string.Empty;
            txtModSold.Text = string.Empty;
            rbModStandard.IsChecked = true;
            dtpModDataInregistrare.SelectedDate = null;
            btnStergeUser.IsEnabled = false;

            lblModNume.Foreground = lblModSold.Foreground = CuloareNormala;
            errModNume.Visibility = errModSold.Visibility = Visibility.Collapsed;
        }

        // ===== STERGE USER - Delete (CRUD complet cerinta lab 10) =====

        private void BtnStergeUser_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSelecteazaUser.SelectedItem is not User userSelectat) return;

            MessageBoxResult confirmare = MessageBox.Show(
                $"Esti sigur ca vrei sa stergi utilizatorul '{userSelectat.Nume}'?\nAceasta actiune nu poate fi anulata.",
                "Confirmare stergere",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirmare != MessageBoxResult.Yes) return;

            _stocare.DeleteUser(userSelectat.IdUser);

            // Reincarca ComboBox si lista
            cmbSelecteazaUser.ItemsSource = null;
            cmbSelecteazaUser.ItemsSource = _stocare.GetUsers();
            cmbSelecteazaUser.SelectedItem = null;

            ResetFormModificaUser();
            IncarcaUseri();

            lblRezModUser.Content = $"Utilizatorul '{userSelectat.Nume}' a fost sters!";
            lblRezModUser.Foreground = CuloareEroare;
        }
    }
}