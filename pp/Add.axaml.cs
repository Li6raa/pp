using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pp;

public partial class Add : Window
{
    private MySqlConnection _connection;
    private string _connString = "server=localhost; database=pp; port=3306;User Id=root;password=root";

    public Add()
    {
        InitializeComponent();
       
    }

    private void AddProvider(object sender, RoutedEventArgs e)
    {
        string название = Название.Text; // Получите значение из элемента управления для ввода имени агента
        string категория = Категория.Text;
        string цена = Цена.Text;
        string количество = Количество.Text;
        string sql = "INSERT INTO ассортимент (Название, Категория_id, Цена, Количество) VALUES (@Название, @Категория_id, @Цена, @Количество)";

        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.AddWithValue("@Название", название);
        command.Parameters.AddWithValue("@Категория_id", категория);
        command.Parameters.AddWithValue("@Цена", цена);
        command.Parameters.AddWithValue("@Количество", количество);
        command.ExecuteNonQuery();
        _connection.Close();
    }

    private void Comeback(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new assortment();
        newWindow.Show();
        this.Close();
    }
}
