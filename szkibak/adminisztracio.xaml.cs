using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace StudentRegistration
{
    public partial class MainWindow : Window
    {
        private List<Student> students = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            if (File.Exists("students.csv"))
            {
                var lines = File.ReadAllLines("students.csv");
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 12)
                    {
                        var student = new Student
                        {
                            Name = parts[0],
                            BirthPlace = parts[1],
                            BirthDate = DateTime.Parse(parts[2]),
                            MotherName = parts[3],
                            Address = parts[4],
                            EnrollmentDate = DateTime.Parse(parts[5]),
                            Major = parts[6],
                            ClassGroup = parts[7],
                            IsBoarder = bool.Parse(parts[8]),
                            Dormitory = parts[9],
                            LogNumber = int.Parse(parts[10]),
                            RegNumber = parts[11]
                        };
                        students.Add(student);
                    }
                }
                UpdateDataGridView();
            }
        }

        private void SaveStudent(Student student)
        {
            using (var writer = new StreamWriter("studentsInformation.csv", true))
            {
                writer.WriteLine($"{student.Name},{student.BirthPlace},{student.BirthDate:yyyy-MM-dd},{student.MotherName}," +
                                 $"{student.Address},{student.EnrollmentDate:yyyy-MM-dd},{student.Major}," +
                                 $"{student.ClassGroup},{student.IsBoarder},{student.Dormitory},{student.LogNumber}," +
                                 $"{student.RegNumber}");
            }
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = txtName.Text;
                var birthPlace = txtBirthPlace.Text;
                var birthDate = DateTime.Parse(txtBirthDate.Text);
                var motherName = txtMotherName.Text;
                var address = txtAddress.Text;
                var enrollmentDate = DateTime.Parse(txtEnrollmentDate.Text);
                var major = txtMajor.Text;
                var classGroup = txtClassGroup.Text;
                var isBoarder = chkIsBoarder.IsChecked == true;
                var dormitory = isBoarder ? txtDormitory.Text : "";

                var logNumber = GenerateLogNumber(enrollmentDate, classGroup);
                var regNumber = $"{logNumber}/{enrollmentDate.Year}";

                var newStudent = new Student
                {
                    Name = name,
                    BirthPlace = birthPlace,
                    BirthDate = birthDate,
                    MotherName = motherName,
                    Address = address,
                    EnrollmentDate = enrollmentDate,
                    Major = major,
                    ClassGroup = classGroup,
                    IsBoarder = isBoarder,
                    Dormitory = dormitory,
                    LogNumber = logNumber,
                    RegNumber = regNumber
                };

                students.Add(newStudent);
                SaveStudent(newStudent);
                UpdateDataGridView();
                MessageBox.Show("Student added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}");
            }
        }

        private int GenerateLogNumber(DateTime enrollmentDate, string classGroup)
        {
            var year = enrollmentDate.Year;
            var filteredStudents = students.Where(s => s.ClassGroup == classGroup).ToList();
            filteredStudents.Sort((s1, s2) => s1.EnrollmentDate.CompareTo(s2.EnrollmentDate));

            int logNumber = filteredStudents.Count(s => s.EnrollmentDate < new DateTime(year, 9, 1)) + 1;
            logNumber += filteredStudents.Count(s => s.EnrollmentDate >= new DateTime(year, 9, 1));
            return logNumber;
        }

        private void UpdateDataGridView()
        {
            dataGridStudents.ItemsSource = null;
            dataGridStudents.ItemsSource = students;
        }

        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = dataGridStudents.SelectedItem as Student;
            if (selectedStudent != null)
            {
                if (MessageBox.Show($"Are you sure you want to delete {selectedStudent.Name}?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    students.Remove(selectedStudent);
                    File.WriteAllLines("students.csv", students.Select(s => $"{s.Name},{s.BirthPlace},{s.BirthDate:yyyy-MM-dd}," +
                        $"{s.MotherName},{s.Address},{s.EnrollmentDate:yyyy-MM-dd},{s.Major},{s.ClassGroup}," +
                        $"{s.IsBoarder},{s.Dormitory},{s.LogNumber},{s.RegNumber}"));
                    UpdateDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Select a student to delete.");
            }
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Major { get; set; }
        public string ClassGroup { get; set; }
        public bool IsBoarder { get; set; }
        public string Dormitory { get; set; }
        public int LogNumber { get; set; }
        public string RegNumber { get; set; }
    }
}
