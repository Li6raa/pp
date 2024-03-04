using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pp;

public partial class procces : Window
{
    private string _connString="server=localhost; database=pp; port=3306;User Id=root;password=root";
    private List<Производственный_процесс> _производственный_процесс;
    private MySqlConnection _connection;
    private DataGrid _ticketsGrid;

    public procces()
    {
        InitializeComponent();
        ShowOrderTable();
    }
     public void ShowOrderTable()
     {
         string sql =
             "SELECT производственный_процесс.ID, производственный_процесс.Название, категория.Название, тип_материала.Название, цехи.Название FROM производственный_процесс " +
             "join категория ON производственный_процесс.Категория_id = категория.ID " +
             "join тип_материала ON производственный_процесс.Тип_материала_id = тип_материала.ID " +
             "join цехи ON производственный_процесс.Цех_id = цехи.ID;";
        _производственный_процесс = new List<Производственный_процесс>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentYch = new Производственный_процесс()
            {
                ID = reader.GetInt32("ID"),
                Название = reader.GetValue(1).ToString(),
                Категория_id= reader.GetValue(2).ToString(),
                Тип_материала_id = reader.GetValue(3).ToString(),
                Цех_id= reader.GetValue(4).ToString(),
            };
            _производственный_процесс.Add(currentYch);
        }
        _connection.Close();
        TicketsGrid.ItemsSource = _производственный_процесс;
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
            string sql = "DELETE FROM производственный_процесс WHERE ID = @OrderId";
        
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