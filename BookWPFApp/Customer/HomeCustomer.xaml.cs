using BusinessLayer;
using LibraryRepositories;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace BookWPFApp.Customer
{
    public partial class HomeCustomer : Window
    {
        private readonly IBookRepository _bookRepository;
        private readonly IShipRepository _shipRepository;

        public HomeCustomer()
        {
            InitializeComponent();

            // Khởi tạo các repository
            _bookRepository = new BookRepository();
            _shipRepository = new ShipRepository();

            // Tải danh sách sách và lịch sử giao hàng
            LoadBooks();
            LoadShippingHistory();
        }

        private int GetCurrentUserId()
        {
            return UserSession.UserId;
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Login loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
        }


        private async void LoadBooks()
        {
            var books = await _bookRepository.GetBookAll();
            var bookList = books.Select(b => new
            {
                b.BookID,
                b.BookName,
                b.Price,
                CategoryName = b.Category != null ? b.Category.CategoryName : "Unknown"
            }).ToList();

            booksListBox.ItemsSource = bookList;
        }

        private async void LoadShippingHistory()
        {
            int userId = GetCurrentUserId();
            var ships = await _shipRepository.GetShipAllByUserId(userId);
            var shipList = ships.Select(s => new
            {

                BookName = s.Books != null ? s.Books.BookName : "Unknown",
                s.DateOrder,
                s.DateShip,
                UserApproveName = s.UsersApprove != null ? s.UsersApprove.UserName : "Unknown",
                Status = s.IsApproved ? "Approved" : "not yet approved"
            }).ToList();

            shippingHistoryDataGrid.ItemsSource = shipList;
        }

        private async void BuyBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (booksListBox.SelectedItem != null)
            {
                // Giả sử đối tượng `selectedBook` là một đối tượng chứa thông tin sách
                var selectedBook = (dynamic)booksListBox.SelectedItem;
                int bookId = selectedBook.BookID;

                // Giả sử `userId` là ID của người dùng hiện tại. Cần có cách xác định người dùng hiện tại trong hệ thống của bạn.
                int userId = GetCurrentUserId(); // Hàm này cần được triển khai để lấy ID người dùng hiện tại

                // Tạo đối tượng ship mới
                var newShip = new Ships
                {
                    DateOrder = DateTime.Now,
                    IsApproved = false, // Đơn hàng mới chưa được phê duyệt
                    DateShip = null,
                    BookID = bookId,
                    UserOrderID = userId,
                    UserApproveID = 1
                };

                // Lưu đơn ship vào cơ sở dữ liệu
                await _shipRepository.Add(newShip);

                MessageBox.Show($"You have bought '{selectedBook.BookName}'", "Purchase Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                // Cập nhật lại lịch sử giao hàng sau khi thêm đơn hàng mới
                LoadShippingHistory();

            }
            else
            {
                MessageBox.Show("Please select a book to buy.", "No Book Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
