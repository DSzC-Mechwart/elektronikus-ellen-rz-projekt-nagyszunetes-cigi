using System.Windows;

namespace KretaKlon;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow {
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AdminButton_OnClick(object sender, RoutedEventArgs e) {
        new adminisztracio().Show();
        Close();
    }

    private void TantargyakButton_OnClick(object sender, RoutedEventArgs e) {
        new tantargy().Show();
        Close();
    }

    private void JegyekButton_OnClick(object sender, RoutedEventArgs e) {
        new grades().Show();
        Close();
    }
}