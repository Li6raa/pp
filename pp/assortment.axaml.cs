using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace pp;

public partial class assortment : Window
{
    private string _connString="server=localhost; database=pp; port=3306;User Id=root;password=root";
    private List<Ассортимент> _ассортимент;
    private List<Заказ> _заказ;
    private List<Категория> _категория;
    private List<Поставщик_материала> _поставщик_материала;
    private List<Производственный_процесс> _производственный_процесс;
    private List<Тип_материала> _тип_материала;
    private List<Услуги> _услуги;
    private List<Цехи> _цехи;
    private List<Клиент> _клиент;
    private MySqlConnection _connection;
    private DataGrid _ticketsGrid;
    private DataGrid _AgentGrid;
    
    public void ShowProductTable()
    {
        string sql =
            "SELECT ассортимент.ID, ассортимент.Название, категория.Название, Цена, Количество FROM ассортимент " +
            "join категория ON ассортимент.Категория_id = категория.ID;";
            
        _ассортимент = new List<Ассортимент>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentYch = new Ассортимент()
            {
                ID = reader.GetInt32("ID"),
                Название = reader.GetValue(1).ToString(),
                Категория_id= reader.GetValue(2).ToString(),
                Цена = reader.GetInt32("Цена"),
                Количество = reader.GetValue(4).ToString(),
            };
            _ассортимент.Add(currentYch);
        }
        _connection.Close();
        АссортGrid.ItemsSource = _ассортимент;
    }
 
    public void FillStatusList()
    {
        _категория = new List<Категория>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        //создается объект и используется для установления связи с базой данных
        MySqlCommand command = new MySqlCommand("select ID, Название, Описание from категория", _connection);//Создание конструктора которому передаются Sql команды
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentstatus = new Категория()
            {
                ID = reader.GetInt32("ID"),
                Название = reader.GetString("Название"),
                Описание =  reader.GetString("Описание"),
            };
            _категория.Add(currentstatus);
        }
        _connection.Close();
        var statusComboBox = this.Find<ComboBox>("Cmdstatus");
        statusComboBox.ItemsSource = _категория;
    }

    public assortment()
    {
        InitializeComponent();
        string sql =
            "SELECT ID, Names, category.Name, Price, provider.Name FROM product " +
            "join category ON product.category_id = category.ID " +
            "join provider ON product.provider_id = provider.ID";
        ShowProductTable();
        FillStatusList();
        this.Width = 700;
        this.Height = 400;
    }
  
    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Ассортимент> cat = _ассортимент;
        cat = cat.Where(x => x.Название.Contains(txtSearch.Text)).ToList();
        АссортGrid.ItemsSource = cat;
    }
    private void Cmdstatus_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var statusComboBox = (ComboBox)sender;
        var currentstatus = statusComboBox.SelectedItem as Категория;
        var filteredstatus = _ассортимент.Where(x => x.Категория_id == currentstatus.Название).ToList();
        АссортGrid.ItemsSource = filteredstatus;
        
    }
    private void Comeback(object? sender, RoutedEventArgs routedEventArgs)//Переход на другую форму
    {
        var newWindow = new MainWindow();
        newWindow.Show();
        this.Close();
    }
    private void GoAssort(object? sender, RoutedEventArgs routedEventArgs)//Переход на другую форму
    {
        var newWindow = new Add();
        newWindow.Show();
        this.Close();
    }
    private void EditAssort(object? sender, RoutedEventArgs routedEventArgs)//Переход на другую форму
    {
        var newWindow = new Edit();
        newWindow.Show();
        this.Close();
    }
}

public class Ассортимент
{
    public int ID { get; set; }
    public string Название { get; set; }
    public string Категория_id { get; set; }
    public int Цена { get; set; }
    public string Количество { get; set; }
}

public class Заказ
{
    public int ID { get; set; }
    public string Ассортимент_id { get; set; }
    public string Клиент_id { get; set; }
    public string Услуга_id { get; set; }
    public string Цена { get; set; }
}

public class Категория
{
    public int ID { get; set; }
    public string Название { get; set; }
    public string Описание { get; set; }
}
public class Клиент
{
    public int ID { get; set; }
    public string Имя { get; set; }
    public string Фамилия { get; set; }
    public string Телефон { get; set; }
}

public class Поставщик_материала
{
    public int ID { get; set; }
    public string Название { get; set; }
    public string Электронный_адрес { get; set; }
    public string Телефон { get; set; }
}

public class Производственный_процесс
{
    public int ID { get; set; }
    public string Название { get; set; }
    public string Категория_id { get; set; }
    public string Тип_материала_id { get; set; }
    public string Цех_id { get; set; }
}
public class Тип_материала
{
    public int ID { get; set; }
    public string Название { get; set; }
    public string Поставщик_материала_id { get; set; }
    public string Описание { get; set; }
}
public class Услуги
{
    public int ID { get; set; }
    public string Название { get; set; }
    public string Описание { get; set; }
}
public class Цехи
{
    public int ID { get; set; }
    public string Название { get; set; }
    public string Описание { get; set; }
}


