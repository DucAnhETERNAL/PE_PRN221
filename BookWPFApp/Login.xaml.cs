using BookWPFApp.Admin;
using BookWPFApp.Customer;
using BusinessLayer;
using LibraryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookWPFApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShipRepository _shipRepository;
        public Login()
        {
            // Khởi tạo các repository
            _bookRepository = new BookRepository();
            _categoryRepository = new CategoryRepository();
            _userRepository = new UserRepository();
            _shipRepository = new ShipRepository();
            InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageTextBlock.Text = "Please enter both username and password.";
                return;
            }

            var user = await _userRepository.Login(username, password);

            if (user != null)
            {
                UserSession.UserId = user.UserID;
                UserSession.UserName = user.UserName;
                UserSession.UserType = user.Type;

                if (user.Type == "Admin")
                {

                    MessageBox.Show("Welcome, Admin!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    HomeAdmin homeAdminWindow = new HomeAdmin();
                    homeAdminWindow.Show();
                    this.Close();
                }
                else if (user.Type == "User")
                {

                    MessageBox.Show("Welcome, User!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    HomeCustomer homeCustomerWindow = new HomeCustomer();
                    homeCustomerWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageTextBlock.Text = "Invalid username or password.";
            }
        }
    }
}
