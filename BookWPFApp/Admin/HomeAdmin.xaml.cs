using BusinessLayer;
using LibraryRepositories;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace BookWPFApp.Admin
{
    /// <summary>
    /// Interaction logic for HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IShipRepository _shipRepository;

        public HomeAdmin()
        {
            InitializeComponent();

            // Initialize repositories
            _bookRepository = new BookRepository();
            _categoryRepository = new CategoryRepository();
            _shipRepository = new ShipRepository();

            // Load initial data for books, categories, and orders
            LoadBooks();
            LoadCategories();
            LoadOrders();
        }

        // Load books into the DataGrid
        private async void LoadBooks()
        {
            var books = await _bookRepository.GetBookAll();
            var bookList = books.Select(b => new
            {
                b.BookID,
                b.BookName,
                b.Price,
                CategoryID = b.Category != null ? b.Category.CategoryID : 0,
                CategoryName = b.Category != null ? b.Category.CategoryName : "Unknown"
            }).ToList();

            booksDataGrid.ItemsSource = bookList;
        }


        // Load categories into the ComboBox and DataGrid
        private async void LoadCategories()
        {
            var categories = await _categoryRepository.GetCategoryAll();
            categoryComboBox.ItemsSource = categories.ToList();
            categoryComboBox.SelectedValuePath = "CategoryID";
            categoryComboBox.DisplayMemberPath = "CategoryName";

            categoriesDataGrid.ItemsSource = categories.ToList();
        }
        private void CategoriesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoriesDataGrid.SelectedItem != null)
            {
                // Cast the selected item to the appropriate type
                var selectedCategory = categoriesDataGrid.SelectedItem as Categories;

                if (selectedCategory != null)
                {
                    categoryIdTextBox.Text = selectedCategory.CategoryID.ToString();
                    categoryNameTextBox.Text = selectedCategory.CategoryName;
                }
            }
            else
            {
                // Reset the form if no item is selected
                ResetCategoryForm();
            }
        }

        // Method to reset the category form fields
        private void ResetCategoryForm()
        {
            categoryIdTextBox.Text = string.Empty;
            categoryNameTextBox.Text = string.Empty;
        }


        // Load orders into the DataGrid
        private async void LoadOrders()
        {
            var orders = await _shipRepository.GetShipAll();
            orderConfirmationDataGrid.ItemsSource = orders.Select(o => new
            {
                o.ShipID,
                BookName = o.Books != null ? o.Books.BookName : "Unknown",
                o.DateOrder,
                o.DateShip,
                UserOrderName = o.UsersOrder != null ? o.UsersOrder.UserName : "Unknown",
                UserApproveName = o.UsersApprove != null ? o.UsersApprove.UserName : "Not Approved",
                Status = o.IsApproved ? "Approved" : "Pending"
            }).ToList();
        }

        // Event handler for Create Book
        private async void CreateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bookNameTextBox.Text) || string.IsNullOrWhiteSpace(priceTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var book = new Books
            {
                BookName = bookNameTextBox.Text,
                Price = decimal.Parse(priceTextBox.Text),
                CategoryID = (int)categoryComboBox.SelectedValue
            };

            await _bookRepository.Add(book);
            LoadBooks();
            MessageBox.Show("Book created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Event handler for Update Book
        private async void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(bookIdTextBox.Text, out int bookId))
            {
                var book = new Books
                {
                    BookID = bookId,
                    BookName = bookNameTextBox.Text,
                    Price = decimal.Parse(priceTextBox.Text),
                    CategoryID = (int)categoryComboBox.SelectedValue
                };

                await _bookRepository.Update(book);
                LoadBooks();
                MessageBox.Show("Book updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a valid book to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for Delete Book
        private async void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(bookIdTextBox.Text, out int bookId))
            {
                await _bookRepository.Delete(bookId);
                LoadBooks();
                MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a valid book to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BooksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBook = booksDataGrid.SelectedItem;

            if (selectedBook != null)
            {
                // Use reflection or dynamic to access properties
                dynamic book = selectedBook;
                bookIdTextBox.Text = book.BookID.ToString();
                bookNameTextBox.Text = book.BookName;
                priceTextBox.Text = book.Price.ToString();

                categoryComboBox.SelectedValue = book.CategoryID; // Make sure the anonymous type includes CategoryID
            }
            else
            {
                ResetBookForm();
            }
        }
        private void OrderConfirmationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orderConfirmationDataGrid.SelectedItem != null)
            {
                // Assuming the items in the DataGrid have an IsApproved property
                dynamic selectedOrder = orderConfirmationDataGrid.SelectedItem; // Replace dynamic with a specific type if possible

                if (selectedOrder.Status == "Approved")
                {
                    confirmOrderButton.Visibility = Visibility.Collapsed; 
                }
                else
                {
                    confirmOrderButton.Visibility = Visibility.Visible; 
                }
            }
            else
            {
                
                confirmOrderButton.Visibility = Visibility.Collapsed;
            }
        }


        // Method to reset the book form fields
        private void ResetBookForm()
        {
            bookIdTextBox.Text = string.Empty;
            bookNameTextBox.Text = string.Empty;
            priceTextBox.Text = string.Empty;
            categoryComboBox.SelectedIndex = -1; // Reset combo box
        }


        // Event handler for Reset Book form
        private void ResetBookButton_Click(object sender, RoutedEventArgs e)
        {
            bookIdTextBox.Text = string.Empty;
            bookNameTextBox.Text = string.Empty;
            priceTextBox.Text = string.Empty;
            categoryComboBox.SelectedIndex = -1;
        }

        // Event handler for Create Category
        private async void CreateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(categoryNameTextBox.Text))
            {
                MessageBox.Show("Please enter a category name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var category = new Categories
            {
                CategoryName = categoryNameTextBox.Text
            };

            await _categoryRepository.Add(category);
            LoadCategories();
            MessageBox.Show("Category created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Event handler for Update Category
        private async void UpdateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(categoryIdTextBox.Text, out int categoryId))
            {
                var category = new Categories
                {
                    CategoryID = categoryId,
                    CategoryName = categoryNameTextBox.Text
                };

                await _categoryRepository.Update(category);
                LoadCategories();
                MessageBox.Show("Category updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a valid category to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for Delete Category
        private async void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(categoryIdTextBox.Text, out int categoryId))
            {
                await _categoryRepository.Delete(categoryId);
                LoadCategories();
                MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a valid category to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for Reset Category form
        private void ResetCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            categoryIdTextBox.Text = string.Empty;
            categoryNameTextBox.Text = string.Empty;
        }

        // Event handler for Confirm Order
        private async void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (orderConfirmationDataGrid.SelectedItem != null)
            {
                dynamic selectedOrder = orderConfirmationDataGrid.SelectedItem;
                int shipId = selectedOrder.ShipID;

                var ship = await _shipRepository.GetShipAllById(shipId);
                if (ship != null)
                {
                    ship.IsApproved = true;
                    ship.DateShip = DateTime.Now;
                    await _shipRepository.Update(ship);
                    LoadOrders();
                    MessageBox.Show("Order confirmed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to confirm order. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to confirm.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Display a confirmation dialog
            var result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Login loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
