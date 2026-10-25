using BookStore;
namespace BookStore
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl;
        private TabPage tabCatalog;
        private TabPage tabMyOrders;
        private TabPage tabProcedures;
        private TabPage tabManageBooks;
        private Label lblCurrentUser;
        private DataGridView dgvBooks;
        private TextBox txtBookSearch;
        private Label label1;
        private ComboBox cmbGenreFilter;
        private Label label2;
        private Label lblBookCount;
        private Button btnRefreshBooks;
        private Button btnAddToCart;
        private DataGridView dgvCart;
        private Label lblCartItems;
        private Label lblCartTotal;
        private Button btnRemoveFromCart;
        private Button btnClearCart;
        private Button btnPlaceOrder;
        private DataGridView dgvOrders;
        private Button btnLoadOrders;
        private Label lblOrderCount;
        private DataGridView dgvOrderDetails;
        private Label lblDetailCount;
        private TextBox txtAuthorSearch;
        private Label label3;
        private Button btnSearchAuthor;
        private DataGridView dgvAuthorSearch;
        private Label lblSearchResult;
        private Panel panel1;
        private Button btnAddFromSearchResults;
        private Label label4;
        private NumericUpDown numMinPrice;
        private Button btnViewAllBooks;
        private Panel panelManageBooks;
        private DataGridView dgvManageBooks;
        private Button btnDeleteBook;
        private Button btnAddNewBook;
        private GroupBox groupBoxBookDetails;
        private TextBox txtBookTitle;
        private Label label5;
        private Label label6;
        private NumericUpDown numBookPrice;
        private Label label7;
        private ComboBox cmbBookAuthor;
        private Label label8;
        private NumericUpDown numPublishedYear;
        private Label label9;
        private TextBox txtBookGenre;
        private Label label10;
        private NumericUpDown numStockQuantity;
        private Button btnSaveBook;
        private Button btnCancelEdit;
        private Button btnRefreshManage;
        private Label lblManageStatus;
        private Button btnEditBook;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabCatalog = new TabPage();
            btnViewAllBooks = new Button();
            panel1 = new Panel();
            btnPlaceOrder = new Button();
            btnClearCart = new Button();
            btnRemoveFromCart = new Button();
            lblCartTotal = new Label();
            lblCartItems = new Label();
            dgvCart = new DataGridView();
            btnAddToCart = new Button();
            btnRefreshBooks = new Button();
            lblBookCount = new Label();
            cmbGenreFilter = new ComboBox();
            label2 = new Label();
            txtBookSearch = new TextBox();
            label1 = new Label();
            dgvBooks = new DataGridView();
            tabMyOrders = new TabPage();
            lblDetailCount = new Label();
            dgvOrderDetails = new DataGridView();
            lblOrderCount = new Label();
            btnLoadOrders = new Button();
            dgvOrders = new DataGridView();
            tabProcedures = new TabPage();
            btnAddFromSearchResults = new Button();
            numMinPrice = new NumericUpDown();
            label4 = new Label();
            lblSearchResult = new Label();
            dgvAuthorSearch = new DataGridView();
            btnSearchAuthor = new Button();
            label3 = new Label();
            txtAuthorSearch = new TextBox();
            tabManageBooks = new TabPage();
            panelManageBooks = new Panel();
            lblManageStatus = new Label();
            btnRefreshManage = new Button();
            btnCancelEdit = new Button();
            btnSaveBook = new Button();
            groupBoxBookDetails = new GroupBox();
            numStockQuantity = new NumericUpDown();
            label10 = new Label();
            txtBookGenre = new TextBox();
            label9 = new Label();
            numPublishedYear = new NumericUpDown();
            label8 = new Label();
            cmbBookAuthor = new ComboBox();
            label7 = new Label();
            numBookPrice = new NumericUpDown();
            label6 = new Label();
            txtBookTitle = new TextBox();
            label5 = new Label();
            btnEditBook = new Button();
            btnDeleteBook = new Button();
            btnAddNewBook = new Button();
            dgvManageBooks = new DataGridView();
            lblCurrentUser = new Label();
            tabControl.SuspendLayout();
            tabCatalog.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            tabMyOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            tabProcedures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAuthorSearch).BeginInit();
            tabManageBooks.SuspendLayout();
            panelManageBooks.SuspendLayout();
            groupBoxBookDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numStockQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPublishedYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBookPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvManageBooks).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabCatalog);
            tabControl.Controls.Add(tabMyOrders);
            tabControl.Controls.Add(tabProcedures);
            tabControl.Controls.Add(tabManageBooks);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 46);
            tabControl.Margin = new Padding(4, 5, 4, 5);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1600, 985);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabCatalog
            // 
            tabCatalog.Controls.Add(btnViewAllBooks);
            tabCatalog.Controls.Add(panel1);
            tabCatalog.Controls.Add(btnAddToCart);
            tabCatalog.Controls.Add(btnRefreshBooks);
            tabCatalog.Controls.Add(lblBookCount);
            tabCatalog.Controls.Add(cmbGenreFilter);
            tabCatalog.Controls.Add(label2);
            tabCatalog.Controls.Add(txtBookSearch);
            tabCatalog.Controls.Add(label1);
            tabCatalog.Controls.Add(dgvBooks);
            tabCatalog.Location = new Point(4, 29);
            tabCatalog.Margin = new Padding(4, 5, 4, 5);
            tabCatalog.Name = "tabCatalog";
            tabCatalog.Padding = new Padding(4, 5, 4, 5);
            tabCatalog.Size = new Size(1592, 952);
            tabCatalog.TabIndex = 0;
            tabCatalog.Text = "Каталог книг";
            tabCatalog.UseVisualStyleBackColor = true;
            // 
            // btnViewAllBooks
            // 
            btnViewAllBooks.Location = new Point(676, 96);
            btnViewAllBooks.Margin = new Padding(4, 5, 4, 5);
            btnViewAllBooks.Name = "btnViewAllBooks";
            btnViewAllBooks.Size = new Size(173, 46);
            btnViewAllBooks.TabIndex = 9;
            btnViewAllBooks.Text = "Показать все";
            btnViewAllBooks.UseVisualStyleBackColor = true;
            btnViewAllBooks.Click += btnViewAllBooks_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnPlaceOrder);
            panel1.Controls.Add(btnClearCart);
            panel1.Controls.Add(btnRemoveFromCart);
            panel1.Controls.Add(lblCartTotal);
            panel1.Controls.Add(lblCartItems);
            panel1.Controls.Add(dgvCart);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1059, 5);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(529, 942);
            panel1.TabIndex = 8;
            // 
            // btnPlaceOrder
            // 
            btnPlaceOrder.BackColor = Color.FromArgb(76, 175, 80);
            btnPlaceOrder.FlatStyle = FlatStyle.Flat;
            btnPlaceOrder.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnPlaceOrder.ForeColor = Color.White;
            btnPlaceOrder.Location = new Point(13, 877);
            btnPlaceOrder.Margin = new Padding(4, 5, 4, 5);
            btnPlaceOrder.Name = "btnPlaceOrder";
            btnPlaceOrder.Size = new Size(500, 54);
            btnPlaceOrder.TabIndex = 5;
            btnPlaceOrder.Text = "\U0001f6d2 ОФОРМИТЬ ЗАКАЗ";
            btnPlaceOrder.UseVisualStyleBackColor = false;
            btnPlaceOrder.Click += btnPlaceOrder_Click;
            // 
            // btnClearCart
            // 
            btnClearCart.Location = new Point(353, 823);
            btnClearCart.Margin = new Padding(4, 5, 4, 5);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(160, 46);
            btnClearCart.TabIndex = 4;
            btnClearCart.Text = "Очистить корзину";
            btnClearCart.UseVisualStyleBackColor = true;
            btnClearCart.Click += btnClearCart_Click;
            // 
            // btnRemoveFromCart
            // 
            btnRemoveFromCart.Location = new Point(13, 823);
            btnRemoveFromCart.Margin = new Padding(4, 5, 4, 5);
            btnRemoveFromCart.Name = "btnRemoveFromCart";
            btnRemoveFromCart.Size = new Size(180, 46);
            btnRemoveFromCart.TabIndex = 3;
            btnRemoveFromCart.Text = "- Убрать из корзины";
            btnRemoveFromCart.UseVisualStyleBackColor = true;
            btnRemoveFromCart.Click += btnRemoveFromCart_Click;
            // 
            // lblCartTotal
            // 
            lblCartTotal.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblCartTotal.Location = new Point(13, 777);
            lblCartTotal.Margin = new Padding(4, 0, 4, 0);
            lblCartTotal.Name = "lblCartTotal";
            lblCartTotal.Size = new Size(500, 38);
            lblCartTotal.TabIndex = 2;
            lblCartTotal.Text = "Общая сумма: 0 руб.";
            lblCartTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCartItems
            // 
            lblCartItems.Dock = DockStyle.Top;
            lblCartItems.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblCartItems.Location = new Point(0, 0);
            lblCartItems.Margin = new Padding(4, 0, 4, 0);
            lblCartItems.Name = "lblCartItems";
            lblCartItems.Size = new Size(527, 46);
            lblCartItems.TabIndex = 1;
            lblCartItems.Text = "\U0001f6d2 КОРЗИНА (0 товаров)";
            lblCartItems.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Location = new Point(13, 54);
            dgvCart.Margin = new Padding(4, 5, 4, 5);
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.RowHeadersVisible = false;
            dgvCart.RowHeadersWidth = 51;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(500, 715);
            dgvCart.TabIndex = 0;
            // 
            // btnAddToCart
            // 
            btnAddToCart.BackColor = Color.FromArgb(33, 150, 243);
            btnAddToCart.FlatStyle = FlatStyle.Flat;
            btnAddToCart.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.Location = new Point(867, 92);
            btnAddToCart.Margin = new Padding(4, 5, 4, 5);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(173, 54);
            btnAddToCart.TabIndex = 7;
            btnAddToCart.Text = "+ В корзину";
            btnAddToCart.UseVisualStyleBackColor = false;
            btnAddToCart.Click += btnAddToCart_Click;
            // 
            // btnRefreshBooks
            // 
            btnRefreshBooks.Location = new Point(867, 31);
            btnRefreshBooks.Margin = new Padding(4, 5, 4, 5);
            btnRefreshBooks.Name = "btnRefreshBooks";
            btnRefreshBooks.Size = new Size(173, 46);
            btnRefreshBooks.TabIndex = 6;
            btnRefreshBooks.Text = "Обновить";
            btnRefreshBooks.UseVisualStyleBackColor = true;
            btnRefreshBooks.Click += btnRefreshBooks_Click;
            // 
            // lblBookCount
            // 
            lblBookCount.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblBookCount.Location = new Point(13, 92);
            lblBookCount.Margin = new Padding(4, 0, 4, 0);
            lblBookCount.Name = "lblBookCount";
            lblBookCount.Size = new Size(200, 38);
            lblBookCount.TabIndex = 5;
            lblBookCount.Text = "Найдено книг: 0";
            lblBookCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbGenreFilter
            // 
            cmbGenreFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGenreFilter.FormattingEnabled = true;
            cmbGenreFilter.Location = new Point(560, 38);
            cmbGenreFilter.Margin = new Padding(4, 5, 4, 5);
            cmbGenreFilter.Name = "cmbGenreFilter";
            cmbGenreFilter.Size = new Size(265, 28);
            cmbGenreFilter.TabIndex = 4;
            cmbGenreFilter.SelectedIndexChanged += cmbGenreFilter_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(493, 43);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 3;
            label2.Text = "Жанр:";
            // 
            // txtBookSearch
            // 
            txtBookSearch.Location = new Point(187, 38);
            txtBookSearch.Margin = new Padding(4, 5, 4, 5);
            txtBookSearch.Name = "txtBookSearch";
            txtBookSearch.Size = new Size(265, 27);
            txtBookSearch.TabIndex = 2;
            txtBookSearch.TextChanged += txtBookSearch_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 43);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(151, 20);
            label1.TabIndex = 1;
            label1.Text = "Поиск по названию:";
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Location = new Point(13, 154);
            dgvBooks.Margin = new Padding(4, 5, 4, 5);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(1027, 769);
            dgvBooks.TabIndex = 0;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
            // 
            // tabMyOrders
            // 
            tabMyOrders.Controls.Add(lblDetailCount);
            tabMyOrders.Controls.Add(dgvOrderDetails);
            tabMyOrders.Controls.Add(lblOrderCount);
            tabMyOrders.Controls.Add(btnLoadOrders);
            tabMyOrders.Controls.Add(dgvOrders);
            tabMyOrders.Location = new Point(4, 29);
            tabMyOrders.Margin = new Padding(4, 5, 4, 5);
            tabMyOrders.Name = "tabMyOrders";
            tabMyOrders.Padding = new Padding(4, 5, 4, 5);
            tabMyOrders.Size = new Size(1592, 952);
            tabMyOrders.TabIndex = 1;
            tabMyOrders.Text = "Мои заказы";
            tabMyOrders.UseVisualStyleBackColor = true;
            // 
            // lblDetailCount
            // 
            lblDetailCount.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblDetailCount.Location = new Point(800, 15);
            lblDetailCount.Margin = new Padding(4, 0, 4, 0);
            lblDetailCount.Name = "lblDetailCount";
            lblDetailCount.Size = new Size(240, 38);
            lblDetailCount.TabIndex = 4;
            lblDetailCount.Text = "Позиций: 0";
            lblDetailCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvOrderDetails
            // 
            dgvOrderDetails.AllowUserToAddRows = false;
            dgvOrderDetails.AllowUserToDeleteRows = false;
            dgvOrderDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderDetails.Location = new Point(800, 62);
            dgvOrderDetails.Margin = new Padding(4, 5, 4, 5);
            dgvOrderDetails.Name = "dgvOrderDetails";
            dgvOrderDetails.ReadOnly = true;
            dgvOrderDetails.RowHeadersVisible = false;
            dgvOrderDetails.RowHeadersWidth = 51;
            dgvOrderDetails.Size = new Size(773, 862);
            dgvOrderDetails.TabIndex = 3;
            // 
            // lblOrderCount
            // 
            lblOrderCount.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblOrderCount.Location = new Point(13, 15);
            lblOrderCount.Margin = new Padding(4, 0, 4, 0);
            lblOrderCount.Name = "lblOrderCount";
            lblOrderCount.Size = new Size(240, 38);
            lblOrderCount.TabIndex = 2;
            lblOrderCount.Text = "Заказов: 0";
            lblOrderCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnLoadOrders
            // 
            btnLoadOrders.Location = new Point(267, 15);
            btnLoadOrders.Margin = new Padding(4, 5, 4, 5);
            btnLoadOrders.Name = "btnLoadOrders";
            btnLoadOrders.Size = new Size(160, 38);
            btnLoadOrders.TabIndex = 1;
            btnLoadOrders.Text = "Загрузить заказы";
            btnLoadOrders.UseVisualStyleBackColor = true;
            btnLoadOrders.Click += btnLoadOrders_Click;
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrders.Location = new Point(13, 62);
            dgvOrders.Margin = new Padding(4, 5, 4, 5);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowHeadersWidth = 51;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(773, 862);
            dgvOrders.TabIndex = 0;
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;
            // 
            // tabProcedures
            // 
            tabProcedures.Controls.Add(btnAddFromSearchResults);
            tabProcedures.Controls.Add(numMinPrice);
            tabProcedures.Controls.Add(label4);
            tabProcedures.Controls.Add(lblSearchResult);
            tabProcedures.Controls.Add(dgvAuthorSearch);
            tabProcedures.Controls.Add(btnSearchAuthor);
            tabProcedures.Controls.Add(label3);
            tabProcedures.Controls.Add(txtAuthorSearch);
            tabProcedures.Location = new Point(4, 29);
            tabProcedures.Margin = new Padding(4, 5, 4, 5);
            tabProcedures.Name = "tabProcedures";
            tabProcedures.Size = new Size(1592, 952);
            tabProcedures.TabIndex = 2;
            tabProcedures.Text = "Поиск по автору";
            tabProcedures.UseVisualStyleBackColor = true;
            // 
            // btnAddFromSearchResults
            // 
            btnAddFromSearchResults.Location = new Point(933, 23);
            btnAddFromSearchResults.Margin = new Padding(4, 5, 4, 5);
            btnAddFromSearchResults.Name = "btnAddFromSearchResults";
            btnAddFromSearchResults.Size = new Size(200, 38);
            btnAddFromSearchResults.TabIndex = 8;
            btnAddFromSearchResults.Text = "Добавить в корзину";
            btnAddFromSearchResults.UseVisualStyleBackColor = true;
            btnAddFromSearchResults.Click += btnAddFromSearchResults_Click;
            // 
            // numMinPrice
            // 
            numMinPrice.DecimalPlaces = 2;
            numMinPrice.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numMinPrice.Location = new Point(813, 26);
            numMinPrice.Margin = new Padding(4, 5, 4, 5);
            numMinPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numMinPrice.Name = "numMinPrice";
            numMinPrice.Size = new Size(107, 27);
            numMinPrice.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(707, 31);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(84, 20);
            label4.TabIndex = 6;
            label4.Text = "Мин. цена:";
            // 
            // lblSearchResult
            // 
            lblSearchResult.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblSearchResult.Location = new Point(13, 77);
            lblSearchResult.Margin = new Padding(4, 0, 4, 0);
            lblSearchResult.Name = "lblSearchResult";
            lblSearchResult.Size = new Size(1560, 38);
            lblSearchResult.TabIndex = 4;
            lblSearchResult.Text = "Найдено книг: 0";
            lblSearchResult.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvAuthorSearch
            // 
            dgvAuthorSearch.AllowUserToAddRows = false;
            dgvAuthorSearch.AllowUserToDeleteRows = false;
            dgvAuthorSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuthorSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuthorSearch.Location = new Point(13, 123);
            dgvAuthorSearch.Margin = new Padding(4, 5, 4, 5);
            dgvAuthorSearch.Name = "dgvAuthorSearch";
            dgvAuthorSearch.ReadOnly = true;
            dgvAuthorSearch.RowHeadersVisible = false;
            dgvAuthorSearch.RowHeadersWidth = 51;
            dgvAuthorSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuthorSearch.Size = new Size(1560, 800);
            dgvAuthorSearch.TabIndex = 3;
            dgvAuthorSearch.CellDoubleClick += dgvAuthorSearch_CellDoubleClick;
            // 
            // btnSearchAuthor
            // 
            btnSearchAuthor.Location = new Point(1147, 23);
            btnSearchAuthor.Margin = new Padding(4, 5, 4, 5);
            btnSearchAuthor.Name = "btnSearchAuthor";
            btnSearchAuthor.Size = new Size(160, 38);
            btnSearchAuthor.TabIndex = 2;
            btnSearchAuthor.Text = "Найти книги";
            btnSearchAuthor.UseVisualStyleBackColor = true;
            btnSearchAuthor.Click += btnSearchAuthor_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 31);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(152, 20);
            label3.TabIndex = 1;
            label3.Text = "Введите имя автора:";
            // 
            // txtAuthorSearch
            // 
            txtAuthorSearch.Location = new Point(173, 26);
            txtAuthorSearch.Margin = new Padding(4, 5, 4, 5);
            txtAuthorSearch.Name = "txtAuthorSearch";
            txtAuthorSearch.Size = new Size(332, 27);
            txtAuthorSearch.TabIndex = 0;
            txtAuthorSearch.TextChanged += txtAuthorSearch_TextChanged;
            txtAuthorSearch.KeyDown += txtAuthorSearch_KeyDown;
            // 
            // tabManageBooks
            // 
            tabManageBooks.Controls.Add(panelManageBooks);
            tabManageBooks.Location = new Point(4, 29);
            tabManageBooks.Margin = new Padding(4, 5, 4, 5);
            tabManageBooks.Name = "tabManageBooks";
            tabManageBooks.Size = new Size(1592, 952);
            tabManageBooks.TabIndex = 3;
            tabManageBooks.Text = "Управление книгами";
            tabManageBooks.UseVisualStyleBackColor = true;
            // 
            // panelManageBooks
            // 
            panelManageBooks.Controls.Add(lblManageStatus);
            panelManageBooks.Controls.Add(btnRefreshManage);
            panelManageBooks.Controls.Add(btnCancelEdit);
            panelManageBooks.Controls.Add(btnSaveBook);
            panelManageBooks.Controls.Add(groupBoxBookDetails);
            panelManageBooks.Controls.Add(btnEditBook);
            panelManageBooks.Controls.Add(btnDeleteBook);
            panelManageBooks.Controls.Add(btnAddNewBook);
            panelManageBooks.Controls.Add(dgvManageBooks);
            panelManageBooks.Dock = DockStyle.Fill;
            panelManageBooks.Location = new Point(0, 0);
            panelManageBooks.Margin = new Padding(4, 5, 4, 5);
            panelManageBooks.Name = "panelManageBooks";
            panelManageBooks.Size = new Size(1592, 952);
            panelManageBooks.TabIndex = 0;
            // 
            // lblManageStatus
            // 
            lblManageStatus.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblManageStatus.ForeColor = Color.Green;
            lblManageStatus.Location = new Point(880, 23);
            lblManageStatus.Margin = new Padding(4, 0, 4, 0);
            lblManageStatus.Name = "lblManageStatus";
            lblManageStatus.Size = new Size(400, 38);
            lblManageStatus.TabIndex = 8;
            lblManageStatus.Text = "Готов к работе";
            lblManageStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnRefreshManage
            // 
            btnRefreshManage.Location = new Point(720, 23);
            btnRefreshManage.Margin = new Padding(4, 5, 4, 5);
            btnRefreshManage.Name = "btnRefreshManage";
            btnRefreshManage.Size = new Size(153, 38);
            btnRefreshManage.TabIndex = 7;
            btnRefreshManage.Text = "Обновить";
            btnRefreshManage.UseVisualStyleBackColor = true;
            btnRefreshManage.Click += btnRefreshManage_Click;
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.BackColor = Color.LightGray;
            btnCancelEdit.Enabled = false;
            btnCancelEdit.Location = new Point(1133, 854);
            btnCancelEdit.Margin = new Padding(4, 5, 4, 5);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.Size = new Size(147, 46);
            btnCancelEdit.TabIndex = 6;
            btnCancelEdit.Text = "Отмена";
            btnCancelEdit.UseVisualStyleBackColor = false;
            btnCancelEdit.Click += btnCancelEdit_Click;
            // 
            // btnSaveBook
            // 
            btnSaveBook.BackColor = Color.FromArgb(0, 123, 255);
            btnSaveBook.Enabled = false;
            btnSaveBook.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnSaveBook.ForeColor = Color.White;
            btnSaveBook.Location = new Point(977, 854);
            btnSaveBook.Margin = new Padding(4, 5, 4, 5);
            btnSaveBook.Name = "btnSaveBook";
            btnSaveBook.Size = new Size(147, 46);
            btnSaveBook.TabIndex = 5;
            btnSaveBook.Text = "Сохранить";
            btnSaveBook.UseVisualStyleBackColor = false;
            btnSaveBook.Click += btnSaveBook_Click;
            // 
            // groupBoxBookDetails
            // 
            groupBoxBookDetails.Controls.Add(numStockQuantity);
            groupBoxBookDetails.Controls.Add(label10);
            groupBoxBookDetails.Controls.Add(txtBookGenre);
            groupBoxBookDetails.Controls.Add(label9);
            groupBoxBookDetails.Controls.Add(numPublishedYear);
            groupBoxBookDetails.Controls.Add(label8);
            groupBoxBookDetails.Controls.Add(cmbBookAuthor);
            groupBoxBookDetails.Controls.Add(label7);
            groupBoxBookDetails.Controls.Add(numBookPrice);
            groupBoxBookDetails.Controls.Add(label6);
            groupBoxBookDetails.Controls.Add(txtBookTitle);
            groupBoxBookDetails.Controls.Add(label5);
            groupBoxBookDetails.Enabled = false;
            groupBoxBookDetails.Location = new Point(977, 62);
            groupBoxBookDetails.Margin = new Padding(4, 5, 4, 5);
            groupBoxBookDetails.Name = "groupBoxBookDetails";
            groupBoxBookDetails.Padding = new Padding(4, 5, 4, 5);
            groupBoxBookDetails.Size = new Size(600, 782);
            groupBoxBookDetails.TabIndex = 4;
            groupBoxBookDetails.TabStop = false;
            groupBoxBookDetails.Text = "Данные книги";
            // 
            // numStockQuantity
            // 
            numStockQuantity.Location = new Point(200, 425);
            numStockQuantity.Margin = new Padding(4, 5, 4, 5);
            numStockQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numStockQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numStockQuantity.Name = "numStockQuantity";
            numStockQuantity.Size = new Size(373, 27);
            numStockQuantity.TabIndex = 11;
            numStockQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label10.Location = new Point(23, 428);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(155, 18);
            label10.TabIndex = 10;
            label10.Text = "Количество на складе:";
            // 
            // txtBookGenre
            // 
            txtBookGenre.Location = new Point(200, 362);
            txtBookGenre.Margin = new Padding(4, 5, 4, 5);
            txtBookGenre.Name = "txtBookGenre";
            txtBookGenre.Size = new Size(373, 27);
            txtBookGenre.TabIndex = 9;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label9.Location = new Point(23, 365);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(54, 18);
            label9.TabIndex = 8;
            label9.Text = "Жанр:";
            // 
            // numPublishedYear
            // 
            numPublishedYear.Location = new Point(200, 298);
            numPublishedYear.Margin = new Padding(4, 5, 4, 5);
            numPublishedYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            numPublishedYear.Minimum = new decimal(new int[] { 1800, 0, 0, 0 });
            numPublishedYear.Name = "numPublishedYear";
            numPublishedYear.Size = new Size(373, 27);
            numPublishedYear.TabIndex = 7;
            numPublishedYear.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label8.Location = new Point(23, 301);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(112, 18);
            label8.TabIndex = 6;
            label8.Text = "Год издания:";
            // 
            // cmbBookAuthor
            // 
            cmbBookAuthor.FormattingEnabled = true;
            cmbBookAuthor.Location = new Point(200, 233);
            cmbBookAuthor.Margin = new Padding(4, 5, 4, 5);
            cmbBookAuthor.Name = "cmbBookAuthor";
            cmbBookAuthor.Size = new Size(373, 28);
            cmbBookAuthor.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label7.Location = new Point(23, 236);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(55, 18);
            label7.TabIndex = 4;
            label7.Text = "Автор:";
            // 
            // numBookPrice
            // 
            numBookPrice.DecimalPlaces = 2;
            numBookPrice.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numBookPrice.Location = new Point(200, 170);
            numBookPrice.Margin = new Padding(4, 5, 4, 5);
            numBookPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numBookPrice.Name = "numBookPrice";
            numBookPrice.Size = new Size(373, 27);
            numBookPrice.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label6.Location = new Point(23, 173);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(51, 18);
            label6.TabIndex = 2;
            label6.Text = "Цена:";
            // 
            // txtBookTitle
            // 
            txtBookTitle.Location = new Point(200, 105);
            txtBookTitle.Margin = new Padding(4, 5, 4, 5);
            txtBookTitle.Name = "txtBookTitle";
            txtBookTitle.Size = new Size(373, 27);
            txtBookTitle.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            label5.Location = new Point(23, 108);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(168, 18);
            label5.TabIndex = 0;
            label5.Text = "Название книги *:";
            // 
            // btnEditBook
            // 
            btnEditBook.BackColor = Color.FromArgb(255, 193, 7);
            btnEditBook.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnEditBook.Location = new Point(187, 23);
            btnEditBook.Margin = new Padding(4, 5, 4, 5);
            btnEditBook.Name = "btnEditBook";
            btnEditBook.Size = new Size(160, 38);
            btnEditBook.TabIndex = 3;
            btnEditBook.Text = "Редактировать";
            btnEditBook.UseVisualStyleBackColor = false;
            btnEditBook.Click += btnEditBook_Click;
            // 
            // btnDeleteBook
            // 
            btnDeleteBook.BackColor = Color.FromArgb(220, 53, 69);
            btnDeleteBook.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnDeleteBook.ForeColor = Color.White;
            btnDeleteBook.Location = new Point(357, 23);
            btnDeleteBook.Margin = new Padding(4, 5, 4, 5);
            btnDeleteBook.Name = "btnDeleteBook";
            btnDeleteBook.Size = new Size(160, 38);
            btnDeleteBook.TabIndex = 2;
            btnDeleteBook.Text = "Удалить";
            btnDeleteBook.UseVisualStyleBackColor = false;
            btnDeleteBook.Click += btnDeleteBook_Click;
            // 
            // btnAddNewBook
            // 
            btnAddNewBook.BackColor = Color.FromArgb(40, 167, 69);
            btnAddNewBook.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnAddNewBook.ForeColor = Color.White;
            btnAddNewBook.Location = new Point(17, 23);
            btnAddNewBook.Margin = new Padding(4, 5, 4, 5);
            btnAddNewBook.Name = "btnAddNewBook";
            btnAddNewBook.Size = new Size(160, 38);
            btnAddNewBook.TabIndex = 1;
            btnAddNewBook.Text = "Добавить книгу";
            btnAddNewBook.UseVisualStyleBackColor = false;
            btnAddNewBook.Click += btnAddNewBook_Click;
            // 
            // dgvManageBooks
            // 
            dgvManageBooks.AllowUserToAddRows = false;
            dgvManageBooks.AllowUserToDeleteRows = false;
            dgvManageBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvManageBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvManageBooks.Location = new Point(17, 62);
            dgvManageBooks.Margin = new Padding(4, 5, 4, 5);
            dgvManageBooks.Name = "dgvManageBooks";
            dgvManageBooks.ReadOnly = true;
            dgvManageBooks.RowHeadersVisible = false;
            dgvManageBooks.RowHeadersWidth = 51;
            dgvManageBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvManageBooks.Size = new Size(940, 838);
            dgvManageBooks.TabIndex = 0;
            dgvManageBooks.SelectionChanged += dgvManageBooks_SelectionChanged;
            // 
            // lblCurrentUser
            // 
            lblCurrentUser.BackColor = Color.LightGray;
            lblCurrentUser.Dock = DockStyle.Top;
            lblCurrentUser.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblCurrentUser.Location = new Point(0, 0);
            lblCurrentUser.Margin = new Padding(4, 0, 4, 0);
            lblCurrentUser.Name = "lblCurrentUser";
            lblCurrentUser.Size = new Size(1600, 46);
            lblCurrentUser.TabIndex = 1;
            lblCurrentUser.Text = "Покупатель: не выбран";
            lblCurrentUser.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1600, 1031);
            Controls.Add(tabControl);
            Controls.Add(lblCurrentUser);
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(1615, 1028);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Книжный магазин - Интернет-магазин";
            tabControl.ResumeLayout(false);
            tabCatalog.ResumeLayout(false);
            tabCatalog.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            tabMyOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            tabProcedures.ResumeLayout(false);
            tabProcedures.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAuthorSearch).EndInit();
            tabManageBooks.ResumeLayout(false);
            panelManageBooks.ResumeLayout(false);
            groupBoxBookDetails.ResumeLayout(false);
            groupBoxBookDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numStockQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPublishedYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBookPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvManageBooks).EndInit();
            ResumeLayout(false);
        }
    }
}