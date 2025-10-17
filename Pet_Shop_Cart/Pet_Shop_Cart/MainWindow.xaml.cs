using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Pet_Shop_Cart
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<CartItem> cartItems;
        private bool isAllSelected = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCart();
        }

        private void InitializeCart()
        {
            cartItems = new ObservableCollection<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    Name = "Áo Cho Chó Mèo Hình Gấu Màu Pastel",
                    Price = 50000,
                    Quantity = 1,
                    Variant = "Trắng, Size M",
                    ImageUrl = "https://paddy.vn/cdn/shop/files/ao-cho-cho-meo-hinh-gau-mau-pastel-paddy-pet-shop_2.jpg?v=1704522195"
                },
                new CartItem
                {
                    Id = 2,
                    Name = "Balo Cho Chó Mèo Phi Hành Gia Hình Mặt Mèo",
                    Price = 230000,
                    Quantity = 1,
                    Variant = "Xanh lá",
                    ImageUrl = "https://paddy.vn/cdn/shop/files/df6bf19f-a453-4761-a049-0d6762e0d543-jpeg.webp?v=1697452448"
                },
                new CartItem
                {
                    Id = 3,
                    Name = "Máy Tinh Dầu Feliway Classic Giảm Căng Thẳng Cho Chó Mèo",
                    Price = 830000,
                    Quantity = 1,
                    ImageUrl = "https://paddy.vn/cdn/shop/files/1_c9cbc731-414a-4408-b9a5-3be1f264dee5.jpg?v=1712919481"
                },
                new CartItem
                {
                    Id = 4,
                    Name = "Pate Cho Mèo Trưởng Thành Me-O Delite Gói 70g",
                    Price = 17000,
                    Quantity = 1,
                    Variant = "Cá Ngừ Cá Hồi Gravy",
                    ImageUrl = "https://paddy.vn/cdn/shop/files/pate-cho-meo-truong-thanh-me-o-delite-goi-70g-5.jpg?v=1760428761"
                },
                new CartItem
                {
                    Id = 5,
                    Name = "Đồ Chơi Cho Mèo Fofos Lông Vũ",
                    Price = 140000,
                    Quantity = 1,
                    Variant = "Lắc Lư (Lắp Pin)",
                    ImageUrl = "https://paddy.vn/cdn/shop/files/do-choi-cho-meo-fofos-long-vu_3.webp?v=1758599542"
                }
            };

            // Subscribe to property changes
            foreach (var item in cartItems)
            {
                item.PropertyChanged += CartItem_PropertyChanged;
            }

            CartItemsControl.ItemsSource = cartItems;
            UpdateSummary();
        }

        private void CartItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected" || e.PropertyName == "Quantity")
            {
                UpdateSummary();
            }
        }

        private void UpdateSummary()
        {
            var selectedItems = cartItems.Where(i => i.IsSelected).ToList();
            int totalItems = selectedItems.Sum(i => i.Quantity);
            decimal totalPrice = selectedItems.Sum(i => i.Price * i.Quantity);

            TotalItemsText.Text = $"{totalItems} sản phẩm";
            TotalPriceText.Text = $"{totalPrice:N0}đ";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back or close window
            this.Close();
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            isAllSelected = !isAllSelected;
            foreach (var item in cartItems)
            {
                item.IsSelected = isAllSelected;
            }
        }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = cartItems.Where(i => i.IsSelected).ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa {selectedItems.Count} sản phẩm đã chọn?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                foreach (var item in selectedItems)
                {
                    cartItems.Remove(item);
                }
                isAllSelected = false;
                UpdateSummary();
            }
        }

        private void DecreaseQty_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var item = button?.Tag as CartItem;
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
            }
        }

        private void IncreaseQty_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var item = button?.Tag as CartItem;
            if (item != null)
            {
                item.Quantity++;
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var item = button?.Tag as CartItem;

            if (item != null)
            {
                var result = MessageBox.Show(
                    "Bạn có chắc muốn xóa sản phẩm?",
                    "Xác nhận",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    cartItems.Remove(item);
                    UpdateSummary();
                }
            }
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = cartItems.Where(i => i.IsSelected).ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sản phẩm để đặt mua",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBox.Show($"Đặt hàng thành công {selectedItems.Count} sản phẩm!",
                "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}