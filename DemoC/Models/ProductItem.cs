using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.IO;

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
                if (string.IsNullOrWhiteSpace(Image))
                    return AssetsLoad("picture.png");

                string path = Path.Combine(AppContext.BaseDirectory, "Images", Image);

                if (!File.Exists(path))
                    return AssetsLoad(Image);

                return new Bitmap(path);
            }
        }

        private static Bitmap AssetsLoad(string filename)
        {
            Uri uri = new Uri($"avares://DemoC/Assets/{filename}");

            try
            {
                return new Bitmap(AssetLoader.Open(uri));
            }
            catch
            {
                return new Bitmap(AssetLoader.Open(new Uri($"avares://DemoC/Assets/picture.png")));
            }
        }
    }
}
