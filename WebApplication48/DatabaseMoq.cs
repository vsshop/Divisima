using WebApplication48.Models;

namespace WebApplication48
{
    public class DatabaseMoq
    {
        public static List<ProductModel> Products { get; set; }
        public static List<UserModel> User { get; set; }
        static DatabaseMoq()
        {
			User = new List<UserModel>();
			Products = new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = Guid.NewGuid(),
                    Image = "./img/product/5.jpg",
                    Title = "Black and White Stripes Dress",
                    Price = 33.3
                },
                new ProductModel()
                {
                    Id = Guid.NewGuid(),
                    Image = "./img/product/6.jpg",
                    Title = "Black",
                    Price = 71
                },
                new ProductModel()
                {
                    Id = Guid.NewGuid(),
                    Image = "./img/product/7.jpg",
                    Title = "Dress",
                    Price = 35.5
                },
                new ProductModel()
                {
                    Id = Guid.NewGuid(),
                    Image = "./img/product/8.jpg",
                    Title = "Stripes Dress",
                    Price = 35.5
                },
                new ProductModel()
                {
                    Id = Guid.NewGuid(),
                    Image = "./img/product/9.jpg",
                    Title = "Black and White",
                    Price = 35.5
                },
                new ProductModel()
                {
                    Id = Guid.NewGuid(),
                    Image = "./img/product/10.jpg",
                    Title = "White",
                    Price = 35.5
                },
                new ProductModel()
                {
                    Id = Guid.NewGuid(),
                    Image = "./img/product/11.jpg",
                    Title = "and",
                    Price = 35.5
                },
                new ProductModel()
                {
                    Id = Guid.NewGuid(),
                    Image = "./img/product/12.jpg",
                    Title = "Black and White Stripes Dress",
                    Price = 35.5
                },
            };
        }
    }
}
