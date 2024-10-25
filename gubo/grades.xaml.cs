using System.IO;
using System.Windows;
using System.Windows.Documents;
using KretaKlon.util;

namespace KretaKlon.gubo;

public partial class grades {

    private static readonly List<Student> Students = [];
    private static gradeInput _gradeInput = null!;
    
    public grades() {
        InitializeComponent();
        if (Students.IsEmpty()) Load();
        _gradeInput = new gradeInput(Students);
    }

    private static void Load() {
        var lines = File.ReadAllLines("output.csv");
        foreach (var line in lines) {
            var parts = line.Split(';');
            if (parts.Length < 2) continue;

            var studentName = parts[0];
            var subject = parts[1];

            var selectedStudent = Students.Find(student => student.Name == studentName);
            if (selectedStudent is not null) {
                selectedStudent.Subjects.Add(new Subject(subject));
                continue;
            }
            
            Students.Add(new Student(
                studentName,
                [new Subject(subject)]
            ));
        }
    }
    
    private void Bevitel_OnClick(object sender, RoutedEventArgs e) {
        _gradeInput.Show();
        Close();
    }

    private void Jegymegnezo_OnClick(object sender, RoutedEventArgs e) {
        new gradeViewer(_gradeInput.Students).Show();
        Close();
    }

    private void GoToMainWindow_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
}