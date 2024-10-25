using System.Windows;
using System.Windows.Controls;

namespace KretaKlon.gubo;

public partial class studentSelector {
    public Student SelectedStudent { get; private set; } = null!;
    private readonly List<Student> _students;

    public studentSelector(List<Student> students) {
        _students = students;
        InitializeComponent();

        Loaded += (_, _) => { foreach (var student in students) StudentComboBox.Items.Add(student.Name); };
    }


    private void SelectButton_OnClick(object sender, RoutedEventArgs e) {
        SelectedStudent = _students.Find(student => 
            student.Name == StudentComboBox.SelectedItem.ToString()
        ) ?? throw new Exception("No student selected");
        
        DialogResult = true;
        Close();
    }

    private void StudentComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        SelectButton.IsEnabled = true;
    }
}