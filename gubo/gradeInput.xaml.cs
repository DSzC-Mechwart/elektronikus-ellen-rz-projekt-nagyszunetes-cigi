using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace KretaKlon.gubo;

public partial class gradeInput {
    public gradeInput() {
        InitializeComponent();
        ((ComboBoxItem)StudentChooser.Items[1]).Tag = new grades().KisPista;


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

    private void StudentChooser_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        var comboBox = (ComboBox)sender;
        // comboBox.SelectedItem
    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();
}