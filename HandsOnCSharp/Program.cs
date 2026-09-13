using System.Data.Common;
using System.Reflection;

namespace CSharpBasics
{
    //Interface for discountable products
    public interface IDiscountable
    {
        decimal ApplyDiscount(decimal percentage);
    }

    // Base Product class with fields, properties, and methodse

    class Product
    {
        //Private field
        private decimal _price;

        public string Name { get; set; }

        // Public property with additional logic in the setter
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value >= 0) _price = value;
                else throw new ArgumentOutOfRangeException("Price cannot be negative.");
            }
        }

        // Constructor

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        // Virtual method
        public virtual void DisplayProductDetails()
        {
            Console.WriteLine($"Product: {Name}, Price: {Price}");
        }

        // Static method to calculate discount
        public static decimal CalculateDiscount(decimal price, decimal discountPercentage)
        {
            return price - (price * discountPercentage / 100);
        }
    }

    // Subclass: Clothing (Discountable)
    class Clothing : Product, IDiscountable
    {
        // Property to store the size as an integer
        public int Size { get; set; }

        public Clothing(string name, decimal price, int size) : base(name, price)
        {
            Size = size;
        }

        // Method to convert size from int to size name

        public string GetSizeName()
        {
            return Size switch
            {
                1 => "Small",
                2 => "Medium",
                3 => "Large",
                _ => "Unknown Size"
            };
        }

        // Override method to include size details

        public override void DisplayProductDetails()
        {
            base.DisplayProductDetails();
            Console.WriteLine($"Size: {GetSizeName()}");
        }

        public decimal ApplyDiscount(decimal percentage)
        {
            Price = Product.CalculateDiscount(Price, percentage);
            return Price;
        }
    }

    class Program
    {
        static void Main()
        {
            List<Clothing> catalog = new List<Clothing>();
            // Creating a Clothing object and demonstrating properties, methods, and polymorphism
            catalog.Add(new Clothing("Super Cool T-shirt", 49.99m, 2));
            catalog.Add(new Clothing("Short Pants", 79.99m, 1));
            catalog.Add(new Clothing("Short Pants", 82.99m, 2));

            for (int i = 0; i < catalog.Count; i++)
            {
                catalog[i].DisplayProductDetails();
            }

            foreach (Clothing item in catalog)
            {
                item.DisplayProductDetails();
            }

            // Applying discount to the Clothing product
            decimal discountedPrice = catalog[0].ApplyDiscount(10);
            Console.WriteLine($"T-shirt price after discount: {discountedPrice:C}");
            Console.WriteLine(Product.CalculateDiscount(29.50m, 0.1m));

        }
    }

}
