using System.Text.RegularExpressions;
using System.Windows.Input;

namespace KretaKlon.gubo;

public partial class gradeInput {
    public gradeInput() {
        InitializeComponent();
    }
    
    private static readonly Regex Regex = new("[^0-9]+");
    
    private static bool IsTextAllowed(string text)
    {
        return !Regex.IsMatch(text);
    }
    
    private void FilterToOnlyNumbers_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }
}