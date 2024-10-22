using KretaKlon.gubo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KretaKlon
{
    public partial class tantargy : Window
    {
        List<List<string>> tanuloEsTantargyLista = new List<List<string>>();
        private List<tantargyy> subjects = new List<tantargyy>();
        private List<Diak> diakok = new List<Diak>();
        private const string FilePathReadingStudents = "studentsInformation.csv";
        private const string FilePath = "subjects.csv";
        private const string FilePathOutput = "output.csv";

        public tantargy()
        {
            InitializeComponent();
            LoadSubjects();
            LoadStudents();
            PopulateSubjectComboBox();
        }

        private void LoadStudents()
        {
            if (!File.Exists(FilePathReadingStudents))
            {

                File.Create(FilePathReadingStudents).Close();
            }

            var lines = File.ReadAllLines(FilePathReadingStudents);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                diakok.Add(new Diak
                {
                    Nev = parts[0]
                });
            }
            cmbStudents.Items.Clear();
            foreach (var line in diakok)
            {
                cmbStudents.Items.Add(new ComboBoxItem { Content = line.Nev });
            }
        }

        private void LoadSubjects() 
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                subjects.Add(new tantargyy
                {
                    Name = parts[0],
                    Grade = Convert.ToInt32(parts[1]),
                    Type = parts[2],
                    HoursPerWeek = Convert.ToInt32(parts[3])
                });
            }
        }


        private void PopulateSubjectComboBox()
        {
            cmbSubjectAssignment.Items.Clear();

            foreach (var subject in subjects)
            {
                cmbSubjectAssignment.Items.Add(new ComboBoxItem { Content = subject.Name });
            }
        }

        private void SaveSubject_Click(object sender, RoutedEventArgs e)
        {
            var subject = new tantargyy
            {
                Name = txtSubjectName.Text,
                Grade = int.Parse(((ComboBoxItem)cmbGrade.SelectedItem).Content.ToString()),
                Type = ((ComboBoxItem)cmbType.SelectedItem).Content.ToString(),
                HoursPerWeek = int.Parse(txtHoursPerWeek.Text)
            };

            subjects.Add(subject);
            SaveSubjectsToFile();
            PopulateSubjectComboBox();
            MessageBox.Show("Tantárgy mentve!");
        }

        private void SaveSubjectsToFile()
        {
            using (var writer = new StreamWriter(FilePath))
            {
                foreach (var subject in subjects)
                {
                    writer.WriteLine($"{subject.Name};{subject.Grade};{subject.Type};{subject.HoursPerWeek}");
                }
            }
        }



        private void DeleteSubject_Click(object sender, RoutedEventArgs e)
        {
            var subjectToDelete = subjects.FirstOrDefault(s => s.Name == txtSubjectName.Text);
            if (subjectToDelete != null)
            {
                subjects.Remove(subjectToDelete);
                SaveSubjectsToFile();
                MessageBox.Show("Tantárgy törölve!");
            }
            else
            {
                MessageBox.Show("Tantárgy nem található!");
            }
        }

        private void Mentes3feladathoz()
        {
            using (var writer = new StreamWriter(FilePathOutput))
            {
                foreach (var diak in diakok)
                {
                    writer.WriteLine($"{diak.Nev};");
                }
            }
        }
        private void ShowReports_Click(object sender, RoutedEventArgs e)
        {
            var report = string.Empty;
            var groupedSubjects = subjects.GroupBy(s => s.Grade);

            foreach (var group in groupedSubjects)
            {
                var grade = group.Key;
                var generalSubjects = group.Where(s => s.Type == "Közismereti").ToList();
                var vocationalSubjects = group.Where(s => s.Type == "Szakmai").ToList();
                var generalHours = generalSubjects.Sum(s => s.TotalHours);
                var vocationalHours = vocationalSubjects.Sum(s => s.TotalHours);

                report += $"Évfolyam {grade}:\n" +
                          $"Közismereti tantárgyak: {generalSubjects.Count}, Óraszám: {generalHours}\n" +
                          $"Szakmai tantárgyak: {vocationalSubjects.Count}, Óraszám: {vocationalHours}\n\n";
            }

            txtReports.Text = report;
        }


        private void AssignSubject_Click(object sender, RoutedEventArgs e)
        {
            var student = ((ComboBoxItem)cmbStudents.Items[cmbStudents.SelectedIndex]).Content.ToString();
            var subject = ((ComboBoxItem)cmbSubjectAssignment.Items[cmbSubjectAssignment.SelectedIndex]).Content.ToString();
            tanuloEsTantargyLista.Add(new List<string> { student, subject });
            MessageBox.Show($"{subject} hozzárendelve {student}-hez.");
            hozzarendelesKiiratas();
        }

        private void hozzarendelesKiiratas()
        {
            using (var writer = new StreamWriter(FilePathOutput))
            {
                foreach (var subject in tanuloEsTantargyLista)
                {
                    writer.WriteLine($"{subject[0]};{subject[1]};");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }


    }

    public class Diak
    {
        public string Nev;
    }

    public class tantargyy
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        public string Type { get; set; }
        public int HoursPerWeek { get; set; }
        public int TotalHours => CalculateTotalHours();


        private int CalculateTotalHours()
        {
            switch (Grade)
            {
                case 12:
                    return Type == "Közismereti" ? 31 * HoursPerWeek : 36 * HoursPerWeek;
                case 13:
                    return 31 * HoursPerWeek;
                default:
                    return 36 * HoursPerWeek;
            }
        }
    }
}
