﻿using System.Text.RegularExpressions;
using System.Windows.Input;

namespace KretaKlon.gubo;

public partial class gradeInput {
    public gradeInput() {
        InitializeComponent();
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

    }

    [GeneratedRegex("[^0-9]+")]
    private static partial Regex MyRegex();
}