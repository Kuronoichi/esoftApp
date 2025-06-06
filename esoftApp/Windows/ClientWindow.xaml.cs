using System.Windows;
using System.Linq;
using esoftApp.Services;

namespace esoftApp.Windows
{
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
            MainService _service = new MainService();
            ClientsDataGrid.ItemsSource = _service.GetAllClients();
        }

        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (mainWindow == null)
            {
                mainWindow = new MainWindow();
            }

            mainWindow.Show();
            this.Close();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}