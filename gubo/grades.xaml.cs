using System.Windows;
using KretaKlon.gubo;

namespace KretaKlon;

public partial class grades {
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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }
}