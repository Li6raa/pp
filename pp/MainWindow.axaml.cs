using System.Net;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace pp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Width = 200;
        this.Height = 300;
    }
   
    private void OpenNewWindow(object? sender, RoutedEventArgs routedEventArgs)
    {
         var newWindow = new assortment();
       newWindow.Show();
       this.Close();
    }
    private void OpenOrder(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new Order();
        newWindow.Show();
        this.Close();
    }
    private void OpenProcess(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new procces();
        newWindow.Show();
        this.Close();
    }
    private void exit(object? sender, RoutedEventArgs routedEventArgs)
    {
        this.Close();
    }
}