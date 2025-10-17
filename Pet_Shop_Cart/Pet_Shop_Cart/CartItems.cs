using System.ComponentModel;

namespace Pet_Shop_Cart
{
    // Model class for Cart Item
    public class CartItem : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private decimal price;
        private int quantity;
        private string variant;
        private string imageUrl;
        private bool isSelected;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public decimal Price
        {
            get => price;
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public string Variant
        {
            get => variant;
            set { variant = value; OnPropertyChanged(nameof(Variant)); }
        }

        public string ImageUrl
        {
            get => imageUrl;
            set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); }
        }

        public bool IsSelected
        {
            get => isSelected;
            set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}