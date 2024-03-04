using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pp;

public partial class Edit : Window
{
    private MySqlConnection _connection;
    private string _connString = "server=localhost; database=pp; port=3306;User Id=root;password=root";

    public Edit()
    {
        InitializeComponent();
    }
    private void btnDelete_Click(object? sender, RoutedEventArgs routedEventArgs)
    {
        //Получить значение идентификатора агента из TextBox
        int assortid= Convert.ToInt32(ID.Text);
    
        // Вызвать метод редактирования товара
        EditProvider(assortid);
    
        
    }
    private void EditProvider(int assortid)
    {
        string connectionString = "server=localhost; database=pp; port=3306;User Id=root;password=root";
        
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string название = Название.Text; // Получите значение из элемента управления для ввода имени агента
            string категория = Категория.Text;
            string цена = Цена.Text;
            string количество = Количество.Text;
            _connection = new MySqlConnection(_connString);
            connection.Open();
            string sql = "UPDATE ассортимент SET Название=@Название, Категория_id=@Категория_id, Цена=@Цена, Количество=@Количество Where ID=@assortid";
            
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@assortid", assortid);
                command.Parameters.AddWithValue("@Название", название);
                command.Parameters.AddWithValue("@Категория_id", категория);
                command.Parameters.AddWithValue("@Цена", цена);
                command.Parameters.AddWithValue("@Количество", количество);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    private void Comeback(object? sender, RoutedEventArgs routedEventArgs)
    {
        var newWindow = new assortment();
        newWindow.Show();
        this.Close();
    }
}