using LibrarieModele;
using LibrarieModele.Enums;
using NivelStocareDate;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NivelUIWPF
{
    public partial class MainWindow : Window
    {
        private const int LUNGIME_MAXIMA_NUME = 15;

        private IStocareData adminStudenti;

        public MainWindow()
        {
            InitializeComponent();

            adminStudenti = StocareFactory.GetAdministratorStocare();

            AfiseazaStudenti();
        }

        private void AfiseazaStudenti()
        {
            dgStudenti.ItemsSource = null;
            dgStudenti.ItemsSource = adminStudenti.GetStudenti();
        }

        private void btnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNume.Text.Trim();
            string prenume = txtPrenume.Text.Trim();
            string note = txtNote.Text.Trim();

            if (!ValideazaDateStudent(nume, prenume, note))
            {
                return;
            }

            Student student = new Student();

            student.Nume = nume;
            student.Prenume = prenume;

            student.ExtrageNote(note);

            if (rbAutomatica.IsChecked == true)
                student.ProgramSTD = ProgramStudiu.Automatica;

            else if (rbElectronica.IsChecked == true)
                student.ProgramSTD = ProgramStudiu.Electronica;

            else
                student.ProgramSTD = ProgramStudiu.Calculatoare;

            student.Discipline = new List<string>();

            if (cbPIU.IsChecked == true)
                student.Discipline.Add("PIU");

            if (cbPCLP.IsChecked == true)
                student.Discipline.Add("PCLP");

            if (cbPOO.IsChecked == true)
                student.Discipline.Add("POO");

            adminStudenti.AddStudent(student);

            AfiseazaStudenti();

            MessageBox.Show("Student adaugat cu succes!");

            ResetareCampuri();
        }

        private void btnReseteaza_Click(object sender, RoutedEventArgs e)
        {
            ResetareCampuri();
        }

        private void ResetareCampuri()
        {
            txtNume.Clear();
            txtPrenume.Clear();
            txtNote.Clear();

            cbPIU.IsChecked = false;
            cbPCLP.IsChecked = false;
            cbPOO.IsChecked = false;

            rbCalculatoare.IsChecked = true;

            ReseteazaErori();
        }

        private bool ValideazaDateStudent(string nume,
                                          string prenume,
                                          string note)
        {
            ReseteazaErori();

            bool valid = true;

            if (string.IsNullOrWhiteSpace(nume))
            {
                AfiseazaEroare(txtNume,
                               tbErrNume,
                               "Numele este obligatoriu!");

                valid = false;
            }

            else if (nume.Length > LUNGIME_MAXIMA_NUME)
            {
                AfiseazaEroare(txtNume,
                               tbErrNume,
                               "Maxim 15 caractere!");

                valid = false;
            }

            if (string.IsNullOrWhiteSpace(prenume))
            {
                AfiseazaEroare(txtPrenume,
                               tbErrPrenume,
                               "Prenumele este obligatoriu!");

                valid = false;
            }

            else if (prenume.Length > LUNGIME_MAXIMA_NUME)
            {
                AfiseazaEroare(txtPrenume,
                               tbErrPrenume,
                               "Maxim 15 caractere!");

                valid = false;
            }

            if (string.IsNullOrWhiteSpace(note))
            {
                AfiseazaEroare(txtNote,
                               tbErrNote,
                               "Introduceti notele!");

                valid = false;
            }

            return valid;
        }

        private void AfiseazaEroare(TextBox textBox,
                                    TextBlock textBlock,
                                    string mesaj)
        {
            textBox.BorderBrush = Brushes.Red;

            textBox.Background =
                new SolidColorBrush(Color.FromRgb(255, 220, 220));

            textBlock.Text = mesaj;

            textBlock.Visibility = Visibility.Visible;
        }

        private void ReseteazaErori()
        {
            AscundeEroare(txtNume, tbErrNume);
            AscundeEroare(txtPrenume, tbErrPrenume);
            AscundeEroare(txtNote, tbErrNote);
        }

        private void AscundeEroare(TextBox textBox,
                                   TextBlock textBlock)
        {
            textBox.ClearValue(Border.BorderBrushProperty);

            textBox.ClearValue(Control.BackgroundProperty);

            textBlock.Text = "";

            textBlock.Visibility = Visibility.Collapsed;
        }

        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtCautareNume.Text.Trim();

            dgStudentiGasiti.ItemsSource =
                adminStudenti.CautaStudentiDupaNume(nume);
        }

        private void btnMeniuAdauga_Click(object sender, RoutedEventArgs e)
        {
            panelAdauga.Visibility = Visibility.Visible;
            panelCauta.Visibility = Visibility.Collapsed;
        }

        private void btnMeniuCauta_Click(object sender, RoutedEventArgs e)
        {
            panelAdauga.Visibility = Visibility.Collapsed;
            panelCauta.Visibility = Visibility.Visible;
        }
    }
}