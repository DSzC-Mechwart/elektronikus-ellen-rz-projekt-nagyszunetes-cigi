using System.Windows;
using KretaKlon.gubo;

namespace KretaKlon;

public partial class grades {
    private static readonly List<Subject> Subjects = [
        new Subject([], "Tori"),
        new Subject([], "Irodalom"),
        new Subject([], "Matek"),
        new Subject([], "Nyelvtan"),
        new Subject([], "Fizika"),
        new Subject([], "Szakmai Angol"),
    ];
    
    public Student KisPista = new Student(Subjects);
    public Student KukoricaJanos = new Student(Subjects);
    public Student PetofiSandor = new Student(Subjects);
    public Student KossuthLajos = new Student(Subjects);
    
    public grades() {
        InitializeComponent();
    }

    private void Bevitel_OnClick(object sender, RoutedEventArgs e) {
        new gradeInput().Show();
        Close();
    }

    private void Jegymegnezo_OnClick(object sender, RoutedEventArgs e) {
        new gradeViewer().Show();
        Close();
    }
}