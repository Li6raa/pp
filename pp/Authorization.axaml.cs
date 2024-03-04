using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace pp;

public partial class Authorization : Window
{
    public Authorization()
    {
        InitializeComponent();
    }
    private void exit(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }
   
}