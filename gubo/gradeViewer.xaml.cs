using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace KretaKlon.gubo
{
    public partial class gradeViewer {
        private readonly List<Student> _students;

        public gradeViewer(List<Student> students)
        {
            InitializeComponent();
            _students = students;

            var selectedStudent = GetSelectedStudent();
            LoadStudentSubjects(selectedStudent);

            Loaded += (_, _) => {
                var numberOfFailingSubjects = selectedStudent.Subjects.Count(subject => 
                    subject.Grades.Sum(gradeClass => 
                        gradeClass.Grade
                    ) / (subject.Grades.Count > 0 ? subject.Grades.Count : 1) > 2
                );

                if (numberOfFailingSubjects >= 3) FailingCheckBox.IsChecked = true;
            };
        }

        private Student GetSelectedStudent()
        {
            var selector = new studentSelector(_students);
            return (selector.ShowDialog() == true 
                ? selector.SelectedStudent
                : throw new Exception("Student not chosen")
            ) ?? throw new Exception("Student not chosen asdfghjkl");
        }

        private void LoadStudentSubjects(Student student) {
            SubjectTabs.Items.Clear();

            foreach (var tabItem in student.Subjects.Select(subject => 
                new TabItem { Header = subject.Name, Content = CreateDataGridForSubject(subject) }
            )) {
                SubjectTabs.Items.Add(tabItem);
            }
        }

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
                        new Setter(BackgroundProperty, subject.Grades.Count > 0 ? subject.Grades.Select(gradeClass => gradeClass.Grade).Sum() / subject.Grades.Count > 2 ? new BrushConverter().ConvertFromString("#424242") : new SolidColorBrush(Colors.Red) : new BrushConverter().ConvertFromString("#424242")),
                        new Setter(ForegroundProperty, Brushes.White)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(ForegroundProperty, Brushes.White),
                        new Setter(BackgroundProperty, subject.Grades.Count > 0 ? subject.Grades.Select(gradeClass => gradeClass.Grade).Sum() / subject.Grades.Count > 2 ? new BrushConverter().ConvertFromString("#424242") : new SolidColorBrush(Colors.Red) : new BrushConverter().ConvertFromString("#424242")),
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

        private void BackButton_OnClick(object sender, RoutedEventArgs e) {
            new grades().Show();
            Close();
        }
    }
}