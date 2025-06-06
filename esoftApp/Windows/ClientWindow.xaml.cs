using System.Windows;
using System.Linq;
using esoftApp.Services;

namespace esoftApp.Windows
{
    public partial class ClientWindow : Window
    {
        private MainService _service;
        public ClientWindow()
        {
            InitializeComponent();
            _service = new MainService();
            UpdateData();
        }

        private void UpdateData()
        {
            ClientsDataGrid.ItemsSource = _service.GetAllClients();
        }

        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}