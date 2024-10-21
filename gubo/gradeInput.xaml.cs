using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KretaKlon.gubo;

public partial class gradeInput {
    public gradeInput() {
        InitializeComponent();
        Load();
    }
    List<List<string>> strings = new List<List<string>>();
    List<Subject> subjects  = new List<Subject>();
    List<Student> students = new List<Student>();
    public void Load()
    {
        tanuloComboBox.Items.Clear();
        tantargyComboBox.Items.Clear();
        var lines = File.ReadAllLines("output.csv");
        int i2 = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(';');
            
            if (i2 == 0)
            {
                List<string> strings1 = new List<string>();
                strings1.Add(parts[0]);
                strings1.Add(parts[1]);
                strings.Add(strings1);
                i2++;
                continue;
            }
            if (strings[0].Contains(parts[0])) {
                strings[0].Add(parts[1]);
            }
            else
            {
                List<string> list = new List<string> { parts[0], parts[1] };
                strings.Append(list);
            }
        }
        for (int i = 0; i < strings.Count(); i++)
        {
            tanuloComboBox.Items.Add(strings[i][0]);
        }
    }
    
    private static readonly Regex Regex = MyRegex();
    
    private static bool IsTextAllowed(string text)
    {
        return !Regex.IsMatch(text);
    }
    
    private void FilterToOnlyNumbers_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        new grades().Show();
        Close();
    }

    private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
    {
        int jegyInp = Convert.ToInt32(jegyTextBox.Text);
        string tantargyInp = temaTextBox.Text;
        string tipus = tipusTextBox.Text;
        Grade jegyek = new Grade(jegyInp, tantargyInp, tipus);
        Subject tantargy = new Subject(tantargyInp);
        tantargy.Jegy(jegyek);
        subjects.Add(tantargy);
        Student diak = new Student(subjects);
        students.Add(diak);
        refreshDataGrid();
    }

    private void tanuloComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        Login login = new Login();
        login.Tanulo = tanuloComboBox.SelectedItem.ToString();
        login.Show();
        for (int i = tanuloComboBox.SelectedIndex; i == tanuloComboBox.SelectedIndex; i++)
        {
            for (int j = 1; j < strings[i].Count(); j++)
            {
                tantargyComboBox.Items.Add(strings[i][j]);
            }
        }
    }

    public void refreshDataGrid()
    {
        var allGrades = new List<Grade>();

        foreach (var student in students)
        {
            foreach (var subject in student.tantargy)
            {
                allGrades.AddRange(subject.Grades);
            }
        }
        jegyekDataGrid.ItemsSource = allGrades.ToArray();
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();
}