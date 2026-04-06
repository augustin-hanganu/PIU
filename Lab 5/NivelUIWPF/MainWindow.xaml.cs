using LibrarieModele;
using NivelStocareDate;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace NivelUIWPF
{
    public partial class MainWindow : Window
    {
        private readonly IStocareData _adminStudenti;

        public MainWindow()
        {
            InitializeComponent();
            _adminStudenti = StocareFactory.GetAdministratorStocare();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            string nume = txtNume.Text.Trim();
            string prenume = txtPrenume.Text.Trim();
            string grupa = txtGrupa.Text.Trim();

            if (string.IsNullOrEmpty(nume) || string.IsNullOrEmpty(prenume))
            {
                SetStatus("Numele si prenumele sunt obligatorii!", "Red");
                return;
            }

            ProgramStudiu programStudiu = (ProgramStudiu)cmbProgramStudiu.SelectedIndex;

            Student student = new Student(0, nume, prenume, grupa, programStudiu);

            string[] partiNote = txtNote.Text.Trim().Split(' ',
                System.StringSplitOptions.RemoveEmptyEntries);
            List<int> listaNote = new List<int>();
            foreach (string parte in partiNote)
            {
                if (int.TryParse(parte, out int nota))
                    listaNote.Add(nota);
            }
            student.SetNote(listaNote.ToArray());

            _adminStudenti.AddStudent(student);
            SetStatus($"Student {nume} {prenume} adaugat cu succes.", "Green");
            ClearForm();
        }

        private void btnAfisareToti_Click(object sender, RoutedEventArgs e)
        {
            List<Student> studenti = _adminStudenti.GetStudenti();
            lstStudenti.Items.Clear();

            if (studenti.Count == 0)
            {
                lstStudenti.Items.Add("Nu exista studenti salvati.");
                SetStatus("Lista goala.", "Gray");
                return;
            }

            foreach (Student s in studenti)
                lstStudenti.Items.Add(s.Info());

            SetStatus($"Total studenti: {studenti.Count}", "Gray");
        }

        private void btnAfisareFaraNote_Click(object sender, RoutedEventArgs e)
        {
            List<Student> studenti = _adminStudenti.GetStudenti();
            lstStudenti.Items.Clear();

            var faraNote = studenti.Where(s => s.GetNote().Length < 2).ToList();

            if (faraNote.Count == 0)
            {
                lstStudenti.Items.Add("Toti studentii au cel putin 2 note.");
                SetStatus("Niciun student fara note.", "Gray");
                return;
            }

            foreach (Student s in faraNote)
                lstStudenti.Items.Add(s.Info());

            SetStatus($"Studenti fara note: {faraNote.Count}", "Gray");
        }

        private void btnStergeLog_Click(object sender, RoutedEventArgs e)
        {
            lstStudenti.Items.Clear();
            SetStatus("Log sters.", "Gray");
        }

        private void ClearForm()
        {
            txtNume.Text = "";
            txtPrenume.Text = "";
            txtGrupa.Text = "";
            txtNote.Text = "";
            cmbProgramStudiu.SelectedIndex = 0;
            txtNume.Focus();
        }

        private void SetStatus(string mesaj, string culoare)
        {
            lblStatus.Content = mesaj;
            lblStatus.Foreground = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(culoare));
        }
    }
}