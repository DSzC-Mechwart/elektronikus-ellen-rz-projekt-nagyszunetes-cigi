using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace KretaKlon.gubo;

public partial class gradeInput {
    public readonly List<Student> Students;
    
    public gradeInput(List<Student> students) {
        Students = students;
        InitializeComponent();
        Load();
    }
    
    private void Load() {
        StudentComboBox.Items.Clear();
        SubjectComboBox.Items.Clear();
        foreach (var student in Students) {
            StudentComboBox.Items.Add(student.Name);
        }
    }
    
    private void FilterToOnlyNumbers_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = IsTextAllowed(e.Text);
        return;

        static bool IsTextAllowed(string text)
        {
            var regex = MyRegex();
            return regex.IsMatch(text);
        }
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        new grades().Show();
        Close();
    }

    private void AddGradeButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedStudentIndex = StudentComboBox.SelectedIndex;
        var selectedSubjectIndex = SubjectComboBox.SelectedIndex;

        if (selectedStudentIndex == -1 || selectedSubjectIndex == -1) return;

        var selectedStudent = Students[selectedStudentIndex];
        var selectedSubject = selectedStudent.Subjects[selectedSubjectIndex];
        
        var newGrade = new GradeClass(int.Parse(GradeTextBox.Text), ThemeTextBox.Text, TypeTextBox.Text);

        selectedSubject.Grades.Add(newGrade);

        RefreshCurrentDataGrid(selectedStudent, selectedSubject, selectedSubjectIndex);
    }

    private void tanuloComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        var selectedStudent = Students[StudentComboBox.SelectedIndex];
        
        var login = new Login {
            Tanulo = selectedStudent.Name
        };
        login.ShowDialog();
        
        LoadStudentSubjects(selectedStudent);

        foreach (var subject in selectedStudent.Subjects) {
            SubjectComboBox.Items.Add(subject.Name);
        }
    }

    private void SubjectComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        var comboBox = (ComboBox)sender;
        SubjectTabs.SelectedIndex = comboBox.SelectedIndex;
    }
    
    private void RefreshCurrentDataGrid(Student currentStudent, Subject currentSubject, int selectedSubjectIndex)
    {
        var selectedStudentIndex = StudentComboBox.SelectedIndex;
        if (selectedStudentIndex == -1) return;

        var selectedGrades = currentStudent
            .Subjects
            .Where(subject => 
                subject.Name == currentSubject.Name
            ).SelectMany(subject => 
                subject.Grades
            ).ToList();

        var selectedGrid = (SubjectTabs.Items[selectedSubjectIndex] as TabItem)?.Content as DataGrid ?? throw new Exception("DataGrid is null");
        
        selectedGrid.ItemsSource = null;
        selectedGrid.ItemsSource = selectedGrades;
        
        foreach (TabItem tabItem in SubjectTabs.Items) {
            (tabItem.Content as DataGrid)?.Items.Refresh();
        }
    }

    private void LoadStudentSubjects(Student student) {
        SubjectTabs.Items.Clear();

        foreach (var tabItem in student.Subjects.Select(subject => 
            new TabItem { Header = subject.Name, Content = CreateDataGridForSubject(subject), Name = ToValidTabName(subject.Name)}
        )) {
            SubjectTabs.Items.Add(tabItem);
        }
    }

    private static string ToValidTabName(string name) => $"{name.Replace(".", "").Replace(" ", "")}DataGrid";
    
    private static DataGrid CreateDataGridForSubject(Subject subject)
        {
            var dataGrid = new DataGrid
            {
                Background = new BrushConverter().ConvertFromString("#424242") as Brush,
                AutoGenerateColumns = false,
                HeadersVisibility = DataGridHeadersVisibility.Column,
                IsReadOnly = true,
                VerticalAlignment = VerticalAlignment.Top,
                RowStyle = new Style(typeof(DataGridRow))
                {
                    Setters =
                    {
                        new Setter(BackgroundProperty, new BrushConverter().ConvertFromString("#424242")),
                        new Setter(ForegroundProperty, Brushes.White)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(ForegroundProperty, Brushes.White),
                        new Setter(BackgroundProperty, new BrushConverter().ConvertFromString("#424242")),
                        new Setter(BorderThicknessProperty, new Thickness(0)),
                    }
                },
                ColumnHeaderStyle = new Style(typeof(DataGridColumnHeader))
                {
                    Setters =
                    {
                        new Setter(BackgroundProperty, new BrushConverter().ConvertFromString("#424242")),
                        new Setter(ForegroundProperty, Brushes.White),
                        new Setter(FontWeightProperty, FontWeights.Bold),
                        new Setter(PaddingProperty, new Thickness(10, 0, 10, 0)),
                        new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center)
                    }
                }
            };

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                Header = "Jegy",
                Binding = new Binding("Grade")
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Width = new DataGridLength(2, DataGridLengthUnitType.Star),
                Header = "Téma",
                Binding = new Binding("Theme")
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Width = new DataGridLength(2, DataGridLengthUnitType.Star),
                Header = "Típus",
                Binding = new Binding("GradeType")
            });

            dataGrid.ItemsSource = subject.Grades;

            return dataGrid;
        }
    
    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();
}