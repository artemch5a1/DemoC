using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace DemoC.Models
{
    public partial class Product
    {
        public string CategoryAndTitle => $"{Category.Title} | {Title}";


        public decimal PriceWithSale => Price - (Price * (decimal)((decimal)Sale / (decimal)100));

        public bool IsSaleNotNull => Sale > 0;

        public bool SaleMoreThen => Sale > 15;

        public Bitmap BitmapImage
        {
            get
            {
                var filename = string.IsNullOrWhiteSpace(Image) ? "picture.png" : Image;

                Uri uri = new Uri($"avares://DemoC/Assets/{filename}");

                return new Bitmap(AssetLoader.Open(uri));
            }
        }
    }
}
