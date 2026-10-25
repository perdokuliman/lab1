using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace BookStore
{
    public partial class MainForm : Form
    {
        private readonly string _connectionString;
        private DataTable _currentUser;
        private List<DataRow> _cart = new List<DataRow>();
        private DataTable _availableBooks;
        private DataTable _userOrders;
        private DataTable _orderDetails;
        private DataTable _authorSearchResults;
        private List<string> _allAuthors = new List<string>();
        private ListBox _suggestionsListBox;
        private System.Windows.Forms.Timer _suggestionTimer;
        private const int SUGGESTION_DELAY = 300;
        private DataTable _manageBooks;
        private DataTable _allAuthorsForManage;
        private bool _isEditMode = false;
        private int _editingBookId = 0;

        public MainForm()
        {
            InitializeComponent();
            InitializeAutoComplete();

            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["BookStoreConnection"]?.ConnectionString
                    ?? "Host=localhost;Port=5432;Database=bookstore;Username=postgres;Password=your_password";

                LoadCurrentUser();
                LoadAvailableBooks();
                InitializeGenreFilter();
                LoadAllAuthors();
                UpdateCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeAutoComplete()
        {
            _suggestionsListBox = new ListBox
            {
                Visible = false,
                IntegralHeight = false,
                Height = 150,
                Font = new Font("Microsoft Sans Serif", 9),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.Black
            };

            this.Controls.Add(_suggestionsListBox);
            _suggestionsListBox.BringToFront();

            _suggestionTimer = new System.Windows.Forms.Timer();
            _suggestionTimer.Interval = SUGGESTION_DELAY;
            _suggestionTimer.Tick += SuggestionTimer_Tick;

            _suggestionsListBox.SelectedIndexChanged += SuggestionsListBox_SelectedIndexChanged;
            _suggestionsListBox.KeyDown += SuggestionsListBox_KeyDown;
            _suggestionsListBox.MouseDoubleClick += SuggestionsListBox_MouseDoubleClick;
            _suggestionsListBox.DrawMode = DrawMode.OwnerDrawFixed;
            _suggestionsListBox.DrawItem += SuggestionsListBox_DrawItem;

            this.Click += (s, e) => HideSuggestions();
            txtAuthorSearch.LostFocus += (s, e) =>
            {
                if (!_suggestionsListBox.Focused && !_suggestionsListBox.ClientRectangle.Contains(_suggestionsListBox.PointToClient(MousePosition)))
                    HideSuggestions();
            };

            PositionSuggestionsBox();
        }

        private void PositionSuggestionsBox()
        {
            if (_suggestionsListBox != null && txtAuthorSearch != null && tabProcedures != null)
            {
                Point relativePoint = txtAuthorSearch.PointToScreen(Point.Empty);
                Point formPoint = this.PointToClient(relativePoint);

                _suggestionsListBox.Location = new Point(
                    formPoint.X,
                    formPoint.Y + txtAuthorSearch.Height + 2
                );
                _suggestionsListBox.Width = txtAuthorSearch.Width;
            }
        }

        private void LoadAllAuthors()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    var cmd = new NpgsqlCommand("SELECT * FROM get_all_authors()", conn);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        _allAuthors.Clear();
                        while (reader.Read())
                        {
                            _allAuthors.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки списка авторов: {ex.Message}");
            }
        }

        private void ShowAuthorSuggestions(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                HideSuggestions();
                return;
            }

            var suggestions = _allAuthors
                .Where(author => author.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                .Take(10)
                .ToList();

            if (suggestions.Any())
            {
                _suggestionsListBox.Items.Clear();
                foreach (var suggestion in suggestions)
                {
                    _suggestionsListBox.Items.Add(suggestion);
                }

                _suggestionsListBox.Visible = true;
                _suggestionsListBox.BringToFront();

                if (_suggestionsListBox.Items.Count > 0)
                {
                    _suggestionsListBox.SelectedIndex = 0;
                }
            }
            else
            {
                HideSuggestions();
            }
        }

        private void HideSuggestions()
        {
            _suggestionsListBox.Visible = false;
        }

        private void SuggestionTimer_Tick(object sender, EventArgs e)
        {
            _suggestionTimer.Stop();
            ShowAuthorSuggestions(txtAuthorSearch.Text.Trim());
        }

        private void SuggestionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Обработка изменения выбранного элемента
        }

        private void SuggestionsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index >= 0 && e.Index < _suggestionsListBox.Items.Count)
            {
                string text = _suggestionsListBox.Items[e.Index].ToString();
                string searchText = txtAuthorSearch.Text.Trim();

                int matchIndex = text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);

                if (matchIndex >= 0 && !string.IsNullOrEmpty(searchText))
                {
                    string before = text.Substring(0, matchIndex);
                    string match = text.Substring(matchIndex, searchText.Length);
                    string after = text.Substring(matchIndex + searchText.Length);

                    using (Brush normalBrush = new SolidBrush(e.ForeColor))
                    using (Brush highlightBrush = new SolidBrush(Color.Blue))
                    {
                        float x = e.Bounds.X;

                        if (!string.IsNullOrEmpty(before))
                        {
                            e.Graphics.DrawString(before, e.Font, normalBrush, x, e.Bounds.Y);
                            x += e.Graphics.MeasureString(before, e.Font).Width;
                        }

                        if (!string.IsNullOrEmpty(match))
                        {
                            e.Graphics.DrawString(match, e.Font, highlightBrush, x, e.Bounds.Y);
                            x += e.Graphics.MeasureString(match, e.Font).Width;
                        }

                        if (!string.IsNullOrEmpty(after))
                        {
                            e.Graphics.DrawString(after, e.Font, normalBrush, x, e.Bounds.Y);
                        }
                    }
                }
                else
                {
                    e.Graphics.DrawString(text, e.Font, new SolidBrush(e.ForeColor), e.Bounds);
                }
            }

            e.DrawFocusRectangle();
        }

        private void SuggestionsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (_suggestionsListBox.SelectedItem != null)
                    {
                        txtAuthorSearch.Text = _suggestionsListBox.SelectedItem.ToString();
                        txtAuthorSearch.Focus();
                        txtAuthorSearch.Select(txtAuthorSearch.Text.Length, 0);
                        HideSuggestions();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

                case Keys.Escape:
                    HideSuggestions();
                    txtAuthorSearch.Focus();
                    break;

                case Keys.Up:
                    if (_suggestionsListBox.SelectedIndex > 0)
                    {
                        _suggestionsListBox.SelectedIndex--;
                    }
                    e.Handled = true;
                    break;

                case Keys.Down:
                    if (_suggestionsListBox.SelectedIndex < _suggestionsListBox.Items.Count - 1)
                    {
                        _suggestionsListBox.SelectedIndex++;
                    }
                    e.Handled = true;
                    break;
            }
        }

        private void SuggestionsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = _suggestionsListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                _suggestionsListBox.SelectedIndex = index;
                txtAuthorSearch.Text = _suggestionsListBox.SelectedItem.ToString();
                txtAuthorSearch.Focus();
                txtAuthorSearch.Select(txtAuthorSearch.Text.Length, 0);
                HideSuggestions();
            }
        }

        private void LoadCurrentUser()
        {
            try
            {
                var defaultPhone = ConfigurationManager.AppSettings["DefaultUserPhone"] ?? "+7 (900) 123-45-67";

                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand(
                        "SELECT id, full_name, phone, email, address FROM users WHERE phone = @phone",
                        conn);
                    cmd.Parameters.AddWithValue("@phone", defaultPhone);

                    var adapter = new NpgsqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        _currentUser = dt;
                        var userRow = dt.Rows[0];
                        lblCurrentUser.Text = $"Покупатель: {userRow["full_name"]}";
                        lblCurrentUser.ForeColor = Color.Green;
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден. Проверьте настройки.",
                            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователя: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAvailableBooks()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT 
                            b.id, 
                            b.title, 
                            b.price, 
                            a.name as author_name, 
                            b.genre, 
                            b.published, 
                            b.stock_quantity,
                            CASE 
                                WHEN b.stock_quantity > 0 THEN 'В наличии'
                                ELSE 'Нет в наличии'
                            END as availability
                        FROM books b
                        JOIN authors a ON b.author_id = a.id
                        ORDER BY b.title";

                    var adapter = new NpgsqlDataAdapter(query, conn);
                    _availableBooks = new DataTable();
                    adapter.Fill(_availableBooks);

                    dgvBooks.DataSource = _availableBooks;

                    dgvBooks.Columns["id"].Visible = false;
                    dgvBooks.Columns["title"].HeaderText = "Название";
                    dgvBooks.Columns["price"].HeaderText = "Цена";
                    dgvBooks.Columns["author_name"].HeaderText = "Автор";
                    dgvBooks.Columns["genre"].HeaderText = "Жанр";
                    dgvBooks.Columns["published"].HeaderText = "Год издания";
                    dgvBooks.Columns["stock_quantity"].HeaderText = "Остаток";
                    dgvBooks.Columns["availability"].HeaderText = "Статус";

                    dgvBooks.Columns["price"].DefaultCellStyle.Format = "N2";
                    dgvBooks.Columns["published"].DefaultCellStyle.Format = "yyyy";

                    foreach (DataGridViewRow row in dgvBooks.Rows)
                    {
                        var stockCell = row.Cells["stock_quantity"];
                        if (stockCell != null && stockCell.Value != DBNull.Value)
                        {
                            int stock = Convert.ToInt32(stockCell.Value);
                            if (stock == 0)
                            {
                                row.DefaultCellStyle.ForeColor = Color.Gray;
                                row.DefaultCellStyle.BackColor = Color.LightGray;
                            }
                            else if (stock < 5)
                            {
                                row.DefaultCellStyle.ForeColor = Color.Orange;
                            }
                        }
                    }

                    lblBookCount.Text = $"Всего книг: {_availableBooks.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки книг: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeGenreFilter()
        {
            try
            {
                cmbGenreFilter.Items.Clear();
                cmbGenreFilter.Items.Add("Все жанры");

                if (_availableBooks != null)
                {
                    var genres = _availableBooks.AsEnumerable()
                        .Select(row => row["genre"].ToString())
                        .Where(genre => !string.IsNullOrEmpty(genre))
                        .Distinct()
                        .OrderBy(genre => genre)
                        .ToList();

                    foreach (var genre in genres)
                    {
                        cmbGenreFilter.Items.Add(genre);
                    }
                }
                cmbGenreFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации фильтра: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCartDisplay()
        {
            var cartTable = new DataTable();
            cartTable.Columns.Add("BookId", typeof(int));
            cartTable.Columns.Add("Title", typeof(string));
            cartTable.Columns.Add("Author", typeof(string));
            cartTable.Columns.Add("Price", typeof(decimal));
            cartTable.Columns.Add("Quantity", typeof(int));
            cartTable.Columns.Add("Subtotal", typeof(decimal));
            cartTable.Columns.Add("AvailableStock", typeof(int));

            foreach (var cartItem in _cart)
            {
                cartTable.Rows.Add(
                    cartItem["id"],
                    cartItem["title"],
                    cartItem["author_name"],
                    cartItem["price"],
                    cartItem["quantity"],
                    Convert.ToDecimal(cartItem["price"]) * Convert.ToInt32(cartItem["quantity"]),
                    cartItem["stock_quantity"]
                );
            }

            dgvCart.DataSource = cartTable;

            if (dgvCart.Columns.Count > 0)
            {
                dgvCart.Columns["BookId"].Visible = false;
                dgvCart.Columns["Title"].HeaderText = "Название";
                dgvCart.Columns["Author"].HeaderText = "Автор";
                dgvCart.Columns["Price"].HeaderText = "Цена";
                dgvCart.Columns["Quantity"].HeaderText = "Кол-во";
                dgvCart.Columns["Subtotal"].HeaderText = "Сумма";
                dgvCart.Columns["AvailableStock"].Visible = false;

                dgvCart.Columns["Price"].DefaultCellStyle.Format = "N2";
                dgvCart.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            }

            int totalItems = _cart.Sum(item => Convert.ToInt32(item["quantity"]));
            decimal totalAmount = _cart.Sum(item =>
                Convert.ToDecimal(item["price"]) * Convert.ToInt32(item["quantity"]));

            lblCartItems.Text = $"Товаров в корзине: {totalItems}";
            lblCartTotal.Text = $"Общая сумма: {totalAmount:N2} руб.";
        }

        private void AddToCart(DataRow bookRow)
        {
            if (!bookRow.Table.Columns.Contains("stock_quantity"))
            {
                MessageBox.Show("Невозможно добавить книгу в корзину", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int bookId = Convert.ToInt32(bookRow["id"]);
            int availableStock = Convert.ToInt32(bookRow["stock_quantity"]);

            if (availableStock <= 0)
            {
                MessageBox.Show("Эта книга временно отсутствует на складе",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existingItem = _cart.FirstOrDefault(item =>
                Convert.ToInt32(item["id"]) == bookId);

            if (existingItem != null)
            {
                int currentQuantity = Convert.ToInt32(existingItem["quantity"]);
                if (currentQuantity < availableStock)
                {
                    existingItem["quantity"] = currentQuantity + 1;
                }
                else
                {
                    MessageBox.Show($"Нельзя добавить больше книг. В наличии только {availableStock} шт.",
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                DataRow cartItem = bookRow.Table.NewRow();
                foreach (DataColumn column in bookRow.Table.Columns)
                {
                    if (bookRow.Table.Columns.Contains(column.ColumnName))
                    {
                        cartItem[column.ColumnName] = bookRow[column.ColumnName];
                    }
                }

                if (!cartItem.Table.Columns.Contains("quantity"))
                {
                    cartItem.Table.Columns.Add("quantity", typeof(int));
                }
                cartItem["quantity"] = 1;

                _cart.Add(cartItem);
            }

            UpdateCartDisplay();
        }

        private void RemoveFromCart(int bookId)
        {
            var item = _cart.FirstOrDefault(item => Convert.ToInt32(item["id"]) == bookId);
            if (item != null)
            {
                int currentQuantity = Convert.ToInt32(item["quantity"]);
                if (currentQuantity > 1)
                {
                    item["quantity"] = currentQuantity - 1;
                }
                else
                {
                    _cart.Remove(item);
                }
                UpdateCartDisplay();
            }
        }

        private void ClearCart()
        {
            if (_cart.Count > 0)
            {
                var result = MessageBox.Show("Очистить корзину?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _cart.Clear();
                    UpdateCartDisplay();
                }
            }
        }

        private void PlaceOrder()
        {
            if (_currentUser == null || _currentUser.Rows.Count == 0)
            {
                MessageBox.Show("Пользователь не выбран", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int userId = Convert.ToInt32(_currentUser.Rows[0]["id"]);
                            decimal totalAmount = _cart.Sum(item =>
                                Convert.ToDecimal(item["price"]) * Convert.ToInt32(item["quantity"]));

                            var orderCmd = new NpgsqlCommand(
                                @"INSERT INTO orders (user_id, total_amount) 
                                  VALUES (@user_id, @total_amount) 
                                  RETURNING id, order_date, order_time",
                                conn, transaction);

                            orderCmd.Parameters.AddWithValue("@user_id", userId);
                            orderCmd.Parameters.AddWithValue("@total_amount", totalAmount);

                            int orderId;
                            DateTime orderDate;
                            TimeSpan orderTime;

                            using (var reader = orderCmd.ExecuteReader())
                            {
                                reader.Read();
                                orderId = reader.GetInt32(0);
                                orderDate = reader.GetDateTime(1);
                                orderTime = reader.GetTimeSpan(2);
                                reader.Close();
                            }

                            foreach (var cartItem in _cart)
                            {
                                int bookId = Convert.ToInt32(cartItem["id"]);
                                int quantity = Convert.ToInt32(cartItem["quantity"]);
                                decimal price = Convert.ToDecimal(cartItem["price"]);

                                var itemCmd = new NpgsqlCommand(
                                    @"INSERT INTO order_items (order_id, book_id, quantity, unit_price) 
                                      VALUES (@order_id, @book_id, @quantity, @price)",
                                    conn, transaction);

                                itemCmd.Parameters.AddWithValue("@order_id", orderId);
                                itemCmd.Parameters.AddWithValue("@book_id", bookId);
                                itemCmd.Parameters.AddWithValue("@quantity", quantity);
                                itemCmd.Parameters.AddWithValue("@price", price);

                                itemCmd.ExecuteNonQuery();
                                var updateStockCmd = new NpgsqlCommand(
                                @"UPDATE books 
                                    SET stock_quantity = GREATEST(0, stock_quantity - @qty)
                                    WHERE id = @book_id",
                                conn, transaction);

                                updateStockCmd.Parameters.AddWithValue("@qty", quantity);
                                updateStockCmd.Parameters.AddWithValue("@book_id", bookId);

                                updateStockCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            MessageBox.Show(
                                $"Заказ №{orderId} успешно оформлен!\n\n" +
                                $"Дата: {orderDate:dd.MM.yyyy}\n" +
                                $"Время: {orderTime:hh\\:mm}\n" +
                                $"Сумма: {totalAmount:N2} руб.\n" +
                                $"Товаров: {_cart.Sum(item => Convert.ToInt32(item["quantity"]))}",
                                "Заказ оформлен",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            _cart.Clear();
                            UpdateCartDisplay();
                            LoadAvailableBooks();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при оформлении заказа: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка оформления заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserOrders()
        {
            if (_currentUser == null || _currentUser.Rows.Count == 0)
            {
                MessageBox.Show("Пользователь не выбран", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int userId = Convert.ToInt32(_currentUser.Rows[0]["id"]);

                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT 
                            o.id,
                            o.order_date,
                            o.order_time,
                            o.total_amount,
                            o.status,
                            u.full_name
                        FROM orders o
                        JOIN users u ON o.user_id = u.id
                        WHERE u.id = @user_id
                        ORDER BY o.order_date DESC, o.order_time DESC";

                    var cmd = new NpgsqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    var adapter = new NpgsqlDataAdapter(cmd);
                    _userOrders = new DataTable();
                    adapter.Fill(_userOrders);

                    dgvOrders.DataSource = _userOrders;

                    if (dgvOrders.Columns.Count > 0)
                    {
                        dgvOrders.Columns["id"].HeaderText = "№";
                        dgvOrders.Columns["order_date"].HeaderText = "Дата";
                        dgvOrders.Columns["order_time"].HeaderText = "Время";
                        dgvOrders.Columns["total_amount"].HeaderText = "Сумма";
                        dgvOrders.Columns["status"].HeaderText = "Статус";
                        dgvOrders.Columns["full_name"].HeaderText = "Покупатель";

                        dgvOrders.Columns["total_amount"].DefaultCellStyle.Format = "N2";
                        dgvOrders.Columns["order_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
                        dgvOrders.Columns["order_time"].DefaultCellStyle.Format = "hh\\:mm";

                        foreach (DataGridViewRow row in dgvOrders.Rows)
                        {
                            if (row.Cells["status"] != null && row.Cells["status"].Value != null)
                            {
                                string status = row.Cells["status"].Value.ToString();
                                if (status == "Выполнен")
                                    row.DefaultCellStyle.ForeColor = Color.Green;
                                else if (status == "Отменен")
                                    row.DefaultCellStyle.ForeColor = Color.Red;
                                else if (status == "В обработке")
                                    row.DefaultCellStyle.ForeColor = Color.Blue;
                            }
                        }
                    }

                    lblOrderCount.Text = $"Заказов: {_userOrders.Rows.Count}";

                    if (_userOrders.Rows.Count > 0)
                    {
                        int firstOrderId = Convert.ToInt32(_userOrders.Rows[0]["id"]);
                        LoadOrderDetails(firstOrderId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderDetails(int orderId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT 
                            oi.order_id,
                            b.title,
                            a.name as author,
                            oi.quantity,
                            oi.unit_price,
                            oi.subtotal
                        FROM order_items oi
                        JOIN books b ON oi.book_id = b.id
                        JOIN authors a ON b.author_id = a.id
                        WHERE oi.order_id = @order_id
                        ORDER BY b.title";

                    var cmd = new NpgsqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@order_id", orderId);

                    var adapter = new NpgsqlDataAdapter(cmd);
                    _orderDetails = new DataTable();
                    adapter.Fill(_orderDetails);

                    dgvOrderDetails.DataSource = _orderDetails;

                    if (dgvOrderDetails.Columns.Count > 0)
                    {
                        dgvOrderDetails.Columns["order_id"].Visible = false;
                        dgvOrderDetails.Columns["title"].HeaderText = "Книга";
                        dgvOrderDetails.Columns["author"].HeaderText = "Автор";
                        dgvOrderDetails.Columns["quantity"].HeaderText = "Кол-во";
                        dgvOrderDetails.Columns["unit_price"].HeaderText = "Цена";
                        dgvOrderDetails.Columns["subtotal"].HeaderText = "Сумма";

                        dgvOrderDetails.Columns["unit_price"].DefaultCellStyle.Format = "N2";
                        dgvOrderDetails.Columns["subtotal"].DefaultCellStyle.Format = "N2";
                    }

                    lblDetailCount.Text = $"Позиций: {_orderDetails.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки деталей заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchBooksByAuthorWithMinPrice()
        {
            string authorName = txtAuthorSearch.Text.Trim();
            decimal minPrice = numMinPrice.Value;

            if (string.IsNullOrEmpty(authorName))
            {
                MessageBox.Show("Введите имя автора для поиска", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    var cmd = new NpgsqlCommand(
                        "SELECT * FROM find_books_by_author_min_price(@author_name, @min_price)",
                        conn);
                    cmd.Parameters.AddWithValue("@author_name", authorName);
                    cmd.Parameters.AddWithValue("@min_price", minPrice);

                    var adapter = new NpgsqlDataAdapter(cmd);
                    _authorSearchResults = new DataTable();
                    adapter.Fill(_authorSearchResults);

                    dgvAuthorSearch.DataSource = _authorSearchResults;

                    if (dgvAuthorSearch.Columns.Count > 0)
                    {
                        dgvAuthorSearch.Columns["book_id"].HeaderText = "ID";
                        dgvAuthorSearch.Columns["book_title"].HeaderText = "Название";
                        dgvAuthorSearch.Columns["author_name"].HeaderText = "Автор";
                        dgvAuthorSearch.Columns["price"].HeaderText = "Цена";
                        dgvAuthorSearch.Columns["genre"].HeaderText = "Жанр";
                        dgvAuthorSearch.Columns["stock_quantity"].HeaderText = "В наличии";
                        dgvAuthorSearch.Columns["publish_year"].HeaderText = "Год издания";
                        dgvAuthorSearch.Columns["availability_status"].HeaderText = "Статус";

                        dgvAuthorSearch.Columns["price"].DefaultCellStyle.Format = "N2";

                        foreach (DataGridViewRow row in dgvAuthorSearch.Rows)
                        {
                            var stockCell = row.Cells["stock_quantity"];
                            if (stockCell != null && stockCell.Value != DBNull.Value)
                            {
                                int stock = Convert.ToInt32(stockCell.Value);
                                if (stock == 0)
                                {
                                    row.DefaultCellStyle.ForeColor = Color.Gray;
                                    row.DefaultCellStyle.BackColor = Color.LightGray;
                                }
                                else if (stock < 5)
                                {
                                    row.DefaultCellStyle.ForeColor = Color.Orange;
                                }
                            }
                        }
                    }

                    lblSearchResult.Text = $"Найдено книг: {_authorSearchResults.Rows.Count}";
                    HideSuggestions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterBooks()
        {
            string search = txtBookSearch.Text.Trim();
            string genre = cmbGenreFilter.SelectedItem?.ToString();

            if (_availableBooks == null) return;

            string filter = "";

            if (!string.IsNullOrEmpty(search))
                filter += $"title LIKE '%{search}%' OR author_name LIKE '%{search}%'";

            if (genre != "Все жанры" && !string.IsNullOrEmpty(genre))
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += $"genre = '{genre}'";
            }

            _availableBooks.DefaultView.RowFilter = filter;
            int filteredCount = _availableBooks.DefaultView.Count;
            lblBookCount.Text = $"Найдено книг: {filteredCount}";
        }

        private void AddFromSearchResults()
        {
            if (dgvAuthorSearch.CurrentRow != null && dgvAuthorSearch.CurrentRow.DataBoundItem is DataRowView rowView)
            {
                try
                {
                    int bookId = Convert.ToInt32(rowView.Row["book_id"]);

                    if (_availableBooks != null)
                    {
                        var bookRows = _availableBooks.Select($"id = {bookId}");
                        if (bookRows.Length > 0)
                        {
                            AddToCart(bookRows[0]);
                            // MessageBox.Show("Книга добавлена в корзину", "Успешно",
                            //  MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            LoadBookDetailsAndAddToCart(bookId, rowView);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления книги: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу из результатов поиска", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadBookDetailsAndAddToCart(int bookId, DataRowView rowView)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT 
                            b.id, 
                            b.title, 
                            b.price, 
                            a.name as author_name, 
                            b.genre, 
                            b.published, 
                            b.stock_quantity,
                            CASE 
                                WHEN b.stock_quantity > 0 THEN 'В наличии'
                                ELSE 'Нет в наличии'
                            END as availability
                        FROM books b
                        JOIN authors a ON b.author_id = a.id
                        WHERE b.id = @book_id";

                    var cmd = new NpgsqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@book_id", bookId);

                    var adapter = new NpgsqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        AddToCart(dt.Rows[0]);
                        //MessageBox.Show("Книга добавлена в корзину", "Успешно",
                        //  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DataRow tempRow = _availableBooks.NewRow();
                        tempRow["id"] = bookId;
                        tempRow["title"] = rowView.Row["book_title"];
                        tempRow["author_name"] = rowView.Row["author_name"];
                        tempRow["price"] = rowView.Row["price"];
                        tempRow["stock_quantity"] = rowView.Row["stock_quantity"];
                        tempRow["genre"] = rowView.Row["genre"];
                        tempRow["published"] = rowView.Row["publish_year"];
                        tempRow["availability"] = Convert.ToInt32(rowView.Row["stock_quantity"]) > 0 ? "В наличии" : "Нет в наличии";

                        AddToCart(tempRow);
                        //MessageBox.Show("Книга добавлена в корзину", "Успешно",
                        //  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления книги: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчики событий
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvBooks.CurrentRow != null && dgvBooks.CurrentRow.DataBoundItem is DataRowView rowView)
            {
                DataRow bookRow = rowView.Row;
                int stock = Convert.ToInt32(bookRow["stock_quantity"]);

                if (stock <= 0)
                {
                    MessageBox.Show("Эта книга временно отсутствует на складе",
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                AddToCart(bookRow);
            }
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow != null && dgvCart.CurrentRow.DataBoundItem is DataRowView rowView)
            {
                int bookId = Convert.ToInt32(rowView.Row["BookId"]);
                RemoveFromCart(bookId);
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            ClearCart();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            PlaceOrder();
        }

        private void btnLoadOrders_Click(object sender, EventArgs e)
        {
            LoadUserOrders();
        }

        private void btnSearchAuthor_Click(object sender, EventArgs e)
        {
            SearchBooksByAuthorWithMinPrice();
        }

        private void txtBookSearch_TextChanged(object sender, EventArgs e)
        {
            FilterBooks();
        }

        private void cmbGenreFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterBooks();
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow != null && dgvOrders.CurrentRow.DataBoundItem is DataRowView rowView)
            {
                int orderId = Convert.ToInt32(rowView.Row["id"]);
                LoadOrderDetails(orderId);
            }
        }
        private void btnRefreshBooks_Click(object sender, EventArgs e)
        {
            LoadAvailableBooks();
            InitializeGenreFilter();
        }

        private void txtAuthorSearch_TextChanged(object sender, EventArgs e)
        {
            _suggestionTimer.Stop();
            _suggestionTimer.Start();
        }

        private void txtAuthorSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (_suggestionsListBox.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        _suggestionsListBox.Focus();
                        e.Handled = true;
                        break;

                    case Keys.Enter:
                        HideSuggestions();
                        SearchBooksByAuthorWithMinPrice();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchBooksByAuthorWithMinPrice();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void btnAddFromSearchResults_Click(object sender, EventArgs e)
        {
            AddFromSearchResults();
        }

        private void btnViewAllBooks_Click(object sender, EventArgs e)
        {
            txtBookSearch.Clear();
            cmbGenreFilter.SelectedIndex = 0;

            if (_availableBooks != null)
            {
                _availableBooks.DefaultView.RowFilter = "";
                lblBookCount.Text = $"Всего книг: {_availableBooks.Rows.Count}";
            }
        }

        private void dgvAuthorSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AddFromSearchResults();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionSuggestionsBox();
        }
    private void LoadManageBooks()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    var query = @"
                SELECT 
                    b.id, 
                    b.title, 
                    b.price, 
                    a.name as author_name, 
                    b.published,
                    b.genre, 
                    b.stock_quantity,
                    CASE 
                        WHEN b.stock_quantity > 0 THEN 'В наличии'
                        ELSE 'Нет в наличии'
                    END as availability
                FROM books b
                JOIN authors a ON b.author_id = a.id
                ORDER BY b.title";

                    var adapter = new NpgsqlDataAdapter(query, conn);
                    _manageBooks = new DataTable();
                    adapter.Fill(_manageBooks);

                    dgvManageBooks.DataSource = _manageBooks;

                    if (dgvManageBooks.Columns.Count > 0)
                    {
                        dgvManageBooks.Columns["id"].Visible = false;
                        dgvManageBooks.Columns["title"].HeaderText = "Название";
                        dgvManageBooks.Columns["price"].HeaderText = "Цена";
                        dgvManageBooks.Columns["author_name"].HeaderText = "Автор";
                        dgvManageBooks.Columns["published"].HeaderText = "Год";
                        dgvManageBooks.Columns["genre"].HeaderText = "Жанр";
                        dgvManageBooks.Columns["stock_quantity"].HeaderText = "Количество";
                        dgvManageBooks.Columns["availability"].HeaderText = "Статус";

                        dgvManageBooks.Columns["price"].DefaultCellStyle.Format = "N2";
                        dgvManageBooks.Columns["published"].DefaultCellStyle.Format = "yyyy";
                    }

                    LoadAuthorsForManage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки книг для управления: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAuthorsForManage()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    var cmd = new NpgsqlCommand("SELECT id, name FROM authors ORDER BY name", conn);
                    var adapter = new NpgsqlDataAdapter(cmd);
                    _allAuthorsForManage = new DataTable();
                    adapter.Fill(_allAuthorsForManage);

                    cmbBookAuthor.Items.Clear();
                    foreach (DataRow row in _allAuthorsForManage.Rows)
                    {
                        cmbBookAuthor.Items.Add(row["name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки авторов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableEditMode(bool enable, bool isNew = false)
        {
            groupBoxBookDetails.Enabled = enable;
            btnSaveBook.Enabled = enable;
            btnCancelEdit.Enabled = enable;

            if (enable)
            {
                btnAddNewBook.Enabled = false;
                btnEditBook.Enabled = false;
                btnDeleteBook.Enabled = false;

                if (isNew)
                {
                    ClearBookForm();
                    _isEditMode = false;
                    _editingBookId = 0;
                    lblManageStatus.Text = "Режим добавления новой книги";
                    lblManageStatus.ForeColor = Color.Blue;
                }
                else
                {
                    _isEditMode = true;
                    lblManageStatus.Text = "Режим редактирования книги";
                    lblManageStatus.ForeColor = Color.Orange;
                }
            }
            else
            {
                btnAddNewBook.Enabled = true;
                btnEditBook.Enabled = true;
                btnDeleteBook.Enabled = true;
                _isEditMode = false;
                _editingBookId = 0;
                ClearBookForm();
                lblManageStatus.Text = "Готов к работе";
                lblManageStatus.ForeColor = Color.Green;
            }
        }

        private void ClearBookForm()
        {
            txtBookTitle.Clear();
            numBookPrice.Value = 0;
            if (cmbBookAuthor.Items.Count > 0)
                cmbBookAuthor.SelectedIndex = 0;
            numPublishedYear.Value = DateTime.Now.Year;
            txtBookGenre.Clear();
            numStockQuantity.Value = 1;
        }

        private int GetOrCreateAuthorId(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
                throw new ArgumentException("Имя автора не может быть пустым");

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                // Проверяем, существует ли автор
                var checkCmd = new NpgsqlCommand("SELECT id FROM authors WHERE name = @name", conn);
                checkCmd.Parameters.AddWithValue("@name", authorName);

                var result = checkCmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }

                // Создаем нового автора
                var insertCmd = new NpgsqlCommand(
                    "INSERT INTO authors (name) VALUES (@name) RETURNING id", conn);
                insertCmd.Parameters.AddWithValue("@name", authorName);

                var newId = insertCmd.ExecuteScalar();
                return Convert.ToInt32(newId);
            }
        }

        private void SaveBook()
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text))
            {
                MessageBox.Show("Введите название книги", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookTitle.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbBookAuthor.Text))
            {
                MessageBox.Show("Выберите автора", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBookAuthor.Focus();
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    // Получаем или создаем автора
                    int authorId = GetOrCreateAuthorId(cmbBookAuthor.Text);

                    if (_isEditMode && _editingBookId > 0)
                    {
                        // Редактирование существующей книги
                        var cmd = new NpgsqlCommand(@"
                    UPDATE books 
                    SET title = @title,
                        price = @price,
                        author_id = @author_id,
                        published = @published,
                        genre = @genre,
                        stock_quantity = @stock_quantity
                    WHERE id = @id", conn);

                        cmd.Parameters.AddWithValue("@id", _editingBookId);
                        cmd.Parameters.AddWithValue("@title", txtBookTitle.Text);
                        cmd.Parameters.AddWithValue("@price", numBookPrice.Value);
                        cmd.Parameters.AddWithValue("@author_id", authorId);
                        cmd.Parameters.AddWithValue("@published", new DateTime((int)numPublishedYear.Value, 1, 1));
                        cmd.Parameters.AddWithValue("@genre", txtBookGenre.Text);
                        cmd.Parameters.AddWithValue("@stock_quantity", (int)numStockQuantity.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Книга успешно обновлена!", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Добавление новой книги
                        var cmd = new NpgsqlCommand(@"
                    INSERT INTO books 
                        (title, price, author_id, published, genre, stock_quantity, created_at) 
                    VALUES 
                        (@title, @price, @author_id, @published, @genre, @stock_quantity, CURRENT_TIMESTAMP)
                    RETURNING id", conn);

                        cmd.Parameters.AddWithValue("@title", txtBookTitle.Text);
                        cmd.Parameters.AddWithValue("@price", numBookPrice.Value);
                        cmd.Parameters.AddWithValue("@author_id", authorId);
                        cmd.Parameters.AddWithValue("@published", new DateTime((int)numPublishedYear.Value, 1, 1));
                        cmd.Parameters.AddWithValue("@genre", txtBookGenre.Text);
                        cmd.Parameters.AddWithValue("@stock_quantity", (int)numStockQuantity.Value);

                        var newId = cmd.ExecuteScalar();
                        _editingBookId = Convert.ToInt32(newId);

                        MessageBox.Show($"Книга успешно добавлена! ID: {_editingBookId}", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Обновляем данные
                    EnableEditMode(false);
                    LoadManageBooks();
                    LoadAvailableBooks(); // Обновляем основной каталог
                    LoadAllAuthors(); // Обновляем список авторов для автодополнения
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения книги: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteBook()
        {
            if (dgvManageBooks.CurrentRow == null)
            {
                MessageBox.Show("Выберите книгу для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить выбранную книгу?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int bookId = Convert.ToInt32(dgvManageBooks.CurrentRow.Cells["id"].Value);

                    using (var conn = new NpgsqlConnection(_connectionString))
                    {
                        conn.Open();

                        // Проверяем, есть ли заказы с этой книгой
                        var checkCmd = new NpgsqlCommand(
                            "SELECT COUNT(*) FROM order_items WHERE book_id = @book_id", conn);
                        checkCmd.Parameters.AddWithValue("@book_id", bookId);

                        int orderCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (orderCount > 0)
                        {
                            MessageBox.Show("Нельзя удалить книгу, которая есть в заказах!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Удаляем книгу
                        var deleteCmd = new NpgsqlCommand("DELETE FROM books WHERE id = @id", conn);
                        deleteCmd.Parameters.AddWithValue("@id", bookId);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Книга успешно удалена!", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Обновляем данные
                            LoadManageBooks();
                            LoadAvailableBooks();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления книги: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Обработчики событий (добавьте в конец класса MainForm):

        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            EnableEditMode(true, true);
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            if (dgvManageBooks.CurrentRow == null)
            {
                MessageBox.Show("Выберите книгу для редактирования", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRowView rowView = (DataRowView)dgvManageBooks.CurrentRow.DataBoundItem;

                _editingBookId = Convert.ToInt32(rowView["id"]);
                txtBookTitle.Text = rowView["title"].ToString();
                numBookPrice.Value = Convert.ToDecimal(rowView["price"]);

                // Устанавливаем автора
                string authorName = rowView["author_name"].ToString();
                cmbBookAuthor.Text = authorName;

                // Устанавливаем год издания - ОБНОВЛЕНО
                if (rowView["published"] != DBNull.Value)
                {
                    object publishedValue = rowView["published"];
                    int year;

                    // Обрабатываем разные типы дат
                    if (publishedValue is DateOnly dateOnly)
                    {
                        year = dateOnly.Year;
                    }
                    else if (publishedValue is DateTime dateTime)
                    {
                        year = dateTime.Year;
                    }
                    else if (publishedValue is string dateString)
                    {
                        // Пробуем распарсить строку
                        if (DateTime.TryParse(dateString, out DateTime parsedDate))
                        {
                            year = parsedDate.Year;
                        }
                        else
                        {
                            year = DateTime.Now.Year;
                        }
                    }
                    else
                    {
                        // Пробуем сконвертировать
                        try
                        {
                            DateTime dt = Convert.ToDateTime(publishedValue);
                            year = dt.Year;
                        }
                        catch
                        {
                            year = DateTime.Now.Year;
                        }
                    }

                    numPublishedYear.Value = year;
                }
                else
                {
                    numPublishedYear.Value = DateTime.Now.Year;
                }

                txtBookGenre.Text = rowView["genre"].ToString();
                numStockQuantity.Value = Convert.ToDecimal(rowView["stock_quantity"]);

                EnableEditMode(true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных книги: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            DeleteBook();
        }

        private void btnSaveBook_Click(object sender, EventArgs e)
        {
            SaveBook();
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            EnableEditMode(false);
        }

        private void btnRefreshManage_Click(object sender, EventArgs e)
        {
            LoadManageBooks();
            LoadAuthorsForManage();
            EnableEditMode(false);
        }

        private void dgvManageBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (_isEditMode && dgvManageBooks.CurrentRow != null)
            {
                // Если в режиме редактирования и выбрана другая строка, спрашиваем о сохранении
                var result = MessageBox.Show("Сохранить текущие изменения?", "Внимание",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveBook();
                }
                else if (result == DialogResult.No)
                {
                    EnableEditMode(false);
                }
                // Если Cancel - ничего не делаем
            }
        }

        // Обновите метод tabControl_SelectedIndexChanged:
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0: // Каталог
                    LoadAvailableBooks();
                    break;
                case 1: // Мои заказы
                    LoadUserOrders();
                    break;
                case 2: // Поиск по автору
                    PositionSuggestionsBox();
                    txtAuthorSearch.Clear();
                    dgvAuthorSearch.DataSource = null;
                    lblSearchResult.Text = "Найдено книг: 0";
                    HideSuggestions();
                    break;
                case 3: // Управление книгами
                    LoadManageBooks();
                    LoadAuthorsForManage();
                    EnableEditMode(false);
                    break;
            }
        }
        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            // Ничего не делаем, но метод должен существовать, чтобы не было ошибки компиляции.
        }
    }
}
        