using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pp;

public partial class Order : Window
{
    private string _connString="server=localhost; database=pp; port=3306;User Id=root;password=root";
    private List<Заказ> _order;
    private MySqlConnection _connection;
    private DataGrid _ticketsGrid;
    public Order()
    {
        InitializeComponent();
        ShowOrderTable();
    }
    public void ShowOrderTable()
    {
        string sql = "SELECT заказ.ID, ассортимент.Название, клиент.Имя, услуги.Название, заказ.Цена FROM заказ " +
                     "join ассортимент ON заказ.Ассортимент_id = ассортимент.ID " +
                     "join клиент ON заказ.Клиент_id = клиент.ID " +
                     "join услуги ON заказ.Услуга_id = услуги.ID;";
        _order = new List<Заказ>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentYch = new Заказ()
            {
                ID = reader.GetInt32("ID"),
                Ассортимент_id = reader.GetValue(1).ToString(),
                Клиент_id= reader.GetValue(2).ToString(),
                Услуга_id = reader.GetValue(3).ToString(),
                Цена=reader.GetValue(4).ToString(),
            };
            _order.Add(currentYch);
        }
        _connection.Close();
        TicketsGrid.ItemsSource = _order;
    }
    private void Comeback(object? sender, RoutedEventArgs routedEventArgs)//Переход на другую форму
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }
    private void btnDelete_Click(object? sender, RoutedEventArgs routedEventArgs)
    {
        //Получить значение идентификатора агента из TextBox
        int orderId = Convert.ToInt32(Delete.Text);
    
        // Вызвать метод удаления агента
        DeleteOrder(orderId);
    
        // Обновить таблицу Product после удаления
        ShowOrderTable();
    }
    public void DeleteOrder(int orderId)
    {
        // Установить строку подключения к базе данных
        string connectionString = "server=localhost; database=pp; port=3306;User Id=root;password=root";
    
        // Создать подключение к базе данных
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            // Открыть подключение
            connection.Open();
        
            // Создать SQL-запрос для удаления агента с указанным идентификатором
            string sql = "DELETE FROM заказ WHERE ID = @OrderId";
        
            // Создать команду с параметрами
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                // Добавить параметр для идентификатора агента
                command.Parameters.AddWithValue("@OrderId", orderId);
            
                // Выполнить команду удаления
                command.ExecuteNonQuery();
            }
            // Закрыть подключение
            connection.Close();
        }
    }
}